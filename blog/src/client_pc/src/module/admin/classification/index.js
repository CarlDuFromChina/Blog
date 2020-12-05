export default [
  {
    path: '/admin/classification',
    name: 'classification',
    component: () => import('./classificationList'),
    meta: { title: '文章分类' }
  }
];
