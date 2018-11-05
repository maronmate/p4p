import axios from 'axios';
import * as readConfig  from '@/helper/index.js';
export default{
    data(){
        return {
        apiUrl : readConfig.getApiHost(),
        keyCode : readConfig.getKeyLocal(),
        }
    },
    async getAllDeathLine(hearderToken)
    {
        const url = this.data().apiUrl+'/api/DeathLine/GetDeathLineList?numberOfRow=0';
        const config = {
            headers: {              
                'Content-Type': 'application/json',
                'Authorization': hearderToken
                }
             }
        return axios.get(url,config);
        
    },
    DeleteDeathLine(ym,hearderToken)
    {
        const url = this.data().apiUrl +'/api/DeathLine/DeleteDeathLine';
        const config = {
        headers: {              
            'Content-Type': 'application/json',
            'Authorization': hearderToken,
            }
         }
        const requestBody = {
            YM: ym,
        }
        return axios.post(url,requestBody,config); 
    },
    AddNewDeathLine(yM,deathLineDate,loginId,hearderToken)
    {   
        const url = this.data().apiUrl +'/api/DeathLine/InsertDeathLine';
        const config = {
        headers: {              
            'Content-Type': 'application/json',
            'Authorization': hearderToken,
            }
         }
        const requestBody = {
            YM: yM,
            UserId: loginId,
            DeathLineDate : deathLineDate
        }
        return axios.post(url,requestBody,config); 
    },
    UpdateDeathLine(yM,deathLineDate,loginId,hearderToken)
    {
        const url = this.data().apiUrl +'/api/DeathLine/UpdateDeathLine';
        const config = {
        headers: {              
            'Content-Type': 'application/json',
            'Authorization': hearderToken,
            }
         }
        const requestBody = {
            YM: yM,
            UserId: loginId,
            DeathLineDate : deathLineDate
        }
        return axios.post(url,requestBody,config);
    },
}