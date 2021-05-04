import Vue from "vue";

import App from "./App.vue";
import axios from 'axios'

// import http from './http-constants'
import Router from "./routes";
import i18n from './lang/i18n'
import VueResource from 'vue-resource';
import Auth from './plugins/Auth.js';
import store from './store';
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
import BootstrapVue from 'bootstrap-vue'
// import ElementUI from 'element-ui'
// Vue.use(ElementUI);
import Vuelidate from 'vuelidate'
import FlagIcon from 'vue-flag-icon'
import Vuesax from 'vuesax'
import 'vuesax/dist/vuesax.css' //Vuesax styles
// import store from './store'
import HenryHub from './hub'
import VueJwtDecode from 'vue-jwt-decode'
import VuePageTransition from 'vue-page-transition'

import VueApexCharts from 'vue-apexcharts'
import VueCookies from 'vue-cookies'
import { TreeGridPlugin } from "@syncfusion/ej2-vue-treegrid";
import VueSweetalert2 from 'vue-sweetalert2';
import { GridPlugin } from '@syncfusion/ej2-vue-grids';
import { CheckBoxPlugin } from "@syncfusion/ej2-vue-buttons";
import { SwitchPlugin } from "@syncfusion/ej2-vue-buttons";
import { DatePickerPlugin } from '@syncfusion/ej2-vue-calendars';
import { DateTimePickerPlugin } from "@syncfusion/ej2-vue-calendars";

import { ButtonPlugin } from '@syncfusion/ej2-vue-buttons';
import { TooltipPlugin } from "@syncfusion/ej2-vue-popups";
import VueSSE from 'vue-sse';
import { RadioButtonPlugin } from "@syncfusion/ej2-vue-buttons";
import { ToastPlugin } from '@syncfusion/ej2-vue-notifications';
import VueToast from 'vue-toast-notification';
// Import one of available themes
import 'vue-toast-notification/dist/theme-default.css';
import VTooltip from 'v-tooltip'
import moment from 'moment'
import VueBreadcrumbs from 'vue-2-breadcrumbs';

Vue.use(VueBreadcrumbs);

Vue.filter('formatDate', function(value) {
  if (value) {
    return moment(String(value)).format('YYYY-MM-DD')
  }
});

Vue.filter('formatDated', function(value) {
  if (value) {
    return moment(String(value)).format('YYYY-MM-DD LTS')
  }
});

Vue.filter('formatDated2', function(value) {
  if (value) {
    return moment(String(value)).format('MMMM')
  }
});
Vue.use(DateTimePickerPlugin);

Vue.use(VTooltip)
Vue.use(VueToast, {
  // One of options
  position: 'top'
})
Vue.use(ToastPlugin);
Vue.use(RadioButtonPlugin);

Vue.use(VueSSE);

Vue.use(TooltipPlugin);
Vue.use(ButtonPlugin);
Vue.use(DatePickerPlugin);
Vue.use(SwitchPlugin);
Vue.use(CheckBoxPlugin);
Vue.use(GridPlugin);
Vue.use(VueSweetalert2);
Vue.use(TreeGridPlugin);
Vue.use(VueCookies)
// axios.defaults.headers.get['Accepts'] = 'application/json'
Vue.$cookies.set("Lang","en");
window.axios = axios;
// Vue.use(http)
Vue.use(VuePageTransition)
Vue.use(Vuesax, {
  // options here
})
Vue.use(HenryHub)
Vue.use(FlagIcon)
// Vue.use(QuestionHub)
Vue.use(VueApexCharts)
// import 'element-ui/lib/theme-chalk/index.css'

// import './custom.scss'
Vue.use(Vuelidate)
Vue.use(BootstrapVue)
Vue.use(Auth);
Vue.use(VueResource);
Vue.prototype.$eventHub = new Vue();

// VueJwtDecode.decode(localStorage.getItem("authToken"))
axios.defaults.baseURL = 'http://10.4.5.174:91'
// axios.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded'
axios.defaults.headers.post['Content-Type'] = 'application/json; charset=utf-8'
axios.interceptors.request.use(function (config) {
  config.headers = {
    'Authorization': 'Bearer ' + Vue.auth.getToken()
  }
  config.headers.post = {
    'Content-Type': 'application/json; charset=utf-8'
  }
    return config;
  }, function (error) {
  // Do something with request error
    return Promise.reject(error);
});


Vue.http.interceptors.push(function (request, next) {
  if (request.url[0] === '/') {
    request.url = process.env.API + request.url;

    var token = Vue.auth.getToken();
    if (token)
    request.headers.set('Authorization', 'Bearer ' + token);
  }

  next(function (response) {
    if (response.status == 422) {
      response.body.errors.forEach(function (e) {
        alertify.error(e);
      });
    }
  });
});
//configure route guards
Router.beforeEach(function (to, from, next) {
  //prevent access to 'requiresGuest' routes;
  if (to.matched.some(function (record) { return record.meta.requiresGuest })
    && Vue.auth.loggedIn()) {
    next({
      path: '/home'
    });
  }
  else if (to.matched.some(function (record) { return record.meta.requiresAuth  })
    && !Vue.auth.loggedIn()) {
    next({
      path: '/login',
      query: { redirect: to.fullPath }
    });
  }
  else {
    next(); // make sure to always call next()!
  }
});
new Vue({
  // el: "#app",
  router: Router,
  i18n,
  store: store,
  template: "<App/>",
  components: { App },
  render: h => h(App)
}).$mount('#app');

// export default app
