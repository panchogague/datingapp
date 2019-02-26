import Home from '../components/Home'
import Register from '../components/Auth/Register'
import VueRouter from 'vue-router'
import MemberList from '../components/pages/member-list/MemberList'
import Messages from '../components/pages/messages/Messages'
import Lists from '../components/pages/lists/Lists'
import Vue from 'vue'

Vue.use(VueRouter)

const routesConfig =  [
    {path:'/', component:Home},
    {path:'/register', component:Register},
    {path:'/member', component:MemberList, meta : {requiresAuth:true}},
    {path:'/messages', component:Messages, meta : {requiresAuth:true}},
    {path:'/lists', component:Lists, meta : {requiresAuth:true}}
];
export const router = new VueRouter({routes:routesConfig});