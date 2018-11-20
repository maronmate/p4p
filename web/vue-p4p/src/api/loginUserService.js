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
    AddNewLoginUser(username,password,isAdmin,name,departmentIdList,hearderToken)
    {   
        const url = this.data().apiUrl +'/api/LoginUser/InsertLoginUser';
        const config = {
        headers: {              
            'Content-Type': 'application/json',
            'Authorization': hearderToken,
            }
         }
        const requestBody = {
            Username : username,
            Password : password,
            IsAdmin : isAdmin,
            Name : name,
            DepartmentIdList : departmentIdList
        }
        return axios.post(url,requestBody,config); 
    },
    UpdateLoginUser(loginUserId,username,password,isAdmin,name,departmentIdList,hearderToken)
    {
        const url = this.data().apiUrl +'/api/LoginUser/'+loginUserId;
        const config = {
        headers: {              
            'Content-Type': 'application/json',
            'Authorization': hearderToken,
            }
         }
        const requestBody = {
            LoginId : loginUserId,
            Username : username,
            Password : password,
            IsAdmin : isAdmin,
            Name : name,
            DepartmentIdList : departmentIdList,
        }
        return axios.put(url,requestBody,config);
    },
    DeleteLoginUser(loginUserId,hearderToken)
    {

        const url = this.data().apiUrl+'/api/LoginUser/'+loginUserId;
        const config = {
            headers: {              
                'Content-Type': 'application/json',
                'Authorization': hearderToken,
                }
             }
        return axios.delete(url,config);
    },

}
