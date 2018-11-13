<template>
       <nav-layout showTitle=true titleText="จัดการบุคคล" showDescription=true descriptionText="สร้างบุคคลเพื่อนำไปลงข้อมูลคะแนน หรือ แสดงในรายงาน">
        <div class="backgroundp4p">         
            <div class="container detail-c">
                <div class = "detail-block" v-show="showEdit">
                    <div class="form-group">
                        <b-row align-v="center" align-h="start" class="mb-4" >
                        </b-row>
                        <b-row align-v="center" align-h="start" class="mb-2" >

                            <b-col cols="3" class="text-right">
                                <div>ชื่อ :</div>
                            </b-col>
                            <b-col cols="3">
                                <input v-model="Name" class="form-control">
                            </b-col> 
                            <b-col cols="2" class="text-right">
                                <div>นามสกุล :</div>
                            </b-col>
                            <b-col cols="3">
                                <input v-model="LastName" class="form-control">
                            </b-col>                       
                        </b-row>

                        <b-row align-v="center" align-h="start" class="mb-2" >

                            <b-col cols="3" class="text-right">
                                <div>วันเกิด :</div>
                            </b-col>
                            <b-col cols="3">
                                <datepicker v-model="BirthDate" name="birthDate" :bootstrap-styling="true" :language="th" :clear-button="true"></datepicker>
                            </b-col>

                            <b-col cols="2" class="text-right">
                                <div>วันที่เริ่มในรายงาน :</div>
                            </b-col>
                            <b-col cols="3">
                                <datepicker v-model="StartDate" name="startDate" :bootstrap-styling="true" :language="th"></datepicker>
                            </b-col>                         
                        </b-row>               
                        <b-row align-v="center" align-h="start" class="mb-2">

                            <b-col cols="3" class="text-right">
                                <div>เลือกแผนก :</div>
                            </b-col>
                            <b-col cols="3">
                                <b-form-select id="ddlDepartment"  v-model="SelectedDepartmentId" :options="ddlDepartment" @change="selectedDepartment"></b-form-select>
                            </b-col>
                        </b-row>

                        <b-row align-v="center" align-h="start" class="mb-2" >
                            <b-col cols="3" class="text-right">
                                <div>เลือกหน่วยงาน :</div>
                            </b-col>
                            <b-col cols="3">
                                <b-form-select id="ddlSubdivision"  v-model="SelectedSubdivisionId" :options="ddlSubDivision"></b-form-select>
                            </b-col>

                            <b-col cols="2" class="text-right">
                                <div>เลือกตำแหน่ง :</div>
                            </b-col>
                            <b-col cols="3">
                                <b-form-select id="ddlPosition"  v-model="SelectedPositionId" :options="ddlPosition"></b-form-select>
                            </b-col>                         
                        </b-row>

                        <b-row align-v="center" align-h="center" class="mb-2">
                            <b-form-checkbox id="chkEnabled"
                                class="float-left mt-2"
                                v-model="Enabled"
                                value=true
                                unchecked-value=false>                            
                            เปิดใช้งาน  
                            </b-form-checkbox>
                        </b-row>

                        <b-row  align-h="center" class="mt-3">
                             <b-col cols="10">
                                <b-alert variant="danger"
                            dismissible
                            :show="showSaveAlert"
                            @dismissed="CloseAlert">
                            {{saveResultMsg}}
                            </b-alert>
                            </b-col>
                        </b-row>
                        <b-row align-v="center" align-h="center" class="mb-3">
                            <b-button class="button-ok m-1" v-on:click="SaveUser">บันทึก</b-button>
                            <b-button class="button-cancel m-1" v-on:click="ClosedUser">ยกเลิก</b-button>
                        </b-row>                 
                    </div>
                    </div>
                </div>
            <div class="container">
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
                            <b-button variant="success" class="button-ok mb-2 mt-2" v-show="!showEdit" v-on:click="AddUser">เพิ่มบุคคล</b-button>
                         </b-col>
                    </b-row>
                    <b-table striped hover :items="userList" :fields="TableHeader" :filter="filter">     
                        <template slot="BirthDate" slot-scope="data" > 
                               
                            <span v-if="data.value">{{data.value|moment("DD/MM/YYYY")}}</span>
                        </template>    
                        <template slot="StartDate" slot-scope="data" >    
                            {{data.value|moment("DD/MM/YYYY")}}
                        </template>    
                        <template slot="Enabled" slot-scope="data">
                            <span class="text-yes" v-if="data.value">เปิด</span>
                            <span class="text-no" v-if="!data.value">ปิด</span>
                        </template>       
                        <template slot="action" slot-scope="row">
                            <b-button v-show="!showEdit" variant="success sm" class="button-ok mr-1" size="sm" @click.stop="EditUser(row.item)">แก้ไข</b-button>
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
                    @ok="DeleteUser">
                    {{this.MsgDelete}}             
            </b-modal>
        </nav-layout>
