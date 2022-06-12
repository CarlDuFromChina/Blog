<template>
  <div>
    <a-row :gutter="[16,16]">
      <a-col :span="12" v-for="(item, index) in data" :key="index">
        <sp-card :title="item.category_name" :loading="loading" :empty="item.data.length === 0">
          <template slot="title">
            <div class="title-box">
              <sp-icon name="sp-blog-folder" :size="30"></sp-icon>
              <span class="title">{{ item.category_name }}</span>
            </div>
          </template>
          <a-list :data-source="item.data" :pagination="pagination">
            <a-list-item slot="renderItem" slot-scope="post">
              <a @click="openPost(post.id)">{{ post.index }}、{{ post.title }}</a>
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
        size: 'small'
      },
      loading: true
    }
  },
  created() {
    sp.get('/api/post/categories').then(resp => {
      this.count = resp.count;
      this.data = resp.data;
      this.data.forEach(category => {
        category.data.forEach((item, index) => {
          item.index = index + 1;
        });
      });
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
.title-box {
  display: inline-block;
  line-height: 30px;
}
.title {
  font-size: 20px;
  line-height: 30px;
  font-weight: 500;
  padding-left: 8px;
}

/deep/ .ant-list-item {
  padding: 0;
  border-bottom: none !important;
  > a {
    color: #86909c;
  }
}

/deep/ .ant-list {
  padding: 0 24px;
}

/deep/ .ant-spin-container {
  height: 140px;
}

/deep/ .ant-list-pagination {
  margin-top: 0;
}
</style>