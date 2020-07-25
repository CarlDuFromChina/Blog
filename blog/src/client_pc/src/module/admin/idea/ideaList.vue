<template>
  <sp-list ref="list" :controllerName="controllerName" :operations="operations" :columns="columns" :editComponent="editComponent"></sp-list>
</template>

<script>
import ideaEdit from './ideaEdit';
import spList from './spList';

export default {
  name: 'ideaList',
  components: { spList },
  data() {
    return {
      controllerName: 'idea',
      editComponent: ideaEdit
    };
  },
  computed: {
    operations() {
      return ['new', 'delete'];
    },
    columns() {
      return [
        { prop: 'content', label: '内容' },
        { prop: 'createdByName', label: '创建人' },
        { prop: 'createdOn', label: '创建日期', type: 'datetime' }
      ];
    }
  }
};
</script>

<style></style>
