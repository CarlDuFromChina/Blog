<template>
  <a-form-model ref="form" :model="data" :rules="rules">
    <a-row :gutter="24">
      <a-col :span="12">
        <a-form-model-item label="名称" prop="name">
          <a-input v-model="data.name"></a-input>
        </a-form-model-item>
      </a-col>
      <a-col :span="12">
        <a-form-model-item label="编码" prop="code">
          <a-input v-model="data.code"></a-input>
        </a-form-model-item>
      </a-col>
    </a-row>
    <a-row :gutter="24">
      <a-col :span="24">
        <a-form-model-item label="描述">
          <a-input v-model="data.description"></a-input>
        </a-form-model-item>
      </a-col>
    </a-row>
    <a-row :gutter="24">
      <a-col :span="24">
        <a-form-model-item label="值" prop="value">
          <a-textarea v-model="data.value" :rows="4"></a-textarea>
        </a-form-model-item>
      </a-col>
    </a-row>
  </a-form-model>
</template>

<script>
import { edit } from '@/mixins';

export default {
  name: 'sysConfigEdit',
  mixins: [edit],
  data() {
    return {
      controllerName: 'sys_config',
      rules: {
        name: [{ required: true, message: '请输入参数名', trigger: 'blur' }],
        code: [{ required: true, message: '请输入编码', trigger: 'blur' }],
        value: [{ required: true, message: '请输入值', trigger: 'blur' }]
      }
    };
  }
};
</script>
