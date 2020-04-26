<template>
  <div class="app-container">
    <div class="filter-container">
      <el-collapse accordion v-model="activeName">
        <el-collapse-item title="查询条件" name="1">
          <el-form :inline="true" :model="filterModel">
            <el-form-item label="记录时间">
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
    </el-row>
    <el-table
      :key="tableKey"
      v-loading="listLoading"
      :data="list"
      border
      fit
      highlight-current-row
     
      @sort-change="sortChange"
    >
      <!-- <el-table-column label="exceptionLogId" prop="exceptionLogId" sortable="custom">
        <template slot-scope="{row}">
          <span>{{ row.exceptionLogId }}</span>
        </template>
      </el-table-column>-->

      <el-table-column type="expand">
        <template slot-scope="props">
          <el-form label-position="left" inline class="demo-table-expand">
            <el-form-item label="堆栈信息">
              <span>{{ props.row.stackTrace }}</span>
            </el-form-item>

            <el-form-item label="请求数据">
              <span>{{ props.row.requestData }}</span>
            </el-form-item>
          </el-form>
        </template>
      </el-table-column>

      <el-table-column label="记录日期" prop="createTime" sortable="custom" width="160px">
        <template slot-scope="{row}">
          <span>{{ row.createTime|formatTime(row.createTime,"yyyy-MM-dd hh:mm:ss")}}</span>
        </template>
      </el-table-column>

      <el-table-column label="错误消息">
        <template slot-scope="{row}">
          <span>{{ row.message }}</span>
        </template>
      </el-table-column>
      <!-- <el-table-column label="堆栈信息" width="600px">
        <template slot-scope="{row}">
          <span>{{ row.stackTrace }}</span>
        </template>
      </el-table-column>-->
      <!-- <el-table-column label="内部信息">
        <template slot-scope="{row}">
          <span>{{ row.innerException }}</span>
        </template>
      </el-table-column>-->
      <el-table-column label="异常类型">
        <template slot-scope="{row}">
          <span>{{ row.exceptionType }}</span>
        </template>
      </el-table-column>
      <el-table-column label="服务器">
        <template slot-scope="{row}">
          <span>{{ row.serverHost }}</span>
        </template>
      </el-table-column>
      <el-table-column label="客户端">
        <template slot-scope="{row}">
          <span>{{ row.clientHost }}</span>
        </template>
      </el-table-column>
      <el-table-column label="运行环境" width="60px">
        <template slot-scope="{row}">
          <span>{{ row.runtime }}</span>
        </template>
      </el-table-column>
      <el-table-column label="请求Url">
        <template slot-scope="{row}">
          <span>{{ row.requestUrl }}</span>
        </template>
      </el-table-column>
      <!-- <el-table-column label="请求数据">
        <template slot-scope="{row}">
          <span>{{ row.requestData }}</span>
        </template>
      </el-table-column>-->
      <el-table-column label="浏览器代理">
        <template slot-scope="{row}">
          <span>{{ row.userAgent }}</span>
        </template>
      </el-table-column>
      <el-table-column label="请求方式" width="60px">
        <template slot-scope="{row}">
          <span>{{ row.httpMethod }}</span>
        </template>
      </el-table-column>
      <!-- <el-table-column label="操作" width="250" class-name="small-padding fixed-width">
        <template slot-scope="{row,$index}">
          <el-button
            v-if="row.status!='deleted'"
            size="mini"
            type="danger"
            @click="handleDelete(row,$index)"
          >删除</el-button>
        </template>
      </el-table-column>-->
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
    createTime: {
      field: "CreateTime",
      method: "Between",
      value: "",
      prefix: "",
      operator: "And"
    }
  },
 
  orderArr: [{ order: "descing", prop: "createTime" }]
};
var data = $.extend(true, myAction.setBaseVueData, currentData);
export default {
  name: "exceptionloglist",
  components: { Pagination },
  directives: { waves },
  filters: {
    formatTime: function(val,format) {
      return myAction.formatTime(val,format);
    }
  },
  data() {
    data.activeName = "1";
    return data;
  },
  created() {
    this.getList();
  },
  methods: {
    getList() {
      this.listLoading = true;
      var url = "/ExceptionLog/_GetPageList";
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
    }
  }
};
</script>
<style >
.el-form-item {
  margin-left: 22px;
}

</style>


