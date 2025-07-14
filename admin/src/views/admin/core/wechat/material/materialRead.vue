<template>
  <a-form-model ref="form" :model="data">
    <!-- 此标签作用为引用策略 -->
    <meta name="referrer" content="never" />
    <a-row :gutter="24">
      <a-col :span="12">
        <a-form-model-item label="名称">
          <a-input v-model="data.name" disabled></a-input>
        </a-form-model-item>
      </a-col>
      <a-col :span="12">
        <a-form-model-item label="创建时间">
          <a-input v-model="data.created_at" disabled></a-input>
        </a-form-model-item>
      </a-col>
    </a-row>
    <a-row>
      <a-col :span="24">
        <a-form-model-item label="图片" v-if="type == 'image'">
          <img :src="data.url" />
        </a-form-model-item>
      </a-col>
    </a-row>
  </a-form-model>
</template>

<script>
export default {
  name: 'materialRead',
  props: {
    data: {
      type: Object,
      default: () => {}
    },
    type: {
      type: String,
      default: 'image'
    }
  },
  data() {
    return {};
  }
};
</script>
