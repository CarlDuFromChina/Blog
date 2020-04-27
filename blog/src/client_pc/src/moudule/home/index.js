import js from './blog/js';
import csharp from './blog/csharp';
import blog from './blog';
import sysmenu from './sysmenu';
import sysEntity from './sysEntity';

export default [
  {
    path: '/home',
    name: 'home',
    component: () => import('./home'),
    children: [].concat(js, csharp, sysmenu, sysEntity)
  }
].concat(blog);
