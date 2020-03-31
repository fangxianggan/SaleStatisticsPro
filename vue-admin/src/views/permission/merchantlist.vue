<template>
  <div class="app-container">
    <div class="filter-container">
      <el-form :inline="true" :model="filterModel">
        <el-form-item label="商户名称">
          <el-input
            v-model="filterModel.merchantName.value"
            placeholder="商户名称"
            style="width: 200px;"
            class="filter-item"
            @keyup.enter.native="handleFilter"
          />
        </el-form-item>
        <el-form-item label="添加时间">
          <el-date-picker
            v-model="filterModel.createTime.value"
            :picker-options="daterangeOptions"
            value-format="yyyy-MM-dd"
            range-separator="至"
            start-placeholder="开始时间"
            end-placeholder="结束时间"
            style="width: 350px;"
            type="daterange"
            placement="bottom-end"
            @keyup.enter.native="handleFilter"
          />
        </el-form-item>
        <el-form-item>
          <el-button
            v-waves
            class="filter-item"
            type="primary"
            icon="el-icon-search"
            @click="handleFilter"
          >查询</el-button>
          <el-button
            class="filter-item"
            style="margin-left: 10px;"
            type="primary"
            icon="el-icon-edit"
            @click="handleCreate"
          >新增</el-button>
        </el-form-item>
      </el-form>
      <!--<el-button v-waves :loading="downloadLoading" class="filter-item" type="primary" icon="el-icon-download" @click="handleDownload">
        导出
      </el-button>-->
    </div>

    <el-table
      :key="tableKey"
      v-loading="listLoading"
      :data="list"
      border
      fit
      highlight-current-row
      style="width: 100%;"
      @sort-change="sortChange"
    >
      <el-table-column label="商户号" prop="merchantNo" sortable="custom" width="180">
        <template slot-scope="{row}">
          <span>{{ row.merchantNo }}</span>
        </template>
      </el-table-column>
      <el-table-column label="商户名称" prop="merchantName" sortable="custom" width="180">
        <template slot-scope="{row}">
          <span>{{ row.merchantName }}</span>
        </template>
      </el-table-column>
      <el-table-column label="商户手机号" prop="merchantPhone" sortable="custom" width="180">
        <template slot-scope="{row}">
          <span>{{ row.merchantPhone }}</span>
        </template>
      </el-table-column>
      <el-table-column label="添加时间" prop="createTime" sortable="custom" width="180px">
        <template slot-scope="{row}">
          <span>{{ row.createTime|formatTime}}</span>
        </template>
      </el-table-column>
      <el-table-column label="修改时间" width="180px">
        <template slot-scope="{row}">
          <span>{{ row.updateTime|formatTime}}</span>
        </template>
      </el-table-column>
      <el-table-column label="备注信息" min-width="100px">
        <template slot-scope="{row}">
          <span>{{ row.remark }}</span>
        </template>
      </el-table-column>

      <el-table-column label="操作" width="280" class-name="small-padding fixed-width">
        <template slot-scope="{row,$index}">
          <el-button type="primary" size="mini" @click="handleUpdate(row)">修改</el-button>

          <el-button
            v-if="row.status!='deleted'"
            size="mini"
            type="danger"
            @click="handleDelete(row,$index)"
          >删除</el-button>

          <el-button type="success" size="mini" @click="handleSetRole(row)">配置角色权限</el-button>
        </template>
      </el-table-column>
    </el-table>

    <pagination
      v-show="total>0"
      :total="total"
      :page.sync="listQuery.page"
      :limit.sync="listQuery.limit"
      @pagination="getList"
    />

    <el-dialog v-el-drag-dialog :title="dialogTitle" :visible.sync="dialogFormVisible">
      <el-form ref="dataForm" :model="temp" label-position="right" label-width="100px">
        <el-row>
          <el-col :span="24">
            <el-form-item label="商户编码" prop="merchantCode" :rules="rules.checkNull">
              <el-input v-model="temp.merchantCode" />
            </el-form-item>
          </el-col>
        </el-row>

        <el-row>
          <el-col :span="24">
            <el-form-item label="商户名称" prop="merchantName" :rules="rules.checkNull">
              <el-input v-model="temp.merchantName" />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="24">
            <el-form-item label="备注信息">
              <el-input v-model="temp.remark" type="textarea" :autosize="{minRows: 2,maxRows: 5}" />
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogFormVisible = false">关闭</el-button>
        <el-button type="primary" @click="dialogStatus==='create'?createData():updateData()">保存</el-button>
      </div>
    </el-dialog>

    <el-dialog v-el-drag-dialog :title="dialogTitle" :visible.sync="dialogSetRoleVisible">
      <el-form
        ref="setRoleForm"
        :model="setRoleTemp"
        label-position="right"
        label-width="100px"
      >
        <el-row>
          <el-col :span="24">
            <el-form-item label="商户名称">
              <el-input v-model="setRoleTemp.merchantName" readonly />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="24">
            <el-form-item label="角色权限">
              <template>
                <el-transfer 
                 v-model="setRoleList"
                 :data="getAllRoleList"
                 :titles="['未设角色', '已设角色']"
                 :button-texts="['到左边', '到右边']"
                  @change="handleSetRoleChange"
                 ></el-transfer>
              </template>
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogSetRoleVisible = false">关闭</el-button>
        <el-button type="primary" @click="setRoleData()">保存</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import waves from "@/directive/waves"; // waves directive
