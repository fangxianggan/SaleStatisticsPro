/* Layout */
import Layout from '@/layout'
import { getMenuListPermission } from '@/api/menu'
import { asyncRoutes,  constantRoutes } from '@/router'


/**
 * 后台查询的菜单数据拼装成路由格式的数据
 * @param routes
 */
export function generaMenu(routes, data) {
  data.forEach(item => {
    // alert(JSON.stringify(item))
    const menu = {
      path: item.path === '#' ? item.Id + '_key' : item.path,
      component: item.path === '#' ? Layout : () => import('@/views'+item.path+'/index'),
      children: [],
      name: 'menu_' + item.Id,
      meta: { title: item.menuName, id: item.Id, roles: ['admin'] }
    }
    if (item.children) {
      generaMenu(menu.children, item.children)
    }
    routes.push(menu)
  })
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
      const loadMenuData = []  
      getMenuListPermission(roles).then(res => {
        Object.assign(loadMenuData,  res.data)
        generaMenu(asyncRoutes, loadMenuData)
      
        let accessedRoutes
        if (roles.includes('admin')) {
          // alert(JSON.stringify(asyncRoutes))
          accessedRoutes = asyncRoutes || []
        } else {
          accessedRoutes = filterAsyncRoutes(asyncRoutes, roles)
        }
        commit('SET_ROUTES', accessedRoutes)
        resolve(accessedRoutes)
      });
    })
  }
}

export default {
  namespaced: true,
  state,
  mutations,
  actions
}
