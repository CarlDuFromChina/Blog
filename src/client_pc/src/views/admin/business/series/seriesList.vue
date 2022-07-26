<template>
  <sp-list
    ref="list"
    :controller-name="controllerName"
    :operations="operations"
    :columns="columns"
    :edit-component="editComponent"
    :headerClick="goEdit"
    :customApi="customApi"
  >
    <a-form-model :model="searchData" slot="more" layout="horizontal" v-bind="{ labelCol: { span: 4 }, wrapperCol: { span: 20 } }" label-align="left">
      <a-row :gutter="24" style="padding: 0 10px">
        <a-col :span="12">
          <a-form-model-item label="起始日期">
            <a-range-picker v-model="searchData.date" />
          </a-form-model-item>
        </a-col>
        <a-col :span="12" style="text-align:right">
          <a-button @click="reset">重置</a-button>
          <a-button type="primary" style="margin-left:10px;" @click="search">查询</a-button>
        </a-col>
      </a-row>
    </a-form-model>
  </sp-list>
</template>

<script>
import postEdit from '../post/postEdit';

export default {
  name: 'blog-list',
  data() {
    return {
      controllerName: 'post',
      editComponent: postEdit,
      operations: ['new', 'delete', 'search', 'more'],
      columns: [
        { prop: 'title', label: '标题' },
        { prop: 'tags', label: '标签', type: 'tag' },
        { prop: 'updated_by_name', label: '最后修改人' },
        { prop: 'updated_at', label: '最后修改日期', type: 'datetime' },
        { prop: 'created_by_name', label: '创建人' },
        { prop: 'created_at', label: '创建日期', type: 'datetime' },
        { prop: 'action', label: '操作', type: 'actions', actions: [{ name: '查看', size: 'small', method: this.goReadonly }] }
      ],
      blogType: [],
      searchData: {
        date: []
      }
    };
  },
  computed: {
    searchList() {
      const searchList = [];
      const { date } = this.searchData;
      if (date && date.length === 2) {
        searchList.push({
          Name: 'created_at',
          Value: [new Date(this.$moment(date[0]).format('YYYY-MM-DD')), new Date(this.$moment(date[1]).format('YYYY-MM-DD'))],
          Type: 4
        });
      }
      return searchList;
    },
    customApi() {
      return `api/${this.controllerName}/search?searchList=${JSON.stringify(
        this.searchList
      )}&orderBy=&pageSize=$pageSize&pageIndex=$pageIndex&searchValue=$searchValue&viewId=ACCE50D6-81A5-4240-BD82-126A50764FAB`;
    }
  },
  methods: {
    search() {
      this.$refs.list.loadData();
    },
    reset() {
      this.searchData.date = [];
    },
    goReadonly(item) {
      const { href } = this.$router.resolve({
        name: 'post',
        params: { id: item.id }
      });
      window.open(href, '_blank');
    },
    goEdit(item) {
      this.$router.push({
        name: `postEdit`,
        params: { id: item.id }
      });
    }
  }
};
</script>

<style lang="less" scoped>
.ant-calendar-picker {
  display: inline-block;
  width: 100%;
}
</style>
