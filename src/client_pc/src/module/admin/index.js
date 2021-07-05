import business from './business';
import core from './core';
import myAdmin from './myAdmin';

export default [
  {
    path: '/admin',
    name: 'admin',
    component: myAdmin,
    children: [].concat(business, core),
    meta: { auth: true } // 需要检验
  }
];
