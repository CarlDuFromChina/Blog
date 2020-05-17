<template>
  <sp-list
    ref="list"
    :customApi="customApi"
    :controllerName="controllerName"
    :operations="operations"
    :columns="columns"
    :editComponent="editComponent"
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
      controllerName: 'SysParam',
      operations: ['new', 'delete'],
      columns: [
        { prop: 'name', label: '名称' },
        { prop: 'code', label: '编码' },
        { prop: 'createdByName', label: '创建人' },
        { prop: 'createdOn', label: '创建日期', type: 'datetime' },
        { prop: 'modifiedByName', label: '最后修改人' },
        { prop: 'modifiedOn', label: '最后修改日期', type: 'datetime' }
      ],
      editComponent: sysParamEdit
    };
  },
  computed: {
    parentId() {
      if (this.relatedAttr && this.relatedAttr.id) {
        return this.relatedAttr.id;
      } else {
        return '';
      }
    },
    searchList() {
      if (!sp.isNullOrEmpty(this.parentId)) {
        return [
          {
            Name: 'sys_paramGroupid',
            Value: this.parentId
          }
        ];
      }
      return [];
    },
    customApi() {
      const list = this.$refs.list;
      let pageIndex = 1;
      let pageSize = 20;
      if (list) {
        pageIndex = list.pageIndex;
        pageSize = list.pageSize;
      }
      let url = `api/${this.controllerName}/GetDataList?searchList=${JSON.stringify(
        this.searchList
      )}&orderBy=&pageSize=${pageSize}&pageIndex=${pageIndex}`;
      return url;
    }
  }
};
</script>

<style></style>
