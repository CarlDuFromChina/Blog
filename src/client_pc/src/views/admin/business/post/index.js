export default [
  {
    path: '/admin/postEdit',
    name: 'postEdit',
    component: () => import('./postEdit')
  },
  {
    path: '/admin/post/:category?',
    name: 'post-list',
    component: () => import('./postList'),
    meta: { title: '文章管理' }
  }
];
