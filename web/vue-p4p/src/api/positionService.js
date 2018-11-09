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
}