import axios from 'axios';
import * as readConfig  from '@/helper/index.js';
export default{
    data(){
        return {
        apiUrl : readConfig.getApiHost(),
        keyCode : readConfig.getKeyLocal(),
        }
    },
    async GetAllUser(hearderToken)
    {
        const url = this.data().apiUrl+'/api/User';
        const config = {
            headers: {              
                'Content-Type': 'application/json',
                'Authorization': hearderToken
                }
             }
        return axios.get(url,config);
        
    },
    AddNewUser(name,lastName,positionId,subdivisionId,birthDate,startDate,enabled,hearderToken)
    {   
        const url = this.data().apiUrl +'/api/User/InsertUser';
        const config = {
        headers: {              
            'Content-Type': 'application/json',
            'Authorization': hearderToken,
            }
         }
        const requestBody = {
            Name : name,
            LastName : lastName,
            PositionId : positionId,
            SubdivisionId : subdivisionId,
            BirthDate : birthDate,
            StartDate : startDate,
            Enabled:enabled
        }
        return axios.post(url,requestBody,config); 
    },
    UpdateUser(id,name,lastName,positionId,subdivisionId,birthDate,startDate,enabled,hearderToken)
    {
        const url = this.data().apiUrl +'/api/User/'+id;
        const config = {
        headers: {              
            'Content-Type': 'application/json',
            'Authorization': hearderToken,
            }
         }
        const requestBody = {
            UserId : id,
            Name : name,
            LastName : lastName,
            PositionId : positionId,
            SubdivisionId : subdivisionId,
            BirthDate : birthDate,
            StartDate : startDate,
            Enabled:enabled
        }
        return axios.put(url,requestBody,config);
    },
    DeleteUser(id,hearderToken)
    {

        const url = this.data().apiUrl+'/api/User/'+id;
        const config = {
            headers: {              
                'Content-Type': 'application/json',
                'Authorization': hearderToken,
                }
             }
        return axios.delete(url,config);
    },

}