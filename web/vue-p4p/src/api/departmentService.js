import axios from 'axios';
import * as readConfig  from '@/helper/index.js';
export default{
    data(){
        return {
        apiUrl : readConfig.getApiHost(),
        keyCode : readConfig.getKeyLocal(),
        ddlDepartment:[]
        }
    },
    async GetAllDepartment(hearderToken)
    {
        const url = this.data().apiUrl+'/api/Department';
        const config = {
            headers: {              
                'Content-Type': 'application/json',
                'Authorization': hearderToken
                }
             }
        return axios.get(url,config);
        
    },
    async GetAllDepartmentDDL(hearderToken)
    {
        const url = this.data().apiUrl+'/api/Department/GetDepartmentDDL';
        const config = {
            headers: {              
                'Content-Type': 'application/json',
                'Authorization': hearderToken
                }
             }
            var ddl = [];
            ddl.push({value: null,text: 'กรุณาเลือกแผนก',disabled:true})
            await axios.get(url,config).then(result =>{
                result.data.forEach(item => {
                    ddl.push({value: item.DepartmentId,text: item.DepartmentName})
                });              
            })        
            return ddl
    },
    async GetAllDepartmentCheckBox(hearderToken)
    {
        const url = this.data().apiUrl+'/api/Department/GetDepartmentDDL';
        const config = {
            headers: {              
                'Content-Type': 'application/json',
                'Authorization': hearderToken
                }
             }
            var ddl = [];
            await axios.get(url,config).then(result =>{
                result.data.forEach(item => {
                    ddl.push({value: item.DepartmentId,text: item.DepartmentName})
                });              
            })        
            return ddl
    },

    DeleteDepartment(departmentId,hearderToken)
    {

        const url = this.data().apiUrl+'/api/Department/'+departmentId;
        const config = {
            headers: {              
                'Content-Type': 'application/json',
                'Authorization': hearderToken,
                }
             }
        return axios.delete(url,config);
    },
    AddNewDepartment(name,orderInReport,showInReport,hearderToken)
    {   
        const url = this.data().apiUrl +'/api/Department/InsertDepartment';
        const config = {
        headers: {              
            'Content-Type': 'application/json',
            'Authorization': hearderToken,
            }
         }
        const requestBody = {
            Name: name,
            OrderInReport : orderInReport,
            ShowInReport:showInReport
        }
        return axios.post(url,requestBody,config); 
    },
    UpdateDepartment(departmentId,name,orderInReport,showInReport,hearderToken)
    {
        const url = this.data().apiUrl +'/api/Department/'+departmentId;
        const config = {
        headers: {              
            'Content-Type': 'application/json',
            'Authorization': hearderToken,
            }
         }
        const requestBody = {
            DepartmentId: departmentId,
            Name: name,
            OrderInReport : orderInReport,
            ShowInReport:showInReport
        }
        return axios.put(url,requestBody,config);
    },
}