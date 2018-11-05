<template>
    <div class="background-table">
        <template>
             <b-table striped hover :items="deathlineListItem" :fields="TableHeader">
              <template slot="YM" slot-scope="data" >    
                  {{data.value|moment("YYYY-MM")}}
              </template>
              <template slot="DeathLineDate" slot-scope="data" >    
                  {{data.value|moment("DD/MM/YYYY")}}
              </template>
              <template slot="ModifyDate" slot-scope="data" >    
                  {{data.value|moment("DD/MM/YYYY")}}
              </template>               
                <template slot="action" slot-scope="row">
                    <b-button v-show="!ShowEdit" variant="success sm" class="button-ok mr-1" size="sm" @click.stop="$emit('EditDeathLineItem', row.item)">แก้ไข</b-button>
                    <b-button v-show="!ShowEdit" variant="danger sm" class="button-cancel mr-1" size="sm" @click="ShowCofirmBoxDialog(row.item)" v-b-modal.modalRef >ลบ</b-button>                    
                </template>
            </b-table>

          
        </template>
          <b-modal 
          id="modalRef"
                    ref="modalRef"
                    title="Confirm Remove"                
                    @ok="EmitDelet">
                    {{this.MsgDelete}}             
            </b-modal>
    </div>
</template>
<script>
import { mapGetters } from 'vuex'
export default {

    Name:'deathline-list',
    
    props: ['ShowEdit'],
    data: function () {
        return {
            TableHeader: [ 
             {key:'YM',label:'งวดปี-เดือน'}
            ,{key: 'DeathLineDate',label:'วันที่ Death Line'}
            ,{key: 'ModifyName',label:'ผู้แก้ไข'}
            ,{key: 'ModifyDate',label:'วันที่แก้ไข'}
            ,{key:'action',label:'' }
            ],
            RemoveItem : null,
        };
    },
    methods:
    {
        ShowCofirmBoxDialog:function(deathline)
        {           
            this.RemoveItem =deathline;
        },
        EmitDelet:function()
        {
            this.$emit('DeleteDeathLineItem', this.RemoveItem);
        }
    },
    computed:
    {
        ...mapGetters({
            deathlineListItem:'deathLineModule/deathLineList',
        }),
        MsgDelete()
        {
            if(this.RemoveItem!=null)
            {
                let YM = new Date(this.RemoveItem.YM);
                let stringYM =  YM.getFullYear() +'-'+(YM.getMonth()+1).toString().padStart(2, '0')
                return "Do you sure to Remove DeathLine "+stringYM+" ?";
            }
            else
                return "";
        }
    }
}
</script>
<style scoped>
   
</style>