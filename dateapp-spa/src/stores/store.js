import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'
import VueJWT from 'vuejs-jwt'

Vue.use(VueJWT)
Vue.use(Vuex)

export const store = new Vuex.Store ({
    state:{
        status:'',
        token: localStorage.getItem('token') || '',
        user:{id:'', username:''}
    },
    mutations: {
        auth_request(state){
            state.status = 'loading'
          },
          auth_success(state,{token, userDecode}){
            state.status = 'success'
            state.token = token
            state.user = userDecode
          },
          auth_error(state){
            state.status = 'error'
          },
          logout(state){
            state.status = ''
            state.token = ''
          }
    },
    actions:{
        login({commit}, user){
            return new Promise((resolve, reject) => {
              commit('auth_request')
              axios({url: 'https://localhost:44386/api/auth/login', data: user, method: 'POST' })
              .then(resp => {
                const token = resp.data.token
                const decodeToken=Vue.$jwt.decode(token)
                const userDecode = {id:decodeToken.nameid, username:decodeToken.unique_name}
                localStorage.setItem('token', token)
                axios.defaults.headers.common['Authorization'] = token
                commit('auth_success', {token,userDecode})
                resolve(resp)
              })
              .catch(err => {
                console.error(err)
                commit('auth_error')
                localStorage.removeItem('token')
                reject(err)
              })
            })
        },
        register({commit}, user){
            return new Promise((resolve, reject) => {
              commit('auth_request')
              axios({url: 'https://localhost:44386/api/auth/register', data: user, method: 'POST' })
              .then(resp => {
                const token = resp.data.token
                const user = resp.data.user
                localStorage.setItem('token', token)
                axios.defaults.headers.common['Authorization'] = token
                commit('auth_success', token, user)
                resolve(resp)
              })
              .catch(err => {
                commit('auth_error', err)
                localStorage.removeItem('token')
                reject(err)
              })
            })
          },
          logout({commit}){
            return new Promise((resolve, reject) => {
              commit('logout')
              localStorage.removeItem('token')
              delete axios.defaults.headers.common['Authorization']
              resolve()
            })
          },
          setTokenUser({commit}){
            const token = localStorage.getItem('token')
            const decodeToken=Vue.$jwt.decode(token)
            const userDecode = {id:decodeToken.nameid, username:decodeToken.unique_name}
            commit('auth_success',{token,userDecode})
          }
    },
    getters: {
        isLoggedIn: state => !!state.token,
        authStatus: state => state.status,
        userName: state => state.user.username
    }
})