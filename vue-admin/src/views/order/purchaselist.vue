<template>
  <div class="app-container">
    <div class="filter-container">
      <el-collapse accordion v-model="activeName">
        <el-collapse-item title="查询条件" name="1">
          <el-form :inline="true" :model="filterModel">
            <el-form-item label="订单号">
              <el-input
                v-model="filterModel.pOrderNum.value"
                placeholder="订单号"
                class="filter-item"
                @keyup.enter.native="handleFilter"
              />
            </el-form-item>

            <el-form-item label="购买时间">
              <el-date-picker
                v-model="filterModel.pOrderCreateTime.value"
                :picker-options="daterangeOptions"
                value-format="yyyy-MM-dd"
                range-separator="至"
                start-placeholder="开始时间"
                end-placeholder="结束时间"
                style="width: 250px;"
                type="daterange"
                placement="bottom-end"
                unlink-panels
                @keyup.enter.native="handleFilter"
              />
            </el-form-item>

            <el-form-item label="供货商">
              <el-select
                v-model="filterModel.businessCode.value"
                clearable
                filterable
                placeholder="供应商"
                @keyup.enter.native="handleFilter"
                @focus="getBusinessCodeSelectList"
              >
                <el-option
                  v-for="(item,index) in businessCodeSList"
                  :key="index"
                  :label="item.label"
                  :value="item.value"
                ></el-option>
              </el-select>
            </el-form-item>

            <el-form-item label="转运仓">
              <el-select
                v-model="filterModel.transferBinCode.value"
                clearable
                filterable
                placeholder="转运仓"
                @keyup.enter.native="handleFilter"
                @focus="getTransferBinCodeSelectList"
              >
                <el-option
                  v-for="(item,index) in transferBinCodeSList"
                  :key="index"
                  :label="item.label"
                  :value="item.value"
                ></el-option>
              </el-select>
            </el-form-item>

            <el-form-item label="标题">
              <el-input
                v-model="filterModel.pOrderTitle.value"
                placeholder="标题"
                class="filter-item"
                @keyup.enter.native="handleFilter"
              />
            </el-form-item>
            <el-form-item label="购货金额">
              <el-input
                v-model="filterModel.allPurchaseAmount.value"
                placeholder="购货金额"
                class="input-with-select"
                @keyup.enter.native="handleFilter"
              >
                <el-select
                  v-model="filterModel.allPurchaseAmount.method"
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

            <el-form-item label="国际运费">
              <el-input
                v-model="filterModel.allInternationFreightAmount.value"
                placeholder="国际运费"
                class="input-with-select"
                @keyup.enter.native="handleFilter"
              >
                <el-select
                  v-model="filterModel.allInternationFreightAmount.method"
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

            <el-form-item label="国内运费">
              <el-input
                v-model="filterModel.allDomesticFreightAmount.value"
                placeholder="国内运费"
                class="input-with-select"
                @keyup.enter.native="handleFilter"
              >
                <el-select
                  v-model="filterModel.allDomesticFreightAmount.method"
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

 <el-form-item label="产品名称">
              <el-input
                v-model="filterModel.productName.value"
                placeholder="产品名称"
                class="filter-item"
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
      @expand-change="getPurchaseOrderInfoViewModelList"
      ref="multipleTable"
      @selection-change="handleSelectionChange"
    >
      <el-table-column type="selection" width="55"></el-table-column>
      <el-table-column type="expand">
        <template slot-scope="{row}">
          <el-table
            :data="row.purchaseOrderInfoViewModels"
            style="width: 100%"
            :row-class-name="tableRowClassName"
          >
            <!--<el-table-column prop="ppOrderNum"
                   label="流水号">
            </el-table-column>-->

            <el-table-column prop="expressNumber" label="国际单号">
              <template slot-scope="{row}">
                <a
                  @click="queryLogisticsInfo(row.expressCompanyCode,row.expressNumber)"
                >{{ row.expressNumber }}</a>
              </template>
            </el-table-column>
            <el-table-column prop="expressFreightAmount" label="国际运费"></el-table-column>

            <el-table-column prop="internationNumber" label="国际转运单号">
              <template slot-scope="{row}">
                <a
                  @click="queryLogisticsInfo(row.internationCompanyCode,row.internationNumber)"
                >{{ row.internationNumber }}</a>
              </template>
            </el-table-column>
            <el-table-column prop="internationFreightAmount" label="国际转运运费"></el-table-column>

            <el-table-column prop="domesticNumber" label="国内转运单号">
              <template slot-scope="{row}">
                <a
                  @click="queryLogisticsInfo(row.domesticCompanyCode,row.domesticNumber)"
                >{{ row.domesticNumber }}</a>
              </template>
            </el-table-column>
            <el-table-column prop="domesticFreightAmount" label="国内转运运费"></el-table-column>
            <el-table-column prop="productName" label="商品名称"></el-table-column>
            <el-table-column prop="simpleCode" label="简码"></el-table-column>

            <el-table-column prop="purchaseCount" label="数量"></el-table-column>
            <el-table-column prop="purchasePrice" label="单价"></el-table-column>
            <el-table-column prop="purchaseAmount" label="金额"></el-table-column>
            <el-table-column prop="purchaseSettlementAmount" label="理赔金额"></el-table-column>
            <el-table-column prop="amount" label="总金额"></el-table-column>
            <el-table-column prop="purchaseOrderInfoState" label="状态">
              <template slot-scope="{row}">
                <span>{{ row.purchaseOrderInfoState|formatOrderState }}</span>
              </template>
            </el-table-column>
          </el-table>
        </template>
      </el-table-column>
      <el-table-column label="订单号" prop="pOrderNum" sortable="custom">
        <template slot-scope="{row}">
          <span>{{ row.pOrderNum }}</span>
        </template>
      </el-table-column>
      <el-table-column label="标题" prop="pOrderTitle" sortable="custom">
        <template slot-scope="{row}">
          <span>{{ row.pOrderTitle }}</span>
        </template>
      </el-table-column>
      <el-table-column label="供货商" prop="businessName" sortable="custom">
        <template slot-scope="{row}">
          <span>{{ row.businessName }}</span>
        </template>
      </el-table-column>
      <el-table-column label="购买时间" prop="pOrderCreateTime" sortable="custom">
        <template slot-scope="{row}">
          <span>{{ row.pOrderCreateTime|formatTime }}</span>
        </template>
      </el-table-column>

      <el-table-column label="转运仓名称" prop="transferBinName" sortable="custom">
        <template slot-scope="{row}">
          <span>{{ row.transferBinName}}</span>
        </template>
      </el-table-column>
      <el-table-column label="购货金额" prop="allPurchaseAmount" sortable="custom">
        <template slot-scope="{row}">
          <span>{{ row.allPurchaseAmount }}</span>
        </template>
      </el-table-column>
      <el-table-column label="国际运费" prop="allInternationFreightAmount" sortable="custom">
        <template slot-scope="{row}">
          <span>{{ row.allInternationFreightAmount }}</span>
        </template>
      </el-table-column>
      <el-table-column label="国内运费" prop="allDomesticFreightAmount" sortable="custom">
        <template slot-scope="{row}">
          <span>{{ row.allDomesticFreightAmount }}</span>
        </template>
      </el-table-column>
      <el-table-column label="购货总金额" prop="allAmount" sortable="custom">
        <template slot-scope="{row}">
          <span>{{ row.allAmount }}</span>
        </template>
      </el-table-column>
      <el-table-column label="状态">
        <template slot-scope="{row}">
          <span>{{ row.purchaseOrderState|formatOrderState }}</span>
        </template>
      </el-table-column>
      <el-table-column label="操作" width="220" class-name="small-padding fixed-width">
        <template slot-scope="{row,$index}">
          <el-button
            v-if="row.purchaseOrderState!='FF02'"
            type="primary"
            size="mini"
            @click="handleUpdate(row)"
          >修改</el-button>
          <el-button
            v-if="row.purchaseOrderState!='FF02'"
            size="mini"
            type="danger"
            @click="handleDelete(row,$index)"
          >删除</el-button>
          <el-button
            v-if="row.purchaseOrderState!='FF02'"
            type="success"
            size="mini"
            @click="handleLock(row)"
          >锁定</el-button>
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

    <el-dialog
      v-el-drag-dialog
      :title="dialogTitle"
      :visible.sync="dialogFormVisible"
      fit
      width="100%"
    >
      <el-form ref="dataForm" :model="temp" label-position="right" label-width="100px">
        <fieldset>
          <legend>商品信息</legend>
          <el-row>
            <el-col :span="8">
              <el-form-item label="进货单号" prop="pOrderNum" :rules="rules.pOrderNum">
                <el-input v-model="temp.pOrderNum" @blur="pOrderNumBlur(temp.pOrderNum)" />
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="进货标题" prop="pOrderTitle" :rules="rules.checkNull">
                <el-input v-model="temp.pOrderTitle" />
              </el-form-item>
            </el-col>

            <el-col :span="8">
              <el-form-item label="进货时间" prop="pOrderCreateTime" :rules="rules.checkNull">
                <el-date-picker
                  v-model="temp.pOrderCreateTime"
                  typeof="date"
                  value-format="yyyy-MM-dd"
                />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row>
            <!--<el-col :span="8">
            <el-form-item label="美国单号" prop="usaNumber">
              <el-input v-model="temp.usaNumber" />
            </el-form-item>
            </el-col>-->
            <el-col :span="8">
              <el-form-item label="供应商" prop="businessCode" :rules="rules.checkNull">
                <el-select
                  v-model="temp.businessCode"
                  clearable
                  filterable
                  placeholder="请选择供应商"
                  @focus="getBusinessCodeSelectList"
                  @change="getBusinessCodeSelectChangeList"
                >
                  <el-option
                    v-for="(item,index) in businessCodeSList"
                    :key="index"
                    :label="item.label"
                    :value="item.value"
                  ></el-option>
                </el-select>
              </el-form-item>
            </el-col>

            <el-col :span="8">
              <el-form-item label="转运仓" prop="transferBinCode" :rules="rules.checkNull">
                <el-select
                  v-model="temp.transferBinCode"
                  clearable
                  filterable
                  placeholder="请选择转运仓"
                  @focus="getTransferBinCodeSelectList"
                  @change="getTransferBinCodeSelectChangeList"
                >
                  <el-option
                    v-for="(item,index) in transferBinCodeSList"
                    :key="index"
                    :label="item.label"
                    :value="item.value"
                  ></el-option>
                </el-select>
              </el-form-item>
            </el-col>
          </el-row>

          <!--<el-row>
          <el-col :span="8">
            <el-form-item label="消费金额">
              <el-input v-model="temp.purchaseOrder.allPurchaseAmount" :readonly="true" />
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item label="运费金额">
              <el-input v-model="temp.purchaseOrder.allFreightAmount" :readonly="true" />
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item label="总金额">
              <el-input v-model="temp.purchaseOrder.allAmount" :readonly="true" />
            </el-form-item>
          </el-col>
          </el-row>-->

          <el-row>
            <el-col :span="24">
              <el-form-item label="备注信息">
                <el-input
                  v-model="temp.remark"
                  type="textarea"
                  :autosize="{minRows: 2,maxRows: 5}"
                />
              </el-form-item>
            </el-col>
          </el-row>
        </fieldset>

        <fieldset>
          <legend>进货详情信息</legend>

          <el-table
            v-loading="listLoading"
            :data="temp.purchaseOrderInfoViewModels"
            border
            fit
            highlight-current-row
            :summary-method="getSummaries"
            show-summary
          >
            <!--<el-table-column align="center" label="进货详情单号">
            <template slot-scope="{row,$index}">

              <el-form-item :prop="'purchaseOrderInfoViewModels.'+$index+'.ppOrderNum'">
                <el-input v-model="row.ppOrderNum" :readonly="true" />
              </el-form-item>
            </template>
            </el-table-column>-->

            <el-table-column align="center" label="国际快递">
              <template slot-scope="{row,$index}">
                <el-form-item :prop="'purchaseOrderInfoViewModels.'+$index+'.expressCompanyCode'">
                  <el-select v-model="row.expressCompanyCode" filterable clearable>
                    <el-option
                      v-for="item in expressCompanyCodeSList"
                      :key="item.value"
                      :label="item.label"
                      :value="item.value"
                    ></el-option>
                  </el-select>
                </el-form-item>
              </template>
            </el-table-column>

            <el-table-column align="center" label="国际单号" width="180">
              <template slot-scope="{row,$index}">
                <el-form-item :prop="'purchaseOrderInfoViewModels.'+$index+'.expressNumber'">
                  <el-input v-model="row.expressNumber" style="width:88%;"></el-input>
                  <a @click="queryLogisticsInfo(row.expressCompanyCode,row.expressNumber)">
                    <i class="el-icon-search"></i>
                  </a>
                </el-form-item>
              </template>
            </el-table-column>

            <el-table-column align="center" label="国际运费(元)" width="60" prop="expressFreightAmount">
              <template slot-scope="{row,$index}">
                <el-form-item
                  :prop="'purchaseOrderInfoViewModels.'+$index+'.expressFreightAmount'"
                  :rules="rules.checkIntAndNull"
                >
                  <el-input v-model="row.expressFreightAmount" />
                </el-form-item>
              </template>
            </el-table-column>

            <el-table-column align="center" label="国际转运快递">
              <template slot-scope="{row,$index}">
                <el-form-item
                  :prop="'purchaseOrderInfoViewModels.'+$index+'.internationExpressCompanyCode'"
                >
                  <el-select v-model="row.internationExpressCompanyCode" filterable clearable>
                    <el-option
                      v-for="item in expressCompanyCodeSList"
                      :key="item.value"
                      :label="item.label"
                      :value="item.value"
                    ></el-option>
                  </el-select>
                </el-form-item>
              </template>
            </el-table-column>

            <el-table-column align="center" label="国际转运单号" width="180">
              <template slot-scope="{row,$index}">
                <el-form-item :prop="'purchaseOrderInfoViewModels.'+$index+'.internationNumber'">
                  <el-input v-model="row.internationNumber" style="width:88%"></el-input>
                  <a
                    @click="queryLogisticsInfo(row.internationExpressCompanyCode,row.internationNumber)"
                  >
                    <i class="el-icon-search"></i>
                  </a>
                </el-form-item>
              </template>
            </el-table-column>

            <el-table-column
              align="center"
              label="国际转运运费(元)"
              width="60"
              prop="internationFreightAmount"
            >
              <template slot-scope="{row,$index}">
                <el-form-item
                  :prop="'purchaseOrderInfoViewModels.'+$index+'.internationFreightAmount'"
                  :rules="rules.checkIntAndNull"
                >
                  <el-input v-model="row.internationFreightAmount" />
                </el-form-item>
              </template>
            </el-table-column>

            <el-table-column align="center" label="国内转运快递">
              <template slot-scope="{row,$index}">
                <el-form-item
                  :prop="'purchaseOrderInfoViewModels.'+$index+'.domesticExpressCompanyCode'"
                >
                  <el-select v-model="row.domesticExpressCompanyCode" filterable clearable>
                    <el-option
                      v-for="item in expressCompanyCodeSList"
                      :key="item.value"
                      :label="item.label"
                      :value="item.value"
                    ></el-option>
                  </el-select>
                </el-form-item>
              </template>
            </el-table-column>

            <el-table-column align="center" label="国内转运单号" width="180">
              <template slot-scope="{row,$index}">
                <el-form-item :prop="'purchaseOrderInfoViewModels.'+$index+'.domesticNumber'">
                  <el-input v-model="row.domesticNumber" style="width:88%;"></el-input>
                  <a @click="queryLogisticsInfo(row.domesticExpressCompanyCode,row.domesticNumber)">
                    <i class="el-icon-search"></i>
                  </a>
                </el-form-item>
              </template>
            </el-table-column>

            <el-table-column align="center" label="国内运费(元)" width="60" prop="domesticFreightAmount">
              <template slot-scope="{row,$index}">
                <el-form-item
                  :prop="'purchaseOrderInfoViewModels.'+$index+'.domesticFreightAmount'"
                  :rules="rules.checkIntAndNull"
                >
                  <el-input v-model="row.domesticFreightAmount" />
                </el-form-item>
              </template>
            </el-table-column>

            <el-table-column align="center" label="商品名称">
              <template slot-scope="{row,$index}">
                <el-form-item
                  :prop="'purchaseOrderInfoViewModels.'+$index+'.pProductCode'"
                  :rules="rules.checkNull"
                >
                  <el-select
                    v-model="row.pProductCode"
                    remote
                    filterable
                    clearable
                    placeholder="请选择商品"
                    :remote-method="getSelectProductDetial"
                    :loading="pProductCodeLoad"
                    @focus="getSelectProductDetial"
                  >
                    <el-option
                      v-for="(item,index) in pProductCodeSListDetail"
                      :key="index"
                      :label="item.productName"
                      :value="item.productCode"
                    >
                      <span style="float: left">{{ item.productName }}</span>
                      <span
                        style="float: right; color: #8492a6; font-size: 13px"
                      >{{ item.simpleCode }}</span>
                    </el-option>
                  </el-select>
                </el-form-item>
              </template>
            </el-table-column>

            <el-table-column align="center" label="数量" width="60" prop="purchaseCount">
              <template slot-scope="{row,$index}">
                <el-form-item
                  :prop="'purchaseOrderInfoViewModels.'+$index+'.purchaseCount'"
                  :rules="rules.checkIntAndNull"
                >
                  <el-input v-model="row.purchaseCount" />
                </el-form-item>
              </template>
            </el-table-column>
            <el-table-column align="center" label="单价(元)" width="80" prop="purchasePrice">
              <template slot-scope="{row,$index}">
                <el-form-item
                  :prop="'purchaseOrderInfoViewModels.'+$index+'.purchasePrice'"
                  :rules="rules.validatePrice"
                >
                  <el-input v-model="row.purchasePrice" />
                </el-form-item>
              </template>
            </el-table-column>

            <el-table-column align="center" label="金额(元)" width="100" prop="purchaseAmount">
              <template slot-scope="{row,$index}">
                <span hidden>{{sumPurchaseAmount(row)}}</span>
                <el-input v-model="row.purchaseAmount" readonly />
              </template>
            </el-table-column>

            <el-table-column
              align="center"
              label="理赔金额(元)"
              width="60"
              prop="purchaseSettlementAmount"
            >
              <template slot-scope="{row,$index}">
                <el-form-item
                  :prop="'purchaseOrderInfoViewModels.'+$index+'.purchaseSettlementAmount'"
                  :rules="rules.checkIntAndNull"
                >
                  <el-input v-model="row.purchaseSettlementAmount" />
                </el-form-item>
              </template>
            </el-table-column>

            <el-table-column align="center" label="总金额(元)" width="100" prop="amount">
              <template slot-scope="{row}">
                <span hidden>{{sumAmount(row)}}</span>
                <el-input v-model="row.amount" readonly />
              </template>
            </el-table-column>

            <el-table-column align="center" label="状态" width="80">
              <template slot-scope="{row,$index}">
                <el-form-item
                  :prop="'purchaseOrderInfoViewModels.'+$index+'.purchaseOrderInfoState'"
                  :rules="rules.checkNull"
                >
                  <el-select v-model="row.purchaseOrderInfoState">
                    <el-option
                      v-for="item in purchaseOrderInfoStateSList"
                      :key="item.value"
                      :label="item.label"
                      :value="item.value"
                    ></el-option>
                  </el-select>
                </el-form-item>
              </template>
            </el-table-column>

            <el-table-column align="center" label="操作" width="80">
              <template slot-scope="{row,$index}">
                <el-button
                  v-if="$index==0"
                  size="mini"
                  type="success"
                  @click="handleAddPurchaseOrderInfo(row)"
                >新增</el-button>
                <el-button
                  v-if="$index!=0"
                  size="mini"
                  type="danger"
                  @click="handleDeletePurchaseOrderInfo(row,$index)"
                >删除</el-button>
              </template>
            </el-table-column>
          </el-table>
        </fieldset>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogFormVisible = false">关闭</el-button>
        <el-button type="primary" @click="dialogStatus==='create'?createData():updateData()">保存</el-button>
      </div>
    </el-dialog>

    <el-dialog title="物流信息" :visible.sync="dialogLogisticsVisible" width="60%" top="50">
      <iframe :src="logisticsUrl" style="width:100%;height:1000px;overflow:hidden;"></iframe>
    </el-dialog>
  </div>
