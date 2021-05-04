<template>
  <section class="content">
      <div class="container-fluid">
        <div class="row">

          <div class="col-md-4">
            <div class="card">
              <div class="card-header">
                <hierarchy>

                </hierarchy>

              </div>
              <div class="card-body">
                <table
                  id="treetable"
                  class="table table-condensed table-hover table-striped fancytree-fade-expander fancytree-colorize-selected"
                >
                  <thead>
                    <tr>
                      <th style="width:10px">Level Number</th>
                      <th class="text-center">Name</th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr>
                      <td></td>
                      <td></td>
                    </tr>
                  </tbody>
                </table>
              </div>
            </div>
          </div>

          <div class="col-md-8">
            <!-- categorylist -->

            <app-category :level="level" :ocID="ocID"></app-category>
            <!-- endcategorylist -->

            <!-- kpilevellist -->
            <app-kpioc></app-kpioc>

            <!-- endkpilevellist -->

            <!-- modal -->
            <!-- <listoc></listoc> -->
            <!-- endmodal -->
          </div>
        </div>
      </div>
  </section>
</template>

<script>
import { HTTP } from "../../http-constants";
// import listoc from "../../components/adminOC/List"
import Hierarchy from "../../components/adminOC/Hierarchy";
import listoc from "../../components/AdminOCCategoryKpi/Modal";
import { initTree } from "../../utils/Plugin-Fancytree-Vuejs/index.js";
import Category from "./Category";
import KPIOC from "./KPIOC";
import EventBus from "../../utils/EventBus.js";
export default {
  name: "index",
  props: [],
  data() {
    return {
      pageIndex: 1,
      pageSize: 10,
      level: 0,
      ocID: 0,
      ListID: []
    };
  },
  components: {
    listoc,
    Hierarchy,
    appCategory: Category,
    "app-kpioc": KPIOC
  },
  created() {
    let self = this;
    self.initialTree();
    self.GetListID()
    var listocs = localStorage.getItem("ListOCs").split(",") || [];

  },
  mounted() {
    let self = this;
    setTimeout(function(){
      self.initialTree();
    },300)
  },

  methods: {
    GetListID(){
      axios.get('CategoryKPILevel/GetListID').then(r=>{
        this.ListID = r.data
      })
    },
    initialTree() {
      let self = this;
      initTree("http://10.10.0.34:91/AdminKPILevel/GetListTree", function(
        event,
        data
        ) {
        var node = data.node,
        $tdList = $(node.tr).find(">td");
        var level = Number($tdList.eq(0).text());
        var name = $tdList.eq(1).text();
        var listocs = localStorage.getItem("ListOCs").split(",") || [];
        let arr = listocs

        let index, value, result;
        for (index = 0; index < arr.length; index++) {
          value = arr[index];
          if (value == Number(data.node.key)) {
            result = value;
             break;
          }
        }
        if (result) {
        }
        else {
        }

        $("#boxCategory .kpi-name h3").text("Category list - " + name);
        $("#boxCategory .levelID").val(data.node.key);
        $("#box .catid").val("");
        $("#box .kpi-name h3").text("KPI");
        $("#box").fadeOut();
        $("#box .periodAll").hide();
        // self.GetListID()
        self.level = level;
        self.ocID = Number(data.node.key);
       // console.log(self.level);
       // console.log(self.ocID);
      });
      $(".fancy-collapse").off("click").on("click", function() {
        $("#treetable").fancytree("getTree").expandAll(false);
      });
      $(".fancy-expand").off("click").on("click", function() {
        $("#treetable")
          .fancytree("getTree")
          .expandAll();
      });
    },
    pagingCategoryKPILevel(totalRow, callback, changePageSize) {
      let self = this;
      var totalPage = Math.ceil(totalRow / self.pageSize);

      //Unbind pagination if it existed or click change pagesize
      if (
        $("#paginationCategoryKPILevel a").length === 0 ||
        changePageSize === true
      ) {
        $("#paginationCategoryKPILevel").empty();
        $("#paginationCategoryKPILevel").removeData("twbs-pagination");
        $("#paginationCategoryKPILevel").unbind("page");
      }

      $("#paginationCategoryKPILevel").twbsPagination({
        totalPages: totalPage === 0 ? 1 : totalPage,
        first: "First",
        next: "Next",
        last: "Last",
        prev: "Previous",
        visiblePages: 10,
        onPageClick: function(event, page) {
          self.pageIndex = page;
          setTimeout(callback, 500);
        }
      });
    },
    pagingKPILevel(totalRow, callback, changePageSize) {
      let self = this;
      var totalPage = Math.ceil(totalRow / self.pageSize);

      //Unbind pagination if it existed or click change pagesize
      if (
        $("#paginationKPILevelTop a,#paginationKPILevelBottom a").length ===
          0 ||
        changePageSize === true
      ) {
        $("#paginationKPILevelTop,#paginationKPILevelBottom").empty();
        $("#paginationKPILevelTop,#paginationKPILevelBottom").removeData(
          "twbs-pagination"
        );
        $("#paginationKPILevelTop,#paginationKPILevelBottom").unbind("page");
      }

      $("#paginationKPILevelTop, #paginationKPILevelBottom").twbsPagination({
        totalPages: totalPage === 0 ? 1 : totalPage,
        first: "First",
        next: "Next",
        last: "Last",
        prev: "Previous",
        visiblePages: 10,
        onPageClick: function(event, page) {
          self.pageIndex = page;
          setTimeout(callback, 500);
        }
      });
    },
    getAllDataByCategory(catid, period) {

      let date = new Date();
      let year = date.getFullYear();
      let week = getDateOfWeekInYear(date);
      let month = date.getMonth();
      let quarter = getCurrentQuarter(date);
      let startYear = year - 5;

      if (period.toLowerCase() === "w")
        self.$router.push(`/dataset/${catid}/w/1/53/${year}`);
      if (period.toLowerCase() === "m")
        self.$router.push(`/dataset/${catid}/m/1/11/${year}`);
      if (period.toLowerCase() === "q")
        self.$router.push(`/dataset/${catid}/q/1/4/${year}`);
      if (period.toLowerCase() === "y")
        self.$router.push(`/dataset/${catid}/y/${startYear}/${year}/${year}`);
    },
    getAllKPILevelByCategory(changePageSize, category) {
      let self = this;
      axios.get(
        `CategoryKPILevel/getAllKPILevelByCategory/${category}/${self.pageIndex}/${self.pageSize}`
      ).then(response => {
        if (response.status) {
          var count = 1;
          var data = response.data;
          self.dataKPILV = response.data.data;
          self.pagingKPILevel(
            response.data.total,
            function() {
              self.getAllKPILevelByCategory("", category);
            },
            changePageSize
          );
          self.registerEvent();
        }
      });
    },
    registerEvent() {
      let self = this;
      $("#boxCategory .btnCategory")
        .off("click")
        .on("click", function() {
          var catid = Number($(this).data("id"));
          var name = $(this)
            .find("span.name")
            .text();
          $("#box h3").text(name + " - KPI");
          $("#box .catid").val(catid);
          $("#box").fadeIn();
          $("#box .periodAll").show();
          self.getAllKPILevelByCategory(true, catid);
        });
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
    },
  }
};
</script>
