<template>
  <a-form-model ref="form" :model="data">
    <a-row>
      <a-col>
        <h2 align="center">写下你的想法</h2>
        <sp-editor v-model="data.content" :uploadImgParams="uploadImgParams" :disabledMenu="['image', 'video']"></sp-editor>
      </a-col>
    </a-row>
  </a-form-model>
</template>

<script>
import { edit } from '@/mixins';

export default {
  name: 'ideaEdit',
  mixins: [edit],
  data() {
    return {
      controllerName: 'idea'
    };
  },
  computed: {
    uploadImgParams() {
      return {
        fileType: 'idea',
        objectId: this.data.id
      };
    }
  },
  methods: {
    preSave() {
      this.data.name = `${this.$moment().format('YYYY-MM-DD HH:mm:ss')} 想法`;
      return true;
    }
  }
};
</script>

<style></style>
