import Vue from 'vue'
import Vuex from 'vuex'

import loginModule from '@/store/Modules/loginModule'
import deathLineModule from '@/store/Modules/deathLineModule'
Vue.use(Vuex)

export default new Vuex.Store({
  modules: {
    loginModule,
    deathLineModule,
  }
})
