import { login, logout, getInfo, register } from '@/api/user'
import { getToken, setToken, removeToken } from '@/utils/auth'
import router, { resetRouter } from '@/router'
import { Message } from 'element-ui'
const state = {
  token: getToken(),
  name: '',
  avatar: '',
  roles: []
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
  },
  SET_ROLES: (state, roles) => {
    state.roles = roles
  }
}

const actions = {
  // user login
  login({ commit }, userInfo, $this) {
    return new Promise((resolve, reject) => {
      login(userInfo).then(res => {
        let type;
        if (res.resultSign != 0 && res.code == 200) {
          switch (res.resultSign) {
            case 0: type = "success"; break;
            case 1: type = "warning"; break;
            case 2: type = "error"; break;
            case 3: type = "info"; break;
            default: type = "info"; break;
          }
          Message({
            message: res.message,
            type: type,
            duration: 5 * 1000
          })
          $this.loading = false;
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
        //console.log("11111");
        if (!d) {
          reject('验证失败从新登陆!')
        }

        const { roles, name, avatar } = d
        // console.log(roles)

        if (roles.length == 0) {
          reject('该用户未设置角色!')
        }
        //  const { name, avatar } = data
        commit('SET_ROLES', roles)
        commit('SET_NAME', name)
        commit('SET_AVATAR', avatar)
        resolve(d)
      }).catch(error => {
        reject(error)
      })
    })
  },

  // user logout
  logout({ commit, state, dispatch }) {
    return new Promise((resolve, reject) => {
      logout(state.token).then(() => {
        commit('SET_TOKEN', '')
        commit('SET_ROLES', [])
        removeToken()
        resetRouter()
        dispatch('tagsView/delAllViews', null, { root: true })
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
      commit('SET_ROLES', [])
      removeToken()
      resolve()
    })
  },
  //
  register({ commit }, registerForm) {
    return new Promise((resolve, reject) => {
      register(registerForm).then(res => {
        let type;
        if (res.code == 200) {
          switch (res.resultSign) {
            case 0: type = "success"; break;
            case 1: type = "warning"; break;
            case 2: type = "error"; break;
            case 3: type = "info"; break;
            default: type = "info"; break;
          }
          Message({
            message: res.message,
            type: type,
            duration: 3 * 1000
          })
        }
        resolve()
      }).catch(error => {
        reject(error)
      });
    })
  },
  // dynamically modify permissions
  changeRoles({ commit, dispatch }, role) {
    return new Promise(async resolve => {
      const token = role + '-token'

      commit('SET_TOKEN', token)
      setToken(token)

      const { roles } = await dispatch('getInfo')

      resetRouter()

      // generate accessible routes map based on roles
      const accessRoutes = await dispatch('permission/generateRoutes', roles, { root: true })

      // dynamically add accessible routes
      router.addRoutes(accessRoutes)

      // reset visited views and cached views
      dispatch('tagsView/delAllViews', null, { root: true })

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

