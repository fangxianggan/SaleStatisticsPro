<template>
  <div class="app-container">
    <div class="filter-container">
      <el-collapse accordion v-model="activeName">
        <el-collapse-item title="查询条件" name="1">
          <el-form :inline="true" :model="filterModel">
            <el-form-item label="产品名称">
              <el-input
                v-model="filterModel.productName.value"
                placeholder="产品名称"
                class="filter-item"
                @keyup.enter.native="handleFilter"
              />
            </el-form-item>
            <!--<el-form-item label="供应商名称">
          <el-select v-model="filterModel.businessCode.value" clearable filterable placeholder="请选择供应商" @change="businessCodeChange">
            <el-option v-for="(item,index) in businessCodeSList"
                       :key="index"
                       :label="item.label"
                       :value="item.value">
            </el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="品牌">
          <el-select v-model="filterModel.brandCode.value" clearable filterable placeholder="请选择品牌" @change="brandCodeChange">
            <el-option v-for="(item,index) in brandCodeSList"
                       :key="index"
                       :label="item.label"
                       :value="item.value">
            </el-option>
          </el-select>
        </el-form-item>

        <el-form-item label="产品分类">
          <el-select v-model="filterModel.categoryCode.value" clearable filterable placeholder="请选择产品分类" @change="categoryCodeChange">
            <el-option v-for="(item,index) in categoryCodeSList"
                       :key="index"
                       :label="item.label"
                       :value="item.value">
            </el-option>
          </el-select>
        </el-form-item>

        <el-form-item label="产品型号">
          <el-input v-model="filterModel.productTypeName.value" placeholder="请输入产品型号" />
        </el-form-item>

        <el-form-item label="产品规格">
          <el-select v-model="filterModel.specsCode.value" clearable filterable placeholder="请选择产品规格" @change="specsCodeChange">
            <el-option v-for="(item,index) in specsCodeSList"
                       :key="index"
                       :label="item.label"
                       :value="item.value">
            </el-option>
          </el-select>
        </el-form-item>

        <el-form-item label="产品颜色">
          <el-input v-model="filterModel.productColor.value" placeholder="请输入产品颜色" />
            </el-form-item>-->

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

      <el-button class="filter-item" type="primary" icon="el-icon-edit" @click="handleCreate">新增</el-button>
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
      <!--<el-table-column type="expand">
        <template slot-scope="{row}">
          <el-form label-position="left" inline class="demo-table-expand">
            <el-form-item label="供应商名称">
              <span>{{ row.businessName }}</span>
            </el-form-item>
            <el-form-item label="品牌名称">
              <span>{{ row.brandName }}</span>
            </el-form-item>
            <el-form-item label="型号">
              <span>{{ row.productTypeName }}</span>
            </el-form-item>
            <el-form-item label="规格">
              <span>{{ row.specsName }}</span>
            </el-form-item>
            <el-form-item label="颜色">
              <span>{{ row.productColor  }}</span>
            </el-form-item>
            <el-form-item label="单位">
              <span>{{ row.unitName  }}</span>
            </el-form-item>
            <el-form-item label="修改时间">
              <span>{{ row.updateTime|formatTime  }}</span>
            </el-form-item>
            <el-form-item label="备注信息">
              <span>{{ row.remark  }}</span>
            </el-form-item>
          </el-form>
        </template>
      </el-table-column>-->
      <el-table-column label="产品简码" prop="simpleCode" sortable="custom" width="180">
        <template slot-scope="{row}">
          <span>{{ row.simpleCode }}</span>
        </template>
      </el-table-column>
      <el-table-column label="产品名称" prop="productName" sortable="custom" width="180">
        <template slot-scope="{row}">
          <span>{{ row.productName }}</span>
        </template>
      </el-table-column>
      <el-table-column label="分类名称" prop="categoryName" sortable="custom" width="180">
        <template slot-scope="{row}">
          <span>{{ row.categoryName }}</span>
        </template>
      </el-table-column>
      <el-table-column label="添加时间" prop="createTime" sortable="custom" width="180px">
        <template slot-scope="{row}">
          <span>{{ row.createTime|formatTime}}</span>
        </template>
      </el-table-column>

      <el-table-column label="备注信息">
        <template slot-scope="{row}">
          <span>{{ row.remark }}</span>
        </template>
      </el-table-column>

      <el-table-column label="操作" width="200" class-name="small-padding fixed-width">
        <template slot-scope="{row,$index}">
          <el-button type="primary" size="mini" @click="handleUpdate(row)">修改</el-button>

          <el-button
            v-if="row.status!='deleted'"
            size="mini"
            type="danger"
            @click="handleDelete(row,$index)"
          >删除</el-button>
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
            <el-form-item label="产品编码">
              <el-input v-model="temp.productCode" :readonly="true" />
            </el-form-item>
          </el-col>

          <el-col :span="24">
            <el-form-item label="产品分类" prop="categoryCode" :rules="rules.checkNull">
              <el-select
                v-model="temp.categoryCode"
                clearable
                filterable
                placeholder="请选择产品分类"
                @focus="getcategoryCodeSList"
              >
                <el-option
                  v-for="(item,index) in categoryCodeSList"
                  :key="index"
                  :label="item.label"
                  :value="item.value"
                ></el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :span="24">
            <el-form-item label="产品简码" prop="simpleCode" :rules="rules.simpleCode ">
              <el-input v-model="temp.simpleCode" placeholder="请输入产品简码" />
            </el-form-item>
          </el-col>

          <el-col :span="24">
            <el-form-item label="产品名称" prop="productName" :rules="rules.checkNull">
              <el-input v-model="temp.productName" placeholder="请输入产品名称" />
            </el-form-item>
          </el-col>
        </el-row>

        <!--<el-row>
          <el-col :span="8">
            <el-form-item label="供应商" prop="businessCode" :rules="rules.checkNull">
              <el-select v-model="temp.businessCode" clearable filterable placeholder="请选择供应商" @change="businessCodeChange">
                <el-option v-for="(item,index) in businessCodeSList"
                           :key="index"
                           :label="item.label"
                           :value="item.value">
                </el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :span="8">
            <el-form-item label="品牌" prop="brandCode" :rules="rules.checkNull">
              <el-select v-model="temp.brandCode" clearable filterable placeholder="请选择品牌" @change="brandCodeChange">
                <el-option v-for="(item,index) in brandCodeSList"
                           :key="index"
                           :label="item.label"
                           :value="item.value">
                </el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :span="8">
            <el-form-item label="产品分类" prop="categoryCode" :rules="rules.checkNull">
              <el-select v-model="temp.categoryCode" clearable filterable placeholder="请选择产品分类" @change="categoryCodeChange">
                <el-option v-for="(item,index) in categoryCodeSList"
                           :key="index"
                           :label="item.label"
                           :value="item.value">
                </el-option>
              </el-select>
            </el-form-item>
          </el-col>
        </el-row>

        <el-row>

          <el-col :span="8">
            <el-form-item label="产品型号" >
              <el-input v-model="temp.productTypeName" placeholder="请输入产品型号" />
            </el-form-item>
          </el-col>

          <el-col :span="8">
            <el-form-item label="产品规格" prop="specsCode" :rules="rules.checkNull">
              <el-select v-model="temp.specsCode" clearable filterable placeholder="请选择产品规格" @change="specsCodeChange">
                <el-option v-for="(item,index) in specsCodeSList"
                           :key="index"
                           :label="item.label"
                           :value="item.value">
                </el-option>
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :span="8">
            <el-form-item label="产品颜色">
              <el-input v-model="temp.productColor" placeholder="请输入产品颜色" />
            </el-form-item>
          </el-col>
        </el-row>-->
        <el-row>
          <!--<el-col :span="8">
            <el-form-item label="单位" prop="unitCode" :rules="rules.checkNull">
              <el-select v-model="temp.unitCode" clearable filterable placeholder="请选择单位" @change="unitCodeChange">
                <el-option v-for="(item,index) in unitCodeSList"
                           :key="index"
                           :label="item.label"
                           :value="item.value">
                </el-option>
              </el-select>
            </el-form-item>
          </el-col>-->

          <el-col :span="24">
            <el-form-item label="备注信息">
              <el-input
                v-model="temp.remark"
                placeholder="请输入备注信息"
                type="textarea"
                :autosize="{minRows: 2,maxRows: 5}"
              />
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
    productName: {
      field: "ProductName",
      method: "Contains",
      value: "",
      prefix: "",
      operator: "And"
    },
    //businessCode:
    //{
    //  field: "BusinessCode",
    //  method: "Equal",
    //  value: "",
    //  prefix: "",
    //  operator: "And"
    //},
    //brandCode:
    //{
    //  field: "BrandCode",
    //  method: "Equal",
    //  value: "",
    //  prefix: "",
    //  operator: "And"
    //},
    //categoryCode:
    //{
    //  field: "CategoryCode",
    //  method: "Equal",
    //  value: "",
    //  prefix: "",
    //  operator: "And"
    //},
    //productTypeName:
    //{
    //  field: "ProductTypeName",
    //  method: "Contains",
    //  value: "",
    //  prefix: "",
    //  operator: "And"
    //},
    //specsCode:
    //{
    //  field: "SpecsCode",
    //  method: "Equal",
    //  value: "",
    //  prefix: "",
    //  operator: "And"
    //},
    //productColor:
    //{
    //  field: "ProductColor",
    //  method: "Contains",
    //  value: "",
    //  prefix: "",
    //  operator: "And"
    //},
    createTime: {
      field: "CreateTime",
      method: "Between",
      value: "",
      prefix: "",
      operator: "And"
    }
  },
  temp: {
    productCode: "",
    simpleCode: "",
    productName: "",
    id: 0,
    createTime: "",
    createUserCode: "",
    updateTime: "",
    updateUserCode: "",
    remark: ""
  },
  orderArr: [],
  //businessCodeSList: [],
  //brandCodeSList: [],
  categoryCodeSList: []
  //specsCodeSList: [],
  //unitCodeSList: []
};
var data = $.extend(false, myAction.setBaseVueData, currentData);

