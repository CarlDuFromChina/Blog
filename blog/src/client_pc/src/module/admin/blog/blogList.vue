<template>
  <sp-list
    :controller-name="controllerName"
    :operations="operations"
    :columns="columns"
    :edit-component="editComponent"
    :headerClick="goEdit"
  ></sp-list>
</template>

<script>
import blogEdit from './blogEdit';

export default {
  name: 'blog-list',
  data() {
    return {
      controllerName: 'Blog',
      editComponent: blogEdit,
      operations: ['new', 'delete', 'search'],
      columns: [
        { prop: 'title', label: '标题' },
        { prop: 'createdByName', label: '创建人' },
        { prop: 'createdOn', label: '创建日期', type: 'datetime' },
        { prop: 'action', label: '操作', type: 'actions', actions: [{ name: '查看', size: 'small', method: this.goReadonly }] }
      ]
    };
  },
  methods: {
    goReadonly(item) {
      this.$router.push({
        name: 'blogReadonly',
        params: { id: item.Id }
      });
    },
    goEdit(item) {
      this.$router.push({
        name: 'blogEdit',
        params: { id: (item || {}).Id || '' }
      });
    }
  }
};
</script>

<style></style>
