<template>
  <sp-view>
    <sp-header title="首页"></sp-header>
    <sp-content>
      <mt-search v-model="searchValue" placeholder="输入博客名快速搜索"></mt-search>
      <div v-infinite-scroll="loadMore" :infinite-scroll-disabled="loading" infinite-scroll-distance="10" class="list">
        <div v-for="(row, index) in list" :key="index" class="card item">
          <div class="avatar">
            <img :src="getDownloadUrl(row)" alt="" />
          </div>
          <div class="content">
            <div class="title">{{ row.title }}</div>
            <div class="info">
              <span><sp-icon name="sp-blog-view" style="padding-right: 8px"></sp-icon>{{ row.reading_times }}</span>
              <span style="float: right">{{ row.createdOn | moment('YYYY-MM-DD') }}</span>
            </div>
          </div>
        </div>
      </div>
    </sp-content>
  </sp-view>
</template>

<script>
export default {
  name: 'index',
  data() {
    return {
      searchValue: '',
      loading: false,
      list: [],
      isLoadedAll: false,
      pageIndex: 1,
      pageSize: 10,
      total: 0
    };
  },
  methods: {
    getDownloadUrl(item) {
      return `${sp.getBaseUrl()}${item.surface_url}`;
    },
    loadMore() {
      sp.get(
        `${sp.getBaseUrl()}api/blog/GetDataList?orderBy=createdon desc&pageSize=${this.pageSize}&pageIndex=${
          this.pageIndex
        }&searchList=&viewId=463BE7FE-5435-4841-A365-C9C946C0D655`
      ).then(resp => {
        this.total = resp.RecordCount;
        this.list = this.list.concat(resp.DataList);
        this.isLoadedAll = this.pageSize * this.pageIndex >= this.total;
        this.pageIndex++;
        this.loading = false;
      });
    }
  }
};
</script>

<style lang="less" scoped>
/deep/ .mint-search {
  height: auto;
}

.list {
  padding: 8px;
  .item {
    background: #fff;
    padding: 15px;
    margin-top: 10px;
  }
}
.card {
  display: flex;
  .avatar {
    border-radius: 4px;
    img {
      width: 104px;
      height: 104px;
      border-radius: 4px;
    }
  }
  .content {
    padding-left: 16px;
    width: 100%;
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    text-align: left;
    .title {
      display: inline-block;
      color: #222222;
      font-size: 16px;
      word-break: break-all;
    }
    .info {
      bottom: 30px;
      color: #888888;
      font-size: 12px;
      position: relative;
      bottom: 10px;
      .rt-icon {
        vertical-align: middle;
        padding-right: 3px;
      }
    }
  }
}
</style>