</template>


<script>
import { getPurchaseOrderInfoViewModelList as fetchPurchaseOrderInfoList } from "@/api/purchaseorder"; // waves directive
import waves from "@/directive/waves"; // waves directive
import Pagination from "@/components/Pagination"; // secondary package based on el-pagination
import myAction from "@/utils/baseutil";
//console.log("0")

export default {
  name: "purchaselist",
  components: { Pagination },
  directives: { waves },
  filters: {
    statusFilter(status) {
      const statusMap = {
        published: "success",
        draft: "info",
        deleted: "danger"
      };
      return statusMap[status];
    },
    formatTime: function(val) {
      return myAction.formatTime(val);
    },
    formatOrderState: function(val) {
      return myAction.formatOrderState(val);
    }
  },
  beforeCreate() {
    // console.log("1")
  },
  data() {
    let currentData = {
      filterModel: {
        pOrderNum: {
          field: "POrderNum",
          method: "Contains",
          value: "",
          prefix: "M.",
          operator: "And"
        },
        pOrderTitle: {
          field: "POrderTitle",
          method: "Contains",
          value: "",
          prefix: "M.",
          operator: "And"
        },
        transferBinCode: {
          field: "TransferBinCode",
          method: "Equal",
          value: "",
          prefix: "M.",
          operator: "And"
        },
        businessCode: {
          field: "BusinessCode",
          method: "Equal",
          value: "",
          prefix: "M.",
          operator: "And"
        },
        pOrderCreateTime: {
          field: "POrderCreateTime",
          method: "BetweenTime",
          value: "",
          prefix: "M.",
          operator: "And"
        },
        allPurchaseAmount: {
          field: "AllPurchaseAmount",
          method: "Equal",
          value: "",
          prefix: "M.",
          operator: "And"
        },
        allInternationFreightAmount: {
          field: "AllInternationFreightAmount",
          method: "Equal",
          value: "",
          prefix: "M.",
          operator: "And"
        },
        allDomesticFreightAmount: {
          field: "AllDomesticFreightAmount",
          method: "Equal",
          value: "",
          prefix: "M.",
          operator: "And"
        },
         productName: {
          field: "ProductName",
          method: "Contains",
          value: "",
          prefix: "F.",
          operator: "And"
        },
      },
      temp: {
        businessName: "",
        transferBinName: "",
        purchaseOrderInfoViewModels: [
          {
            productCode: "",
            productName: "",
            simpleCode: "",
            code: "",
            pOrderNum: "",
            ppOrderNum: "",
            expressCompanyCode: "",
            expressNumber: "",
            expressFreightAmount: 0,
            internationExpressCompanyCode: "",
            internationNumber: "",
            internationFreightAmount: 0,
            domesticExpressCompanyCode: "",
            domesticNumber: "",
            domesticFreightAmount: 0,
            pProductCode: "",
            purchaseCount: 0,
            purchasePrice: 0,
            purchaseAmount: 0,
            amount: 0,
            purchaseSettlementAmount: 0,
            purchaseOrderInfoState: "",
            id: 0,
            createTime: "",
            createUserCode: "",
            updateTime: "",
            updateUserCode: "",
            remark: ""
          }
        ],
        pOrderNum: "",
        businessCode: "",
        pOrderTitle: "",
        pOrderCreateTime: "",
        usaNumber: "",
        transferBinCode: "",
        allPurchaseAmount: 0,
        allInternationFreightAmount: 0,
        allDomesticFreightAmount: 0,
        allPurchaseSettlementAmount: 0,
        allAmount: 0,
        purchaseOrderState: "",
        id: 0,
        createTime: "",
        createUserCode: "",
        updateTime: "",
        updateUserCode: "",
        remark: ""
      },
      orderArr: []
    };
    let data = $.extend(false, myAction.setBaseVueData, currentData);
    //console.log("2")
    let custRules = {
      pOrderNum: [
        { required: true, message: "请输入订单号", trigger: "blur" },
        {
          validator: (rule, value, callback) => {
            let obj = {
              url: "/PurchaseOrder/GetPOrderNumIsExist",
              data: { id: this.temp.id, pOrderNum: value },
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
    data.transferBinCodeSList = [];
    data.businessCodeSList = [];
    data.pProductCodeSListDetail = [];
    data.pProductCodeLoad = false;
    // data.pOrderNum = myAction.newOrderNumber()
    data.downloadLoading = false;
    data.purchaseOrderInfoStateSList = [
      {
        value: "SS01",
        label: "未发货"
      },
      {
        value: "SS02",
        label: "已发货"
      },
      {
        value: "SS03",
        label: "已签收"
      },
      {
        value: "SS04",
        label: "已丢失"
      }
    ];
    data.multipleSelection = [];
    data.expressCompanyCodeSList = [];
    data.dialogLogisticsVisible = false;
    data.logisticsUrl = "#";
    data.activeName = "1";
    return data;
  },
  created() {
    this.getList();
    // this.getTransferBinCodeSelectList()
    // this.getBusinessCodeSelectList()
    this.getExpressCompanyCodeSelectList();
  },
  methods: {
    getList() {
      this.listLoading = true;
      var url = "/PurchaseOrder/GetPurchaseOrderPageList";
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
    pOrderNumBlur(val) {
      $.each(this.temp.purchaseOrderInfoViewModels, function(i, e) {
        e.pOrderNum = val;
      });
    },
    //查询产品详情的数据
    getSelectProductDetial(keyName) {
      this.pProductCodeLoad = true;
      let url = "/Product/getSelectProductDetial";
      if (typeof keyName == "object") keyName = "";
      let data = { keyName: keyName };
      this.$ajax(url, data, { method: "get" }).then(response => {
        this.pProductCodeSListDetail = response.data;
        this.pProductCodeLoad = false;
      });
    },
    //获取产品分类
    getTransferBinCodeSelectList() {
      let url = "/TransferBin/GetSelectViewModelList";
      let data = {};
      this.$ajax(url, data, { method: "get" }).then(response => {
        this.transferBinCodeSList = response.data;
      });
    },
    getTransferBinCodeSelectChangeList(val) {
      this.temp.transferBinName = this.transferBinCodeSList.filter(function(x) {
        return x.value == val;
      })[0].label;
    },
    getBusinessCodeSelectList() {
      let url = "/Business/GetSelectViewModelList";
      let data = {};
      this.$ajax(url, data, { method: "get" }).then(response => {
        this.businessCodeSList = response.data;
      });
    },
    getBusinessCodeSelectChangeList(val) {
      this.temp.businessName = this.businessCodeSList.filter(function(x) {
        return x.value == val;
      })[0].label;
    },
    getExpressCompanyCodeSelectList() {
      let url = "/ExpressCompany/GetSelectViewModelList";
      let data = {};
      this.$ajax(url, data, { method: "get" }).then(response => {
        this.expressCompanyCodeSList = response.data;
      });
    },
    //获取订单详情 row expand
    getPurchaseOrderInfoViewModelList(row, expandedRows) {
      // if (row.purchaseOrderInfoViewModels == null) {
      //   fetchPurchaseOrderInfoList(row).then(response => {
      //     row.purchaseOrderInfoViewModels = response.data;
      //   });
      // }
    },
    //新增
    handleAddPurchaseOrderInfo(row) {
      //console.log(row);
      let pPOrderNum = myAction.newOrderNumber();
      var purchaseOrderInfoState = "SS01";
      this.temp.purchaseOrderInfoViewModels.push({
        productCode: "",
        productName: "",
        simpleCode: "",
        pOrderNum: this.temp.pOrderNum,
        ppOrderNum: pPOrderNum,
        expressCompanyCode: row.expressCompanyCode,
        expressNumber: row.expressCompanyCode,
        expressFreightAmount: row.expressFreightAmount,
        internationExpressCompanyCode: row.internationExpressCompanyCode,
        internationNumber: row.internationNumber,
        internationFreightAmount: row.internationFreightAmount,
        domesticExpressCompanyCode: row.domesticExpressCompanyCode,
        domesticNumber: row.domesticNumber,
        domesticFreightAmount: row.domesticFreightAmount,
        pProductCode: "",
        purchaseCount: 0,
        purchasePrice: 0,
        purchaseAmount: 0,
        amount: 0,
        purchaseSettlementAmount: 0,
        purchaseOrderInfoState: purchaseOrderInfoState,
        id: 0,
        createTime: "",
        createUserCode: "",
        updateTime: "",
        updateUserCode: "",
        remark: ""
      });
    },
    //删除
    handleDeletePurchaseOrderInfo(row, index) {
      this.temp.purchaseOrderInfoViewModels.splice(index, 1);
    },
    handleLock(row) {
      //锁定数据
      var title = '<span style="color: red;">是否要锁定这条数据?</span>';
      this.$confirm(title, "提示", {
        dangerouslyUseHTMLString: true,
        type: "warning",
        confirmButtonText: "确定",
        cancelButtonText: "取消"
      }).then(() => {
        let orderNumber = row.pOrderNum;
        let url = "/PurchaseOrder/GetPurchaseOrderInfoDoSign";
        let data = { orderNumber: orderNumber };
        this.$ajax(url, data, { method: "get" }).then(response => {
          if (response.resultSign == 1) {
            this.$confirm(response.message, "提示", {
              dangerouslyUseHTMLString: true,
              type: "warning",
              confirmButtonText: "确定",
              cancelButtonText: "取消"
            }).then(() => {
              let orderNumber = row.pOrderNum;
              let url = "/PurchaseOrder/GetPurchaseOrderInfoLock";
              let data = { orderNumber: orderNumber };
              this.$ajax(url, data, { method: "get" }).then(response => {
                if (response.resultSign == 0) {
                  row.purchaseOrderInfoState = "FF02"; //锁定
                }
                myAction.getNotifyFunc(response, this);
              });
            });
          } else {
            let orderNumber = row.pOrderNum;
            let url = "/PurchaseOrder/GetPurchaseOrderInfoLock";
            let data = { orderNumber: orderNumber };
            this.$ajax(url, data, { method: "get" }).then(response => {
              if (response.resultSign == 0) {
                row.purchaseOrderInfoState = "FF02"; //锁定
              }
              myAction.getNotifyFunc(response, this);
            });
          }
        });
      });
    },
    //小计
    sumPurchaseAmount(row) {
      let purchaseAmount = row.purchaseCount * row.purchasePrice;
      row.purchaseAmount = purchaseAmount.toFixed(2);
    },
    //小计+运费
    sumAmount(row) {
      let settlementAmount = parseFloat(row.purchaseSettlementAmount);
      let allAmount = 0;
      if (settlementAmount > 0) {
        allAmount = settlementAmount;
        row.purchaseCount = 0;
      } else {
        allAmount =
          parseFloat(row.purchaseAmount) +
          parseFloat(row.internationFreightAmount) +
          parseFloat(row.domesticFreightAmount) +
          parseFloat(row.expressFreightAmount);
      }
      row.amount = allAmount.toFixed(2);
    },
    //合计
    getSummaries(param) {
      const { columns, data } = param;
      const sums = [];
      columns.forEach((column, index) => {
        if (index === 0) {
          sums[index] = "合计";
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
          sums[index] += "";
        } else {
          sums[index] = "";
        }
      });
      // console.log(sums)
      return sums;
    },
    //合计上面的字段 赋值
    setHJValue(prop, val) {
      switch (prop) {
        case "purchaseAmount":
          this.temp.allPurchaseAmount = val;
          break;
        case "internationFreightAmount":
          this.temp.allInternationFreightAmount = val;
          break;
        case "domesticFreightAmount":
          this.temp.allDomesticFreightAmount = val;
          break;
        case "purchaseSettlementAmount":
          this.temp.allPurchaseSettlementAmount = val;
          break;
        case "amount":
          this.temp.allAmount = val;
          break;
        default:
          break;
      }
    },
    tableRowClassName({ row, rowIndex }) {
     
      if (rowIndex === 1) {
        return "warning-row";
      } else if (rowIndex === 3) {
        return "success-row";
      }
      return "success-row";
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
      let pPOrderNum = myAction.newOrderNumber();
      let purchaseOrderState = "FF01";
      let purchaseOrderInfoState = "SS01";
      this.temp = {
        purchaseOrderInfoViewModels: [
          {
            productCode: "",
            productName: "",
            simpleCode: "",
            pOrderNum: this.temp.pOrderNum,
            ppOrderNum: pPOrderNum,
            expressCompanyCode: "",
            expressNumber: "",
            expressFreightAmount: 0,
            internationExpressCompanyCode: "",
            internationNumber: "",
            internationFreightAmount: 0,
            domesticExpressCompanyCode: "",
            domesticNumber: "",
            domesticFreightAmount: 0,
            pProductCode: "",
            purchaseCount: 0,
            purchasePrice: 0,
            purchaseAmount: 0,
            amount: 0,
            purchaseSettlementAmount: 0,
            purchaseOrderInfoState: purchaseOrderInfoState,
            id: 0,
            createTime: "",
            createUserCode: "",
            updateTime: "",
            updateUserCode: "",
            remark: ""
          }
        ],
        pOrderNum: "",
        businessCode: "",
        pOrderTitle: "",
        pOrderCreateTime: "",
        usaNumber: "",
        transferBinCode: "",
        allPurchaseAmount: 0,
        allInternationFreightAmount: 0,
        allDomesticFreightAmount: 0,
        allPurchaseSettlementAmount: 0,
        allAmount: 0,
        id: 0,
        createTime: "",
        createUserCode: "",
        purchaseOrderState: purchaseOrderState,
        updateTime: "",
        updateUserCode: "",
        remark: ""
      };
    },
    //新增 页面
    handleCreate() {
      this.resetTemp();
      this.dialogStatus = "create";
      this.dialogTitle = "新增进货单";
      this.dialogFormVisible = true;
      this.$nextTick(() => {
        this.$refs["dataForm"].clearValidate();
      });
    },
    //提交数据
    createData() {
      this.$refs["dataForm"].validate(valid => {
        if (valid) {
          var url = "/PurchaseOrder/SaveData";
          var data = this.temp;
          this.$ajax(url, data).then(response => {
            this.list.unshift(this.temp);
            this.total++;
            this.dialogFormVisible = false;
            myAction.getNotifyFunc(response, this);
          });
        }
      });
    },
    //修改 页面
    handleUpdate(row) {
      if (row.purchaseOrderInfoViewModels == null) {
        fetchPurchaseOrderInfoList(row).then(response => {
          row.purchaseOrderInfoViewModels = response.data;
          this.temp = Object.assign({}, row); // copy obj
          this.businessCodeSList = [
            { label: this.temp.businessName, value: this.temp.businessCode }
          ];
          this.transferBinCodeSList = [
            {
              label: this.temp.transferBinName,
              value: this.temp.transferBinCode
            }
          ];
        });
      } else {
        this.temp = Object.assign({}, row); // copy obj
        this.businessCodeSList = [
          { label: this.temp.businessName, value: this.temp.businessCode }
        ];
        this.transferBinCodeSList = [
          { label: this.temp.transferBinName, value: this.temp.transferBinCode }
        ];
      }
      this.dialogStatus = "update";
      this.dialogTitle = "修改进货单";
      this.dialogFormVisible = true;
      if (this.pProductCodeSListDetail.length == 0)
        this.getSelectProductDetial("");

      this.$nextTick(() => {
        this.$refs["dataForm"].clearValidate();
      });
    },
    //提交修改数据
    updateData() {
      this.$refs["dataForm"].validate(valid => {
        if (valid) {
          const tempData = Object.assign({}, this.temp);
          var url = "/PurchaseOrder/SaveData";
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
          let url = "/PurchaseOrder/GetDel";
          let data = { orderNumber: row.pOrderNum };
          this.$ajax(url, data, { method: "get" }).then(response => {
            if (response.resultSign == 0) {
              this.list.splice(index, 1);
              this.total--;
            }
            myAction.getNotifyFunc(response, this);
          });
        })
        .catch(action => {});
    },
    //下载excel
    handleDownload() {
      this.downloadLoading = true;
      var url = "/PurchaseOrder/GetPurchaseListViewModelExportExcel";
      var data = {};
      if (this.multipleSelection.length > 0) {
        let value = this.multipleSelection
          .map(function(x) {
            return x.pOrderNum;
          })
          .join(",");
        var excelModel = {
          pOrderNum: {
            field: "POrderNum",
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
    },
    queryLogisticsInfo(code, num) {
      this.dialogLogisticsVisible = true;
      this.logisticsUrl =
        "https://www.kuaidi100.com/chaxun?com=" + code + "&nu=" + num + "";
    }
  },
  computed: {}
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

.el-table .el-input__inner {
  padding: 0 5px !important;
}
.el-table .cell,
.el-table th div {
  padding: 0 5px !important;
}
.el-dialog__body {
  padding-top: 0px !important;
}

.input-with-select .el-input-group__prepend {
  background-color: #fff;
}

 .el-table .warning-row {
    background: oldlace;
  }

  .el-table .success-row {
    background: #f0f9eb;
  }
</style>


