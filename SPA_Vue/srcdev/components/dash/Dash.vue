<template>

    <div class="wrapper">
      <app-header />
      <app-sidebar :key="componentKey" />
      <div class="content-wrapper">
        <app-breadcrumb />
        <section class="content">
          <transition name="fade">
            <router-view  ></router-view>
          </transition>
        </section>
      </div>
      <app-footer></app-footer>
      <aside class="control-sidebar control-sidebar-dark">
      </aside>
    </div>
</template>

<script>
import Sidebar from './Sidebar.vue';
import Footer from "./Footer.vue";
import Header from "./Header";
import Breadcrumb from "./Breadcrumb";
import VueJwtDecode from 'vue-jwt-decode'

export default {
  name: "dashboard",
  data() {
    return {
      componentKey: 0,
      role: null
    }
  },
  created(){
    let self = this
    self.forceRerender();
    self.role = VueJwtDecode.decode(localStorage.getItem("authToken")).Role
  },
  methods: {
    forceRerender() {
      this.componentKey += 1;
    }
  },
  components: {
    AppSidebar: Sidebar,
    AppFooter: Footer,
    AppBreadcrumb: Breadcrumb,
    AppHeader: Header
  }
};
</script>

<style lang="scss" scoped></style>
