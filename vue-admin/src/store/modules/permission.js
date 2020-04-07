import { asyncRoutes, constantRoutes } from '@/router'
import { getMenuListPermission } from '@/api/menu'
import Layout from '@/layout'

/**
 * Use meta.role to determine if the current user has permission
 * @param roles
 * @param route
 */
function hasPermission(roles, route) {
  if (route.meta && route.meta.roles) {
    return roles.some(role => route.meta.roles.includes(role))
  } else {
    return true
  }
}

/**
 * 后台查询的菜单数据拼装成路由格式的数据
 * @param routes
 */
function generaMenu(routes, data) {

  data.forEach(item => {
    const menu = {
      path: item.path,
      children: [],
      name: item.name,
      meta: { title: item.meta.title, icon: item.meta.icon }
    }
    if (item.children.length > 0) {
      menu.component = Layout;
    } else {
      var path = item.path;
      menu.component = () => import(`@/views${path}`)
    }
    if (item.children.length > 0) {
      generaMenu(menu.children, item.children)
    }
    routes.push(menu)
  })
}


/**
 * Filter asynchronous routing tables by recursion
 * @param routes asyncRoutes
 * @param roles
 */
export function filterAsyncRoutes(routes, roles) {
  const res = []
  routes.forEach(route => {
    const tmp = { ...route }
    if (hasPermission(roles, tmp)) {
      if (tmp.children) {
        tmp.children = filterAsyncRoutes(tmp.children, roles)
      }
      res.push(tmp)
    }
  })
  return res
}

const state = {
  routes: [],
  addRoutes: []
}

const mutations = {
  SET_ROUTES: (state, routes) => {
    state.addRoutes = routes
    state.routes = constantRoutes.concat(routes)
  }
}

const actions = {
  generateRoutes({ commit }, roles) {
    return new Promise(resolve => {
      var aa = [];
      getMenuListPermission(roles).then(res => {
        generaMenu(aa, res.data)
        let accessedRoutes
        accessedRoutes = aa;
        //404错误页要最后加载进去
        accessedRoutes.push({ path: '*', redirect: '/404', hidden: true })
        commit('SET_ROUTES', accessedRoutes)
        resolve(accessedRoutes)
      })
    })
  }
}

export default {
  namespaced: true,
  state,
  mutations,
  actions
}
