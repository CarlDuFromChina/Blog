<template>
  <div :style="{ height: '100%', overflowY: 'auto' }">
    <div class="header">
      <div class="header-avatar">
        <a-avatar :size="64" :src="avatarUrl" />
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
        <div id="calendar" style="width: 100%; height: 200px"></div>
      </div>
      <div class="section">
        <p class="section-title">我的动态</p>
        <a-timeline>
          <a-timeline-item v-for="(item, index) in timeline" :key="index"
            >{{ item.created_at | moment('YYYY-MM-DD') }}{{ ' 发表了：' + item.title }}</a-timeline-item
          >
        </a-timeline>
      </div>
    </div>
  </div>
</template>

<script>
import * as echarts from 'echarts';

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
    this.avatarUrl = `${sp.getServerUrl()}api/system/avatar/${sp.getUserId()}`;
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
      return '';
    }
  },
  methods: {
    getUserInfo() {
      sp.get(`api/user_info/${sp.getUserId()}`).then(resp => {
        this.user_info = resp;
      });
    },
    getVirtulData(year, activityData) {
      year = year || '2017';
      var date = +echarts.number.parseDate(year + '-01-01');
      var end = +echarts.number.parseDate(year + '-12-31');
      var dayTime = 3600 * 24 * 1000;
      var data = [];
      for (var time = date; time <= end; time += dayTime) {
        const key = echarts.format.formatTime('yyyy-MM-dd', time);
        data.push([key, (activityData.find(e => e.created_date === key) || {}).count || 0]);
      }
      return data;
    },
    async loadCalendar() {
      const el = document.getElementById('calendar');
      const activityData = await sp.get('api/post/activity_records');
      const myChart = echarts.init(el);
      var option = {
        visualMap: {
          show: false,
          min: 0,
          max: 5,
          inRange: {
            color: ['#ebedf0', '#c6e48b', '#7bc96f', '#239a3b', '#196027']
          }
        },
        backgroundColor: '#fff',
        calendar: {
          cellSize: [14, 14],
          itemStyle: {
            borderColor: '#fff',
            borderWidth: 4
          },
          splitLine: {
            show: false
          },
          yearLabel: { show: false },
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
      this.timeline = await sp.get('api/analysis/timeline');
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
