<template>
  <sp-list
    ref="list"
    :controllerName="controllerName"
    :operations="operations"
    :columns="columns"
    :editComponent="editComponent"
    :allow-select="isNormal"
    :header-click="openLink"
    v-bind="$attrs"
    v-on="$listeners"
  ></sp-list>
</template>

<script>
import recommandBlogEdit from './recommandBlogEdit';

export default {
  name: 'recommandBlogList',
  data() {
    return {
      controllerName: 'RecommandBlog',
      editComponent: recommandBlogEdit
    };
  },
  computed: {
    operations() {
      return !this.isReadonly ? ['new', 'delete'] : [];
    },
    columns() {
      const editColumns = [
        { prop: 'name', label: '名称', width: 300 },
        { prop: 'url', label: '地址', width: 800 },
        { prop: 'createdByName', label: '创建人' },
        { prop: 'createdOn', label: '创建日期', type: 'datetime' }
      ];
      const readonlyColumns = [{ prop: 'name', label: '最新推荐' }];
      return !this.isReadonly ? editColumns : readonlyColumns;
    },
    isReadonly() {
      return this.$attrs.type === 'readonly';
    }
  },
  methods: {
    openLink(row) {
      window.open(row.url);
    }
  }
};
</script>

<style></style>
