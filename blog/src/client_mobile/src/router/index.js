import Vue from 'vue';
import Router from 'vue-router';
import routes from '../module';

Vue.use(Router);
const router = new Router({
  routes: routes
});

router.beforeEach((to, from, next) => {
  if (to.matched.length !== 0) {
    next();
  } else {
    next({ path: '/404' });
  }
});

export default router;
