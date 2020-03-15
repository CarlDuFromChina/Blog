<template>
  <el-table :data="tableData">
    <el-table-column
      prop="date"
      label="日期"
      width="140"
    ></el-table-column>
    <el-table-column
      prop="name"
      label="姓名"
      width="120"
    ></el-table-column>
    <el-table-column
      prop="address"
      label="地址"
    ></el-table-column>
  </el-table>
</template>

<script>
const item = {
  date: '2016-05-02',
  name: '王小虎',
  address: '上海市普陀区金沙江路 1518 弄'
};
export default {
  name: 'test',
  data() {
    return {
      tableData: Array(4).fill(item)
    };
  }
};
</script>

<style>
</style>
