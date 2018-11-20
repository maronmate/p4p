<template>
    <nav-layout showTitle=true titleText="จัดการผู้ใช้" showDescription=true descriptionText="เพื่อใช้เข้าสู่ระบบ">
        <div class="backgroundp4p">         
            <div class="container">
                <div class = "detail-block" v-show="showEdit">
                      <div class="form-group">
                        <b-row align-v="start" align-h="start" class="mb-2" >
                        <b-col cols="5" class="text-right">
                            <b-row align-v="center" align-h="start" class="mb-4" >
                            </b-row>
                        <b-row align-v="center" align-h="start" class="mb-2" >
                           <b-col cols="4" class="text-right">
                                <div>ชื่อ :</div>
                            </b-col>
                            <b-col cols="8">
                                <input v-model="Name" class="form-control">
                            </b-col>                       
                        </b-row>
                        <b-row align-v="center" align-h="start" class="mb-2" >
                            <b-col cols="4" class="text-right">
                                <div>ชื่อผู้ใช้ :</div>
                            </b-col>
                            <b-col cols="8">
                                <input v-model="Username" class="form-control">
                            </b-col>                   
                        </b-row>  
                        <b-row align-v="center" align-h="start" class="mb-2" >
                            <b-col cols="4" class="text-right">
                                <div>รหัสผ่าน :</div>
                            </b-col>
                            <b-col cols="8">
                                <b-input-group>
                                    <b-form-input :disabled="!(EditPassword || IsNew)" type="password" v-model="Password"  />
                                    <b-input-group-append>
                                        <b-btn :disabled="IsNew" @click="EditPasswordClick">แก้ไข</b-btn>
                                    </b-input-group-append>
                                </b-input-group>
                            </b-col>                     
                        </b-row>  
                    
                        </b-col>
                         <b-col cols="1" class="text-right"> </b-col>
     
                        <b-col cols="5" class="text-left">
                            <b-row align-v="center" align-h="center" class="mb-4" >
                            </b-row>
                            <b-row  align-h="center" class="mb-2" >
                                 <b-col cols="8">
                                    <b-form-group label="Administrator จะสามารถเข้าถึงได้ทุกแผนก">
                                        <b-form-checkbox id="chkIsAdmin"
                                        class="float-left"
                                        v-model="IsAdmin"
                                        value= true
                                        unchecked-value= false
                                        @change="AdminClick">  

                                        Administrator  
                                        </b-form-checkbox>
                                    </b-form-group>
                                </b-col>
                            </b-row>  

                            <b-row  align-h="center" class="mb-2" >
                                <b-col cols="8" class="text-left" >
                                    <b-form-group label="เลือกการเข้าถึงแผนก" >
                                        <b-form-checkbox-group stacked :disabled ="DisabledBox"  v-model="DepartmentIdList" name="department" :options="cbDepartment">
                                        </b-form-checkbox-group>
                                    </b-form-group>
                                </b-col>     
                            </b-row>                                                 
                        
                        </b-col>
                        </b-row>
                        <b-row  align-h="center" class="mt-1">
                             <b-col cols="10">
                                <b-alert variant="danger"
                            dismissible
                            :show="showSaveAlert"
                            @dismissed="CloseAlert">
                            {{saveResultMsg}}
                            </b-alert>
                            </b-col>
                        </b-row>
                        <b-row align-v="center" align-h="center" class="mb-2">
                            <b-button class="button-ok m-1" v-on:click="SaveUserLogin">บันทึก</b-button>
                            <b-button class="button-cancel m-1" v-on:click="ClosedUserLogin">ยกเลิก</b-button>
                        </b-row>      
                      </div>
                </div>
                <div class="background-table">
                     <b-row  align-h="end" align-v="center" >
                        <b-col md="4" class="my-1">
                            <b-form-group horizontal label="Filter" class="mb-0">
                                <b-input-group>
                                    <b-form-input v-model="filter" placeholder="Type to Search" />
                                    <b-input-group-append>
                                        <b-btn :disabled="!filter" @click="filter = ''">Clear</b-btn>
                                    </b-input-group-append>
                                </b-input-group>
                            </b-form-group>
                        </b-col>
                        <b-col cols="6" >
                            </b-col>
                        <b-col cols="2" >
                            <b-button variant="success" class="button-ok mb-2 mt-2" v-show="!showEdit" v-on:click="AddUserLogin">เพิ่มผู้ใช้</b-button>
                         </b-col>
                    </b-row>
                    <b-table striped hover :items="loginUserList" :fields="TableHeader" :filter="filter">    
                        <template slot="Password" slot-scope="data">
                                <span>xxxxx</span>
                        </template> 
                        <template slot="IsAdmin" slot-scope="data">
                            <span class="text-yes" v-if="data.value">Yes</span>
                            <span class="text-no" v-if="!data.value">No</span>
                        </template>      
                        <template slot="action" slot-scope="row">
                            <b-button v-show="!showEdit" variant="success sm" class="button-ok mr-1" size="sm" @click.stop="EditUserLogin(row.item)">แก้ไข</b-button>
                            <b-button v-show="!showEdit" variant="danger sm" class="button-cancel mr-1" size="sm" @click="ShowCofirmBoxDialog(row.item)" v-b-modal.modalRef >ลบ</b-button>                    
                        </template>
                    </b-table>
                </div>
            </div>
        </div>
         <b-modal 
          id="modalRef"
                    ref="modalRef"
                    title="Confirm Remove"                
                    @ok="DeleteUserLogin">
                    {{this.MsgDelete}}             
            </b-modal>
    </nav-layout>