export default {
  name: "productlist",
  components: { Pagination },
  directives: { waves },
  filters: {
    formatTime: function(val) {
      return myAction.formatTime(val);
    }
  },
  data() {
    let custRules = {
      simpleCode: [
        { required: true, message: "请输入产品简码", trigger: "blur" },
        {
          validator: (rule, value, callback) => {
            let obj = {
              url: "/Product/GetSimpleCodeIsExist",
              data: { id: this.temp.id, simpleCode: value },
              value: value,
              method: "get"
            };
            this.checkRemoteData(obj, callback);
          },
          trigger: ["change", "blur"]
        }
      ]
    };
    data.rules = $.extend(false, myAction.setBaseValidateRules(), custRules);
    return data;
  },
  created() {
    this.getList();
    //this.getbusinessCodeSList()
    //this.getbrandCodeSList()
    //this.getcategoryCodeSList()
    //this.getspecsCodeSList()
    //this.getunitCodeSList()
  },
  methods: {
    //初始化列表数据
    getList() {
      this.listLoading = true;
      var url = "/Product/GetProductViewModelPageList";
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
    //select数据
    //getbusinessCodeSList() {
    //  let url = "/Business/GetSelectBusinessData";
    //  let data = {};
    //  this.$ajax(url, data, { method: "get" }).then(response => {
    //    this.businessCodeSList = response.data;
    //  })
    //},
    //getbrandCodeSList() {
    //  let url = "/Brand/GetSelectBrandData";
    //  let data = {};
    //  this.$ajax(url, data, { method: "get" }).then(response => {
    //    this.brandCodeSList = response.data;
    //  })
    //},
    getcategoryCodeSList() {
      let url = "/Category/GetSelectViewModelList";
      let data = {};
      this.$ajax(url, data, { method: "get" }).then(response => {
        this.categoryCodeSList = response.data;
      });
    },
    //getspecsCodeSList() {
    //  let url = "/Specs/GetSelectSpecsData";
    //  let data = {};
    //  this.$ajax(url, data, { method: "get" }).then(response => {
    //    this.specsCodeSList = response.data;
    //  })
    //},
    //getunitCodeSList() {
    //  let url = "/Unit/GetSelectUnitData";
    //  let data = {};
    //  this.$ajax(url, data, { method: "get" }).then(response => {
    //    this.unitCodeSList = response.data;
    //  })
    //},
    //businessCodeChange(val) {
    //  let text = "";
    //  var arr = this.businessCodeSList.filter(function (x) { return x.value == val });
    //  if (arr.length > 0) {
    //    text = arr[0].label;
    //  }
    //  this.temp.businessName = text;
    //},
    //brandCodeChange(val) {
    //  let text = "";
    //  var arr = this.brandCodeSList.filter(function (x) { return x.value == val });
    //  if (arr.length > 0) {
    //    text = arr[0].label;
    //  }
    //  this.temp.brandName = text;
    //},
    //categoryCodeChange(val) {
    //  let text = "";
    //  var arr = this.categoryCodeSList.filter(function (x) { return x.value == val });
    //  if (arr.length > 0) {
    //    text = arr[0].label;
    //  }
    //  this.temp.categoryName = text;
    //},
    //specsCodeChange(val) {
    //  let text = "";
    //  var arr = this.specsCodeSList.filter(function (x) { return x.value == val });
    //  if (arr.length > 0) {
    //    text = arr[0].label;
    //  }
    //  this.temp.specsName = text;
    //},
    //unitCodeChange(val) {
    //  let text = "";
    //  var arr = this.unitCodeSList.filter(function (x) { return x.value == val });
    //  if (arr.length > 0) {
    //    text = arr[0].label;
    //  }
    //  this.temp.unitName = text;
    //},
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
      let productCode = myAction.newGuid();
      this.temp = {
        productCode: productCode,
        simpleCode: "",
        productName: "",
        id: 0,
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
      this.dialogTitle = "新增产品";
      this.dialogFormVisible = true;
      this.$nextTick(() => {
        this.$refs["dataForm"].clearValidate();
      });
    },
    //提交数据
    createData() {
      this.$refs["dataForm"].validate(valid => {
        if (valid) {
          var url = "/Product/_SaveData";
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
      this.dialogTitle = "修改产品";
      this.dialogFormVisible = true;
      this.categoryCodeSList = [
        { label: this.temp.categoryName, value: this.temp.categoryCode }
      ];
      this.$nextTick(() => {
        this.$refs["dataForm"].clearValidate();
      });
    },
    //提交修改数据
    updateData() {
      this.$refs["dataForm"].validate(valid => {
        if (valid) {
          const tempData = Object.assign({}, this.temp);
          var url = "/Product/_SaveData";
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
   
            let url = "/Product/_DelData";
            let data = { id: row.id };
            this.$ajax(url, data, { method: "get" }).then(response => {
              if (response.resultSign == 0) {
                this.list.splice(index, 1);
                this.total--;
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

.el-select {
  display: block;
}

.demo-table-expand {
  font-size: 0;
}

.demo-table-expand label {
  width: 90px;
  color: #99a9bf;
}

.demo-table-expand .el-form-item {
  margin-right: 0;
  margin-bottom: 0;
  width: 50%;
}
</style>


