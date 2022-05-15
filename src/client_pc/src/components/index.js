import Vue from 'vue';
import register from './register';
import spHeader from './spHeader';
import spButtonList from './spButtonList';
import spIcon from './spIcon';
import spList from './spList';
import spSection from './spSection';
import spMenu from './spMenu';
import spMenuItem from './spMenuItem';
import spTag from './spTag';
import spEditor from './spEditor';
import spSelect from './spSelect';
import spSwitch from './spSwitch';
import spBlogCard from './spBlogCard';
import spComments from './spComments';
import spCard from './spCard';
import spLogin from './spLogin';
import cloudUpload from './cloudUploadDialog';

const components = [
  { name: register.name, component: register },
  { name: spHeader.name, component: spHeader },
  { name: spButtonList.name, component: spButtonList },
  { name: spIcon.name, component: spIcon },
  { name: spList.name, component: spList },
  { name: spSection.name, component: spSection },
  { name: spMenu.name, component: spMenu },
  { name: spMenuItem.name, component: spMenuItem },
  { name: spTag.name, component: spTag },
  { name: spEditor.name, component: spEditor },
  { name: spSelect.name, component: spSelect },
  { name: spSwitch.name, component: spSwitch },
  { name: spBlogCard.name, component: spBlogCard },
  { name: spComments.name, component: spComments },
  { name: spCard.name, component: spCard },
  { name: spLogin.name, component: spLogin },
  { name: cloudUpload.name, component: cloudUpload }
];

const install = _Vue => {
  components.forEach(item => {
    _Vue.component(item.name, item.component);
  });
};

Vue.use(install);
