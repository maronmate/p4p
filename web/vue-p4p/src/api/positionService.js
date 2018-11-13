import axios from 'axios';
import * as readConfig  from '@/helper/index.js';
export default{
    data(){
        return {
        apiUrl : readConfig.getApiHost(),
        keyCode : readConfig.getKeyLocal(),
        }
    },

    async GetAllPosition(hearderToken)
    {
        const url = this.data().apiUrl+'/api/Position';
        const config = {
            headers: {              
                'Content-Type': 'application/json',
                'Authorization': hearderToken
                }
             }
        return axios.get(url,config);
        
    },
    AddNewPosition(positionName,departmentId,targetPoint,hearderToken)
    {   
        const url = this.data().apiUrl +'/api/Position/InsertPosition';
        const config = {
        headers: {              
            'Content-Type': 'application/json',
            'Authorization': hearderToken,
            }
         }
        const requestBody = {
            PositionName : positionName,
            DepartmentId : departmentId,
            TargetPoint : targetPoint
        }
        return axios.post(url,requestBody,config); 
    },
    UpdatePosition(positionId,positionName,departmentId,targetPoint,hearderToken)
    {
        const url = this.data().apiUrl +'/api/Position/'+positionId;
        const config = {
        headers: {              
            'Content-Type': 'application/json',
            'Authorization': hearderToken,
            }
         }
        const requestBody = {
            PositionId : positionId,
            PositionName : positionName,
            DepartmentId : departmentId,
            TargetPoint : targetPoint
        }
        return axios.put(url,requestBody,config);
    },
    DeletePosition(positionId,hearderToken)
    {

        const url = this.data().apiUrl+'/api/Position/'+positionId;
        const config = {
            headers: {              
                'Content-Type': 'application/json',
                'Authorization': hearderToken,
                }
             }
        return axios.delete(url,config);
    },
    async GetPositionDDLbyDepartment(departmentId,hearderToken)
    {
        if(departmentId == null)
        {
            var ddl = [];
            ddl.push({value: null,text: 'กรุณาเลือกตำแหน่ง',disabled:true})
            return ddl;
        }
        const url = this.data().apiUrl+'/api/Position/GetPositionByDepartmentId/'+departmentId;
        const config = {
            headers: {              
                'Content-Type': 'application/json',
                'Authorization': hearderToken
                }
             }
            var ddl = [];
            ddl.push({value: null,text: 'กรุณาเลือกตำแหน่ง',disabled:true})
            await axios.get(url,config).then(result =>{
                result.data.forEach(item => {
                    ddl.push({value: item.PositionId,text: item.PositionName})
                });              
            })        
            return ddl
    },
}