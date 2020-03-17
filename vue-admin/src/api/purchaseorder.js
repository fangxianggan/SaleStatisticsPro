

import http from '@/request/http'

//获取订单详情数据
export function getPurchaseOrderInfoViewModelList(row) {
  let url = "/PurchaseOrder/GetPurchaseOrderInfoViewModelList";
  let data = { orderNumber: row.pOrderNum };
  return http(url, data, { method: "get" }) 
}
