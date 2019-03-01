import axios from 'axios';
import {URLAPI} from '../config/variables.js';

export default{
    getMembers(){
        return axios({url: URLAPI+'users', method: 'GET' })
        .then(resp => {
            return resp.data;
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
    }
}