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

const originPush = Router.prototype.push;
Router.prototype.push = function push(location, onResolve, onReject) {
  if (onResolve || onReject) return originPush.call(this, location, onResolve, onReject);

  return originPush.call(this, location).catch(err => err);
};

export default router;
