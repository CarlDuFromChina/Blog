// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue';
import App from './App';
import VueRouter from 'vue-router';
import moduleRouter from './moudule';
import spComponents from 'sixpence.platform.pc.vue';
import components from './components';
import moment from 'vue-moment';
import './assets/icons';
import './style/index.less';
import './directives';
import menus from './moudule/menu';

Vue.config.productionTip = false;

const install = _Vue => {
  components.forEach(item => {
    _Vue.component(item.name, item.component);
  });
};

Vue.use(install);
Vue.use(spComponents);
Vue.use(moment);

Vue.use(VueRouter);
const router = new VueRouter({
  routes: [
    {
      // 顶层
      path: '/',
      component: App,
      children: moduleRouter,
      redirect: 'home'
    }
  ]
});

sp = Object.assign(sp,
  { refreshRouter: menus.register }
);

// TODO: 验证身份
// sp.get('api/DataService/test').then(resp => {
//   sp.refreshRouter.call({ router });
// });

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  components: { App },
  template: '<App/>'
});
