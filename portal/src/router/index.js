import Vue from 'vue';
import App from '../App.vue';
import VueRouter from 'vue-router';
import NProgress from 'nprogress';
import 'nprogress/nprogress.css';

Vue.use(VueRouter);

const router = new VueRouter({
  mode: 'history',
  routes: [
    {
      // 顶层
      path: '/',
      component: App,
      children: [
        {
          path: '/index',
          name: 'index',
          component: () => import('@/views/index/index.vue'),
          redirect: '/index/home',
          children: [
            {
              path: '/index/categories',
              name: 'categories',
              component: () => import('@/views/index/categories.vue'),
              meta: { title: '文章分类', keepAlive: true },
            },
            {
              path: '/index/home',
              name: 'home',
              component: () => import('@/views/index/home'),
              meta: { title: '主页', keepAlive: true }
            },
            {
              path: '/index/reading',
              name: 'reading',
              component: () => import('@/views/index/reading'),
              meta: { title: '读书笔记' }
            },
            {
              path: '/index/guidelines',
              name: 'guidelines',
              component: () => import('@/views/index/guidelines'),
              meta: { title: '用户协议' }
            }
          ]
        },
        {
          path: '/post/:id',
          name: 'post',
          component: () => import('@/views/post.vue')
        },
        {
          path: '/reading-note/:id',
          name: 'readingNote',
          component: () => import('@/views/readingNote.vue')
        }
      ],
      redirect: 'index'
    },
    {
      path: '/*',
      name: '404',
      component: () => import('@/views/404.vue')
    }
  ]
});

NProgress.configure({ showSpinner: false });

router.beforeEach((to, from, next) => {
  NProgress.start();
  next();
});

router.afterEach(() => {
  NProgress.done();
});

export default router;
