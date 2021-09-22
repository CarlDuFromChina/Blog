<template>
  <sp-card :loading="loading" class="series" title="精品文章" :empty="!listData || listData.length == 0">
    <a class="item" v-for="(item, index) in listData" :key="index" @click="read(item)">
      <div class="item-start">{{ item.title }}</div>
      <div class="item-end">
        <span><a-icon type="eye" style="margin-right: 8px" />{{ item.reading_times || 0 }}</span>
      </div>
    </a>
  </sp-card>
</template>

<script>
export default {
  name: 'seriesList',
  data() {
    return {
      listData: [],
      loading: false
    };
  },
  mounted() {
    this.loadData();
  },
  methods: {
    async loadData(value = '') {
      if (this.searchValue !== value) {
        this.searchValue = value;
      }
      if (this.loading) {
        return;
      }
      this.loading = true;
      try {
        let url = 'api/blog/GetViewData?orderBy=createdon desc';
        url += '&pageSize=5';
        url += '&pageIndex=1';
        url += '&searchList=';
        url += '&viewId=834F8083-47BC-42F3-A6B2-DE25BE755714';
        url += `&searchValue=${this.searchValue}`;
        const resp = await sp.get(url);
        if (resp) {
          this.listData = this.listData.concat(resp.DataList);
        }
      } catch (error) {
        this.$message.error(error);
      } finally {
        setTimeout(() => {
          this.loading = false;
        }, 200);
      }
    },
    read(item) {
      const { href } = this.$router.resolve({
        name: 'blogReadonly',
        params: { id: item.Id }
      });
      window.open(href, '_blank');
    }
  }
};
</script>

<style lang="less" scoped>
.series {
  /deep/ .ant-card-body {
    padding: 0px;
  }
  .item {
    display: flex;
    padding: 10px;
    cursor: pointer;
    align-items: center;
    justify-content: space-between;
    &-start {
      color: #000000;
    }
    &-end {
      color: #4a4a4a;
    }
  }
  .item:hover {
    background: hsla(0, 0%, 85.1%, 0.1);
  }
}
</style>
