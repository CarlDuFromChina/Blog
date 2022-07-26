<template>
  <a-form-model ref="form" :model="data">
    <a-row :gutter="24" v-if="pageState == 'edit'">
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
    <a-row v-if="pageState == 'edit'">
      <a-col :span="24">
        <a-form-model-item label="图片">
          <img :src="imageUrl" style="max-width: 100%;max-height: 100%;" />
        </a-form-model-item>
      </a-col>
    </a-row>
  </a-form-model>
</template>

<script>
import { edit } from '@/mixins';

export default {
  name: 'materialRead',
  mixins: [edit],
  data() {
    return {
      controllerName: 'sys_file',
      baseUrl: sp.getServerUrl()
    };
  },
  computed: {
    imageUrl() {
      return sp.getDownloadUrl(this.data.id, false);
    }
  }
};
</script>
