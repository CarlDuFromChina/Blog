<template>
  <div class="archive" v-infinite-scroll="loadMore">
    <el-timeline>
      <el-timeline-item v-for="item in data" :key="item.Id" :timestamp="$moment(item.createdOn).format('YYYY-MM-DD HH:mm')" placement="top">
        <el-card class="blogCard" shadow="hover" @click.native.stop="goReadonly(item)">
          <h4>{{ item.title }}</h4>
          <p>{{ item.modifiedByName + ' 提交于 ' + $moment(item.createdOn).format('YYYY-MM-DD HH:mm') }}</p>
        </el-card>
      </el-timeline-item>
    </el-timeline>
    <p v-if="loading" v-loading="loading" element-loading-text="拼命加载中" element-loading-spinner="el-icon-loading"></p>
    <span v-if="noMore" class="noMore">没有更多了...</span>
  </div>
</template>

<script>
export default {
  name: 'archive',
  data() {
    return {
      pageSize: 10,
      pageIndex: 1,
      totalRecords: 0,
      loading: false,
      data: [],
      activities: [
        {
          content: '我和js',
          timestamp: '2020-04-15'
        },
        {
          content: '我和js',
          timestamp: '2019-04-13'
        },
        {
          content: '我和js',
          timestamp: '2018-04-11'
        }
      ]
    };
  },
  created() {
    this.fetchData();
  },
  beforeDestroy() {
    this.$bus.$off('load-more');
  },
  computed: {
    noMore() {
      return this.pageSize * this.pageIndex >= this.totalRecords;
    }
  },
  methods: {
    goReadonly(row) {
      if (!sp.isNullOrEmpty(row.Id)) {
        this.$router.push({
          name: 'blogReadonly',
          params: { id: row.Id }
        });
      } else {
        this.$message.error('查看失败');
      }
    },
    loadMore() {
      if (!this.noMore) {
        this.pageSize += 10;
        this.fetchData();
      }
    },
    fetchData() {
      if (this.loading) {
        return;
      }
      this.loading = true;
      sp.get(`api/blog2/GetDataList?orderBy=createdon desc&pageSize=${this.pageSize}&pageIndex=${this.pageIndex}&searchList=`)
        .then(resp => {
          this.totalRecords = resp.RecordCount;
          this.data = resp.DataList;
        })
        .catch(() => this.$message.error('加载出错了'))
        .finally(() => {
          setTimeout(() => {
            this.loading = false;
          }, 200);
        });
    }
  }
};
</script>

<style lang="less" scoped>
.archive {
  width: 100%;
  background: #fff;
  /deep/ .el-timeline {
    padding: 20px;
    .el-timeline-item {
      font-size: 20px;
    }
  }
  .noMore {
    display: inline-block;
    width: 100%;
    text-align: center;
    padding-bottom: 10px;
    font-size: 15px;
  }
}
/deep/.el-card.is-hover-shadow:hover {
  box-shadow: 0 2px 12px 0 #6750d7;
}
</style>
