import App from './App';
import moduleRouter from './module';
import { myAdmin, adminRouter } from './module/admin';
import platform from 'sixpence.platform.pc.vue';
import components from './components';
import 'mavon-editor/dist/css/index.css';
import './assets/icons';
import './style/index.less';
import './directives';

const Vue = require('vue');
const VueRouter = require('vue-router');
const Vuex = require('vuex');
const moment = require('moment');

Vue.config.productionTip = false;

const install = _Vue => {
  components.forEach(item => {
    _Vue.component(item.name, item.component);
  });
};

const antd = require('antd');
Vue.use(antd);
Vue.use(install);
Vue.use(platform.install);
Vue.use(Vuex);

Vue.prototype.$bus = new Vue();

Vue.prototype.$moment = moment; // 赋值使用
Vue.use(moment);
Vue.filter('moment', (data, formatStr) => (sp.isNullOrEmpty(data) ? '' : moment(data).format(formatStr)));

Vue.prototype.$bus = new Vue();

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
