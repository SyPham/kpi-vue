<template>
    <vue-page-transition name="fade-in-down">
      <router-view ></router-view>
    </vue-page-transition>
</template>

<script>
export default {
  created(){
    // setInterval(() => this.getStatus(),10000);
    // this.getStatus()
  },
  mounted(){


  },
  beforeDestroy() {
    // Make sure to close the connection with the events server
    // when the component is destroyed, or we'll have ghost connections!
    // msgServer.close();
  },
  methods: {
    getStatus: function () {
      let url = 'http://10.4.5.132:991/User/GetAll/getall';
      fetch(url, { mode: 'no-cors'})
        .then((resp) => {
          console.log(resp)
          // if (!resp.ok || resp.status != 200) site.status = false;
          // if (resp.type == "opaque") site.status = true;
          // self.$set(self.sites, index, site);
        })
    }
  }
}
</script>

<style lang="scss">
.fade-enter-active, .fade-leave-active {
  transition-property: opacity;
  transition-duration: .25s;
}

.fade-enter-active {
  transition-delay: .25s;
}

.fade-enter, .fade-leave-active {
  opacity: 0
}
@import '../node_modules/@syncfusion/ej2-base/styles/bootstrap.css';
@import '../node_modules/@syncfusion/ej2-buttons/styles/bootstrap.css';
@import '../node_modules/@syncfusion/ej2-calendars/styles/bootstrap.css';
@import '../node_modules/@syncfusion/ej2-dropdowns/styles/bootstrap.css';
@import '../node_modules/@syncfusion/ej2-inputs/styles/bootstrap.css';
@import '../node_modules/@syncfusion/ej2-navigations/styles/bootstrap.css';
@import '../node_modules/@syncfusion/ej2-popups/styles/bootstrap.css';
@import '../node_modules/@syncfusion/ej2-splitbuttons/styles/bootstrap.css';
@import "../node_modules/@syncfusion/ej2-grids/styles/bootstrap.css";
@import "../node_modules/@syncfusion/ej2-treegrid/styles/bootstrap4.css";
@import '../node_modules/@syncfusion/ej2-buttons/styles/bootstrap.css';
@import "../node_modules/@syncfusion/ej2-vue-popups/styles/bootstrap.css";
@import "../node_modules/@syncfusion/ej2-vue-navigations/styles/bootstrap.css";
@import "../node_modules/@syncfusion/ej2-vue-notifications/styles/bootstrap.css";
@import "../node_modules/@syncfusion/ej2-vue-dropdowns/styles/material.css";
@import 'devextreme/dist/css/dx.common.css';
@import 'devextreme/dist/css/dx.light.css';
</style>
