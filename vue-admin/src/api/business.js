


/**
 * 获取供应商列表
 * @param {any} data
 */
export function _getPageList(data) {
  return request({
    headers: {
      'content-type': 'application/json',
      "accept": "application/json",
    },
    url: '/Product/_GetPageList',
    method: 'post',
    data
  });
}



/**
 * 供应商 添加修改保存
 * @param {any} data
 */
export function _postSaveData(data) {
  return request({
    headers: {
      'content-type': 'application/json',
      "accept": "application/json",
    },
    url: '/Product/_SaveData',
    method: 'post',
    data
  })
}

/**
 * 删除 供应商
 * @param {any} code
 */
export function _delData(id) {
  return request({
    url: '/Product/_DelData',
    method: 'get',
    params: { id: id }
  })
}
/**
 * 获取供应商
 *
 */
export function _getSelectData(code) {
  return request({
    headers: {
      'content-type': 'application/json',
      "accept": "application/json",
    },
    url: '/Product/getSelectProductData',
    method: 'get',
    params: { "code": code }
  });
}
