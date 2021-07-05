<template>
  <div>
    <el-table ref="table" :data="tableData" style="width: 100%" row-key="Id" @selection-change="handleSelectionChange">
      <el-table-column type="selection" width="55" v-if="allowSelect"></el-table-column>
      <el-table-column
        v-for="(column, index) in columns"
        :key="index"
        :label="column.label"
        :prop="column.prop"
        :width="column.width"
        :sortable="column.sortable ? 'custom' : false"
      >
        <template slot-scope="scope">
          <span v-if="index == 0">
            <a class="compute-span" href="javascript:;" @click.stop.prevent="handleClick(scope.row)">{{ scope.row[column.prop] }}</a>
          </span>
          <span v-else-if="column.type == 'date'">{{ scope.row[column.prop] | moment('YYYY-MM-DD') }}</span>
          <span v-else-if="column.type == 'datetime'">{{ scope.row[column.prop] | moment('YYYY-MM-DD HH:mm') }}</span>
          <span v-else>{{ scope.row[column.prop] }}</span>
        </template>
      </el-table-column>
      <slot />
    </el-table>
    <el-pagination
      v-if="pagination"
      background
      layout="prev, pager, next"
      @size-change="sizeChange"
      @current-change="currentPage"
      :current-page="pageIndex"
      :page-count="pageSize"
      :pager-count="pagerCount"
      :total="total"
    >
    </el-pagination>
  </div>
</template>

<script>
import pagination from '../mixins/pagination';

export default {
  name: 'sp-table',
  mixins: [pagination],
  props: {
    columns: {
      type: Array,
      default: () => []
    },
    fetchData: {
      type: Function,
      required: true
    },
    allowSelect: {
      type: Boolean,
      default: false
    },
    pagination: {
      type: Boolean,
      default: true
    }
  },
  data() {
    return {
      tableData: []
    };
  },
  created() {
    this.loadData();
  },
  methods: {
    loadData() {
      this.fetchData().then(resp => {
        this.tableData = resp;
      });
    },
    handleClick(row) {
      this.$emit('link-click', row);
    },
    handleSelectionChange(val) {
      this.$emit('selection-change', val);
    }
  }
};
</script>

<style lang="less" scoped>
.compute-span {
  color: #409eff;
  text-decoration: none;
}
.el-pagination {
  text-align: center;
  padding-top: 20px;
}
</style>
