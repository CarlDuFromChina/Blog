<template>
  <a-form-model ref="form" :model="data" :rules="rules">
    <a-row :gutter="24">
      <a-col :span="12">
        <a-form-model-item label="名称" prop="name">
          <a-input v-model="data.name"></a-input>
        </a-form-model-item>
      </a-col>
      <a-col :span="12">
        <a-form-model-item label="钩子地址" prop="hook">
          <a-input v-model="data.hook"></a-input>
        </a-form-model-item>
      </a-col>
    </a-row>
    <a-row :gutter="24">
      <a-col :span="12">
        <a-form-model-item label="类型" prop="robot_type">
          <sp-select v-model="data.robot_type" :options="selectDataList.robot_type" @change="item => (data.robot_type_name = item.name)"></sp-select>
        </a-form-model-item>
      </a-col>
    </a-row>
    <a-row :gutter="24">
      <a-col :span="12">
        <a-form-model-item label="描述">
          <a-input v-model="data.description"></a-input>
        </a-form-model-item>
      </a-col>
    </a-row>
  </a-form-model>
</template>

<script>
import { edit, select } from '@/mixins';

export default {
  name: 'robot-edit',
  mixins: [edit, select],
  data() {
    return {
      controllerName: 'robot',
      rules: {
        name: [{ required: true, message: '请输入参数名', trigger: 'blur' }],
        hook: [{ required: true, message: '请输入钩子地址', trigger: 'blur' }],
        value: [{ required: true, message: '请输入值', trigger: 'blur' }]
      },
      selectParamNameList: ['robot_type'],
      robotType: []
    };
  }
};
</script>
