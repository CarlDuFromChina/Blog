<template>
  <sp-list
    ref="list"
    :controllerName="controllerName"
    :operations="operations"
    :columns="columns"
    :editComponent="editComponent"
    :customApi="customApi"
    v-bind="$attrs"
    v-on="$listeners"
  ></sp-list>
</template>

<script>
import sysParamEdit from './sysParamEdit';

export default {
  name: 'sysParamList',
  props: {
    relatedAttr: {
      type: Object
    }
  },
  provide() {
    return {
      parentId: () => this.relatedAttr
    };
  },
  data() {
    return {
      controllerName: 'sys_param',
      operations: ['new', 'delete', 'search'],
      columns: [
        { prop: 'name', label: '名称' },
        { prop: 'code', label: '编码' },
        { prop: 'created_by_name', label: '创建人' },
        { prop: 'created_at', label: '创建日期', type: 'datetime' },
        { prop: 'updated_by_name', label: '最后修改人' },
        { prop: 'updated_at', label: '最后修改日期', type: 'datetime' }
      ],
      editComponent: sysParamEdit
    };
  },
  computed: {
    searchList() {
      return [
        {
          Name: 'sys_paramGroupid',
          Value: (this.relatedAttr || {}).id || ''
        }
      ];
    },
    customApi() {
      return `api/${this.controllerName}/search?searchList=${JSON.stringify(
        this.searchList
      )}&orderBy=&pageSize=$pageSize&pageIndex=$pageIndex&searchValue=$searchValue`;
    }
  }
};
</script>

<style></style>
