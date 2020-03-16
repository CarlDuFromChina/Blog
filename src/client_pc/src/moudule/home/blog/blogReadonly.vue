<template>
  <div class="blog">
    <div class="__header">
      <el-button type="primary" icon="el-icon-back" @click="$router.back()">返回</el-button>
    </div>
    <div class="__body">
      <div class="__body__wrapper">
        <div class="__title">123</div>
        <div class="__content">
          <vue-markdown :source="data"></vue-markdown>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import VueMarkdown from 'vue-markdown';
import marked from 'marked';

export default {
  name: 'blog',
  components: { VueMarkdown },
  data() {
    return {
      data: '',
      title: ''
    };
  },
  created() {
    this.data = marked(this.$route.params.content || '123', {
      sanitize: true
    });
    this.title = this.$route.params.title;
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
      height: calc(100% - 120px);
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
