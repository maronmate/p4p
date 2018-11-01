<template>
    <div class='backgroundp4p' >
        <b-container class="login-card">
            <b-card class="text-left">
                <b-card-header class="hearder-card">ลงชื่อเข้าใช้</b-card-header>
                <b-card-body>
                    <b-form @submit="onlogin">
                         <b-form-group id="Username"
                                        label="ชื่อผู้ใช้:"
                                        label-for="username">
                            <b-form-input id="username"
                                        type="text"
                                        v-model="input.username"
                                        required
                                        placeholder="ใส่ชื่อผู้ใช้ (username)">
                            </b-form-input>
                        </b-form-group>
                        <b-form-group id="Password"
                                        label="รหัสผ่าน:"
                                        label-for="password">
                            <b-form-input id="password"
                                        type="password"
                                        v-model="input.password"
                                        required
                                        placeholder="ใส่รหัสผ่าน (password)">
                            </b-form-input>
                        </b-form-group>
                         <b-alert variant="danger" dismissible :show="!loginPass">
                          ชื่อผู้ใช้ หรือ รหัสผ่าน ไม่ถูกต้อง.
                        </b-alert> 
                        
                         <b-button type="submit" size="sm" class="button-submit button-max">ลงชื่อเข้าใช้</b-button>      
                    </b-form>
                </b-card-body>
            </b-card>
        </b-container>
    </div>
</template>

<script>
import { mapGetters } from 'vuex'

export default {
    name: 'Login',
    data() {
    return {
      input:{    
        username:"",
        password:""
      },
    }
  },
  mounted() {
    
  },
   computed: 
    {
         ...mapGetters({
      loginUser: "loginModule/loginUser",
      loginMsg: "loginModule/loginMsg"
          }),
          loginPass()
          {
              if(this.loginMsg == null || this.loginMsg == '' )
              {
                  return true;
              }
              return false;
          }
    },
  methods: {
      async onlogin(evt) {
      evt.preventDefault();
      await this.$store.dispatch('loginModule/requestLogin',{username:this.input.username,password:this.input.password,keyCode:this.$root._data.keyCode}).then()
      {
            if(this.loginUser)
            {
                 this.$router.push({ path: "/" });
            }
            
      }
    },
  }
}
</script>
<style scoped>
    .login-card
    {
        position: absolute;
        width: 400px;
        top: 40%;
        left: 50%;
        margin-right: -50%;
        transform: translate(-50%, -50%);
    }
    .button-max
    {
        margin-top: 5px;
        height: 40px;
        width:100%;
    }
    button:hover {
        background-color: #dddddd !important
    }
</style>