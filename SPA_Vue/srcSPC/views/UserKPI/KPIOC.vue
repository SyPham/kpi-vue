<template>
  <div class="card " id="box" style="display:none">
    <div class="card-header with-border kpi-name">
      <h3 class="card-title pull-left">KPI</h3>
      <span class="id" style="display:none"></span>
      <span class="code" style="display:none"></span>
      <span class="level" style="display:none"></span>
      <input
        type="text"
        class="form-control catid"
        value=""
        style="display:none"
      />
    </div>
    <div class="card-body">
      <div class="text-center periodAll" style="display:none">
        <a class="btn bg-primary btn-sm weeklyperiod" data-period="W"
          ><i class="fa fa-eye"></i>&#160;{{$t("Viewallbyweekly")}}</a
        >
        <a class="btn bg-primary btn-sm monthlyperiod" data-period="m"
          ><i class="fa fa-eye"></i>&#160;{{$t("Viewallbymonthly")}}</a
        >
        <a class="btn bg-primary btn-sm quarterlyperiod" data-period="q"
          ><i class="fa fa-eye"></i>&#160;{{$t("Viewallbyquarterly")}}</a
        >
        <a class="btn bg-primary btn-sm halfyearperiod" data-period="h"
          ><i class="fa fa-eye"></i>&#160;Half Year</a
        >
        <a class="btn bg-primary btn-sm yearlyperiod" data-period="y"
          ><i class="fa fa-eye"></i>&#160;{{$t("Viewallbyyearly")}}</a
        >
      </div>
    </div>
    <div class="card-body">
      <table class="table table-striped">
        <thead>
          <tr>
            <th>#</th>
            <th>KPI Organization Chart</th>
            <th>Period</th>
          </tr>
        </thead>
        <tbody
          v-for="(item, key, index) in data"
          :key="index"
          class="tbody"
          id="tblkpilevel"
        >
          <tr :data-id="item.ID">
            <td>{{ key + 1 }}</td>
            <td>
              <div class="tooltip-css ">
                <div class="pretty p-icon p-rotate ">

                  <div class="state p-success textOverflow">
                    <div class="black labelOverflow">
                      {{ item.KPIName }}
                    </div>
                  </div>
                </div>
                <div class="top">{{ item.KPIName }}<i></i></div>
              </div>
            </td>
            <td>
              <button class="btn btn-sm week" @click='switchChartPriod("W",item.KPILevelCode)' :class=" auditPeriodClass( item.Checked, item.WeeklyChecked, item.StatusUploadDataW, item.StatusEmptyDataW)"
                :disabled="auditPeriodAttr(item.Checked,item.WeeklyChecked,item.StatusUploadDataW,item.StatusEmptyDataW)">
                <i class="fa fa-bar-chart" aria-hidden="true"></i> {{$t("Weekly")}}
              </button>

              <button class="btn btn-sm month" @click='switchChartPriod("M",item.KPILevelCode)' :class="auditPeriodClass(item.Checked,item.MonthlyChecked,item.StatusUploadDataM,item.StatusEmptyDataM)"
                :disabled="auditPeriodAttr(item.Checked,item.MonthlyChecked,item.StatusUploadDataM,item.StatusEmptyDataM)">
                <i class="fa fa-bar-chart" aria-hidden="true"></i> {{$t("Monthly")}}
              </button>



              <button
                class="btn btn-sm quarter"
                @click='switchChartPriod("Q",item.KPILevelCode)'
                :class="auditPeriodClass(item.Checked,item.QuarterlyChecked,item.StatusUploadDataQ,item.StatusEmptyDataQ)"
                :disabled="auditPeriodAttr(item.Checked,item.WeeklyChecked,item.StatusUploadDataQ,item.StatusEmptyDataQ)">
                <i class="fa fa-bar-chart" aria-hidden="true"></i> {{$t("Quarterly")}}
              </button>
              <button class="btn btn-sm half" @click='switchChartPriod("H",item.KPILevelCode)' :class="auditPeriodClass(item.Checked,item.HalfYearChecked,item.StatusUploadDataH,item.StatusEmptyDataH)"
                :disabled="auditPeriodAttr(item.Checked,item.HalfYearChecked,item.StatusUploadDataH,item.StatusEmptyDataH)">
                <i class="fa fa-bar-chart" aria-hidden="true"></i> Half Year
              </button>
              <button
                class="btn btn-sm year"
                @click='switchChartPriod("Y",item.KPILevelCode)'
                :class="auditPeriodClass(item.Checked,item.YearlyChecked,item.StatusUploadDataY,item.StatusEmptyDataY)"
                :disabled="auditPeriodAttr(item.Checked,item.WeeklyChecked,item.StatusUploadDataY,item.StatusEmptyDataY)">
                <i class="fa fa-bar-chart" aria-hidden="true"></i> {{$t("Yearly")}}
              </button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
    <div class="card-footer clearfix">
      <ul
        id="paginationKPILevelBottom"
        class="pagination pagination-sm no-margin pull-right"
      >
        <li><a href="#">«</a></li>
        <li><a href="#">1</a></li>
        <li><a href="#">2</a></li>
        <li><a href="#">3</a></li>
        <li><a href="#">»</a></li>
      </ul>
    </div>
  </div>
