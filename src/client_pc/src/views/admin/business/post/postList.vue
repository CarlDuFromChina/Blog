<template>
  <sp-list
    ref="list"
    :controller-name="controllerName"
    :operations="operations"
    :columns="columns"
    :edit-component="editComponent"
    :headerClick="goEdit"
    :searchList="searchList"
    :exportData="exportData"
  >
    <a-form-model :model="searchData" slot="more" layout="horizontal" v-bind="{ labelCol: { span: 4 }, wrapperCol: { span: 20 } }" label-align="left">
      <a-row :gutter="24" style="padding: 0 10px">
        <a-col :span="10">
          <a-form-model-item label="起始日期">
            <a-range-picker v-model="searchData.date" />
          </a-form-model-item>
        </a-col>
        <a-col :span="10">
          <a-form-model-item label="博客分类" v-if="!category">
            <a-select mode="multiple" style="width: 100%" placeholder="Please select" v-model="searchData.type">
              <a-select-option v-for="item in blogType" :key="item.Value">
                {{ item.Name }}
              </a-select-option>
            </a-select>
          </a-form-model-item>
        </a-col>
        <a-col :span="4" style="text-align:right">
          <a-button @click="reset">重置</a-button>
          <a-button type="primary" style="margin-left:10px;" @click="search">查询</a-button>
        </a-col>
      </a-row>
    </a-form-model>
  </sp-list>
</template>

<script>
import postEdit from './postEdit';

export default {
  name: 'blog-list',
  data() {
    return {
      controllerName: 'post',
      editComponent: postEdit,
      operations: ['new', 'delete', 'search', 'more', 'export'],
      columns: [
        { prop: 'title', label: '标题' },
        { prop: 'tags', label: '标签', type: 'tag' },
        { prop: 'article_type_name', label: '文章类型' },
        { prop: 'is_series_name', label: '是否系列' },
        { prop: 'is_show_name', label: '是否展示' },
        { prop: 'created_by_name', label: '创建人' },
        { prop: 'updated_at', label: '最后修改日期', type: 'datetime' },
        { prop: 'action', label: '操作', type: 'actions', actions: [{ name: '查看', size: 'small', method: this.goReadonly }] }
      ],
      blogType: [],
      searchData: {
        type: [],
        date: []
      },
      category: ''
    };
  },
  computed: {
    searchList() {
      const searchList = [];
      const { type, date } = this.searchData;
      if (date && date.length === 2) {
        searchList.push({
          Name: 'created_at',
          Value: [new Date(this.$moment(date[0]).format('YYYY-MM-DD')), new Date(this.$moment(date[1]).format('YYYY-MM-DD'))],
          Type: 4
        });
      }
      if (!sp.isNull(type) && type.length > 0) {
        searchList.push({ Name: 'post_type', Value: type, Type: 5 });
      }
      if (!sp.isNullOrEmpty(this.category)) {
        searchList.push({ Name: 'post_type', Value: this.category, Type: 0 });
      }
      return searchList;
    }
  },
  created() {
    if (this.$route.params.category) {
      this.category = this.$route.params.category;
    } else {
      // 获取博客类型选项集
      sp.get('api/category/search').then(resp => {
        this.blogType = resp.DataList.map(item => ({
          Name: item.name,
          Value: item.code
        }));
      });
    }
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
      const { href } = this.$router.resolve({
        name: 'post',
        params: { id: item.id }
      });
      window.open(href, '_blank');
    },
    goEdit(item) {
      this.$router.push({
        name: 'postEdit',
        params: { id: (item || {}).id || '' }
      });
    },
    exportData() {
      const selectionIds = this.$refs.list.selectionIds;
      if (!selectionIds || selectionIds.length === 0) {
        this.$message.warning('请选择一项，再进行导出');
        return;
      }
      if (selectionIds.length > 1) {
        this.$message.warning('最多导出一条记录');
      }

      const id = selectionIds[0];
      sp.get(`api/post/export/markdown/${id}`);
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
