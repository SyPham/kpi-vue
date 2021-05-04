<template>
  <div id="app-category">
    <div class="card" id="boxCategory">
      <div class="card-header with-border kpi-name">
        <h3 style="font-size:18px" class="box-title pull-left">
          Categories list
        </h3>
        <span class="id" style="display:none"></span>
        <span class="code" style="display:none"></span>
        <span class="level" style="display:none"></span>
        <input type="text" class="form-control levelID" style="display:none" />
        <!-- <select class="form-control pull-right" style="width:50%"></select> -->
      </div>
      <div class="card-body pb-3" id="btnCategory" style="max-height:210px;overflow-y:scroll" >
        <button v-for="(item, key, index) in data" :key="index" class="btn mb-2 btn-sm btnCategory category2 ml-2 rounded-pill text-white font-weight-bold" :data-id="item.ID" @click="changeCategoryId(item.ID)">
          <span v-if="item.Total > 1" class="name"
            >{{ item.Name }}  ( {{ item.Total }} items )</span
          >
          <span v-else class="name"
            >{{ item.Name }}  ( {{ item.Total }} item )</span
          >
        </button>
      </div>
      <div class="card-body pb-3 text-danger" id="btnCategory2" style="max-height:210px;overflow-y:scroll;text-align:center;" >
        <span >You need permission to enter this area. Contact Admin room for further instructions</span>
      </div>
      <div class="card-body pb-3 text-success" id="showerror" style="max-height:210px;overflow-y:scroll;text-align:center;" >
        <span  style="text-align:center;" class="showerror">No data yet</span>
      </div>

    </div>
  </div>
</template>

