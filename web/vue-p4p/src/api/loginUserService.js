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
    async login(username,password)
    {
        const requestBody = {
            grant_type:'password',
            username: username,
            password: password
        }
        var querystring = require('querystring');

        const config = {
        headers: {              
            'Content-Type': 'application/x-www-form-urlencoded',
            }
        }
        const url = this.data().apiUrl +'/login/token';
        var d = this;
        await axios.post(url,querystring.stringify(requestBody),config).then(
            response => {
                if(response.status == 200)
                {
                    d.result = response;
                    
                }
            },
                error => {
                    d.result = error;
                  //  d.result.errorData = error;
            }

        )
        return d.result;
    }

}
