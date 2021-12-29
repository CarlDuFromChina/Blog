<template>
  <sp-list :controller-name="controllerName" :operations="operations" :columns="columns" :edit-component="editComponent"></sp-list>
</template>

<script>
import newsMaterialEdit from './newsMaterialEdit';

export default {
  name: 'materialList',
  data() {
    return {
      controllerName: 'WeChatNews',
      editComponent: newsMaterialEdit,
      operations: ['new', 'delete', 'search'],
      columns: [
        { prop: 'name', label: '关键词' },
        { prop: 'createdByName', label: '创建人' },
        { prop: 'createdOn', label: '创建日期', type: 'datetime' }
      ]
    };
  }
};
</script>

<style></style>
