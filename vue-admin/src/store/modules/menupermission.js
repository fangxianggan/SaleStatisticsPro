/* Layout */
import Layout from '@/layout'
import { getMenuListPermission } from '@/api/menu'
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


export function convertTree11(routers,menuList) {
  routers.forEach(r => {
      menuList.forEach((m, i) => {
          if (m.parentId && m.parentId == r.meta.id) {
              if (!r.children) r.children = []
              m.fullPath = r.meta.fullPath + '/' + m.path
              let menu = {
                  path: m.path,
                  component: () => import('@/views'+r.meta.fullPath+'/'+m.path),
                  meta: { id: m.id, title: m.menuName, fullPath: r.meta.fullPath + '/' + m.path }
              }
              r.children.push(menu)
          }
      })
      if (r.children) convertTree(r.children,menuList)
  })
 
}

const state = {
  routes: [],
  addRoutes: []
}

const mutations = {
  SET_ROUTES: (state, routes) => {
    state.addRoutes = routes
    state.routes = [].concat(routes)
  }
}

const actions = {
  generateRoutes({ commit }, roles) {
    
    return new Promise(resolve => {
      
      let menuRouters = [] //定义一个空数组，这个是用来装真正路由数据的

      getMenuListPermission(roles).then(res=>{

        let menuList = res.data //这是后端的菜单数据
        //下面就要根据后端的菜单数据组装树型路由数据
        //先取出根节点，没有父id的就是根节点
        menuList.forEach((m, i) => {
            if (m.parentId == 0) {
                m.fullPath = '/' + m.path
                let module = {
                    path: '/' + m.path,
                    component: Layout,
                    name:m.menuName,
                    meta: { id: m.id, title: m.menuName, icon: m.icon },
                     children: [
                        {
                            path: '',
                            name:m.menuName,
                            component:() => import('@/views/' + m.path + '/index'),
                            meta: {
                                menuHide: true,
                                title: m.menuName
                            }
                        }
                    ] 
                }
                menuRouters.push(module)
            }
        })

//定义一个递归方法
function convertTree(routers) {
  routers.forEach(r => {
      menuList.forEach((m, i) => {
          if (m.parent_id && m.parent_id == r.meta.id) {
              if (!r.children) r.children = []
              m.fullPath = r.meta.fullPath + '/' + m.path
              let menu = {
                  path: m.path,
                  component: () => import('@/views'+r.meta.fullPath+'/'+m.path),
                  meta: { id: m.id, title: m.title, fullPath: r.meta.fullPath + '/' + m.path }
              }
              r.children.push(menu)
          }
      })
      if (r.children) convertTree(r.children)
  })
}
      
       convertTree(menuRouters);
       console.log(menuRouters);
        commit('SET_ROUTES', menuRouters)
        resolve(menuRouters)
        
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
