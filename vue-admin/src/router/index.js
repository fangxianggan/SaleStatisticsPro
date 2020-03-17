import Vue from 'vue'
import Router from 'vue-router'

Vue.use(Router)

/* Layout */
import Layout from '@/layout'

/**
 * Note: sub-menu only appear when route children.length >= 1
 * Detail see: https://panjiachen.github.io/vue-element-admin-site/guide/essentials/router-and-nav.html
 *
 * hidden: true                   if set true, item will not show in the sidebar(default is false)
 * alwaysShow: true               if set true, will always show the root menu
 *                                if not set alwaysShow, when item has more than one children route,
 *                                it will becomes nested mode, otherwise not show the root menu
 * redirect: noRedirect           if set noRedirect will no redirect in the breadcrumb
 * name:'router-name'             the name is used by <keep-alive> (must set!!!)
 * meta : {
    roles: ['admin','editor']    control the page roles (you can set multiple roles)
    title: 'title'               the name show in sidebar and breadcrumb (recommend set)
    icon: 'svg-name'             the icon show in the sidebar
    breadcrumb: false            if set false, the item will hidden in breadcrumb(default is true)
    activeMenu: '/example/list'  if set path, the sidebar will highlight the path you set
  }
 */

/**
 * constantRoutes
 * a base page that does not have permission requirements
 * all roles can be accessed
 */
export const constantRoutes = [
  {
    path: '/login',
    component: () => import('@/views/login/index'),
    hidden: true
  },

  {
    path: '/404',
    component: () => import('@/views/404'),
    hidden: true
  },
  {
    path: '/',
    component: Layout,
    redirect: '/dashboard',
    children: [{
      path: 'dashboard',
      name: '首页',
      component: () => import('@/views/dashboard/index'),
      meta: { title: '首页', icon: 'dashboard' }
    }]
  },
  {
    path: '/order',
    component: Layout,
    redirect: '/example/table',
    name: '订单管理',
    meta: { title: '订单管理', icon: 'example' },
    children: [
      {
        path: 'purchaselist',
        name: '进货列表',
        component: () => import('@/views/order/purchaselist'),
        meta: { title: '进货列表', icon: 'tree' }
      },
      {
        path: 'salelist',
        name: '销售列表',
        component: () => import('@/views/order/salelist'),
        meta: { title: '销售列表', icon: 'tree' }
      },

    ]
  },
  {
    path: '/report',
    component: Layout,
    redirect: '/example/table',
    name: '统计表报',
    meta: { title: '统计表报', icon: 'example' },
    children: [
      {
        path: 'productstatisticslist',
        name: '产品统计报表',
        component: () => import('@/views/report/productstatisticslist'),
        meta: { title: '产品统计报表', icon: 'tree' }
      }
     

    ]
  },
  {
    path: '/system',
    component: Layout,
    redirect: '/example/table',
    name: '系统管理',
    meta: { title: '系统管理', icon: 'form' },
    children: [
      {
        path: 'businesslist',
        name: '供应商列表',
        component: () => import('@/views/system/businesslist'),
        meta: { title: '供应商列表', icon: 'tree' }
      }, {
        path: 'userinfolist',
        name: '用户列表',
        component: () => import('@/views/system/userinfolist'),
        meta: { title: '用户列表', icon: 'tree' }
      },
      //{
      //  path: 'brandlist',
      //  name: '品牌列表',
      //  component: () => import('@/views/system/brandlist'),
      //  meta: { title: '品牌列表', icon: 'tree' }
      //},
      {
        path: 'categorylist',
        name: '产品分类列表',
        component: () => import('@/views/system/categorylist'),
        meta: { title: '产品分类列表', icon: 'tree' }
      },
      {
        path: 'productlist',
        name: '产品列表',
        component: () => import('@/views/system/productlist'),
        meta: { title: '产品列表', icon: 'tree' }
      },
      //{
      //  path: 'specslist',
      //  name: '规格列表',
      //  component: () => import('@/views/system/specslist'),
      //  meta: { title: '规格列表', icon: 'tree' }
      //},
      {
        path: 'expresscompanylist',
        name: '快递公司列表',
        component: () => import('@/views/system/expresscompanylist'),
        meta: { title: '快递公司列表', icon: 'tree' }
      },
      {
        path: 'transferbinlist',
        name: '转运仓列表',
        component: () => import('@/views/system/transferbinlist'),
        meta: { title: '转运仓列表', icon: 'tree' }
      }
    ]
  },
  {
    path: '/logs',
    component: Layout,
    redirect: '/example/table',
    name: '日志管理',
    meta: { title: '日志管理', icon: 'example' },
    children: [
      {
        path: 'sqlloglist',
        name: 'sql日志',
        component: () => import('@/views/logs/sqlloglist'),
        meta: { title: 'sql日志', icon: 'tree' }
      },
      {
        path: 'errorloglist',
        name: 'error日志',
        component: () => import('@/views/logs/errorloglist'),
        meta: { title: 'error日志', icon: 'tree' }
      },

    ]
  },

  // 404 page must be placed at the end !!!
  { path: '*', redirect: '/404', hidden: true },
  {
    path: '/profile',
    component: Layout,
    redirect: '/profile/index',
    hidden: true,
    children: [
      {
        path: 'index',
        component: () => import('@/views/profile/index'),
        name: '个人中心',
        meta: { title: 'Profile', icon: 'user', noCache: true }
      }
    ]
  }
]

const createRouter = () => new Router({
  // mode: 'history', // require service support
  scrollBehavior: () => ({ y: 0 }),
  routes: constantRoutes
})

const router = createRouter()

// Detail see: https://github.com/vuejs/vue-router/issues/1234#issuecomment-357941465
export function resetRouter() {
  const newRouter = createRouter()
  router.matcher = newRouter.matcher // reset router
}

export default router
