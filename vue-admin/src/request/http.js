import axios from 'axios'
import { MessageBox, Message } from 'element-ui'
import store from '@/store'
import { getToken,setToken} from '@/utils/auth'


//console.log(myAction)
// create an axios instance
const service = axios.create({
  baseURL: process.env.VUE_APP_BASE_API, // url = base url + request url
  withCredentials: false, // send cookies when cross-domain requests
  timeout: 5000, // request timeout
  responseType: 'json',
  headers: {
    'content-type': 'application/json;charset=UTF-8'   //转换为key=value的格式必须增加content-type
  }
})
/* 请求拦截器 */
// request interceptor
service.interceptors.request.use(
  config => {
    // do something before request is sent
    if (config.method === 'post' && config.data.constructor !== FormData) {
      config.data = config.data
    } else {
      config.params = config.params || {}
    }
    if (store.getters.token) {
      // let each request carry token
      // ['X-Token'] is a custom headers key
      // please modify it according to the actual situation
      config.headers['X-Token'] = getToken()
      if (!config.headers['X-Token']) {
        delete config.headers['X-Token']
      }
    }
    return config
  },
  error => {
    // do something with request error
    console.log(error) // for debug
    return Promise.reject(error)
  }
)

// response interceptor
service.interceptors.response.use(
  /**
   * If you want to get http information such as headers or status
   * Please return  response => response
  */

  /**
   * Determine the request status by custom code
   * Here is just an example
   * You can also judge the status by HTTP Status Code
   */
  response => {
    const res = response.data

    // if the custom code is not 20000, it is judged as an error.
    if (res.code !== 200) {
      Message({
        message: res.message || 'Error',
        type: 'error',
        duration: 5 * 1000
      })

      // 50008: Illegal token; 50012: Other clients logged in; 50014: Token expired;
      if (res.code === 50008 || res.code === 50012 || res.code === 50014) {
        // to re-login
        MessageBox.confirm('You have been logged out, you can cancel to stay on this page, or log in again', 'Confirm logout', {
          confirmButtonText: 'Re-Login',
          cancelButtonText: 'Cancel',
          type: 'warning'
        }).then(() => {
          store.dispatch('user/resetToken').then(() => {
            location.reload()
          })
        })
      }
      return Promise.reject(new Error(res.message || 'Error'))
    } else {
      if(res.token!="")
      {
        setToken(res.token)
      }
      return res
    }
  },
  error => {

    if (error && error.response) {
      switch (error.response.status) {
        case 400:
          console.log('错误请求')
          break;
        case 401:
          console.log('未授权，请重新登录')
          break;
        case 403:
          console.log('拒绝访问')
          break;
        case 404:
          console.log('请求错误,未找到该资源')
          break;
        case 405:
          console.log('请求方法未允许')
          break;
        case 408:
          console.log('请求超时')
          break;
        case 500:
          console.log('服务器端出错')
          break;
        case 501:
          console.log('网络未实现')
          break;
        case 502:
          console.log('网络错误')
          break;
        case 503:
          console.log('服务不可用')
          break;
        case 504:
          console.log('网络超时')
          break;
        case 505:
          console.log('http版本不支持该请求')
          break;
        default:
          console.log(`连接错误${error.response.status}`)
      }
    } else {
      console.log('连接到服务器失败')
    }
    console.log('err' + error) // for debug
    Message({
      message: error.message,
      type: 'error',
      duration: 5 * 1000
    })
    return Promise.reject(error)
  }
)


/**
 * 简单封装 
 * @param {any} path
 * @param {any} data
 * @param {any} options
 */
const http = (path, data = {}, options = {}) => {
  
  let url = path
  let method = options.method || 'post'
  let params = { url, method }

  // 后台提供的接口大都是post和get，所以只考虑了这两种请求方式
  if (options.method === 'get') {
    Object.assign(params, { params: data }, options)
  } else {
    // post
    Object.assign(params, { data }, options)
  }
  return service(params)
}

export default http
 
