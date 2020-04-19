// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue';
import App from './App';
import VueRouter from 'vue-router';
import mouduleRouter from './moudule';
import spComponents from 'sixpence.platform.pc.vue';
import moment from 'vue-moment';
import './assets/icons';

Vue.config.productionTip = false;

Vue.use(spComponents);
Vue.use(moment);

Vue.use(VueRouter);
const router = new VueRouter({
  routes: [
    {
      // 顶层
      path: '/',
      component: App,
      children: mouduleRouter
    }
  ]
});

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  components: { App },
  template: '<App/>'
});