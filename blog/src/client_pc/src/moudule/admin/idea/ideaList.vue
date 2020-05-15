<template>
  <sp-list
    ref="list"
    :controllerName="controllerName"
    :operations="operations"
    :columns="columns"
    :editComponent="editComponent"
    :allow-select="!isReadonly"
    v-bind="$attrs"
    v-on="$listeners"
  ></sp-list>
</template>

<script>
import ideaEdit from './ideaEdit';

export default {
  name: 'ideaList',
  data() {
    return {
      controllerName: 'idea',
      editComponent: ideaEdit
    };
  },
  computed: {
    operations() {
      return !this.isReadonly ? ['new', 'delete'] : [];
    },
    columns() {
      const editColumns = [
        { prop: 'content', label: '内容' },
        { prop: 'createdByName', label: '创建人' },
        { prop: 'createdOn', label: '创建日期', type: 'datetime' }
      ];
      const readonlyColumns = [{ prop: 'content', label: '最新想法' }];
      return !this.isReadonly ? editColumns : readonlyColumns;
    },
    isReadonly() {
      return this.$attrs.type === 'readonly';
    }
  }
};
</script>

<style></style>
