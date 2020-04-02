
import http from '@/request/http'
import qs from 'qs'


/**
 *  单个参数 post 传递
 * @param {any} data
 */
export function getRoleMenuPermission(roleCode) {
    //console.log(roleId);
    var url = "/Menu/GetRoleMenuPermission";
    var param = qs.stringify({ "": roleCode });
    return http(url, param, {
      headers: {
        'content-type': 'application/x-www-form-urlencoded'
      }
    })
  }
  
  export function setRoleMenuPermission(roleCode,menuIds) {
    //console.log(roleId);
    var url = "/Menu/SetRoleMenuPermission";
    var param = { roleCode: roleCode,menuIds:menuIds };
    return http(url, param)
  }
  /**
   * 
   * @param {*} roleCode 
   */
  export function getMenuListPermission(roleCodes) {
    //console.log(roleId);
    var url = "/Menu/GetMenuRouterList";
    var param = qs.stringify({ "": roleCodes });
    return http(url, param, {
      headers: {
        'content-type': 'application/x-www-form-urlencoded'
      }
    })
  }