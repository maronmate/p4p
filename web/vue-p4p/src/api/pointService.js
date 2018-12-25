import axios from 'axios';
import * as readConfig  from '@/helper/index.js';
export default{
    data(){
        return {
        apiUrl : readConfig.getApiHost(),
        keyCode : readConfig.getKeyLocal(),
        PageSize : readConfig.getPageSize(),
        result:null,
        resultData:null,
        errorData:null
        }       
    },

    SearchUserPoint(DepartmentId,SubdivisionId,PositionId,UserId,YMStart,YMEnd,hearderToken)
    {   
        const url = this.data().apiUrl +'/api/Point/SearchUserPoints';
        const config = {
        headers: {              
            'Content-Type': 'application/json',
            'Authorization': hearderToken,
            }
         }
        const requestBody = {
            DepartmentId: DepartmentId,
            SubdivisionId : SubdivisionId,
            PositionId:PositionId,
            UserId:UserId,
            YMStart:YMStart,
            YMEnd:YMEnd
        }
        return axios.post(url,requestBody,config); 
    },

    SearchPointCount(DepartmentId,SubdivisionId,PositionId,UserId,YMStart,YMEnd,hearderToken)
    {   
        const url = this.data().apiUrl +'/api/Point/SearchPointCount';
        const config = {
        headers: {              
            'Content-Type': 'application/json',
            'Authorization': hearderToken,
            }
         }
        const requestBody = {
            DepartmentId: DepartmentId,
            SubdivisionId : SubdivisionId,
            PositionId:PositionId,
            UserId:UserId,
            YMStart:YMStart,
            YMEnd:YMEnd
        }
        return axios.post(url,requestBody,config); 
    },

    SearchPoints(DepartmentId,SubdivisionId,PositionId,UserId,YMStart,YMEnd,SortFieldName,SortOnDesc,CurrentPage,hearderToken)
    {   
        const url = this.data().apiUrl +'/api/Point/SearchPoints';
        const config = {
        headers: {              
            'Content-Type': 'application/json',
            'Authorization': hearderToken,
            }
         }
        const requestBody = {
            DepartmentId: DepartmentId,
            SubdivisionId : SubdivisionId,
            PositionId:PositionId,
            UserId:UserId,
            YMStart:YMStart,
            YMEnd:YMEnd,
            SortFieldName:SortFieldName,
            SortOnDesc:SortOnDesc,
            PageSize:this.data().PageSize,
            CurrentPage:CurrentPage
        }
        return axios.post(url,requestBody,config); 
    },

}