import axios from 'axios';
import * as readConfig  from '@/helper/index.js';
export default{
    data(){
        return {
        apiUrl : readConfig.getApiHost(),
        keyCode : readConfig.getKeyLocal(),
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