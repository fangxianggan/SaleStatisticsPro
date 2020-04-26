<template>
  <div class="app-container">
    <div class="filter-container">
      <el-collapse accordion v-model="activeName">
        <el-collapse-item title="查询条件" name="1">
          <el-form :inline="true" :model="filterModel">
            <el-form-item label="数据备份名称">
              <el-input
                v-model="filterModel.backUpName.value"
                placeholder="数据备份名称"
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
                style="width: 250px;"
                type="daterange"
                placement="bottom-end"
                @keyup.enter.native="handleFilter"
              />
            </el-form-item>
          </el-form>
        </el-collapse-item>
      </el-collapse>
    </div>
    <el-row class="el-table-header-buttom">
      <el-button
        v-waves
        class="filter-item"
        type="primary"
        icon="el-icon-search"
        @click="handleFilter"
      >查询</el-button>

      <el-button class="filter-item" type="primary" icon="el-icon-edit" @click="handleCreate">新增备份</el-button>
    </el-row>
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
      <el-table-column label="备份时间" prop="createTime" sortable="custom" width="160px">
        <template slot-scope="{row}">
          <span>{{ row.createTime|formatTime}}</span>
        </template>
      </el-table-column>
      <el-table-column label="备份数据库名" prop="dbName" sortable="custom">
        <template slot-scope="{row}">
          <span>{{ row.dbName }}</span>
        </template>
      </el-table-column>
      <el-table-column label="备份名称" prop="backUpName" sortable="custom">
        <template slot-scope="{row}">
          <span>{{ row.backUpName }}</span>
        </template>
      </el-table-column>
      <el-table-column label="备份路径" prop="path" sortable="custom">
        <template slot-scope="{row}">
          <span>{{ row.path }}</span>
        </template>
      </el-table-column>

      <el-table-column label="备注信息">
        <template slot-scope="{row}">
          <span>{{ row.remark }}</span>
        </template>
      </el-table-column>

      <el-table-column label="操作" width="250" class-name="small-padding fixed-width">
        <template slot-scope="{row,$index}">
          <el-button type="success" size="mini"   @click="restoreData(row,$index)">数据还原</el-button>
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
            <el-form-item label="数据库名" prop="dbName" :rules="rules.checkNull">
              <el-select v-model="temp.dbName" clearable filterable placeholder="选择数据库">
                <el-option
                  v-for="(item,index) in dbNameSList"
                  :key="index"
                  :label="item.label"
                  :value="item.value"
                ></el-option>
              </el-select>
            </el-form-item>
          </el-col>
        </el-row>

        <el-row>
          <el-col :span="24">
            <el-form-item label="备份名称" prop="backUpName" :rules="rules.checkNull">
              <el-input v-model="temp.backUpName" />
            </el-form-item>
          </el-col>
        </el-row>

        <el-row>
          <el-col :span="24">
            <el-form-item label="备份路径" prop="path" :rules="rules.checkNull">
              <el-input v-model="temp.path" />
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
  </div>
</template>

<script>
import waves from "@/directive/waves"; // waves directive
import Pagination from "@/components/Pagination"; // secondary package based on el-pagination
import myAction from "@/utils/baseutil";
var currentData = {
  filterModel: {
    backUpName: {
      field: "BackUpName",
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
    backUpName: "",
    path: "",
    id: 0,
    dbName: "",
    createTime: "",
    createUserCode: "",
    updateTime: "",
    updateUserCode: "",
    remark: ""
  },
  orderArr: []
};
var data = $.extend(false, myAction.setBaseVueData, currentData);
export default {
  name: "DataBackuplist",
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
    data.dbNameSList = [];
    data.defaultPath = "";
    return data;
  },
  created() {
    this.getList();
    this.getDBSelectList();
    this.getDBDefaultPath();
  },
  methods: {
    getList() {
      this.listLoading = true;
      var url = "/DataBackup/_GetPageList";
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
    //
    getDBSelectList() {
      let url = "/DataBackup/GetSelectViewModelList";
      let data = {};
      this.$ajax(url, data, { method: "get" }).then(response => {
        this.dbNameSList = response.data;
      });
    },
    getDBDefaultPath() {
      let url = "/DataBackup/getDBDefaultPath";
      let data = {};
      this.$ajax(url, data, { method: "get" }).then(response => {
        this.defaultPath = response.data;
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
      let name = myAction.formatTime(new Date(), "yyyyMMddhhmmssqS");
      this.temp = {
        backUpName: name + ".bak",
        path: this.defaultPath,
        id: 0,
        dbName: "",
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
      this.dialogTitle = "新增数据备份";
      this.dialogFormVisible = true;
      this.$nextTick(() => {
        this.$refs["dataForm"].clearValidate();
      });
    },
    //提交数据
    createData() {
      this.$refs["dataForm"].validate(valid => {
        if (valid) {
          var url = "/DataBackup/DataBaseBackup";
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
    //数据恢复
    restoreData(row, index) {
      var title = '<span style="color: red;">是否要恢复这次备份吗?</span>';
      this.$confirm(title, "提示", {
        dangerouslyUseHTMLString: true,
        type: "warning",
        confirmButtonText: "确定",
        cancelButtonText: "取消"
      })
        .then(() => {
          let url = "/DataBackup/DestoreData";
          let data = { id: row.id };
          this.$ajax(url, data, { method: "get" }).then(response => {
            if (response.resultSign == 0) {
              window.location.href="/login";
            }
            myAction.getNotifyFunc(response, this);
          });
        })
        .catch(action => {});
    }
  }
};
</script>
<style scoped>
.el-form-item {
  margin-left: 22px;
}
</style>


