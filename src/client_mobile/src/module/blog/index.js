export default [
  {
    name: 'blog-list',
    path: '/index/blogList',
    component: () => import('./blogList')
  },
  {
    name: 'blog',
    path: '/index/blog/:id',
    component: () => import('./blogReadonly')
  }
];
