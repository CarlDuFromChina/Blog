<template>
  <div :infinite-scroll-disabled="busy" class="card-list">
    <a-row v-if="data && data.length > 0">
      <a-col :span="24" v-for="item in data" :key="item.id">
        <sp-card class="card-item" :loading="loading">
          <div class="card-item-wrapper">
            <img alt="example" :src="imageName == 'surface_url' ? getDownloadUrl(item[imageName]) : item[imageName]" />
            <div class="content">
              <h2 class="title">{{ item.name }}</h2>
              <div>
                <sp-icon name="sp-blog-author" :size="15"></sp-icon><span>{{ item[authorName] }}</span>
              </div>
              <div>
                <sp-icon name="sp-blog-time" :size="15"></sp-icon><span>{{ item.updated_at | moment('YYYY-MM-DD HH:MM') }}</span>
              </div>
              <div class="description">{{ item.description }}</div>
              <div><a-button class="readme" type="primary" size="small" ghost @click="goReadonly(item)">阅读全文</a-button></div>
            </div>
          </div>
        </sp-card>
      </a-col>
    </a-row>
    <a-empty v-else style="padding-top:30%" />
    <a-modal title="博客" v-model="editVisible" @ok="editVisible = false" width="70%">
      <div id="blogRead"></div>
    </a-modal>
  </div>
</template>

<script>
import infiniteScroll from 'vue-infinite-scroll';
import { pagination } from '@/mixins';

export default {
  name: 'spBlogCard',
  directives: { infiniteScroll },
  mixins: [pagination],
  props: {
    getDataApi: {
      type: String,
      default: ''
    },
    newTag: {
      type: Boolean,
      default: false
    },
    imageName: {
      type: String,
      default: 'surface_url'
    },
    authorName: {
      type: String,
      default: 'author'
    },
    handleReadClick: {
      type: Function
    }
  },
  data() {
    return {
      isFirstLoad: true,
      busy: false,
      editVisible: false,
      getDownloadUrl: sp.getDownloadUrl,
      data: [],
      loading: false
    };
  },
  mounted() {
    this.loadData();
  },
  beforeDestroy() {
    this.$bus.$emit('reset');
  },
  methods: {
    isNewBlog(item) {
      return this.$moment().diff(this.$moment(item.created_at), 'day') < 5;
    },
    loadData() {
      if (this.loading) {
        this.$bus.$emit('loading-finish');
        return;
      }
      this.loading = true;

      if (sp.isNullOrEmpty(this.getDataApi)) {
        this.$bus.$emit('loading-finish');
        this.$bus.$emit('loaded-all');
        return;
      }

      if (this.pageSize * this.pageIndex >= this.total && !this.isFirstLoad) {
        this.$bus.$emit('loading-finish');
        this.$bus.$emit('loaded-all');
        this.loading = false;
        return;
      }

      this.busy = true;
      this.$emit('loading');
      if (!this.isFirstLoad) {
        this.pageIndex += 1;
      }
      sp.get(this.getDataApi.replace('$pageSize', this.pageSize).replace('$pageIndex', this.pageIndex))
        .then(resp => {
          this.data = this.data.concat(resp.DataList);
          this.total = resp.RecordCount;
          this.isFirstLoad = false;
          this.busy = false;
        })
        .finally(() => {
          this.loading = false;
          this.$emit('loading-close');
          this.$bus.$emit('loading-finish');
        });
    },
    goReadonly(row) {
      if (this.handleReadClick && typeof this.handleReadClick === 'function') {
        this.handleReadClick(row);
        return;
      }
      this.editVisible = true;
      setTimeout(() => {
        const read = document.getElementById('blogRead');
        read.innerHTML = row.content;
      }, 40);
    }
  }
};
</script>

<style lang="less" scoped>
/deep/.ant-col.ant-col-6 {
  padding: 5px;
}
.ant-card-cover img {
  float: left;
  width: 206px;
  height: 116px;
}

.card-list {
  margin: 0 auto;
  .card-item {
    width: 100%;
    margin: 10px auto;
    &-wrapper {
      height: 150px;
      display: flex;
      img {
        width: 200px;
        height: 100%;
        max-width: 200px;
        max-height: 100%;
      }
      .content {
        position: relative;
        max-height: 200px;
        margin-left: 10px;
        .title {
          font-size: 18px;
          font-weight: 600;
        }
        .description {
          display: -webkit-box;
          -webkit-box-orient: vertical;
          -webkit-line-clamp: 2;
          overflow: hidden;
          text-overflow: ellipsis;
          word-break: break-all;
          word-wrap: break-word;
        }
        .svg-icon {
          padding-right: 10px;
        }
        .readme {
          position: absolute;
          bottom: 0px;
        }
      }
    }
  }
}
</style>
