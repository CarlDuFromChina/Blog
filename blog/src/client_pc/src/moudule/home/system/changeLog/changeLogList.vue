<template>
  <div class="blog blog__readonly">
    <div class="blog-body">
      <div class="bodyWrapper">
        <div class="bodyWrapper-title">{{ data.title }}</div>
        <div class="bodyWrapper-content">
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
