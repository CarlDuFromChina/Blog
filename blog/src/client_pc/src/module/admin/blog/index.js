export default [{
  path: '/blogReadonly/:id?',
  name: 'blogReadonly',
  component: () => import('./blogReadonly')
},
{
  path: '/blogEdit/:id?',
  name: 'blogEdit',
  component: () => import('./blogEdit')
}];
