<template>
  <el-row>
    <el-col :span="5" v-for="(item, index) in data" :key="index" style="min-width:400px;margin:10px">
      <el-card class="blogCard" shadow="hover" @click.native.stop="goReadonly(item)">
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
            <el-button @click.stop="deleteData(item)" type="text" size="small" class="button">删除</el-button>
            <el-button @click.stop="goEdit(item)" type="text" size="small" class="button">编辑</el-button>
          </div>
        </div>
        <div style="display: inline-block; float : right;">
          <img :src="`${baseUrl}/${item.imageSrc}`" class="image" />
        </div>
      </el-card>
    </el-col>
    <el-col :span="5" style="min-width:400px;margin:10px;">
      <el-card class="blogCard" style="text-align:center">
        <el-button icon="el-icon-plus" circle style="font-size:50px;margin-top:30px" @click="createData"></el-button>
      </el-card>
    </el-col>
  </el-row>
</template>

<script>
import { pagination } from 'sixpence.platform.pc.vue';

export default {
  name: 'spBlogCard',
  mixins: [pagination],
  props: {
    fetch: { type: Function }
  },
  data() {
    return {
      data: [],
      baseUrl: ''
    };
  },
  created() {
    this.baseUrl = localStorage.getItem('baseUrl');
    this.loadData();
  },
  computed: {
    buttons() {
      return [{ name: 'new', icon: 'el-icon-plus', operate: this.createData }];
    }
  },
  methods: {
    loadData() {
      this.$emit('loading');
      this.fetch()
        .then(resp => {
          this.data = resp.DataList;
          this.total = resp.RecordCount;
        })
        .catch(error => this.$message.error(error))
        .finally(() => {
          this.$emit('loading-close');
        });
    },
    deleteData(item) {
      this.$confirm('此操作将永久删除该菜单, 是否继续?', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      })
        .then(() => {
          sp.post('api/Blog/DeleteData', [item.Id]).then(() => {
            this.$message.success('删除成功');
            this.loadData();
          });
        })
        .catch(() => {
          this.$message({
            type: 'info',
            message: '已取消删除'
          });
        });
    },
    createData() {
      this.$router.push({
        name: 'blogEdit'
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
            id: row.Id
          }
        });
      } else {
        this.$message.error('编辑失败');
      }
    }
  }
};
</script>

<style lang="less" scoped>
/deep/.el-card.is-hover-shadow:hover {
  box-shadow: 0 2px 12px 0 #6750d7;
}
</style>
