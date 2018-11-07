import departmentService from '@/api/departmentService'
import isAfter from "date-fns/is_after";

const type = {
    loadDepartmentList:'LOAD_DEPARTMENT',
    showEdit: 'SHOW_EDIT_FROM',
    saveResultMsg: 'SET_ERROR_MESSAGE',
    showResultMsg: 'SHOW_ERROR_MESSAGE',
}
const state = {
    departmentList:[],
    showEditAddForm : false,
    saveResultMsg : null,
    showResultMsg : false,
}
const getters = {
    departmentList(state, getters) {
        return state.departmentList;
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
}
const actions = {
    async loadDepartmentList({ state, commit,rootGetters }) {
        let hearderToken = rootGetters["loginModule/header"]
        let departments = await departmentService.GetAllDepartment(hearderToken);
        commit(type.loadDepartmentList,departments.data)
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
        async requestAddDepartment({state,commit,dispatch,rootGetters},newDepartment )
        {
            let hearderToken = rootGetters["loginModule/header"]
            let result = await departmentService.AddNewDepartment(newDepartment.name,newDepartment.orderInReport,newDepartment.showInReport,hearderToken);   
            await dispatch('setApiResult',result)
        },
        async requestUpdateDepartment({state,commit,dispatch,rootGetters},updateDepartment)
        {
            let hearderToken = rootGetters["loginModule/header"]
            let result = await departmentService.UpdateDepartment(updateDepartment.departmentId,updateDepartment.name,updateDepartment.orderInReport,updateDepartment.showInReport,hearderToken);
            await dispatch('setApiResult',result)
        },
        async requestDeleteDepartment({state,commit,dispatch,rootGetters},deleteDepartment)
        {
            let hearderToken = rootGetters["loginModule/header"]
            let result = await departmentService.DeleteDepartment(deleteDepartment.departmentId,hearderToken)
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
                    await dispatch('loadDepartmentList')
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
}
const mutations = {
    [type.loadDepartmentList](state, departments){
        state.departmentList = departments;
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
}
export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
}