<template>
  <sp-markdown-read :title="data.title" :content="data.content">
    <div class="blog-header">
      <el-button type="primary" icon="el-icon-back" @click="$router.back()">返回</el-button>
    </div>
  </sp-markdown-read>
</template>

<script>
export default {
  name: 'blogReadonly',
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
    }
  }
};
</script>
