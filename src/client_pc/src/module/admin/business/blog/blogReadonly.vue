<template>
  <div id="blog" class="blog blog__readonly">
    <div class="blog-header">
      <sp-icon
        name="sp-blog-logo"
        @click="() => this.$router.push({ name: 'home' })"
        size="32"
        style="display: inline-flex; border-radius: 6px; cursor: pointer"
      ></sp-icon>
      <a-menu mode="horizontal">
        <a-menu-item key="index" @click="() => this.$router.push({ name: 'home' })"> 首页 </a-menu-item>
        <a-menu-item key="friend" @click="() => this.$router.push({ name: 'friends' })"> 友人帐 </a-menu-item>
        <a-menu-item key="note" @click="() => this.$router.push({ name: 'readingNote' })"> 读书笔记 </a-menu-item>
      </a-menu>
    </div>
    <div class="blog-body" style="background-color: #e9ecef">
      <div class="bodyWrapper">
        <a-layout>
          <a-layout-sider width="70%" theme="light">
            <a-card>
              <a-skeleton :loading="loading">
                <div class="block">
                  <div style="display: flex">
                    <a-avatar :src="`${baseUrl}api/System/GetAvatar?id=${data.createdBy}`" style="margin-right: 10px"></a-avatar>
                    <div>
                      <a>{{ user.name }}</a>
                      <div style="color: #72777b; font-size: 12px; padding-top: 5px">{{ data.createdOn | moment('YYYY-MM-DD HH:mm') }}</div>
                    </div>
                  </div>
                </div>
                <img v-if="data.big_surface_url" :src="baseUrl + data.big_surface_url" class="bodyWrapper-background" />
                <div class="bodyWrapper-title">{{ data.title }}</div>
                <div id="blog_content" class="bodyWrapper-content">
                  <article v-highlight v-html="formatterContent" class="markdown-body"></article>
                </div>
              </a-skeleton>
            </a-card>
            <sp-comments :object-id="Id" :disabled="!!data.disable_comment" objectName="blog"></sp-comments>
          </a-layout-sider>
          <a-layout-sider width="30%" style="margin-left: 20px" theme="light">
            <a-card class="block">
              <div class="block-title">
                <span>关于作者</span>
              </div>
              <div class="block-body">
                <a-skeleton :loading="loading">
                  <a style="display: flex">
                    <a-avatar :src="`${baseUrl}api/System/GetAvatar?id=${data.createdBy}`" style="margin-right: 10px"></a-avatar>
                    <div>
                      <a>{{ user.name }}</a>
                      <div style="color: #72777b; font-size: 12px; padding-top: 5px">{{ user.introduction }}</div>
                    </div>
                  </a>
                  <div id="block-content" style="padding-top: 20px">
                    <sp-icon name="sp-blog-zan" :size="30" style="padding-right: 10px"></sp-icon>
                    <span>获得点赞</span>
                    <span>{{ data.upvote_times || 0 }}</span>
                  </div>
                  <div class="block-content">
                    <sp-icon name="sp-blog-view" :size="30" style="padding-right: 10px"></sp-icon>
                    <span>文章被阅读</span>
                    <span>{{ data.reading_times || 0 }}</span>
                  </div>
                </a-skeleton>
              </div>
            </a-card>
            <a-card class="block recommand">
              <div class="block-title">
                <span>好文推荐</span>
              </div>
              <a class="item" v-for="(item, index) in recommandList" :key="index" @click="read(item)">
                <div class="item-start">{{ item.name }}</div>
              </a>
            </a-card>
            <div id="content" class="block catalog">
              <div style="font-size: 16px">目录</div>
            </div>
          </a-layout-sider>
        </a-layout>
      </div>
    </div>
    <a-back-top :target="getBlogEl" :visibilityHeight="100" />
  </div>
</template>

<script>
import Vue from 'vue';
import 'mavon-editor/src/lib/css/markdown.css';
const marked = require('marked');

const renderer = new marked.Renderer();
renderer.heading = function(text, level, raw) {
  const anchor = tocObj.add(text, level);
  return `<a id=${anchor} class="anchor-fix"></a><h${level}>${text}</h${level}>\n`;
};

marked.setOptions({
  renderer: renderer
});
const tocObj = {
  add: function(text, level) {
    var anchor = `toc${level}${++this.index}`;
    this.toc.push({ anchor: anchor, level: level, text: text });
    return anchor;
  },
  toHTML: function() {
    let levelStack = [];
    let result = '';
    const addStartUL = () => {
      result += '<ul>';
    };
    const addEndUL = () => {
      result += '</ul>\n';
    };
    const addLI = (anchor, text) => {
      result += '<li class="content-item" @click="goAnchor(\'' + anchor + '\')"><a href="javascript:void(0)">' + text + '</a></li>\n';
    };
    this.toc.forEach(function(item) {
      let levelIndex = levelStack.indexOf(item.level);
      // 没有找到相应level的ul标签，则将li放入新增的ul中
      if (levelIndex === -1) {
        levelStack.unshift(item.level);
        addStartUL();
        addLI(item.anchor, item.text);
      } else if (levelIndex === 0) {
        // 找到了相应level的ul标签，并且在栈顶的位置则直接将li放在此ul下
        addLI(item.anchor, item.text);
      } else {
        // 找到了相应level的ul标签，但是不在栈顶位置，需要将之前的所有level出栈并且打上闭合标签，最后新增li
        while (levelIndex--) {
          levelStack.shift();
          addEndUL();
        }
        addLI(item.anchor, item.text);
      }
    });
    // 如果栈中还有level，全部出栈打上闭合标签
    while (levelStack.length) {
      levelStack.shift();
      addEndUL();
    }
    // 清理先前数据供下次使用
    this.toc = [];
    this.index = 0;
    return result;
  },
  toc: [],
  index: 0
};

