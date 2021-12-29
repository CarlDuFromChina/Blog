export default [
  {
    path: '/admin/fileManage',
    name: 'fileManage',
    component: () => import('./fileManage'),
    meta: { title: '文件列表' }
  }
];
