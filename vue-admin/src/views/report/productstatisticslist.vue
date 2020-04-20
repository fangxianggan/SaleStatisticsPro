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

            <el-form-item label="产品简码">
              <el-input
                v-model="filterModel.simpleCode.value"
                placeholder="产品简码"
                class="filter-item"
                @keyup.enter.native="handleFilter"
              />
            </el-form-item>

            <el-form-item label="产品数量">
              <el-input
                v-model="filterModel.purchaseCount.value"
                placeholder="产品数量"
                class="input-with-select"
                @keyup.enter.native="handleFilter"
              >
                <el-select
                  v-model="filterModel.purchaseCount.method"
                  class="el-sel"
                  slot="prepend"
                  placeholder="请选择"
                >
                  <el-option label="等于" value="Equal"></el-option>
                  <el-option label="大于" value="GreaterThan"></el-option>
                  <el-option label="小于" value="LessThan"></el-option>
                </el-select>
              </el-input>
            </el-form-item>

            <el-form-item label="产品库存">
              <el-input
                v-model="filterModel.stock.value"
                placeholder="产品库存"
                class="input-with-select"
                @keyup.enter.native="handleFilter"
              >
                <el-select
                  v-model="filterModel.stock.method"
                  class="el-sel"
                  slot="prepend"
                  placeholder="请选择"
                >
                  <el-option label="等于" value="Equal"></el-option>
                  <el-option label="大于" value="GreaterThan"></el-option>
                  <el-option label="小于" value="LessThan"></el-option>
                </el-select>
              </el-input>
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
      <el-button
        v-waves
        :loading="downloadLoading"
        class="filter-item"
        type="primary"
        icon="el-icon-download"
        @click="handleDownload"
      >导出</el-button>
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
      ref="multipleTable"
      @selection-change="handleSelectionChange"
    >
      <el-table-column type="selection" width="55"></el-table-column>
      <el-table-column label="产品简码" prop="simpleCode" sortable="custom">
        <template slot-scope="{row}">
          <span>{{ row.simpleCode}}</span>
        </template>
      </el-table-column>
      <el-table-column label="产品名称" prop="productName" sortable="custom">
        <template slot-scope="{row}">
          <span>{{ row.productName}}</span>
        </template>
      </el-table-column>
      <el-table-column label="净利润" prop="profitAmount" sortable="custom">
        <template slot-scope="{row}">
          <span>{{ row.profitAmount}}</span>
        </template>
      </el-table-column>
      <el-table-column label="存库" prop="stock" sortable="custom">
        <template slot-scope="{row}">
          <span>{{ row.stock}}</span>
        </template>
      </el-table-column>
      <el-table-column label="进货数量" prop="purchaseCount" sortable="custom">
        <template slot-scope="{row}">
          <span>{{ row.purchaseCount}}</span>
        </template>
      </el-table-column>
      <el-table-column label="进货金额" prop="purchaseAmount" sortable="custom">
        <template slot-scope="{row}">
          <span>{{ row.purchaseAmount}}</span>
        </template>
      </el-table-column>
      <el-table-column label="进货运费" prop="purchaseFreightAmount" sortable="custom">
        <template slot-scope="{row}">
          <span>{{ row.purchaseFreightAmount}}</span>
        </template>
      </el-table-column>
      <el-table-column label="进货理赔金额" prop="purchaseSettlementAmount" sortable="custom">
        <template slot-scope="{row}">
          <span>{{ row.purchaseSettlementAmount}}</span>
        </template>
      </el-table-column>
      <el-table-column label="进货总额" prop="allPurchaseAmount" sortable="custom">
        <template slot-scope="{row}">
          <span>{{ row.allPurchaseAmount}}</span>
        </template>
      </el-table-column>

      <el-table-column label="销售数量" prop="saleCount" sortable="custom">
        <template slot-scope="{row}">
          <span>{{ row.saleCount}}</span>
        </template>
      </el-table-column>
      <el-table-column label="销售金额" prop="saleAmount" sortable="custom">
        <template slot-scope="{row}">
          <span>{{ row.saleAmount}}</span>
        </template>
      </el-table-column>
      <el-table-column label="销售运费" prop="saleFreightAmount" sortable="custom">
        <template slot-scope="{row}">
          <span>{{ row.saleFreightAmount}}</span>
        </template>
      </el-table-column>
      <el-table-column label="销售理赔金额" prop="saleSettlementAmount" sortable="custom">
        <template slot-scope="{row}">
          <span>{{ row.saleSettlementAmount}}</span>
        </template>
      </el-table-column>
      <el-table-column label="销售总额" prop="allSaleAmount" sortable="custom">
        <template slot-scope="{row}">
          <span>{{ row.allSaleAmount}}</span>
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
    simpleCode: {
      field: "SimpleCode",
      method: "Contains",
      value: "",
      prefix: "",
      operator: "And"
    },
    purchasePrice: {
      field: "PurchasePrice",
      method: "Equal",
      value: "",
      prefix: "",
      operator: "And"
    },
    purchaseCount: {
      field: "PurchaseCount",
      method: "Equal",
      value: "",
      prefix: "",
      operator: "And"
    },
    stock: {
      field: "Stock",
      method: "Equal",
      value: "",
      prefix: "",
      operator: "And"
    }
  },
  temp: {
    id: 0,
    simpleCode: "",
    productCode: "",
    productName: "",
    purchaseCount: 0,
    purchaseAmount: 0,
    purchaseFreightAmount: 0,
    allPurchaseAmount: 0,
    saleCount: 0,
    saleAmount: 0,
    saleFreightAmount: 0,
    allSaleAmount: 0,
    profitAmount: 0,
    stock: 0
  },
  orderArr: []
};
var data = $.extend(false, myAction.setBaseVueData, currentData);
export default {
  name: "productstatisticslist",
  components: { Pagination },
  directives: { waves },
  filters: {
    formatTime: function(val) {
      return myAction.formatTime(val);
    }
  },
  data() {
    data.downloadLoading = false;
    data.multipleSelection = [];
    data.activeName = "1";
    return data;
  },
  created() {
    this.getList();
  },
  methods: {
    getList() {
      this.listLoading = true;
      var url = "/Statistics/GetProductViewModelPageList";
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
    //下载excel
    handleDownload() {
      this.downloadLoading = true;
      var url = "/Statistics/GetProductStatisticsViewModelExportExcel";
      var data = {};
      if (this.multipleSelection.length > 0) {
        let value = this.multipleSelection
          .map(function(x) {
            return x.simpleCode;
          })
          .join(",");
        var excelModel = {
          SimpleCode: {
            field: "simpleCode",
            method: "In",
            value: value,
            prefix: "",
            operator: "And"
          }
        };
        data = myAction.getQueryModel(0, 0, 0, excelModel, []);
      } else {
        data = myAction.getQueryModel(
          this.listQuery.limit,
          this.listQuery.page,
          this.total,
          this.filterModel,
          this.orderArr
        );
      }

      this.$ajax(url, data, {
        method: "post"
      }).then(res => {
        this.downloadLoading = false;
        var bstr = atob(res.data.content),
          n = bstr.length,
          u8arr = new Uint8Array(n);
        while (n--) {
          u8arr[n] = bstr.charCodeAt(n);
        }

        var blob = new Blob([u8arr], {
          type: "application/vnd.ms-excel;charset=utf-8"
        });
        // 针对于IE浏览器的处理, 因部分IE浏览器不支持createObjectURL
        if (window.navigator && window.navigator.msSaveOrOpenBlob) {
          window.navigator.msSaveOrOpenBlob(blob, res.data.fileName);
        } else {
          var downloadElement = document.createElement("a");
          var href = window.URL.createObjectURL(blob); // 创建下载的链接
          downloadElement.href = href;
          downloadElement.download = res.data.fileName; // 下载后文件名
          document.body.appendChild(downloadElement);
          downloadElement.click(); // 点击下载
          document.body.removeChild(downloadElement); // 下载完成移除元素
          window.URL.revokeObjectURL(href); // 释放掉blob对象
        }
      });
    },
    //chenkbox
    toggleSelection(rows) {
      if (rows) {
        rows.forEach(row => {
          this.$refs.multipleTable.toggleRowSelection(row);
        });
      } else {
        this.$refs.multipleTable.clearSelection();
      }
    },
    //checkbox 事件处理
    handleSelectionChange(val) {
      this.multipleSelection = val;
    }
  }
};
</script>
<style scoped>
.el-form-item {
  margin-left: 22px;
}
</style>


