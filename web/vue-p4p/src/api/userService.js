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
    async GetUserDDLbyFilter(canEmpty,departmentId,subdivisionId,positionId,hearderToken)
    {
        
        if(subdivisionId == null)
        {
            subdivisionId = 0;
        }
        if(positionId == null)
        {
            positionId = 0;
        }
        if(departmentId === null)
        {
            var ddl = [];
            ddl.push({value: null,text: '',disabled:false})
            return ddl
        }
        else
        {
            const url = this.data().apiUrl+'/api/User/GetUserDDLByFilter/'+departmentId+'/'+subdivisionId+'/'+positionId;
            const config = {
            headers: {              
                'Content-Type': 'application/json',
                'Authorization': hearderToken
                }
             }
        
            var ddl = [];
            if(canEmpty === true)
                ddl.push({value: null,text: '',disabled:false})
            else
                ddl.push({value: null,text: 'กรุณาเลือกผู้ใช้',disabled:true})

            await axios.get(url,config).then(result =>{
                result.data.forEach(item => {
                    ddl.push({value: item.UserId,text: item.UserFullName})
                });              
            })        
            return ddl
        }
    },
}