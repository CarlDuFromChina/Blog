<template>
  <sp-list
    :controllerName="controllerName"
    :operations="operations"
    :columns="columns"
    :customApi="customApi"
    :editComponent="editComponent"
  ></sp-list>
</template>

<script>
import editComponent from './fileManageEdit';

export default {
  name: 'fileManage',
  data() {
    return {
      controllerName: 'SysFile',
      operations: ['new', 'delete', 'search'],
      columns: [
        { prop: 'name', label: '名称' },
        { prop: 'content_type', label: '文件类型' },
        { prop: 'createdByName', label: '创建人' },
        { prop: 'createdOn', label: '创建日期', type: 'datetime' }
      ],
      editComponent
    };
  },
  computed: {
    customApi() {
      return `api/${this.controllerName}/GetViewData?pageIndex=$pageIndex&pagesize=$pageSize&orderBy=&searchValue=$searchValue&searchList=&viewId=DD1D72FB-D7DE-49AC-B387-273375E6A7BA`;
    }
  }
};
</script>

<style></style>
