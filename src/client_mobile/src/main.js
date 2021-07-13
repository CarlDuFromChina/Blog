// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue';
import App from './App';
import router from './router';
import components from './components';
import MintUI from 'mint-ui';
import 'mint-ui/lib/style.css';
import './style/index.less';
import './assets/icons';
import moment from 'vue-moment';
import './lib/extension';
import './directives';
import store from './store';

Vue.use(moment);
Vue.use(MintUI);
Vue.use(components);
Vue.prototype.$bus = new Vue();
Vue.prototype.$message = MintUI.MessageBox;
Vue.config.productionTip = false;

const serverUrl = localStorage.getItem('server_url');
if (process.env.NODE_ENV === 'development' && !sp.isNullOrEmpty(serverUrl)) {
  store.commit('updateServerUrl', serverUrl);
  console.info('服务器地址修改为：' + serverUrl);
}

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  store,
  components: { App },
  template: '<App/>'
});
