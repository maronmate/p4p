<template>
    <div>
        <b-navbar toggleable="md"  type="dark" variant="info">
            <b-navbar-toggle target="nav_collapse"></b-navbar-toggle>
            <b-navbar-brand href="#">P4P</b-navbar-brand>
            <b-collapse is-nav id="nav_collapse">
                <b-navbar-nav >
                     <b-nav-item v-if=loginUser href="#"><router-link to="/">ลงคะแนน</router-link> </b-nav-item>
                      <b-nav-item v-if=loginUser href="#"><router-link to="/">รวมคะแนน</router-link> </b-nav-item>
                      <b-nav-item-dropdown v-if=isAdmin>
                        <template slot="button-content">
                            <a>Administrator</a>
                        </template>
                    <b-dropdown-item href="#"><router-link to="/admin/deathline">Death line</router-link></b-dropdown-item>
                    <b-dropdown-item href="#"><router-link to="/admin/managetype">จัดการบุคคล</router-link></b-dropdown-item>
                    <b-dropdown-item href="#"><router-link to="/admin/managepriority">จัดการแผนก</router-link></b-dropdown-item>
                    <b-dropdown-item href="#"><router-link to="/admin/managepriority">จัดการหน่วยงาน</router-link></b-dropdown-item>
                    <b-dropdown-item href="#"><router-link to="/admin/managepriority">จัดการตำแหน่ง</router-link></b-dropdown-item>
                </b-nav-item-dropdown>
                </b-navbar-nav>

                <!-- Right aligned nav items -->
                <b-navbar-nav class="ml-auto">
                    <b-nav-item v-if=!loginUser href="#"><router-link to="/login">ลงชื่อเข้าใช้</router-link> </b-nav-item>
                    <b-nav-item-dropdown v-if=loginUser  right>
                        <template slot="button-content" >
                             <a>{{loginName}}</a>
                        </template>
                        <b-dropdown-item @click="logOut">ออกจากระบบ</b-dropdown-item>
                    </b-nav-item-dropdown>
                    
                </b-navbar-nav>
            </b-collapse>
        </b-navbar>
        <slot></slot>
    </div>    
</template>

<style scoped>
nav a {
  color: #4e4e4ebd !important;
}
a{
    color: #4e4e4ebd !important;
}
nav a.router-link-exact-active {
  color: #777777 !important;
}
.bg-info
{
    background-color: #eeeeee !important;
    height: 60px;
}
ul li a {
    color: #4e4e4ebd !important;
  }
  ul li ::after{
    color: #4e4e4ebd !important;
  }
  ul li a:hover {
  color: #cccccc !important;
}

.navbar-brand
{
    color: #555555 !important;
    margin-left: 30px;
}
</style>

<script>
import { mapGetters } from "vuex";

export default {
  data: () => ({
    title: "Nav template"
  }),
  methods: {
    logOut: function() {
        this.$store.dispatch("loginModule/logout");
        this.$router.push({ path: "/login" });
    }
  },
  computed: {
    ...mapGetters({
      loginUser: "loginModule/loginUser",
      isAdmin:'loginModule/isAdmin',
      loginName:'loginModule/loginName'
    })
  }
};
</script>