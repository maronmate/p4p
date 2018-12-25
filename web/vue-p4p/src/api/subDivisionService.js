import axios from 'axios';
import * as readConfig  from '@/helper/index.js';
export default{
    data(){
        return {
        apiUrl : readConfig.getApiHost(),
        keyCode : readConfig.getKeyLocal(),
        }
    },
    async GetAllSubDivision(hearderToken)
    {
        const url = this.data().apiUrl+'/api/Subdivision';
        const config = {
            headers: {              
                'Content-Type': 'application/json',
                'Authorization': hearderToken
                }
             }
        return axios.get(url,config);
        
    },
    AddNewSubDivision(name,departmentId,orderInDepartment,hearderToken)
    {   
        const url = this.data().apiUrl +'/api/Subdivision/InsertSubdivision';
        const config = {
        headers: {              
            'Content-Type': 'application/json',
            'Authorization': hearderToken,
            }
         }
        const requestBody = {
            Name : name,
            DepartmentId : departmentId,
            OrderInDepartment : orderInDepartment
        }
        return axios.post(url,requestBody,config); 
    },
    UpdateSubDivision(subdivisionId,name,departmentId,orderInDepartment,hearderToken)
    {
        const url = this.data().apiUrl +'/api/Subdivision/'+subdivisionId;
        const config = {
        headers: {              
            'Content-Type': 'application/json',
            'Authorization': hearderToken,
            }
         }
        const requestBody = {
            SubdivisionId : subdivisionId,
            Name : name,
            DepartmentId : departmentId,
            OrderInDepartment : orderInDepartment
        }
        return axios.put(url,requestBody,config);
    },
    DeleteSubDivision(subdivisionId,hearderToken)
    {

        const url = this.data().apiUrl+'/api/Subdivision/'+subdivisionId;
        const config = {
            headers: {              
                'Content-Type': 'application/json',
                'Authorization': hearderToken,
                }
             }
        return axios.delete(url,config);
    },
    async GetSubdivisionDDLbyDepartment(canEmpty,departmentId,hearderToken)
    {
        if(departmentId == null)
        {
            var ddl = [];
            if(canEmpty == true)
                ddl.push({value: null,text: '',disabled:false})
            else
                ddl.push({value: null,text: 'กรุณาเลือกหน่วยงาน',disabled:true})
            return ddl;
        }
        const url = this.data().apiUrl+'/api/Subdivision/GetSubdivisionByDepartmentId/'+departmentId;
        const config = {
            headers: {              
                'Content-Type': 'application/json',
                'Authorization': hearderToken
                }
             }
            var ddl = [];
            if(canEmpty == true)
                ddl.push({value: null,text: '',disabled:false})
            else
                ddl.push({value: null,text: 'กรุณาเลือกหน่วยงาน',disabled:true})
            await axios.get(url,config).then(result =>{
                result.data.forEach(item => {
                    ddl.push({value: item.SubdivisionId,text: item.SubdivisionName})
                });              
            })        
            return ddl
    },
}