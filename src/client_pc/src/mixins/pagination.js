export default {
  data() {
    return {
      pageIndex: 1,
      pageSize: 20,
      pagerCount: 5,
      total: 0
    };
  },
  methods: {
    sizeChange(count) {
      this.pageSize = count;
    },
    currentPage(index) {
      this.pageIndex = index;
    }
  }
};
