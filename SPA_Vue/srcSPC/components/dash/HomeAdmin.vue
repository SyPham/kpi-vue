<template>
  <section class="content">
    <div class="container-fluid">
      <div class="row">
        <div   class="col-12 col-sm-6 col-md-3">
          <a  href="#/AdminLevel/" style="text-decoration: none; ">
            <div  class="info-box demo">
              <span  class="info-box-icon bg-orange elevation-1"><i  class="fa fa-list-ol"></i></span>
              <div  class="info-box-content"><span  class="info-box-text">(1) OC</span> <span  class="info-box-number " style="color: rgb(255, 255, 255);"></span></div>
            </div>
          </a>
        </div>
        <div  class="col-12 col-sm-6 col-md-3">
            <a  href="#/AdminKPI/" style="text-decoration: none;">
                <div  class="info-box demo">
                    <span  class="info-box-icon btn-foursquare elevation-1"><i  class="fas fa-chart-line"></i></span>
                    <div  class="info-box-content"><span  class="info-box-text">(2) KPI</span> <span  class="info-box-number " style="color: rgb(255, 255, 255);"></span></div>
                </div>
            </a>
        </div>
        <div  class="col-12 col-sm-6 col-md-3">
            <a  href="#/AdminCategory/" style="text-decoration: none;">
                <div  class="info-box demo">
                    <span  class="info-box-icon bg-navy elevation-1"><i  class="fab fa-gg-circle"></i></span>
                    <div  class="info-box-content"><span  class="info-box-text">(3) Category</span> <span  class="info-box-number " style="color: rgb(255, 255, 255);"></span></div>
                </div>
            </a>
        </div>
        <div  class="col-12 col-sm-6 col-md-3">
            <a  href="#/OCCategories/" style="text-decoration: none;">
                <div  class="info-box demo">
                    <span  class="info-box-icon bg-danger elevation-1"><i  class="far fa-gem"></i></span>
                    <div  class="info-box-content"><span  class="info-box-text">(4) OC Category</span> <span  class="info-box-number " style="color: rgb(255, 255, 255);"></span></div>
                </div>
            </a>
        </div>
        <div  class="col-12 col-sm-6 col-md-3">
            <a  href="#/AdminOC/" style="text-decoration: none;">
                <div  class="info-box demo">
                    <span  class="info-box-icon bg-green elevation-1"><i  class="fa fa-book"></i></span>
                    <div  class="info-box-content"><span  class="info-box-text">(5) OC KPI</span> <span  class="info-box-number " style="color: rgb(255, 255, 255);"></span></div>
                </div>
            </a>
        </div>
        <div  class="col-12 col-sm-6 col-md-3">
            <a  href="#/CategoryKPILevelAdmin/" style="text-decoration: none;">
                <div  class="info-box demo">
                    <span  class="info-box-icon bg-orange elevation-1"><i  class="far fa-gem"></i></span>
                    <div  class="info-box-content"><span  class="info-box-text">(6) OC Category KPI</span> <span  class="info-box-number " style="color: rgb(255, 255, 255);"></span></div>
                </div>
            </a>
        </div>
        <div  class="col-12 col-sm-6 col-md-3">
            <a  href="#/AdminUser/" style="text-decoration: none;">
                <div  class="info-box demo">
                    <span  class="info-box-icon bg-purple elevation-1"><i  class="far fa-user"></i></span>
                    <div  class="info-box-content"><span  class="info-box-text">(7) User</span> <span  class="info-box-number " style="color: rgb(255, 255, 255);"></span></div>
                </div>
            </a>
        </div>
        <div  class="col-12 col-sm-6 col-md-3">
            <a  href="#/AddUserToLevel/" style="text-decoration: none;">
                <div  class="info-box demo">
                    <span  class="info-box-icon bg-maroon elevation-1"><i  class="fa fa-leaf"></i></span>
                    <div  class="info-box-content"><span  class="info-box-text">(8) OC User</span> <span  class="info-box-number " style="color: rgb(255, 255, 255);"></span></div>
                </div>
            </a>
        </div>

        <div class="col-12 col-sm-6 col-md-3">
          <a href="#/Overview/" style="text-decoration: none;">
            <div class="info-box demo">
              <span class="info-box-icon bg-danger elevation-1"><i  class="fas fa-search-plus"></i></span>
              <div class="info-box-content"><span  class="info-box-text">KPI Overview</span> <span  class="info-box-number " style="color: rgb(255, 255, 255);"></span></div>
            </div>
          </a>
        </div>

        <div  class="col-12 col-sm-6 col-md-3">
          <a  href="#/KpiKind/" style="text-decoration: none;">
            <div  class="info-box demo">
              <span class="info-box-icon bg-danger elevation-1"><i  class="fas fa-search-plus"></i></span>
              <div class="info-box-content"><span  class="info-box-text">KPI Kind</span> <span class="info-box-number " style="color: rgb(255, 255, 255);"></span></div>
            </div>
          </a>
        </div>
      </div>
    </div>
  </section>

