<template>
  <div id="blog" class="blog blog__readonly">
    <div class="blog-header"></div>
    <div class="blog-body" style="background-color:#e9ecef">
      <div class="bodyWrapper">
        <a-layout>
          <a-layout-sider width="70%" theme="light">
            <a-card>
              <a-skeleton :loading="loading">
                <div class="bodyWrapper-title">{{ data.title }}</div>
                <div class="bodyWrapper-content">
                  <div v-highlight v-html="formatterContent"></div>
                </div>
              </a-skeleton>
            </a-card>
            <sp-comment :object-id="Id"></sp-comment>
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
                </a-skeleton>
              </div>
            </a-card>
            <div id="content" class="block"></div>
          </a-layout-sider>
        </a-layout>
      </div>
    </div>
  </div>
</template>

<script>
import 'mavon-editor/dist/css/index.css';
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
      loading: false,
      imageUrl: '',
      formatterContent: '',
      user: {},
      height: null
    };
  },
  async created() {
    await this.loadData();
    this.user = await sp.get(`api/UserInfo/GetData?id=${this.data.createdBy}`);
    this.imageUrl = sp.getBaseUrl() + this.user.avatarUrl;
    this.recordReadingTimes();
  },
  mounted() {
    document.getElementById('blog').addEventListener('scroll', this.handleScroll);
  },
  watch: {
    'data.content': {
      immediate: true,
      handler(newVal) {
        if (!sp.isNullOrEmpty(newVal)) {
          this.formatterContent = marked(newVal, {
            sanitize: true
          });
          const content = document.querySelector('#content');
          const Vue = require('vue');
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
    handleScroll() {
      const content = document.getElementById('content');
      const blog = document.getElementById('blog');
      if (this.height > blog.scrollTop) {
        content.style.position = 'relative';
        content.style.marginTop = 20;
      } else {
        content.style.position = 'fixed';
        content.style.top = '0';
        content.style.marginTop = 0;
      }
    },
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
</style>
