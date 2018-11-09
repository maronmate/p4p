<template>
       <nav-layout showTitle=true titleText="จัดการหน่วยงาน" showDescription=true descriptionText="สร้างหน่วยงานเพื่อนำไปใช้ในส่วนอื่นของโปรแกรม">
        <div class="backgroundp4p">         
            <div class="container">
                <div class = "detail-block" v-show="showEdit">
                      <div class="form-group">
                           <b-row align-v="center" align-h="start" class="mb-4" >
                        </b-row>
                        <b-row align-v="center" align-h="start" class="mb-2" >
                           <b-col cols="3"></b-col>
                            <b-col cols="2" class="text-right">
                                <div>ชื่อหน่วยงาน :</div>
                            </b-col>
                            <b-col cols="3">
                                <input v-model="SubdivisionName" class="form-control">
                            </b-col>                          
                        </b-row>
                        <b-row align-v="center" align-h="start" class="mb-2">
                            <b-col cols="3">
                            </b-col>
                            <b-col cols="2" class="text-right">
                                <div>เลือกแผนก :</div>
                            </b-col>
                            <b-col cols="3">
                                <b-form-select id="ddlDepartment"  v-model="DepartmentId" :options="ddlDepartment"></b-form-select>
                            </b-col>
                        </b-row>
                        <b-row align-v="center" align-h="start" class="mb-2" >
                           <b-col cols="3"></b-col>
                            <b-col cols="2" class="text-right">
                                <div>ลำดับในแผนก :</div>
                            </b-col>
                            <b-col cols="3">
                                <input v-model="OrderInDepartment" type="number" class="form-control">
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
                         <b-row align-v="center" align-h="center" class="mb-2">
                            <b-button class="button-ok m-1" v-on:click="SaveSubDivision">บันทึก</b-button>
                            <b-button class="button-cancel m-1" v-on:click="ClosedSubDivision">ยกเลิก</b-button>
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
                            <b-button variant="success" class="button-ok mb-2 mt-2" v-show="!showEdit" v-on:click="AddSubDivision">เพิ่มตำแหน่ง</b-button>
                         </b-col>
                    </b-row>
                    <b-table striped hover :items="subDivisionList" :fields="TableHeader" :filter="filter">     
                        <template slot="action" slot-scope="row">
                            <b-button v-show="!showEdit" variant="success sm" class="button-ok mr-1" size="sm" @click.stop="EditSubDivision(row.item)">แก้ไข</b-button>
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
                    @ok="DeleteSubDivision">
                    {{this.MsgDelete}}             
            </b-modal>
       </nav-layout>
</template>
<script>
import { mapGetters } from 'vuex'
export default {
    name: 'ManageSubDivision',
    data() {
        return {
            IsNew : true,
             TableHeader: [ 
             {key:'SubdivisionId',label:'ID',sortable: true}
            ,{key: 'SubdivisionName',label:'ชื่อหน่วยงาน',sortable: true}

            ,{key: 'DepartmentName',label:'ชื่อแผนก',sortable: true}
            ,{key: 'UserInSubdivisionCount',label:'จำนวนคนในหน่วยงาน',sortable: true}
            ,{key: 'OrderInDepartment',label:'ลำดับในแผนก',sortable: true}
            ,{key: 'action',label:'' }
            ],
            RemoveItem : null,
            SubdivisionId : null,
            SubdivisionName : null,
            DepartmentId : null,
            OrderInDepartment:null,
            filter: null,
        }
    },
     mounted() {
        this.initSubDivision();
    },
    methods:
    {
        AddSubDivision()
        {
            this.DepartmentId = null;
            this.SubdivisionName = null;
            this.SubdivisionId=null;
            this.OrderInDepartment=null;
            this.IsNew = true;                      
            this.ShowEditForm(true);
        },
        EditSubDivision(subdivision)
        {
            this.DepartmentId = subdivision.DepartmentId;
            this.SubdivisionName = subdivision.SubdivisionName;
            this.SubdivisionId=subdivision.SubdivisionId;
            this.OrderInDepartment=subdivision.OrderInDepartment;
            this.IsNew = false;                      
            this.ShowEditForm(true);
        },
        ClosedSubDivision()
        {
            this.ShowEditForm(false);
            this.DepartmentId = null;
            this.SubdivisionName = null;
            this.SubdivisionId=null;
            this.OrderInDepartment=null;   
            this.SetResultMsg(false,'');
        },

        SaveSubDivision()
        {
            if(this.SubdivisionName != null && this.DepartmentId != null)
            {

                if(this.OrderInDepartment < 0 )
                {
                   this.SetResultMsg(true,'ลำดับในแผนก ต้องมีค่ามากกว่าหรือเท่ากับ 0') 
                   return;
                }
                if(this.IsNew)
                        this.AddNewSubDivision(this.SubdivisionName,this.DepartmentId,this.OrderInDepartment);
                    else
                        this.UpdateSubDivision(this.SubdivisionId,this.SubdivisionName,this.DepartmentId,this.OrderInDepartment);
            }
            else
            {
                
                this.SetResultMsg(true,'ยังลงข้อมูลไม่ครบ')
            }
          
        },
        AddNewSubDivision: function(name,departmentId,orderInDepartment)
        {
            this.$store.dispatch('subDivisionModule/requestAddSubDivision',{
                    name:name
                    ,departmentId :departmentId  
                    ,orderInDepartment :orderInDepartment               
            })
            
        },
        UpdateSubDivision: function(id,name,departmentId,orderInDepartment)
        {
               this.$store.dispatch('subDivisionModule/requestUpdateSubDivision',{
                    subdivisionId:id
                    ,name:name
                    ,departmentId :departmentId  
                    ,orderInDepartment :orderInDepartment                
            })     
        },
        DeleteSubDivision()
        {
             if(this.RemoveItem)
             {
                this.$store.dispatch('subDivisionModule/requestDeleteSubDivision',{subdivisionId:this.RemoveItem.SubdivisionId});
             }
        },

        initSubDivision()
        {
            this.loadDepartmentDDL()
            this.loadSubDivision()
        },
        loadDepartmentDDL()
        {
            this.$store.dispatch('subDivisionModule/loadDDLDepartment')
        },
        loadSubDivision()
        {
            this.$store.dispatch('subDivisionModule/loadSubDivisionList')
        },
        ShowCofirmBoxDialog:function(subdivision)
        {         
            this.RemoveItem =subdivision;
        },
        ShowEditForm(show)
        {
            this.$store.dispatch('subDivisionModule/showEditForm',show)
        }, 
        SetResultMsg : function(show,msg)
        {
            this.$store.dispatch('subDivisionModule/showErrorMessage',{show:show,message:msg})
        },  
        CloseAlert()
        {
            this.SetResultMsg(false,'');
        },
    },
    computed: 
    {
        ...mapGetters({
            subDivisionList:'subDivisionModule/subDivisionList',
            showEdit: 'subDivisionModule/showEdit',
            showSaveAlert: 'subDivisionModule/showSaveAlert',
            saveResultMsg: 'subDivisionModule/saveResultMsg',
            headerToken:'loginModule/header',
            loginId:'loginModule/loginId',
            ddlDepartment:"subDivisionModule/ddlDepartment",
        }),
         MsgDelete()
        {
            if(this.RemoveItem!=null)
            {
                return "ต้องการลบหน่วยงาน '"+this.RemoveItem.SubdivisionName+"' หรือไม่?";
            }
            else
                return "";
        }
    }
}
</script>
<style scoped>

</style>