</template>
<script>
import { mapGetters } from 'vuex'
import Datepicker from 'vuejs-datepicker';
import {th} from 'vuejs-datepicker/dist/locale'
export default {
    name: 'ManageUser',
     components: {
        Datepicker,

    },
     data() {
        return {
            IsNew : true,
             TableHeader: [ 
             {key:'UserId',label:'ID',sortable: true}
            ,{key: 'Name',label:'ชื่อ',sortable: true}
            ,{key: 'LastName',label:'นามสกุล',sortable: true}
            ,{key: 'DepartmentName',label:'ชื่อแผนก',sortable: true}
            ,{key: 'PositionName',label:'ชื่อตำแหน่ง',sortable: true}
            ,{key: 'TargetPoint',label:'แต้มประกัน',sortable: true}
            ,{key: 'SubdivisionName',label:'ชื่อหน่วยงาน',sortable: true}
            ,{key: 'BirthDate',label:'วันเกิด',sortable: true}
            ,{key: 'StartDate',label:'วันที่เริ่มในรายงาน',sortable: true}
            ,{key: 'Enabled',label:'เปิดใช้งาน',sortable: true}
            ,{key: 'action',label:'' }
            ],
            RemoveItem : null,

            UserId : null,
            Name : null,
            LastName : null,
            SelectedPositionId : null,
            SelectedSubdivisionId : null,
            BirthDate:null,
            StartDate:null,
            Enabled:true,
            
            SelectedDepartmentId : null,
            filter: null,

             th:th,
        }
    },
     mounted() {
        this.initUser();
    },
    methods:
    {
         AddUser()
        {
            this.UserId = null;
            this.Name = null;
            this.LastName=null;
            this.SelectedPositionId=null;
            this.SelectedSubdivisionId=null;
            this.BirthDate=null;
            this.StartDate=null;
            this.Enabled=true;
            this.SelectedDepartmentId=null;

            this.IsNew = true;                      
            this.ShowEditForm(true);
        },
        EditUser(user)
        {
            this.UserId = user.UserId;
            this.Name = user.Name;
            this.LastName = user.LastName;
            this.SelectedPositionId = user.PositionId;
            this.SelectedSubdivisionId = user.SubdivisionId;
            this.BirthDate = user.BirthDate;
            this.StartDate = user.StartDate;
            this.Enabled = user.Enabled;
            this.SelectedDepartmentId = user.DepartmentId;  
            this.loadPositionDDL(user.DepartmentId);
            this.loadSubdivisionDDL(user.DepartmentId);
            this.IsNew = false;                    
            this.ShowEditForm(true);
        },
        ClosedUser()
        {
            this.ShowEditForm(false);
            this.UserId = null;
            this.Name = null;
            this.LastName=null;
            this.SelectedPositionId=null;
            this.SelectedSubdivisionId=null;
            this.BirthDate=null;
            this.StartDate=null;
            this.Enabled=true;
            this.SelectedDepartmentId=null;  
            this.$store.dispatch('manageUserModule/clcDDL')
            this.SetResultMsg(false,'');
        },

        SaveUser()
        {
            if(this.Name != null && this.LastName != null && this.SelectedPositionId != null && this.SelectedSubdivisionId != null && this.StartDate != null && this.Enabled != null)
            {
                if(this.IsNew)
                {
                    let startDateString = this.StartDate.toDateString();
                    let birthDateString = null
                    if(this.BirthDate != null)
                        birthDateString = this.BirthDate.toDateString();
                    this.AddNewUser(this.Name,this.LastName,this.SelectedPositionId,this.SelectedSubdivisionId,birthDateString,startDateString,this.Enabled);
                }
                else
                {
                    let startDateString = new Date(this.StartDate).toDateString();
                    let birthDateString = null
                    if(this.BirthDate != null)
                        birthDateString = new Date(this.BirthDate).toDateString();
                    this.UpdateUser(this.UserId,this.Name,this.LastName,this.SelectedPositionId,this.SelectedSubdivisionId,birthDateString,startDateString,this.Enabled);
                }
            }
            else
            {
                
                this.SetResultMsg(true,'ยังลงข้อมูลไม่ครบ')
            }         
        },
        AddNewUser: function(name,lastName,positionId,subdivisionId,birthDate,startDate,enabled)
        {
            this.$store.dispatch('manageUserModule/requestAddUser',{
                    name:name
                    ,lastName :lastName  
                    ,positionId :positionId  
                    ,subdivisionId :subdivisionId  
                    ,birthDate :birthDate  
                    ,startDate :startDate  
                    ,enabled :enabled               
            })
            
        },
        UpdateUser: function(id,name,lastName,positionId,subdivisionId,birthDate,startDate,enabled)
        {
               this.$store.dispatch('manageUserModule/requestUpdateUser',{
                    userId:id
                    ,name:name
                    ,lastName :lastName  
                    ,positionId :positionId  
                    ,subdivisionId :subdivisionId  
                    ,birthDate :birthDate  
                    ,startDate :startDate  
                    ,enabled :enabled                
            })     
        },
        DeleteUser()
        {
             if(this.RemoveItem)
             {
                this.$store.dispatch('manageUserModule/requestDeleteUser',{userId:this.RemoveItem.UserId});
             }
        },

        initUser()
        {
            this.loadDepartmentDDL()
            this.loadUserList()
        },
        loadDepartmentDDL()
        {
            this.$store.dispatch('manageUserModule/loadDDLDepartment')
        },
        selectedDepartment: function(e) 
        {
            if(this.SelectedDepartmentId != e)
            {
                this.SelectedDepartmentId = e;
                this.SelectedPositionId = null;
                this.SelectedSubdivisionId = null;
            }
            this.loadPositionDDL(this.SelectedDepartmentId);
            this.loadSubdivisionDDL(this.SelectedDepartmentId);
        },
         loadPositionDDL(departmentId)
        {
            this.$store.dispatch('manageUserModule/loadDDLPosition',departmentId)
        },
         loadSubdivisionDDL(departmentId)
        {
            this.$store.dispatch('manageUserModule/loadDDLSubdivision',departmentId)
        },
        loadUserList()
        {
            this.$store.dispatch('manageUserModule/loadUserList')
        },
        ShowCofirmBoxDialog:function(user)
        {         
            this.RemoveItem =user;
        },
        ShowEditForm(show)
        {
            this.$store.dispatch('manageUserModule/showEditForm',show)
        }, 
        SetResultMsg : function(show,msg)
        {
            this.$store.dispatch('manageUserModule/showErrorMessage',{show:show,message:msg})
        },  
        CloseAlert()
        {
            this.SetResultMsg(false,'');
        },
    },
     computed: 
    {
        ...mapGetters({
            userList:'manageUserModule/userList',
            showEdit: 'manageUserModule/showEdit',
            showSaveAlert: 'manageUserModule/showSaveAlert',
            saveResultMsg: 'manageUserModule/saveResultMsg',
            ddlDepartment:"manageUserModule/ddlDepartment",
            ddlPosition:"manageUserModule/ddlPosition",
            ddlSubDivision:"manageUserModule/ddlSubDivision",
            headerToken:'loginModule/header',
            loginId:'loginModule/loginId',
        }),
         MsgDelete()
        {
            if(this.RemoveItem!=null)
            {
                return "ต้องการลบบุคคล '"+this.RemoveItem.Name+" "+this.RemoveItem.LastName+"' หรือไม่?";
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
.container
{
    max-width: 1600px;
}
.detail-c
{
    width: 960px;
}
.form-control[readonly]{
  background-color:  #ffffff!important;
}
</style>


