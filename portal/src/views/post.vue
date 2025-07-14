<template>
  <div id="blog" class="blog blog__readonly">
    <blog-menu></blog-menu>
    <div class="blog-body">
      <div class="bodyWrapper">
        <a-layout>
          <!--左侧按钮：点赞、评论-->
          <a-layout-sider width="10%" theme="light" style="text-align: center">
          </a-layout-sider>
          <!--右侧内容-->
          <a-layout-sider width="60%" theme="light">
            <a-card>
              <a-skeleton :loading="loading">
                <!--标题、作者-->
                <div class="block">
                  <div class="bodyWrapper-title">{{ data.title }}</div>
                  <div style="display: flex">
                    <div>
                      <div style="color: #72777b; font-size: 14px; padding-top: 5px; font-style: italic;">
                        作者：{{ data.created_by_name }}
                      </div>
                      <div style="color: #72777b; font-size: 14px; padding-top: 5px; font-style: italic;">
                        最后修改时间：{{ data.updated_at | moment('YYYY-MM-DD HH:mm') }}
                      </div>
                    </div>
                  </div>
                </div>
                <!--内容-->
                <div id="blog_content" class="bodyWrapper-content">
                  <article v-highlight v-html="formatterContent" class="markdown-body" @click="handleImgClick($event)"></article>
                </div>
              </a-skeleton>
            </a-card>
            <!--disqus评论组件-->
            <Disqus
              :shortname="disqusShortName"
              lang="zh-CN"
              v-if="!data.disable_comment"
              :style="{ marginTop: '32px'}"
            >
            </Disqus>
          </a-layout-sider>
          <!--右侧目录栏-->
          <a-layout-sider width="20%" style="margin-left: 20px" theme="light">
            <sp-card id="catalog" :loading="false" class="catalog">
              <div slot="title" class="title">目录</div>
              <div id="content" class="block"></div>
              <a-empty slot="empty" v-if="isCatagoryEmpty" :description="false" />
            </sp-card>
          </a-layout-sider>
        </a-layout>
      </div>
    </div>
    <a-back-top :target="getBlogEl" :visibilityHeight="100" />
    <a-modal
      :visible="previewVisible"
      :footer="null"
      @cancel="() => (this.previewVisible = false)"
      class="preview-dialog"
      width="80%"
      :centered="true"
      :closable="false"
    >
      <img alt="example" style="width: 100%" :src="previewImage" :style="{ cursor: 'zoom-out' }" @click="() => (this.previewVisible = false)" />
    </a-modal>
  </div>
</template>

<script>
import Vue from 'vue';
import 'highlight.js/styles/atom-one-dark.css';
import { Disqus } from 'vue-disqus';
const marked = require('marked');

// 修改 header 标签的渲染器
const renderer = new marked.Renderer();
renderer.heading = function (text, level) {
  const anchor = tocObj.add(text, level);
  return `<a id=${anchor} class="anchor-fix"></a><h${level}>${text}</h${level}>\n`;
};

