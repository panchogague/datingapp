import axios from 'axios';
import {URLAPI} from '../config/variables.js';

export default{
    getMembers(pageNumber,pageSize){
        return axios({url: URLAPI+'users', method: 'GET' ,
            params: {
                pagenumber: pageNumber,
                pagesize:pageSize
                    }
                 })
        .then(resp => {
            var pagination={};
            if(resp.headers["pagination"]){
                pagination =JSON.parse(resp.headers["pagination"]);
            }
            var result = {
                data: resp.data,
                pagination: pagination
            }
          
            return result;
        })
        .catch((err)=> 
        {
            console.log(err);
            throw error.response;
        });
    },
    getMember(id){
        return axios({url: URLAPI+'users/'+id, method: 'GET' })
        .then(resp => {
            return resp.data;
        })
        .catch((err)=> 
        {
            console.log(err);
            throw error.response;
        });
    },
    updateMember(id,member){
        return  axios({url: URLAPI+'users/'+id, data: member, method: 'PUT' })
    },
    addPicture(id,photo){
        console.log(photo);
        return  axios({url: URLAPI+'photos/'+id, data: photo, method: 'POST' }/*,{
            headers: {
                'Content-Type': 'multipart/form-data'
            }}*/);
    },
    setPictureMain(userId,photoId){
        return  axios({url: URLAPI+'photos/'+userId+'/setMain/'+photoId, method: 'POST' })
    },
    deletePicture(userId,photoId){
        return  axios({url: URLAPI+'photos/'+userId+'/deletePhoto/'+photoId, method: 'DELETE' })
    }
}