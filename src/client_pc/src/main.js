import App from './App';
import Vue from 'vue';
import Vuex from 'vuex';
import Antd from 'ant-design-vue';
import router from './router';
import store from './store';
import './lib';
import './components';
import 'mavon-editor/dist/css/index.css';
import './assets/icons';
import './style/index.less';
import './directives';
import 'current-device';

const moment = require('moment');
const echarts = require('echarts');
Vue.config.productionTip = false;
Vue.use(Antd);
Vue.use(moment);
Vue.use(Vuex);

Vue.prototype.$bus = new Vue();
Vue.filter('moment', (data, formatStr) => (sp.isNullOrEmpty(data) ? '' : moment(data).format(formatStr)));
moment.locale('zh-cn');
Vue.prototype.$moment = moment;
Vue.prototype.$echarts = echarts;

// 如果是移动端则跳转到移动端应用
if (window.device.mobile()) {
  window.location.href = `${window.location.origin}/debug/#/`;
}

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  store,
  components: { App },
  template: '<App/>'
});
