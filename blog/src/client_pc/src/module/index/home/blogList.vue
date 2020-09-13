<template>
  <div class="blog-list">
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
          <a slot="title" @click="readBlog(item)">{{ item.title }}</a>
          <a-avatar slot="avatar" src="http://www.dumiaoxin.top:8002/temp/1B715131BA7631E818D1713D3E6766E541717022.png" />
        </a-list-item-meta>
      </a-list-item>
    </a-list>
    <a-spin :spinning="loading" :delay="100" style="width:100%;padding: 10px 0;text-align:center;">
      <span v-if="isLoadedAll">到底了....</span>
    </a-spin>
  </div>
</template>

<script>
import { pagination } from 'sixpence.platform.pc.vue';

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
  methods: {
    fetchData() {
      try {
        sp.get(`api/blog/GetDataList?orderBy=createdon desc&pageSize=${this.pageSize}&pageIndex=${this.pageIndex}&searchList=`).then(resp => {
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
  border-bottom: 1px solid #ebedf0;
  padding: 10px 20px 24px 20px;
  color: rgba(0, 0, 0, 0.65);
  background: #ffffff;
  margin-right: 50px;
}
</style>
