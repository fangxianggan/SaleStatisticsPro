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
      style="width: 100%;"
      @sort-change="sortChange"
    >
      <el-table-column type="expand">
        <template slot-scope="props">
          <el-form label-position="left" inline class="demo-table-expand">

           
            <el-form-item label="请求Url">
              <span>{{ props.row.url }}</span>
            </el-form-item>

             <el-form-item label="上一次url">
              <span>{{ props.row.urlReferrer }}</span>
            </el-form-item>

            <el-form-item label="浏览器信息">
              <span>{{ props.row.userAgent }}</span>
            </el-form-item>

            <el-form-item label="请求数据">
              <span>{{ props.row.requestData }}</span>
            </el-form-item>
          </el-form>
        </template>
      </el-table-column>

      <el-table-column label="操作日期" prop="createTime" sortable="custom" width="160">
        <template slot-scope="{row}">
          <span>{{ row.createTime|formatTime(row.createTime,"yyyy-MM-dd hh:mm:ss")}}</span>
        </template>
      </el-table-column>
      <el-table-column label="操作人" prop="createUserCode" sortable="custom" width="140">
        <template slot-scope="{row}">
          <span>{{ row.createUserCode}}</span>
        </template>
      </el-table-column>

      <el-table-column label="控制器名称">
        <template slot-scope="{row}">
          <span>{{ row.controllerName }}</span>
        </template>
      </el-table-column>
      <el-table-column label="操作名称">
        <template slot-scope="{row}">
          <span>{{ row.actionName }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Action执行时间(秒)">
        <template slot-scope="{row}">
          <span>{{ row.actionExecutionTime }}</span>
        </template>
      </el-table-column>
      <el-table-column label="描述">
        <template slot-scope="{row}">
          <span>{{ row.describe }}</span>
        </template>
      </el-table-column>
      <el-table-column label="客户端">
        <template slot-scope="{row}">
          <span>{{ row.clientHost}}</span>
        </template>
      </el-table-column>
      <el-table-column label="服务端IP地址" width="100">
        <template slot-scope="{row}">
          <span>{{ row.serverHost }}</span>
        </template>
      </el-table-column>
      <el-table-column label="请求内容大小">
        <template slot-scope="{row}">
          <span>{{ row.requestContentLength }}</span>
        </template>
      </el-table-column>
      <el-table-column label="请求类型">
        <template slot-scope="{row}">
          <span>{{ row.requestType}}</span>
        </template>
      </el-table-column>

      <el-table-column label="响应状态">
        <template slot-scope="{row}">
          <span>{{ row.responseStatus }}</span>
        </template>
      </el-table-column>

      <!-- <el-table-column label="操作" width="100" class-name="small-padding fixed-width">
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
  name: "OperateLoglist",
  components: { Pagination },
  directives: { waves },
  filters: {
    formatTime: function(val, format) {
      return myAction.formatTime(val, format);
    },
    formatRequestType: function(val) {
      var t;

      return t;
    }
  },
  data() {
    return data;
  },
  created() {
    this.getList();
  },
  methods: {
    getList() {
      this.listLoading = true;
      var url = "/OperateLog/_GetPageList";
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
<style scoped>
.el-form-item {
  margin-left: 22px;
}
</style>


