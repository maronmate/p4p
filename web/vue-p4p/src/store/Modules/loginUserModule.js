import loginUserService from '@/api/loginUserService'
import departmentService from '@/api/departmentService'

const type = {
    loadLoginUserList:'LOAD_LOGIN_USER',
    loadCBDepartment:'LOAD_CB_DEPARTMENT',
    showEdit: 'SHOW_EDIT_FROM',
    saveResultMsg: 'SET_ERROR_MESSAGE',
    showResultMsg: 'SHOW_ERROR_MESSAGE',
}
const state = {
    loginUserList:[],
    showEditAddForm : false,
    saveResultMsg : null,
    showResultMsg : false,
    cbDepartment :[]
}
const getters = {
    loginUserList(state, getters) {
        return state.loginUserList;
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
    cbDepartment(state, getters) {
        return state.cbDepartment;
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
    async loadCheckBoxDepartment({ state, commit,rootGetters }) {
        let hearderToken = rootGetters["loginModule/header"]
        let departments = await departmentService.GetAllDepartmentCheckBox(hearderToken);
        commit(type.loadCBDepartment,departments)
        },
    async loadLoginUserList({ state, commit,rootGetters }) {
            let hearderToken = rootGetters["loginModule/header"]
            let loginUser = await loginUserService.GetAllLoginUser(hearderToken);
            commit(type.loadLoginUserList,loginUser.data)
        },

    async requestAddLoginUser({state,commit,dispatch,rootGetters},newLoginUser )
        {
            let hearderToken = rootGetters["loginModule/header"]
            let result = await loginUserService.AddNewLoginUser(newLoginUser.Username,newLoginUser.Password,newLoginUser.IsAdmin,newLoginUser.Name,newLoginUser.DepartmentIdList,hearderToken);   
            await dispatch('setApiResult',result)
        },
    async requestUpdateLoginUser({state,commit,dispatch,rootGetters},updateLoginUser)
        {
            let hearderToken = rootGetters["loginModule/header"]
            let result = await loginUserService.UpdateLoginUser(updateLoginUser.LoginId,updateLoginUser.Username,updateLoginUser.Password,updateLoginUser.IsAdmin,updateLoginUser.Name,updateLoginUser.DepartmentIdList,hearderToken);
            await dispatch('setApiResult',result)
        },
    async requestDeleteLoginUser({state,commit,dispatch,rootGetters},deleteLoginUser)
        {
            let hearderToken = rootGetters["loginModule/header"]
            let result = await loginUserService.DeleteLoginUser(deleteLoginUser.LoginId,hearderToken)
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
                    await dispatch('loadLoginUserList')
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
    [type.loadLoginUserList](state, login){
        state.loginUserList = login;
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
    [type.loadCBDepartment](state, department){
        state.cbDepartment = department;
    },
}
export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
}