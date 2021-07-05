export default {
  data() {
    return {
      selectParamNameList: [],
      selectEntityNameList: [],
      selectDataList: {}
    };
  },
  created() {
    this.loadSelectDataList();
  },
  methods: {
    async loadSelectDataList() {
      const resp1 = await sp.get(`api/SysParamGroup/GetParamsList?code=${this.selectParamNameList.join(',')}`);
      this.selectParamNameList.forEach((item, index) => {
        this.$set(this.selectDataList, item, resp1[index]);
      });
      const resp2 = await sp.get(`api/SysParamGroup/GetEntitiyList?code=${this.selectEntityNameList.join(',')}`);
      this.selectEntityNameList.forEach((item, index) => {
        this.$set(this.selectDataList, item, resp2[index]);
      });
      if (this.loadSelectDataListComplete && typeof this.loadSelectDataListComplete === 'function') {
        this.loadSelectDataListComplete();
      }
    }
  }
};
