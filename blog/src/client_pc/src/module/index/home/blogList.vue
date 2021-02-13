<template>
  <sp-card class="blog-list">
    <a-list item-layout="vertical" size="large" :data-source="listData">
      <a-list-item slot="renderItem" key="item.title" slot-scope="item">
        <template slot="actions">
          <span :key="'eye'">
            <a-icon type="eye" style="margin-right: 8px" />
            {{ item.reading_times }}
          </span>
          <span :key="'like-o'">
            <a-icon type="like-o" style="margin-right: 8px" />
            {{ item.upvote_times }}
          </span>
          <span :key="'message'">
            <a-icon type="message" style="margin-right: 8px" />
            {{ item.message || 0 }}
          </span>
        </template>
        <a-list-item-meta :description="item.description">
          <span slot="title" style="font-size:14px;">
            <a @click="readBlog(item)">{{ item.title }}</a>
            <div>{{ item.createdOn | moment('YYYY-MM-DD HH:mm') }}</div>
          </span>
          <img :src="`${baseUrl}${item.surface_url}`" slot="avatar" style="width:150px;height:100px;" />
        </a-list-item-meta>
      </a-list-item>
    </a-list>
    <a-spin :spinning="loading" :delay="100" style="width:100%;padding: 10px 0;text-align:center;">
      <span v-if="isLoadedAll">到底了....</span>
    </a-spin>
  </sp-card>
</template>

<script>
import { pagination } from 'vue-pc';

export default {
  name: 'blog-list',
  mixins: [pagination],
  data() {
    return {
      baseUrl: sp.getBaseUrl(),
      listData: [],
      loading: false,
      isLoadedAll: false,
      actions: [{ type: 'eye' }, { type: 'like-o' }, { type: 'message' }]
    };
  },
  created() {
    this.fetchData();
    this.$bus.$on('load-more', () => {
      if (this.isLoadedAll) {
        return;
      }
      if (this.loading) {
        return;
      }
      this.loading = true;
      setTimeout(() => {
        this.fetchData();
      }, 500);
    });
  },
  computed: {
    avatar() {
      return `${this.baseUrl}/api/SysFile/Download?objectId=13c5929e-cfca-406b-979b-d7a102a7ed10`;
    }
  },
  methods: {
    fetchData() {
      try {
        sp.get(
          `api/blog/GetDataList?orderBy=createdon desc&pageSize=${this.pageSize}&pageIndex=${this.pageIndex}&searchList=&viewId=463BE7FE-5435-4841-A365-C9C946C0D655`
        ).then(resp => {
          this.total = resp.RecordCount;
          this.listData = this.listData.concat(resp.DataList);
          this.isLoadedAll = this.pageSize * this.pageIndex >= this.total;
          this.pageIndex++;
        });
      } finally {
        this.loading = false;
      }
    },
    readBlog(item) {
      const { href } = this.$router.resolve({
        name: 'blogReadonly',
        params: { id: item.Id }
      });
      window.open(href, '_blank');
    }
  }
};
</script>

<style lang="less" scoped>
.blog-list {
  padding: 10px 20px 24px 20px;
  margin-right: 50px;
}

.ant-list-item-meta-content {
  margin-top: -10px;
}

.ant-list-vertical .ant-list-item-meta {
  margin-bottom: 0px;
}

.ant-list-vertical .ant-list-item-meta-title {
  margin-bottom: 0px;
}
</style>
