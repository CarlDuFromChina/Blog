<template>
  <sp-view>
    <sp-content>
      <mt-search v-model="searchValue" placeholder="输入书名快速搜索"></mt-search>
      <div v-infinite-scroll="loadData" :infinite-scroll-disabled="loading" infinite-scroll-distance="10" class="list">
        <div v-for="row in list" :key="row.id" class="card item" @click="goReadonly(row.id)">
          <div class="avatar">
            <img :src="(row.big_surfaceid || '').toDownloadUrl()" alt="" />
          </div>
          <div class="content">
            <div class="title">{{ row.name }}</div>
            <div class="info">
              <span><sp-icon name="sp-blog-people" style="padding-right: 8px"></sp-icon>{{ row.created_by_name }}</span>
              <span style="float: right">{{ row.created_at | moment('YYYY-MM-DD') }}</span>
            </div>
          </div>
        </div>
      </div>
      <sp-error type="no-content" v-show="list.length === 0"></sp-error>
    </sp-content>
  </sp-view>
</template>

<script>
import loading from '../loading';

export default {
  name: 'reading-list',
  mixins: [loading],
  data() {
    return {
      searchValue: '',
      list: [],
      isLoadedAll: false,
      pageIndex: 1,
      pageSize: 10,
      total: 0
    };
  },
  watch: {
    searchValue() {
      this.init();
      this.$nextTick(() => {
        this.loadData();
      });
    }
  },
  methods: {
    init() {
      this.list = [];
      this.isLoadedAll = false;
      this.pageIndex = 1;
      this.pageSize = 10;
      this.total = 0;
    },
    goReadonly(id) {
      this.$router.push({ name: 'reading-readonly', params: { id: id } });
    },
    fetch() {
      return sp
        .get(
          `${sp.getServerUrl()}api/reading_note/search?searchValue=${this.searchValue}&orderBy=created_at desc&pageSize=${this.pageSize}&pageIndex=${
            this.pageIndex
          }&searchList=&viewId=03860DF4-0E9E-4330-80BF-6A1E9AC797A6`
        )
        .then(resp => {
          this.total = resp.RecordCount;
          this.list = this.list.concat(resp.DataList);
          this.isLoadedAll = this.pageSize * this.pageIndex >= this.total;
          this.pageIndex++;
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
