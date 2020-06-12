// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue';
import Vuex from 'vuex';
import VueBus from 'vue-bus';
import App from './App';
import VueRouter from 'vue-router';
import moduleRouter from './module';
import * as platform from 'sixpence.platform.pc.vue';
import components from './components';
import moment from 'vue-moment';
import './assets/icons';
import './style/index.less';
import './directives';
import menus from './module/menu';

Vue.config.productionTip = false;

const install = _Vue => {
  components.forEach(item => {
    _Vue.component(item.name, item.component);
  });
};

Vue.use(install);
Vue.use(platform.default.install);
Vue.use(moment);
Vue.use(VueBus);
Vue.use(Vuex);

Vue.use(VueRouter);
const router = platform.default.router;
router.options.routes.push([
  {
    // 顶层
    path: '/',
    component: App,
    children: moduleRouter,
    redirect: 'index/home'
  }
]);

sp = Object.assign(sp,
  { refreshRouter: menus.register }
);

sp.get('api/DataService/test').then(resp => {
  if (resp) {
    sp.refreshRouter.call({ router });
  }
});

Vue.use(Vuex);
const store = platform.default.store;
debugger;
/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  store,
  components: { App },
  template: '<App/>'
});
