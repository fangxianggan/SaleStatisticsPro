import http from '@/request/http'

//获取订单详情数据
export function getSaleOrderInfoViewModelList(row) {
  let url = "/SaleOrder/GetSaleOrderInfoViewModelList";
  let data = { orderNumber: row.sOrderNum };
  return http(url, data, { method: "get" })
}
