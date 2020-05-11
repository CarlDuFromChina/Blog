<template>
  <sp-markdown-read
    :title="data.title"
    :content="data.content"
    v-loading="loading"
    element-loading-text="拼命加载中"
    element-loading-spinner="el-icon-loading"
  >
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
      controllerName: 'blog2',
      data: {},
      content: '',
      loading: false
    };
  },
  created() {
    this.loadData();
  },
  methods: {
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
    }
  }
};
</script>
