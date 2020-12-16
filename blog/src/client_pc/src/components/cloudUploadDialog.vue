<template>
  <a-modal title="图库" v-model="visible" width="60%">
    <a-form-model-item label="数据源">
      <a-radio-group v-model="source" @change="onChange">
        <a-radio :value="1">
          本地图库
        </a-radio>
        <a-radio :value="2">
          云图库
        </a-radio>
      </a-radio-group>
    </a-form-model-item>
    <a-form-model-item label="关键词" v-show="source == 2">
      <a-input-search placeholder="请输入关键词" enter-button @search="loadData" v-model="searchValue" />
    </a-form-model-item>
    <a-spin :spinning="loading" class="gallery">
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
    </a-spin>
    <a-pagination
      show-size-changer
      :default-current="pageIndex"
      :default-pageSize="pageSize"
      :total="total"
      @showSizeChange="sizeChange"
      @change="currentPage"
    />
    <span slot="footer" class="dialog-footer">
      <a-button @click="visible = false">取 消</a-button>
      <a-button type="primary" @click="handleOk" :loading="loading">确 定</a-button>
    </span>
  </a-modal>
</template>

<script>
import { pagination } from 'sixpence.platform.pc.vue';

export default {
  name: 'cloudUpload',
  mixins: [pagination],
  data() {
    return {
      visible: false,
      dataList: [],
      controllerName: 'Gallery',
      selected: null,
      loading: false,
      baseUrl: sp.getBaseUrl(),
      source: 1,
      pageSize: 12,
      searchValue: ''
    };
  },
  created() {
    this.loadData();
  },
  methods: {
    handleSelect(item) {
      this.selected = item;
    },
    onChange(value) {
      if (this.source === 2) {
        this.dataList = [];
        this.total = 0;
      } else {
        this.loadData();
      }
    },
    currentPage(index) {
      this.pageIndex = index;
      this.loadData();
    },
    getLocalData() {
      let url = `api/${this.controllerName}/GetDataList?searchValue=&viewId=0F0DC786-CF7D-4997-B42C-47FB09B12AAE&searchList=&orderBy=`;
      url += `&pageIndex=${this.pageIndex}&pageSize=${this.pageSize}`;
      return sp.get(url).then(resp => {
        this.dataList = resp.DataList.map(item => ({
          id: item.Id,
          previewURL: `${this.baseUrl}${item.preview_url}`,
          preview_url: item.preview_url,
          previewid: item.previewid,
          image_url: item.image_url,
          imageid: item.imageid
        }));
        this.total = resp.RecordCount;
      });
    },
    getCloudData() {
      return sp
        .get(
          `api/${this.controllerName}/GetImages?searchValue=${encodeURIComponent(this.searchValue)}&pageIndex=${this.pageIndex}&pageSize=${
            this.pageSize
          }`
        )
        .then(resp => {
          if (resp) {
            this.dataList = resp.hits;
            this.total = resp.total;
          }
        });
    },
    async loadData() {
      this.loading = true;
      try {
        if (this.source === 1) {
          await this.getLocalData();
        } else {
          await this.getCloudData();
        }
      } catch (error) {
        this.$message.error('请求失败');
      } finally {
        setTimeout(() => {
          this.loading = false;
        }, 200);
      }
    },
    uploadImages(item) {
      return sp.post('api/Gallery/UploadImage', item).then(resp => {
        this.$emit('selected', {
          surfaceid: resp.Item1,
          surface_url: `api/SysFile/Download?objectId=${resp.Item1}`,
          big_surfaceid: resp.Item2,
          big_surface_url: `api/SysFile/Download?objectId=${resp.Item2}`
        });
      });
    },
    async handleOk(e) {
      if (!this.selected) {
        this.$message.error('请选择一个图片');
        return;
      }
      if (this.source === 1) {
        this.$emit('selected', {
          surfaceid: this.selected.previewid,
          surface_url: this.selected.preview_url,
          big_surfaceid: this.selected.imageid,
          big_surface_url: this.selected.image_url
        });
      } else {
        await this.uploadImages();
      }
      this.visible = false;
    }
  }
};
</script>

<style lang="less" scoped>
.gallery {
  /deep/ .ant-spin-container {
    width: 100%;
    display: flex;
    flex-wrap: wrap;
  }
  /deep/ .ant-card-cover {
    max-width: 200px;
    min-width: 100%;
    max-height: 150px;
    width: 200px;
    height: 150px;
  }
  .item {
    width: 20%;
    margin: 10px;
    img {
      max-width: 200px;
      min-width: 100%;
      max-height: 150px;
      width: 200px;
      height: 150px;
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

/deep/ .ant-pagination {
  text-align: center;
}
</style>
