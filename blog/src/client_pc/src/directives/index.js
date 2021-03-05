import heightLight from './highlight';
import Vue from 'vue';

const directives = [heightLight];

directives.forEach(e => {
  Vue.directive(e.name, e);
});
