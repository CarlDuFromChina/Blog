<template>
  <a-form-model ref="form" :model="data" :rules="rules">
    <a-row :gutter="24">
      <a-col :span="12">
        <a-form-model-item label="名称" prop="name">
          <a-input v-model="data.name"></a-input>
        </a-form-model-item>
      </a-col>
      <a-col :span="12">
        <a-form-model-item label="执行时间" prop="runtime">
          <a-input v-model="data.runtime"></a-input>
        </a-form-model-item>
      </a-col>
    </a-row>
    <a-row :gutter="24">
      <a-col :span="12">
        <a-form-model-item label="机器人">
          <sp-select v-model="data.robotid" :options="selectDataList.robot" @change="item => (data.robotid_name = item.name)"></sp-select>
        </a-form-model-item>
      </a-col>
      <a-col :span="12">
        <a-form-model-item label="消息类型">
          <sp-select
            v-model="data.message_type"
            :options="selectDataList.message_type"
            @change="item => (data.message_type_name = item.name)"
          ></sp-select>
        </a-form-model-item>
      </a-col>
    </a-row>
    <a-row :gutter="24">
      <a-col :span="24">
        <a-form-model-item label="内容">
          <a-textarea v-model="data.content"></a-textarea>
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
      controllerName: 'robot_message_task',
      rules: {
        name: [{ required: true, message: '请输入任务名', trigger: 'blur' }],
        runtime: [{ required: true, message: '请输入执行时间', trigger: 'blur' }]
      },
      selectParamNameList: ['message_type'],
      selectEntityNameList: ['robot']
    };
  }
};
</script>
