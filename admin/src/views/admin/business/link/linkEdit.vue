<template>
  <a-form-model ref="form" :model="data" :rules="rules">
    <a-form-model-item label="名称" prop="name">
      <a-input v-model="data.name"></a-input>
    </a-form-model-item>
    <a-form-model-item label="简短介绍">
      <a-input v-model="data.brief"></a-input>
    </a-form-model-item>
    <a-form-model-item label="链接类型" prop="link_type">
      <a-input v-model="data.link_type"></a-input>
    </a-form-model-item>
    <a-form-model-item label="链接地址" prop="link_url">
      <a-input v-model="data.link_url"></a-input>
    </a-form-model-item>
  </a-form-model>
</template>

<script>
import { edit } from '@/mixins';

export default {
  name: 'link-edit',
  mixins: [edit],
  data() {
    return {
      controllerName: 'link',
      rules: {
        name: [{ required: true, message: '请输入名称', trigger: 'blur' }],
        link_type: [{ required: true, message: '请输入链接类型', trigger: 'blur' }],
        link_url: [{ required: true, message: '请输入链接地址', trigger: 'blur' }]
      }
    };
  }
};
</script>

<style></style>
