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
  </a-form-model>
</template>

<script>
import { edit } from '@/mixins';

export default {
  inject: ['parentId'],
  name: 'sys-param-edit',
  mixins: [edit],
  data() {
    return {
      controllerName: 'SysParam',
      rules: {
        name: [{ required: true, message: '请输入名称', trigger: 'blur' }],
        code: [{ required: true, message: '请再次编码', trigger: 'blur' }]
      }
    };
  },
  methods: {
    saveData() {
      this.$refs.form.validate(valid => {
        if (valid) {
          const { id, name } = this.parentId();
          this.data.sys_paramGroupid = id;
          this.data.sys_paramGroupid_name = name;
          const operateName = sp.isNullOrEmpty(this.id) ? 'CreateData' : 'UpdateData';
          if (sp.isNullOrEmpty(this.id)) {
            this.data.id = uuid.generate();
          }
          sp.post(`api/${this.controllerName}/${operateName}`, this.data).then(() => {
            if (this.postSave && typeof this.postSave === 'function') {
              this.postSave();
            }
            this.$emit('close');
            this.$emit('load-data');
            this.$message.success('添加成功');
          });
        } else {
          this.$message.error('请检查表单必填项');
        }
      });
    }
  }
};
</script>
