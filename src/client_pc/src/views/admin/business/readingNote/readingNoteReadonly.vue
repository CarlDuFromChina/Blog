<template>
  <div class="blog blog__readonly">
    <blog-menu></blog-menu>
    <div class="blog-body" style="background-color:#e9ecef">
      <div class="bodyWrapper">
        <a-layout>
          <a-layout-sider width="70%" theme="light">
            <a-card>
              <a-skeleton :loading="loading"> </a-skeleton>
              <img :src="getDownloadUrl(data.big_surface_url)" class="bodyWrapper-background" />
              <div class="bodyWrapper-title">{{ data.name }}</div>
              <div id="content" v-show="!loading"></div>
            </a-card>
            <sp-comment v-show="showComment" :object-id="id" :disabled="data.disable_comment" objectName="reading_note"></sp-comment>
          </a-layout-sider>
          <a-layout-sider width="30%" style="margin-left:20px" theme="light">
            <a-card class="block">
              <div class="block-title">
                关于作者
              </div>
              <div class="block-body">
                <a-skeleton :loading="loading">
                  <a style="display:flex;">
                    <a-avatar :src="imageUrl" style="margin-right:10px;"></a-avatar>
                    <div>
                      <a>{{ user.name }}</a>
                      <div style="color:#72777b;font-size:12px;padding-top: 5px;">{{ user.introduction }}</div>
                    </div>
                  </a>
                  <div id="block-content" style="padding-top:20px">
                    <sp-icon name="sp-blog-zan" :size="30" style="padding-right:10px"></sp-icon>
                    <span>获得点赞</span>
                    <span>{{ data.upvote_times || 0 }}</span>
                  </div>
                  <div class="block-content">
                    <sp-icon name="sp-blog-view" :size="30" style="padding-right:10px"></sp-icon>
                    <span>文章被阅读</span>
                    <span>{{ data.reading_times || 0 }}</span>
                  </div>
                </a-skeleton>
              </div>
            </a-card>
          </a-layout-sider>
        </a-layout>
      </div>
    </div>
  </div>
</template>

<script>
import blogMenu from '../../../index/blogMenu.vue';

export default {
  name: 'readingNoteReadonly',
  components: { blogMenu },
  data() {
    return {
      id: this.$route.params.id,
      controllerName: 'reading_note',
      data: {},
      loading: false,
      imageUrl: '',
      formatterContent: '',
      user: {},
      height: null,
      getDownloadUrl: sp.getDownloadUrl
    };
  },
  async mounted() {
    await this.loadData();
    document.getElementById('content').innerHTML = this.data.content;
    this.user = await sp.get(`api/user_info/${this.data.created_by}`);
    this.imageUrl = `${sp.getServerUrl()}api/sys_file/download?objectId=13c5929e-cfca-406b-979b-d7a102a7ed10`;
  },
  computed: {
    showComment() {
      return this.$store.getters.getShowComment;
    }
  },
  methods: {
    async loadData() {
      this.loading = true;
      try {
        this.data = await sp.get(`api/${this.controllerName}/${this.id}`);
      } finally {
        setTimeout(() => {
          this.loading = false;
        }, 200);
      }
    }
  }
};
</script>

<style lang="less" scoped>
.block {
  width: 300px;
  margin-bottom: 20px;
  /deep/ .ant-card-body {
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

/deep/ .block {
  .content-item {
    color: #000000;
    &:hover {
      color: #007fff;
    }
    & .active {
      color: #007fff;
    }
  }
  a {
    color: #000000;
    &:hover {
      color: #007fff;
    }
  }
}

/deep/ .ant-layout-sider-light {
  background: #e9ecef;
}

.blog {
  height: 100%;
  &.blog__readonly {
    overflow-y: auto;
    overflow-x: hidden;
    .blog-header {
      width: 100%;
      height: 60px;
      display: inline-block;
      line-height: 60px;
      padding-left: 20px;
    }
    .blog-body {
      background-color: #e9ecef;
      color: #212529;
      padding-top: 24px;
      padding-bottom: 40px;
      .bodyWrapper {
        width: 70%;
        min-height: 800px;
        margin: 0 auto;
        .ant-layout {
          background: transparent;
        }
        .bodyWrapper-title {
          font-size: 2.5rem;
          text-align: left;
          font-weight: 600;
          color: #000000d9;
        }
        &-background {
          max-width: 100%;
          max-width: 100%;
          width: 100%;
          height: 100%;
          margin-bottom: 20px;
        }
        .bodyWrapper-content {
          height: 100%;
          min-height: 1000px;
          img {
            max-width: 100%;
          }
        }
      }
    }
  }
}

#content {
  min-height: 600px;
  padding-top: 50px;
}
</style>
