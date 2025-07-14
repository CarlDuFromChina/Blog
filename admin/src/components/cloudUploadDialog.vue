<template>
  <a-modal title="图库" v-model="visible" width="60%">
    <a-form-model-item label="数据源">
      <a-radio-group v-model="source" @change="onChange">
        <a-radio :value="1"> 本地图库 </a-radio>
        <a-radio :value="2"> 云图库 </a-radio>
        <a-radio :value="3"> 随机图片 </a-radio>
      </a-radio-group>
    </a-form-model-item>
    <a-form-model-item label="关键词" v-show="source == 2">
      <a-input-search placeholder="请输入关键词" enter-button @search="loadData" v-model="searchValue" />
    </a-form-model-item>
    <a-spin :spinning="loading" v-show="source == 1 || source == 2" class="gallery">
      <a-col
        v-for="item in dataList"
        class="item"
        :key="item.id"
        :class="{ active: (selected || {}).id == item.id }"
        :span="6"
        @click="handleSelect(item)"
      >
        <img alt="example" :src="item.previewURL" />
      </a-col>
      <a-empty v-show="!dataList || dataList.length == 0" style="width: 100%"></a-empty>
    </a-spin>
    <a-pagination
      v-show="source == 1 || source == 2"
      show-size-changer
      :current="pageIndex"
      :pageSize="pageSize"
      :total="total"
      @showSizeChange="sizeChange"
      @change="currentPage"
      style="padding-top: 20px"
    />
    <span slot="footer" class="dialog-footer">
      <a-button @click="visible = false">取 消</a-button>
      <a-button type="primary" @click="handleOk" :loading="loading">确 定</a-button>
    </span>
  </a-modal>
</template>

<script>
import { pagination } from '@/mixins';

export default {
  name: 'cloudUpload',
  mixins: [pagination],
  data() {
    return {
      visible: false,
      dataList: [],
      controllerName: 'gallery',
      selected: null,
      loading: false,
      baseUrl: sp.getServerUrl(),
      source: 1,
      pageSize: 12,
      searchValue: '',
      isSearchValueChanged: false
    };
  },
  created() {
    this.loadData();
  },
  watch: {
    searchValue() {
      this.isSearchValueChanged = true;
    }
  },
  methods: {
    handleSelect(item) {
      this.selected = item;
    },
    onChange() {
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
      let url = `api/${this.controllerName}/search?searchValue=&viewId=0F0DC786-CF7D-4997-B42C-47FB09B12AAE&searchList=&orderBy=`;
      url += `&pageIndex=${this.pageIndex}&pageSize=${this.pageSize}`;
      return sp.get(url).then(resp => {
        this.dataList = resp.DataList.map(item => ({
          id: item.id,
          previewURL: sp.getDownloadUrl(item.preview_url),
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
          `api/${this.controllerName}/cloud/search?searchValue=${encodeURIComponent(this.searchValue)}&pageIndex=${this.pageIndex}&pageSize=${
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
      if (this.isSearchValueChanged) {
        this.pageIndex = 1;
        this.isSearchValueChanged = false;
      }
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
    async uploadImages() {
      return sp.post('api/gallery/upload', this.selected);
    },
    async getRandomImage() {
      return sp.get('api/gallery/random_image');
    },
    async handleOk() {
      switch (this.source) {
        case 1:
          if (!this.selected) {
            this.$message.error('请选择一个图片');
            return;
          }
          this.$emit('selected', {
            surfaceid: this.selected.previewid,
            surface_url: this.selected.preview_url,
            big_surfaceid: this.selected.imageid,
            big_surface_url: this.selected.image_url
          });
          break;
        case 2: {
          if (!this.selected) {
            this.$message.error('请选择一个图片');
            return;
          }
          var resp = await this.uploadImages();
          this.$emit('selected', {
            surfaceid: resp[0],
            surface_url: `api/sys_file/download?objectId=${resp[0]}`,
            big_surfaceid: resp[1],
            big_surface_url: `api/sys_file/download?objectId=${resp[1]}`
          });
          break;
        }
        case 3: {
          var gallery = await this.getRandomImage();
          this.$emit('selected', {
            surfaceid: gallery.previewid,
            surface_url: gallery.preview_url,
            big_surfaceid: gallery.imageid,
            big_surface_url: gallery.image_url
          });
          break;
        }
        default:
          break;
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
  .item {
    max-height: 150px;
    padding: 12px;
    img {
      max-width: 100%;
      max-height: 100%;
      min-width: 100%;
      min-height: 100%;
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
