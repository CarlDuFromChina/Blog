import spIcon from './spIcon';
import layout from './layout';
import spError from './spError';

const components = [
  { name: spIcon.name, component: spIcon },
  { name: spError.name, component: spError }
].concat(layout);

const install = _Vue => {
  components.forEach(item => {
    _Vue.component(item.name, item.component);
  });
};

export default install;
