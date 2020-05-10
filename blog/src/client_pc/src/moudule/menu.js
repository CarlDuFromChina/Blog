import 'sixpence.platform.pc.vue';
import NotFound from './notFound';
import system from './admin/system';
import admin from './admin';
import { Loading } from 'element-ui';

async function loadRouter() {
  let routers = await sp.get('api/Blog/GetBlogRouterNameList');
  routers = routers.map(item => ({
    path: `/admin/${item}`,
    name: item,
    component: () => import('./admin/blog/blogList')
  }));
  return [].concat(routers).concat(system.map(item => item[0]));
}

function register() {
  const loading = Loading.service({
    lock: true,
    text: '加载菜单信息',
    spinner: 'el-icon-loading'
  });
  loadRouter().then(resp => {
    this.router.selfaddRoutes = params => {
      this.router.addRoutes(params);
    };
    admin[0].children = resp;
    this.router.selfaddRoutes([admin[0]]);
    this.router.addRoutes([{ path: '*', component: NotFound }]);
  });
  setTimeout(() => {
    loading.close();
  }, 1000);
}

export default {
  register: register
};
