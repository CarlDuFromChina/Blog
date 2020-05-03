<template>
  <el-table :data="data" style="width: 100%;padding: 0 20px 40px 20px">
    <el-table-column label="名称" prop="name"> </el-table-column>
    <el-table-column label="编码" prop="code"> </el-table-column>
  </el-table>
</template>

<script>
export default {
  props: {
    relatedAttr: {
      type: Object
    }
  },
  name: 'sysParamEdit',
  data() {
    return {
      controllerName: 'SysParam',
      parentId: '',
      data: []
    };
  },
  created() {
    if (this.relatedAttr && this.relatedAttr.id) {
      this.parentId = this.relatedAttr.id;
      this.$nextTick(() => this.loadData());
    }
  },
  computed: {
    searchList() {
      return [
        {
          Name: 'sys_paramGroupId',
          Value: this.parentId
        }
      ];
    }
  },
  methods: {
    loadData() {
      sp.get(`api/${this.controllerName}/GetDataList?searchList=${JSON.stringify(this.searchList)}`).then(resp => {
        this.data = resp;
      });
    }
  }
};
</script>
