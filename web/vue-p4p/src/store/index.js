import Vue from 'vue'
import Vuex from 'vuex'

import loginModule from '@/store/Modules/loginModule'
import deathLineModule from '@/store/Modules/deathLineModule'
import departmentModule from '@/store/Modules/departmentModule'
Vue.use(Vuex)

export default new Vuex.Store({
  modules: {
    loginModule,
    deathLineModule,
    departmentModule,
  }
})
