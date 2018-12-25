<template>

    <nav-layout showTitle=true titleText="ลงคะแนน p4p" showDescription=true descriptionText="ลงคะแนนโดยการเลือกบุคคลจากตารางข้างล่าง">
        <div class="backgroundp4p">         
            <div class="container">
                 <div class = "detail-block">
                       <b-form-group>
                           <b-row align-v="center" align-h="start" class="mb-2 mt-2">
                               <b-col cols="2">
                               <label>ค้นหาคะแนนบุคคล</label>
                               </b-col>
                            </b-row> 
                            <b-row align-v="center" align-h="start" class="mb-2 mt-2">

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
                                <b-form-select id="ddlSubdivision"  v-model="SelectedSubdivisionId" :options="ddlSubDivision" @change="selectedSubdivision"></b-form-select>
                            </b-col>

                            <b-col cols="2" class="text-right">
                                <div>เลือกตำแหน่ง :</div>
                            </b-col>
                            <b-col cols="3">
                                <b-form-select id="ddlPosition"  v-model="SelectedPositionId" :options="ddlPosition" @change="selectedPosition"></b-form-select>
                            </b-col>                         
                        </b-row>
                        <b-row align-v="center" align-h="start" class="mb-2">

                            <b-col cols="3" class="text-right">
                                <div>เลือกบุคคล :</div>
                            </b-col>
                            <b-col cols="3">
                                <b-form-select id="ddlUser"  v-model="SelectedUserId" :options="ddlUser" ></b-form-select>
                            </b-col>
                        </b-row>
                         <b-row align-v="center" align-h="start" class="mb-2" >
                            <b-col cols="3" class="text-right">
                                <div>ปีเดือนที่เริ่ม :</div>
                            </b-col>
                            <b-col cols="3">
                                <datepicker v-model="SelectedymStart" name="ymStart" :bootstrap-styling="true" :language="th" :clear-button="true" minimum-view='month' format="MMMM yyyy"></datepicker>
                            </b-col>

                            <b-col cols="2" class="text-right">
                                <div>ปีเดือนที่จบ :</div>
                            </b-col>
                            <b-col cols="3">
                                <datepicker v-model="SelectedymEnd" name="ymEnd" :bootstrap-styling="true" :language="th" :clear-button="true" minimum-view='month' format="MMMM yyyy"></datepicker>
                            </b-col>                         
                        </b-row>
                         <b-row align-v="center" align-h="center" class="mb-2">
                            <b-button class="button-ok m-1" v-on:click="searchUserPoint">ค้นหา</b-button>
                        </b-row>                          
                      </b-form-group>
                 </div>               
            </div>
            <b-row align-h="center">
            <div class="background-table"  v-show="showTable">
                    <b-row  align-h="end" align-v="center" class="mt-2" >
                        <b-col md="3" class="my-1">
                            <b-form-group horizontal label="Filter" class="mb-0">
                                <b-input-group>
                                    <b-form-input v-model="filter" placeholder="Type to Search" />
                                    <b-input-group-append>
                                        <b-btn :disabled="!filter" @click="filter = ''">Clear</b-btn>
                                    </b-input-group-append>
                                </b-input-group>
                            </b-form-group>
                        </b-col>
                        <b-col cols="7" >
                            </b-col>
                        <b-col cols="2" >
                         </b-col>
                    </b-row>
                    <b-table  hover :items="userPointList" :fields="userPointHeader" :filter="filter" :fixed="true" :responsive ="true" :bordered ="true">     
                        <template slot="Name" slot-scope="data">
                           {{data.item.Name}} {{data.item.LastName}}
                        </template>
                        <template slot="TragetPoint" slot-scope="data">
                           {{data.item.TragetPoint|formatNumber}}
                        </template>
                        <template v-for="(field,index) in dynamicHeader" :slot="field.columnName" slot-scope="cell">
                            <span class="txtPoint" :key="'t'+index" >{{cell.item[field.columnName] | formatNumber}}</span>
                            <img @click="OpenRemark(cell.item['r'+field.columnName],cell.item['Name'],cell.item['LastName'],field.columnName)" v-show="cell.item['r'+field.columnName]"  :key="index" src="../assets/doc.png" v-b-tooltip.hover.html="ShowTooltip(cell.item['r'+field.columnName])">  
          
                        </template>
                    </b-table>
            </div>
            </b-row>
        </div>
        <b-modal id="modalRef"
            ref="modalRef"
             title="" 
             hide-footer
             hide-header
            >
            <div class="modelTitleData pt-2 pb-2">
                <b-container fluid>
                    <b-row align-h="start" align-v="center">
                        <b-col cols="3">
                            ผู้ใช้ :
                        </b-col>
                        <b-col cols="9">
                            {{this.RemarkName}}
                        </b-col>
                    </b-row>
                    <b-row align-h="start" align-v="center">
                        <b-col cols="3">
                            งวดปี-เดือน :
                        </b-col>
                        <b-col cols="9">
                            {{this.RemarkYM}}
                        </b-col>
                    </b-row>
                     <b-row align-h="start" align-v="center">
                        <b-col cols="3">
                            Remark :
                        </b-col>
                        <b-col cols="9">
                            {{this.RemarkDetail}}
                        </b-col>
                    </b-row>
                    
                </b-container>
            </div>
            <div>
                <b-row align-h="end" align-v="center">
                    <b-button class="button-ok mt-2 mr-3" variant="success"  @click="closeRemark">OK</b-button> 
                </b-row>
            </div>   
        </b-modal>
    </nav-layout>
