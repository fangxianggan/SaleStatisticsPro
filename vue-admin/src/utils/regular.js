/**
 * Created by PanJiaChen on 16/11/18.
 */




/**
 * 基础 正则校验的规则
 */

export default {
  /**
   * 手机号校验
   * @param {value} value 
   */
  validateIsPhone(value) {
    const reg = /^1[3|4|5|7|8][0-9]{9}$/;
    return reg.test(value)
  },
  /**
   * 密码校验
   * 至少8个字符，至少1个字母，1个数字：
   * @param {value} value 
   */
  validatePassword(value)
  {
    const reg=/^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$/;
    return reg.test(value)
  }

}
