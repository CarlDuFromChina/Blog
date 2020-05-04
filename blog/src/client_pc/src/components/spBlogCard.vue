<template>
  <el-row>
    <el-col :span="8" v-for="(item, index) in data" :key="index">
      <el-card class="blogCard" shadow="hover">
        <div style="display:inline-block;">
          <strong
            ><div class="blogCard-title">{{ item.title }}</div></strong
          >
          <p>
            <span class="creator">{{ item.createdByName }}</span>
          </p>
          <p>
            <span class="date">{{ item.createdOn | moment('YYYY-MM-DD HH:MM') }}</span>
          </p>
          <div class="clearfix">
            <el-button @click="goReadonly(item)" type="text" size="small" class="button">查看</el-button>
            <el-button @click="goEdit(item)" type="text" size="small" class="button">编辑</el-button>
          </div>
        </div>
        <div style="display: inline-block; float : right;">
          <img :src="`${baseUrl}/${item.imageSrc}`" class="image" />
        </div>
      </el-card>
    </el-col>
    <el-col :span="8">
      <el-card class="blogCard" style="text-align:center">
        <el-button icon="el-icon-plus" circle style="font-size:50px;margin-top:30px" @click="createData"></el-button>
      </el-card>
    </el-col>
  </el-row>
</template>

<script>
export default {
  name: 'spBlogCard',
  props: {
    fetch: { type: Function }
  },
  data() {
    return {
      data: [],
      baseUrl: '',
      loading: false
    };
  },
  created() {
    this.loading = true;
    this.baseUrl = window.localStorage.getItem('baseUrl');
    this.fetch()
      .then(resp => {
        this.data = resp;
      })
      .catch(error => this.$message.error(error))
      .finally(() =>
        setTimeout(() => {
          this.loading = false;
        }, 200)
      );
  },
  computed: {
    buttons() {
      return [{ name: 'new', icon: 'el-icon-plus', operate: this.createData }];
    }
  },
  methods: {
    createData() {
      this.$router.push({
        name: 'blogEdit',
        params: {
          Id: ''
        }
      });
    },
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
    goEdit(row) {
      if (!sp.isNullOrEmpty(row.Id)) {
        this.$router.push({
          name: 'blogEdit',
          params: {
            Id: row.Id
          }
        });
      } else {
        this.$message.error('编辑失败');
      }
    }
  }
};
</script>

<style lang="less">
.blogCard {
  display: inline-block;
  min-width: 400px;
  min-height: 180px;
  margin-top: 20px;
  .blogCard-title {
    font-size: 18px;
    color: black;
    text-overflow: ellipsis;
    max-width: 210px;
    overflow: hidden;
    white-space: nowrap;
  }
  .creator {
    font-size: 15px;
    color: teal;
  }
  .date {
    font-size: 15px;
    color: #999;
  }
  .button {
    padding: 0;
    font-size: 15px;
    float: initial;
  }
  .image {
    width: 140px;
    height: 140px;
    display: inline-block;
  }
}
</style>
