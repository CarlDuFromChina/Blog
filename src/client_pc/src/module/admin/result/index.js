export default [
  {
    path: '/admin/403',
    name: '403',
    component: () => import('./403.vue')
  },
  {
    path: '/admin/500',
    name: '500',
    component: () => import('./500.vue')
  },
  {
    path: '/admin/*',
    name: '404',
    component: () => import('./404.vue')
  }
];
