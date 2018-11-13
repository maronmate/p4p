import subDivisionService from '@/api/subDivisionService'
import departmentService from '@/api/departmentService'
import positionService from '@/api/positionService'
import userService from '@/api/userService'
const type = {
    loadUserList:'LOAD_SUB_DIVISION',
    loadDdlDepartment:'LOAD_DDL_DEPARTMENT',
    onLoadAPI:'ON_LOAD_API',
    loadDdlPosition:'LOAD_DDL_POSITION',
    loadDdlSubDivision:'LOAD_DDL_SUBDIVISION',
    showEdit: 'SHOW_EDIT_FROM',
    saveResultMsg: 'SET_ERROR_MESSAGE',
    showResultMsg: 'SHOW_ERROR_MESSAGE',
}
const state = {
    userList:[],
    showEditAddForm : false,
    saveResultMsg : null,
    showResultMsg : false,
    ddlDepartment :[],
    ddlPosition :[],
    ddlSubDivision :[],
    onLoad : false,
}
const getters = {
    userList(state, getters) {
        return state.userList;
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
    ddlPosition(state, getters) {
        return state.ddlPosition;
    },
    ddlSubDivision(state, getters) {
        return state.ddlSubDivision;
    },
    onLoad(state, getters) {
        return state.onLoad;
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
    async loadUserList({ state, commit,rootGetters }) {
            let hearderToken = rootGetters["loginModule/header"]
            let users = await userService.GetAllUser(hearderToken);
            commit(type.loadUserList,users.data)
    },

    async requestAddUser({state,commit,dispatch,rootGetters},newUser)
    {
        let hearderToken = rootGetters["loginModule/header"]
        let result = await userService.AddNewUser(newUser.name,newUser.lastName,newUser.positionId,newUser.subdivisionId,newUser.birthDate,newUser.startDate,newUser.enabled,hearderToken);   
        await dispatch('setApiResult',result)
    },
    async requestUpdateUser({state,commit,dispatch,rootGetters},updateUser)
    {
        let hearderToken = rootGetters["loginModule/header"]
        let result = await userService.UpdateUser(updateUser.userId,updateUser.name,updateUser.lastName,updateUser.positionId,updateUser.subdivisionId,updateUser.birthDate,updateUser.startDate,updateUser.enabled,hearderToken);
        await dispatch('setApiResult',result)
    },
    async requestDeleteUser({state,commit,dispatch,rootGetters},deleteUser)
    {
        let hearderToken = rootGetters["loginModule/header"]
        let result = await userService.DeleteUser(deleteUser.userId,hearderToken)
        await dispatch('setApiResult',result)
    },

    async loadDDLDepartment({ state, commit,rootGetters }) {
        let hearderToken = rootGetters["loginModule/header"]
        let departments = await departmentService.GetAllDepartmentDDL(hearderToken);
        commit(type.loadDdlDepartment,departments)
    },
    async loadDDLPosition({ state, commit,rootGetters },departmentId) {
        let hearderToken = rootGetters["loginModule/header"]
        let positions = await positionService.GetPositionDDLbyDepartment(departmentId,hearderToken);
        commit(type.loadDdlPosition,positions)
    },
    async loadDDLSubdivision({ state, commit,rootGetters },departmentId) {
        let hearderToken = rootGetters["loginModule/header"]
        let subdivisions = await subDivisionService.GetSubdivisionDDLbyDepartment(departmentId,hearderToken);
        commit(type.loadDdlSubDivision,subdivisions)
    },
    async clcDDL({ state, commit,rootGetters }) {
        commit(type.loadDdlSubDivision,[])
        commit(type.loadDdlPosition,[])
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
                await dispatch('loadUserList')
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
    [type.loadUserList](state, users){
        state.userList = users;
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
    [type.loadDdlPosition](state, ddlPosition){
        state.ddlPosition = ddlPosition;
    },
    [type.loadDdlSubDivision](state, ddlSubDivision){
        state.ddlSubDivision = ddlSubDivision;
    },
    [type.onLoadAPI](state, load){
        state.onLoad = load;
    },
}
export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
}