
// ajaxPlugin.js
import http from '@/request/http'

let ajaxPlugin = {}

ajaxPlugin.install = Vue => {
  Vue.prototype.$ajax = http

  /**
 * 定义后台请求方法验证方法
 * @param {any} obj
 * {
 * url:"",
 * data:{}，
 * value:"",
 * }
 */
  Vue.prototype.checkRemoteData = (obj, callback) => {
    let url = obj.url;
    let data = obj.data;
    let method = obj.method;
    if (obj.value == "" || obj.value == undefined) return false;
    http(url, data, { method: method }).then(response => {
      if (response.resultSign == 1) {
        callback(new Error(response.message))
      } else {
        callback()
      }
    })
  }
}

export default ajaxPlugin

