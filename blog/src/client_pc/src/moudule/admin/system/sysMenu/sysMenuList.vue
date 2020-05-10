<template>
  <div>
    <sp-header>
      <sp-button-list :buttons="buttons"></sp-button-list>
    </sp-header>
    <el-table
      ref="multipleTable"
      :data="tableData"
      style="width: 100%"
      @selection-change="handleSelectionChange"
      :tree-props="{ children: 'ChildMenus', hasChildren: 'hasChildren' }"
      default-expand-all
      row-key="Id"
    >
      <el-table-column type="selection" width="55"></el-table-column>
      <el-table-column label="菜单名">
        <template slot-scope="scope">
          <a class="compute-span" href="javascript:;" @click.stop.prevent="handleClick(scope.row)">{{ scope.row.name }}</a>
        </template>
      </el-table-column>
      <el-table-column prop="router" label="路由"></el-table-column>
      <el-table-column prop="createdByName" label="创建人"></el-table-column>
      <el-table-column label="创建日期" width="200">
        <template slot-scope="scope">{{ formatDate(scope.row.createdOn) }}</template>
      </el-table-column>
      <el-table-column prop="modifiedByName" label="最后修改人"></el-table-column>
      <el-table-column label="最后修改日期" width="200">
        <template slot-scope="scope">{{ formatDate(scope.row.modifiedOn) }}</template>
      </el-table-column>
      <el-table-column prop="stateCodeName" label="状态"></el-table-column>
    </el-table>
    <el-dialog title="编辑" :visible.sync="editVisible" width="50%">
      <component
        v-if="editVisible"
        :is="editComponent"
        @close="editVisible = false"
        :related-attr="relatedAttr"
        @load-data="$emit('load-data')"
      ></component>
    </el-dialog>
  </div>
</template>

<script>
export default {
  name: 'sysMenuList',
  data() {
    return {
      controllerName: 'SysMenu',
      tableData: [],
      buttons: [
        { icon: 'el-icon-plus', text: '', operate: this.createData },
        { icon: 'el-icon-delete', text: '', operate: this.deleteData }
      ],
      editComponent: () => import('./sysMenuEdit'),
      editVisible: false,
      selections: [],
      relatedAttr: null
    };
  },
  created() {
    this.$on('load-data', this.loadData);
    this.$emit('load-data');
  },
  methods: {
    loadData() {
      sp.get('api/sysmenu/GetDataList').then(resp => {
        this.tableData = resp;
      });
    },
    formatDate(value) {
      if (!sp.isNullOrEmpty(value)) {
        return this.$moment(value).format('YYYY-MM-DD HH:MM');
      }
      return '';
    },
    handleClick(row) {
      this.relatedAttr = {
        id: row.Id
      };
      this.editVisible = true;
    },
    createData() {
      this.relatedAttr = {};
      this.editVisible = true;
    },
    deleteData() {
      if (!this.selections || this.selections.length === 0) {
        this.$message.warning('请选择一项，再进行删除');
        return;
      }
      this.$confirm('此操作将永久删除该菜单, 是否继续?', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      })
        .then(() => {
          const ids = this.selections.map(item => {
            return item.Id;
          });
          sp.post(`api/${this.controllerName}/DeleteData`, ids).then(() => {
            this.$message({
              type: 'success',
              message: '删除成功!'
            });
          });
        })
        .catch(() => {
          this.$message({
            type: 'info',
            message: '已取消删除'
          });
        });
    },
    toggleSelection(rows) {
      if (rows) {
        rows.forEach(row => {
          this.$refs.multipleTable.toggleRowSelection(row);
        });
      } else {
        this.$refs.multipleTable.clearSelection();
      }
    },
    handleSelectionChange(val) {
      this.selections = val;
    }
  }
};
</script>

<style>
.compute-span {
  color: #409eff;
  text-decoration: none;
}
</style>