</template>

<script>
import axios from 'axios';
import Sidebar from './Sidebar.vue';
import { HTTP } from "../../http-constants";
import VueJwtDecode from "vue-jwt-decode";
import i18n from "../../lang/i18n";
import signal from "../../hub";
import EventBus from "../../utils/EventBus.js";
export default {
  name: "homeAdmin",
 data() {
    return {
     menuAdmin: [],
     menuUser: [],
     menus: [],
     menuSidebar:'',
     loggedIn: this.$auth.loggedIn(),
     arrayID: [],
     data: [],
     listdata: [],
     userid: 0,
     locale: $cookies.get("Lang"),
     role: '',
     activetab: null,
     tabsAdmin: false,
     tabsUser: true
    }
  },
   components: {
    AppSidebar: Sidebar,
  },
  mounted(){
    let seft = this;
    seft.userid = VueJwtDecode.decode(localStorage.getItem("authToken")).nameid;
    seft.role = Number(localStorage.getItem('Role'))
    seft.getall2();
    seft.getMenuAdmin()
    seft.getMenuUser()
  },
  watch:{
    locale : function(newOld,oldVal){
      this.locale = newOld
      // this.getall2()
      this.getMenuAdmin()
      this.getMenuUser()
    },
  },
  created(){
    let seft = this;
    EventBus.$on('flag', locale =>{
      seft.locale = locale
    });
    seft.locale = $cookies.get("Lang")
    // seft.getall2();
    seft.menuSidebar = JSON.parse(localStorage.getItem('Menus'))
    // EventBus.$on('tabsAdmin',activetab=>{
    //   seft.tabsAdmin = true
    //   seft.activetab = activetab
    //   seft.tabsUser = false
    // })
    // EventBus.$on('tabsUser',activetab=>{
    //   seft.tabsAdmin = false
    //   seft.tabsUser = true
    //   seft.activetab = activetab
    // })
    // EventBus.$on('tabs',activetabdefault=>{
    //   seft.tabsUser = false
    //   seft.tabsAdmin = true
    // })
  },
  destroyed() {
    // Stop listening the event hello with handler
    EventBus.$off('flag', this.locale);
    // EventBus.$off('tabsAdmin');
    // EventBus.$off('tabsUser');
  },
  computed: {
    user: function() {
      return this.$store.state.currentUser;
    }
  },

  methods: {

    getall2(){
    let seft = this
    axios.get(`/Menus/Getall2/${seft.locale}`).then(r=>{
        seft.menus = r.data
      })
    },
    getMenuAdmin(){
      axios.get(`/Menus/GetMenuAdmin/${this.locale}`).then(res=>{
        this.menuAdmin = res.data
      })
    },
    getMenuUser(){
      axios.get(`/Menus/GetMenuUser/${this.locale}`).then(res=>{
        this.menuUser = res.data
      })
    },
    getAllNotifications() {
      let seft = this;
      axios.get("/Home/GetNotifications").then(r=>{
         seft.arrayID = r.data.arrayID;
        seft.data = r.data.data;
        seft.listdata = r.data;
        seft.userid = VueJwtDecode.decode(localStorage.getItem("authToken")).nameid;

      })
    },
  }
}
</script>

<style lang="scss" scoped></style>
