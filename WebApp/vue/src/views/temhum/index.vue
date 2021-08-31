<template>
  <div class="app-container">
    <div class="head-container">
      <el-form :inline="true" style="margin:0 20px;">
        <el-form-item label="温度">
          <el-input placeholder="请输入" style="width: 200px;" class="filter-item" v-model="listQuery.FilterTem" @keyup.enter.native="handleFilter" size="small" clearable></el-input>
        </el-form-item>
        <el-form-item label="湿度">
          <el-input placeholder="请输入" style="width: 200px;" class="filter-item" v-model="listQuery.FilterHum" @keyup.enter.native="handleFilter" size="small" clearable></el-input>
        </el-form-item>
        <el-form-item>
          <el-button class="filter-item" size="mini" type="success" icon="el-icon-search" @click="handleFilter">搜索</el-button>
          <el-button class="filter-item" size="mini" type="primary" icon="el-icon-plus" @click="handleCreate">新增</el-button>
        </el-form-item>
      </el-form>
    </div>
    <el-dialog :visible.sync="dialogFormVisible" :close-on-click-modal="false" @close="cancel()"
      :title="formTitle">
      <el-form ref="form" :model="form" :rules="rules" size="medium" label-width="100px">
        <el-form-item label="温度" prop="name">
          <el-input v-model="form.tem" placeholder="请输入温度" clearable :style="{width: '100%'}"></el-input>
        </el-form-item>
        <el-form-item label="湿度" prop="description">
          <el-input v-model="form.hum" placeholder="请输入湿度" :style="{width: '100%'}"></el-input>
        </el-form-item>
        <el-form-item label="备注" prop="price">
          <el-input v-model="form.remark" placeholder="请输入备注" clearable :style="{width: '100%'}"></el-input>
        </el-form-item>
      </el-form>
      <div slot="footer">
        <el-button size="small" type="text" @click="cancel">取消</el-button>
        <el-button size="small" v-loading="formLoading" type="primary" @click="save">确认</el-button>
      </div>
    </el-dialog>
    <el-table ref="multipleTable" v-loading="listLoading" :data="list" size="small" style="width: 100%;"
      @sort-change="sortChange" @selection-change="handleSelectionChange" @row-click="handleRowClick">
      <el-table-column type="index" width="50" align="center"></el-table-column>
      <el-table-column label="温度" prop="tmp" align="center" />
      <el-table-column label="湿度" prop="hum" align="center" />
      <el-table-column label="备注" prop="remark" align="center" />
      <el-table-column label="操作" align="center">
        <template slot-scope="{row}">
          <el-button type="primary" size="mini" @click="handleUpdate(row)" icon="el-icon-edit">编辑</el-button>
          <el-button type="danger" size="mini" @click="handleDelete(row)" icon="el-icon-delete">删除</el-button>
        </template>
      </el-table-column>
    </el-table>
    <div class="page">
      <pagination v-show="totalCount>0" :total="totalCount" :page.sync="page" :limit.sync="listQuery.MaxResultCount" @pagination="getList" />
    </div>
  </div>
</template>
<script>

import Pagination from "@/components/Pagination";
import permission from "@/directive/permission/index.js";
const defaultForm = {
  id: null,
  Tem: undefined,
  Hum: undefined,
  Remark: undefined,
}
export default {
  name: 'TemHum',
  components: {
    Pagination
  },
  directives: {
    permission
  },
  props: [],
  data() {
    return {
      rules: {
        Tem: [],
        Hum: [],
        Remark: [],
      },
      form: Object.assign({}, defaultForm),
      list: null,
      totalCount: 0,
      listLoading: true,
      formLoading: false,
      listQuery: {
        FilterTem: '',
        FilterHum: '',
        Sorting: '',
        SkipCount: 0,
        MaxResultCount: 10
      },
      page: 1,
      dialogFormVisible: false,
      multipleSelection: [],
      formTitle: '',
      isEdit: false,
    }
  },
  computed: {},
  watch: {},
  created() {},
  mounted() {},
  methods: {
    getList() {
      this.listLoading = true;
      this.listQuery.SkipCount = (this.page - 1) * this.listQuery.MaxResultCount;
      this.$axios.gets('/api/business/temhum', this.listQuery).then(response => {
        this.list = response.items;
        this.totalCount = response.totalCount;
        this.listLoading = false;
      });
    },
    fetchData(id) {
      this.$axios.gets('/api/business/temhum/' + id).then(response => {
        this.form = response;
      });
    },
    handleFilter() {
      this.page = 1;
      this.getList();
    },
    handleCreate() {
      this.formTitle = '新增TemHum';
      this.isEdit = false;
      this.dialogFormVisible = true;
    },
    handleDelete(row) {
      var params = [];
      let alert = '';
      if (row) {
        params.push(row.id);
        alert = row.name;
      }
      else {
        if (this.multipleSelection.length === 0) {
          this.$message({
            message: '未选择',
            type: 'warning'
          });
          return;
        }
        this.multipleSelection.forEach(element => {
          let id = element.id;
          params.push(id);
        });
        alert = '选中项';
      }
      this.$confirm('是否删除' + alert + '?', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        this.$axios.posts('Form', params).then(response => {
          this.$notify({
            title: '成功',
            message: '删除成功',
            type: 'success',
            duration: 2000
          });
          this.getList();
        });
      }).catch(() => {
        this.$message({
          type: 'info',
          message: '已取消删除'
        });
      });
    },
    handleUpdate(row) {
      this.formTitle = '修改Form';
      this.isEdit = true;
      if (row) {
        this.fetchData(row.id);
        this.dialogFormVisible = true;
      }
      else {
        if (this.multipleSelection.length != 1) {
          this.$message({
            message: '编辑必须选择单行',
            type: 'warning'
          });
          return;
        }
        else {
          this.fetchData(this.multipleSelection[0].id);
          this.dialogFormVisible = true;
        }
      }
    },
    save() {
      this.$refs.form.validate(valid => {
        if (valid) {
          this.formLoading = true;
          this.form.roleNames = this.checkedRole;
          if (this.isEdit) {
            this.$axios.puts('/api/business/temhum/' + this.form.id, this.form).then(response => {
              this.formLoading = false;
              this.$notify({
                title: '成功',
                message: '更新成功',
                type: 'success',
                duration: 2000
              });
              this.dialogFormVisible = false;
              this.getList();
            }).catch(() => {
              this.formLoading = false;
            });
          }
          else {
            this.$axios.posts('/api/business/temhum', this.form).then(response => {
              this.formLoading = false;
              this.$notify({
                title: '成功',
                message: '新增成功',
                type: 'success',
                duration: 2000
              });
              this.dialogFormVisible = false;
              this.getList();
            }).catch(() => {
              this.formLoading = false;
            });
          }
        }
      });
    },
    sortChange(data) {
      const {
        prop,
        order
      } = data;
      if (!prop || !order) {
        this.handleFilter();
        return;
      }
      this.listQuery.Sorting = prop + ' ' + order;
      this.handleFilter();
    },
    handleSelectionChange(val) {
      this.multipleSelection = val;
    },
    handleRowClick(row, column, event) {
      this.$refs.multipleTable.clearSelection();
      this.$refs.multipleTable.toggleRowSelection(row);
    },
    cancel() {
      this.form = Object.assign({}, defaultForm);
      this.dialogFormVisible = false;
      this.$refs.form.clearValidate();
    },
  }
}
</script>

<style lang="scss" scoped>
.page {
  display: block;
  text-align: right;
  padding: 5px 2px 0 0;
}
</style>
