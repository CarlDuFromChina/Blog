<template>
  <div>
    <sp-list
      :controllerName="controllerName"
      :columns="columns"
      :headerClick="headerClick"
      :operations="operations"
      :editComponent="editComponent"
      allow-select
    ></sp-list>
    <template v-if="drawer">
      <el-drawer ref="drawer" title="编辑" :visible.sync="drawer" :direction="direction" size="60%" :before-close="handleClose">
        <sys-param-group-edit ref="groupEdit" :related-attr="relatedAttr" isGrid></sys-param-group-edit>
        <sys-param-list style="padding:10px" :related-attr="relatedAttr" :pageSize="10"></sys-param-list>
        <div style="display: flex">
          <el-button @click="cancelForm" style="flex: 1">取 消</el-button>
          <el-button type="primary" @click="$refs.drawer.closeDrawer()" :loading="loading" style="flex: 1">{{
            loading ? '提交中 ...' : '确 定'
          }}</el-button>
        </div>
      </el-drawer>
    </template>
  </div>
</template>

<script>
import sysParamGroupEdit from './sysParamGroupEdit';
import sysParamList from './sysParamList';

export default {
  name: 'sysParamGroupList',
  components: { sysParamGroupEdit, sysParamList },
  data() {
    return {
      controllerName: 'SysParamGroup',
      operations: ['new', 'delete'],
      columns: [
        { prop: 'name', label: '名称' },
        { prop: 'code', label: '编码' },
        { prop: 'createdByName', label: '创建人' },
        { prop: 'createdOn', label: '创建日期', type: 'datetime' },
        { prop: 'modifiedByName', label: '最后修改人' },
        { prop: 'modifiedOn', label: '最后修改日期', type: 'datetime' }
      ],
      editComponent: sysParamGroupEdit,
      direction: 'rtl',
      drawer: false,
      relatedAttr: {},
      loading: false
    };
  },
  methods: {
    saveData() {
      this.$refs.groupEdit.saveData();
    },
    handleClose(done) {
      if (this.loading) {
        return;
      }
      this.$confirm('确定要提交表单吗？')
        .then(() => {
          this.loading = true;
          this.saveData();
          done();
        })
        .catch(() => {})
        .finally(() => {
          // 动画关闭需要一定的时间
          setTimeout(() => {
            this.loading = false;
          }, 400);
        });
    },
    cancelForm() {
      this.drawer = false;
    },
    headerClick(row) {
      this.relatedAttr = {
        id: row.Id,
        name: row.name
      };
      this.drawer = true;
    }
  }
};
</script>

<style></style>
