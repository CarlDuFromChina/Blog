<template>
  <div class="blog blog__readonly">
    <slot />
    <div class="blog-body" :style="{ 'background-color': backgroudColor }">
      <div class="bodyWrapper">
        <el-container>
          <el-aside width="70%">
            <el-card>
              <div class="bodyWrapper-title">{{ title }}</div>
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
                  <sp-icon name="sp-blog-zan" :size="30" style="padding-right:10px"></sp-icon>
                  <span>获得点赞</span>
                  <span>122</span>
                </div>
                <div class="block-content">
                  <sp-icon name="sp-blog-view" :size="30" style="padding-right:10px"></sp-icon>
                  <span>文章被阅读</span>
                  <span>123</span>
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
  name: 'spMarkdownRead',
  props: {
    title: {
      type: String,
      default: ''
    },
    content: {
      type: String,
      default: ''
    },
    backgroudColor: {
      type: String,
      default: '#e9ecef'
    }
  },
  data() {
    return {
      formatterContent: '',
      imageUrl: '',
      user: {}
    };
  },
  created() {
    sp.get(`api/UserInfo/GetData?id=${sp.getUser()}`).then(resp => {
      this.user = resp;
    });
    this.imageUrl = localStorage.getItem('Avatar');
  },
  watch: {
    content: {
      immediate: true,
      handler(newVal) {
        if (!sp.isNullOrEmpty(newVal)) {
          this.formatterContent = marked(newVal, {
            sanitize: true
          });
        }
      }
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
