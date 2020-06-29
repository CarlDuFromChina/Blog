<template>
  <div class="blog blog__readonly" v-loading="loading" element-loading-text="拼命加载中" element-loading-spinner="el-icon-loading">
    <div class="blog-header">
      <el-button type="primary" icon="el-icon-back" @click="$router.back()">返回</el-button>
    </div>
    <div class="blog-body" style="background-color:#e9ecef">
      <div class="bodyWrapper">
        <el-container>
          <el-aside width="70%">
            <el-card>
              <div class="bodyWrapper-title">{{ data.title }}</div>
              <div class="bodyWrapper-content">
                <div v-highlight v-html="formatterContent"></div>
              </div>
            </el-card>
          </el-aside>
          <el-aside width="30%" style="margin-left:20px">
            <el-card class="block">
              <div class="block-title">
                关于作者
              </div>
              <div class="block-body">
                <a style="display:flex;">
                  <el-avatar :src="imageUrl" style="margin-right:10px;"></el-avatar>
                  <div>
                    <a>{{ user.name }}</a>
                    <div style="color:#72777b;font-size:12px;padding-top: 5px;">{{ user.introduction }}</div>
                  </div>
                </a>
                <div class="block-content" style="padding-top:20px">
                  <sp-icon name="sp-blog-zan" :size="30" style="padding-right:10px" @click="upvote"></sp-icon>
                  <span>获得点赞</span>
                  <span>{{ data.upvote_times || 0 }}</span>
                </div>
                <div class="block-content">
                  <sp-icon name="sp-blog-view" :size="30" style="padding-right:10px"></sp-icon>
                  <span>文章被阅读</span>
                  <span>{{ data.reading_times || 0 }}</span>
                </div>
              </div>
            </el-card>
          </el-aside>
        </el-container>
      </div>
    </div>
  </div>
</template>

<script>
import marked from 'marked';
import 'mavon-editor/dist/css/index.css';

export default {
  name: 'blogReadonly',
  data() {
    return {
      Id: this.$route.params.id,
      controllerName: 'blog',
      data: {},
      loading: false,
      imageUrl: '',
      formatterContent: '',
      user: {}
    };
  },
  async created() {
    await this.loadData();
    this.user = await sp.get(`api/UserInfo/GetData?id=${this.data.createdBy}`);
    this.imageUrl = sp.getBaseUrl() + this.user.avatarUrl;
    this.recordReadingTimes();
  },
  watch: {
    'data.content': {
      immediate: true,
      handler(newVal) {
        if (!sp.isNullOrEmpty(newVal)) {
          this.formatterContent = marked(newVal, {
            sanitize: true
          });
        }
      }
    }
  },
  methods: {
    async loadData() {
      this.loading = true;
      try {
        this.data = await sp.get(`api/${this.controllerName}/GetData?id=${this.Id}`);
        if (this.loadComplete && typeof this.loadComplete === 'function') {
          this.loadComplete();
        }
      } catch (error) {
        this.$message.error('加载失败');
        this.$router.back();
      } finally {
        setTimeout(() => {
          this.loading = false;
        }, 200);
      }
    },
    recordReadingTimes() {
      sp.get(`/api/Blog/RecordReadingTimes?blogid=${this.Id}`);
    },
    upvote() {
      this.data.upvote_times = (this.data.upvote_times || 0) + 1;
      sp.get(`/api/Blog/Upvote?blogid=${this.Id}`);
    }
  }
};
</script>

<style lang="less" scoped>
.block {
  /deep/ .el-card__body {
    padding: 0px;
  }
  .block-title {
    padding: 10px 20px;
    line-height: 1.6;
    border-bottom: 1px solid hsla(0, 0%, 58.8%, 0.1);
  }
  .block-body {
    padding: 10px 20px;
    .block-content {
      padding-bottom: 5px;
      display: flex;
      align-items: center;
      > span {
        padding-right: 5px;
      }
    }
  }
}
</style>
