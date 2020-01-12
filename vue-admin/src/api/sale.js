
import request from '@/utils/request'

/**
 * 获取所有的销售订单
 * @param {any} params
 */
export function getSaleOrderList(params)
{
  return request({
    url: '/sale/getSaleOrderList',
    method: 'post',
    params
  })
}