</template>

<script>
import listoc from "../../components/AdminOCCategoryKpi/Modal";
import EventBus from "../../utils/EventBus.js";
import { HTTP } from "../../http-constants";

export default {
  name: "app-KPIOC",
  data() {
    return {
      data: [],
      categoryId: 0,
      pageIndex: 1,
      pageSize: 10,
      locale: $cookies.get("Lang")
    };
  },
  watch: {
    categoryId: function(newVal, oldVal) {
      let self = this;
      self.categoryId = newVal;
      self.getAllKPILevelByCategory();
    },
    locale: function(newOld,oldVal){
      this.locale = newOld
      this.getAllKPILevelByCategory()
    }
  },
  methods: {
    auditPeriodClass(
      checked = false,
      periodChecked = false,
      statusUploadData = false,
      statusEmptyData = false
      ) {
      let className;
      //Neu co trong bang KPIOC thi mau xanh, nguoc lai mau xam
      if (checked == true) {
        //Neu co check vao period thi kiem tra co upload data chua, nguoc lai mau xam
        if (periodChecked == true) {
          //Neu data chua upload dung han thi mau xanh
          if (statusUploadData == true && statusEmptyData == false) {
            className = "btn btn-sm month btn-success";
          } else
            //Neu den han ma chua upload data thi mau vang
            className = "btn btn-sm month btn-warning";

        } else
          className = "btn btn-sm month bg-navy";

      } else
        className = "btn btn-sm month bg-navy";

      return className;
    },
    auditPeriodAttr(checked= false, periodChecked= false, statusUploadData= false, statusEmptyData= false) {
      let attr;
      //Neu co trong bang KPIOC thi mau xanh, nguoc lai mau xam
      if (checked == true) {
        //Neu co check vao period thi kiem tra co upload data chua, nguoc lai mau xam
        if (periodChecked == true) {
          //Neu data chua upload dung han thi mau xanh
          if (statusUploadData == true && statusEmptyData == false) {
            attr = false;
          } else
            //Neu den han ma chua upload data thi mau vang
            attr = false;

        } else
          attr = true;

      } else
        attr = true;

      return attr;
    },
    getAllKPILevelByCategory(changePageSize) {
      let self = this;
      EventBus.$on("CatWasEdit", categoryId => {
        self.categoryId = categoryId;
      });

      axios.get(
        `CategoryKPILevel/getAllKPILevelByCategory2/${self.categoryId}/${self.locale}/${self.pageIndex}/${self.pageSize}`
      ).then(response => {
        if (response.data.status) {
          var count = 1;
          var data = response.data;
          self.data = response.data.data;
          self.pagingKPILevel(
            response.data.total,
            function() {
              self.getAllKPILevelByCategory("");
            },
            changePageSize
          );
          self.registerEvent();
        }
      });
    },
    pagingKPILevel(totalRow, callback, changePageSize) {
      let self = this;
      var totalPage = Math.ceil(totalRow / self.pageSize);

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
    switchChartPriod(period, kpilevelcode) {
      var year = new Date().getFullYear();
      let self = this;
      switch (period) {
        case "W":
          window.open(`#/ChartPeriod/${kpilevelcode}/${this.categoryId}/${period}/${year}/1/53`,'_blank')
          // self.$router.push(
          //   `/ChartPeriod/${kpilevelcode}/${this.categoryId}/${period}/${year}/1/53`
          // );
          break;
        case "M":
          window.open(`#/ChartPeriod/${kpilevelcode}/${this.categoryId}/${period}/${year}/1/12`,'_blank')
          // self.$router.push(
          //   `/ChartPeriod/${kpilevelcode}/${this.categoryId}/${period}/${year}/1/12`
          // );
          break;
        case "H":
          window.open(`#/ChartPeriod/${kpilevelcode}/${this.categoryId}/${period}/${year}/1/2`,'_blank')
          // self.$router.push(
          //   `/ChartPeriod/${kpilevelcode}/${this.categoryId}/${period}/${year}/1/2`
          // );
          break;
        case "Q":
          window.open(`#/ChartPeriod/${kpilevelcode}/${this.categoryId}/${period}/${year}/1/4`,'_blank')
          // self.$router.push(
          //   `/ChartPeriod/${kpilevelcode}/${this.categoryId}/${period}/${year}/1/4`
          // );
          break;
        case "Y":
          window.open(`#/ChartPeriod/${kpilevelcode}/${this.categoryId}/${period}/${year}/${year}/${year}`,'_blank')
          // self.$router.push(
          //   `/ChartPeriod/${kpilevelcode}/${this.categoryId}/${period}/${year}/${year}/${year}`
          // );
          break;
      }
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

      var date = new Date();
      $("#tblkpilevel .week")
        .off("click")
        .on("click", function(e) {

          let year = date.getFullYear();
          var kpilevelcode = $(this).data("kpilevelcode");
          var period = $(this).data("period");
          var end = getDateOfWeekInYear(date);
          var catid = Number($("#box .catid").val());
          self.$router.push(
            `/ChartPeriod/${kpilevelcode}/${catid}/${period}/${year}/1/${end}`
          );

        });
      $("#tblkpilevel .month")
        .off("click")
        .on("click", function(e) {
          let self = this;
          var kpilevelcode = $(this).data("kpilevelcode");
          var period = $(this).data("period");
          let year = date.getFullYear();
          var start = date.getMonth();
          var end = date.getMonth();
          var catid = Number($("#box .catid").val());
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


      $("#box .periodAll .halfyearperiod")
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
    getAllDataByCategory(catid, period) {
      let self = this
      let date = new Date();
      let year = date.getFullYear();
      let week = getDateOfWeekInYear(date);
      let month = date.getMonth();
      let quarter = getCurrentQuarter(date);
      let startYear = year - 5;

      if (period.toLowerCase() === "w")
        self.$router.push(`/dataset/${self.categoryId}/w/1/53/${year}`);
      if (period.toLowerCase() === "m")
        self.$router.push(`/dataset/${self.categoryId}/m/1/12/${year}`);
      if (period.toLowerCase() === "h")
        self.$router.push(`/dataset/${self.categoryId}/h/1/2/${year}`);
      if (period.toLowerCase() === "q")
        self.$router.push(`/dataset/${self.categoryId}/q/1/4/${year}`);
      if (period.toLowerCase() === "y")
        self.$router.push(`/dataset/${self.categoryId}/y/${startYear}/${year}/${year}`);
    },
  },
  created() {
    let self = this
    self.getAllKPILevelByCategory();
    EventBus.$on('flag', locale =>{
      self.locale = locale
    });
  }

};
</script>

<style scoped></style>
