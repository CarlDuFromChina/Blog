import spHeader from './spHeader';
import spView from './spView';
import spContent from './spContent';

const components = [
  { name: spHeader.name, component: spHeader },
  { name: spView.name, component: spView },
  { name: spContent.name, component: spContent }
];

export default components;
