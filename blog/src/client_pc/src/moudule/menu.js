import 'sixpence.platform.pc.vue';
import NotFound from './notFound';
import system from './home/system';
import home from './home';
import { Loading } from 'element-ui';

async function loadRouter() {
  let routers = await sp.get('api/Blog/GetBlogRouterNameList');
  routers = routers.map(item => ({
    path: `/home/${item}`,
    name: item,
    component: () => import('./home/blog/blogList')
  }));
  return [].concat(routers).concat(system.map(item => item[0]));
}

function register(_router) {
  const loading = Loading.service({
    lock: true,
    text: '加载菜单信息',
    spinner: 'el-icon-loading'
  });
  loadRouter().then(resp => {
    _router.selfaddRoutes = params => {
      _router.addRoutes(params);
    };
    home[0].children = resp;
    _router.selfaddRoutes([home[0]]);
    _router.addRoutes([{ path: '*', component: NotFound }]);
  });
  setTimeout(() => {
    loading.close();
  }, 2000);
}

export default {
  register: register
};
