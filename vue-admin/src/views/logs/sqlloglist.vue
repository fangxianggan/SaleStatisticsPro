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
      <!-- <el-table-column label="sqlLogId" prop="sqlLogId" sortable="custom" width="120">
        <template slot-scope="{row}">
          <span>{{ row.sqlLogId }}</span>
        </template>
      </el-table-column> -->
      <el-table-column label="记录日期" prop="createTime" sortable="custom" width="120">
        <template slot-scope="{row}">
          <span>{{ row.createTime|formatTime}}</span>
        </template>
      </el-table-column>
      <el-table-column label="执行时间(秒)" prop="elapsedTime" sortable="custom" width="140">
        <template slot-scope="{row}">
          <span>{{ row.elapsedTime}}</span>
        </template>
      </el-table-column>
      <!-- <el-table-column label="sql参数" width="100">
        <template slot-scope="{row}">
          <span>{{ row.parameter }}</span>
        </template>
      </el-table-column> -->
      <el-table-column label="sql语句">
        <template slot-scope="{row}">
          <span>{{ row.operateSql }}</span>
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
      </el-table-column> -->
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
  temp: {
    sqlLogId: "",
    operateSql: "",
    endDateTime: "",
    elapsedTime: 0,
    parameter: "",
    id: 0,
    createTime: "",
    createUserCode: "",
    updateTime: "",
    updateUserCode: "",
    remark: ""
  },
  orderArr: [{ order: "descing", prop: "createTime" }]
};
var data = $.extend(true, myAction.setBaseVueData, currentData);
export default {
  name: "sqlloglist",
  components: { Pagination },
  directives: { waves },
  filters: {
    formatTime: function(val) {
      return myAction.formatTime(val);
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
      var url = "/SqlLog/_GetPageList";
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
      this.temp = {
        sqlLogId: "",
        operateSql: "",
        endDateTime: "",
        elapsedTime: 0,
        parameter: "",
        id: 0,
        createTime: "",
        createUserCode: "",
        updateTime: "",
        updateUserCode: "",
        remark: ""
      };
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
          let url = "/SqlLog/_DelData";
          let data = { id: row.sqlLogId };
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
</style>


