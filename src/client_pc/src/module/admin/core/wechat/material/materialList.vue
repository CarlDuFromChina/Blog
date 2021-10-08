<template>
  <vue-waterfall-easy ref="waterfall" style="height: 100%" :imgsArr="data" @scrollReachBottom="loadData">
    <div slot="waterfall-head">
      <sp-header></sp-header>
    </div>
  </vue-waterfall-easy>
</template>

<script>
import vueWaterfallEasy from 'vue-waterfall-easy2';

export default {
  name: 'materialList',
  components: { vueWaterfallEasy },
  data() {
    return {
      isFirstLoad: true,
      busy: false,
      editVisible: false,
      data: [],
      pageIndex: 1,
      pageSize: 15,
      loading: false,
      controllerName: 'WeChatMaterial',
      operations: ['search', 'more'],
      columns: [
        { prop: 'name', label: '名称' },
        { prop: 'type', label: '类型' },
        { prop: 'createdOn', label: '创建时间', type: 'datetime' }
      ],
      materialList: [],
      searchData: {
        type: 'image'
      },
      baseUrl: sp.getServerUrl()
    };
  },
  created() {
    this.getSysParam();
    this.loadData();
  },
  computed: {
    customApi() {
      return `api/${this.controllerName}/GetViewData?pageIndex=$pageIndex&pagesize=$pageSize&orderBy=&searchValue=&searchList=${JSON.stringify(
        this.searchList
      )}`;
    },
    searchList() {
      return [
        {
          Name: 'type',
          Value: this.searchData.type
        }
      ];
    }
  },
  methods: {
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
          this.data = this.data.concat(
            resp.DataList.map(item => {
              return {
                src: this.baseUrl.trimEnd('/') + item.local_url,
                href: this.baseUrl.trimEnd('/') + item.local_url,
                info: item.name
              };
            })
          );
          this.total = resp.RecordCount;
          this.isFirstLoad = false;
          this.busy = false;
        })
        .finally(() => {
          this.loading = false;
          if (this.total === this.data.length) {
            this.$refs.waterfall.waterfallOver();
          }
        });
    },
    async getSysParam() {
      this.materialList = await sp.get('api/SysParamGroup/GetParams?code=wechat_material_type');
    }
  }
};
</script>

<style lang="less" scoped>
/deep/ .vue-waterfall-easy-scroll {
  max-width: 100% !important;
}

/deep/ .vue-waterfall-easy {
  max-width: 100% !important;
  width: calc(100% - 60px) !important;
}
</style>
