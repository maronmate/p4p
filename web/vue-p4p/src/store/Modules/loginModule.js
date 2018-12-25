import loginUserService from '@/api/loginUserService'
import SecureLS from 'secure-ls'
import isAfter from "date-fns/is_after";
import * as readConfig  from '@/helper/index.js';

const type = {
    updateLoginUser: 'UPDATE_LOGINUSER',
    getLoginResultMsg:'LOGIN_RESULT_MSG'
}
const state = {
    loginUser: null,
    header: null,
    resultMsg:'',
    keyCode: readConfig.getKeyLocal()

}
const getters = {
    header(state, getters) {
        return state.header;
    },
    isAdmin(state, getters) {
        if (state.loginUser) {
            if (state.loginUser.IsAdmin == "True") {
                return true;
            }
        }
        return false;
    },
    loginUser(state, getters) {
        return state.loginUser;
    },
    loginMsg(state, getters) {
        return state.resultMsg;
    },
    loginId(state, getters) {
        if(state.loginUser)
            return state.loginUser.Id;
        else
            return null;
    },
    loginName(state, getters) {
        if(state.loginUser)
            return state.loginUser.Name;
        else
            return null;
    },
    allDepartment(state, getters) {
        if(state.loginUser)
            return state.loginUser.DepartmentList.split(",");
        else
            return null;
    },
    canAccessDepartment: (state, getters) => (departmentId) => {
        let result = false;
        let isadmin = getters.isAdmin;
        if (isadmin) {
            result = true;
        }
        else {
            let departmentList = getters.allDepartment;
            if (departmentList) {
                var isDepartment = departmentList.find(p => p === departmentId)
                result = isDepartment;
            }
        }
        return result;
    }

}
const actions = {
    async setAuthenticationStore({ state, commit }) {
        let now = new Date();
        let keyCode = state.keyCode;
        var ls = new SecureLS({ encodingType: 'Base64', isCompression: false, encryptionSecret: keyCode });
        let token = ls.get('tokenp4p');

        if (token) {
            let tokenExpiryDate = new Date(token.ExpireTime)
            if (isAfter(now, tokenExpiryDate)) {
                console.log("Token expired");
                ls.remove('tokenp4p')
                commit(type.updateLoginUser, null)
            }
            else {
                commit(type.updateLoginUser, token)
            }
        }
    },
    async logout({commit})
    {
        let keyCode = state.keyCode;
        var ls = new SecureLS({ encodingType: 'Base64', isCompression: false, encryptionSecret: keyCode });
        ls.remove('tokenp4p')
        commit(type.updateLoginUser, null)
    },
    async requestLogin({ state, commit, dispatch }, loginRequest) {
        let keyCode = state.keyCode;
        var ls = new SecureLS({ encodingType: 'Base64', isCompression: false, encryptionSecret: keyCode });

        ls.remove('tokenp4p')
        commit(type.updateLoginUser, null)

        let result = await loginUserService.login(loginRequest.username, loginRequest.password);
        if (result && result.status == 200) {
            ls.set('tokenp4p', result.data)
            commit(type.updateLoginUser, result.data)
            commit(type.getLoginResultMsg, '')
        }
        else
        {
            console.log('login error') 
            console.log(result)
            commit(type.getLoginResultMsg, result)
        }
    },
}
const mutations = {
    [type.updateLoginUser](state, resultLogin) {
        if (resultLogin != null) {
            state.header = resultLogin.token_type + ' ' + resultLogin.access_token,
                state.loginUser = resultLogin
        } else {
            state.header = null,
                state.loginUser = null
        }
    },
    [type.getLoginResultMsg](state, resultMsg){
        state.resultMsg = resultMsg;
    },
}
export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
}