import Pagination from "@/components/Pagination"; // secondary package based on el-pagination
import myAction from "@/utils/baseutil";
import {
  getMerchantRolePermission,
  getRoleTransferData,
  setMerchantRolePermission
} from "@/api/merchant";
var currentData = {
  filterModel: {
    merchantName: {
      field: "MerchantName",
      method: "Contains",
      value: "",
      prefix: "",
      operator: "And"
    },
    createTime: {
      field: "CreateTime",
      method: "Between",
      value: "",
      prefix: "",
      operator: "And"
    }
  },
  temp: {
    merchantNo: "",
    merchantName: "",
    merchantPassword: "",
    merchantPhone: "",
    id: "",
    createTime: "",
    updateTime: ""
  },
  setRoleTemp: {
    merchantNo: "",
    merchantName: "",
    roleCodes: []
  },
  orderArr: []
};
var data = $.extend(false, myAction.setBaseVueData, currentData);
export default {
  name: "merchantlist",
  components: { Pagination },
  directives: { waves },
  filters: {
    formatTime: function(val) {
      return myAction.formatTime(val);
    }
  },
  data() {
    let custRules = {};
    data.rules = $.extend(false, myAction.setBaseValidateRules(), custRules);
    data.dialogSetRoleVisible = false;
    data.setRoleList=[];
    data.getAllRoleList=[];
    return data;
  },
  created() {
    this.getList();
    
  },
  methods: {
    getList() {
      this.listLoading = true;
      var url = "/MerchantInfo/_GetPageList";
      var data = myAction.getQueryModel(
        this.listQuery.limit,
        this.listQuery.page,
        this.total,
        this.filterModel,
        this.orderArr
      );
      //var data = myAction.getItemsModel(this.filterModel);
      this.$ajax(url, data).then(response => {
        this.list = response.data;
        this.total = response.total;

        // Just to simulate the time of the request
        setTimeout(() => {
          this.listLoading = false;
        }, 1.5 * 300);
      });
    },
   
    //查询处理的事件
    handleFilter() {
      this.listQuery.page = 1;
      this.getList();
    },
    //排序事件
    sortChange(data) {
      this.orderArr = [];
      this.orderArr.push(data);
      this.handleFilter();
    },
    resetTemp() {
      // let merchantCode = myAction.newGuid();
      this.temp = {
        id: "",
        merchantCode: "",
        merchantName: "",
        createTime: "",
        createUserCode: "",
        updateTime: "",
        updateUserCode: "",
        remark: ""
      };
    },
    //新增 页面
    handleCreate() {
      this.resetTemp();
      this.dialogStatus = "create";
      this.dialogTitle = "新增商户";
      this.dialogFormVisible = true;
      this.$nextTick(() => {
        this.$refs["dataForm"].clearValidate();
      });
    },
    //提交数据
    createData() {
      this.$refs["dataForm"].validate(valid => {
        if (valid) {
          var url = "/Merchant/_SaveData";
          var data = this.temp;
          this.$ajax(url, data).then(response => {
            var d = response.data;
            this.list.unshift(d);
            this.total++;
            this.dialogFormVisible = false;
            myAction.getNotifyFunc(response, this);
          });
        }
      });
    },
    //修改 页面
    handleUpdate(row) {
      this.temp = Object.assign({}, row); // copy obj
      this.dialogStatus = "update";
      this.dialogTitle = "修改商户";
      this.dialogFormVisible = true;
      this.$nextTick(() => {
        this.$refs["dataForm"].clearValidate();
      });
    },
    //提交修改数据
    updateData() {
      this.$refs["dataForm"].validate(valid => {
        if (valid) {
          const tempData = Object.assign({}, this.temp);
          var url = "/Merchant/_SaveData";
          var data = tempData;
          this.$ajax(url, data).then(response => {
            var d = response.data;
            const index = this.list.findIndex(v => v.id === this.temp.id);
            this.list.splice(index, 1, d);
            this.dialogFormVisible = false;
            myAction.getNotifyFunc(response, this);
          });
        }
      });
    },
    //删除数据
    handleDelete(row, index) {
      var title = '<span style="color: red;">是否要删除这条数据?</span>';
      this.$confirm(title, "提示", {
        dangerouslyUseHTMLString: true,
        type: "warning",
        confirmButtonText: "确定",
        cancelButtonText: "取消"
      })
        .then(() => {
          this.$ajax(
            "/Merchant/GetIsDeleteFlag",
            { code: row.merchantCode },
            { method: "get" }
          ).then(d => {
            if (d.resultSign == 1) {
              myAction.getNotifyFunc(d, this);
              return false;
            }
            let url = "/Merchant/_DelData";
            let data = { id: row.id };
            this.$ajax(url, data, { method: "get" }).then(response => {
              if (response.resultSign == 0) {
                this.list.splice(index, 1);
                this.total--;
              }
              myAction.getNotifyFunc(response, this);
            });
          });
        })
        .catch(action => {});
    },
    //配置角色权限
    handleSetRole(row) {
      getRoleTransferData(row.merchantNo).then(res => {
        this.setRoleTemp.merchantNo = row.merchantNo;
        this.setRoleTemp.merchantName = row.merchantName;
      
        this.dialogStatus = "update";
        this.dialogTitle = "配置角色权限";
        this.dialogSetRoleVisible = true;

        this.$nextTick(() => {
          this.getAllRoleList=res.data.allRoleList;
          this.setRoleList=res.data.setRoleList;
          this.$refs["setRoleForm"].clearValidate();
        });
      });
    },
    //权限数据
    setRoleData() {
      this.$refs["setRoleForm"].validate(valid => {
        if (valid) {
          this.dialogSetRoleVisible = false;
          let merchantNo = this.setRoleTemp.merchantNo;
          let roleCodes = this.setRoleList;
          setMerchantRolePermission(merchantNo, roleCodes).then(res => {
            var d = res.data;
            myAction.getNotifyFunc(res, this);
          });
        }
      });
    },
    handleSetRoleChange(value, direction, movedKeys)
    {
     this.setRoleTemp.roleCodes=value;

    }
  }
};
</script>
<style scoped>
.el-form-item {
  margin-left: 22px;
}
</style>


