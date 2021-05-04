<template>
    <div class="limiter">
      <div class="container-login100">
        <div class="wrap-login100 p-l-55 p-r-55 p-t-65 p-b-50">

            <span class="login100-form-title p-b-33">
               KPI System
            </span>

            <!-- <div class="wrap-input100 validate-input" >
              <input @keyup.enter="login" v-model="user.username" class="input100" type="text" name="email" placeholder="Username">
              <span class="focus-input100-1"></span>
              <span class="focus-input100-2"></span>
            </div>

            <div class="wrap-input100 rs1 validate-input" >

              <input class="input100" @keyup.enter="login"  v-model="user.password" type="password" name="pass" placeholder="Password">

            </div> -->
            <div data-v-1d03bb73="" class="input-group mb-3">
              <input data-v-1d03bb73="" @keyup.enter="login" v-model="user.username" type="text" name="username" placeholder="Username" class="form-control">
              <div data-v-1d03bb73="" class="input-group-append">
               <div data-v-1d03bb73="" class="input-group-text">
                 <span >
                   <i class="fas fa-user"></i>
                 </span>
                </div>
              </div>
            </div>

            <div data-v-1d03bb73="" class="input-group mb-3">
              <input @keyup.enter="login"  v-model="user.password" data-v-1d03bb73="" :type="type" name="pass" placeholder="Password" class="form-control">
              <div data-v-1d03bb73="" class="input-group-append">
               <div data-v-1d03bb73="" class="input-group-text">
                 <span style="cursor:pointer" >
                   <i :class="fonticon" @click="showPassword" aria-hidden="true"></i>
                 </span>
                </div>
              </div>
            </div>

            <div class="container-login100-form-btn m-t-20">
              <button class="login100-form-btn" @click="login" @keyup.enter="login">
                Sign in
              </button>

            </div>
            <div class="text-center p-t-45 p-b-4">
              <p class="message">Forgot Password? <a href="#/Forgot-Password">Recovery now</a></p>
            </div>
        </div>
      </div>
    </div>
</template>

<script>
import i18n from '../../lang/i18n';
export default {
   data() {
    return {
      user: {
        username: '',
        password: ''
      },
      fonticon: "fa fa-eye-slash",
      type:'password',
      menuSidebar: 'aaa',
      loggedIn: this.$auth.loggedIn(),
    }
  },
  created(){


  },
  mounted(){
    $('.message a').click(function(){
      $('form').animate({height: "toggle", opacity: "toggle"}, "slow");
    });
  },
  methods: {
    showPassword(){
      if(this.type === 'password') {
          this.type = 'text'
          this.fonticon = 'fa fa-eye'
       } else {
          this.type = 'password'
          this.fonticon = 'fa fa-eye-slash'
       }
    },
    login(){
      let self = this
      axios.post("api/auth/login", self.user )
        .then(function(res) {
          self.$auth.setToken(res.data.token, Date.now() + 14400000 ,res.data.user.Menus ,res.data.user.User.Alias,res.data.user.User.Role); // + 4 hours
          localStorage.setItem("ListOCs", res.data.user.User.ListOCs);
          localStorage.setItem("LevelOC", res.data.user.User.LevelOC);
          localStorage.setItem("Username", res.data.user.User.Username);
          var routerArrUser2 = [
            "/KPITrendView/",
            "/Overview/",
            "/ChartPeriod",
            "/CategoryKPILevel",
            "/Favourite",
            "/Workplace",
            "/Compare",
            "/Dataset",
            "/LateOnUpload",
            "/ListHistoryNotification",
            "/ChartPeriod/ListTasks"
          ]
          var routerArrUser = [
            "/KPITrendView/",
            "/Overview/",
            "/AdminKPI",
            "/AdminUser",
            "/AdminCategory",
            "/AdminOC",
            "/AddUserToLevel",
            "/AdminLevel",
            "/OCCategories",
            "/CategoryKPILevel",
            "/CategoryKPILevelAdmin",
            "/ChartPeriod/ListTasks",
            "/Dataset",
            "/Workplace",
            "/Compare",
            "/LateOnUpload",
            "/ListHistoryNotification",
            "/ChartPeriod",
            "/Favourite",
            "/home"
          ];

          var routerArrAdmin = [
            "/KPITrendView/",
            "/Overview/",
            "/AdminKPI",
            "/AdminUser",
            "/adminCategory",
            "/AdminOC",
            "/AddUserToLevel",
            "/AdminLevel",
            "/OCCategories",
            "/CategoryKPILevelAdmin",
            "/home"
          ];

          var uri = self.$route.query.redirect || "";
          if(self.$route.query.redirect.length != 0 )
          {
            var cpr = routerArrUser2.includes(uri);
            routerArrUser2.forEach((item, index)=>{
               if(uri.includes(item)) cpr = true;
            });

            var roleUser = routerArrUser.includes(uri);
            routerArrUser.forEach((item, index)=>{
               if(uri.includes(item)) roleUser = true;
            });
            var roleAdmin = routerArrAdmin.includes(uri);

            routerArrAdmin.forEach((item, index)=>{
               if(uri.includes(item)) roleAdmin = true;
            });
            if (Number(localStorage.getItem("Role")) == 2 && roleUser && Number(localStorage.getItem("LevelOC")) == 3 ) {
              self.$router.push(uri);
            }else if (Number(localStorage.getItem("Role")) == 2 && cpr && localStorage.getItem("LevelOC") != 3   ) {
              self.$router.push(uri);
            }else if (Number(localStorage.getItem("Role")) == 2 && roleUser && localStorage.getItem("LevelOC") != 3) {
              self.$router.push("/home");
            }else if (Number(localStorage.getItem("Role")) == 1 && cpr == false) {
              self.$router.push("/home");
            }else if (Number(localStorage.getItem("Role")) == 2 && roleAdmin) {
              self.$router.push("/home");
            } else if (Number(localStorage.getItem("Role")) == 1 && roleAdmin  ) {
              self.$router.push(uri);
            } else if (Number(localStorage.getItem("Role")) == 1 && roleUser ) {
              self.$router.push("/home");
            }
          }
          else{
            self.$router.push(uri);
          }
          success("success!");
      }).catch((res) => {
          warning("Username and password are incorrect, please try again!")
        });
    }
  }
}
</script>
<style >
  button {
    padding: auto;
    border: 1px solid green;
    font-size: 15px;
    margin: auto;
  }
</style>

