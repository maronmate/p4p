import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import BootstrapVue from 'bootstrap-vue'
import NavLayout from '@/layout/layout'
import * as readConfig  from '@/helper/index.js';
import numeral from 'numeral';

Vue.use(BootstrapVue)
Vue.use(require('vue-moment'));
Vue.use(require('numeral'));


  Vue.filter("formatNumber", function (value) {
    return numeral(value).format("0,0");
  });

import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'

Vue.component('nav-layout', NavLayout)
let api = readConfig.getApiHost();
let key = readConfig.getKeyLocal();
Vue.config.productionTip = false

new Vue({
  router,
  store,
  render: h => h(App),
  data:{apiurl:api,keyCode:key,backgroundColor: '#cccccc'}

}).$mount('#app')
