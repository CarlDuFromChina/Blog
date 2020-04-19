<template>
  <el-form ref="form" :model="data" label-width="80px" :rules="rules">
    <el-row>
      <el-col :span="8">
        <el-form-item label="名称" prop="name">
          <el-input v-model="data.name"></el-input>
        </el-form-item>
      </el-col>
      <el-col :span="8">
        <el-form-item label="编码" prop="code">
          <el-input v-model="data.code"></el-input>
        </el-form-item>
      </el-col>
      <el-col :span="8">
        <el-form-item label="类型" prop="attr_type">
          <el-select v-model="data.attr_type" placeholder="请选择">
            <el-option v-for="item in typeOptions" :key="item.value" :label="item.label" :value="item.value"> </el-option>
          </el-select>
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="8">
        <el-form-item label="长度">
          <el-input v-model="data.attr_length"></el-input>
        </el-form-item>
      </el-col>
      <el-col :span="8">
        <el-form-item label="必填">
          <el-checkbox v-model="data.isrequire2"></el-checkbox>
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
      controllerName: 'SysAttrs',
      typeOptions: [
        {
          value: 'varchar',
          label: '字符串'
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
        }
      ],
      rules: {
        name: [{ required: true, message: '请输入实体名', trigger: 'blur' }],
        code: [{ required: true, message: '请输入编码', trigger: 'blur' }],
        attr_type: [{ required: true, message: '请选择字段类型', trigger: 'blur' }]
      }
    };
  },
  created() {
    this.data.entityid = this.parent.id;
    this.data.entityidname = this.parent.name;
  },
  methods: {
    saveData() {
      if (sp.isNullOrEmpty(this.Id)) {
        this.data.Id = sp.newUUID();
      }
      this.$refs.form.validate(resp => {
        if (resp) {
          this.data.isrequire = this.data.isrequire2 ? 1 : 0;
          sp.post(`api/${this.controllerName}/CreateData`, this.data)
            .then(resp => {
              this.$emit('close');
              this.$message.success('添加成功');
            })
            .catch(error => {
              this.$message.error(error);
            });
        } else {
          this.$message.error('请检查表单必填项');
        }
      });
    }
  }
};
</script>

<style></style>
