export default [
  {
    path: '/admin/blogEdit/:id?',
    name: 'blogEdit',
    component: () => import('./blogEdit')
  },
  {
    path: '/admin/blogs',
    name: 'blogList',
    component: () => import('./blogList'),
    meta: { title: '文章管理' }
  }
];
