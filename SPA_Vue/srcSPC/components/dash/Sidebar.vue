<template>
  <div id="sidebar">
    <!-- Main Sidebar Container -->
    <aside class="main-sidebar sidebar-dark-primary elevation-4">
      <!-- Brand Logo -->
      <div class="sidebar">
        <nav class="mt-2">
          <ul class="nav nav-sidebar flex-column" data-widget="treeview" role="menu" data-accordion="false">
            <li class="nav-item">
              <a class="nav-link" data-widget="pushmenu" href="#" role="button"><i class="fas fa-bars"></i></a>
            </li>
          </ul>
        </nav>
      </div>
      <!-- <a   v-on:click="activetabUser(activetab = 2)" style="cursor: pointer;color: white"  class="brand-link">
        <img
          src="../../assets/logo-login.png"
          alt="KPI System Logo"
          class="brand-image img-circle elevation-3"
          style="opacity: .8"
        />
        <span class="brand-text font-weight-light">KPI System</span>
      </a> -->

      <div v-if="role == 1" class="sidebar">
        <nav class="mt-2">
          <ul class="nav nav-pills nav-sidebar flex-column" data-widget="treeview" role="menu" data-accordion="false"  >
            <li class="nav-item has-treeview">
              <a class="nav-link" href="#/home">
                <i class="fas fa-users-cog"></i>
                <p>
                  Moderator
                  <i class="right fas fa-angle-left"></i>
                </p>
              </a>
              <ul v-for="(item,key,index) in sidebarAdmin" :key="index"  class="nav nav-treeview">
                <li  class="nav-item">
                  <a :href="'#' + item.Link"  class="nav-link">
                    <i :class="item.FontAwesome"></i>
                    <p>{{$t(item.Code)}}</p>
                  </a>
                </li>
              </ul>
            </li>
          </ul>
        </nav>
      </div>

       <div v-else class="sidebar">
         <nav v-if="levelOC == 3 || levelOC == 2" class="mt-2">
          <ul class="nav nav-pills nav-sidebar flex-column" data-widget="treeview" role="menu" data-accordion="false"  >
            <li class="nav-item has-treeview">
              <a style="cursor: pointer" @click="activetabAdmin(activetab = 1)"  v-bind:class="[ activetab === 1 ? 'nav-link active' : 'nav-link' ]"  >
                <i class="fas fa-users-cog"></i>
                <p>
                  Moderator
                  <!-- <i class="right fas fa-angle-left"></i> -->
                </p>
              </a>

            </li>
          </ul>
        </nav>
        <nav class="mt-2">
          <ul class="nav nav-pills nav-sidebar flex-column" data-widget="treeview" role="menu" data-accordion="false"  >
            <li class="nav-item has-treeview">
              <a style="cursor: pointer" v-on:click="activetabUser(activetab = 2)" v-bind:class="[ activetab === 2 ? 'nav-link active' : 'nav-link' ]"  >
                <i class="fas fa-user"></i>
                <p>
                  User
                  <!-- <i class="right fas fa-angle-left"></i> -->
                </p>
              </a>
              <!-- <ul v-for="(item,key,index) in sidebarUser" :key="index"  class="nav nav-treeview">
                <li  class="nav-item">
                  <a :href="'#' + item.Link"  class="nav-link">
                    <i :class="item.FontAwesome"></i>
                    <p>{{item.Code}}</p>
                  </a>
                </li>
              </ul> -->
            </li>
          </ul>
        </nav>
      </div>

    </aside>
  </div>
</template>

<script>
import EventBus from "../../utils/EventBus.js";
export default {
  name: "sidebar",
  data() {
    return {
    activetab: 2,
     menuSidebar: [],
     sidebarAdmin: [],
     sidebarUser: [],
     lang: '',
     locale: $cookies.get("Lang"),
     role: '',
     levelOC: ''
    }
  },
  mounted(){

  },
  destroyed() {

  },
  beforeMount(){
    let self = this
    self.loadmenuSidebar();
  },
  created(){
    let seft = this
    EventBus.$on('flag', locale =>{
      seft.locale = locale
    })
    seft.loadmenuSidebar();
    EventBus.$on('event-name', function (data) {
    })
    seft.getMenuUser()
    seft.getMenuAdmin()
    seft.role = localStorage.getItem('Role')
    seft.levelOC = localStorage.getItem('LevelOC')
  },
  watch:{
    locale : function(newOld,oldVal){
      this.locale = newOld
      this.loadmenuSidebar()
    }
  },
  methods: {
    activetabs(activetabdefault){
      this.$router.push("/home");
      this.activetab = 2
      EventBus.$emit('tabs', activetabdefault);
    },
    activetabAdmin(activetab){
      this.$router.push("/home/admin");
      EventBus.$emit('tabsAdmin', activetab);
    },
    activetabUser(activetab){
      this.$router.push("/home");
      EventBus.$emit('tabsUser', activetab);
    },
    loadmenuSidebar(){
      let seft = this;
      axios.get(`Menus/Getall2/${seft.locale}`).then(r=>{
        seft.menuSidebar = r.data
      })
    },
    getMenuAdmin(){
      axios.get(`/Menus/GetMenuAdmin/${this.locale}`).then(res=>{
        this.sidebarAdmin = res.data
      })
    },
    getMenuUser(){
      axios.get(`/Menus/GetMenuUser/${this.locale}`).then(res=>{
        this.sidebarUser = res.data
      })
    },
  }
};
</script>