marked.setOptions({
  renderer: renderer
});
const tocObj = {
  add: function (text, level) {
    var anchor = `toc${level}${++this.index}`;
    this.toc.push({ anchor: anchor, level: level, text: text });
    return anchor;
  },
  toHTML: function () {
    let levelStack = [];
    let result = '';
    const addStartUL = () => {
      result += '<ul>';
    };
    const addEndUL = () => {
      result += '</ul>\n';
    };
    const addLI = (anchor, text) => {
      result += `<li class="content-item" @click="goAnchor('${anchor}')"><a id="catalog-${anchor}" href="javascript:void(0)">${text}</a></li>\n`;
    };
    this.toc.forEach(function (item) {
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
    // this.toc = [];
    // this.index = 0;
    return result;
  },
  toc: [],
  index: 0
};

export default {
  name: 'post',
  components: { Disqus },
  data() {
    return {
      id: this.$route.params.id,
      controllerName: 'post',
      data: {},
      loading: false,
      formatterContent: '',
      height: null,
      isUp: false,
      getDownloadUrl: sp.getDownloadUrl,
      previewImage: '',
      previewVisible: false,
      disqusShortName: process.env.VUE_APP_DISQUS_SHORTNAME,
      isCatagoryEmpty: false
    };
  },
  async created() {
    await this.loadData();
  },
  mounted() {
    var blogVM = document.getElementById('blog');

    blogVM.addEventListener('scroll', function() {
      // 吸住目录
      const catalogVM = document.getElementById('catalog');
      if (this.height > this.scrollTop) {
        catalogVM.style.position = 'relative';
        catalogVM.style.marginTop = 20;
      } else {
        catalogVM.style.position = 'sticky';
        catalogVM.style.top = '0';
        catalogVM.style.marginTop = 0;
      }

      for (let i = 0; i < tocObj.toc.length; i++) {
        const item = tocObj.toc[i];
        const next = i + 1 === tocObj.toc.length ? null : tocObj.toc[i + 1];
        var el = document.getElementById(item.anchor);
        var nextEl = document.getElementById(next?.anchor);
        var removeStatus = () => {
          for (let j = 0; j < tocObj.toc.length; j++) {
            if (i !== j) {
              document.getElementById(`catalog-${tocObj.toc[j].anchor}`).className = '';
            }
          }
        }

        // 当前滚动距离小于第一个标题
        if (i === 0 && this.scrollTop < el.offsetTop) {
          document.getElementById(`catalog-${item.anchor}`).className = 'active';
          removeStatus();
          break;
        }

        // 当前滚动距离大于这个标题但是还在这个标题范围之中 || 最后一个标题
        if (this.scrollTop > el.offsetTop && (sp.isNil(nextEl) || this.scrollTop < nextEl.offsetTop)) {
          document.getElementById(`catalog-${item.anchor}`).className = 'active';
          removeStatus();
          break;
        }
      }
    });
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
          this.isCatagoryEmpty = tocObj.toc.length === 0;
        }
      }
    }
  },
  methods: {
    handleImgClick($event) {
      var currentSrc = $event.target.currentSrc; // 拿到图片路径
      if (currentSrc) {
        this.previewImage = currentSrc;
        this.previewVisible = true;
      }
    },
    getBlogEl() {
      return document.getElementById('blog');
    },
    handleLinkClick() {
      const contentVM = document.getElementById('blog_content');
      const nodes = contentVM.getElementsByTagName('a');
      for (const node of nodes) {
        node.target = '_blank';
      }
    },
    async loadData() {
      this.loading = true;
      try {
        this.data = await sp.get(`api/${this.controllerName}/${this.id}`);
      } finally {
        setTimeout(() => {
          this.loading = false;
          this.$nextTick(() => {
            // 内容里的链接打开新的Tab
            this.handleLinkClick();
          });
        }, 200);
      }
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
          padding-bottom: 20px;
          text-align: left;
          font-weight: 600;
          color: #000000d9;
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

.title {
  color: #1d2129;
  font-size: 16px;
  border-bottom: 1px solid #e4e6eb;
  text-transform: uppercase;
  padding-bottom: 8px;
}

.catalog {
  padding-top: 8px;
  position: sticky;
}

.block {
  width: 100%;
  margin-bottom: 56px;
  border-radius: 6px;


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
    .block-author {
      display: flex;
      padding: 15px 0;
      border-bottom: 2px solid hsla(0, 0%, 58.8%, 0.1);
    }
    .block-content {
      padding-bottom: 5px;
      display: flex;
      align-items: center;
      > span {
        padding-right: 5px;
      }
      > .svg-icon {
        display: flex;
      }
    }
  }
}

/deep/ .block {
  ul {
    list-style: none;
    padding-left: 20px;
  }
  > ul {
    padding-left: 0px;
  }
  .content-item {
    color: #000000;
    padding: 8px 0;
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

.toolbar {
  position: fixed;
  top: 16rem;
  &-item {
    display: block;
    position: relative;
    margin-bottom: 1rem;
    width: 2.5rem;
    height: 2.5rem;
    background-color: #fff;
    background-position: 50%;
    background-repeat: no-repeat;
    border-radius: 50%;
    box-shadow: 0 2px 4px 0 rgba(0, 0, 0, 0.04);
    cursor: pointer;
    /deep/ svg {
      height: 2.5rem !important;
      vertical-align: middle;
      font-size: 1.5rem;
      color: #b2bac2;
    }
  }
}
</style>

<style lang="less">
.anchor-fix {
  display: block;
  height: 0; /*same height as header*/
  visibility: hidden;
}

.preview-dialog {
  .ant-modal-body {
    padding: 0;
    text-align: center;
  }

  .ant-modal-content {
    background: transparent !important;
    box-shadow: none !important;
  }

  img {
    width: auto !important;
    height: auto !important;
    max-width: 100%;
  }
}
</style>
