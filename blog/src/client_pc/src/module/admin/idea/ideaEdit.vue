<template>
  <a-form-model ref="form" :model="data">
    <a-row>
      <a-col>
        <h2 align="center">写下你的想法</h2>
        <sp-editor v-model="data.content"></sp-editor>
      </a-col>
    </a-row>
  </a-form-model>
</template>

<script>
import { edit } from 'sixpence.platform.pc.vue';

export default {
  name: 'ideaEdit',
  mixins: [edit],
  data() {
    return {
      controllerName: 'idea'
    };
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
