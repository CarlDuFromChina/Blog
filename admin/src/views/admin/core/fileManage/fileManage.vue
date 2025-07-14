<template>
  <sp-list
    ref="list"
    :controllerName="controllerName"
    :operations="operations"
    :columns="columns"
    :customApi="customApi"
    :editComponent="editComponent"
    :searchList="searchList"
  >
    <a-form-model :model="searchData" slot="more" layout="horizontal" v-bind="{ labelCol: { span: 4 }, wrapperCol: { span: 20 } }" label-align="left">
      <a-row :gutter="24" style="padding: 0 10px">
        <a-col :span="18">
          <a-form-model-item label="关联实体">
            <a-select mode="multiple" style="width: 100%" placeholder="Please select" v-model="searchData.file_type">
              <a-select-option v-for="item in selectDataList.sys_entity" :key="item.Value">
                {{ item.Name }}
              </a-select-option>
            </a-select>
          </a-form-model-item>
        </a-col>
        <a-col :span="6" style="text-align:right">
          <a-button @click="reset">重置</a-button>
          <a-button type="primary" style="margin-left:10px;" @click="search">查询</a-button>
        </a-col>
      </a-row>
    </a-form-model>
  </sp-list>
</template>

<script>
import editComponent from './fileManageEdit';
import { select } from '@/mixins';

export default {
  name: 'fileManage',
  mixins: [select],
  data() {
    return {
      controllerName: 'sys_file',
      operations: ['delete', 'search', 'more'],
      columns: [
        { prop: 'name', label: '名称' },
        { prop: 'file_type', label: '关联实体' },
        { prop: 'content_type', label: '文件类型' },
        { prop: 'created_by_name', label: '创建人' },
        { prop: 'created_at', label: '创建日期', type: 'datetime' }
      ],
      editComponent,
      searchData: {
        file_type: []
      },
      selectEntityNameList: ['sys_entity']
    };
  },
  computed: {
    searchList() {
      const searchList = [];
      if (!sp.isNullOrEmpty(this.searchData.file_type)) {
        searchList.push({ Name: 'file_type', Value: this.searchData.file_type, Type: 5 });
      }
      return searchList;
    },
    customApi() {
      let url = `api/${this.controllerName}/search?pageIndex=$pageIndex&pagesize=$pageSize`;
      url += `&orderBy=&searchValue=$searchValue&searchList=${JSON.stringify(this.searchList)}&viewId=DD1D72FB-D7DE-49AC-B387-273375E6A7BA`;
      return url;
    }
  },
  methods: {
    reset() {
      this.searchData = { file_type: [] };
    },
    search() {
      this.$refs.list.loadData();
    }
  }
};
</script>

<style></style>
