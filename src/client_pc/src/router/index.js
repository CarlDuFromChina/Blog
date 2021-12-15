import Vue from 'vue';
import App from '../App.vue';
import VueRouter from 'vue-router';
import routes from '../module';
import store from '../store';
import NProgress from 'nprogress';
import 'nprogress/nprogress.css';

Vue.use(VueRouter);

const router = new VueRouter({
  routes: [{
    // 顶层
    path: '/',
    component: App,
    children: routes,
    redirect: 'index'
  }]
});

NProgress.configure({ showSpinner: false });

const DEFAULT_TITLE = `Sixpence Blog`;

router.beforeEach((to, from, next) => {
  NProgress.start();
  if (to.meta.title) {
    document.title = to.meta.title;
  } else {
    if (document.title !== DEFAULT_TITLE) {
      document.title = DEFAULT_TITLE;
    }
  }
  if (to.matched.some(m => m.meta.auth)) {
    if (store.getters.isLoggedIn) {
      next();
    } else {
      next({ name: 'login' });
      NProgress.done();
    }
  } else {
    if (to.name === 'login' && store.getters.isLoggedIn) {
      next({ name: 'admin' });
      NProgress.done();
    } else {
      next();
    }
  }
});

router.afterEach(() => {
  NProgress.done();
});

export default router;
