<template>
  <sp-list :controller-name="controllerName" :operations="operations" :columns="columns" :edit-component="editComponent"></sp-list>
</template>

<script>
import linkEdit from './linkEdit';

export default {
  name: 'link-list',
  data() {
    return {
      controllerName: 'link',
      editComponent: linkEdit,
      operations: ['new', 'delete'],
      columns: [
        { prop: 'name', label: '名称' },
        { prop: 'link_type', label: '链接类型' },
        { prop: 'link_url', label: '链接地址' }
      ]
    };
  }
};
</script>

<style></style>
