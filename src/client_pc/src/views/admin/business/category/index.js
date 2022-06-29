export default [
  {
    path: '/admin/category',
    name: 'category',
    component: () => import('./categoryList'),
    meta: { title: '文章分类' }
  }
];