export default {
  name: 'blogReadonly',
  data() {
    return {
      Id: this.$route.params.id,
      controllerName: 'blog',
      data: {},
      recommandList: [],
      loading: false,
      formatterContent: '',
      user: {},
      height: null,
      baseUrl: sp.getServerUrl()
    };
  },
  async created() {
    await this.loadData();
    if (this.data.title) {
      document.title = this.data.title;
    }
    this.user = await sp.get(`api/UserInfo/GetData?id=${this.data.createdBy}`);
    this.loadRecommand();
  },
  mounted() {
    document.getElementById('blog').addEventListener('scroll', this.handleScroll);
  },
  watch: {
    'data.content': {
      immediate: true,
      handler(newVal) {
        if (!sp.isNullOrEmpty(newVal)) {
          this.formatterContent = marked(newVal);
          const content = document.querySelector('#content');
          const MyComponent = Vue.extend({
            template: tocObj.toHTML(),
            data() {
              return {
                activeList: []
              };
            },
            methods: {
              goAnchor(id) {
                const classList = event.srcElement.classList;
                if (!this.activeList.includes(classList)) {
                  this.activeList.push(classList);
                }
                this.activeList.forEach(item => {
                  item.remove('active');
                });
                classList.add('active');
                const node = document.getElementById(id);
                if (node) {
                  node.scrollIntoView();
                }
              }
            }
          });
          const component = new MyComponent().$mount();
          content.appendChild(component.$el);
          this.height = content.offsetTop;
        }
      }
    }
  },
  methods: {
    getBlogEl() {
      return document.getElementById('blog');
    },
    loadRecommand() {
      sp.get('api/RecommendInfo/GetRecommendList').then(resp => {
        this.recommandList = resp;
      });
    },
    read(item) {
      sp.get(`api/RecommendInfo/RecordReadingTimes?id=${item.Id}`);
      item.reading_times = (item.reading_times || 0) + 1;
      window.open(item.url);
    },
    handleScroll() {
      const content = document.getElementById('content');
      const blog = document.getElementById('blog');
      if (this.height > blog.scrollTop) {
        content.style.position = 'relative';
        content.style.marginTop = 20;
      } else {
        content.style.position = 'sticky';
        content.style.top = '0';
        content.style.marginTop = 0;
      }
    },
    handleLinkClick() {
      const content = document.getElementById('blog_content');
      const nodes = content.getElementsByTagName('a');
      for (const node of nodes) {
        node.target = '_blank';
      }
    },
    async loadData() {
      this.loading = true;
      try {
        this.data = await sp.get(`api/${this.controllerName}/GetData?id=${this.Id}`);
      } finally {
        setTimeout(() => {
          this.loading = false;
          this.$nextTick(() => {
            this.handleLinkClick();
          });
        }, 200);
      }
    },
    upvote() {
      this.data.upvote_times = (this.data.upvote_times || 0) + 1;
      sp.get(`/api/Blog/Upvote?blogid=${this.Id}`);
    }
  }
};
</script>

<style lang="less" scoped>
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
      background: #fff;
    }
    .blog-body {
      background-color: #e9ecef;
      color: #212529;
      padding-top: 24px;
      padding-bottom: 40px;
      .bodyWrapper {
        width: 80%;
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

.block {
  width: 300px;
  margin-bottom: 20px;
  border-radius: 6px;

  .catalog {
    position: sticky;
  }

  /deep/ .ant-card-body {
    padding: 0px;
  }
  .block-title {
    font-size: 14px;
    padding: 10px 12px;
    border-bottom: 2px solid hsla(0, 0%, 58.8%, 0.1);
    display: flex;
    align-items: center;
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
.anchor-fix {
  display: block;
  height: 20px; /*same height as header*/
  margin-top: -20px; /*same height as header*/
  visibility: hidden;
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

/deep/ .blog-header {
  display: flex !important;
  align-items: center;
}

/deep/ .ant-menu {
  font-size: 16px;
  border-bottom: none !important;
}

.recommand {
  /deep/ .ant-card-body {
    padding: 0px;
  }
  .item {
    display: flex;
    padding: 10px;
    cursor: pointer;
    align-items: center;
    justify-content: space-between;
    font-size: 13px;
    &-start {
      color: #000000;
    }
    &-end {
      color: #4a4a4a;
    }
  }
  .item:hover {
    background: hsla(0, 0%, 85.1%, 0.1);
  }
}
</style>
