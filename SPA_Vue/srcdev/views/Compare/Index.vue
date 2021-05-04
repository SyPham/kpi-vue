<template>
  <div>
    <section id="chartperiod" class="content">
      <div class="row">
        <div class="col-md-12">
          <!-- LINE CHART -->
          <div class="box box-widget">
            <div class="box-header with-border">
              <h3 class="box-title" style="font-weight:bold" >KPI Compare Chart - {{kpiname}} - {{period}}</h3>

              
            </div>
            <div class="box-body">
              <button
                type="button"
                class="btn margin pull-right"
                style="background-color:#09a99f;color:#fff"
              >
                <i class="fa fa-star"></i>
                <span>Target : </span>
                <span class="standard text-bold" style>standard</span>
              </button>
              <button
                type="button"
                class="btn margin pull-right"
                style="background-color:#09a99f;color:#fff"
              >
                <i class="fa fa-star"></i>
                <span>Target : </span>
                <span class="standard text-bold" style>chartVM2s.targets</span>
              </button>
              <div class="chart">
                <canvas id="planet-chart" style="height:500px"></canvas>
              </div>
            </div>
          </div>
        </div>
       
      </div>
    </section>
  </div>
</template>
<script>
import { HTTP } from "../../http-constants";
import VueJwtDecode from "vue-jwt-decode";
import LineChart from "../../utils/ChartJs/LineChart";
import { initLineChart } from "../../plugins/LineChartPlugin";
import planetChartData from "../../plugins/Chartjs2/Demo";
export default {
  data() {
    return {
      itemList: [],
      chart: {},
      planetChartData: planetChartData,
      datacollection: {},
      weekly: [],
      years: [],
      data: [],
      kpiname: "",
      datasets: {},
      period: "",
      unit: "",
      labels: [],
      targets: [],
      standards: [],
      dataremarks: [],
      statusfavorite: true,
      dataCharts: {},
      options: {
        animation: {
          duration: 0
        },
        hover: {
          animationDuration: 0
        },
        responsiveAnimationDuration: 0,
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
              return value;
            }
          }
        },
        scales: {
          yAxes: [
            {
              stacked: true
            }
          ]
        },
        title: {
          display: true,
          text: "",
          fontSize: 20,
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
              // display: true,
              position: "left",
              ticks: {
                beginAtZero: true,
                padding: 10,
                fontSize: 12,
                stepSize: 10,
                min: 0
              },
              scaleLabel: {
                // display: true,
                labelString: this.unit,
                fontSize: 12,
                fontStyle: "normal"
              }
            }
          ],
          xAxes: [
            {
              gridLines: {
                // display: true,
                tickMarkLength: 8
              },
              ticks: {
                fontSize: 12
              },
              scaleLabel: {
                // display: true,
                labelString: this.period,
                fontSize: 12,
                fontStyle: "normal"
              }
            }
          ]
        }
      },
    };
  },
  mounted() {
    let seft = this
    seft.createChart("planet-chart", seft.datasets, seft.targets, seft.labels);
  },
  created() {
      let seft = this
      seft.LoadDataCompare();
  },
  mounted(){
    let seft = this
    setTimeout(function(){
      seft.LoadDataCompare();
    },300)
    
  },
  methods: {
    convertPeriod(period) {
        switch (period) {
          case "M":
            return "Monthly";
          case "W":
            return "Weekly";
          case "Q":
            return "Quarterly";
          case "Y":
            return "Yearly";
        }
      return "N/A";
    },
    createChart(chartId, datasets, targets, labels, label, unit) {
      let seft = this;
      const ctx = document.getElementById(chartId);

      const myChart = new Chart(ctx, {
        type: "line",
        labels: labels,
        data: {
          labels: labels,
          datasets: seft.itemList
          
        },
        options: seft.options
      });
      seft.chart = myChart;
      seft.chart.update();
      //console.log(seft.chart);
    },
    LoadDataCompare(){
        let seft = this
        axios.get(`Compare/Compare/${seft.$route.params.obj}`)
        .then(r=>{
          //console.log(r.data)
          seft.kpiname = r.data[0].kpiname
          seft.period = seft.convertPeriod(r.data[0].period)
         // console.log(seft.period)
          seft.datasets  = r.data;
          let COLORS =  [
            '#FF3784',
            '#36A2EB',
            '#4BC0C0',
            '#F77825',
            '#9966FF',
            '#00A8C6',
            '#379F7A',
            '#CC2738',
            '#8B628A',
            '#8FBE00',
            '#606060'
          ];
          for (var item = 0; item < seft.datasets.length; item ++) {
            let  ListData = {
              label: seft.datasets[item].label,
              spanGaps: false,
              backgroundColor: COLORS[(item+1)],
              borderColor: COLORS[(item+1)],
              fill: false,
              data: seft.datasets[item].datasets,
              datalabels: {
                align: 'center',
                anchor: 'center'
              }
            };
            seft.itemList.push(ListData);
                
         console.log('itemList',seft.itemList);
          }
         // console.log(seft.itemList);
          seft.label = r.data[0].label;
          seft.labels = r.data[0].labels;
          seft.targets = r.data[0].targets;
          // console.log(seft.label)

          (seft.options.label = r.label),
          (seft.options.title.text =
            "KPI Compare Chart -" + r.data[0].kpiname + " - " + seft.convertPeriod(
            r.data[0].period
          )),
          (seft.options.scales.yAxes[0].scaleLabel.labelString =
            r.data[0].Unit);
          seft.options.scales.xAxes[0].scaleLabel.labelString = seft.convertPeriod(
            r.data[0].period
          );
          seft.createChart(
            "planet-chart",
            seft.datasets,
            seft.targets,
            seft.labels,
            seft.label,
            seft.unit
          );
        })
    }
  }
};
</script>