<template>
  <sp-list :controller-name="controllerName" :operations="operations" :columns="columns" :edit-component="editComponent"></sp-list>
</template>

<script>
import ideaEdit from './ideaEdit';

export default {
  name: 'ideaList',
  data() {
    return {
      controllerName: 'idea',
      editComponent: ideaEdit,
      operations: ['new', 'delete'],
      columns: [
        { prop: 'name', label: '内容' },
        { prop: 'createdByName', label: '创建人' },
        { prop: 'createdOn', label: '创建日期', type: 'datetime' }
      ]
    };
  }
};
</script>

<style></style>
