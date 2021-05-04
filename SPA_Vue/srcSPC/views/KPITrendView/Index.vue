<template>
  <section id="chartperiod" class="content">
    <div class="container-fluid">
      <div class="row">
        <div class="col-md-12">
          <h6 style="color:transparent">asd</h6>
        </div>
        <div class="col-md-12">
          <div class="box box-widget">
            <div class="box-header with-border">
              <h5
                v-if="period == 'W'"
                class="box-title1"
                style="font-weight:bold"
              >
                KPI Chart - {{ name }} - {{ $t("Weekly") }}
              </h5>
              <h5
                v-if="period == 'M'"
                ref="message"
                class="box-title1"
                id="box-title1"
                style="font-weight:bold"
              >
                KPI Chart - {{ name }} - {{ $t("Monthly") }}
              </h5>
              <h5
                v-if="period == 'Q'"
                class="box-title1"
                style="font-weight:bold"
              >
                KPI Chart - {{ name }} - {{ $t("Quarterly") }}
              </h5>
              <h5
                v-if="period == 'H'"
                class="box-title1"
                style="font-weight:bold"
              >
                KPI Chart - {{ name }} - Half Year
              </h5>
              <h5
                v-if="period == 'Y'"
                class="box-title1"
                style="font-weight:bold"
              >
                KPI Chart - {{ name }} - {{ $t("Yearly") }}
              </h5>
            </div>
            <div class="box-body">
              <h6 style="color:transparent">asd</h6>
              <div class="col-md-12">
                <!-- @* Year *@ -->
                <div class="form-inline" id="searchYear">
                  <label class="my-1 mr-2" for="YearOfYear">Year</label>
                  <select
                    v-model="year"
                    @change="changeyears($event)"
                    class="custom-select form-control my-1 mr-sm-2 YearOfYear"
                    id="YearOfYear"
                  >
                    <option value="0" selected>Choose...</option>

                    <option
                      v-for="(n, key, index) in 100"
                      :key="index"
                      :value="2000 + n"
                      >{{ 2000 + n }}</option
                    >
                  </select>
                  <button
                    style="margin: 3px"
                    type="button"
                    class="btn btn-sm bg-navy margin"
                  >
                    {{ $t("Manager_label") }}:
                    <strong>{{ OwnerManagerment }}</strong>
                  </button>
                  <button
                    style="margin: 3px"
                    type="button"
                    class="btn btn-sm bg-navy margin"
                  >
                    {{ $t("Owner_label") }}:
                    <strong>{{ Owner }}</strong>
                  </button>
                  <button
                    style="margin: 3px"
                    type="button"
                    class="btn btn-sm bg-navy margin"
                  >
                    {{ $t("Updater_label") }}:
                    <strong>{{ PIC }}</strong>
                  </button>
                  <button
                    style="margin: 3px"
                    type="button"
                    class="btn btn-sm bg-navy margin"
                  >
                    {{ $t("Sponsor_label") }}:
                    <strong>{{ Sponsor }}</strong>
                  </button>
                  <button
                    style="margin: 3px"
                    type="button"
                    class="btn btn-sm bg-navy margin"
                  >
                    {{ $t("Participant_label") }}:
                    <strong>{{ Participant }}</strong>
                  </button>

                  <button
                    @click="onExporting($event)"
                    style="margin: 3px"
                    type="button"
                    class="btn btn-sm bg-success margin"
                  >
                    <i class="far fa-file-excel"></i> Export Excel
                  </button>

                  <button
                    @click="ResetChart"
                    style="margin: 3px"
                    type="button"
                    class="btn btn-sm bg-primary margin"
                  >
                    <i class="fas fa-sync-alt"></i> Reset Chart
                  </button>
                </div>
              </div>

              <div class="chart-container">
                <canvas id="planet-charttrend"></canvas>
              </div>
            </div>
          </div>
        </div>
        <div class="col-md-12">
          <h6>Action Plan List:</h6>

          <div>
            <DxDataGrid
              id="priceDataGrid"
              :row-alternation-enabled="true"
              :data-source="AllDataActionPlanByKPILevelID"
              :ref="priceGridRefKey"
              :show-borders="true"
              :show-row-lines="true"
              :show-column-lines="true"
              :allow-column-resizing="true"
              :column-auto-width="true"
              :column-resizing-mode="currentMode"
            >
              <DxHeaderFilter :visible="showHeaderFilter" />

              <DxSearchPanel
                :visible="true"
                :width="240"
                placeholder="Search..."
              />

              <DxColumn
                data-field="RootCause"
                :width="200"
                alignment="center"
                header-cell-template="headerTemplate"
              />

              <DxColumn
                data-field="Index"
                caption="No."
                header-cell-template="IndexTemplate"
              />

              <DxColumn
                data-field="CreatedTimed"
                caption="Create Month"
                header-cell-template="CreatedTimedTemplate"
              />

              <DxColumn
                data-field="Title"
                caption="Action plan"
                header-cell-template="TitleTemplate"
              />

              <DxColumn
                data-field="Description"
                caption="Description"
                width="200px"
                header-cell-template="DescriptionTemplate"
              />

              <DxColumn
                data-field="Tag"
                caption="PIC"
                header-cell-template="TagTemplate"
              />

              <DxColumn
                data-field="Deadline"
                data-type="date"
                caption="Due Date"
                header-cell-template="DeadlineTemplate"
              />

              <DxColumn
                data-field="UpdateSheduleDate"
                caption="Update Schedule Date"
                header-cell-template="UpdateSheduleDateTemplate"
              />

              <DxColumn
                data-field="ActualFinishDate"
                header-cell-template="ActualFinishDateTemplate"
              />

              <DxColumn
                data-field="Remark"
                header-cell-template="RemarkTemplate"
              />

              <DxColumn
                data-field="ApprovedStatus2"
                caption="Approved"
                header-cell-template="ApprovedStatusTemplate"
              />

              <template #headerTemplate="{data}">
                <span> <b style="color:black;"> Root Cause </b></span>
              </template>
              <template #IndexTemplate="{data}">
                <span> <b style="color:black;"> No. </b></span>
              </template>
              <template #CreatedTimedTemplate="{data}">
                <span> <b style="color:black;"> Create Month </b></span>
              </template>
              <template #TitleTemplate="{data}">
                <span> <b style="color:black;"> Action plan </b></span>
              </template>
              <template #DescriptionTemplate="{data}">
                <span> <b style="color:black;"> Description </b></span>
              </template>
              <template #TagTemplate="{data}">
                <span> <b style="color:black;"> PIC </b></span>
              </template>
              <template #DeadlineTemplate="{data}">
                <span> <b style="color:black;"> Due Date</b></span>
              </template>
              <template #UpdateSheduleDateTemplate="{data}">
                <span> <b style="color:black;"> Update Schedule Date</b></span>
              </template>
              <template #ActualFinishDateTemplate="{data}">
                <span> <b style="color:black;"> Actual Finish Date</b></span>
              </template>
              <template #RemarkTemplate="{data}">
                <span> <b style="color:black;"> Remark</b></span>
              </template>
              <template #ApprovedStatusTemplate="{data}">
                <span> <b style="color:black;"> Approved</b></span>
              </template>
            </DxDataGrid>
          </div>

          <div style="visibility: hidden;">
            <DxDataGrid
              id="ratingDataGrid"
              height="10px"
              :data-source="AllDataActionPlanByKPILevelID2"
              :ref="ratingGridRefKey"
            >
              <DxColumn
                data-field="Picture"
                cell-template="grid-cell-template"
              />

              <template #grid-cell-template>
                <div>
                  <img :src="urlimage" />
                </div>
              </template>
              <DxExport :enabled="true" />
            </DxDataGrid>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<script>
