<template>
  <div class="login-container">
    <div class="login-wrap" v-show="showLogin">
      <el-form
        ref="loginForm"
        :model="loginForm"
        :rules="loginRules"
        class="login-form"
        auto-complete="on"
        label-position="left"
      >
        <div class="title-container">
          <h3 class="title">订单统计系统</h3>
        </div>

        <el-form-item prop="UserName">
          <span class="svg-container">
            <svg-icon icon-class="mobile"  />
          </span>
          <el-input
            ref="username"
            v-model="loginForm.UserName"
            placeholder="手机号"
            name="UserName"
            type="text"
            tabindex="1"
            auto-complete="on"
          />
        </el-form-item>

        <el-form-item prop="Password">
          <span class="svg-container">
            <svg-icon icon-class="password" />
          </span>
          <el-input
            :key="passwordType"
            ref="password"
            v-model="loginForm.Password"
            :type="passwordType"
            placeholder="密码"
            name="Password"
            tabindex="2"
            auto-complete="on"
            @keyup.enter.native="handleLogin"
          />
          <span class="show-pwd" @click="showPwd">
            <svg-icon :icon-class="passwordType === 'password' ? 'eye' : 'eye-open'" />
          </span>
        </el-form-item>

        <el-button
          :loading="loading"
          type="primary"
          style="width:100%;margin-bottom:30px;"
          @click.native.prevent="handleLogin"
        >登录</el-button>

        <div class="tips" style="float:right;">
          <span style="margin-right:20px;" v-on:click="ToRegister">在线注册</span>
          <span v-on:click="ToPassword">忘记密码</span>
        </div>
      </el-form>
    </div>
    <div class="register-wrap" v-show="showRegister">
      <el-form
        ref="registerForm"
        :model="registerForm"
        :rules="registerRules"
        class="login-form"
        auto-complete="on"
        label-position="left"
      >
        <div class="title-container">
          <h3 class="title">商户注册</h3>
        </div>

        <el-form-item prop="phoneNumber">
          <span class="svg-container">
            <svg-icon icon-class="mobile"  />
          </span>
          <el-input
            ref="phoneNumber"
            v-model="registerForm.phoneNumber"
            placeholder="手机号"
            name="phoneNumber"
            type="text"
            tabindex="1"
            auto-complete="on"
          />
        </el-form-item>
            <el-form-item prop="nickName">
          <span class="svg-container">
            <svg-icon icon-class="user"  />
          </span>
          <el-input
            ref="nickName"
            v-model="registerForm.nickName"
            placeholder="昵称"
            name="nickName"
            type="text"
          />
        </el-form-item>
        <el-form-item prop="password">
          <span class="svg-container">
            <svg-icon icon-class="password" />
          </span>
          <el-input
            :key="passwordType"
            ref="password"
            v-model="registerForm.password"
            :type="passwordType"
            placeholder="输入密码"
            name="password"
            tabindex="2"
            auto-complete="on"
          />
          <span class="show-pwd" @click="showPwd">
            <svg-icon :icon-class="passwordType === 'password' ? 'eye' : 'eye-open'" />
          </span>
        </el-form-item>
        <el-form-item prop="passwordTwo">
          <span class="svg-container">
            <svg-icon icon-class="password" />
          </span>
          <el-input
            :key="passwordType"
            ref="passwordTwo"
            v-model="registerForm.passwordTwo"
            :type="passwordType"
            placeholder="再次输入密码"
            name="passwordTwo"
            tabindex="2"
            auto-complete="on"
          />
          <span class="show-pwd" @click="showPwd">
            <svg-icon :icon-class="passwordType === 'password' ? 'eye' : 'eye-open'" />
          </span>
        </el-form-item>
        <el-button
          :loading="loading"
          type="primary"
          style="width:100%;margin-bottom:30px;"
          @click.native.prevent="handleRegister"
        >注册</el-button>
        <div class="tips">
          <span v-on:click="ToLogin">已有账号？马上登录</span>
        </div>
      </el-form>
    </div>
  </div>
</template>

