<template>
  <sp-markdown-read :content="content" :title="data.title" backgroudColor="transparent"></sp-markdown-read>
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
      content: '',
      title: '测试'
    };
  },
  created() {
    // const md = require('./changeLog.md');
    this.content = marked('', {
      sanitize: true
    });
  }
};
</script>
