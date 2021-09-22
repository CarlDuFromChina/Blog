<template>
  <div style="overflow-y: scroll; height: 100%">
    <sp-header>
      <div v-if="buttons && buttons.length > 0" style="display:inline-block">
        <sp-button-list :buttons="buttons" @search-change="loadData" @unfold="showMore = true" @fold="showMore = false"></sp-button-list>
      </div>
    </sp-header>
    <vue-waterfall-easy @click="showModal" :imgsArr="dataList" @scrollReachBottom="loadData"> </vue-waterfall-easy>
    <a-modal title="图片" v-model="readVisible" width="850px">
      <img class="big-image" :src="imgUrl" />
      <template slot="footer">
        <a-button type="primary" @click="downloadImg">点击下载</a-button>
      </template>
    </a-modal>
    <gallery-edit v-model="editVisible"></gallery-edit>
  </div>
</template>

<script>
import vueWaterfallEasy from 'vue-waterfall-easy2';
import galleryEdit from './galleryEdit';

export default {
  name: 'gallery',
  components: { vueWaterfallEasy, galleryEdit },
  data() {
    return {
      imgUrl: '',
      readVisible: false,
      editVisible: false,
      isFirstLoad: true,
      busy: false,
      dataList: [],
      pageIndex: 1,
      pageSize: 15,
      total: 0,
      loading: false,
      controllerName: 'Gallery',
      baseUrl: sp.getServerUrl(),
      buttons: [{ name: 'new', icon: 'plus', operate: () => (this.editVisible = true) }, { name: 'search' }]
    };
  },
  computed: {
    customApi() {
      return `api/${this.controllerName}/GetViewData?pageIndex=$pageIndex&pagesize=$pageSize&orderBy=&searchValue=&searchList=&viewId=`;
    }
  },
  created() {
    this.loadData();
  },
  methods: {
    showModal(event, { index, value }) {
      // 阻止a标签跳转
      event.preventDefault();
      this.imgUrl = '';
      this.imgUrl = value.infoUrl;
      this.readVisible = true;
    },
    downloadImg() {
      window.open(this.imgUrl, '_blank');
    },
    loadData() {
      if (this.loading) {
        return;
      }
      this.loading = true;

      if (this.pageSize * this.pageIndex >= this.total && !this.isFirstLoad) {
        return;
      }

      this.busy = true;
      if (!this.isFirstLoad) {
        this.pageIndex += 1;
      }
      sp.get(this.customApi.replace('$pageSize', this.pageSize).replace('$pageIndex', this.pageIndex))
        .then(resp => {
          this.dataList = this.dataList.concat(
            resp.DataList.map(item => {
              return {
                src: this.baseUrl + item.preview_url,
                name: item.name,
                infoUrl: this.baseUrl + item.image_url
              };
            })
          );
          this.total = resp.RecordCount;
          this.isFirstLoad = false;
          this.busy = false;
        })
        .finally(() => {
          this.loading = false;
        });
    }
  }
};
</script>

<style lang="less" scoped>
/deep/ .vue-waterfall-easy-scroll {
  position: initial !important;
  max-width: 100% !important;
}

/deep/ .vue-waterfall-easy {
  max-width: 100% !important;
  width: calc(100% - 60px) !important;
}

.big-image {
  width: 100%;
  height: 100%;
  max-width: 800px;
  max-height: 600px;
}
</style>
