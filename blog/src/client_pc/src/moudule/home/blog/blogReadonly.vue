<template>
  <div>
    <markdown-read :title="data.title" :content="content">
      <div class="blog-header">
        <el-button type="primary" icon="el-icon-back" @click="$router.back()">返回</el-button>
      </div>
    </markdown-read>
  </div>
</template>

<script>
import markdownRead from '../../../components/markdownRead';
import marked from 'marked';

export default {
  name: 'blogReadonly',
  components: { markdownRead },
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
