import request from '@/utils/request'

export function login(data) {
  return request({
    url: '/Token/Login',
    method: 'post',
    data
  })
}

export function getInfo(token) {
  return request({
    url: '/Token/Info',
    method: 'post',
    params: { token }
  })
}

export function logout() {
  return request({
    url: '/Token/Logout',
    method: 'post'
  })
}
