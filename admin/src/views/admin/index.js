import business from './business';
import core from './core';
import myAdmin from './myAdmin';
import result from './result';

export default [
  {
    path: '/admin',
    name: 'admin',
    redirect: '/admin/workplace',
    component: myAdmin,
    children: [].concat(business, core, result),
    meta: { auth: true } // 需要检验
  }
];
