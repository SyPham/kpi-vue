<template>
  <section class="content">
    <div class="container-fluid">
      <div v-if="role == 1" class="row">
        <div
          v-for="(item, key, index) in menuAdmin"
          :key="index"
          class="col-12 col-sm-6 col-md-3"
        >
          <a :href="'#' + item.Link" style="text-decoration:none">
            <div class="info-box demo">
              <span
                :class="
                  'info-box-icon' +
                    ' ' +
                    item.BackgroudColor +
                    ' ' +
                    'elevation-1'
                "
                ><i :class="item.FontAwesome"></i
              ></span>

              <div class="info-box-content">
                <span class="info-box-text">{{ item.Name }}</span>
                <span class="info-box-number " style="color:#fff"> </span>
              </div>
            </div>
          </a>
        </div>
      </div>

      <div v-else>
        <div v-if="tabsUser" class="row">
          <div
            v-for="(item, key, index) in menuUser"
            :key="index"
            class="col-12 col-sm-6 col-md-3"
          >
            <a :href="'#' + item.Link" style="text-decoration:none">
              <div class="info-box demo">
                <span
                  :class="
                    'info-box-icon' +
                      ' ' +
                      item.BackgroudColor +
                      ' ' +
                      'elevation-1'
                  "
                  ><i :class="item.FontAwesome"></i
                ></span>

                <div class="info-box-content">
                  <span class="info-box-text">{{ item.Name }}</span>
                  <span class="info-box-number " style="color:#fff"> </span>
                </div>
              </div>
            </a>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<script>
import axios from "axios";
import Sidebar from "./Sidebar.vue";
import { HTTP } from "../../http-constants";
import VueJwtDecode from "vue-jwt-decode";
import i18n from "../../lang/i18n";
import signal from "../../hub";
import EventBus from "../../utils/EventBus.js";
export default {
  name: "home",
  data() {
    return {
      menuAdmin: [],
      menuUser: [],
      menus: [],
      menuSidebar: "",
      loggedIn: this.$auth.loggedIn(),
      arrayID: [],
      data: [],
      listdata: [],
      userid: 0,
      locale: $cookies.get("Lang"),
      role: "",
      activetab: null,
      tabsAdmin: false,
      tabsUser: true
    };
  },

  components: {
    AppSidebar: Sidebar
  },

  mounted() {
    let seft = this;
    seft.userid = VueJwtDecode.decode(localStorage.getItem("authToken")).nameid;
    seft.role = Number(localStorage.getItem("Role"));
    seft.getall2();
    seft.getMenuAdmin();
    seft.getMenuUser();
  },

  watch: {
    locale: function(newOld, oldVal) {
      this.locale = newOld;
      this.getMenuAdmin();
      this.getMenuUser();
    }
  },

  created() {
    let seft = this;
    EventBus.$on("flag", locale => {
      seft.locale = locale;
    });
    seft.locale = $cookies.get("Lang");
    seft.menuSidebar = JSON.parse(localStorage.getItem("Menus"));
    EventBus.$on("tabsAdmin", activetab => {
      seft.tabsAdmin = true;
      seft.activetab = activetab;
      seft.tabsUser = false;
    });
    EventBus.$on("tabsUser", activetab => {
      seft.tabsAdmin = false;
      seft.tabsUser = true;
      seft.activetab = activetab;
    });
  },

  destroyed() {
    EventBus.$off("flag", this.locale);
  },

  computed: {
    user: function() {
      return this.$store.state.currentUser;
    }
  },

  methods: {

    getall2() {
      let seft = this;
      axios.get(`/Menus/Getall2/${seft.locale}`).then(r => {
        seft.menus = r.data;
      });
    },

    getMenuAdmin() {
      axios.get(`/Menus/GetMenuAdmin/${this.locale}`).then(res => {
        this.menuAdmin = res.data;
      });
    },

    getMenuUser() {
      axios.get(`/Menus/GetMenuUser/${this.locale}`).then(res => {
        this.menuUser = res.data;
      });
    },

    getAllNotifications() {
      let seft = this;
      axios.get("/Home/GetNotifications").then(r => {
        seft.arrayID = r.data.arrayID;
        seft.data = r.data.data;
        seft.listdata = r.data;
        seft.userid = VueJwtDecode.decode(
          localStorage.getItem("authToken")
        ).nameid;
      });
    }

  }
};
</script>

<style lang="scss" scoped></style>
