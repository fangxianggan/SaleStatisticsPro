import { login, logout, getInfo } from '@/api/user'
import { getToken, setToken, removeToken } from '@/utils/auth'
import { resetRouter } from '@/router'
import {  Message } from 'element-ui'
const state = {
  token: getToken(),
  name: '',
  avatar: ''
}

const mutations = {
  SET_TOKEN: (state, token) => {
    state.token = token
  },
  SET_NAME: (state, name) => {
    state.name = name
  },
  SET_AVATAR: (state, avatar) => {
    state.avatar = avatar
  }
}

const actions = {
  // user login
  login({ commit }, userInfo) {
    return new Promise((resolve, reject) => {
      login(userInfo).then(res => {
        let type;
        if (res.resultSign != 0 && res.code == 200) {
          switch (res.resultSign) {
            case 0:  type = "success"; break;
            case 1: type = "warning"; break;
            case 2: type = "error"; break;
            case 3: type = "info"; break;
            default:type = "info"; break;
          }
          Message({
            message: res.message ,
            type: type,
            duration: 5 * 1000
          })
          return false;
        } 
        commit('SET_TOKEN', res.token)
        setToken(res.token)
        resolve()
      }).catch(error => {
        reject(error)
      });
    })
  },

  // get user info
  getInfo({ commit, state }) {
    return new Promise((resolve, reject) => {
      getInfo(state.token).then(response => {
        const d = response.data
       // console.log(d);
        if (!d) {
          reject('Verification failed, please Login again.')
        }

      //  const { name, avatar } = data

        commit('SET_NAME', d.name)
        commit('SET_AVATAR', d.avatar)
        resolve(d)
      }).catch(error => {
        reject(error)
      })
    })
  },

  // user logout
  logout({ commit, state }) {
    return new Promise((resolve, reject) => {
      logout(state.token).then(() => {
        commit('SET_TOKEN', '')
        removeToken()
        resetRouter()
        resolve()
      }).catch(error => {
        reject(error)
      })
    })
  },

  // remove token
  resetToken({ commit }) {
    return new Promise(resolve => {
      commit('SET_TOKEN', '')
      removeToken()
      resolve()
    })
  }
}

export default {
  namespaced: true,
  state,
  mutations,
  actions
}

