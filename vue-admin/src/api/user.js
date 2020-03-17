import http from '@/request/http'
import qs from 'qs'
export function login(data) {

  let url = "/Token/Login";
  return http(url, data) 
 
}


/**
 *  单个参数 post 传递
 * @param {any} data
 */
export function logout(data) {
  var url = "/Token/Logout";
  var param = qs.stringify({ "": data });
  return http(url, param, {
    headers: {
      'content-type': 'application/x-www-form-urlencoded'
    }
  })
}


/**
 *  单个参数 post 传递
 * @param {any} data
 */
export function getInfo(data) {
  var url = "/Token/GetUserInfo";
  var param = qs.stringify({ "": data});
  return http(url, param, {
    headers: {
      'content-type': 'application/x-www-form-urlencoded'   
    }})
}
