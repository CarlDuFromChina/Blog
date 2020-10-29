<template>
  <div ref="editor"></div>
</template>

<script>
import E from 'wangeditor';

export default {
  name: 'sp-editor',
  props: {
    value: {
      type: String,
      default: ''
    }
  },
  model: {
    prop: 'value',
    event: 'input'
  },
  data() {
    return {
      editor: {}
    };
  },
  watch: {
    value() {
      this.editor.txt.html(this.value);
    }
  },
  mounted() {
    this.editor = new E(this.$refs.editor);
    this.editor.config.onchange = html => {
      this.$emit('input', html);
    };
    this.editor.create();
  }
};
</script>

<style></style>
