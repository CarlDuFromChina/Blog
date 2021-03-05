import App from './App';
import Vue from 'vue';
import VueRouter from 'vue-router';
import moduleRouter from './module';
import { myAdmin, adminRouter } from './module/admin';
import platform from 'vue-pc-admin';
import './components';
import 'mavon-editor/dist/css/index.css';
import './assets/icons';
import './style/index.less';
import './directives';
import storage from 'web-storage';
import 'current-device';

Vue.config.productionTip = false;
Vue.use(platform.install);
Vue.prototype.$indexDB = new storage.IndexedDB();

// 合并平台路由
let routes = platform.router.options.routes;
routes = moduleRouter.concat(routes);

routes.forEach(item => {
  if (item.name === 'admin') {
    item.component = myAdmin;
    item.children = item.children.concat(adminRouter);
  }
});
const router = new VueRouter({
  routes: [
    {
      // 顶层
      path: '/',
      component: App,
      children: routes
    }
  ]
});

const store = platform.store;

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
