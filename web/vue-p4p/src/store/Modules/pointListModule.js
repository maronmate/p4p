import pointService from '@/api/pointService'
import subDivisionService from '@/api/subDivisionService'
import departmentService from '@/api/departmentService'
import positionService from '@/api/positionService'
import userService from '@/api/userService'

const type = {
    loadUserPointList:'LOAD_USER_POINT_LIST',  
    loadUserPointHeader:'LOAD_USER_POINT_HEADER',
    onLoadAPI:'ON_LOAD_API',

    loadDdlDepartment:'LOAD_DDL_DEPARTMENT',
    loadDdlPosition:'LOAD_DDL_POSITION',
    loadDdlSubDivision:'LOAD_DDL_SUBDIVISION',
    loadDdlUser:'LOAD_DDL_USER',

    showEdit: 'SHOW_EDIT_FROM',
    saveResultMsg: 'SET_ERROR_MESSAGE',
    showResultMsg: 'SHOW_ERROR_MESSAGE',

    showTable: "SHOW_TABLE",
    setDynamicHeader :"SET_DYNAMIC_HEADER"

}
const state = {
    userPointList:[],
    userPointHeader:[],
    dynamicHeader:[],
    showEditAddForm : false,
    saveResultMsg : null,
    showResultMsg : false,

    ddlDepartment :[],
    ddlPosition :[],
    ddlSubDivision :[],
    ddlUser :[],

    onLoad : false,

    showTable: false,
}
const getters = {
    dynamicHeader(state, getters) {
        return state.dynamicHeader;
    },
    showTable(state, getters) {
        return state.showTable;
    },
    userPointList(state, getters) {
        return state.userPointList;
    },
    userPointHeader(state, getters) {
        return state.userPointHeader;
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
    ddlUser(state, getters) {
        return state.ddlUser;
    },
    onLoad(state, getters) {
        return state.onLoad;
    },
}
const actions = {

    async loadDDLDepartment({ state, commit,rootGetters }) {
        let hearderToken = rootGetters["loginModule/header"]
        let isAdmin = rootGetters["loginModule/isAdmin"]
        let loginId = rootGetters["loginModule/loginId"]

        if(isAdmin === true)
        {
            let departments = await departmentService.GetAllDepartmentDDL(true,hearderToken);
            commit(type.loadDdlDepartment,departments)
        }
        else
        {
            let departments = await departmentService.GetAllDepartmentDDLByLogin(true,hearderToken,loginId);       
            commit(type.loadDdlDepartment,departments)
        }
    },
    async loadDDLPosition({ state, commit,rootGetters },departmentId) {
        let hearderToken = rootGetters["loginModule/header"]
        let positions = await positionService.GetPositionDDLbyDepartment(true,departmentId,hearderToken);
        commit(type.loadDdlPosition,positions)
    },
    async loadDDLSubdivision({ state, commit,rootGetters },departmentId) {
        let hearderToken = rootGetters["loginModule/header"]
        let subdivisions = await subDivisionService.GetSubdivisionDDLbyDepartment(true,departmentId,hearderToken);
        commit(type.loadDdlSubDivision,subdivisions)
    },
    async loadDDLUser({ state, commit,rootGetters },filter) {
        let hearderToken = rootGetters["loginModule/header"]
        let isAdmin = rootGetters["loginModule/isAdmin"]
        if(isAdmin=== true)
        {
            let users = await userService.GetUserDDLbyFilter(true,filter.departmentId,filter.subdivisionId,filter.positionId,hearderToken);
            commit(type.loadDdlUser,users)
        }
        else
        {
            if(filter.departmentId && filter.departmentId>0)
            {
                let users = await userService.GetUserDDLbyFilter(true,filter.departmentId,filter.subdivisionId,filter.positionId,hearderToken);
                commit(type.loadDdlUser,users)
            }
            else
            {
                commit(type.loadDdlUser,[])
            }
        }
    },
    async loadUserPointList({ state, commit,rootGetters,dispatch },filter)
    {
        let hearderToken = rootGetters["loginModule/header"]
        let isAdmin = rootGetters["loginModule/isAdmin"]
        if(isAdmin=== true)
        {
            let userpoints = await pointService.SearchUserPoint(filter.departmentId,filter.subdivisionId,filter.positionId,filter.userId,filter.ymStart,filter.ymEnd,hearderToken);
            let data = userpoints.data;
            if (!data)
                data = [] 
            commit(type.loadUserPointList,data)
            await dispatch('CreateHeader',data )
        }
        else
        {
            if(filter.departmentId && filter.departmentId>0)
            {
                let userpoints = await pointService.SearchUserPoint(filter.departmentId,filter.subdivisionId,filter.positionId,filter.userId,filter.ymStart,filter.ymEnd,hearderToken);
                let data = userpoints.data;
                if (!data)
                    data = [] 
                commit(type.loadUserPointList,data)
                await dispatch('CreateHeader',data)
            }
            else
            {
                commit(type.loadUserPointList,[])
                await dispatch('CreateHeader',[])
            }
        }
    },
    CreateHeader({ state, commit,rootGetters },data)
    {
        let header = [];
        let show = false;
        let dynamicColumn =[];

        if(data && data[1])
        {
            for(var element in data[1])
            {
                if(element === 'Name')
                {
                    let column = {key:element,label:'ชื่อ-สกุล',sortable: true, thStyle :"{ color: red}"}
                    header.push(column);
                }
                else if(element === 'DepartmentName')
                {
                    let column = {key:element,label:'แผนก',sortable: true}
                    header.push(column);
                }
                else if(element === 'SubdivisionName')
                {
                    let column = {key:element,label:'หน่วยงาน',sortable: true}
                    header.push(column);
                }
                else if(element === 'PositionName')
                {
                    let column = {key:element,label:'ตำแหน่ง',sortable: true}
                    header.push(column);
                }
                else if(element === 'TragetPoint')
                {
                    let column = {key:element,label:'แต้มประกัน',sortable: true}
                    header.push(column);
                }
                else
                {
                    let date = new Date(element);
                    if(date != 'Invalid Date')
                    {
                        let column = {key:element,label:element,sortable: true,tdClass:"column-ym"}
                        header.push(column);
                        let dynamic = {columnName:element}
                        dynamicColumn.push(dynamic);
                    }                  
                }
                show = true;
            }

        }
        commit(type.loadUserPointHeader,header)
        commit(type.showTable,show)
        commit(type.setDynamicHeader,dynamicColumn)
    }


}
const mutations = {
    [type.setDynamicHeader](state, dynamic){
        state.dynamicHeader = dynamic;
    },
    [type.showTable](state, show){
        state.showTable = show;
    },
    [type.loadUserPointList](state, userPoint){
        state.userPointList = userPoint;
    },
    [type.loadUserPointHeader](state, header){
        state.userPointHeader = header;
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
    [type.loadDdlUser](state, ddlUser){
        state.ddlUser = ddlUser;
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