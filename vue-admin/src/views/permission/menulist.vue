<template>
  <div class="app-container">
    <div class="filter-container">
      <el-collapse accordion v-model="activeName">
        <el-collapse-item title="查询条件" name="1">
          <el-form :inline="true" :model="filterModel">
            <el-form-item label="菜单名称">
              <el-input
                v-model="filterModel.menuName.value"
                placeholder="菜单名称"
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

      <el-button class="filter-item" type="primary" icon="el-icon-edit" @click="handleCreate">新增</el-button>
    </el-row>
    <el-table
      :key="tableKey"
      row-key="id"
      v-loading="listLoading"
      :data="list"
      border
      fit
      highlight-current-row
      style="width: 100%;"
      @sort-change="sortChange"
      default-expand-all
      :tree-props="{children: 'children', hasChildren: 'hasChildren'}"
    >
      <el-table-column label="菜单名称" prop="menuName" sortable="custom" width="180">
        <template slot-scope="{row}">
          <span>{{ row.menuName }}</span>
        </template>
      </el-table-column>
      <el-table-column label="菜单编码" prop="menuCode" sortable="custom" width="180">
        <template slot-scope="{row}">
          <span>{{ row.menuCode }}</span>
        </template>
      </el-table-column>
      <el-table-column label="路径地址" prop="path" sortable="custom" width="180">
        <template slot-scope="{row}">
          <span>{{ row.path }}</span>
        </template>
      </el-table-column>
      <el-table-column label="菜单icon" prop="icon" sortable="custom" width="180">
        <template slot-scope="{row}">
          <span>
            <svg-icon :icon-class="row.icon" />
          </span>
        </template>
      </el-table-column>
      <el-table-column label="排序" prop="sort" sortable="custom" width="180">
        <template slot-scope="{row}">
          <span>{{ row.sort }}</span>
        </template>
      </el-table-column>
      <el-table-column label="添加时间" prop="createTime" sortable="custom" width="180px">
        <template slot-scope="{row}">
          <span>{{ row.createTime|formatTime}}</span>
        </template>
      </el-table-column>

      <el-table-column label="备注信息" min-width="100px">
        <template slot-scope="{row}">
          <span>{{ row.remark }}</span>
        </template>
      </el-table-column>

      <el-table-column label="操作" width="250" class-name="small-padding fixed-width">
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

    <el-dialog v-el-drag-dialog :title="dialogTitle" :visible.sync="dialogFormVisible" top="10">
      <el-form ref="dataForm" :model="temp" label-position="right" label-width="100px">
        <el-row>
          <el-col :span="24">
            <el-form-item label="菜单编码" prop="menuCode" :rules="rules.checkNull">
              <el-input v-model="temp.menuCode" />
            </el-form-item>
          </el-col>
        </el-row>

        <el-row>
          <el-col :span="24">
            <el-form-item label="菜单名称" prop="menuName" :rules="rules.checkNull">
              <el-input v-model="temp.menuName" />
            </el-form-item>
          </el-col>
        </el-row>

        <el-row>
          <el-col :span="24">
            <el-form-item label="父节点" prop="parentId" :rules="rules.checkNull">
              <el-tree
                :data="treeData"
                show-checkbox
                default-expand-all
                node-key="id"
                ref="menuTree"
                :props="defaultProps"
                @check-change="checkChange"
                :check-strictly="true"
              ></el-tree>
            </el-form-item>
          </el-col>
        </el-row>

        <el-row>
          <el-col :span="24">
            <el-form-item label="路径" prop="path" :rules="rules.checkNull">
              <el-input v-model="temp.path" />
            </el-form-item>
          </el-col>
        </el-row>

        <el-row>
          <el-col :span="24">
            <el-form-item id="icon-input" label="icon图标" prop="icon" :rules="rules.checkNull">
              <el-input v-model="temp.icon" readonly @focus="openIcon" />
              <svg-icon :icon-class="temp.icon" class="fs40" style="vertical-align: -0.35em;" />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="24">
            <el-form-item label="是否掩藏" prop="hidden" :rules="rules.checkNull">
              <el-switch v-model="temp.hidden"></el-switch>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="24">
            <el-form-item label="排序" prop="sort" :rules="rules.checkNull">
              <el-input v-model="temp.sort" />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="24">
            <el-form-item label="是否子节点" prop="noChildren" :rules="rules.checkNull">
              <el-switch v-model="temp.noChildren"></el-switch>
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

    <el-dialog v-el-drag-dialog title="icon图标" :visible.sync="dialogIconVisible">
      <el-row id="icon-el-row">
        <el-col :span="2">
          <svg-icon icon-class="404" class="fs40" @click="iconClick('404')" />
        </el-col>

        <el-col :span="2">
          <svg-icon icon-class="bug" class="fs40" @click="iconClick('bug')" />
        </el-col>

        <el-col :span="2">
          <svg-icon icon-class="chart" class="fs40" @click="iconClick('chart')" />
        </el-col>

        <el-col :span="2">
          <svg-icon icon-class="clipboard" class="fs40" @click="iconClick('clipboard')" />
        </el-col>

        <el-col :span="2">
          <svg-icon icon-class="component" class="fs40" @click="iconClick('component')" />
        </el-col>

        <el-col :span="2">
          <svg-icon icon-class="dashboard" class="fs40" @click="iconClick('dashboard')" />
        </el-col>

        <el-col :span="2">
          <svg-icon icon-class="documentation" class="fs40" @click="iconClick('documentation')" />
        </el-col>

        <el-col :span="2">
          <svg-icon icon-class="drag" class="fs40" @click="iconClick('drag')" />
        </el-col>

        <el-col :span="2">
          <svg-icon icon-class="edit" class="fs40" @click="iconClick('edit')" />
        </el-col>

        <el-col :span="2">
          <svg-icon icon-class="education" class="fs40" @click="iconClick('education')" />
        </el-col>

        <el-col :span="2">
          <svg-icon icon-class="email" class="fs40" @click="iconClick('email')" />
        </el-col>
        <el-col :span="2">
          <svg-icon icon-class="example" class="fs40" @click="iconClick('example')" />
        </el-col>

        <el-col :span="2">
          <svg-icon icon-class="excel" class="fs40" @click="iconClick('excel')" />
        </el-col>

        <el-col :span="2">
          <svg-icon
            icon-class="exit-fullscreen"
            class="fs40"
            @click="iconClick('exit-fullscreen')"
          />
        </el-col>

        <el-col :span="2">
          <svg-icon icon-class="eye-open" class="fs40" @click="iconClick('eye-open')" />
        </el-col>

        <el-col :span="2">
          <svg-icon icon-class="eye" class="fs40" @click="iconClick('eye')" />
        </el-col>
        <el-col :span="2">
          <svg-icon icon-class="form" class="fs40" @click="iconClick('form')" />
        </el-col>
        <el-col :span="2">
          <svg-icon icon-class="fullscreen" class="fs40" @click="iconClick('fullscreen')" />
        </el-col>
        <el-col :span="2">
          <svg-icon icon-class="guide" class="fs40" @click="iconClick('guide')" />
        </el-col>
        <el-col :span="2">
          <svg-icon icon-class="icon" class="fs40" @click="iconClick('icon')" />
        </el-col>
        <el-col :span="2">
          <svg-icon icon-class="international" class="fs40" @click="iconClick('international')" />
        </el-col>
        <el-col :span="2">
          <svg-icon icon-class="language" class="fs40" @click="iconClick('language')" />
        </el-col>

        <el-col :span="2">
          <svg-icon icon-class="link" class="fs40" @click="iconClick('link')" />
        </el-col>
        <el-col :span="2">
          <svg-icon icon-class="list" class="fs40" @click="iconClick('list')" />
        </el-col>
        <el-col :span="2">
          <svg-icon icon-class="lock" class="fs40" @click="iconClick('lock')" />
        </el-col>
        <el-col :span="2">
          <svg-icon icon-class="message" class="fs40" @click="iconClick('message')" />
        </el-col>
        <el-col :span="2">
          <svg-icon icon-class="mobile" class="fs40" @click="iconClick('mobile')" />
        </el-col>
        <el-col :span="2">
          <svg-icon icon-class="money" class="fs40" @click="iconClick('money')" />
        </el-col>
        <el-col :span="2">
          <svg-icon icon-class="nested" class="fs40" @click="iconClick('nested')" />
        </el-col>
        <el-col :span="2">
          <svg-icon icon-class="password" class="fs40" @click="iconClick('password')" />
        </el-col>
        <el-col :span="2">
          <svg-icon icon-class="pdf" class="fs40" @click="iconClick('pdf')" />
        </el-col>
        <el-col :span="2">
          <svg-icon icon-class="people" class="fs40" @click="iconClick('people')" />
        </el-col>
        <el-col :span="2">
          <svg-icon icon-class="peoples" class="fs40" @click="iconClick('peoples')" />
        </el-col>
        <el-col :span="2">
          <svg-icon icon-class="qq" class="fs40" @click="iconClick('qq')" />
        </el-col>
        <el-col :span="2">
          <svg-icon icon-class="search" class="fs40" @click="iconClick('search')" />
        </el-col>
        <el-col :span="2">
          <svg-icon icon-class="shopping" class="fs40" @click="iconClick('shopping')" />
        </el-col>
        <el-col :span="2">
          <svg-icon icon-class="size" class="fs40" @click="iconClick('size')" />
        </el-col>
        <el-col :span="2">
          <svg-icon icon-class="skill" class="fs40" @click="iconClick('skill')" />
        </el-col>
        <el-col :span="2">
          <svg-icon icon-class="star" class="fs40" @click="iconClick('star')" />
        </el-col>
        <el-col :span="2">
          <svg-icon icon-class="tab" class="fs40" @click="iconClick('tab')" />
        </el-col>
        <el-col :span="2">
          <svg-icon icon-class="table" class="fs40" @click="iconClick('table')" />
        </el-col>
        <el-col :span="2">
          <svg-icon icon-class="theme" class="fs40" @click="iconClick('theme')" />
        </el-col>
        <el-col :span="2">
          <svg-icon icon-class="tree-table" class="fs40" @click="iconClick('tree-table')" />
        </el-col>
        <el-col :span="2">
          <svg-icon icon-class="tree" class="fs40" @click="iconClick('tree')" />
        </el-col>
        <el-col :span="2">
          <svg-icon icon-class="user" class="fs40" @click="iconClick('user')" />
        </el-col>
        <el-col :span="2">
          <svg-icon icon-class="wechat" class="fs40" @click="iconClick('wechat')" />
        </el-col>
        <el-col :span="2">
          <svg-icon icon-class="zip" class="fs40" @click="iconClick('zip')" />
        </el-col>
      </el-row>

      <div slot="footer" class="dialog-footer">
        <el-button type="primary" @click="dialogIconVisible = false">关闭</el-button>
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
    menuName: {
      field: "MenuName",
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
    menuCode: "",
    menuName: "",
    path: "",
    icon: "example",
    hidden: false,
    parentId: 0,
    id: 0,
    createTime: "",
    createUserCode: "",
    updateTime: "",
    updateUserCode: "",
    remark: "",
    merchantId: "",
    sort: 0,
    noChildren: false
  },
  orderArr: []
};
var data = $.extend(false, myAction.setBaseVueData, currentData);
export default {
  name: "menulist",
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
    data.treeData = [];
    data.defaultProps = {
      children: "children",
      label: "label"
    };
    data.leafCheckArr = [];
    data.oldCheckKey = "";
    data.dialogIconVisible = false;
    return data;
  },
  created() {
    this.getList();
  },
  methods: {
    getList() {
      this.listLoading = true;
      var url = "/Menu/GetMenuPageList";
      var data = myAction.getQueryModel(
        this.listQuery.limit,
        this.listQuery.page,
        this.total,
        this.filterModel,
        this.orderArr
      );
      //var data = myAction.getItemsModel(this.filterModel);
      this.$ajax(url, data).then(response => {
        this.list = response.data[0].children;
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
      // let menuCode = myAction.newGuid();
      this.temp = {
        menuCode: "",
        menuName: "",
        path: "",
        icon: "example",
        hidden: false,
        parentId: 0,
        id: 0,
        createTime: "",
        createUserCode: "",
        updateTime: "",
        updateUserCode: "",
        remark: "",
        merchantId: "",
        sort: 0,
        noChildren: false
      };
    },
    //新增 页面
    handleCreate() {
      this.getMenuTreeList();
      this.resetTemp();
      this.dialogStatus = "create";
      this.dialogTitle = "新增菜单";
      this.dialogFormVisible = true;
      this.$nextTick(() => {
        this.$refs["dataForm"].clearValidate();
      });
    },
    //提交数据
    createData() {
      this.$refs["dataForm"].validate(valid => {
        if (valid) {
          var url = "/Menu/_SaveData";
          var data = this.temp;
          this.$ajax(url, data).then(response => {
            this.dialogFormVisible = false;
            myAction.getNotifyFunc(response, this);
            this.getList();
          });
        }
      });
    },
    //修改 页面
    handleUpdate(row) {
      this.getMenuTreeList();
      this.temp = Object.assign({}, row); // copy obj
      this.dialogStatus = "update";
      this.dialogTitle = "修改菜单";
      this.dialogFormVisible = true;
      this.$nextTick(() => {
        this.$refs.menuTree.setCheckedKeys([row.parentId]);
        this.$refs["dataForm"].clearValidate();
      });
    },
    //提交修改数据
    updateData() {
      this.$refs["dataForm"].validate(valid => {
        if (valid) {
          const tempData = Object.assign({}, this.temp);
          var url = "/Menu/_SaveData";
          var data = tempData;
          this.$ajax(url, data).then(response => {
            var d = response.data;
            // const index = this.list.findIndex(v => v.id === this.temp.id);
            // this.list.splice(index, 1, d);
            this.dialogFormVisible = false;
            myAction.getNotifyFunc(response, this);
             this.getList();
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
          this.$ajax(
            "/Menu/GetIsDeleteFlag",
            { menuId: row.id },
            { method: "get" }
          ).then(d => {
            if (d.resultSign == 1) {
              myAction.getNotifyFunc(d, this);
              return false;
            }
            let url = "/Menu/_DelData";
            let data = { id: row.id };
            this.$ajax(url, data, { method: "get" }).then(response => {
              if (response.resultSign == 0) {
                this.list.splice(index, 1);
                this.total--;
              }
              myAction.getNotifyFunc(response, this);
            });
          });
        })
        .catch(action => {});
    },
    //选中单机事件
    checkChange() {
      this.leafCheckArr = this.$refs.menuTree.getCheckedKeys();

      let arr = [];

      this.leafCheckArr.forEach(item => {
        arr.push(item);
      });

      if (this.leafCheckArr.length > 1) {
        arr = this.leafCheckArr.filter(item => {
          return item != this.oldCheckKey;
        });
      } // this.oldCheckKey就是最后选中的节点的值

      this.oldCheckKey = arr.join("");

      this.$refs.menuTree.setCheckedKeys([]);

      this.$refs.menuTree.setCheckedKeys([this.oldCheckKey]); // 通过 this.$refs.fileTree.getCheckedNodes()[0]['label'] 可以拿到最后选中的节点的label

      this.temp.parentId = this.oldCheckKey;
    },
    getMenuTreeList() {
      var url = "/Menu/GetTreeMenuList";
      let data = { id: 0 };
      this.$ajax(url, data, { method: "get" }).then(res => {
        this.treeData = res.data;
      });
    },
    openIcon() {
      this.dialogIconVisible = true;
    },
    iconClick(icon) {
      this.temp.icon = icon;
      this.dialogIconVisible = false;
    }
  }
};
</script>
<style scoped>
.el-form-item {
  margin-left: 22px;
}
#icon-el-row .el-col {
  margin-top: 30px;
  cursor: pointer;
}
#icon-input .el-input {
  width: 90%;
}
</style>


