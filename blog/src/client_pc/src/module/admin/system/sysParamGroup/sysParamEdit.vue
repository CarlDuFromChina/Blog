<template>
  <el-form ref="form" :model="data" label-width="80px" :rules="rules">
    <el-row>
      <el-col :span="12">
        <el-form-item label="名称" prop="name">
          <el-input v-model="data.name"></el-input>
        </el-form-item>
      </el-col>
      <el-col :span="12">
        <el-form-item label="编码" prop="code">
          <el-input v-model="data.code"></el-input>
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col style="text-align:right">
        <span class="dialog-footer">
          <el-button @click="$emit('close')">取 消</el-button>
          <el-button type="primary" @click="saveData">确 定</el-button>
        </span>
      </el-col>
    </el-row>
  </el-form>
</template>

<script>
import { edit } from 'sixpence.platform.pc.vue';

export default {
  props: {
    relatedAttr: {
      type: Object
    }
  },
  inject: ['parentId'],
  name: 'sysParamEdit',
  mixins: [edit],
  data() {
    return {
      controllerName: 'SysParam',
      rules: {
        name: [{ required: true, message: '请输入名称', trigger: 'Null' }],
        code: [{ required: true, message: '请再次编码', trigger: 'Null' }]
      }
    };
  },
  methods: {
    saveData() {
      this.$refs.form.validate(valid => {
        if (valid) {
          const { id, name } = this.parentId();
          this.data.sys_paramGroupid = id;
          this.data.sys_paramGroupidName = name;
          const operateName = sp.isNullOrEmpty(this.Id) ? 'CreateData' : 'UpdateData';
          if (sp.isNullOrEmpty(this.Id)) {
            this.data.Id = sp.newUUID();
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
