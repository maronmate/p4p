import subDivisionService from '@/api/subDivisionService'
import departmentService from '@/api/departmentService'

const type = {
    loadSubDivisionList:'LOAD_SUB_DIVISION',
    loadDdlDepartment:'LOAD_DDL_DEPARTMENT',
    showEdit: 'SHOW_EDIT_FROM',
    saveResultMsg: 'SET_ERROR_MESSAGE',
    showResultMsg: 'SHOW_ERROR_MESSAGE',
}
const state = {
    subDivisionList:[],
    showEditAddForm : false,
    saveResultMsg : null,
    showResultMsg : false,
    ddlDepartment :[]
}
const getters = {
    subDivisionList(state, getters) {
        return state.subDivisionList;
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
    ddlDepartment(state, getters) {
        return state.ddlDepartment;
    },
}
const actions = {
    async showEditForm({state, commit}, show)
    {
         commit(type.showEdit,show )
    },
    async showErrorMessage({state, commit}, error)
    {
         commit(type.showResultMsg,error.show )
         commit(type.saveResultMsg,error.message )
    },
    async loadDDLDepartment({ state, commit,rootGetters }) {
        let hearderToken = rootGetters["loginModule/header"]
        let departments = await departmentService.GetAllDepartmentDDL(hearderToken);
        commit(type.loadDdlDepartment,departments)
        },
    async loadSubDivisionList({ state, commit,rootGetters }) {
            let hearderToken = rootGetters["loginModule/header"]
            let subDivision = await subDivisionService.GetAllSubDivision(hearderToken);
            commit(type.loadSubDivisionList,subDivision.data)
        },
    async requestAddSubDivision({state,commit,dispatch,rootGetters},newSubDivision)
        {
            let hearderToken = rootGetters["loginModule/header"]
            let result = await subDivisionService.AddNewSubDivision(newSubDivision.name,newSubDivision.departmentId,newSubDivision.orderInDepartment,hearderToken);   
            await dispatch('setApiResult',result)
        },
    async requestUpdateSubDivision({state,commit,dispatch,rootGetters},updateSubDivision)
        {
            let hearderToken = rootGetters["loginModule/header"]
            let result = await subDivisionService.UpdateSubDivision(updateSubDivision.subdivisionId,updateSubDivision.name,updateSubDivision.departmentId,updateSubDivision.orderInDepartment,hearderToken);
            await dispatch('setApiResult',result)
        },
    async requestDeleteSubDivision({state,commit,dispatch,rootGetters},deleteSubDivision)
        {
            let hearderToken = rootGetters["loginModule/header"]
            let result = await subDivisionService.DeleteSubDivision(deleteSubDivision.subdivisionId,hearderToken)
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
                    await dispatch('loadSubDivisionList')
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
    [type.loadSubDivisionList](state, subDivisions){
        state.subDivisionList = subDivisions;
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
    [type.loadDdlDepartment](state, departmentDDL){
        state.ddlDepartment = departmentDDL;
    },
}
export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
}