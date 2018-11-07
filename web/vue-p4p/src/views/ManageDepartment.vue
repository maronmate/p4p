<template>
    <nav-layout showTitle=true titleText="จัดการแผนก" showDescription=true descriptionText="สร้างแผนกเพื่อนำไปใช้ในส่วนอื่นของโปรแกรม">
        <div class="backgroundp4p">         
        <div class="container">
        
                <div class = "detail-block" v-show="showEdit">
                    <div class="form-group">
                          <b-row align-v="center" align-h="start" class="mb-4" >
                              </b-row>
                         <b-row align-v="center" align-h="start" class="mb-2" >
                           <b-col cols="3"></b-col>
                            <b-col cols="2" class="text-right">
                                <div>ชื่อแผนก :</div>
                            </b-col>
                            <b-col cols="4">
                                <input v-model="DepartmentName" class="form-control" style="width: 300px;">
                            </b-col>
                            
                         </b-row>
                         <b-row align-v="center" align-h="start" class="mb-2" >
                             <b-col cols="3"></b-col>
                             <b-col cols="2" class="text-right">
                                <div>ลำดับแผนกในรายงาน :</div>
                            </b-col>
                             <b-col cols="3">
                                <input v-model="OrderInReport" type="number" class="form-control" style="width: 300px;">
                            </b-col>
                         
                        </b-row>
                        <b-row align-v="center" align-h="center" class="mb-2">
                            <b-col cols="1"></b-col>
                             <b-col cols="3" class="text-right">
                        
                            <b-form-checkbox id="chkIsShowReport"
                                class="float-left mt-2"
                                v-model="ShowInReport"
                                value=true
                                unchecked-value=false>                            
                            แสดงแผนกในรายงาน</b-form-checkbox>
                             </b-col>
                        </b-row>
                          <b-row  align-h="center" class="mt-3">
                             <b-col cols="8">
                                <b-alert variant="danger"
                            dismissible
                            :show="showSaveAlert"
                            @dismissed="CloseAlert">
                            {{saveResultMsg}}
                            </b-alert>
                            </b-col>
                        </b-row>
                        <b-row align-v="center" align-h="center" class="mb-3">
                            <b-button class="button-ok m-1" v-on:click="SaveDepartment">บันทึก</b-button>
                            <b-button class="button-cancel m-1" v-on:click="ClosedDepartment">ยกเลิก</b-button>
                        </b-row>                        
                    </div>
               </div>
                <div class="row col col-md-2 col-md-push-10 float-right">
                    <b-button variant="success" class="button-ok ml-2 mb-2 mt-2 btn-sm" v-show="!showEdit" v-on:click="AddDepartment">เพิ่มแผนก</b-button>
                </div>
                <div class="background-table">
                    <b-table striped hover :items="departmentList" :fields="TableHeader">  
                        <template slot="ShowInReport" slot-scope="data">
                            <span class="text-yes" v-if="data.value">Yes</span>
                            <span class="text-no" v-if="!data.value">No</span>
                        </template>         
                        <template slot="action" slot-scope="row">
                            <b-button v-show="!showEdit" variant="success sm" class="button-ok mr-1" size="sm" @click.stop="EditDepartment(row.item)">แก้ไข</b-button>
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
                    @ok="DeleteDepartment">
                    {{this.MsgDelete}}             
            </b-modal>
    </nav-layout>
</template>

