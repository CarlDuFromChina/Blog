// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue';
import App from './App';
import VueRouter from 'vue-router';
import mouduleRouter from './moudule';
import spCompoents from 'sixpence.platform.pc.vue';

Vue.config.productionTip = false;

Vue.use(spCompoents);

Vue.use(VueRouter);
const router = new VueRouter({
  routes: [({ // 顶层
    path: '/',
    component: App,
    children: mouduleRouter
  })]
});

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  components: { App },
  template: '<App/>'
});
