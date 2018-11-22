import axios from 'axios';
import * as readConfig  from '@/helper/index.js';
export default{
    data(){
        return {
        apiUrl : readConfig.getApiHost(),
        keyCode : readConfig.getKeyLocal(),
        result:null,
        resultData:null,
        errorData:null
        }       
    },
    async GetAllLoginUser(hearderToken)
    {
        const url = this.data().apiUrl+'/api/LoginUser';
        const config = {
            headers: {              
                'Content-Type': 'application/json',
                'Authorization': hearderToken
                }
             }
        return axios.get(url,config);
        
    },

}