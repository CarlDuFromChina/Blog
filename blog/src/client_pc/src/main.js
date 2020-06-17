import Vue from 'vue';
import Vuex from 'vuex';
import VueBus from 'vue-bus';
import App from './App';
import VueRouter from 'vue-router';
import moduleRouter from './module';
import admin from './module/admin';
import platform from 'sixpence.platform.pc.vue';
import components from './components';
import moment from 'vue-moment';
import './assets/icons';
import './style/index.less';
import './directives';

Vue.config.productionTip = false;

const install = _Vue => {
  components.forEach(item => {
    _Vue.component(item.name, item.component);
  });
};

Vue.use(install);
Vue.use(platform.install);
Vue.use(moment);
Vue.use(VueBus);
Vue.use(Vuex);

// 合并平台路由
let routes = platform.router.options.routes;
routes = routes.concat(moduleRouter);
routes.forEach(item => {
  if (item.name === 'admin') {
    item.children = item.children.concat(admin);
  }
});
const router = new VueRouter({
  routes: [
    {
      // 顶层
      path: '/',
      component: App,
      children: routes,
      redirect: 'index'
    }
  ]
});

Vue.use(Vuex);
const store = platform.store;
/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  store,
  components: { App },
  template: '<App/>'
});