<script>
import { HTTP } from "../../http-constants";
import EventBus from "../../utils/EventBus.js";
export default {
  name: "app-category",
  props: {
    level: Number,
    ocID: Number
  },
  data() {
    return {
      data: [],
      pageIndex: 1,
      pageSize: 10,
      ListID: [],
      length: 0,
      locale: $cookies.get("Lang")
    };
  },
  mounted(){
    $('#btnCategory').hide();
    $('#btnCategory2').hide();
    $('#showerror').hide();
  },
  methods: {
    GetListID(){
      axios.get('CategoryKPILevel/GetListID').then(r=>{
        // console.log("GetListID")
        this.ListID = r.data
        // console.log(this.ListID)
      })
    },
    changeCategoryId(catId) {
      EventBus.$emit("CatWasEdit", catId);
      // let self = this
      EventBus.$emit("SendLevel", this.ocID );

    },
    getAllCategories() {
      let self = this;
      var listocs = localStorage.getItem("ListOCs").split(",") || [];
      let arr = listocs
      let index, value, result;
      for (index = 0; index < arr.length; index++) {
          value = arr[index];
          if (value == self.ocID) {
            result = value;
              break;
          }
      }
      if(self.ocID == result )
      {
        axios.get(`CategoryKPILevel/GetAllCategories2/${self.ocID}/${self.locale}/${self.level}/${self.pageIndex}/${self.pageSize}`).then(response => {
          if (response.data.status) {
            if(response.data.data.length == 0){
              $('#showerror').show();
              $('#btnCategory2').hide();
              $('#btnCategory').hide();
            }
            else{
              self.data = response.data.data;
             // console.log(response.data.data);
              $('#btnCategory').show();
              $('#btnCategory2').hide();
              $('#showerror').hide();
              self.registerEvent();
            }
          }
        });
      }
      else{
        $('#btnCategory').hide();
        $('#btnCategory2').show();
        $('#showerror').hide();
        warning("Your do not have access")
      }
    },
    registerEvent() {
      let self = this;
      $("#boxCategory .btnCategory").off("click").on("click", function(e) {
          var catid = Number($(this).data("id"));
          // console.log(catid)
          var name = $(this).find("span.name").text();
          $("#box h3").text(name + " - KPI");
          $("#box .catid").val(catid);
          $("#box").fadeIn();
          $("#box .periodAll").show();
          //self.getAllKPILevelByCategory(true, catid);
        });
      //To dam dong da click
      $("#boxCategory .btnCategory").addClass("category2");

      $("#tblCategory tr")
        .off("click")
        .on("click", function() {
          var catName = $(this)
            .find("td:eq(1)")
            .text();
          $("#boxKPILevel .kpi-name h3").text("KPI Level - " + catName);
          var catID = $(this).data("id");

          $("#boxKPILevel .catid").val(catID);
          self.getAllKPIlevels();
        });
      //show list kpilevel

      var date = new Date();
      $("#tblkpilevel .week")
        .off("click")
        .on("click", function(e) {
          //e.preventDefault();

          let year = date.getFullYear();
          var kpilevelcode = $(this).data("kpilevelcode");
          var period = $(this).data("period");
          var end = self.getDateOfWeekInYear(date);
          var catid = Number($("#box .catid").val());
          self.$router.push(
            `/ChartPeriod/${kpilevelcode}/${catid}/${period}/${year}/1/${end}`
          );
          //self.$router.push("/ChartPeriod/?kpilevelcode=" + kpilevelcode + "&catid=" + catid + "&period=" + period + "&year=" + year + "&start=1&end=" + currentWeek);
          // window.location.href = "/ChartPeriod/?kpilevelcode=" + kpilevelcode + "&catid=" + catid + "&period=" + period + "&year=" + year + "&start=1&end=" + currentWeek;
        });
      $("#tblkpilevel .month")
        .off("click")
        .on("click", function(e) {
          //e.preventDefault();
          let self = this;
          var kpilevelcode = $(this).data("kpilevelcode");
          var period = $(this).data("period");
          let year = date.getFullYear();
          var start = date.getMonth();
          var end = date.getMonth();
          var catid = Number($("#box .catid").val());
          // self.$router.push("/ChartPeriod/?kpilevelcode=" + kpilevelcode + "&catid=" + catid + "&period=" + period + "&year=" + year + "&start=1&end=" + end);
          //self.$router.push(`/ChartPeriod/${self.kpilevelcode}/${self.catid}/${self.period}/${self.year}/1/${self.end}`);
          self.$router.push(
            `/ChartPeriod/${kpilevelcode}/${catid}/${period}/${year}/1/${end}`
          );
        });
      $("#tblkpilevel .quarter")
        .off("click")
        .on("click", function(e) {
          //e.preventDefault();
          var kpilevelcode = $(this).data("kpilevelcode");
          var period = $(this).data("period");
          let year = date.getFullYear();
          let end = self.getCurrentQuarter(date);
          var catid = Number($("#box .catid").val());

          self.$router.push(
            `/ChartPeriod/${kpilevelcode}/${catid}/${period}/${year}/1/${end}`
          );
        });
      $("#tblkpilevel .year")
        .off("click")
        .on("click", function(e) {
          //e.preventDefault();
          var kpilevelcode = $(this).data("kpilevelcode");
          var period = $(this).data("period");
          let year = date.getFullYear();
          var catid = Number($("#box .catid").val());
          self.$router.push(
            `/ChartPeriod/${kpilevelcode}/${catid}/${period}/${year}/${year}/${year}`
          );
          //window.location.href = "/ChartPeriod/?kpilevelcode=" + kpilevelcode + "&catid=" + catid + "&period=" + period + "&year=" + year + "&start=" + year + "&end=" + year ;
        });

      $("#box .periodAll .weeklyperiod")
        .off("click")
        .on("click", function(e) {
          var period = $(this).data("period");
          var catid = Number($("#box .catid").val());
          self.getAllDataByCategory(catid, period);
        });

      $("#box .periodAll .monthlyperiod")
        .off("click")
        .on("click", function(e) {
          var period = $(this).data("period");
          var catid = Number($("#box .catid").val());
          self.getAllDataByCategory(catid, period);
        });

      $("#box .periodAll .quarterlyperiod")
        .off("click")
        .on("click", function(e) {
          var period = $(this).data("period");
          var catid = Number($("#box .catid").val());
          self.getAllDataByCategory(catid, period);
        });

      $("#box .periodAll .yearlyperiod")
        .off("click")
        .on("click", function(e) {
          var period = $(this).data("period");
          var catid = Number($("#box .catid").val());
          self.getAllDataByCategory(catid, period);
        });
    }
  },
  created() {
    let self = this;
    // self.getAllCategories();
    self.GetListID()
    EventBus.$on('flag', locale =>{
      self.locale = locale
    });
  },
  watch: {
    level: function(newVal, oldVal) {
      let self = this;
      self.level = newVal;
      self.getAllCategories();
    },
    ocID: function(newVal, oldVal) {
      let self = this;
      self.ocID = newVal;

      self.getAllCategories();
    },
    locale: function(newOld,oldVal){
      this.locale = newOld
      this.getAllCategories()
    }
  }
};
</script>

<style lang="scss" scoped></style>
