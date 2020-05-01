<template>
  <div class="blog">
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
  name: 'changeLogList',
  components: { VueMarkdown },
  data() {
    return {
      Id: this.$route.params.id,
      controllerName: 'ChangeLog',
      data: {},
      content: ''
    };
  },
  created() {
    // this.loadData();
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
  .__body {
    background-color: #e9ecef;
    color: #212529;
    padding-top: 24px;
    .__body__wrapper {
      max-width: 900px;
      margin: 0 auto;
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
