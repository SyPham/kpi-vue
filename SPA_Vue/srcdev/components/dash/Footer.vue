<template>
  <div id="appFooter">
    <footer class="main-footer ">
      <strong>Copyright &copy; 2020-2021 <a href="http://10.4.0.76:93/" target="blank">By Lab Team</a>.</strong>
      All rights reserved.
      <div class="float-right d-none d-sm-inline-block">
        <i class="fas fa-user"></i> <b>Online:</b> {{online}}
      </div>
    </footer>
  </div>
</template>

<script>
import signal from "../../hub";
export default {
  name: "appFooter",

  data() {
    return {
      online: 0
    }
  },
  mounted() {
    // fetch('https://api.ipify.org?format=json')
    // .then(response => response.json())
    // .then(response => {
    //   console.log(response);
    //   this.clientIp = response.ip;
    // });
  },
  created() {
    if (window.performance) {
      // console.info("window.performance works fine on this browser");
    }
      // console.info(window.performance);
    if (performance.navigation.type == performance.navigation.TYPE_RELOAD) {
      // console.info( "This page is reloaded" );
    } else {
      // console.info( "This page is not reloaded");
    }
    this.GetUserOnline();
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl("http://10.4.5.132:991/henry-hub")
      .configureLogging(signalR.LogLevel.Information)
      .build();
    this.connection.start().then(function () {
      // console.log("connected");
    });
    this.connection.on("updateCount", (data) => {
      // this.GetUserOnline();
    });
  },
  destroyed() {
    // this.connection.off("updateCount");
  },
  methods: {
    GetUserOnline() {
      axios.get('AdminUser/GetUserOnline').then((result) => {
        this.online = result.data.length
      })
      .catch((err) => {
      });
    }
  }
};
</script>

<style lang="scss" scoped></style>
