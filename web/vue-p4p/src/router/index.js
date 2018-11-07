import Vue from 'vue'
import Router from 'vue-router'
import Home from '@/views/Home.vue'
import Forbidden from '@/views/Forbidden'
import Login from '@/views/Login'
import DeathLine from '@/views/DeathLine'
import ManageDepartment from '@/views/ManageDepartment'
import store from '@/store/index'

Vue.use(Router)

const authMiddleware = (to, from, next) => {
  async function CheckAuthentication() {
    await store.dispatch["loginModule/setAuthenticationStore"];
  let isAdmin = store.getters['loginModule/isAdmin'];
  if(isAdmin)
    return next()
  else
    return next('/forbidden');
  } 
  CheckAuthentication();
}

export default new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home
    },
    {
      path:'/login',
      name:'login',
      component: Login
    },
    {
      path: '/forbidden',
      name: 'forbidden',
      component: Forbidden
    },
    {
      path:'/admin/deathline',
      name: 'deathline',
      component: DeathLine,
      beforeEnter: authMiddleware
    },
    {
      path:'/admin/managedepartment',
      name: 'manageDepartment',
      component: ManageDepartment,
      beforeEnter: authMiddleware
    }
  ]
  
})
