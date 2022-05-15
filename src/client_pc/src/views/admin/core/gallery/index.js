export default [
  {
    path: '/admin/gallery',
    name: 'gallery',
    component: () => import('./gallery'),
    meta: { title: '图库' }
  }
];
