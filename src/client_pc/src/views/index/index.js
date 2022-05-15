export default [
  {
    path: '/index',
    name: 'index',
    component: () => import('./index.vue'),
    redirect: '/index/home',
    children: [{
      path: '/index/categories',
      name: 'categories',
      component: () => import('./categories.vue')
    }].concat(
      {
        path: '/index/home',
        name: 'home',
        component: () => import('./home/home.vue')
      },
      {
        path: '/index/reading',
        name: 'reading',
        component: () => import('./reading')
      },
      {
        path: '/index/messageRemind',
        name: 'messageRemind',
        component: () => import('./messageRemind/index.vue')
      })
  }
];
