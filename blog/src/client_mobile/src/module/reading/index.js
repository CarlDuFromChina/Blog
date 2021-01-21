export default [{
  name: 'reading-list',
  path: '/index/readingList',
  component: () => import('./readingList')
}, {
  name: 'reading-readonly',
  path: '/index/reading/:id',
  component: () => import('./readingReadonly')
}];
