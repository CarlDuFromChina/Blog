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

router.beforeEach((to, from, next) => {
  NProgress.start();
  if (to.matched.some(m => m.meta.auth)) {
    if (store.getters.isLoggedIn) {
      next();
    } else {
      next({ name: 'login' });
    }
  } else {
    if (to.name === 'login' && store.getters.isLoggedIn) {
      next({ name: 'admin' });
    } else {
      next();
    }
  }
});

router.afterEach(() => {
  NProgress.done();
});

export default router;
