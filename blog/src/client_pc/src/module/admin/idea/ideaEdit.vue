<template>
  <a-model-form ref="form" :model="data">
    <a-row>
      <a-col>
        <h2 align="center">写下你的想法</h2>
        <a-input type="textarea" maxlength="500" rows="10" v-model="data.content"></a-input>
      </a-col>
    </a-row>
    <p></p>
    <a-row>
      <a-col style="text-align:right">
        <span class="dialog-footer">
          <a-button @click="$emit('close')">取 消</a-button>
          <a-button type="primary" @click="saveData">发 布</a-button>
        </span>
      </a-col>
    </a-row>
  </a-model-form>
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
