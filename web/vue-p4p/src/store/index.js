import Vue from 'vue'
import Vuex from 'vuex'

import loginModule from '@/store/Modules/loginModule'
import deathLineModule from '@/store/Modules/deathLineModule'
import departmentModule from '@/store/Modules/departmentModule'
import positionModule from '@/store/Modules/positionModule'
import subDivisionModule from '@/store/Modules/subDivisionModule'
Vue.use(Vuex)

export default new Vuex.Store({
  modules: {
    loginModule,
    deathLineModule,
    departmentModule,
    positionModule,
    subDivisionModule,
  }
})
