
import request from '@/utils/request'
export function validateSimpleCode(value)
{
 
  let url = "/Product/GetSimpleCodeIsExist";
  let data = { "simpleCode": value };
  return request({
    url:url,
    method: 'get',
    params: data
  })
}
