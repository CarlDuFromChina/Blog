<template>
  <el-row>
    <el-col :span="10" v-for="(item, index) in data" :key="index" :offset="index > 0 ? 2 : 0">
      <el-card id="card" :body-style="{padding: '0px'}">
        <div style="padding: 14px; display: inline-block; ">
          <strong><span class="title">{{ item.title }}</span></strong>
          <p><span class="creator">{{ item.createdByName }}</span></p>
          <p><span class="date">{{ item.createdOn | moment('YYYY-MM-DD HH:MM') }}</span></p>
          <div class="bottom clearfix">            
            <el-button @click="goReadonly(item)" type="text" size="small" class="button">查看</el-button>
            <el-button @click="goEdit(item)" type="text" size="small" class="button">编辑</el-button>
          </div>
        </div>
        <div style="display: inline-block; float : right ; padding : 14px">
            <img src="https://shadow.elemecdn.com/app/element/hamburger.9cf7b091-55e9-11e9-a976-7f4d0b07eef6.png" class="image">
        </div>
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
      loading: false
    };
  },
  created() {
    this.loading = true;
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

<style>
  .card {
    width: 400px;
    height: 150px;
  }
  .title {
    font-size: 20px;
    color: black;
  }
  .creator {
    font-size: 15px;
    color: teal;
  }
  .date {
    font-size: 13px;
    color: #999;
  }
  .bottom {
    margin-top: 13px;
    line-height: 10px;
  }
  .button {
    padding: 0;
    font-size: 15px;
    float: initial;
  }
  .image {
    width: 150px;
    height: 150px;
    display: inline-block;  
  }
</style>
