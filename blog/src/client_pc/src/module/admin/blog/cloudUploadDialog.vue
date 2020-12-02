<template>
  <a-modal title="图库" :visible="visible" :confirm-loading="confirmLoading" width="60%">
    <a-form-model-item label="关键词">
      <a-input-search placeholder="请输入关键词" enter-button @search="loadData" />
    </a-form-model-item>
    <div class="gallery">
      <a-card hoverable v-for="item in dataList" class="item" :key="item.id">
        <img slot="cover" alt="example" :src="item.previewURL" />
      </a-card>
    </div>
    <span slot="footer" class="dialog-footer">
      <a-button @click="visible = false">取 消</a-button>
      <a-button type="primary" @click="handleOk">确 定</a-button>
    </span>
  </a-modal>
</template>

<script>
export default {
  name: 'cloudUpload',
  data() {
    return {
      visible: false,
      confirmLoading: false,
      dataList: [1, 2, 3, 4, 5],
      controllerName: 'Gallery'
    };
  },
  methods: {
    loadData(value) {
      sp.get(`api/${this.controllerName}/GetImages?searchValue=${value}`).then(resp => {
        this.dataList = resp.hits;
      });
    },
    handleOk(e) {
      this.confirmLoading = true;
      setTimeout(() => {
        this.visible = false;
        this.confirmLoading = false;
      }, 2000);
    }
  }
};
</script>

<style lang="less" scoped>
.gallery {
  display: flex;
  flex-wrap: wrap;
  justify-content: space-between;
  .item {
    width: 20%;
    margin: 10px;
  }
}
</style>
