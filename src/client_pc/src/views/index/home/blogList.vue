<template>
  <div>
    <sp-card class="blog-list" :loading="firstLoading">
      <a-list item-layout="vertical" size="large" :data-source="listData">
        <a-list-item slot="renderItem" :key="item.id" slot-scope="item">
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
          <img :src="getSurface(item.surface_url)" slot="extra" class="blog_pic" />
          <a-list-item-meta :description="item.description">
            <span slot="title" style="font-size: 14px">
              <div>
                <a-tag v-if="item.is_pop" color="blue">置顶</a-tag>
                <a @click="readBlog(item)">{{ item.title }}</a>
              </div>
              <div class="meta-container">
                <span class="meta-container-author">{{ item.created_by_name }}</span>
                <span class="meta-container-date">{{ formtDate(item.created_at) }}</span>
                <a-tag v-show="item.article_type_name" :color="getColor(item)">
                  {{ item.article_type_name }}
                </a-tag>
              </div>
              <div class="meta-brief">{{ item.brief }}</div>
            </span>
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
      firstLoading: true,
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
    getColor(item) {
      switch (item.article_type_name) {
        case '原创':
          return this.colors[0];
        case '转载':
          return this.colors[1];
        case '翻译':
          return this.colors[2];
        default:
          return '';
      }
    },
    getSurface(url) {
      var downloadUrl = sp.getDownloadUrl(url);
      if (sp.isNullOrEmpty(downloadUrl)) {
        return require('../../../assets/images/pic_err.png');
      }
      return downloadUrl;
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
          `api/post/search?orderBy=&pageSize=${this.pageSize}&pageIndex=${this.pageIndex}&searchList=&viewId=${this.viewId}&searchValue=${this.searchValue}`
        ).then(resp => {
          this.total = resp.RecordCount;
          this.listData = this.listData.concat(resp.DataList);
          this.isLoadedAll = this.pageSize * this.pageIndex >= this.total;
          this.pageIndex++;
          setTimeout(() => {
            if (this.firstLoading) {
              this.firstLoading = false;
            }
          }, 200);
        });
      } finally {
        this.loading = false;
      }
    },
    readBlog(item) {
      const { href } = this.$router.resolve({
        name: 'post',
        params: { id: item.id }
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

.blog_pic {
  width: 120px;
  height: 80px;
  border-radius: 2px;
}
</style>
