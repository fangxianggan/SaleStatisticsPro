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
      var path=item.path;
      if(item.path=="/dashboard")
      {
        menu.path="/";
        menu.redirect= '/dashboard',
        menu.children=[{
              path: '/dashboard',
              name: '首页',
              component: () => import('@/views/dashboard/index'),
              meta: { title: '首页', icon: 'dashboard' }
        }];
        menu.component = Layout;
      }else{
        menu.component =() => import(`@/views${path}`)
      }
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
  //console.log(res)
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
        if (roles.includes('admin')) {
          //accessedRoutes = asyncRoutes || []
          accessedRoutes = aa;
        } else {
          console.log("111")
          accessedRoutes = filterAsyncRoutes(asyncRoutes, roles)
        }
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
