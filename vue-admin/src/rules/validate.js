import baseRegular from "@/utils/regular";


/**
 * 空值验证
 */
let checkNull = { required: true, message: '不能为空', trigger: ['blur', 'change'] };
/**
 * 手机号验证
 */
let checkPhoneNumer = {
    required: true,
    validator: function (rule, value, callback) {

        if (value.trim() == "") {
            callback(new Error("请输入手机号"));
            return false;
        }
        if (!baseRegular.validateIsPhone(value)) {
            callback(new Error("手机号有误"));
        } else {
            callback();
        }
    },
    trigger: ['blur', 'change']
}

/**
 * 验证密码
 */
let checkPassword = {
    required: true,
    validator: function (rule, value, callback) {

        if (value.trim() == "") {
            callback(new Error("请输入密码"));
            return false;
        }
        if (!baseRegular.validatePassword(value)) {
            callback(new Error("密码至少8位包含字母和数字"));
        } else {
            callback();
        }
    },
    trigger: ['blur']
}



/**
 * 校验 规则
 */
export default {
    checkPhoneNumer,
    checkNull,
    checkPassword


}
