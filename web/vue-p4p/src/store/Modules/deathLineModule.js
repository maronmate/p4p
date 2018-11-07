import deathLineService from '@/api/deathLineService'
import isAfter from "date-fns/is_after";
const type = {
    loadDeathLineList:'LOAD_DEATHLINE',
    showEdit: 'SHOW_EDIT_FROM',
    saveResultMsg: 'SET_ERROR_MESSAGE',
    showResultMsg: 'SHOW_ERROR_MESSAGE',
    initYear:'INIT_YEAR',
    initMonth:'INIT_MONTH'
}
const state = {
    deathLineList:[],
    showEditAddForm : false,
    saveResultMsg : null,
    showResultMsg : false,
    monthList:[],
    yearList:[],

}
const getters = {
    deathLineList(state, getters) {
        return state.deathLineList;
    },
    showEdit(state, getters) {
        return state.showEditAddForm;
    },
    saveResultMsg(state, getters) {
        return state.saveResultMsg;
    },
    showSaveAlert(state, getters) {
        return state.showResultMsg;
    },
    monthList(state, getters) {
        return state.monthList;
    },
    yearList(state, getters) {
        return state.yearList;
    },
}
const actions = {
    async loadDeathLineList({ state, commit,rootGetters }) {
    let hearderToken = rootGetters["loginModule/header"]
    let deathLines = await deathLineService.getAllDeathLine(hearderToken);
    commit(type.loadDeathLineList,deathLines.data)
    },
    async showEditForm({state, commit}, show)
    {
         commit(type.showEdit,show )
    },
    async showErrorMessage({state, commit}, error)
    {
         commit(type.showResultMsg,error.show )
         commit(type.saveResultMsg,error.message )
    },
    async requestAddDeathLine({state,commit,dispatch,rootGetters},newDeathLine )
    {
        let hearderToken = rootGetters["loginModule/header"]
        let result = await deathLineService.AddNewDeathLine(newDeathLine.yM,newDeathLine.deathLineDate,newDeathLine.loginId,hearderToken);   
        await dispatch('setApiResult',result)
    },
    async requestUpdateDeathLine({state,commit,dispatch,rootGetters},updateDeathLine)
    {
        let hearderToken = rootGetters["loginModule/header"]
        let result = await deathLineService.UpdateDeathLine(updateDeathLine.yM,updateDeathLine.deathLineDate,updateDeathLine.loginId,hearderToken);
        await dispatch('setApiResult',result)
    },
    async requestDeleteDeathLine({state,commit,dispatch,rootGetters},deleteDeathLine)
    {
        let hearderToken = rootGetters["loginModule/header"]
        let result = await deathLineService.DeleteDeathLine(deleteDeathLine.yM,hearderToken)
        await dispatch('setApiResult',result)
    },
    async setApiResult({state,commit,dispatch},result)
    {
        if(result.status == 201 || result.status == 200)
        {
            if(result.data.Result !="OK")
            {
                await dispatch('showErrorMessage',{show:true,message:result.data.ResultDescription})
            }
            else
            {
                await dispatch('loadDeathLineList')
                await dispatch('showEditForm',false )
                await dispatch('showErrorMessage',{show:false,message:""})
            }            
        }
        else
        {
            let errorMsg={show:true,
            message:"api error : "+result.status+" : "+result.statusText}

            await dispatch('showErrorMessage',{show:true,message:errorMsg})
        }
    },
    initYearMonth({ state, commit })
    {
        let months = [ 
            {value:null,text:'กรุณาเลือกเดือน',disabled:true},
            {value:'1',text:'มกราคม'},
            {value:'2',text:'กุมภาพันธ์'},
            {value:'3',text:'มีนาคม'},
            {value:'4',text:'เมษายน'},
            {value:'5',text:'พฤษภาคม'},
            {value:'6',text:'มิถุนายน'},
            {value:'7',text:'กรกฎาคม'},
            {value:'8',text:'สิงหาคม'},
            {value:'9',text:'กันยายน'},
            {value:'10',text:'ตุลาคม'},
            {value:'11',text:'พฤศจิกายน'},
            {value:'12',text:'ธันวาคม'}
        ] 
        commit(type.initMonth,months);

        let currentYear = new Date().getFullYear();
        let years=[
            {value: null,text: 'กรุณาเลือกปี',disabled:true},
            {value: currentYear+2,text: currentYear+2},
            {value: currentYear+1,text: currentYear+1},
            {value: currentYear,text: currentYear},
            {value: currentYear-1,text: currentYear-1},
            {value: currentYear-2,text: currentYear-2},
        ]
        commit(type.initYear,years);
    },
    

}
const mutations = {
    [type.loadDeathLineList](state, deathLines){
        state.deathLineList = deathLines;
    },
    [type.showEdit](state, show) {
        state.showEditAddForm = show
    },
    [type.saveResultMsg](state, msg) {
        state.saveResultMsg = msg
    },
    [type.showResultMsg](state, show) {
        state.showResultMsg = show
    },
    [type.initYear](state, years) {
        state.yearList = years
    },
    [type.initMonth](state, months) {
        state.monthList = months
    },
}

export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
}