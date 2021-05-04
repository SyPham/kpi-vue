import Chart from "chart.js";
import ChartDataLabels from "chartjs-plugin-datalabels";

Chart.plugins.register(ChartDataLabels);
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
        return value;
      },
      display: function(context) {
        //return context.dataset.label !=="Target" && context.dataset.label !=="Target";
      },
      id: "p1"
    }
  }
const initLineChart = function (element,dataChart){
     new Chart(element, {
        type: "line",
        data: dataChart,
        options: {
          plugins: pluginsDatasets,
          title: {
            display: true,
            text: "KPI Chart - @models.label - @models.kpiname",
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
                display: true,
                position: "left",
                ticks: {
                  beginAtZero: true,
                  padding: 10,
                  fontSize: 12,
                  stepSize: 10,
                  min: 0
                },
                scaleLabel: {
                  display: true,
                  labelString: "unit",
                  fontSize: 12,
                  fontStyle: "normal"
                }
              }
            ],
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
                  labelString: "labelString",
                  fontSize: 12,
                  fontStyle: "normal"
                }
              }
            ]
          }
        }
      });
}

export {initLineChart};