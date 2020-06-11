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
    <el-row v-if="!isGrid">
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
  name: 'sysParamGroupEdit',
  props: {
    isGrid: {
      type: Boolean,
      default: false
    }
  },
  mixins: [edit],
  data() {
    return {
      controllerName: 'SysParamGroup',
      rules: {
        name: [{ required: true, message: '请输入名称', trigger: 'Null' }],
        code: [{ required: true, message: '请再次编码', trigger: 'Null' }]
      }
    };
  }
};
</script>
