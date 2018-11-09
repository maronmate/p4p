<template>
    <nav-layout showTitle=true titleText="จัดการตำแหน่ง" showDescription=true descriptionText="สร้างตำแหน่งเพื่อนำไปใช้ในส่วนอื่นของโปรแกรม">
        <div class="backgroundp4p">         
            <div class="container">
                 <div class = "detail-block" v-show="showEdit">
                      <div class="form-group">
                        <b-row align-v="center" align-h="start" class="mb-4" >
                        </b-row>
                        <b-row align-v="center" align-h="start" class="mb-2" >
                           <b-col cols="3"></b-col>
                            <b-col cols="2" class="text-right">
                                <div>ชื่อตำแหน่ง :</div>
                            </b-col>
                            <b-col cols="3">
                                <input v-model="PositionName" class="form-control">
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
                                <div>แต้มประกัน :</div>
                            </b-col>
                            <b-col cols="3">
                                <input v-model="TargetPoint" type="number" class="form-control">
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
                            <b-button class="button-ok m-1" v-on:click="SavePosition">บันทึก</b-button>
                            <b-button class="button-cancel m-1" v-on:click="ClosedPosition">ยกเลิก</b-button>
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
                            <b-button variant="success" class="button-ok mb-2 mt-2" v-show="!showEdit" v-on:click="AddPosition">เพิ่มตำแหน่ง</b-button>
                         </b-col>
                    </b-row>
                    <b-table striped hover :items="positionList" :fields="TableHeader" :filter="filter">     
                        <template slot="action" slot-scope="row">
                            <b-button v-show="!showEdit" variant="success sm" class="button-ok mr-1" size="sm" @click.stop="EditPosition(row.item)">แก้ไข</b-button>
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
                    @ok="DeletePosition">
                    {{this.MsgDelete}}             
            </b-modal>
    </nav-layout>
</template>
<script>
import { mapGetters } from 'vuex'
export default {
    name: 'ManagePosition',
      data() {
        return {
            IsNew : true,
             TableHeader: [ 
             {key:'PositionId',label:'ID',sortable: true}
            ,{key: 'PositionName',label:'ชื่อตำแหน่ง',sortable: true}

            ,{key: 'DepartmentName',label:'ชื่อแผนก',sortable: true}
            ,{key: 'UserInPositionCount',label:'จำนวนคนในตำแหน่ง',sortable: true}
            ,{key: 'TargetPoint',label:'แต้มประกัน',sortable: true}
            ,{key: 'action',label:'' }
            ],
            RemoveItem : null,
            PositionId : null,
            PositionName : null,
            DepartmentId : null,
            TargetPoint:null,
            filter: null,
        }
    },
     mounted() {
        this.initPosition();
    },
    methods:
    {

        AddPosition()
        {
            this.DepartmentId = null;
            this.PositionName = null;
            this.PositionId=null;
            this.TargetPoint=null;
            this.IsNew = true;                      
            this.ShowEditForm(true);
        },
        EditPosition(position)
        {
            this.DepartmentId = position.DepartmentId;
            this.PositionName = position.PositionName;
            this.PositionId=position.PositionId;
            this.TargetPoint=position.TargetPoint;
            this.IsNew = false;                      
            this.ShowEditForm(true);
        },
        ClosedPosition()
        {
            this.ShowEditForm(false);
            this.DepartmentId = null;
            this.PositionName = null;
            this.PositionId=null;
            this.TargetPoint=null;   
            this.SetResultMsg(false,'');
        },

        SavePosition()
        {
            if(this.PositionName != null)
            {

                if(this.TargetPoint < 0 )
                {
                   this.SetResultMsg(true,'แต้มประกัน ต้องมีค่ามากกว่าหรือเท่ากับ 0') 
                   return;
                }
                if(this.IsNew)
                        this.AddNewPosition(this.PositionName,this.DepartmentId,this.TargetPoint);
                    else
                        this.UpdatePosition(this.PositionId,this.PositionName,this.DepartmentId,this.TargetPoint);
            }
            else
            {
                
                this.SetResultMsg(true,'ยังลงข้อมูลไม่ครบ')
            }
          
        },
        AddNewPosition: function(name,departmentId,targetPoint)
        {
            this.$store.dispatch('positionModule/requestAddPosition',{
                    name:name
                    ,departmentId :departmentId  
                    ,targetPoint :targetPoint               
            })
            
        },
        UpdatePosition: function(id,name,departmentId,targetPoint)
        {
               this.$store.dispatch('positionModule/requestUpdatePosition',{
                    positionId:id
                    ,name:name
                    ,departmentId :departmentId  
                    ,targetPoint :targetPoint                
            })     
        },
        DeletePosition()
        {
             if(this.RemoveItem)
             {
                this.$store.dispatch('positionModule/requestDeletePosition',{positionId:this.RemoveItem.PositionId});
             }
        },

        initPosition()
        {
            this.loadDepartmentDDL()
            this.loadPosition()
        },
        loadDepartmentDDL()
        {
            this.$store.dispatch('positionModule/loadDDLDepartment')
        },
        loadPosition()
        {
            this.$store.dispatch('positionModule/loadPositionList')
        },
        ShowCofirmBoxDialog:function(position)
        {         
            this.RemoveItem =position;
        },

        ShowEditForm(show)
        {
            this.$store.dispatch('positionModule/showEditForm',show)
        }, 
        SetResultMsg : function(show,msg)
        {
            this.$store.dispatch('positionModule/showErrorMessage',{show:show,message:msg})
        },  
        CloseAlert()
        {
            this.SetResultMsg(false,'');
        },
    },
     computed: 
    {
        ...mapGetters({
            positionList:'positionModule/positionList',
            showEdit: 'positionModule/showEdit',
            showSaveAlert: 'positionModule/showSaveAlert',
            saveResultMsg: 'positionModule/saveResultMsg',
            headerToken:'loginModule/header',
            loginId:'loginModule/loginId',
            ddlDepartment:"positionModule/ddlDepartment",
        }),
         MsgDelete()
        {
            if(this.RemoveItem!=null)
            {
                return "ต้องการลบตำแหน่ง '"+this.RemoveItem.PositionName+"' หรือไม่?";
            }
            else
                return "";
        }
    }
}
</script>
<style scoped>

</style>


