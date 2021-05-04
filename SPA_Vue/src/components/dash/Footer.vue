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
  created() {
    this.GetUserOnline();
    this.connection = new signalR.HubConnectionBuilder()
    .withUrl("http://10.4.5.174:91/henry-hub")
    // .withUrl("http://10.10.0.34:91/henry-hub")
    .configureLogging(signalR.LogLevel.Information)
    .build();
    this.connection.start().then(function () {
    });
    this.connection.on("updateCount", (data) => {
      // this.GetUserOnline();
    });
  },
  destroyed() {
    this.connection.off("updateCount");
  },
  methods: {
    GetUserOnline() {
      axios.get('AdminUser/GetUserOnline').then((result) => {
        this.online = result.data.length
      }).catch((err) => {

      });
    }
  }
};
</script>

<style lang="scss" scoped></style>