</template>
<script>
import { mapGetters } from 'vuex'
export default {
    name: 'ManageLoginUser',
     data() {
        return {
            IsNew : true,
             TableHeader: [ 
             {key:'LoginId',label:'ID',sortable: true}
            ,{key: 'Name',label:'ชื่อ',sortable: true}            
            ,{key: 'Username',label:'ชื่อผู้ใช้',sortable: true}
            ,{key: 'Password',label:'รหัสผ่าน',sortable: true}
            ,{key: 'IsAdmin',label:'administrator',sortable: true}
            ,{key: 'DisplayDepartment',label:'แผนก',sortable: false}
            ,{key: 'action',label:'' }
            ],
            RemoveItem : null,
            LoginId : null,
            Name : null,
            IsAdmin : false,
            Username:null,
            Password:null,
            DepartmentIdList:[],
            filter: null,
            EditPassword:false,
            DisabledBox : false,
        }
    },
    mounted() {
        this.initUserLogin();
    },
    methods:
    {
         AdminClick: function(e) 
        {
            if(e == "true")
            {
                this.DisabledBox = true;
                this.DepartmentIdList = [];
            }
            else
            {
                this.DisabledBox = false;
            }
        },
        
        EditPasswordClick()
        {
            this.EditPassword = !this.EditPassword;
            if(this.EditPassword === false)
            {
                this.Password = null;
            }
        },
        AddUserLogin()
        {
            this.LoginId = null;
            this.IsAdmin = false;
            this.Name=null;
            this.Username=null;
            this.Password=null;
            this.DepartmentIdList=[];
            this.EditPassword=false;
            this.IsNew = true;      
            this.DisabledBox = false;                
            this.ShowEditForm(true);
        },
        EditUserLogin(userlogin)
        {
            this.LoginId = userlogin.LoginId;
            this.IsAdmin = userlogin.IsAdmin;
            this.Name=userlogin.Name;
            this.Username=userlogin.Username;
            this.Password=null;
            this.EditPassword=false;
            this.DepartmentIdList=userlogin.DepartmentIdList;
            this.DisabledBox = false;     
            this.IsNew = false;                      
            this.ShowEditForm(true);
        },
        ClosedUserLogin()
        {
            this.ShowEditForm(false);
            this.LoginId = null;
            this.IsAdmin = false;
            this.Name=null;
            this.Username=null;
            this.Password=null;
            this.EditPassword=false;
            this.DisabledBox = false;     
            this.DepartmentIdList=[];  
            this.SetResultMsg(false,'');
        },

        SaveUserLogin()
        {
            if(this.Name != null && this.Username != null )
            {
                if(this.IsNew)
                {
                    if(this.Password != null)
                        this.AddNewUserLogin(this.Username,this.Password,this.IsAdmin,this.Name,this.DepartmentIdList);
                    else
                        this.SetResultMsg(true,'ยังไม่ได้ใส่รหัสผ่าน')

                }
                else
                {
                    if(this.EditPassword === false || (this.EditPassword === true && this.Password != null))
                        this.UpdateUserLogin(this.LoginId,this.Username,this.Password,this.IsAdmin,this.Name,this.DepartmentIdList);
                    else
                        this.SetResultMsg(true,'ยังไม่ได้ใส่รหัสผ่าน')
                }
            }
            else
            {
                
                this.SetResultMsg(true,'ยังลงข้อมูลไม่ครบ')
            }
          
        },
        AddNewUserLogin: function(username,password,isAdmin,name,departmentIdList)
        {
            this.$store.dispatch('loginUserModule/requestAddLoginUser',{
                    Username :username
                    ,Password :password  
                    ,IsAdmin :isAdmin 
                    ,Name : name
                    ,DepartmentIdList : departmentIdList               
            })
            
        },
        UpdateUserLogin: function(id,username,password,isAdmin,name,departmentIdList)
        {
               this.$store.dispatch('loginUserModule/requestUpdateLoginUser',{
                    LoginId :id
                    ,Username :username
                    ,Password :password  
                    ,IsAdmin :isAdmin 
                    ,Name : name
                    ,DepartmentIdList : departmentIdList              
            })     
        },
        DeleteUserLogin()
        {
             if(this.RemoveItem)
             {
                this.$store.dispatch('loginUserModule/requestDeleteLoginUser',{LoginId:this.RemoveItem.LoginId});
             }
        },

        initUserLogin()
        {
            this.loadDepartmentCheckBox()
            this.loadUserLogin()
        },
        loadDepartmentCheckBox()
        {
            this.$store.dispatch('loginUserModule/loadCheckBoxDepartment')
        },
        loadUserLogin()
        {
            this.$store.dispatch('loginUserModule/loadLoginUserList')
        },

        ShowCofirmBoxDialog:function(userlogin)
        {         
            this.RemoveItem =userlogin;
        },
        ShowEditForm(show)
        {
            this.$store.dispatch('loginUserModule/showEditForm',show)
        }, 
        SetResultMsg : function(show,msg)
        {
            this.$store.dispatch('loginUserModule/showErrorMessage',{show:show,message:msg})
        },  
        CloseAlert()
        {
            this.SetResultMsg(false,'');
        },
    },
    computed: 
    {
        ...mapGetters({
            loginUserList:'loginUserModule/loginUserList',
            showEdit: 'loginUserModule/showEdit',
            showSaveAlert: 'loginUserModule/showSaveAlert',
            saveResultMsg: 'loginUserModule/saveResultMsg',
            headerToken:'loginModule/header',
            loginId:'loginModule/loginId',
            cbDepartment:"loginUserModule/cbDepartment",
        }),
         MsgDelete()
        {
            if(this.RemoveItem!=null)
            {
                return "ต้องการลบ login '"+this.RemoveItem.Username+"' หรือไม่?";
            }
            else
                return "";
        }
    }
}
    
</script>
<style scoped>
.text-yes
{
    color: #057305a6;
    
}
.text-no
{
    color: #fd9595;
}
</style>