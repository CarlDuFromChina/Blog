<template>
  <div>
    <sp-header>
      <sp-button-list :buttons="buttons"></sp-button-list>
    </sp-header>
    <el-table :data="data" v-loading="loading" element-loading-text="拼命加载中" element-loading-spinner="el-icon-loading">
      <el-table-column prop="title" label="标题"> </el-table-column>
      <el-table-column prop="createdOn" label="日期" width="200">
        <template slot-scope="scope">
          <span>{{ scope.row.createdOn | moment('YYYY-MM-DD HH:MM') }}</span>
        </template>
      </el-table-column>
      <el-table-column prop="createdByName" label="作者" width="120"></el-table-column>
      <el-table-column fixed="right" label="操作" width="100">
        <template slot-scope="scope">
          <el-button @click="goReadonly(scope.row)" type="text" size="small">查看</el-button>
          <el-button @click="goEdit(scope.row)" type="text" size="small">编辑</el-button>
        </template>
      </el-table-column>
    </el-table>
  </div>
</template>

<script>
export default {
  name: 'spBlogTable',
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

<style></style>
