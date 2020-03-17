<template>
  <div class="app-container">

    <div class="filter-container">
      <el-form :inline="true" :model="filterModel">
        <el-form-item label="销售订单号">
          <el-input v-model="filterModel.sOrderNum.value" placeholder="销售订单号" style="width: 200px;" class="filter-item" @keyup.enter.native="handleFilter" />
        </el-form-item>
        <el-form-item label="销售时间">
          <el-date-picker v-model="filterModel.sOrderCreateTime.value" :picker-options="daterangeOptions" value-format="yyyy-MM-dd" range-separator="至" start-placeholder="开始时间" end-placeholder="结束时间" style="width: 350px;" type="daterange" placement="bottom-end" @keyup.enter.native="handleFilter" />
        </el-form-item>

        <el-form-item label="手机号">
          <el-input v-model="filterModel.phoneNumber.value" placeholder="用户手机号" style="width: 200px;" class="filter-item" @keyup.enter.native="handleFilter" />
        </el-form-item>


        <el-button v-waves class="filter-item" type="primary" icon="el-icon-search" @click="handleFilter">
          查询
        </el-button>
        <el-button class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-edit" @click="handleCreate">
          新增
        </el-button>
        <el-button v-waves :loading="downloadLoading" class="filter-item" type="primary" icon="el-icon-download" @click="handleDownload">
          导出
        </el-button>
       
      </el-form>

    </div>

    <el-table :key="tableKey"
              v-loading="listLoading"
              :data="list"
              border
              fit
              highlight-current-row
              style="width: 100%;"
              @sort-change="sortChange"
              @expand-change="getSaleOrderInfoViewModelList"
                ref="multipleTable"
              @selection-change="handleSelectionChange">
      <el-table-column type="selection"
                       width="55">
      </el-table-column>
      <el-table-column type="expand">
        <template slot-scope="{row}">
          <el-table :data="row.saleOrderInfoViewModels"
                    style="width: 100%"
                    :row-class-name="tableRowClassName">
            <!--<el-table-column prop="ssOrderNum"
                   label="订单编号">
  </el-table-column>-->

            <el-table-column prop="productName"
                             label="商品名称">
            </el-table-column>
            <el-table-column prop="simpleCode"
                             label="简码">
            </el-table-column>

            <el-table-column prop="expressNumber"
                             label="国内单号">
              <template slot-scope="{row}">
                <a @click="queryLogisticsInfo(row.expressCompanyCode,row.expressNumber)">{{ row.expressNumber }}</a>
              </template>
            </el-table-column>
            <el-table-column prop="saleFreightAmount"
                             label="国内运费">
            </el-table-column>
            <el-table-column prop="saleCount"
                             label="购买数量">
            </el-table-column>
            <el-table-column prop="salePrice"
                             label="单价">
            </el-table-column>
            <el-table-column prop="saleAmount"
                             label="金额">
            </el-table-column>
            <el-table-column prop="saleFreightAmount"
                             label="运费">
            </el-table-column>
            <el-table-column prop="saleSumAmount"
                             label="总金额">
            </el-table-column>
          </el-table>
        </template>
      </el-table-column>
      <el-table-column label="下单号" prop="sOrderNum" sortable="custom" :class-name="getSortClass('sOrderNum')">
        <template slot-scope="{row}">
          <span>{{ row.sOrderNum }}</span>
        </template>
      </el-table-column>
      <el-table-column label="销售时间">
        <template slot-scope="{row}">
          <span>{{ row.sOrderCreateTime|formatTime}}</span>
        </template>
      </el-table-column>

      <el-table-column label="用户手机号">
        <template slot-scope="{row}">
          <span>{{ row.phoneNumber }}</span>
        </template>
      </el-table-column>

      <el-table-column label="购买金额">
        <template slot-scope="{row}">
          <span>{{ row.allSaleAmount }}</span>
        </template>
      </el-table-column>
      <el-table-column label="运费金额">
        <template slot-scope="{row}">
          <span>{{ row.allSaleFreightAmount }}</span>
        </template>
      </el-table-column>
      <el-table-column label="总金额">
        <template slot-scope="{row}">
          <span>{{ row.allSaleSumAmount }}</span>
        </template>
      </el-table-column>
      <el-table-column label="状态">
        <template slot-scope="{row}">
          <span>{{ row.saleOrderState|formatOrderState }}</span>
        </template>
      </el-table-column>
      <el-table-column label="操作" width="320" class-name="small-padding fixed-width">
        <template slot-scope="{row,$index}">
          <el-button v-if="row.saleOrderState!='FF02'" type="primary" size="mini" @click="handleUpdate(row)">
            修改
          </el-button>
          <el-button v-if="row.saleOrderState!='FF02'" size="mini" type="danger" @click="handleDelete(row,$index)">
            删除
          </el-button>
          <el-button v-if="row.saleOrderState!='FF02'" type="success" size="mini" @click="handleLock(row)">
            锁定
          </el-button>
        </template>
      </el-table-column>
    </el-table>

    <pagination v-show="total>0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="getList" />

    <el-dialog v-el-drag-dialog :title="dialogTitle" :visible.sync="dialogFormVisible" width="100%">
      <el-form ref="dataForm" :model="temp" label-position="right" label-width="100px">

        <fieldset>
          <legend>订单信息</legend>
          <el-row>
            <el-col :span="8">
              <el-form-item label="下单号">
                <el-input v-model="temp.sOrderNum" />
              </el-form-item>
            </el-col>

            <el-col :span="8">
              <el-form-item label="订单时间">
                <el-date-picker v-model="temp.sOrderCreateTime" typeof="date" value-format="yyyy-MM-dd" />
              </el-form-item>
            </el-col>

            <el-col :span="8">
              <el-form-item prop="phoneNumber" :rules="rules.checkNull" label="手机号码">
                <el-select v-model="temp.phoneNumber" remote filterable clearable placeholder="请选择手机号码" :remote-method="getUserInfoList" :loading="userInfoLoad" @focus="getUserInfoList">
                  <el-option v-for="(item,index) in userInfoSListDetail"
                             :key="index"
                             :label="item.phoneNumber"
                             :value="item.phoneNumber">
                    <span style="float: left">{{ item.phoneNumber }}</span>
                    <el-popover placement="right"
                                width="auto"
                                trigger="hover">
                      <el-form-item label="用户名:">
                        <span>{{ item.userName }}</span>
                      </el-form-item>
                      <el-form-item label="昵称:">
                        <span>{{ item.nickName }}</span>
                      </el-form-item>
                      <el-form-item label="性别:">
                        <span>{{ item.gender }}</span>
                      </el-form-item>
                      <el-form-item label="地址:">
                        <span>{{ item.receAddress }}</span>
                      </el-form-item>
                      <span style="float: right; color: #8492a6; font-size: 13px" slot="reference">{{ item.nickName }}</span>
                    </el-popover>
                  </el-option>
                </el-select>
              </el-form-item>

            </el-col>
          </el-row>


        </fieldset>

        <fieldset>
          <legend>订单详情信息</legend>

          <el-table v-loading="listLoading" :data="temp.saleOrderInfoViewModels" border fit highlight-current-row :summary-method="getSummaries" show-summary>
            <!--<el-table-column align="center" label="流水编号">
            <template slot-scope="{row}">
              <el-input v-model="row.ssOrderNum" readonly />
            </template>
          </el-table-column>-->

            <el-table-column align="center" label="国内快递">
              <template slot-scope="{row,$index}">
                <el-form-item :prop="'saleOrderInfoViewModels.'+$index+'.expressCompanyCode'" >
                  <el-select v-model="row.expressCompanyCode" filterable clearable>
                    <el-option v-for="item in expressCompanyCodeSList"
                               :key="item.value"
                               :label="item.label"
                               :value="item.value">
                    </el-option>
                  </el-select>
                </el-form-item>
              </template>
            </el-table-column>

            <el-table-column align="center" label="快递单号" prop="expressNumber">
              <template slot-scope="{row,$index}">
                <el-form-item :prop="'saleOrderInfoViewModels.'+$index+'.expressNumber'">
                  <el-input v-model="row.expressNumber" style="width:88%" />
                  <a @click="queryLogisticsInfo(row.expressCompanyCode,row.expressNumber)"><i class="el-icon-search"></i></a>
                </el-form-item>
              </template>
            </el-table-column>

            <el-table-column align="center" label="运费" width="120" prop="saleFreightAmount">
              <template slot-scope="{row,$index}">
                <el-form-item :prop="'saleOrderInfoViewModels.'+$index+'.saleFreightAmount'" :rules="rules.checkIntAndNull">
                  <el-input v-model="row.saleFreightAmount" />
                </el-form-item>
              </template>
            </el-table-column>

            <el-table-column align="center" label="商品名称" prop="sProductCode">
              <template slot-scope="{row,$index}">
                <el-form-item :prop="'saleOrderInfoViewModels.'+$index+'.sProductCode'" :rules="rules.checkNull">
                  <el-select v-model="row.sProductCode" remote filterable clearable placeholder="请选择商品" :remote-method="getProductStockDetail" :loading="pProductCodeLoad" @focus="getProductStockDetail">
                    <el-option v-for="(item,index) in pProductCodeSListDetail"
                               :key="index"
                               :label="item.productName"
                               :value="item.productCode">
                      <span style="float: left">{{ item.productName }}</span>
                      <el-popover placement="right"
                                  width="auto"
                                  trigger="hover">

                        <el-form-item label="库存量:">
                          <span>{{ item.productStock }}</span>
                        </el-form-item>
                        <!--<el-form-item label="供应商名称:">
                        <span>{{ item.businessName }}</span>
                      </el-form-item>
                      <el-form-item label="品牌名称:">
                        <span>{{ item.brandName }}</span>
                      </el-form-item>
                      <el-form-item label="分类:">
                        <span>{{ item.categoryName }}</span>
                      </el-form-item>
                      <el-form-item label="型号:">
                        <span>{{ item.productTypeName }}</span>
                      </el-form-item>
                      <el-form-item label="规格:">
                        <span>{{ item.specsName }}</span>
                      </el-form-item>
                      <el-form-item label="颜色:">
                        <span>{{ item.productColor  }}</span>
                      </el-form-item>
                      <el-form-item label="单位:">
                        <span>{{ item.unitName  }}</span>
                      </el-form-item>-->


                        <span style="float: right; color: #8492a6; font-size: 13px" slot="reference">
                          {{ item.simpleCode }}
                        </span>



                      </el-popover>
                    </el-option>
                  </el-select>
                </el-form-item>


              </template>
            </el-table-column>



            <el-table-column align="center" label="数量" width="120" prop="saleCount">

              <template slot-scope="{row,$index}">



                <el-form-item :prop="'saleOrderInfoViewModels.'+$index+'.saleCount'" :rules="rules.checkSaleCount">


                  <el-input v-model="row.saleCount" />
                </el-form-item>
              </template>
            </el-table-column>





            <el-table-column align="center" label="单价" width="120" prop="salePrice">
              <template slot-scope="{row,$index}">
                <el-form-item :prop="'saleOrderInfoViewModels.'+$index+'.salePrice'" :rules="rules.validatePrice">
                  <el-input v-model="row.salePrice" />
                </el-form-item>
              </template>
            </el-table-column>

            <el-table-column align="center" label="金额" width="120" prop="saleAmount">
              <template slot-scope="{row,$index}">
                <span hidden>{{sumSaleAmount(row)}}</span>
                <el-input v-model="row.saleAmount" readonly />
              </template>
            </el-table-column>


            <el-table-column align="center" label="理赔金额" width="120" prop="saleSettlementAmount">
              <template slot-scope="{row,$index}">
                <el-form-item :prop="'saleOrderInfoViewModels.'+$index+'.saleSettlementAmount'" :rules="rules.checkIntAndNull">
                  <el-input v-model="row.saleSettlementAmount" />
                </el-form-item>
              </template>
            </el-table-column>
            <el-table-column align="center" label="总金额" width="120" prop="saleSumAmount">
              <template slot-scope="{row,$index}">
                <span hidden>{{sumSaleSumAmount(row)}}</span>
                <el-input v-model="row.saleSumAmount" readonly />
              </template>
            </el-table-column>

            <el-table-column align="center" label="状态" width="120">
              <template slot-scope="{row,$index}">
                <el-form-item :prop="'saleOrderInfoViewModels.'+$index+'.saleOrderInfoState'" :rules="rules.checkNull">
                  <el-select v-model="row.saleOrderInfoState">
                    <el-option v-for="item in saleOrderInfoStateSList"
                               :key="item.value"
                               :label="item.label"
                               :value="item.value">
                    </el-option>
                  </el-select>
                </el-form-item>
              </template>
            </el-table-column>


            <el-table-column align="center" label="操作" width="80">
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

    <el-dialog title="物流信息" :visible.sync="dialogLogisticsVisible" width="60%" top="50">
      <iframe :src="logisticsUrl" style="width:100%;height:1000px;overflow:hidden;"></iframe>
    </el-dialog>

  </div>
