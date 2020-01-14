
import request from '@/utils/request'

/**
 * 获取所有的销售订单
 * @param {any} params
 */
export function getSaleOrderList(query)
{
  console.log(query);
  return request({
    url: '/SaleOrder/GetList',
    method: 'post',
    params:query
  })
}
