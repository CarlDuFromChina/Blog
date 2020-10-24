<template>
  <a-form-model ref="form" :model="data">
    <a-row>
      <a-col>
        <h2 align="center">写下你的想法</h2>
        <div ref="editor"></div>
      </a-col>
    </a-row>
  </a-form-model>
</template>

<script>
import { edit } from 'sixpence.platform.pc.vue';
import E from 'wangeditor';

export default {
  name: 'ideaEdit',
  mixins: [edit],
  data() {
    return {
      controllerName: 'idea',
      editor: {}
    };
  },
  mounted() {
    this.editor = new E(this.$refs.editor);
    this.editor.config.onchange = html => {
      this.data.content = html;
    };
    this.editor.create();
  },
  methods: {
    loadComplete() {
      this.editor.txt.html(this.data.content);
    }
  }
};
</script>

<style></style>
