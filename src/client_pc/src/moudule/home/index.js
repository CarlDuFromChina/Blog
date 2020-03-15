import test from './test';
import test2 from './test2';

export default [
  {
    path: '/home',
    name: 'home',
    component: () => import('./home'),
    children: [].concat(test, test2)
  }
];
