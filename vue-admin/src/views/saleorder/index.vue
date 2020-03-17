<template>
  <div class="app-container">

    <div class="filter-container">
      <el-input v-model="filterModel.sOrderNum" placeholder="销售订单号" style="width: 200px;" class="filter-item" @keyup.enter.native="handleFilter" />

      <el-date-picker v-model="filterModel.sOrderCreateTime" :picker-options="daterangeOptions" value-format="yyyy-MM-dd" range-separator="至" start-placeholder="开始时间" end-placeholder="结束时间" style="width: 350px;" type="daterange" placement="bottom-end" @keyup.enter.native="handleFilter" />

      <el-input v-model="filterModel.userPurchase" placeholder="用户姓名" style="width: 200px;" class="filter-item" @keyup.enter.native="handleFilter" />
      <el-input v-model="filterModel.userPurchasePhone" placeholder="用户手机号" style="width: 200px;" class="filter-item" @keyup.enter.native="handleFilter" />
      <el-select v-model="filterModel.userPurchaseGender" placeholder="用户性别" clearable style="width: 120px" class="filter-item">
        <el-option v-for="item in userPurchaseGenderOptions" :key="item.id" :label="item.text" :value="item.id" />
      </el-select>
      <el-input v-model="filterModel.userPurchaseAddress" placeholder="用户地址" style="width: 200px;" class="filter-item" @keyup.enter.native="handleFilter" />

      <el-button v-waves class="filter-item" type="primary" icon="el-icon-search" @click="handleFilter">
        查询
      </el-button>
      <el-button class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-edit" @click="handleCreate">
        新增
      </el-button>
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
      <el-table-column label="销售订单号" prop="sOrderNum" sortable="custom" width="180" :class-name="getSortClass('sOrderNum')">
        <template slot-scope="{row}">
          <span>{{ row.sOrderNum }}</span>
        </template>
      </el-table-column>
      <el-table-column label="销售时间" width="180px">
        <template slot-scope="{row}">
          <span>{{ row.sOrderCreateTime}}</span>
        </template>
      </el-table-column>
      <el-table-column label="用户姓名" width="180px">
        <template slot-scope="{row}">
          <span>{{ row.userPurchase }}</span>
        </template>
      </el-table-column>
      <el-table-column label="用户手机号" width="180px">
        <template slot-scope="{row}">
          <span>{{ row.userPurchasePhone }}</span>
        </template>
      </el-table-column>
      <el-table-column label="用户性别" width="100px">
        <template slot-scope="{row}">
          <span>{{  row.userPurchaseGender|formatGender }}</span>
        </template>
      </el-table-column>
      <el-table-column label="用户地址" min-width="100px">
        <template slot-scope="{row}">
          <span>{{ row.userPurchaseAddress }}</span>
        </template>
      </el-table-column>
      <el-table-column label="购买总量" width="100px">
        <template slot-scope="{row}">
          <span>{{ row.allPurchaseCount }}</span>
        </template>
      </el-table-column>
      <el-table-column label="购买金额" width="100px">
        <template slot-scope="{row}">
          <span>{{ row.allPurchaseAmount }}</span>
        </template>
      </el-table-column>
      <el-table-column label="操作" width="250" class-name="small-padding fixed-width">
        <template slot-scope="{row,$index}">
          <el-button type="primary" size="mini" @click="handleUpdate(row)">
            修改
          </el-button>
          <el-button v-if="row.status!='draft'" size="mini" @click="handleModifyStatus(row,'draft')">
            查看详情
          </el-button>
          <el-button v-if="row.status!='deleted'" size="mini" type="danger" @click="handleDelete(row,$index)">
            删除
          </el-button>
        </template>
      </el-table-column>
    </el-table>

    <pagination v-show="total>0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="getList" />

    <el-dialog :title="textMap[dialogStatus]" :visible.sync="dialogFormVisible" >
      <el-form ref="dataForm" :rules="rules" :model="temp" label-position="left" label-width="70px" style="width:auto;">

        <fieldset>
          <legend>订单信息</legend>
          <el-row>
            <el-col :span="12">
              <el-form-item label="订单号">
                <el-input v-model="temp.saleOrder.sOrderNum" />
              </el-form-item>
            </el-col>

            <el-col :span="10">
              <el-form-item label="订单时间">
                <el-date-picker v-model="temp.saleOrder.sOrderCreateTime" typeof="date" value-format="yyyy-MM-dd" />
              </el-form-item>
            </el-col>

          </el-row>
          <el-row>
            <el-col :span="10">
              <el-form-item label="用户姓名">
                <el-input v-model="temp.saleOrder.userPurchase" />
              </el-form-item>
            </el-col>
            <el-col :span="10">
              <el-form-item label="手机号">
                <el-input v-model="temp.saleOrder.userPurchasePhone" />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row>
            <el-col :span="10">
              <el-form-item label="性别">
                <el-radio-group v-model="temp.saleOrder.userPurchaseGender">
                  <el-radio :label="0">男</el-radio>
                  <el-radio :label="1">女</el-radio>
                  <el-radio :label="2">未知</el-radio>
                </el-radio-group>
              </el-form-item>
            </el-col>
            <el-col :span="10">
              <el-form-item label="地址">
                <el-input v-model="temp.saleOrder.userPurchaseAddress" type="textarea" :autosize="{minRows: 2,maxRows: 5}" />
              </el-form-item>
            </el-col>
          </el-row>
        </fieldset>

        <fieldset>
          <legend>订单详情信息</legend>

          <el-table v-loading="listLoading" :data="temp.saleOrderInfos" border fit highlight-current-row style="width: 100%">
            <el-table-column align="center" label="进货单号">
              <template slot-scope="{row}">

                <el-input v-model="row.pOrderNum" />

              </template>
            </el-table-column>

            <el-table-column align="center" label="进货商">

              <template slot-scope="{row}">
                <el-select v-model="row.sBusinessCode" filterable placeholder="请选择"     @change="businessChange(row)" style="width:100%;">
                  <el-option v-for="(item,index) in row.businessSList"
                             :key="index"
                             :label="item.label"
                             :value="item.value">
                  </el-option>
                </el-select>
              </template>

            </el-table-column>
            <el-table-column align="center" label="商品名称">
              <template slot-scope="{row}">
                <el-select v-model="row.sProductCode" filterable placeholder="请选择" @change="productChange(row)" style="width:100%;">
                  <el-option v-for="(item,index) in productSList"
                             :key="index"
                             :label="item.label"
                             :value="item.value">
                  </el-option>
                </el-select>
              </template>
            </el-table-column>

            <el-table-column align="center" label="系列名称">
              <template slot-scope="{row}">

                <el-select v-model="row.sProductTypeCode" filterable placeholder="请选择" style="width:100%;">
                  <el-option v-for="(item,index) in row.productTypeSList"
                             :key="index"
                             :label="item.label"
                             :value="item.value">
                  </el-option>
                </el-select>

              </template>
            </el-table-column>

            <el-table-column align="center" label="数量">
              <template slot-scope="{row}">
                <el-input v-model="row.saleCount" />
              </template>
            </el-table-column>

            <el-table-column align="center" label="单价">
              <template slot-scope="{row}">
                <el-input v-model="row.salePrice" />
              </template>
            </el-table-column>
            <el-table-column align="center" label="金额">
              <template slot-scope="{row}">
                <el-input v-model="row.saleAmount" />
              </template>
            </el-table-column>
            <el-table-column align="center" label="操作">
              <template slot-scope="{row,$index}">
                <el-button v-if="$index==0" size="mini" type="success" @click="handleAddSaleOrderInfo(row)">
                  新增
                </el-button>
                <el-button v-if="$index!=0" size="mini" type="danger" @click="handleDeleteSaleOrderInfo(row,$index)">
                  删除
                </el-button>
              </template>
            </el-table-column>

          </el-table>
        </fieldset>

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
  import { getPageList, postSaveData, getModel, delData } from '@/api/saleorder'  //
  import { getSelectProductTypeData, getSelectProductData, getSelectBusinessData } from '@/api/system'  //
  import waves from '@/directive/waves' // waves directive
  import Pagination from '@/components/Pagination' // secondary package based on el-pagination
  import myAction from '@/utils/baseutil'

  export default {
    name: 'index',
    components: { Pagination },
    directives: { waves },
    filters: {
      statusFilter(status) {
        const statusMap = {
          published: 'success',
          draft: 'info',
          deleted: 'danger'
        }
        return statusMap[status]
      },
      formatGender: function (val) {
        return myAction.formatGender(val);
      }


    },
    data() {
      const $this = this;
      return {
        tableKey: 0,
        list: null,
        businessSList: [],
        total: 0,
        listLoading: true,
        listQuery: {
          page: 1,
          limit: 20
        },
        filterModel:
        {
          sOrderNum: "",
          sOrderCreateTime: "",
          userPurchase: "",
          userPurchasePhone: "",
          userPurchaseGender: "",
          userPurchaseAddress: "",
          allPurchaseCount: "",
          allPurchaseAmount: ""
        },
        userPurchaseGenderOptions: [{ id: 0, text: "男" }, { id: 1, text: "女" }, { id: 2, text: "未知" }],

        daterangeOptions: {
          disabledDate(date) {
            const disabledDay = date.getDate();
            return disabledDay === 15;
          },
          shortcuts: [
            {
              text: '最近一周',
              value() {
                const end = new Date();
                const start = new Date();
                start.setTime(start.getTime() - 3600 * 1000 * 24 * 7);
                return [start, end];
              },
              onClick: function (picker) {
                picker.$emit('pick', this.value());
              }

            },
            {
              text: '最近一个月',
              value() {
                const end = new Date();
                const start = new Date();
                start.setTime(start.getTime() - 3600 * 1000 * 24 * 30);
                return [start, end];
              },
              onClick: function (picker) {
                picker.$emit('pick', this.value());
              }
            },
            {
              text: '最近三个月',
              value() {
                const end = new Date();
                const start = new Date();
                start.setTime(start.getTime() - 3600 * 1000 * 24 * 90);
                return [start, end];
              },
              onClick: function (picker) {
                picker.$emit('pick', this.value());
              }
            }
          ]
        },
        showReviewer: false,
        temp: {
          saleOrder: {
            sOrderNum: undefined,
            sOrderCreateTime: new Date(),
            userPurchase: '',
            userPurchasePhone: '',
            userPurchaseGender: '',
            userPurchaseAddress: "",
            allPurchaseCount: "",
            allPurchaseAmount: "",
          },
          saleOrderInfos: [
            {
              sOrderNum: "",
              pOrderNum: "",
              sBusinessCode: "",
              sProductCode: "",
              sProductTypeCode: "",
              saleCount: "",
              salePrice: "",
              saleAmount: "",
            }
          ]
        },
        dialogFormVisible: false,
        dialogStatus: '',
        textMap: {
          update: 'Edit',
          create: 'Create'
        },
        dialogPvVisible: false,
        pvData: [],
        rules: {
          type: [{ required: true, message: 'type is required', trigger: 'change' }],
          timestamp: [{ type: 'date', required: true, message: 'timestamp is required', trigger: 'change' }],
          title: [{ required: true, message: 'title is required', trigger: 'blur' }]
        },
        downloadLoading: false
      }
    },
    created() {
      this.getList()
      this.getBusinessSList()
      this.getProductSList()

    },
    methods: {
      getList() {
        this.listLoading = true
        getPageList(this.getQueryModel()).then(response => {

          this.list = response.data
          this.total = response.total

          // Just to simulate the time of the request
          setTimeout(() => {
            this.listLoading = false
          }, 1.5 * 300)
        })
      },
      getBusinessSList() {
        getSelectBusinessData().then(response => {
          this.businessSList = response.data;
        })
      },
     
      //供应商 联动
      businessChange(row) {
        getSelectProductData(row.sBusinessCode).then(response => {
          row.productSList = response.data;
        })
      },
      // 商品 联动事件
      productChange(row) {
        getSelectProductTypeData(row.sProductCode).then(response => {
          row.productTypeSList = response.data;
        })
      },
      handleFilter() {
        this.listQuery.page = 1
        this.getList()
      },
      handleModifyStatus(row, status) {
        this.$message({
          message: '操作Success',
          type: 'success'
        })
        row.status = status
      },
      sortChange(data) {
        const { prop, order } = data
        if (prop === 'id') {
          this.sortByID(order)
        }
      },
      sortByID(order) {
        if (order === 'ascending') {
          this.listQuery.sort = '+id'
        } else {
          this.listQuery.sort = '-id'
        }
        this.handleFilter()
      },
      resetTemp() {
        let sOrderNum = myAction.newOrderNumber();
        this.temp = {
          saleOrder: {
            ID: "",
            sOrderNum: sOrderNum,
            sOrderCreateTime: new Date(),
            userPurchase: '',
            userPurchasePhone: '',
            userPurchaseGender: '',
            userPurchaseAddress: "",
            allPurchaseCount: "",
            allPurchaseAmount: "",
          },
          saleOrderInfos: [
            {
              ID: "",
              sOrderNum: sOrderNum,
              pOrderNum: "",
              sBusinessCode: "",
              sProductCode: "",
              productSList:[],
              sProductTypeCode: "",
              productTypeSList: [],
              saleCount: "",
              salePrice: "",
              saleAmount: "",
            }
          ]
        }
      },
      getSortClass: function (key) {
        const sort = this.listQuery.sort
        return sort === `+${key}` ? 'ascending' : 'descending'
      },
      getQueryModel: function () {

        const queryModel = {
          pageSize: this.listQuery.limit,
          pageIndex: this.listQuery.page,
          total: this.total,
          items: [
            {
              field: "SOrderNum",
              method: "Contains",
              value: myAction.convertNULL(this.filterModel.sOrderNum),
              prefix: "",
              orGroup: "And",
              operator: "Contains"
            },
            {
              field: "SOrderCreateTime",
              method: "StdIn",
              value: myAction.convertNULL(this.filterModel.sOrderCreateTime),
              prefix: "",
              orGroup: "And",
              operator: "StdIn"
            },
            {
              field: "UserPurchase",
              method: "Contains",
              value: myAction.convertNULL(this.filterModel.userPurchase),
              prefix: "",
              orGroup: "And",
              operator: "Contains"
            },
            {
              field: "UserPurchasePhone",
              method: "Contains",
              value: myAction.convertNULL(this.filterModel.userPurchasePhone),
              prefix: "",
              orGroup: "And",
              operator: "Contains"
            },
            {
              field: "UserPurchaseGender",
              method: "Equal",
              value: myAction.convertNULL(this.filterModel.userPurchaseGender),
              prefix: "",
              orGroup: "And",
              operator: "Equal"
            },
            {
              field: "UserPurchaseAddress",
              method: "Contains",
              value: myAction.convertNULL(this.filterModel.userPurchaseAddress),
              prefix: "",
              orGroup: "And",
              operator: "Contains"
            }
          ],
          orderList: [
            {
              field: "ID",
              isDesc: true
            }
          ],
          isReport: true
        };
        return queryModel;
      },
      handleCreate() {
        this.resetTemp()
        this.dialogStatus = 'create'
        this.dialogFormVisible = true
        this.$nextTick(() => {
          this.$refs['dataForm'].clearValidate()
        })
      },
      createData() {
        this.$refs['dataForm'].validate((valid) => {
          if (valid) {
            postSaveData(this.temp).then(response => {
              this.list.unshift(this.temp)
              this.dialogFormVisible = false
              this.$notify({
                title: 'Success',
                message: 'Created Successfully',
                type: 'success',
                duration: 2000
              })
            })
          }
        })
      },
      handleUpdate(row) {

        getModel(row.sOrderNum).then(response => {
          this.temp = response.data;
          this.dialogStatus = 'update'
          this.dialogFormVisible = true
          this.$nextTick(() => {
            this.$refs['dataForm'].clearValidate()
          })

        })

      },
      updateData() {
        this.$refs['dataForm'].validate((valid) => {
          if (valid) {
            const tempData = Object.assign({}, this.temp)
            postSaveData(tempData).then(() => {
              const index = this.list.findIndex(v => v.id === this.temp.id)
              this.list.splice(index, 1, this.temp)
              this.dialogFormVisible = false
              this.$notify({
                title: 'Success',
                message: 'Update Successfully',
                type: 'success',
                duration: 2000
              })
            })
          }
        })
      },
      handleDelete(row, index) {

        delData(row.id).then(response => {
          if (response.resultSign == 0) {
            this.list.splice(index, 1)
            this.total--;
          }
          this.$notify({
            title: '消息',
            message: response.message,
            type: response.resultSign,
            duration: 2000
          })

        })


      },
      handleAddSaleOrderInfo(row) {
        this.temp.saleOrderInfos.push({
          sOrderNum: "",
          pOrderNum: "",
          sBusinessCode: "",
          sProductCode: "",
          sProductTypeCode: "",
          saleCount: "",
          salePrice: "",
          saleAmount: "",
        });
      },
      handleDeleteSaleOrderInfo(row, index) {
        this.$notify({
          title: 'Success',
          message: 'Delete Successfully',
          type: 'success',
          duration: 2000
        })
        this.temp.saleOrderInfos.splice(index, 1)
      },
    }

  }
</script>
<style scoped>
  .el-form-item {
    margin-left: 22px;
  }
 
</style>


