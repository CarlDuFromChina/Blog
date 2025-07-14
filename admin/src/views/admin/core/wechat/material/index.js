export default [
  {
    name: 'material',
    path: '/admin/material',
    component: () => import('./materialList'),
    meta: { title: '素材管理' }
  }
];
