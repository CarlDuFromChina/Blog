export default [
  {
    name: 'blog-list',
    path: '/blogList',
    component: () => import('./blogList')
  },
  {
    name: 'blog',
    path: '/blog/:id',
    component: () => import('./blogReadonly')
  }
];
