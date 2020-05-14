<template>
  <el-form ref="form" :model="data">
    <el-row>
      <el-col>
        <h2 align="center">写下你的想法</h2>
        <el-input type="textarea" maxlength="500" show-word-limit rows="10" v-model="data.content"></el-input>
      </el-col>
    </el-row>
    <p></p>
    <el-row>
      <el-col style="text-align:right">
        <span class="dialog-footer">
          <el-button @click="$emit('close')">取 消</el-button>
          <el-button type="primary" @click="saveData">发 布</el-button>
        </span>
      </el-col>
    </el-row>
  </el-form>
</template>

<script>
import { edit } from 'sixpence.platform.pc.vue';

export default {
  name: 'ideaEdit',
  mixins: [edit],
  data() {
    return {
      controllerName: 'idea'
    };
  }
};
</script>

<style>
</style>
