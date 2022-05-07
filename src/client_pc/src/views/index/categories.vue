<template>
  <div>
    <a-row :gutter="[16,16]">
      <a-col :span="12" v-for="(item, index) in data" :key="index">
        <sp-card :title="item.category_name" :loading="loading" :empty="item.data.length === 0">
          <a-list :data-source="item.data" :pagination="pagination">
            <a-list-item slot="renderItem" slot-scope="post">
              <a @click="openPost(post.id)">{{ post.title }}</a>
            </a-list-item>
          </a-list>
        </sp-card>
      </a-col>
    </a-row>
  </div>
</template>

<script>
export default {
  name: 'categories',
  data() {
    return {
      count: 0,
      data: [],
      pagination: {
        pageSize: 5,
      },
      loading: true
    }
  },
  created() {
    sp.get('/api/blog/categories').then(resp => {
      this.count = resp.count;
      this.data = resp.data;
      this.loading = false;
    });
  },
  methods: {
    openPost(id) {
      this.$router.push({
        name: 'post',
        params: { id: id }
      });
    }
  }
}
</script>

<style lang="less" scoped>
/deep/ .ant-list-item {
  padding: 0;
  border-bottom: none;
}
</style>