</template>

<script>
import { mapGetters } from 'vuex'
import Datepicker from 'vuejs-datepicker';
import {th} from 'vuejs-datepicker/dist/locale'
import moment from 'moment'
export default {
     name: 'PointList',
     components: {
        Datepicker,
    },

     data() {
        return {
            IsNew : true,
            SelectedDepartmentId : null,
            SelectedSubdivisionId : null,
            SelectedPositionId : null,
            SelectedUserId : null,
            SelectedymStart : null,
            SelectedymEnd : null,
            filter : null,
            th:th,
            RemarkDetail :"",
            RemarkName :"",
            RemarkYM :"",
        }
        
    },
    mounted() {
        this.initData();
    },
     methods:
    {
        ShowTooltip(remark)
        {
            var value = ""
            if(remark)
                value = remark
            return value
        },
        OpenRemark(detail,name,lastName,ym)
        {
            this.RemarkName = name +' '+lastName,
            this.RemarkYM = ym,
            this.RemarkDetail = detail;
            this.$refs.modalRef.show();
        },
        closeRemark()
        {
            this.RemarkName ="",
            this.RemarkYM ="",
            this.RemarkDetail = "";
            this.$refs.modalRef.hide();
        },
        searchUserPoint()
        {
            let startYM = null;
            if(this.SelectedymStart)
            {
                startYM = moment(this.SelectedymStart).format('YYYY-MM-DD');
            }

            let endYM = null;
             if(this.SelectedymEnd)
             {
                endYM = moment(this.SelectedymEnd).format('YYYY-MM-DD'); 
             }

            let filter= {departmentId:this.SelectedDepartmentId
            ,subdivisionId:this.SelectedSubdivisionId
            ,positionId:this.SelectedPositionId
            ,userId:this.SelectedUserId
            ,ymStart:startYM
            ,ymEnd:endYM}
            this.$store.dispatch('pointListModule/loadUserPointList',filter)
        },
        selectedDepartment: function(e) 
        {
            if(this.SelectedDepartmentId != e)
            {
                this.SelectedDepartmentId = e;
                this.SelectedPositionId = null;
                this.SelectedSubdivisionId = null;
                this.SelectedUserId = null;
            }
            this.loadPositionDDL(this.SelectedDepartmentId);
            this.loadSubdivisionDDL(this.SelectedDepartmentId);
            this.loadUserDDL(this.SelectedDepartmentId, this.SelectedSubdivisionId,this.SelectedPositionId)
        },
        selectedSubdivision: function(e) 
        {
            if(this.SelectedSubdivisionId != e)
            {
                this.SelectedSubdivisionId = e;
                this.SelectedUserId = null;
            }
            this.loadUserDDL(this.SelectedDepartmentId, this.SelectedSubdivisionId,this.SelectedPositionId)
        },
        selectedPosition: function(e) 
        {
            if(this.SelectedPositionId != e)
            {
                this.SelectedPositionId = e;
                this.SelectedUserId = null;
            }
            this.loadUserDDL(this.SelectedDepartmentId, this.SelectedSubdivisionId,this.SelectedPositionId)
        },

        loadPositionDDL(departmentId)
        {
            this.$store.dispatch('pointListModule/loadDDLPosition',departmentId)
        },
        loadSubdivisionDDL(departmentId)
        {
            this.$store.dispatch('pointListModule/loadDDLSubdivision',departmentId)
        },
        loadUserDDL(departmentId,subdivisionId,positionId)
        {
            this.$store.dispatch('pointListModule/loadDDLUser',{
                    departmentId:departmentId,
                    subdivisionId:subdivisionId,
                    positionId:positionId
                    })
        },

        initData()
        {
            this.loadDepartmentDDL()
        },
        loadDepartmentDDL()
        {
            this.$store.dispatch('pointListModule/loadDDLDepartment')
        },
       
    },
      computed: 
    {
        ...mapGetters({
            userPointList:'pointListModule/userPointList',
            userPointHeader:'pointListModule/userPointHeader',
            dynamicHeader:'pointListModule/dynamicHeader',
            showEdit: 'pointListModule/showEdit',
            showTable: 'pointListModule/showTable',
            showSaveAlert: 'pointListModule/showSaveAlert',
            saveResultMsg: 'pointListModule/saveResultMsg',
            headerToken:'loginModule/header',
            loginId:'loginModule/loginId',
            ddlDepartment:'pointListModule/ddlDepartment',
            ddlPosition:'pointListModule/ddlPosition',
            ddlSubDivision:'pointListModule/ddlSubDivision',
            ddlUser:'pointListModule/ddlUser',
        }),
    }
    
}
</script>

<style scoped>
    .txtPoint
    {
        text-align: center;

    }
    .background-table
    {
        max-width: 1800px;

    }
    .modelTitleData
    {
        text-align: left;
        border-top-style: solid;
        border-top-width: thin;
        border-top-color: #e0e0e0bd;
        border-bottom-style: solid;
        border-bottom-width: thin;
        border-bottom-color: #e0e0e0bd;
    
    }
    img {
        float: right;
        margin-right: 5px;
        width: 20px;
        transition: width 1s;
    }
    img:hover {
        cursor: pointer;
    }
    th {
        width: 20px;
        max-width: 40px;
        min-width: 20px;
    }
</style>