</template>

<script>
  import { getSaleOrderInfoViewModelList as fetchSaleOrderInfoList } from '@/api/saleorder' // waves
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
      formatTime: function (val) {
        return myAction.formatTime(val);
      },
      formatOrderState: function (val) {
        return myAction.formatOrderState(val);
      }


    },
    data() {
     
      let currentData = {
        filterModel:
        {
          sOrderNum:
          {
            field: "SOrderNum",
            method: "Contains",
            value: "",
            prefix: "",
            operator: "And"
          },
          phoneNumber:
          {
            field: "PhoneNumber",
            method: "Contains",
            value: "",
            prefix: "",
            operator: "And"
          },
          sOrderCreateTime: {
            field: "SOrderCreateTime",
            method: "BetweenTime",
            value: "",
            prefix: "",
            operator: "And"
          }

        },
        temp: {
          "saleOrderInfoViewModels": [
            {

              "productName": "",
              "simpleCode": "",
              "sOrderNum": "",
              "pOrderNum": "",
              "expressCompanyCode": "",
              "expressNumber": "",
              "sProductCode": "",
              "saleCount": 0,
              "salePrice": 0,
              "saleAmount": 0,
              "saleFreightAmount": 0,
              "saleSumAmount": 0,
              "saleSettlementAmount": 0,
              "id": 0,
              "createTime": "",
              "createUserCode": "",
              "updateTime": "",
              "updateUserCode": "",
              "saleOrderInfoState": "",
              "remark": ""
            }
          ],
          "sOrderNum": "",
          "sOrderCreateTime": "",
          "phoneNumber": "",
          "allSaleAmount": 0,
          "allSaleFreightAmount": 0,
          "allSaleSumAmount": 0,
          "allSaleSettlementAmount": 0,
          "id": 0,
          "createTime": "",
          "saleOrderState": "",
          "createUserCode": "",
          "updateTime": "",
          "updateUserCode": "",
          "remark": ""
        },
        orderArr: [],
      };
    //  console.log(myAction.setBaseVueData)
      let data = $.extend(false, myAction.setBaseVueData, currentData);
      let custRules = {
        //购买数量
        checkSaleCount: [
          {
            required: true, message: '不能为空', trigger: ['blur', 'change']
          },
          {
            validator: (rule, value, callback) => {
              let productProp = rule.field.split('.')[0] + "." + rule.field.split('.')[1] + "." + "sProductCode"
              let $dataArr = this.$refs["dataForm"].$data.fields;
              let productCode = $dataArr.filter(function (x) {
                return x.prop == productProp;
              })[0].fieldValue;

              //为空
              if (productCode == "") {
                myAction.getNotifyFunc({ message: "请选择商品", resultSign: 2 }, this);
                return callback(new Error("请选择商品"));
              }

              const reg = /^[1-9][0-9]*$/;
              if (reg.test(value)) {
                var url = "/PurchaseOrder/CheckProductStock";
                var data = { productCode: productCode, saleCount: value };
                this.$ajax(url, data, { method: "get" }).then(response => {
                  if (response.resultSign == 2) {
                    myAction.getNotifyFunc(response, this);
                    return callback(new Error(response.message));
                  } else {
                    callback();
                  }
                })
              } else {
                return callback(new Error('必须为正整数'));
              }
            }, trigger: 'blur',
          }
        ]
      };
      data.rules = $.extend(false, myAction.setBaseValidateRules(), custRules);
      data.userInfoSListDetail = []
      data.pProductCodeSListDetail = []
      data.userInfoLoad = true;
      data.pProductCodeLoad = true;
      data.sOrderNum = myAction.newOrderNumber()
      data.saleOrderInfoStateSList = [{
        value: 'SS01',
        label: '未发货'
      }, {
        value: 'SS02',
        label: '已发货'
      }, {
        value: 'SS03',
        label: '已签收'
      }, {
        value: 'SS04',
        label: '已丢失'
        }]
      data.multipleSelection=[]
      data.expressCompanyCodeSList = []
      data.dialogLogisticsVisible = false
      data.logisticsUrl = "#"
      data.downloadLoading=false
      return data
    },
    //beforeDestory() {
    //  console.log("1111销毁1111")
    //},
    //destoryed() {
    //  console.log("销毁1111")
    //},
    mounted() {
     
      //console.log("5555销毁1111")
    },
    created() {
      //sthis.getList()
      this.getList()
      //console.log("000")

      this.getExpressCompanyCodeSelectList()
    },
    methods: {
     
      getList() {
        //console.log();
        this.listLoading = true
        let url = "/SaleOrder/GetSaleOrderViewModelPageList";
        let data = myAction.getQueryModel(this.listQuery.limit, this.listQuery.page, this.total, this.filterModel, this.orderArr)
        this.$ajax(url, data).then(response => {
          this.list = response.data
          this.total = response.total

          //console.log(this.filterModel.sOrderCreateTime.value)
          // Just to simulate the time of the request
          setTimeout(() => {
            this.listLoading = false
          }, 1.5 * 300)
        })
      },
      //获取用户信息
      getUserInfoList(keyName) {
        this.userInfoLoad = true;
        let url = "/UserInfo/GetUserInfoList";
        if (typeof (keyName) == "object") keyName = "";
        let data = { keyName: keyName };
        this.$ajax(url, data, { method: "get" }).then(response => {
          this.userInfoSListDetail = response.data;
          this.userInfoLoad = false;
        });
      },
      //查询产品详情的数据
      getProductStockDetail(keyName) {
        this.pProductCodeLoad = true;
        let url = "/PurchaseOrder/GetProductStockViewModelList";
        if (typeof (keyName) == "object") keyName = "";
        let data = { keyName: keyName };
        this.$ajax(url, data, { method: "get" }).then(response => {
          this.pProductCodeSListDetail = response.data;
          this.pProductCodeLoad = false;
        })
      },
      //详情
      getSaleOrderInfoViewModelList(row) {

        if (row.saleOrderInfoViewModels == null) {
          fetchSaleOrderInfoList(row).then(response => {
            row.saleOrderInfoViewModels = response.data;
          });
        }

      },
      getExpressCompanyCodeSelectList() {
        let url = "/ExpressCompany/GetSelectViewModelList";
        let data = {};
        this.$ajax(url, data, { method: "get" }).then(response => {
          this.expressCompanyCodeSList = response.data;
        })
      },
      //小计
      sumSaleAmount(row) {
        let saleAmount = row.saleCount * row.salePrice;
        row.saleAmount = saleAmount.toFixed(2);
      },
      //小计+运费
      sumSaleSumAmount(row) {
        let saleSettlementAmount = parseFloat(row.saleSettlementAmount)
        let allAmount = 0;
        if (saleSettlementAmount > 0) {
          allAmount = saleSettlementAmount;
          row.salePrice = 0;//东西丢了 库存减少 相当于单价变成0
        } else {
          allAmount = parseFloat(row.saleAmount) + parseFloat(row.saleFreightAmount);
        }
        row.saleSumAmount = allAmount.toFixed(2);
      },
      //合计
      getSummaries(param) {
        const { columns, data } = param;
        const sums = [];
        columns.forEach((column, index) => {
          if (index === 0) {
            sums[index] = '总价';
            return;
          }

          if (column.property == undefined) {
            return false;
          }

          const values = data.map(item => Number(item[column.property]));

          if (!values.every(value => isNaN(value))) {
            sums[index] = values.reduce((prev, curr) => {
              const value = Number(curr);
              if (!isNaN(value)) {
                return prev + curr;
              } else {
                return prev;
              }
            }, 0);
            sums[index] = sums[index].toFixed(2);
            this.setHJValue(column.property, sums[index]);
            sums[index] += '';
          } else {
            sums[index] = '';
          }
        });

        return sums;
      },
      //合计上面的字段 赋值
      setHJValue(prop, val) {

        switch (prop) {
          case "saleAmount": this.temp.allSaleAmount = val; break;
          case "saleFreightAmount": this.temp.allSaleFreightAmount = val; break;
          case "saleSettlementAmount": this.temp.allSaleSettlementAmount = val; break;
          case "saleSumAmount": this.temp.allSaleSumAmount = val; break;
          default: break;
        }
      },
      tableRowClassName({ row, rowIndex }) {
        if (rowIndex === 1) {
          return 'warning-row';
        } else if (rowIndex === 3) {
          return 'success-row';
        }
        return '';
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
        let ssOrderNum = myAction.newOrderNumber();
        let saleOrderInfoState = "SS01";//未发货
        let saleOrderState = "FF01";//未完成
        this.temp = {
          "saleOrderInfoViewModels": [
            {
              "productName": "",
              "simpleCode": "",
              "sOrderNum": this.sOrderNum,
              "ssOrderNum": ssOrderNum,
              "expressCompanyCode": "",
              "expressNumber": "",
              "sProductCode": "",
              "saleCount": 0,
              "salePrice": 0,
              "saleAmount": 0,
              "saleFreightAmount": 0,
              "saleSumAmount": 0,
              "saleSettlementAmount": 0,
              "id": 0,
              "createTime": "",
              "createUserCode": "",
              "updateTime": "",
              "updateUserCode": "",
              "saleOrderInfoState": saleOrderInfoState,
              "remark": ""
            }
          ],
          "sOrderNum": this.sOrderNum,
          "sOrderCreateTime": "",
          "userPurchase": "",
          "userPurchasePhone": "",
          "userPurchaseGender": 0,
          "userPurchaseAddress": "",
          "allSaleAmount": 0,
          "allSaleFreightAmount": 0,
          "allSaleSumAmount": 0,
          "allSaleSettlementAmount": 0,
          "id": 0,
          "createTime": "",
          "createUserCode": "",
          "saleOrderState": saleOrderState,
          "updateTime": "",
          "updateUserCode": "",
          "remark": ""
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
        this.dialogTitle = '新增销售订单'
        this.dialogFormVisible = true
        this.$nextTick(() => {
          this.$refs['dataForm'].clearValidate()
        })
      },
      createData() {
        this.$refs['dataForm'].validate((valid) => {
          if (valid) {
            var url = "/SaleOrder/SaveData";
            var data = this.temp;
            this.$ajax(url, data).then(response => {
              var d = response.data;
              this.list.unshift(d)
              this.total++;
              this.dialogFormVisible = false
              myAction.getNotifyFunc(response, this);
            })
          }
        })
      },
      //修改 页面
      handleUpdate(row) {

        if (row.saleOrderInfoViewModels == null) {
          fetchSaleOrderInfoList(row).then(response => {
            row.saleOrderInfoViewModels = response.data;
            this.temp = Object.assign({}, row) // copy obj
          });
        } else {
          this.temp = Object.assign({}, row) // copy obj
        }

        this.dialogStatus = 'update'
        this.dialogTitle = '修改销售订单'
        this.dialogFormVisible = true
        
        if (this.pProductCodeSListDetail.length == 0) this.getProductStockDetail("")
        this.$nextTick(() => {
          this.$refs['dataForm'].clearValidate()
        })
      },
      //提交修改数据
      updateData() {
        this.$refs['dataForm'].validate((valid) => {
          if (valid) {
            const tempData = Object.assign({}, this.temp)
            var url = "/SaleOrder/SaveData";
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
        }).then(() => {
          let url = "/SaleOrder/GetDel"
          let data = { "sOrderNum": row.sOrderNum }
          this.$ajax(url, data, { method: "get" }).then(response => {
            if (response.resultSign == 0) {
              this.list.splice(index, 1)
              this.total--;
            }
            myAction.getNotifyFunc(response, this);
          })
        })
          .catch(action => {

          });
      },
      handleAddSaleOrderInfo(row) {
        let ssOrderNum = myAction.newOrderNumber();
        let saleOrderInfoState = "SS01";//未发货
        this.temp.saleOrderInfoViewModels.push(
          {
            "productName": "",
            "simpleCode": "",
            "sOrderNum": this.temp.sOrderNum,
            "ssOrderNum": ssOrderNum,
            "expressCompanyCode": row.expressCompanyCode,
            "expressNumber": row.expressNumber,
            "sProductCode": "",
            "saleCount": 0,
            "salePrice": 0,
            "saleAmount": 0,
            "saleFreightAmount": row.saleFreightAmount,
            "saleSumAmount": 0,
            "id": 0,
            "createTime": "",
            "createUserCode": "",
            "updateTime": "",
            "updateUserCode": "",
            "saleOrderInfoState": saleOrderInfoState,
            "remark": ""
          }
        );
      },
      handleDeleteSaleOrderInfo(row, index) {

        this.temp.saleOrderInfoViewModels.splice(index, 1)
      },
      handleLock(row) {
        //锁定数据
        var title = '<span style="color: red;">是否要锁定这条数据?</span>';
        this.$confirm(title, '提示', {
          dangerouslyUseHTMLString: true,
          type: 'warning',
          confirmButtonText: '确定',
          cancelButtonText: '取消'
        }).then(() => {

          let orderNumber = row.sOrderNum;
          let url = "/SaleOrder/GetSaleOrderInfoDoSign";
          let data = { orderNumber: orderNumber }
          this.$ajax(url, data, { method: "get" }).then(response => {

            if (response.resultSign == 1) {

              this.$confirm(response.message, '提示', {
                dangerouslyUseHTMLString: true,
                type: 'warning',
                confirmButtonText: '确定',
                cancelButtonText: '取消'
              }).then(() => {

                let orderNumber = row.sOrderNum;
                let url = "/SaleOrder/GetSaleOrderInfoLock";
                let data = { orderNumber: orderNumber }
                this.$ajax(url, data, { method: "get" }).then(response => {
                  if (response.resultSign == 0) {
                    row.saleOrderInfoState = "FF02";//锁定
                  }
                  myAction.getNotifyFunc(response, this);
                });

              });

            } else {

              let orderNumber = row.sOrderNum;
              let url = "/SaleOrder/GetSaleOrderInfoLock";
              let data = { orderNumber: orderNumber }
              this.$ajax(url, data, { method: "get" }).then(response => {
                if (response.resultSign == 0) {
                  row.saleOrderInfoState = "FF02";//锁定
                }
                myAction.getNotifyFunc(response, this);
              });

            }

          })

        })
      },
      //下载excel
      handleDownload() {
        this.downloadLoading = true
        var url = "/SaleOrder/GetSaleListViewModelExportExcel";
        var data = {};
        if (this.multipleSelection.length > 0) {
          let value = this.multipleSelection.map(function (x) {
            return x.sOrderNum;
          }).join(',');
          var excelModel = {
            pOrderNum:
            {
              field: "SOrderNum",
              method: "In",
              value: value,
              prefix: "",
              operator: "And"
            }
          }
          data = myAction.getQueryModel(0, 0, 0, excelModel, [])
        } else {
          data = myAction.getQueryModel(this.listQuery.limit, this.listQuery.page, this.total, this.filterModel, this.orderArr)
        }

        this.$ajax(url, data, {
          method: "post",
        }).then(res => {

          this.downloadLoading = false
          var bstr = atob(res.data.content),
            n = bstr.length,
            u8arr = new Uint8Array(n);
          while (n--) {
            u8arr[n] = bstr.charCodeAt(n);
          }

          var blob = new Blob([u8arr], {
            type: 'application/vnd.ms-excel;charset=utf-8'
          })
          // 针对于IE浏览器的处理, 因部分IE浏览器不支持createObjectURL
          if (window.navigator && window.navigator.msSaveOrOpenBlob) {
            window.navigator.msSaveOrOpenBlob(blob, res.data.fileName)
          } else {
            var downloadElement = document.createElement('a')
            var href = window.URL.createObjectURL(blob) // 创建下载的链接
            downloadElement.href = href
            downloadElement.download = res.data.fileName // 下载后文件名
            document.body.appendChild(downloadElement)
            downloadElement.click() // 点击下载
            document.body.removeChild(downloadElement) // 下载完成移除元素
            window.URL.revokeObjectURL(href) // 释放掉blob对象
          }

        })
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
        this.multipleSelection = val
      },
      queryLogisticsInfo(code, num) {
        this.dialogLogisticsVisible = true;
        this.logisticsUrl = "https://www.kuaidi100.com/chaxun?com=" + code + "&nu=" + num + "";
      }
    },
   

  }
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

  .el-popover .el-form-item {
    margin: 0px;
  }

  .el-table .warning-row {
    background: oldlace;
  }

  .el-table .success-row {
    background: #f0f9eb;
  }
</style>
<style>
  .el-table .el-form-item {
    margin-left: 0px !important;
    margin-bottom: 0px !important;
  }

  .el-table .el-form-item__content {
    margin-left: 0px !important;
  }

  .el-dialog__body {
    padding-top: 0px !important;
  }
</style>