<script >
    import { mapGetters } from 'vuex'

    export default {
    name: 'DeathLine',
    data() {
        return {
            IsNew : true,
             TableHeader: [ 
             {key:'DepartmentId',label:'ID',sortable: true}
            ,{key: 'DepartmentName',label:'ชื่อแผนก',sortable: true}
            ,{key: 'OrderInReport',label:'ลำดับแผนกในรายงาน',sortable: true}
            ,{key: 'ShowInReport',label:'แสดงแผนกในรายงาน',sortable: true}
            ,{key: 'PositionCount',label:'จำนวนตำแหน่งในแผนก',sortable: true}
            ,{key: 'SubDivisionCount',label:'จำนวนหน่วยงานในแผนก',sortable: true}
            ,{key: 'UserCount',label:'จำนวนคนในแผนก',sortable: true}         
            ,{key:'action',label:'' }
            ],
            RemoveItem : null,
            DepartmentId : null,
            DepartmentName : null,
            OrderInReport:null,
            ShowInReport: true,
        }
    },
    mounted() {
        this.loadDepartment();
    },
     methods:
     
    {
        ClosedDepartment()
        {
            this.ShowEditForm(false);
            this.DepartmentId = null
            this.DepartmentName = null
            this.OrderInReport=null
            this.ShowInReport=true    
            this.SetResultMsg(false,'');
        },
        AddDepartment()
        {
            this.DepartmentId = null
            this.DepartmentName = null
            this.OrderInReport=null
            this.ShowInReport=true
            this.IsNew = true;                      
            this.ShowEditForm(true);
        },
          ShowEditForm(show)
        {
            this.$store.dispatch('departmentModule/showEditForm',show)
        },
        EditDepartment(department)
        {
            console.log(department)
            this.DepartmentId = department.DepartmentId
            this.DepartmentName = department.DepartmentName
            this.OrderInReport=department.OrderInReport
            this.ShowInReport=department.ShowInReport
            this.IsNew = false;                      
            this.ShowEditForm(true);
        },
         CloseAlert()
        {
            this.SetResultMsg(false,'');
        },
        AddNewDepartment: function(name,orderInReport,showInReport)
        {
            this.$store.dispatch('departmentModule/requestAddDepartment',{
                    name:name
                    ,orderInReport :orderInReport  
                    ,showInReport :showInReport               
            })
            
        },
        UpdateDepartment: function(id,name,orderInReport,showInReport)
        {
               this.$store.dispatch('departmentModule/requestUpdateDepartment',{
                    departmentId:id
                    ,name:name
                    ,orderInReport :orderInReport  
                    ,showInReport :showInReport               
            })     
        },
        ShowCofirmBoxDialog:function(department)
        {         
            this.RemoveItem =department;
        },
        loadDepartment()
        {
            this.$store.dispatch('departmentModule/loadDepartmentList')
        },
        DeleteDepartment()
        {
             if(this.RemoveItem)
             {
                 console.log(this.RemoveItem.DepartmentId)
                this.$store.dispatch('departmentModule/requestDeleteDepartment',{departmentId:this.RemoveItem.DepartmentId});
             }
        },

        SaveDepartment()
        {
            if(this.DepartmentName != null)
            {
                if(this.OrderInReport == null )
                {
                   this.OrderInReport = 0 
                }
                if(this.OrderInReport < 0 )
                {
                   this.SetResultMsg(true,'ลำดับแผนกในรายงาน ต้องมีค่ามากกว่าหรือเท่ากับ0') 
                   return;
                }
                if(this.IsNew)
                        this.AddNewDepartment(this.DepartmentName,this.OrderInReport,this.ShowInReport);
                    else
                        this.UpdateDepartment(this.DepartmentId,this.DepartmentName,this.OrderInReport,this.ShowInReport);
            }
            else
            {
                
                this.SetResultMsg(true,'ยังลงข้อมูลไม่ครบ')
            }
          
        },
         SetResultMsg : function(show,msg)
        {
            this.$store.dispatch('departmentModule/showErrorMessage',{show:show,message:msg})
        },
        
    },
    computed: 
    {
        ...mapGetters({
            departmentList:'departmentModule/departmentList',
            showEdit: 'departmentModule/showEdit',
            showSaveAlert: 'departmentModule/showSaveAlert',
            saveResultMsg: 'departmentModule/saveResultMsg',
            headerToken:'loginModule/header',
            loginId:'loginModule/loginId',
        }),
         MsgDelete()
        {
            if(this.RemoveItem!=null)
            {
                return "ต้องการลบแผนก '"+this.RemoveItem.DepartmentName+"' หรือไม่?";
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
</style>