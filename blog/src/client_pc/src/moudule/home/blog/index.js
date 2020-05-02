import js from './js';
import csharp from './csharp';
import css from './css';

export const blogRouter = [
  {
    path: '/blogReadonly/:id?',
    name: 'blogReadonly',
    component: () => import('./blogReadonly')
  },
  {
    path: '/blogEdit/:id?',
    name: 'blogEdit',
    component: () => import('./blogEdit')
  }
];

export const blogsRouter = [js, csharp, css];

export default { blogRouter, blogsRouter };
