<template>
  <sp-list
    ref="list"
    :controller-name="controllerName"
    :operations="operations"
    :columns="columns"
    :edit-component="editComponent"
    :headerClick="goEdit"
    :searchList="searchList"
  >
    <a-form-model :model="searchData" slot="more" layout="horizontal" v-bind="{ labelCol: { span: 4 }, wrapperCol: { span: 20 } }" label-align="left">
      <a-row :gutter="24" style="padding: 0 10px">
        <a-col :span="12">
          <a-form-model-item label="起始日期">
            <a-range-picker v-model="searchData.date" />
          </a-form-model-item>
        </a-col>
        <a-col :span="6">
          <a-form-model-item label="博客分类">
            <a-select mode="multiple" style="width: 100%" placeholder="Please select" v-model="searchData.type">
              <a-select-option v-for="item in blogType" :key="item.Value">
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
import blogEdit from './blogEdit';

export default {
  name: 'blog-list',
  data() {
    return {
      controllerName: 'Blog',
      editComponent: blogEdit,
      operations: ['new', 'delete', 'search', 'more'],
      columns: [
        { prop: 'title', label: '标题' },
        { prop: 'createdByName', label: '创建人' },
        { prop: 'createdOn', label: '创建日期', type: 'datetime' },
        { prop: 'action', label: '操作', type: 'actions', actions: [{ name: '查看', size: 'small', method: this.goReadonly }] }
      ],
      blogType: [],
      searchData: {
        type: [],
        date: []
      }
    };
  },
  computed: {
    searchList() {
      const searchList = [];
      const { type, date } = this.searchData;
      if (date && date.length === 2) {
        searchList.push({
          Name: 'createdOn',
          Value: [new Date(this.$moment(date[0]).format('YYYY-MM-DD')), new Date(this.$moment(date[1]).format('YYYY-MM-DD'))],
          Type: 4
        });
      }
      if (!sp.isNull(type) && type.length > 0) {
        searchList.push({ Name: 'blog_type', Value: type, Type: 5 });
      }
      return searchList;
    }
  },
  created() {
    // 获取博客类型选项集
    sp.get('api/SysParamGroup/GetParams?code=blog_type').then(resp => {
      this.blogType = resp;
    });
  },
  methods: {
    search() {
      this.$refs.list.loadData();
    },
    reset() {
      this.searchData.type = [];
      this.searchData.date = [];
    },
    goReadonly(item) {
      this.$router.push({
        name: 'blogReadonly',
        params: { id: item.Id }
      });
    },
    goEdit(item) {
      this.$router.push({
        name: 'blogEdit',
        params: { id: (item || {}).Id || '' }
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
