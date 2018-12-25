<template>
    <div>
        <b-navbar toggleable="md"  type="dark" variant="info">
            <b-navbar-toggle target="nav_collapse"></b-navbar-toggle>
            <b-navbar-brand href="#">P4P</b-navbar-brand>
            <b-collapse is-nav id="nav_collapse">
                <b-navbar-nav >
                     <b-nav-item v-if=loginUser href="#"><router-link to="/pointlist">ลงคะแนน</router-link> </b-nav-item>
                      <b-nav-item v-if=loginUser href="#"><router-link to="/">รวมคะแนน</router-link> </b-nav-item>
                      <b-nav-item-dropdown v-if=isAdmin>
                        <template slot="button-content">
                            <a>Administrator</a>
                        </template>
                    <b-dropdown-item href="#"><router-link to="/admin/deathline">จัดการ Death line</router-link></b-dropdown-item>
                    <b-dropdown-item href="#"><router-link to="/admin/manageuser">จัดการบุคคล</router-link></b-dropdown-item>
                    <b-dropdown-item href="#"><router-link to="/admin/managedepartment">จัดการแผนก</router-link></b-dropdown-item>
                    <b-dropdown-item href="#"><router-link to="/admin/managesubdivision">จัดการหน่วยงาน</router-link></b-dropdown-item>
                    <b-dropdown-item href="#"><router-link to="/admin/manageposition">จัดการตำแหน่ง</router-link></b-dropdown-item>
                    <b-dropdown-item href="#"><router-link to="/admin/manageloginuser">จัดการผู้ใช้</router-link></b-dropdown-item>
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
             <div class="titel-block" v-if="showTitle">
                 <b-container fluid >
                    <b-row align-h="start" align-v="center">
                        <b-col>
                            <div class="titel-text">{{titleText}}</div> 
                            <div class="description-text" v-if="showDescription"> {{descriptionText}}</div> 
                        </b-col>
                    </b-row>
                 </b-container>
                
        </div>
        <div class="empty-block backgroundp4p"></div>
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
    background-color: #dddddd !important;
    height: 60px;
}
    .titel-block
    {
        background-color: #dddddd;
         height: 60px;
         color: #4e4e4ebd;
         
    }
    .titel-text
    {
       color: #4e4e4ebd;
       font-size: xx-large; 
       text-align: left;
       margin-left: 30px;
       float: left;
    }
    .description-text
    {
        color: #5f5f5f5f;
        font-size: large; 
        text-align: left;
        float: left;
        margin-left: 20px;
        margin-top: 13px;
         
    }
    .vertical-center {
        position: absolute;
        top: 50%;
        -ms-transform: translateY(-50%);
        transform: translateY(-50%);
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
  props: ["showTitle","titleText","showDescription","descriptionText"]
  ,
    mounted()
    {

    },
  methods: {
    logOut: function() {
        this.$store.dispatch("loginModule/logout");
        this.$router.push({ path: "/login" });
    },

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