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
    },
    uploadImgParams: {
      type: Object,
      default: () => {}
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
    },
    uploadImgParams() {
      this.editor.config.uploadImgParams = this.uploadImgParams;
    }
  },
  mounted() {
    this.editor = new E(this.$refs.editor);
    this.editor.config.onchange = html => {
      this.$emit('input', html);
    };
    this.editor.config.uploadImgParamsWithUrl = true;
    this.editor.config.uploadImgHeaders = {
      Authorization: `BasicAuth ${localStorage.getItem('Token') || ''}`
    };
    this.editor.config.uploadImgMaxLength = 1; // 最多一次上传一张照片
    this.editor.config.uploadImgServer = `${sp.getBaseUrl()}api/DataService/UploadImage`; // 上传图片服务地址
    this.editor.config.uploadImgHooks = {
      customInsert: function(insertImgFn, result) {
        insertImgFn(`${sp.getBaseUrl()}${result.downloadUrl}`);
      }
    };
    this.editor.create();
  }
};
</script>

<style></style>
