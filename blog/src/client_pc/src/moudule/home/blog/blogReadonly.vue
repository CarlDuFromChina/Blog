<template>
  <div class="blog">
    <div class="__header">
      <el-button type="primary" icon="el-icon-back" @click="$router.back()">返回</el-button>
    </div>
    <div class="__body">
      <div class="__body__wrapper">
        <div class="__title">{{ data.title }}</div>
        <div class="__content">
          <vue-markdown :source="content"></vue-markdown>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import VueMarkdown from 'vue-markdown';
import marked from 'marked';

export default {
  name: 'blogReadonly',
  components: { VueMarkdown },
  data() {
    return {
      Id: this.$route.params.id,
      controllerName: 'blog',
      data: {},
      content: ''
    };
  },
  created() {
    this.loadData();
  },
  methods: {
    async loadData() {
      this.data = await sp.get(`api/${this.controllerName}/GetData?id=${this.Id}`);
      if (this.loadComplete && typeof this.loadComplete === 'function') {
        this.loadComplete();
      }
    },
    loadComplete() {
      this.content = marked(this.data.content, {
        sanitize: true
      });
    }
  }
};
</script>

<style lang="less">
.blog {
  height: 100%;
  .__header {
    width: 100%;
    height: 60px;
    display: inline-block;
    line-height: 60px;
    padding-left: 20px;
    box-shadow: 0 0.125rem 0.25rem rgba(0, 0, 0, 0.075);
  }
  .__body {
    height: calc(100% - 60px);
    background-color: #e9ecef;
    color: #212529;
    padding-top: 24px;
    .__body__wrapper {
      width: calc(100% - 200px);
      margin: 0px 104px;
      padding: 20px;
      background-color: #fff;
      .__title {
        font-size: 2rem;
        text-align: center;
      }
      .__content {
        height: 100%;
      }
    }
  }
}
</style>
