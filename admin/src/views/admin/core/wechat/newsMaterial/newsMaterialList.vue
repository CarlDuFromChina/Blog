<template>
  <sp-list :controller-name="controllerName" :operations="operations" :columns="columns" :edit-component="editComponent"></sp-list>
</template>

<script>
import newsMaterialEdit from './newsMaterialEdit';

export default {
  name: 'materialList',
  data() {
    return {
      controllerName: 'wechat_news',
      editComponent: newsMaterialEdit,
      operations: ['new', 'delete', 'search'],
      columns: [
        { prop: 'name', label: '关键词' },
        { prop: 'created_by_name', label: '创建人' },
        { prop: 'created_at', label: '创建日期', type: 'datetime' }
      ]
    };
  }
};
</script>

<style></style>
