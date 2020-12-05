<template>
  <a-modal title="图库" v-model="visible" width="60%">
    <a-form-model-item label="关键词">
      <a-input-search placeholder="请输入关键词" enter-button @search="loadData" />
    </a-form-model-item>
    <div class="gallery">
      <a-card
        hoverable
        v-for="item in dataList"
        class="item"
        :key="item.id"
        :class="{ active: (selected || {}).id == item.id }"
        @click="handleSelect(item)"
      >
        <img slot="cover" alt="example" :src="item.previewURL" />
      </a-card>
      <a-empty v-show="!dataList || dataList.length == 0" style="width: 100%"></a-empty>
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
      dataList: [],
      controllerName: 'Gallery',
      selected: null
    };
  },
  methods: {
    handleSelect(item) {
      this.selected = item;
    },
    async loadData(value) {
      sp.get(`api/${this.controllerName}/GetImages?searchValue=${encodeURIComponent(value)}`).then(resp => {
        this.dataList = resp.hits;
      });
    },
    handleOk(e) {
      this.visible = false;
      this.confirmLoading = false;
      this.$emit('selected', this.selected);
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
    img {
      max-width: 100%;
      max-height: 100%;
      width: 100%;
      height: 100%;
    }
  }
  .active {
    position: relative;
    border: 1px solid #99cc33;
  }
  .active::after {
    display: inline-block;
    content: '√';
    color: #fff;
    width: 0;
    height: 0;
    border-top: 20px solid transparent;
    border-left: 20px solid transparent;
    border-right: 20px solid #99cc33;
    border-bottom: 20px solid #99cc33;
    position: absolute;
    bottom: 0;
    right: 0;
  }
}
</style>
