<template>
  <div class="app-container">

    <div class="filter-container">
      <el-form :inline="true" :model="filterModel">
        <el-form-item label="规格名称">
          <el-input v-model="filterModel.specsName.value" placeholder="规格名称" style="width: 200px;" class="filter-item" @keyup.enter.native="handleFilter" />
        </el-form-item>
        <el-form-item label="添加时间">
          <el-date-picker v-model="filterModel.createTime.value" :picker-options="daterangeOptions" value-format="yyyy-MM-dd" range-separator="至" start-placeholder="开始时间" end-placeholder="结束时间" style="width: 350px;" type="daterange" placement="bottom-end" @keyup.enter.native="handleFilter" />
        </el-form-item>
        <el-form-item>
          <el-button v-waves class="filter-item" type="primary" icon="el-icon-search" @click="handleFilter">
            查询
          </el-button>
          <el-button class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-edit" @click="handleCreate">
            新增
          </el-button>
        </el-form-item>
      </el-form>
      <!--<el-button v-waves :loading="downloadLoading" class="filter-item" type="primary" icon="el-icon-download" @click="handleDownload">
        导出
      </el-button>-->

    </div>

    <el-table :key="tableKey"
              v-loading="listLoading"
              :data="list"
              border
              fit
              highlight-current-row
              style="width: 100%;"
              @sort-change="sortChange">
      <el-table-column label="规格名称" prop="specsName" sortable="custom" width="180">
        <template slot-scope="{row}">
          <span>{{ row.specsName }}</span>
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

      <el-table-column label="操作" width="250" class-name="small-padding fixed-width">
        <template slot-scope="{row,$index}">
          <el-button type="primary" size="mini" @click="handleUpdate(row)">
            修改
          </el-button>

          <el-button v-if="row.status!='deleted'" size="mini" type="danger" @click="handleDelete(row,$index)">
            删除
          </el-button>
        </template>
      </el-table-column>
    </el-table>

    <pagination v-show="total>0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="getList" />

    <el-dialog  v-el-drag-dialog  :title="dialogTitle" :visible.sync="dialogFormVisible">
      <el-form ref="dataForm"  :model="temp" label-position="left" label-width="100px" >

        <el-row>
          <el-col :span="24">
            <el-form-item label="规格编码">
              <el-input v-model="temp.specsCode" :readonly="true" />
            </el-form-item>
          </el-col>
        </el-row>

        <el-row>
          <el-col :span="24">
            <el-form-item label="规格名称" prop="specsName" :rules="rules.checkNull">
              <el-input v-model="temp.specsName" />
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
        <el-button @click="dialogFormVisible = false">
          关闭
        </el-button>
        <el-button type="primary" @click="dialogStatus==='create'?createData():updateData()">
          保存
        </el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
  import waves from '@/directive/waves' // waves directive
  import Pagination from '@/components/Pagination' // secondary package based on el-pagination
  import myAction from '@/utils/baseutil'
  var currentData = {
    filterModel:
    {
      specsName:
      {
        field: "SpecsName",
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
      id: "",
      specsCode: "",
      specsName: "",
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
    name: 'specslist',
    components: { Pagination },
    directives: { waves },
    filters: {
      formatTime: function (val) {
        return myAction.formatTime(val);
      }
    },
    data() {
      let custRules = {};
      data.rules = $.extend(false, myAction.setBaseValidateRules(), custRules);
      return data
    },
    created() {
      this.getList()
    },
    methods: {
      getList() {
        this.listLoading = true
        var url = "/Specs/_GetPageList"
        var data = myAction.getQueryModel(this.listQuery.limit, this.listQuery.page, this.total, this.filterModel, this.orderArr)
        //var data = myAction.getItemsModel(this.filterModel);
        this.$ajax(url, data).then(response => {
          this.list = response.data
          this.total = response.total

          // Just to simulate the time of the request
          setTimeout(() => {
            this.listLoading = false
          }, 1.5 * 300)
        })
      },
      //查询处理的事件
      handleFilter() {
        this.listQuery.page = 1
        this.getList()
      },
      //排序事件
      sortChange(data) {
        this.orderArr = [];
        this.orderArr.push(data);
        this.handleFilter()
      },
      resetTemp() {
        let specsCode = myAction.newGuid();
        this.temp = {
          id: "",
          specsCode: specsCode,
          specsName: "",
          createTime: "",
          createUserCode: "",
          updateTime: "",
          updateUserCode: "",
          remark: ""
        }
      },
      //新增 页面
      handleCreate() {
        this.resetTemp()
        this.dialogStatus = 'create'
        this.dialogTitle = '新增规格'
        this.dialogFormVisible = true
        this.$nextTick(() => {
          this.$refs['dataForm'].clearValidate()
        })
      },
      //提交数据
      createData() {
        this.$refs['dataForm'].validate((valid) => {
          if (valid) {
            var url = "/Specs/_SaveData";
            var data = this.temp;
            this.$ajax(url, data).then(response => {
              var d = response.data;
              this.list.unshift(d)
              this.total++;
              this.dialogFormVisible = false
              myAction.getNotifyFunc(response,this);
            })
          }
        })
      },
      //修改 页面
      handleUpdate(row) {
        this.temp = Object.assign({}, row) // copy obj
        this.dialogStatus = 'update'
        this.dialogTitle = '修改规格'
        this.dialogFormVisible = true
        this.$nextTick(() => {
          this.$refs['dataForm'].clearValidate()
        })
      },
      //提交修改数据
      updateData() {
        this.$refs['dataForm'].validate((valid) => {
          if (valid) {
            const tempData = Object.assign({}, this.temp)
            var url = "/Specs/_SaveData";
            var data = tempData;
            this.$ajax(url, data).then(response => {
              var d = response.data;
              const index = this.list.findIndex(v => v.id === this.temp.id)
              this.list.splice(index, 1, d)
              this.dialogFormVisible = false
              myAction.getNotifyFunc(response, this);
            })
          }
        })
      },
      //删除数据
      handleDelete(row, index) {
        var title = '<span style="color: red;">是否要删除这条数据?</span>';
        this.$confirm(title, '提示', {
          dangerouslyUseHTMLString: true,
          type: 'warning',
          confirmButtonText: '确定',
          cancelButtonText: '取消'
        })
          .then(() => {

            this.$ajax("/Specs/GetIsDeleteFlag", { code: row.specsCode }, { method: "get" }).then(d => {
              if (d.resultSign == 1) {
                myAction.getNotifyFunc(d, this);
                return false;
              }

              let url = "/Specs/_DelData"
              let data = { "id": row.id }
              this.$ajax(url, data, { method: "get" }).then(response => {
                if (response.resultSign == 0) {
                  this.list.splice(index, 1)
                  this.total--;
                }
                myAction.getNotifyFunc(response, this);
              })
            })
          })
          .catch(action => {

          });
      },
    }

  }
</script>
<style scoped>
  .el-form-item {
    margin-left: 22px;
  }
</style>


