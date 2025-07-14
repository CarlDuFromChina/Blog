<template>
  <a-form-model ref="form" :model="data" :rules="rules">
    <a-row :gutter="24">
      <a-col :span="8">
        <a-form-model-item label="名称" prop="name">
          <a-input v-model="data.name"></a-input>
        </a-form-model-item>
      </a-col>
      <a-col :span="8">
        <a-form-model-item label="编码" prop="code">
          <a-input v-model="data.code"></a-input>
        </a-form-model-item>
      </a-col>
      <a-col :span="8">
        <a-form-model-item label="类型" prop="attr_type">
          <a-select v-model="data.attr_type" placeholder="请选择">
            <a-select-option v-for="item in typeOptions" :key="item.value" :value="item.value">{{ item.label }}</a-select-option>
          </a-select>
        </a-form-model-item>
      </a-col>
    </a-row>
    <a-row :gutter="24">
      <a-col :span="8">
        <a-form-model-item label="长度">
          <a-input v-model="data.attr_length" type="number"></a-input>
        </a-form-model-item>
      </a-col>
      <a-col :span="8">
        <a-form-model-item label="必填">
          <a-switch v-mode="data.isrequire"></a-switch>
        </a-form-model-item>
      </a-col>
    </a-row>
  </a-form-model>
</template>

<script>
import { edit } from '@/mixins';

export default {
  name: 'sysAttrsEdit',
  mixins: [edit],
  props: {
    parent: {
      type: Object,
      default: () => {}
    }
  },
  data() {
    return {
      controllerName: 'sys_attrs',
      typeOptions: [
        {
          value: 'varchar',
          label: '字符串'
        },
        {
          value: 'text',
          label: '长文本'
        },
        {
          value: 'INT4',
          label: '数值（4）'
        },
        {
          value: 'INT8',
          label: '数值（8）'
        },
        {
          value: 'timestamp',
          label: '时间'
        },
        {
          value: 'bytea',
          label: '二进制'
        },
        {
          value: 'json',
          label: 'json'
        }
      ],
      rules: {
        name: [{ required: true, message: '请输入字段名', trigger: 'blur' }],
        code: [{ required: true, message: '请输入编码', trigger: 'blur' }],
        attr_type: [{ required: true, message: '请选择字段类型', trigger: 'blur' }]
      }
    };
  },
  created() {
    this.data.entityid = this.parent.id;
    this.data.entityid_name = this.parent.name;
    this.data.entityCode = this.parent.entityCode;
  },
  methods: {
    saveData() {
      if (this.data.attr_type !== 'timestamp' && this.data.attr_length === 0) {
        this.$message.error('字段长度必须大于0');
        return;
      }
      const that = this;
      this.$refs.form.validate(resp => {
        if (resp) {
          if (sp.isNull(that.data.attr_length)) {
            that.data.attr_length = 0;
          }
          sp.post(`api/${this.controllerName}`, that.data)
            .then(() => {
              that.$emit('close');
              that.$refs.form.resetFields();
              that.$message.success('添加成功');
            })
            .catch(error => {
              that.$message.error(error);
            });
        } else {
          that.$message.error('请检查表单必填项');
        }
      });
    }
  }
};
</script>

<style></style>
