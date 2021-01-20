import spIcon from './spIcon';
import layout from './layout';

const components = [{ name: spIcon.name, component: spIcon }].concat(layout);

const install = _Vue => {
  components.forEach(item => {
    _Vue.component(item.name, item.component);
  });
};

export default install;
