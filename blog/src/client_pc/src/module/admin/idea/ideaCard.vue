<template>
  <div class="infinite-list">
    <div v-for="(item, index) in dataList" :key="index" class="infinite-list-item">
      <div class="profile">
        <div class="avatar"></div>
        <div class="user-info">
          <span>{{ item.createdByName }}</span>
          <span class="time">{{ item.createdOn | moment('YYYY-MM-DD HH:mm') }}</span>
        </div>
      </div>
      <div class="content">{{ item.content }}</div>
    </div>
  </div>
</template>

<script>
import { pagination } from 'sixpence.platform.pc.vue';

export default {
  name: 'ideaCard',
  mixins: [pagination],
  data() {
    return {
      dataList: [],
      controllerName: 'idea',
      pageIndex: 1,
      pageSize: 5
    };
  },
  created() {
    this.loadData();
  },
  methods: {
    loadData() {
      let url = `api/${this.controllerName}/GetDataList?searchList=&orderBy=createdon desc&pageSize=${this.pageSize}&pageIndex=${this.pageIndex}`;
      try {
        sp.get(url).then(resp => {
          if (resp && resp.DataList) {
            this.dataList = resp.DataList;
            this.total = resp.RecordCount;
          } else {
            this.dataList = resp;
          }
        });
      } catch (error) {
        this.$message.error(error);
      }
    }
  }
};
</script>

<style lang="less" scoped>
.infinite-list {
  height: 600px;
  .infinite-list-item {
    display: inline-block;
    min-height: 120px;
    width: 100%;
    background: #fff;
    color: #17181a;
    margin: 10px 0px;
    .profile {
      display: flex;
      padding: 10px;
      background-color: #fff;
      font-size: 13px;
      .avatar {
        background-image: url('http://www.dumiaoxin.top:8002/temp/1B715131BA7631E818D1713D3E6766E541717022.png');
        width: 50px;
        height: 50px;
        border-radius: 50%;
        display: inline-block;
        position: relative;
        background-position: 50%;
        background-size: cover;
        background-repeat: no-repeat;
        background-color: #eee;
      }
      .user-info {
        display: flex;
        flex-direction: column;
        padding-left: 10px;
        background-color: #fff;
        font-size: 13px;
        > span {
          line-height: 28px;
          font-size: 15px;
          font-weight: 600;
          &.time {
            font-size: 13px;
            font-weight: 400;
            color: #8f969c;
          }
        }
      }
    }
    .content {
      padding: 0 20px 10px 20px;
      line-height: 1.6;
    }
  }
}
</style>
