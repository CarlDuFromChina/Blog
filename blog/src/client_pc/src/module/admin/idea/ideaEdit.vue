<template>
  <a-form-model ref="form" :model="data">
    <a-row>
      <a-col>
        <h2 align="center">写下你的想法</h2>
        <a-input type="textarea" maxlength="500" rows="10" v-model="data.content"></a-input>
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
  }
};
</script>

<style></style>
