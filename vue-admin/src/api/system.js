import request from '@/utils/request'

/**
 * 获取供应商列表
 * @param {any} data
 */
export function getBusinessList(data) {
  return request({
    headers: {
      'content-type': 'application/json',
      "accept": "application/json",
    },
    url: '/Business/_GetPageList',
    method: 'post',
    data 
  });
}

/**
 * 供应商 添加修改保存
 * @param {any} data
 */
export function postBusinessSaveData(data)
{
  return request({
    headers: {
      'content-type': 'application/json',
      "accept": "application/json",
    },
    url: '/Business/_SaveData',
    method: 'post',
    data
  })
}

/**
 * 删除 供应商
 * @param {any} code
 */
export function delBusinessData(id)
{
  return request({
    url: '/Business/_DelData',
    method: 'get',
    params: { id: id }
  })
}
/**
 * 获取供应商select
 *
 */
export function getSelectBusinessData() {
  return request({
    headers: {
      'content-type': 'application/json',
      "accept": "application/json",
    },
    url: '/Business/getSelectBusinessData',
    method: 'get',

  });
}




/**
 * 获取产品类型列表
 * @param {any} data
 */
export function getProductTypeList(data) {
  return request({
    headers: {
      'content-type': 'application/json',
      "accept": "application/json",
    },
    url: '/ProductType/_GetPageList',
    method: 'post',
    data
  });
}

/**
 * 产品类型 添加修改保存
 * @param {any} data
 */
export function postProductTypeSaveData(data) {
  return request({
    headers: {
      'content-type': 'application/json',
      "accept": "application/json",
    },
    url: '/ProductType/_SaveData',
    method: 'post',
    data
  })
}

/**
 * 删除 产品类型
 * @param {any} code
 */
export function delProductTypeData(id) {
  return request({
    url: '/ProductType/_DelData',
    method: 'get',
    params: {id:id}
  })
}

/**
 * 获取商品系列select
 * @param {any} productCode
 */
export function getSelectProductTypeData(productCode) {
  return request({
    headers: {
      'content-type': 'application/json',
      "accept": "application/json",
    },
    url: '/ProductType/getSelectProductTypeData',
    method: 'get',
    params: { "productCode": productCode}
  });
}

/**
 * 获取装运仓列表
 * @param {any} data
 */
export function getTransferBinList(data) {
  return request({
    headers: {
      'content-type': 'application/json',
      "accept": "application/json",
    },
    url: '/TransferBin/_GetPageList',
    method: 'post',
    data
  });
}

/**
 * 装运仓 添加修改保存
 * @param {any} data
 */
export function postTransferBinSaveData(data) {
  return request({
    headers: {
      'content-type': 'application/json',
      "accept": "application/json",
    },
    url: '/TransferBin/_SaveData',
    method: 'post',
    data
  })
}

/**
 * 删除 装运仓
 * @param {any} code
 */
export function delTransferBinData(code) {
  return request({
    url: '/TransferBin/_DelData',
    method: 'get',
    params: code
  })
}

/**
 * 获取转运仓select
 *
 */
export function getSelectTransferBinData() {
  return request({
    headers: {
      'content-type': 'application/json',
      "accept": "application/json",
    },
    url: '/TransferBin/getSelectTransferBinData',
    method: 'get',

  });
}
