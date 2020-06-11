<template>
  <el-form ref="form" :model="data" label-width="80px">
    <el-row>
      <el-col :span="12">
        <el-form-item label="标题">
          <el-input v-model="data.name"></el-input>
        </el-form-item>
      </el-col>
      <el-col :span="12">
        <el-form-item label="链接">
          <el-input v-model="data.url"></el-input>
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col>
        <el-form-item label="分类">
          <el-select v-model="data.recommend_type" @change="handleTypeChange">
            <el-option :label="item.Name" :value="item.Value" v-for="(item, index) in recommentType" :key="index"></el-option>
          </el-select>
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
  name: 'recommendInfoEdit',
  mixins: [edit],
  data() {
    return {
      controllerName: 'RecommendInfo',
      Id: '',
      data: {},
      recommentType: []
    };
  },
  created() {
    sp.get('api/SysParamGroup/GetParams?code=recommend_type').then(resp => {
      this.recommentType = resp;
    });
  },
  methods: {
    handleTypeChange(value) {
      const arrs = this.recommentType.filter(item => item.Value === value);
      if (arrs.length > 0) {
        this.data.recommend_typeName = arrs[0].Name;
      }
    }
  }
};
</script>

<style></style>