<script>
import validate from "@/rules/validate";
export default {
  name: "Login",
  data() {
    const checkPasswordTwo = (rule, value, callback) => {
      if (value === "") {
        callback(new Error("请输入密码"));
        return false;
      }
      if (value !== this.registerForm.password) {
        callback(new Error("输入密码不一致"));
      } else {
        callback();
      }
    };
    const checkPhoneNumberExsit = (rule, value, callback) => {
      if (value === "") {
        return false;
      } else {
        var url = "/Token/IsExistPhoneNumber";
        var data = {phoneNumber:value};
        this.$ajax(url,data,{method:"get"}).then(res => {
          if (res.data) {
            callback(new Error("该手机号已经注册过了!"));
          } else {
            callback();
          }
        });
      }
    };
    return {
      loginForm: {
        UserName: "",
        Password: ""
      },
      registerForm: {
        phoneNumber: "",
        nickName:"",
        password: "",
        passwordTwo: ""
      },

      loginRules: {
        UserName: [validate.checkPhoneNumer],
        Password: [validate.checkPassword]
      },
      registerRules: {
        phoneNumber: [
          validate.checkPhoneNumer,
          { required: true, trigger: "blur", validator: checkPhoneNumberExsit }
        ],
        password: [validate.checkPassword],
        passwordTwo: [
          { required: true, trigger: "blur", validator: checkPasswordTwo }
        ]
      },
      loading: false,
      passwordType: "password",
      redirect: undefined,
      showLogin: true,
      showRegister: false
    };
  },
  watch: {
    $route: {
      handler: function(route) {
        this.redirect = route.query && route.query.redirect;
      },
      immediate: true
    }
  },
  methods: {
    showPwd() {
      if (this.passwordType === "password") {
        this.passwordType = "";
      } else {
        this.passwordType = "password";
      }
      this.$nextTick(() => {
        this.$refs.password.focus();
      });
    },
    handleLogin() {
      this.$refs.loginForm.validate(valid => {
        if (valid) {
          this.loading = true;
          this.$store
            .dispatch("user/login", this.loginForm,this)
            .then(res => {
              this.$router.push({ path: this.redirect || "/" });
              //console.log(this.$router);
              this.loading = false;
            })
            .catch(() => {
              this.loading = false;
            });
        } else {
          
          // console.log('error submit!!')
          return false;
        }
      });
    },
    handleRegister() {
      this.$refs.registerForm.validate(valid => {
        if (valid) {
          this.loading = true;
          this.$store
            .dispatch("user/register", this.registerForm)
            .then(res => {
              this.showRegister = false;
              this.showLogin = true;
              this.loading = false;
            })
            .catch(() => {
              this.loading = false;
            });
        } else {
          // console.log('error submit!!')
          return false;
        }
      });
    },
    //注册
    ToRegister() {
      this.showRegister = true;
      this.showLogin = false;
    },
    ToPassword() {},
    ToLogin() {
      this.showRegister = false;
      this.showLogin = true;
    }
  }
};
</script>

<style lang="scss">
/* 修复input 背景不协调 和光标变色 */
/* Detail see https://github.com/PanJiaChen/vue-element-admin/pull/927 */

$bg: #283443;
$light_gray: #fff;
$cursor: #fff;

@supports (-webkit-mask: none) and (not (cater-color: $cursor)) {
  .login-container .el-input input {
    color: $cursor;
  }
}

/* reset element-ui css */
.login-container {
  .el-input {
    display: inline-block;
    height: 47px;
    width: 85%;

    input {
      background: transparent;
      border: 0px;
      -webkit-appearance: none;
      border-radius: 0px;
      padding: 12px 5px 12px 15px;
      color: $light_gray;
      height: 47px;
      caret-color: $cursor;

      &:-webkit-autofill {
        box-shadow: 0 0 0px 1000px $bg inset !important;
        -webkit-text-fill-color: $cursor !important;
      }
    }
  }

  .el-form-item {
    border: 1px solid rgba(255, 255, 255, 0.1);
    background: rgba(0, 0, 0, 0.1);
    border-radius: 5px;
    color: #454545;
  }
}
</style>

<style lang="scss" scoped>
$bg: #2d3a4b;
$dark_gray: #889aa4;
$light_gray: #eee;

.login-container {
  min-height: 100%;
  width: 100%;
  background-color: $bg;
  overflow: hidden;

  .login-form {
    position: relative;
    width: 520px;
    max-width: 100%;
    padding: 160px 35px 0;
    margin: 0 auto;
    overflow: hidden;
  }

  .tips {
    font-size: 14px;
    color: #fff;
    margin-bottom: 10px;

    span {
      &:first-of-type {
        margin-right: 16px;
      }
    }
  }

  .svg-container {
    padding: 6px 5px 6px 15px;
    color: $dark_gray;
    vertical-align: middle;
    width: 30px;
    display: inline-block;
  }

  .title-container {
    position: relative;

    .title {
      font-size: 26px;
      color: $light_gray;
      margin: 0px auto 40px auto;
      text-align: center;
      font-weight: bold;
    }
  }

  .show-pwd {
    position: absolute;
    right: 10px;
    top: 7px;
    font-size: 16px;
    color: $dark_gray;
    cursor: pointer;
    user-select: none;
  }
}
</style>
