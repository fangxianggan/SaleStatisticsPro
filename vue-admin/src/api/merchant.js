
import http from '@/request/http'
import qs from 'qs'


/**
 *  单个参数 post 传递
 * @param {any} data
 */
export function getMerchantRolePermission(merchantNo) {
  //console.log(roleId);
  var url = "/MerchantInfo/GetMerchantRolePermission";
  var param = qs.stringify({ "": merchantNo });
  return http(url, param, {
    headers: {
      'content-type': 'application/x-www-form-urlencoded'
    }
  })
}


export function getRoleTransferData(merchantNo) {
  //console.log(roleId);
  var url = "/MerchantInfo/GetRoleTransferData";
  var param = qs.stringify({ "": merchantNo });
  return http(url, param, {
    headers: {
      'content-type': 'application/x-www-form-urlencoded'
    }
  })
}

export function setMerchantRolePermission(merchantNo, roleCodes) {
  //console.log(roleId);
  var url = "/MerchantInfo/SetMerchantRolePermission";
  var param = { merchantNo: merchantNo, roleCodes: roleCodes };
  return http(url, param)
}

