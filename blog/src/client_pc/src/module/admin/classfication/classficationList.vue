<template>
  <sp-list ref="list" :controllerName="controllerName" :operations="operations" :columns="columns" :editComponent="editComponent"></sp-list>
</template>

<script>
import editComponent from './classficationEdit';

export default {
  name: 'classfication-list',
  data() {
    return {
      controllerName: 'Classfication',
      editComponent,
      columns: [
        { prop: 'name', label: '名称' },
        { prop: 'is_free', label: '是否付费' },
        { prop: 'is_show', label: '是否前台显示' }
      ],
      operations: ['new', 'delete', 'search']
    };
  }
};
</script>

<style></style>
