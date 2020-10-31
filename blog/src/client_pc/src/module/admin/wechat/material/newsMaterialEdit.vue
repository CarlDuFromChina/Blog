<template>
  <a-form-model ref="form" :model="data">
    <a-row :gutter="24">
      <a-col :span="12">
        <a-form-model-item label="标题">
          <a-input v-model="data.name"></a-input>
        </a-form-model-item>
      </a-col>
    </a-row>
    <a-row :gutter="24">
      <a-col :span="24">
        <a-form-model-item label="内容">
          <sp-editor v-model="data.content"></sp-editor>
        </a-form-model-item>
      </a-col>
    </a-row>
  </a-form-model>
</template>

<script>
import { edit } from 'sixpence.platform.pc.vue';

export default {
  name: 'newsMaterial-edit',
  mixins: [edit],
  data() {
    return {
      controllerName: 'WeChatNews'
    };
  }
};
</script>
