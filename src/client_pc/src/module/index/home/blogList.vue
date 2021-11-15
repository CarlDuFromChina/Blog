<template>
  <div>
    <sp-card class="blog-list">
      <a-list item-layout="vertical" size="large" :data-source="listData">
        <a-list-item slot="renderItem" :key="item.Id" slot-scope="item">
          <template slot="actions">
            <span :key="'eye'">
              <a-icon type="eye" style="margin-right: 8px" />
              {{ item.reading_times }}
            </span>
            <span :key="'like-o'">
              <a-icon type="like-o" style="margin-right: 8px" />
              {{ item.upvote_times }}
            </span>
            <span :key="'message'" v-show="showComment">
              <a-icon type="message" style="margin-right: 8px" />
              {{ item.comment_count || 0 }}
            </span>
          </template>
          <a-list-item-meta :description="item.description">
            <span slot="title" style="font-size: 14px">
              <div>
                <a-tag v-if="item.is_pop" color="blue">置顶</a-tag>
                <a @click="readBlog(item)">{{ item.title }}</a>
              </div>
              <div class="meta-container">
                <span class="meta-container-author">{{ item.createdByName }}</span>
                <span class="meta-container-date">{{ formtDate(item.createdOn) }}</span>
                <a-tag v-for="(tag, index) in JSON.parse(item.tags)" :key="index" :color="colors[index % colors.length]">
                  {{ tag }}
                </a-tag>
              </div>
              <div class="meta-brief">{{ item.brief }}</div>
            </span>
            <img :src="getSurface(item.surface_url)" slot="avatar" style="width: 150px; height: 100px" />
          </a-list-item-meta>
        </a-list-item>
      </a-list>
      <a-spin :spinning="loading" :delay="100" style="width: 100%; padding: 10px 0; text-align: center">
        <span v-if="isLoadedAll && pageIndex > 2">到底了....</span>
      </a-spin>
    </sp-card>
  </div>
</template>

<script>
import { pagination } from '@/mixins';

export default {
  name: 'blog-list',
  mixins: [pagination],
  data() {
    return {
      baseUrl: sp.getServerUrl(),
      listData: [],
      loading: false,
      isLoadedAll: false,
      searchValue: '',
      viewId: '463BE7FE-5435-4841-A365-C9C946C0D655',
      actions: [{ type: 'eye' }, { type: 'like-o' }, { type: 'message' }],
      colors: ['pink', 'red', 'orange', 'green', 'cyan', 'blue', 'purple']
    };
  },
  async created() {
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
    showComment() {
      return this.$store.getters.getShowComment;
    }
  },
  methods: {
    getSurface(url) {
      return sp.getDownloadUrl(url);
    },
    onSearch(value) {
      this.searchValue = value || '';
      this.listData = [];
      this.goFirst();
      this.fetchData();
    },
    formtDate(val) {
      return this.$moment(val).fromNow();
    },
    fetchData() {
      try {
        sp.get(
          `api/blog/GetViewData?orderBy=&pageSize=${this.pageSize}&pageIndex=${this.pageIndex}&searchList=&viewId=${this.viewId}&searchValue=${this.searchValue}`
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
.search {
  padding: 0 50px 24px 0;
}

.blog-list {
  padding: 10px 20px 24px 20px;
  margin-right: 50px;
}

.ant-list-vertical .ant-list-item-meta {
  margin-bottom: 0px;
}

.ant-list-vertical .ant-list-item-meta-title {
  margin-bottom: 0px;
}

.meta-container {
  color: #86909c;
  font-size: 13px;
  &-author {
    color: #4e5969;
  }
  &-date {
    position: relative;
    padding: 0 12px;
    &::before {
      position: absolute;
      top: 50%;
      transform: translateY(-50%);
      display: block;
      width: 1px;
      height: 14px;
      background: #e5e6eb;
      content: ' ';
      left: 6px;
    }
    &::after {
      position: absolute;
      top: 50%;
      transform: translateY(-50%);
      display: block;
      width: 1px;
      height: 14px;
      background: #e5e6eb;
      content: ' ';
      right: 6px;
    }
  }
}

.meta-brief {
  display: -webkit-box;
  -webkit-line-clamp: 2;
  -webkit-box-orient: vertical;
  word-break: break-all;
  color: #86909c;
  overflow: hidden;
  text-overflow: ellipsis;
  font-size: 13px;
  line-height: 22px;
}
</style>
