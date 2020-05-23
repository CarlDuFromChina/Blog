export default [
  {
    path: '/index',
    name: 'index',
    component: () => import('./index.vue'),
    redirect: '/index/home',
    children: [{
      path: '/index/home',
      name: 'home',
      component: () => import('./home')
    }, {
      path: '/index/aboutme',
      name: 'aboutme',
      component: () => import('./aboutme')
    }, {
      path: '/index/archive',
      name: 'archive',
      component: () => import('./archive')
    }, {
      path: '/index/friends',
      name: 'friends',
      component: () => import('./friends')
    }]
  }
];
