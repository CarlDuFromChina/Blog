export default [
  {
    path: '/admin/postEdit/:id?',
    name: 'postEdit',
    component: () => import('./postEdit')
  },
  {
    path: '/admin/post/:classification?',
    name: 'post-list',
    component: () => import('./postList'),
    meta: { title: '文章管理' }
  }
];
