<template>
    <nav-layout showTitle=true titleText="จัดการ DeathLine" showDescription=true descriptionText="เพื่อกำหนดวันที่สิ้นสุดการลงข้อมูล P4P ของแต่ละเดือน">
        <div class="backgroundp4p">          
            <b-container >
            <div class = "detail-block" v-show="showEdit">
               <div class="form-group">
            
            <b-row align-v="center" align-h="start" >
                <b-col cols="1">
                </b-col>
                <b-col cols="2" class="text-right mt-3" >
                    <h5>เลือกงวด ปี-เดือน</h5>
                </b-col>
            </b-row>
            <b-row align-v="center" align-h="start">
                <b-col cols="2">
                </b-col>
                <b-col cols="1" class="text-right">
                    <div>ปี :</div>
                </b-col>
                <b-col cols="3">
                    <b-form-select id="ddlYear" :disabled="!IsNew" v-model="selectedYear" :options="yearList"></b-form-select>
                </b-col>
                <b-col cols="1"  class="text-right">
                    <div>เดือน :</div>
                </b-col>
                <b-col cols="3">
                    <b-form-select id="ddlMonth" :disabled="!IsNew" v-model="selectedMonth" :options="monthList"></b-form-select>
                </b-col>
                <b-col cols="1">
                </b-col>
            </b-row>
             <b-row align-v="center" align-h="start" class="mt-2">
                  <b-col cols="1">
                </b-col>
                 <b-col cols="2" class="text-right">
                    <div>เลือกวันที่ DeathLine :</div>
                </b-col>
                <b-col cols="2">
                    <datepicker v-model="selectedDeathLine" name="deathLineDate"></datepicker>
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
                <b-button class="button-ok m-1" v-on:click="SaveDeathLine">บันทึก</b-button>
                <b-button class="button-cancel m-1" v-on:click="ClosedDeathLine">ยกเลิก</b-button>
            </b-row>
           
            </div>
            </div>

            <div class="row col col-md-2 col-md-push-10 float-right">
                    <b-button variant="success" class="button-ok ml-2 mb-2 mt-2 btn-sm" v-show="!showEdit" v-on:click="AddDeathLine">เพิ่ม DeathLine</b-button>
                </div>
            <div>
                <deathline-list v-bind:ShowEdit="showEdit" @EditDeathLineItem="EditDeathLine" @DeleteDeathLineItem="DeleteDeathLine" ></deathline-list>
            </div>
            
        </b-container>
        </div>
    </nav-layout>
</template>

<script >
import { mapGetters } from 'vuex'
import deathlineList from '@/components/deathLine/deathLineList'
import Datepicker from 'vuejs-datepicker';

export default {
    name: 'DeathLine',
    
    components: {
        deathlineList,
        Datepicker,

    },
     data() {
    return {
        selectedYear:null,
        selectedMonth:null,
        selectedDeathLine:null,
        IsNew : true,
        }
    },
    mounted() {
        this.loadDeathLine();
    },
    methods:
    {
        loadDeathLine()
        {
            this.$store.dispatch('deathLineModule/initYearMonth')
            this.$store.dispatch('deathLineModule/loadDeathLineList')
           // console.log(this.deathlineListItem)
        },
        AddDeathLine()
        {
            this.selectedYear = null;
            this.selectedMonth = null;
            this.selectedDeathLine = null;   
            this.IsNew = true;                      
            this.ShowEditForm(true);
            
        },
         ShowEditForm(show)
        {
            this.$store.dispatch('deathLineModule/showEditForm',show)
        },
         SetResultMsg : function(show,msg)
        {
            this.$store.dispatch('deathLineModule/showErrorMessage',{show:show,message:msg})
        },
        EditDeathLine(deathLine)
        {
            if(deathLine!=null)
            {
                let ym = new Date(deathLine.YM);
                this.selectedYear = ym.getFullYear();
                this.selectedMonth = ym.getMonth()+1;
                this.selectedDeathLine = deathLine.DeathLineDate;
                this.ShowEditForm(true); 
                this.IsNew = false;  
            }     
        },
        DeleteDeathLine(deathLine)
        {
            let ym = new Date(deathLine.YM).toDateString();
            this.$store.dispatch('deathLineModule/requestDeleteDeathLine',{yM:ym});
        },
        ClosedDeathLine()
        {
            this.ShowEditForm(false);
            this.selectedYear = null;
            this.selectedMonth = null;
            this.selectedDeathLine = null;      
            this.SetResultMsg(false,'');
        },
        CloseAlert()
        {
            this.SetResultMsg(false,'');
        },
        AddNewDeathLine: function(ym,deathLineDate,editUser)
        {
            this.$store.dispatch('deathLineModule/requestAddDeathLine',{
                    yM:ym
                    ,deathLineDate :deathLineDate  
                    ,loginId :editUser               
            })
            
        },
        UpdateDeathLine: function(ym,deathLineDate,editUser)
        {
               this.$store.dispatch('deathLineModule/requestUpdateDeathLine',{
                    yM: ym
                    ,deathLineDate: deathLineDate
                    ,loginId :editUser          
            })     
        },
        SaveDeathLine()
        {
            if(this.selectedYear != null && this.selectedMonth != null && this.selectedDeathLine != null)
            {
                let ymString =  this.selectedYear +'-'+this.selectedMonth+'-01';
                let ym = new Date(ymString).toDateString();
                let deathlineString = this.selectedDeathLine.toDateString();

                 if(this.IsNew)
                        this.AddNewDeathLine(ym,deathlineString,this.loginId);
                    else
                        this.UpdateDeathLine(ym,deathlineString,this.loginId);
            }
            else
            {
                
                this.SetResultMsg(true,'ยังลงข้อมูลไม่ครบ')
            }
          
        },
    },
      computed: 
    {
        ...mapGetters({
            deathlineListItem:'deathLineModule/deathLineList',
            showEdit: 'deathLineModule/showEdit',
            showSaveAlert: 'deathLineModule/showSaveAlert',
            saveResultMsg: 'deathLineModule/saveResultMsg',
            headerToken:'loginModule/header',
            monthList:'deathLineModule/monthList',
            yearList:'deathLineModule/yearList',
            loginId:'loginModule/loginId',
        })
    }
}

</script>

<style scoped>
.right{
    float:right;
}
.text-right
{
    text-align: right;
}
.select-box
{
    width: 30px;
    height: 30px;
}


</style>