import LineChart from "../../utils/ChartJs/LineChart";
import { initLineChart } from "../../plugins/LineChartPlugin";
import planetChartData from "../../plugins/Chartjs2/Demo";
import VueJwtDecode from "vue-jwt-decode";
import EventBus from "../../utils/EventBus.js";
import Vue from "vue";
import Multiselect from "vue-multiselect";
import {
  DxDataGrid,
  DxColumn,
  DxExport,
  DxHeaderFilter,
  DxSearchPanel,
  DxFilterRow
} from "devextreme-vue/data-grid";
import { DxSelectBox, DxCheckBox } from "devextreme-vue";
import { exportDataGrid } from "devextreme/excel_exporter";
import ExcelJS from "exceljs";
import saveAs from "file-saver";
const priceGridRefKey = "priceDataGrid";
const ratingGridRefKey = "ratingDataGrid";
export default {

  data() {
    return {
      pointBackgroundColors: [],
      lastTarget: "",
      year: new Date().getFullYear(),
      currentMode: "widget",
      showFilterRow: true,
      showHeaderFilter: true,
      ratingGridRefKey,
      priceGridRefKey,
      urlimage: "",
      filterOptions: {
        type: "CheckBox"
      },
      filter: {
        type: "CheckBox"
      },
      excelName: "",
      AllDataActionPlanByKPILevelID: [],
      AllDataActionPlanByKPILevelID2: [
        {
          ID: 86,
          Title: "ddd"
        }
      ],
      window: {
        width: 0,
        height: 0
      },
      dateValue: new Date(),
      pageSettings: { pageSize: 10 },
      data: [],
      defaultLink: null,
      title: null,
      name: "",
      comID: 0,
      date: "",
      data2: [],
      OwnerManagerment: "",
      Owner: "",
      PIC: "",
      Sponsor: "",
      Participant: "",
      vstart: 0,
      vend: 0,
      chart: {},
      min: 1,
      stepSize: 10,
      planetChartData: planetChartData,
      datacollection: {},
      weekly: [],
      years: [],
      data: [],
      start: 0,
      kpiname: "",
      end: 0,
      datasets: {},
      period: "",
      unit: "",
      labels: [],
      targets: [],
      scores: [],
      standards: [],
      dataremarks: [],
      statusfavorite: true,
      dataCharts: {},
      searchyear: 0,
      currentUser: Number(
        VueJwtDecode.decode(localStorage.getItem("authToken")).nameid
      ),
      kpilevelcode: "",
      kpilevelID: 0,
      locale: $cookies.get("Lang")
    };
  },

  provide: {},

  components: {
    DxDataGrid,
    DxColumn,
    DxExport,
    DxHeaderFilter,
    DxSearchPanel,
    DxFilterRow,
    DxSelectBox,
    DxCheckBox,
    LineChart,
    Multiselect
  },
  
  created() {
    window.addEventListener("resize", this.handleResize);
    // this.handleResize();
    let seft = this;
    EventBus.$on("flag", locale => {
      seft.locale = locale;
    });
    let boxTitle = $("#chartperiod .box-title1").text();
    seft.period = seft.$route.params.period;
    seft.vstart = seft.$route.params.start;
    seft.vend = seft.$route.params.end;
    seft.searchyear = seft.$route.params.year;
    seft.kpilevelcode = seft.$route.params.kpilevelcode;
    // seft.Loadchart();
  },
  computed: {
    priceGridInstance: function() {
      return this.$refs[priceGridRefKey].instance;
    },
    ratingGridInstance: function() {
      return this.$refs[ratingGridRefKey].instance;
    }
  },
  mounted() {
    let self = this;
    self.Loadchart();
  },
  destroyed() {
    window.removeEventListener("resize", this.handleResize);
  },
  beforeDestroy: function() {},

  watch: {
    locale: function(newOld, oldVal) {
      this.locale = newOld;
      // this.Loadchart()
    }
  },

  methods: {
    ResetChart() {
      this.LoadAllDataActionPlanByKPILevelID(this.kpilevelID);
    },

    onExporting() {
      let self = this;
      self.$vs.loading();
      self.converImg();
      let boxTitle = $("#chartperiod .box-title1").text();
      const context = this;
      const workbook = new ExcelJS.Workbook();
      const worksheet = workbook.addWorksheet("Capacity Commitment");
      exportDataGrid({
        component: context.ratingGridInstance,
        worksheet: worksheet,
        // autoFilterEnabled: true,
        topLeftCell: { row: 10, column: 1 },
        customizeCell: options => {
          const { excelCell, gridCell } = options;
          if (gridCell.rowType === "data") {
            if (gridCell.column.dataField === "Picture") {
              excelCell.value = undefined;
              const image = workbook.addImage({
                base64: self.urlimage,
                extension: "png"
              });
              worksheet.getRow(excelCell.row).height = 250;
              worksheet.mergeCells(11, 1, 11, 8);
              worksheet.addImage(image, {
                tl: { col: excelCell.col - 1, row: excelCell.row - 1 },
                ext: { width: 1000, height: 340 }
              });
            }
          }
        }
      })
        .then(function(dataGridRange) {
          // header
          const headerRow = worksheet.getRow(1);
          headerRow.height = 30;
          worksheet.mergeCells(1, 1, 1, 8);

          headerRow.getCell(1).value = boxTitle;
          headerRow.getCell(1).font = { name: "Segoe UI Light", size: 22 };
          headerRow.getCell(1).alignment = { horizontal: "left" };

          const ownerRow = worksheet.getRow(4);

          ownerRow.getCell(1).value = "Manager";
          ownerRow.getCell(1).alignment = { horizontal: "center" };
          ownerRow.getCell(1).fill = {
            type: "pattern",
            pattern: "solid",
            fgColor: { argb: "e6b800" }
          };
          ownerRow.getCell(2).value = self.OwnerManagerment;
          ownerRow.getCell(2).alignment = { horizontal: "center" };

          ownerRow.getCell(3).value = "Owner";
          ownerRow.getCell(3).alignment = { horizontal: "center" };
          ownerRow.getCell(3).fill = {
            type: "pattern",
            pattern: "solid",
            fgColor: { argb: "e6b800" }
          };
          ownerRow.getCell(4).value = self.Owner;
          ownerRow.getCell(4).alignment = { horizontal: "center" };

          ownerRow.getCell(5).value = "Updater";
          ownerRow.getCell(5).alignment = { horizontal: "center" };
          ownerRow.getCell(5).fill = {
            type: "pattern",
            pattern: "solid",
            fgColor: { argb: "e6b800" }
          };
          ownerRow.getCell(6).value = self.PIC;
          ownerRow.getCell(6).alignment = { horizontal: "center" };

          ownerRow.getCell(7).value = "Sponsor";
          ownerRow.getCell(7).alignment = { horizontal: "center" };
          ownerRow.getCell(7).fill = {
            type: "pattern",
            pattern: "solid",
            fgColor: { argb: "e6b800" }
          };
          ownerRow.getCell(8).value = self.Sponsor;
          ownerRow.getCell(8).alignment = { horizontal: "center" };

          ownerRow.getCell(9).value = "Participant";
          ownerRow.getCell(9).alignment = { horizontal: "center" };
          ownerRow.getCell(9).fill = {
            type: "pattern",
            pattern: "solid",
            fgColor: { argb: "e6b800" }
          };
          ownerRow.getCell(10).value = self.Participant;
          ownerRow.getCell(10).alignment = { horizontal: "center" };
          // debugger
          if (self.$route.params.period == "H") {
            const dataRow = worksheet.getRow(6);
            dataRow.getCell(1).value = "Half Year";
            dataRow.getCell(1).fill = {
              type: "pattern",
              pattern: "solid",
              fgColor: { argb: "b3b3b3" }
            };
            let idataRow = 2;
            for (let item of self.labels) {
              dataRow.getCell(idataRow).value = item;
              dataRow.getCell(idataRow).alignment = { horizontal: "center" };
              dataRow.getCell(idataRow).fill = {
                type: "pattern",
                pattern: "solid",
                fgColor: { argb: "b3b3b3" }
              };
              idataRow++;
            }
            const targetRow = worksheet.getRow(7);
            targetRow.getCell(1).value = "Target";
            let itargetRow = 2;
            for (let item of self.targets) {
              targetRow.getCell(itargetRow).value = item + "%";
              targetRow.getCell(itargetRow).alignment = {
                horizontal: "center"
              };
              itargetRow++;
            }
            const datasetsRow = worksheet.getRow(8);
            datasetsRow.getCell(1).value = "Actual";
            let idatasetsRow = 2;
            for (let item of self.datasets) {
              datasetsRow.getCell(idatasetsRow).value = item + "%";
              datasetsRow.getCell(idatasetsRow).alignment = {
                horizontal: "center"
              };
              idatasetsRow++;
            }
          }
          if (self.$route.params.period == "M") {
            const dataRow = worksheet.getRow(6);
            dataRow.getCell(1).value = "Monthly";
            dataRow.getCell(1).fill = {
              type: "pattern",
              pattern: "solid",
              fgColor: { argb: "b3b3b3" }
            };
            let idataRow = 2;
            for (let item of self.labels) {
              dataRow.getCell(idataRow).value = item;
              dataRow.getCell(idataRow).alignment = { horizontal: "center" };
              dataRow.getCell(idataRow).fill = {
                type: "pattern",
                pattern: "solid",
                fgColor: { argb: "b3b3b3" }
              };
              idataRow++;
            }
            const targetRow = worksheet.getRow(7);
            targetRow.getCell(1).value = "Target";
            let itargetRow = 2;
            for (let item of self.targets) {
              targetRow.getCell(itargetRow).value = item + "%";
              targetRow.getCell(itargetRow).alignment = {
                horizontal: "center"
              };
              itargetRow++;
            }
            const datasetsRow = worksheet.getRow(8);
            datasetsRow.getCell(1).value = "Actual";
            let idatasetsRow = 2;
            for (let item of self.datasets) {
              datasetsRow.getCell(idatasetsRow).value = item + "%";
              datasetsRow.getCell(idatasetsRow).alignment = {
                horizontal: "center"
              };
              idatasetsRow++;
            }
          }
          if (self.$route.params.period == "W") {
            const dataRow = worksheet.getRow(6);
            dataRow.getCell(1).value = "Weekly";
            dataRow.getCell(1).fill = {
              type: "pattern",
              pattern: "solid",
              fgColor: { argb: "b3b3b3" }
            };
            let idataRow = 2;
            for (let item of self.labels) {
              dataRow.getCell(idataRow).value = item;
              dataRow.getCell(idataRow).alignment = { horizontal: "center" };
              dataRow.getCell(idataRow).fill = {
                type: "pattern",
                pattern: "solid",
                fgColor: { argb: "b3b3b3" }
              };
              idataRow++;
            }
            const targetRow = worksheet.getRow(7);
            targetRow.getCell(1).value = "Target";
            let itargetRow = 2;
            for (let item of self.targets) {
              targetRow.getCell(itargetRow).value = item + "%";
              targetRow.getCell(itargetRow).alignment = {
                horizontal: "center"
              };
              itargetRow++;
            }
            const datasetsRow = worksheet.getRow(8);
            datasetsRow.getCell(1).value = "Actual";
            let idatasetsRow = 2;
            for (let item of self.datasets) {
              datasetsRow.getCell(idatasetsRow).value = item + "%";
              datasetsRow.getCell(idatasetsRow).alignment = {
                horizontal: "center"
              };
              idatasetsRow++;
            }
          }
          if (self.$route.params.period == "Q") {
            const dataRow = worksheet.getRow(6);
            dataRow.getCell(1).value = "Quaterly";
            dataRow.getCell(1).fill = {
              type: "pattern",
              pattern: "solid",
              fgColor: { argb: "b3b3b3" }
            };
            let idataRow = 2;
            for (let item of self.labels) {
              dataRow.getCell(idataRow).value = item;
              dataRow.getCell(idataRow).alignment = { horizontal: "center" };
              dataRow.getCell(idataRow).fill = {
                type: "pattern",
                pattern: "solid",
                fgColor: { argb: "b3b3b3" }
              };
              idataRow++;
            }
            const targetRow = worksheet.getRow(7);
            targetRow.getCell(1).value = "Target";
            let itargetRow = 2;
            for (let item of self.targets) {
              targetRow.getCell(itargetRow).value = item + "%";
              targetRow.getCell(itargetRow).alignment = {
                horizontal: "center"
              };
              itargetRow++;
            }
            const datasetsRow = worksheet.getRow(8);
            datasetsRow.getCell(1).value = "Actual";
            let idatasetsRow = 2;
            for (let item of self.datasets) {
              datasetsRow.getCell(idatasetsRow).value = item + "%";
              datasetsRow.getCell(idatasetsRow).alignment = {
                horizontal: "center"
              };
              idatasetsRow++;
            }
          }
          if (self.$route.params.period == "Y") {
            const dataRow = worksheet.getRow(6);
            dataRow.getCell(1).value = "Yearly";
            dataRow.getCell(1).fill = {
              type: "pattern",
              pattern: "solid",
              fgColor: { argb: "b3b3b3" }
            };
            let idataRow = 2;
            for (let item of self.labels) {
              dataRow.getCell(idataRow).value = item;
              dataRow.getCell(idataRow).alignment = { horizontal: "center" };
              dataRow.getCell(idataRow).fill = {
                type: "pattern",
                pattern: "solid",
                fgColor: { argb: "b3b3b3" }
              };
              idataRow++;
            }
            const targetRow = worksheet.getRow(7);
            targetRow.getCell(1).value = "Target";
            let itargetRow = 2;
            for (let item of self.targets) {
              targetRow.getCell(itargetRow).value = item + "%";
              targetRow.getCell(itargetRow).alignment = {
                horizontal: "center"
              };
              itargetRow++;
            }
            const datasetsRow = worksheet.getRow(8);
            datasetsRow.getCell(1).value = "Actual";
            let idatasetsRow = 2;
            for (let item of self.datasets) {
              datasetsRow.getCell(idatasetsRow).value = item + "%";
              datasetsRow.getCell(idatasetsRow).alignment = {
                horizontal: "center"
              };
              idatasetsRow++;
            }
          }
          return Promise.resolve();
        })
        .then(function() {
          return exportDataGrid({
            worksheet: worksheet,
            component: context.priceGridInstance,
            topLeftCell: { row: 13, column: 1 },
            customizeCell: function(options) {
              const { gridCell, excelCell } = options;
              if (excelCell.row === 13) {
                excelCell.alignment = { horizontal: "center" };
                excelCell.border = {
                  top: { style: "thin" },
                  left: { style: "thin" },
                  bottom: { style: "thin" },
                  right: { style: "thin" }
                };
                excelCell.fill = {
                  type: "pattern",
                  pattern: "solid",
                  fgColor: { argb: "009900" }
                };
              }
              if (gridCell.rowType === "data") {
                excelCell.alignment = { horizontal: "center" };
              }
            }
          });
        })
        .then(function() {
          workbook.xlsx.writeBuffer().then(function(buffer) {
            saveAs(
              new Blob([buffer], { type: "application/octet-stream" }),
              "KPI system download 2.xlsx"
            );
            self.$vs.loading.close();
            success("Download success");
          });
        });
    },

    converImg() {
      var img = "http://codepattern.net/Blog/pics/CodepatternLogoN.png";
      // const ctx = document.getElementById("planet-chart");
      var img2 = this.chart.toBase64Image();
      this.urlimage = img2;
    },

    onChangePoint() {},

    LoadAllDataActionPlanByKPILevelID(id) {
      axios.get(`ChartPeriod/GetAllActionPlan/${id}/${this.currentUser}`)
      .then(res => {
        this.AllDataActionPlanByKPILevelID = res.data.data;
      });
    },

    handleResize() {
      this.Loadchart();
    },

    createChart(chartId, datasets, targets, labels, label, unit, scores) {
      let seft = this;
      let pluginsDatasets = {
        datalabels: {
          backgroundColor: function(context) {
            return context.dataset.backgroundColor;
          },
          borderRadius: 4,
          color: "white",
          font: {
            weight: "bold",
            size: 12
          },
          formatter: function(value, context) {
            if (seft.unit == "Percentage") {
              return value + "%";
            } else {
              return value;
            }
          },
          display: function(context) {},
          id: "p1"
        }
      };
      const canvas = document.getElementById(chartId);
      const ctx = canvas.getContext("2d");
      var option = {
        responsive: true,
        maintainAspectRatio: false,
        showScale: false,
        plugins: {
          datalabels: {
            backgroundColor: function(context) {
              return context.dataset.backgroundColor;
            },
            borderRadius: 4,
            color: "white",
            font: {
              weight: "bold"
            },
            formatter: function(value, context) {
              if (seft.unit == "Percentage") {
                return value + "%";
              } else {
                return value;
              }
            }
          }
        },
        title: {
          display: true,
          text: "",
          fontSize: 14,
          fontColor: "black"
        },
        elements: {
          point: {
            radius: 0
          },
          line: {
            tension: 0.2
          }
        },

        scales: {
          yAxes: [
            {
              // scaleLabel: {
              //   display: true,
              //   labelString: seft.unit,
              //   fontSize: 12,
              //   fontStyle: "normal"
              // },
              id: 'A',
              type: 'linear',
              position: 'left',
              ticks: {
                beginAtZero: true,
                padding: 10,
                fontSize: 12,
                stepSize: 10,
              }
            },
            {
              // scaleLabel: {
              //   display: true,
              //   labelString: seft.unit,
              //   fontSize: 12,
              //   fontStyle: "normal"
              // },
              id: 'B',
              type: 'linear',
              position: 'right',
              ticks: {
                max: 6,
                min: 0
              }
            }
          ],
          // yAxes: [
          //   {
          //     display: true,
          //     position: "left",
          //     ticks: {
          //       beginAtZero: true,
          //       padding: 10,
          //       fontSize: 12,
          //       stepSize: 10,
          //       min: -5
          //     },
          //     scaleLabel: {
          //       display: true,
          //       labelString: seft.unit,
          //       fontSize: 12,
          //       fontStyle: "normal"
          //     }
          //   }
          // ],
          
          xAxes: [
            {
              gridLines: {
                display: true,
                tickMarkLength: 8
              },
              ticks: {
                fontSize: 12
              },
              scaleLabel: {
                display: true,
                labelString: seft.period,
                fontSize: 12,
                fontStyle: "normal"
              }
            }
          ]
        },

        tooltips: {
          intersect: false,
          mode: "label",
          position: "nearest",
          callbacks: {}
        }
      };

      var myChart = new Chart(ctx, {
        type: "line",
        data: {
          labels: labels,
          datasets: [
            {
              label: "Perfomance Data",
              backgroundColor: seft.pointBackgroundColors,
              borderColor: "#e7263b ",
              fill: false,
              yAxisID: 'A',
              data: datasets,
              pointRadius: 10,
              pointHitRadius: 15
            },
            {
              label: "Targets",
              data: targets,
              // yAxisID: 'A',
              backgroundColor: "#3c8d8a",
              borderColor: "#3c8d8a",
              borderWidth: 3,
              fill: false,
              pointRadius: 10
            },
            {
              label: "Score for perfomance",
              data: scores,
              yAxisID: 'B',
              backgroundColor: "#db9925",
              borderColor: "#db9925",
              borderWidth: 3,
              fill: false,
              pointRadius: 10
            }
          ],
          data: labels
        },
        options: option
      });

      seft.chart = myChart;

      canvas.onclick = function(evt) {
        var activePoints = myChart.getElementAtEvent(evt);
        var ind = activePoints[0]._index + 1;
        var label = myChart.data.labels[activePoints[0]._index];
        var period = seft.$route.params.period;
        var point = ind;
        if (period == "Y") {
          seft.SortActionPlanByPeriod(label);
        } else {
          seft.SortActionPlanByPeriod(point);
        }
      };

    },

    SortActionPlanByPeriod(point) {
      axios
        .get(
          `Dataset/SortActionPlanByPeriodTest/${this.$route.params.period}/${point}/${this.kpilevelID}/${this.$route.params.kpilevelcode}/${this.currentUser}`
        )
        .then(res => {
          this.AllDataActionPlanByKPILevelID = res.data.data;
        });
    },

    convertPeriod(period, status = true) {
      if (status) {
        switch (period) {
          case "M":
            return "Months In Year";
          case "W":
            return "Weeks In Year";
          case "Q":
            return "Quarters In Year";
          case "H":
            return "Halfs In Year";
          case "Y":
            return "Years In Year";
        }
      } else {
        switch (period) {
          case "M":
            return "Monthly";
          case "W":
            return "Weekly";
          case "Q":
            return "Quarterly";
          case "H":
            return "Half Year";
          case "Y":
            return "Yearly";
        }
      }

      return "N/A";
    },

    convertPoint(point, period) {
      if (period == "M") {
        switch (point) {
          case 1:
            return "Jan";
          case 2:
            return "Fer";
          case 3:
            return "Mar";
          case 4:
            return "Apr";
          case 5:
            return "May";
          case 6:
            return "Jun";
          case 7:
            return "Jul";
          case 8:
            return "Aug";
          case 9:
            return "Sep";
          case 10:
            return "Oct";
          case 11:
            return "Nov";
          case 12:
            return "Dec";
        }
      }
      return "N/A";
    },

    Loadchart() {
      let seft = this;
      axios
        .get(
          `ChartPeriod/ListDatas2/${seft.$route.params.kpilevelcode}/${seft.$route.params.catid}/${seft.$route.params.period}/${seft.locale}/${seft.$route.params.year}/${seft.$route.params.start}/${seft.$route.params.end}`,
          {
            headers: {
              Authorization: "Bearer " + localStorage.getItem("authToken")
            }
          }
        )
        .then(response => {
          seft.statusfavorite = response.data.statusfavorite;
          seft.unit = response.data.Unit;
          seft.datasets = response.data.datasets;
          seft.scores = response.data.score;
          seft.labels = response.data.labels;
          seft.label = response.data.label;
          seft.kpiname = response.data.kpiname;
          seft.name = response.data.kpiname;
          seft.PIC = response.data.PIC;
          seft.kpilevelID = response.data.kpilevelID;
          seft.LoadAllDataActionPlanByKPILevelID(seft.kpilevelID);
          seft.Owner = response.data.Owner;
          seft.OwnerManagerment = response.data.OwnerManagerment;
          seft.Sponsor = response.data.Sponsor;
          seft.Participant = response.data.Participant;
          seft.dataremarks = response.data.Dataremarks;
          seft.targets = response.data.targets;
          seft.createChart(
            "planet-charttrend",
            seft.datasets,
            seft.targets,
            seft.labels,
            seft.label,
            seft.unit,
            seft.scores
          );
          var lastDataset = seft.datasets[seft.datasets.length - 1],
            lastTarget = seft.targets[seft.targets.length - 1];
          // seft.lastTarget = seft.targets[seft.targets.length - 1];
          if (response.data.Status == false) {
            let i = 0;
            for (i = 0; i < seft.chart.data.datasets[0].data.length; i++) {
              if (seft.chart.data.datasets[0].data[i] > lastTarget) {
                seft.pointBackgroundColors.push("green");
                seft.chart.data.datasets[0].borderColor = "#e7263b";
              } else {
                seft.pointBackgroundColors.push("red");
                seft.chart.data.datasets[0].borderColor = "#e7263b";
              }
              seft.chart.update();
            }
          } else {
            let i = 0;
            for (i = 0; i < seft.chart.data.datasets[0].data.length; i++) {
              if (seft.chart.data.datasets[0].data[i] > lastTarget) {
                seft.pointBackgroundColors.push("red");
                seft.chart.data.datasets[0].borderColor = "green";
              } else {
                seft.pointBackgroundColors.push("green");
                seft.chart.data.datasets[0].borderColor = "green";
              }
              seft.chart.update();
            }
          }
        });
    },

    changeyears(event) {
      let newYear = event.target.value;
      this.$router.push(
        `/KPITrendView/${this.$route.params.kpilevelcode}/${this.$route.params.catid}/${this.$route.params.period}/${newYear}/${this.$route.params.start}/${this.$route.params.end}`
      );
      this.Loadchart();
    }

  }
};
</script>

<style lang="scss" scoped>
img {
  width: 100px;
}
.chart-container {
  position: relative;
  margin: auto;
  height: 58vh;
  width: auto;
}
</style>
<style src="vue-multiselect/dist/vue-multiselect.min.css"></style>
