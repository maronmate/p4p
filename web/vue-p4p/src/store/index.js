import Vue from 'vue'
import Vuex from 'vuex'

import loginModule from '@/store/Modules/loginModule'
import deathLineModule from '@/store/Modules/deathLineModule'
import departmentModule from '@/store/Modules/departmentModule'
import positionModule from '@/store/Modules/positionModule'
import subDivisionModule from '@/store/Modules/subDivisionModule'
import manageUserModule from '@/store/Modules/manageUserModule'
import loginUserModule from '@/store/Modules/loginUserModule'
import pointListModule from '@/store/Modules/pointListModule'
Vue.use(Vuex)

export default new Vuex.Store({
  modules: {
    loginModule,
    deathLineModule,
    departmentModule,
    positionModule,
    subDivisionModule,
    manageUserModule,
    loginUserModule,
    pointListModule
  }
})
