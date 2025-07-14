<template>
  <a-form-model ref="form" :model="data">
    <a-row :gutter="24">
      <a-col :span="12">
        <a-form-model-item label="关键词">
          <a-input v-model="data.name"></a-input>
        </a-form-model-item>
      </a-col>
    </a-row>
    <a-row :gutter="24">
      <a-col :span="24">
        <a-form-model-item label="回复内容">
          <a-input type="textarea" maxlength="500" rows="10" v-model="data.reply_content"></a-input>
        </a-form-model-item>
      </a-col>
    </a-row>
  </a-form-model>
</template>

<script>
import { edit } from '@/mixins';

export default {
  name: 'keywords-edit',
  mixins: [edit],
  data() {
    return {
      controllerName: 'wechat_keywords'
    };
  }
};
</script>
