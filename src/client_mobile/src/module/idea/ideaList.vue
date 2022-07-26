<template>
  <sp-view>
    <sp-content>
      <div v-infinite-scroll="loadData" :infinite-scroll-disabled="loading" infinite-scroll-distance="10">
        <div v-for="(item, index) in dataList" :key="index" class="list">
          <div class="list-item">
            <div style="color: #909399">{{ item.created_at | moment('YYYY-MM-DD') }}</div>
            <div v-html="item.content"></div>
          </div>
        </div>
      </div>
      <sp-error type="no-content" v-show="dataList.length === 0"></sp-error>
    </sp-content>
  </sp-view>
</template>

<script>
import loading from '../loading';

export default {
  name: 'idea-list',
  mixins: [loading],
  data() {
    return {
      dataList: [],
      pageIndex: 1,
      pageSize: 10,
      total: 0
    };
  },
  methods: {
    fetch() {
      return sp.get(`api/idea/search?searchList=&orderBy=created_at desc&pageSize=${this.pageSize}&pageIndex=${this.pageIndex}`).then(resp => {
        if (resp && resp.DataList) {
          this.dataList = resp.DataList;
          this.total = resp.RecordCount;
        } else {
          this.dataList = resp;
        }
      });
    }
  }
};
</script>

<style lang="less" scoped>
.list {
  &-item {
    text-align: left;
    margin: 20px;
    position: relative;
    /deep/ p {
      margin-top: 0px;
    }
    &::before {
      content: '';
      width: 11px;
      height: 11px;
      border-radius: 100%;
      background-color: #91c2fc;
      position: absolute;
      left: -15px;
    }
    &::after {
      content: '';
      width: 3px;
      height: calc(~'100% + 10px'); /*加上10px是item底部的margin*/
      background-color: #91c2fc;
      position: absolute;
      top: 10px;
      left: -11px;
    }
  }
}
</style>
