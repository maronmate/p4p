import positionService from '@/api/positionService'
import departmentService from '@/api/departmentService'

const type = {
    loadPositionList:'LOAD_POSITION',
    loadDdlDepartment:'LOAD_DDL_DEPARTMENT',
    showEdit: 'SHOW_EDIT_FROM',
    saveResultMsg: 'SET_ERROR_MESSAGE',
    showResultMsg: 'SHOW_ERROR_MESSAGE',
}
const state = {
    positionList:[],
    showEditAddForm : false,
    saveResultMsg : null,
    showResultMsg : false,
    ddlDepartment :[]
}
const getters = {
    positionList(state, getters) {
        return state.positionList;
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
        let departments = await departmentService.GetAllDepartmentDDL(false,hearderToken);
        commit(type.loadDdlDepartment,departments)
        },
    async loadPositionList({ state, commit,rootGetters }) {
            let hearderToken = rootGetters["loginModule/header"]
            let positions = await positionService.GetAllPosition(hearderToken);
            commit(type.loadPositionList,positions.data)
        },
    async requestAddPosition({state,commit,dispatch,rootGetters},newPosition )
        {
            let hearderToken = rootGetters["loginModule/header"]
            let result = await positionService.AddNewPosition(newPosition.name,newPosition.departmentId,newPosition.targetPoint,hearderToken);   
            await dispatch('setApiResult',result)
        },
    async requestUpdatePosition({state,commit,dispatch,rootGetters},updatePosition)
        {
            let hearderToken = rootGetters["loginModule/header"]
            let result = await positionService.UpdatePosition(updatePosition.positionId,updatePosition.name,updatePosition.departmentId,updatePosition.targetPoint,hearderToken);
            await dispatch('setApiResult',result)
        },
    async requestDeletePosition({state,commit,dispatch,rootGetters},deletePosition)
        {
            let hearderToken = rootGetters["loginModule/header"]
            let result = await positionService.DeletePosition(deletePosition.positionId,hearderToken)
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
                    await dispatch('loadPositionList')
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
    [type.loadPositionList](state, positions){
        state.positionList = positions;
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