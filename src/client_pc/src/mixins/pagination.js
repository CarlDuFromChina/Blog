export default {
  data() {
    return {
      pageIndex: 1,
      pageSize: 20,
      pageCount: 5,
      total: 0
    };
  },
  methods: {
    goFirst() {
      this.pageIndex = 1;
      this.total = 0;
    },
    sizeChange(count) {
      this.pageSize = count;
    },
    currentPage(index) {
      this.pageIndex = index;
    }
  }
};
