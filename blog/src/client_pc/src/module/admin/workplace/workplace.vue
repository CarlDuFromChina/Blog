<template>
  <div>
    <div class="header">
      <div class="header-avatar">
        <a-avatar :size="64" src="http://karldu.cn//api/SysFile/Download?objectId=13c5929e-cfca-406b-979b-d7a102a7ed10" />
      </div>
      <div class="header-content">
        <div class="header-content-title">{{ welcome }}，{{ user_info.name }}</div>
        <div>{{ user_info.introduction }}</div>
      </div>
    </div>
    <div class="divide"></div>
    <div class="content">
      <div class="section">
        <p class="section-title">贡献</p>
        <div id="calendar" style="width: 100%; height: 400px"></div>
      </div>
      <div class="section">
        <p class="section-title">我的动态</p>
        <a-timeline>
          <a-timeline-item v-for="(item, index) in timeline" :key="index"
            >{{ item.createdon | moment('YYYY-MM-DD') }}{{ ' 发表了：' + item.title }}</a-timeline-item
          >
        </a-timeline>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'workplace',
  data() {
    return {
      user_info: {},
      timeline: []
    };
  },
  created() {
    this.getUserInfo();
    this.loadTimeline();
  },
  mounted() {
    this.loadCalendar();
  },
  computed: {
    welcome() {
      const hour = this.$moment().hours();
      if (hour > 0 && hour <= 9) {
        return '早上好';
      } else if (hour > 9 && hour <= 11) {
        return '上午好';
      } else if (hour > 11 && hour <= 12) {
        return '中午好';
      } else if (hour > 12 && hour <= 17) {
        return '下午好';
      } else if (hour > 17 && hour <= 24) {
        return '晚上好';
      }
    }
  },
  methods: {
    getUserInfo() {
      sp.get(`api/UserInfo/GetData?id=${sp.getUser()}`).then(resp => {
        this.user_info = resp;
      });
    },
    getVirtulData(year, activityData) {
      year = year || '2017';
      var date = +this.$echarts.number.parseDate(year + '-01-01');
      var end = +this.$echarts.number.parseDate(year + '-12-31');
      var dayTime = 3600 * 24 * 1000;
      var data = [];
      for (var time = date; time <= end; time += dayTime) {
        const key = this.$echarts.format.formatTime('yyyy-MM-dd', time);
        data.push([key, (activityData.find(e => e.created_date === key) || {}).count || 0]);
      }
      return data;
    },
    async loadCalendar() {
      const el = document.getElementById('calendar');
      const activityData = await sp.get('api/Blog/GetActivity');
      const myChart = this.$echarts.init(el);
      var option = {
        visualMap: {
          show: false,
          min: 0,
          max: 5
        },
        calendar: {
          range: this.$moment()
            .year()
            .toString()
        },
        series: {
          type: 'heatmap',
          coordinateSystem: 'calendar',
          data: this.getVirtulData(this.$moment().year(), activityData)
        }
      };
      myChart.setOption(option);
    },
    async loadTimeline() {
      this.timeline = await sp.get('api/Analysis/GetTimeline');
    }
  }
};
</script>

<style lang="less" scoped>
.header {
  padding: 20px;
  display: flex;
  &-avatar {
    flex: 0 1 72px;
  }
  &-content {
    position: relative;
    top: 4px;
    -webkit-box-flex: 1;
    -ms-flex: 1 1 auto;
    flex: 1 1 auto;
    margin-left: 24px;
    color: rgba(0, 0, 0, 0.45);
    line-height: 22px;
    &-title {
      margin-bottom: 6px;
      color: rgba(0, 0, 0, 0.85);
      font-weight: 500;
      font-size: 20px;
      line-height: 28px;
    }
  }
}

.divide {
  background: #f0f2f5;
  height: 25px;
}

.section {
  padding: 24px;
  &-title {
    font-size: 24px;
  }
}
</style>
