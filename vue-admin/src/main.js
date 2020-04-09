import Vue from 'vue'

import 'normalize.css/normalize.css' // A modern alternative to CSS resets

import ElementUI from 'element-ui'
import 'element-ui/lib/theme-chalk/index.css'
import locale from 'element-ui/lib/locale/lang/zh-CN' // lang i18n

import '@/styles/index.scss' // global css
//import '@/assets/icon/iconfont.css';
//import '@/assets/icon/iconfont.js'

import App from './App'
import store from './store'
import router from './router'

import '@/icons' // icon
import '@/permission' // permission control
import $ from 'jquery'

// main.js
import ajaxPlugin from '@/plugins/ajaxPlugin'
Vue.use(ajaxPlugin)
//拖动 dialog
import elDragDialog from '@/directive/el-drag-dialog'
Vue.use(elDragDialog)

//引入echarts
import echarts from 'echarts'
Vue.use(echarts)


//import { server } from './utils/request'
/**
 * If you don't want to use mock-server
 * you want to use MockJs for mock api
 * you can execute: mockXHR()
 *
 * Currently MockJs will be used in the production environment,
 * please remove it before going online! ! !
 */
//import { mockXHR } from '../mock'
//if (process.env.NODE_ENV === 'production') {
//  mockXHR()
//}

// set ElementUI lang to EN
Vue.use(ElementUI, { locale })


Vue.config.productionTip = false




new Vue({
  el: '#app',
  router,
  $,
  store,
  render: h => h(App)
})
