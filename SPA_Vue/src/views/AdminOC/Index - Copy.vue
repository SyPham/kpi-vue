<template>
  <div class="row">
    <div class="container-fluid">
      <div class="row mb-2">
        <div class="col-sm-6"></div>
        <div class="col-sm-6">
          <ol class="breadcrumb float-sm-right">
            <li class="breadcrumb-item">
              <a href="#/home">Home</a>
            </li>
            <li class="breadcrumb-item active">KPI OC</li>
          </ol>
        </div>
      </div>
    </div>
    <div class="col-md-12">
      <div class="callout bg-yellow-gradient" style="border-color:#c57901">
        <h3>5. OC KPI</h3>
      </div>
    </div>
    <div class="col-md-4">
      <hierarchy></hierarchy>
      <table
        id="treetable"
        class="table table-condensed table-hover table-striped fancytree-fade-expander fancytree-colorize-selected"
      >
        <thead>
          <tr>
            <th>Level</th>
            <th class="text-right">Name</th>
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
    <div class="col-md-8">
      <listoc></listoc>
      <!-- <modal-oc></modal-oc> -->
      <div class="box" id="box">
        <div class="box-header with-border kpi-name">
          <h3 style="font-size:18px" class="box-title pull-left">KPI</h3>
          <span class="id" style="display:none"></span>
          <span class="code" style="display:none"></span>
          <span class="level" style="display:none"></span>
          <!-- <select class="form-control pull-right" style="width:50%"></select> -->
        </div>
        <div class="box-body">
          <div class="box-body">
            <table class="table table-bordered">
              <thead>
                <tr>
                  <th>#</th>
                  <th>KPI Organization Chart</th>
                  <th>Weekly</th>
                  <th>Monthly</th>
                  <th>Quaterly</th>
                  <th>Yearly</th>
                  <th>Modify</th>
                </tr>
              </thead>
              <tbody  class="tbody" id="tblkpilevel">
                <tr v-for="(item,key,index) in events" :key="index" :data-id="item.ID">
                  <td :data-kpilevel="item.ID">{{key+1}}</td>
                  <td :data-unit="item.Unit" class="tooltip-css">
                    <div class="top">
                      {{item.KPIName}}
                      <i></i>
                    </div>

                    <div class="pretty p-switch p-fill">
                      <input v-if="item.Checked == true" type="checkbox" :data-code="item.KPICode" :value="item.KPIID" checked/>
                      <input v-else type="checkbox" :data-code="item.KPICode" :value="item.KPIID" />
                      <span class="level" :data-level="item.LevelNumber" style="display:none"></span>
                      <span class="kpi-code" :data-kpicode="item.LevelCode" style="display:none"></span>
                      <div class="state p-success">
                        <i class="icon fa fa-check"></i>
                        <label class="textOverflow black">{{item.KPIName}}</label>
                      </div>
                    </div>
                  </td>
                  <td>
                     <div  class="pretty p-switch p-fill">
                      <input v-if="item.WeeklyChecked === true"  class="weekly" :data-code="item.KPICode " :value="item.KPIID" type="checkbox" checked/>
                      <input v-else class="weekly"  :data-code="item.KPICode" :value="item.KPIID" type="checkbox"/>
                      <div class="state p-success">
                        <i class="icon fa fa-check"></i>
                        <label class="black"></label>
                      </div>
                    </div>
                  </td>
                  <td>
                    <div class="pretty p-switch p-fill">
                      <input v-if="item.MonthlyChecked === true" class="monthly" data-code="item.KPICode" value="item.KPIID" type="checkbox" checked/>
                      <input v-else class="monthly" data-code="item.KPICode" value="item.KPIID" type="checkbox"/>
                      <div class="state p-success">
                        <i class="icon fa fa-check"></i>
                        <label class="black"></label>
                      </div>
                    </div>
                  </td>
                  <td>
                    <div class="pretty p-switch p-fill">
                      <input v-if="item.QuarterlyChecked === true" class="quaterly" data-code="item.KPICode" value="item.KPIID" type="checkbox" checked/>
                      <input v-else class="quaterly" data-code="item.KPICode" value="item.KPIID" type="checkbox"/>
                      <div class="state p-success">
                        <i class="icon fa fa-check"></i>
                        <label class="black"></label>
                      </div>
                    </div>
                  </td>

                  <td>
                   
                    
                    
                    <div class="pretty p-switch p-fill">
                      <input v-if="item.YearlyChecked === true" class="yearly" data-code="item.KPICode" value="item.KPIID" type="checkbox" checked/>
                      <input v-else class="yearly" data-code="item.KPICode" value="item.KPIID" type="checkbox"/>
                      <div class="state p-success">
                        <i class="icon fa fa-check"></i>
                        <label class="black"></label>
                      </div>
                    </div>
                  </td>

                  <td>
                    <div v-if="item.YearlyChecked || item.QuarterlyChecked || item.MonthlyChecked || item.WeeklyChecked === true" class="pretty p-icon p-rotate">
                      <input @click="showdata" class="showdata" type="checkbox">
                      <div class="state p-success">
                        <!-- <i class="icon far fa-edit"></i> -->
                        <!-- <label class="label label-success">Modify</label> -->
                      <button style="font-size: 12px; margin-bottom:-7px;" @click="showdata" id=""  class="btn-sm btn-info showdata ">
                        <i class="far fa-edit"></i>
                      </button>
                      </div> 
                    </div>
                    <div v-else class="pretty p-icon p-rotate">
                      
                    </div>
                  </td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
        <div class="box-footer clearfix">
          <Paginate
            v-model="page"
            :page-count="totalPage"
            :prev-text="'Prev'"
            :next-text="'Next'"
            :page-range="3"
            :margin-pages="2"
            :container-class="'pagination'"
            :page-class="'page-item'"
            :prev-class="'page-item'"
            :next-class="'page-item'"
            :page-link-class="'page-link'"
            :prev-link-class="'page-link'"
            :next-link-class="'page-link'"
            :click-handler="changePage"
          ></Paginate>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
// import { HTTP } from "../../http-constants";
// import listoc from "../../components/adminOC/List"
import Hierarchy from "../../components/adminOC/Hierarchy";
import listoc from "../../components/adminOC/Modal";
import { HTTP } from '../../http-constants';
import EventBus from "../../utils/EventBus.js";
import Paginate from "vuejs-paginate";
export default {
  name: "IndexKpi",
  data() {
    return {
      events: [],
      locale: $cookies.get("Lang"),
      totalPage: 0,
      page: 1,
      skip: 0,
      pageSize: 6,
      // test: "A"
    };
  },
  components: {
    listoc,
    Hierarchy,
    Paginate
  },
  mounted(){
    $('#modal-group-monthly #datepicker').datepicker({
      autoclose: true, 
      uiLibrary: 'bootstrap4',
      format: 'mm-dd-yyyy'
    });
    $('#modal-group-quarterly #datepicker2').datepicker({
      autoclose: true, 
      uiLibrary: 'bootstrap4',
      format: 'mm-dd-yyyy'
    });
    $('#modal-group-yearly #datepicker3').datepicker({
      autoclose: true, 
      uiLibrary: 'bootstrap4',
      format: 'mm-dd-yyyy'
    });
    $('#modal-group-showdata #datepicker4').datepicker({
      autoclose: true, 
      uiLibrary: 'bootstrap4',
      format: 'mm-dd-yyyy'
    });
    $('#modal-group-showdata #datepicker5').datepicker({
      autoclose: true, 
      uiLibrary: 'bootstrap4',
      format: 'mm-dd-yyyy'
    });
    $('#modal-group-showdata #datepicker6').datepicker({
      autoclose: true, 
      uiLibrary: 'bootstrap4',
      format: 'mm-dd-yyyy'
    });
  },
  watch:{
    locale: function(newOld,oldVal){
      this.locale = newOld
      this.loadDataKPILevel()
    }
  },
  created() {
    let seft = this;
    EventBus.$on('flag', locale =>{
      seft.locale = locale
    });
    seft.loadAll();
    $('#datepicker').datepicker({
      autoclose: true, 
      uiLibrary: 'bootstrap4',
      format: 'mm-dd-yyyy'
    });
    
  },
  methods: {
    showdata(){
      console.log('showdata')
       //update weeklychecked kpilevel
       this.loadDataKPILevel()
      $('#modal-group-showdata').modal('show').fadeIn(500);
    },
    changePage(pageNum) {
      this.loadDataKPILevel(this.name,pageNum);
    },
    loadAll() {
      let self = this;
      let glyph_opts = {
        preset: "bootstrap3",
        map: {}
      };
      function logEvent(event, data, msg) {
        msg = msg ? ": " + msg : "";
        $.ui.fancytree.info(
          "Event('" + event.type + "', node=" + data.node + ")" + msg
        );
      }
      $(function() {
        let seft = this;
        kpiController.init();
        setTimeout(function(){

          $("#treetable").fancytree({
            extensions: ["glyph", "table"],
            checkbox: false,
            selectMode: 2,
            dnd5: {
              preventVoidMoves: true,
              preventRecursion: true,
              autoExpandMS: 400,
              dragStart: function(node, data) {
                return true;
              },
              dragEnter: function(node, data) {
                // return ["before", "after"];
                return true;
              },
              dragDrop: function(node, data) {
                data.otherNode.moveTo(node, data.hitMode);
              }
            },
            glyph: glyph_opts,
            source: {
              url: "http://10.4.4.92:91/AdminKPILevel/GetListTree",
              debugDelay: 1000
            },
            table: {
              indentation: 20,
              nodeColumnIdx: 1
              //checkboxColumnIdx: 0
            },
            gridnav: {
              autofocusInput: false,
              handleCursorKeys: true
            },
            focus: function(event, data) {
              event.preventDefault();
              logEvent(event, data, ", targetType=" + data.targetType);
  
              $("#box .kpi-name h3").text("KPI - " + data.node.title);
              $("#box .kpi-name .code").text(data.node.key);
  
              //kpiController.loadDataCategory();
              self.loadDataKPILevel(true, data.node.key);
  
              $("html, body").animate(
                {
                  scrollTop: $("#box").offset().top
                },
                500
              );
              // return false to prevent default behavior (i.e. activation, ...)
              //return false;
            },
            // lazyLoad: function (event, data) {
            //     data.result = { url: "https://localhost:44309/AdminKPILevel/GetListTree", debugDelay: 1000 };
            // },
            renderColumns: function(event, data) {
              var node = data.node,
                $tdList = $(node.tr).find(">td");
  
              // (Index #0 is rendered by fancytree by adding the checkbox)
              // Set column #1 info from node data:
              // (Index #2 is rendered by fancytree)
              // Set column #3 info from node data:
  
              $tdList
                .eq(0)
                .addClass("text-bold")
                .text(node.data.levelnumber);
              $tdList
                .eq(1)
                .find("span.fancytree-icon")
                .removeClass("fancytree-icon")
                .addClass("fa fa-home");
              $tdList.eq(1).addClass("text-bold");
              // Static markup (more efficiently defined as html row template):
              // $tdList.eq(3).html("<input type='input' value='" + "" + "'>");
              // ...
            }
          });
        },500)
        $(".fancy-collapse")
          .off("click")
          .on("click", function() {
            $("#treetable")
              .fancytree("getTree")
              .expandAll(false);
          });
        $(".fancy-expand")
          .off("click")
          .on("click", function() {
            $("#treetable")
              .fancytree("getTree")
              .expandAll();
          });
      });
      let kpiLevelConfig = {
        pageSize: 6,
        pageIndex: 1
      };
      let kpiController = {
      init: function() {
        self.registerEvent();
        },
      };
    },
    loadDataKPILevel: function(changePageSize, level) {
      let self = this
      //data onto an array
      let levelid = $("#box .kpi-name .code").text();
      console.log("Id of level is " + levelid);
      let category = $("#box select").find(":selected").val();
      let kpiLevelConfig = {
        pageSize: 6,
        pageIndex: 1
      };
      let locale = $cookies.get("Lang")
      console.log(locale)
      if (category === undefined || category === "") category = 0;
      axios.get(`http://10.4.4.92:91/AdminKPILevel/LoadDataKPILevel2/${levelid}/${category}/${locale}/${kpiLevelConfig.pageIndex}/${kpiLevelConfig.pageSize}`)
      .then(r => {
        console.log(r)
        if (r.data.status) {
          let count,
            data = r.data.data,
            page = r.data.page,
            pageSize = r.data.pageSize;
          if (page === 1) count = 1;
          else count = (page - 1) * pageSize + 1;
          self.events = r.data.data;
          //  debugger
          console.log(self.events);

          self.pagingKPILevel(
            r.data.total,
            function() {
              console.log("change pageSize");
              self.loadDataKPILevel("", level);
            },
            changePageSize
          );
          self.registerEvent();
        }
      })
        
    },
    pagingKPILevel: function(totalRow, callback, changePageSize) {
      let kpiLevelConfig = {
        pageSize: 6,
        pageIndex: 1
      };
      var totalPage = Math.ceil(totalRow / kpiLevelConfig.pageSize);

      //Unbind pagination if it existed or click change pagesize
      if (
        $("#paginationKPILevel a").length === 0 ||
        changePageSize === true
      ) {
        $("#paginationKPILevel").empty();
        $("#paginationKPILevel").removeData("twbs-pagination");
        $("#paginationKPILevel").unbind("page");
      }

      $("#paginationKPILevel").twbsPagination({
        totalPages: totalPage === 0 ? 1 : totalPage,
        first: "First",
        next: "Next",
        last: "Last",
        prev: "Previous",
        visiblePages: 10,
        onPageClick: function(event, page) {
          kpiLevelConfig.pageIndex = page;
          setTimeout(callback, 500);
        }
      });
    },
    registerEvent: function () {
      let self = this
      $('#modal-group-weekly .target').off('change').on('change', function (e) { 
      });
      $('#box select').off('change').on('change', function (e) {
        var code = $(this).parent().children('.code').text();
        self.loadDataKPILevel(true, code);
      });
      
      //-----------------------------------------------------------------------------------------------------------------
      //show list kpilevel
      $('#box select').off('change').on('change', function (e) {
          var code = $(this).parent().children('.code').text();
          self.loadDataKPILevel(true, code);
      });

      $('#tblkpilevel tr td:nth-child(2) input').change(function () {
          var ID = $(this).closest('tr').data("id");
          console.log(ID)
          var KPIID = $(this).val();
          var LevelID = $('#box .kpi-name .code').text();
          var TimeCheck = new Date();
          var KPILevelCode = "";
          var Checked = $(this).is(':checked');
          var UserCheck = $('#user').data('userid');
          var Weekly = null;
          var Monthly = null;
          var Quarterly = null;
          var Yearly = null;
          var WeeklyChecked = null;
          var MonthlyChecked = null;
          var QuarterlyChecked = null;
          var YearlyChecked = null;
          var WeeklyPublic = null;
          var MonthlyPublic = null;
          var QuarterlyPublic = null;
          var YearlyPublic = null;

          self.updateKPILevel(ID, KPIID, KPILevelCode, UserCheck, Checked, WeeklyChecked, MonthlyChecked, QuarterlyChecked, YearlyChecked, Weekly, Monthly, Quarterly, Yearly, TimeCheck, LevelID, WeeklyPublic, MonthlyPublic, QuarterlyPublic, YearlyPublic);
      });

      //----------------------------------------------------------------------------------------------
       //update weeklychecked showdata
      $('#tblkpilevel .showdata ').on('click', function () {
          self.getAllUser();
          var ID = $(this).closest('tr').data("id");
          console.log("update weeklychecked kpilevel")
          console.log(ID)
          var KPIID = $(this).closest('tr').find('td:nth-child(2) input').val();

          var LevelID = $('#box .kpi-name .code').text();
          var TimeCheck = new Date();
          var KPILevelCode = "";
          var Checked = null;
          var UserCheck = $('#user').data('userid');
          var Weekly = null;
          var Monthly = null;
          var Quarterly = null;
          var Yearly = null;
          var WeeklyChecked = $(this).is(':checked');
          var MonthlyChecked = null;
          var QuarterlyChecked = null;
          var YearlyChecked = null;
          var WeeklyPublic = null;
          var MonthlyPublic = null;
          var QuarterlyPublic = null;
          var YearlyPublic = null;
          self.loadDetail(ID);
          // self.updateKPILevel(ID, KPIID, KPILevelCode, UserCheck, Checked, WeeklyChecked, MonthlyChecked, QuarterlyChecked, YearlyChecked, Weekly, Monthly, Quarterly, Yearly, TimeCheck, LevelID, WeeklyPublic, MonthlyPublic, QuarterlyPublic, YearlyPublic);
          if ($(this).is(':checked')) {
              $('#modal-group-showdata').modal('show').fadeIn(1000);
              $('#modal-group-showdata .KPILevelID').val(ID);
          }
          var unit = $(this).closest('tr').find('td:nth-child(2)').data("unit");
          if (unit === "Numeric") {
              $('#modal-group-showdata .containStandard').hide();
          } else {
              $('#modal-group-showdata .containStandard').show();
          }
          // self.loadDataKPILevel();
      });

      //update weeklychecked kpilevel
      $('#tblkpilevel .weekly').off('click').on('click', function () {
          self.getAllUser();
          var ID = $(this).closest('tr').data("id");
          console.log("update weeklychecked kpilevel")
          console.log(ID)
          var KPIID = $(this).closest('tr').find('td:nth-child(2) input').val();

          var LevelID = $('#box .kpi-name .code').text();
          var TimeCheck = new Date();
          var KPILevelCode = "";
          var Checked = null;
          var UserCheck = $('#user').data('userid');
          var Weekly = null;
          var Monthly = null;
          var Quarterly = null;
          var Yearly = null;
          var WeeklyChecked = $(this).is(':checked');
          var MonthlyChecked = null;
          var QuarterlyChecked = null;
          var YearlyChecked = null;
          var WeeklyPublic = null;
          var MonthlyPublic = null;
          var QuarterlyPublic = null;
          var YearlyPublic = null;
          self.loadDetail(ID);
          self.updateKPILevel(ID, KPIID, KPILevelCode, UserCheck, Checked, WeeklyChecked, MonthlyChecked, QuarterlyChecked, YearlyChecked, Weekly, Monthly, Quarterly, Yearly, TimeCheck, LevelID, WeeklyPublic, MonthlyPublic, QuarterlyPublic, YearlyPublic);
          if ($(this).is(':checked')) {
              $('#modal-group-weekly').modal('show').fadeIn(1000);
              $('#modal-group-weekly .KPILevelID').val(ID);
          }
          var unit = $(this).closest('tr').find('td:nth-child(2)').data("unit");
          if (unit === "Numeric") {
              $('#modal-group-weekly .containStandard').hide();
          } else {
              $('#modal-group-weekly .containStandard').show();
          }
          self.loadDataKPILevel();
      });
      //update monthly checked kpilevel
      $('#tblkpilevel .monthly').off('click').on('click', function () {

          self.getAllUser();
          var ID = $(this).closest('tr').data("id");
          var KPIID = $(this).closest('tr').find('td:nth-child(2) input').val();
          var LevelID = $('#box .kpi-name .code').text();
          var TimeCheck = new Date();
          var KPILevelCode = "";
          var Checked = null;
          var UserCheck = $('#user').data('userid');
          var Weekly = null;
          var Monthly = null;
          var Quarterly = null;
          var Yearly = null;
          var WeeklyChecked = null;
          var MonthlyChecked = $(this).is(':checked');
          var QuarterlyChecked = null;
          var YearlyChecked = null;
          var WeeklyPublic = null;
          var MonthlyPublic = null;
          var QuarterlyPublic = null;
          var YearlyPublic = null;
          self.loadDetail(ID);
          self.updateKPILevel(ID, KPIID, KPILevelCode, UserCheck, Checked, WeeklyChecked, MonthlyChecked, QuarterlyChecked, YearlyChecked, Weekly, Monthly, Quarterly, Yearly, TimeCheck, LevelID, WeeklyPublic, MonthlyPublic, QuarterlyPublic, YearlyPublic);
          if ($(this).is(':checked')) {
              $('#modal-group-monthly').modal('show').fadeIn(1000);
              $('#modal-group-monthly .KPILevelID').val(ID);
          }
          var unit = $(this).closest('tr').find('td:nth-child(2)').data("unit");
          if (unit === "Numeric") {
              $('#modal-group-monthly .containStandard').hide();
          } else {
              $('#modal-group-monthly .containStandard').show();
          }
          self.loadDataKPILevel();
      });
      //update quaterly checked kpilevel
      $('#tblkpilevel .quaterly').off('click').on('click', function () {
          self.getAllUser();

          var ID = $(this).closest('tr').data("id");
          var KPIID = $(this).closest('tr').find('td:nth-child(2) input').val();
          var LevelID = $('#box .kpi-name .code').text();
          var TimeCheck = new Date();
          var KPILevelCode = "";
          var Checked = null;
          var UserCheck = $('#user').data('userid');
          var Weekly = null;
          var Monthly = null;
          var Quarterly = null;
          var Yearly = null;
          var WeeklyChecked = null;
          var MonthlyChecked = null;
          var QuarterlyChecked = $(this).is(':checked');
          var YearlyChecked = null;
          var WeeklyPublic = null;
          var MonthlyPublic = null;
          var QuarterlyPublic = null;
          var YearlyPublic = null;
          self.loadDetail(ID);
          self.updateKPILevel(ID, KPIID, KPILevelCode, UserCheck, Checked, WeeklyChecked, MonthlyChecked, QuarterlyChecked, YearlyChecked, Weekly, Monthly, Quarterly, Yearly, TimeCheck, LevelID, WeeklyPublic, MonthlyPublic, QuarterlyPublic, YearlyPublic);
          if ($(this).is(':checked')) {
              $('#modal-group-quarterly').modal('show').fadeIn(1000);
              $('#modal-group-quarterly .KPILevelID').val(ID);
          }
          var unit = $(this).closest('tr').find('td:nth-child(2)').data("unit");
          if (unit === "Numeric") {
              $('#modal-group-quarterly .containStandard').hide();
          } else {
              $('#modal-group-quartly .containStandard').show();
          }
          self.loadDataKPILevel();
      });
      //update yearly checked kpilevel
      $('#tblkpilevel .yearly').off('click').on('click', function () {
          self.getAllUser();

          var ID = $(this).closest('tr').data("id");
          var KPIID = $(this).closest('tr').find('td:nth-child(2) input').val();
          var LevelID = $('#box .kpi-name .code').text();
          var TimeCheck = new Date();
          var KPILevelCode = "";
          var Checked = null;
          var UserCheck = $('#user').data('userid');
          var Weekly = null;
          var Monthly = null;
          var Quarterly = null;
          var Yearly = null;
          var WeeklyChecked = null;
          var MonthlyChecked = null;
          var QuarterlyChecked = null;
          var YearlyChecked = $(this).is(':checked');
          var WeeklyPublic = null;
          var MonthlyPublic = null;
          var QuarterlyPublic = null;
          var YearlyPublic = null;
          self.loadDetail(ID);
          self.updateKPILevel(ID, KPIID, KPILevelCode, UserCheck, Checked, WeeklyChecked, MonthlyChecked, QuarterlyChecked, YearlyChecked, Weekly, Monthly, Quarterly, Yearly, TimeCheck, LevelID, WeeklyPublic, MonthlyPublic, QuarterlyPublic, YearlyPublic);
          if ($(this).is(':checked')) {
              $('#modal-group-yearly').modal('show').fadeIn(1000);
              $('#modal-group-yearly .KPILevelID').val(ID);
          }
          var unit = $(this).closest('tr').find('td:nth-child(2)').data("unit");
          if (unit === "Numeric") {
              $('#modal-group-yearly .containStandard').hide();
          } else {
              $('#modal-group-yearly .containStandard').show();
          }
          self.loadDataKPILevel();
      });

      //----------------------------------------------------------------------------------------------
      //save showdata modal weekly
      $('#modal-group-showdata #btnsaveweekly1').off('click').on('click', function () {
        // debugger
          var ID = Number($('#modal-group-showdata .KPILevelID').val());
          console.log(ID)
          var Target = $('#modal-group-showdata .target1').val();
          var Period = "W";

          var KPIID = $('#modal-group-showdata .weekly1').val();
          console.log(KPIID)
          var LevelID = $('#box .kpi-name .code').text();
          var TimeCheck = new Date();
          var KPILevelCode = "";
          var Checked = null;
          var UserCheck = $('#user').data('userid');
          var Weekly = $("#modal-group-showdata .weekly1").val();
          var Monthly = null;
          var Quarterly = null;
          var Yearly = null;
          var WeeklyChecked = null;
          var MonthlyChecked = null;
          var QuarterlyChecked = null;
          var YearlyChecked = null;
          var WeeklyPublic = $('#modal-group-showdata input[type="radio"][name=weekpublic1]:checked').val() === "true" ? true : false;
          var MonthlyPublic = null;
          var QuarterlyPublic = null;
          var YearlyPublic = null;
          self.updateKPILevel(ID, KPIID, KPILevelCode, UserCheck, Checked, WeeklyChecked, MonthlyChecked, QuarterlyChecked, YearlyChecked, Weekly, Monthly, Quarterly, Yearly, TimeCheck, LevelID, WeeklyPublic, MonthlyPublic, QuarterlyPublic, YearlyPublic,Target,Period);

          $('#modal-group-weekly').modal('hide');
          // self.loadDataKPILevel();


      });

      //save showdata modal monthly
      $("#modal-group-showdata #btnsavemonthlymodal").off('click').on('click', function (e) {

          e.preventDefault();
          var ID = Number($('#modal-group-showdata .KPILevelID').val());
          var Target = $('#modal-group-showdata .target2').val();
          var Period = "M";

          var KPIID = $('#modal-group-showdata .monthly').data('id');
          var LevelID = $('#box .kpi-name .code').text();
          var TimeCheck = new Date();
          var KPILevelCode = "";
          var Checked = null;
          var UserCheck = $('#user').data('userid');
          var Weekly = null;
          var Monthly = $("#modal-group-showdata input[name=monthly-datepicker]").val();
          var Quarterly = null;
          var Yearly = null;
          var WeeklyChecked = null;
          var MonthlyChecked = null;
          var QuarterlyChecked = null;
          var YearlyChecked = null;
          var WeeklyPublic = null;
          var MonthlyPublic = $('#modal-group-showdata input[type="radio"][name=monthlypublic]:checked').val() === "true" ? true : false;
          var QuarterlyPublic = null;
          var YearlyPublic = null;
          self.updateKPILevel(ID, KPIID, KPILevelCode, UserCheck, Checked, WeeklyChecked, MonthlyChecked, QuarterlyChecked, YearlyChecked, Weekly, Monthly, Quarterly, Yearly, TimeCheck, LevelID, WeeklyPublic, MonthlyPublic, QuarterlyPublic, YearlyPublic,Target,Period);

          $('#modal-group-monthly').modal('hide');

      });

      //save showdata modal quaterly
      $("#modal-group-showdata #btnsavequaterlymodal").off('click').on('click', function (e) {
          e.preventDefault();
          var ID = Number($('#modal-group-showdata .KPILevelID').val());
          var Target = $('#modal-group-showdata .target3').val();
          var Period = "Q";

          var KPIID = $('#modal-group-showdata .quaterly').data('id');
          var LevelID = $('#box .kpi-name .code').text();
          var TimeCheck = new Date();
          var KPILevelCode = "";
          var Checked = null;
          var UserCheck = $('#user').data('userid');
          var Weekly = null;
          var Monthly = null;
          var Quarterly = $("#modal-group-showdata input[name=quarterly-datepicker]").val();
          var Yearly = null;
          var WeeklyChecked = null;
          var MonthlyChecked = null;
          var QuarterlyChecked = null;
          var YearlyChecked = null;
          var WeeklyPublic = null;
          var MonthlyPublic = null;
          var QuarterlyPublic = $('#modal-group-showdata input[type="radio"][name=quarterlypublic]:checked').val() === "true" ? true : false;
          var YearlyPublic = null;

          self.updateKPILevel(ID, KPIID, KPILevelCode, UserCheck, Checked, WeeklyChecked, MonthlyChecked, QuarterlyChecked, YearlyChecked, Weekly, Monthly, Quarterly, Yearly, TimeCheck, LevelID, WeeklyPublic, MonthlyPublic, QuarterlyPublic, YearlyPublic,Target,Period);

          $('#modal-group-quarterly').modal('hide');
      });

      // save modal yearly
      $("#modal-group-showdata #btnsaveyearly").off('click').on('click', function (e) {
          e.preventDefault();
          var ID = Number($('#modal-group-showdata .KPILevelID').val());
          var Target = $('#modal-group-showdata .target4').val();
          var Period = "Y";

          var KPIID = $('#modal-group-showdata .yearly').data('id');
          var LevelID = $('#box .kpi-name .code').text();
          var TimeCheck = new Date();
          var KPILevelCode = "";
          var Checked = null;
          var UserCheck = $('#user').data('userid');
          var Weekly = null;
          var Monthly = null;
          var Quarterly = null;
          var Yearly = $("#modal-group-showdata input[name=yearly-datepicker]").val();
          var WeeklyChecked = null;
          var MonthlyChecked = null;
          var QuarterlyChecked = null;
          var YearlyChecked = null;
          var WeeklyPublic = null;
          var MonthlyPublic = null;
          var QuarterlyPublic = null;
          var YearlyPublic = $('#modal-group-showdata input[type="radio"][name=quarterlypublic]:checked').val() === "true" ? true : false;

          self.updateKPILevel(ID, KPIID, KPILevelCode, UserCheck, Checked, WeeklyChecked, MonthlyChecked, QuarterlyChecked, YearlyChecked, Weekly, Monthly, Quarterly, Yearly, TimeCheck, LevelID, WeeklyPublic, MonthlyPublic, QuarterlyPublic, YearlyPublic,Target,Period);

          $('#modal-group-yearly').modal('hide');

      });


      //update weekly modal
      $('#btnsaveweekly').off('click').on('click', function () {
        // debugger
          var ID = Number($('#modal-group-weekly .KPILevelID').val());
          console.log(ID)
          var Target = $('#modal-group-weekly .target').val();
          var Period = "W";

          var KPIID = $('#modal-group-weekly .weekly').val();
          console.log(KPIID)
          var LevelID = $('#box .kpi-name .code').text();
          var TimeCheck = new Date();
          var KPILevelCode = "";
          var Checked = null;
          var UserCheck = $('#user').data('userid');
          var Weekly = $("#modal-group-weekly .weekly").val();
          var Monthly = null;
          var Quarterly = null;
          var Yearly = null;
          var WeeklyChecked = null;
          var MonthlyChecked = null;
          var QuarterlyChecked = null;
          var YearlyChecked = null;
          var WeeklyPublic = $('#modal-group-weekly input[type="radio"][name=weekpublic]:checked').val() === "true" ? true : false;
          var MonthlyPublic = null;
          var QuarterlyPublic = null;
          var YearlyPublic = null;
          self.updateKPILevel(ID, KPIID, KPILevelCode, UserCheck, Checked, WeeklyChecked, MonthlyChecked, QuarterlyChecked, YearlyChecked, Weekly, Monthly, Quarterly, Yearly, TimeCheck, LevelID, WeeklyPublic, MonthlyPublic, QuarterlyPublic, YearlyPublic,Target,Period);

          $('#modal-group-weekly').modal('hide');
          self.loadDataKPILevel();


      });

      //update monthly modal
      $("#btnsavemonthlymodal").off('click').on('click', function (e) {

          var ID = Number($('#modal-group-monthly .KPILevelID').val());
          var Target = $('#modal-group-monthly .target').val();
          var Period = "M";

          var KPIID = $('#modal-group-monthly .monthly').data('id');
          var LevelID = $('#box .kpi-name .code').text();
          var TimeCheck = new Date();
          var KPILevelCode = "";
          var Checked = null;
          var UserCheck = $('#user').data('userid');
          var Weekly = null;
          var Monthly = $("#modal-group-monthly input[name=monthly-datepicker]").val();
          var Quarterly = null;
          var Yearly = null;
          var WeeklyChecked = null;
          var MonthlyChecked = null;
          var QuarterlyChecked = null;
          var YearlyChecked = null;
          var WeeklyPublic = null;
          var MonthlyPublic = $('#modal-group-monthly input[type="radio"][name=monthlypublic]:checked').val() === "true" ? true : false;
          var QuarterlyPublic = null;
          var YearlyPublic = null;
          self.updateKPILevel(ID, KPIID, KPILevelCode, UserCheck, Checked, WeeklyChecked, MonthlyChecked, QuarterlyChecked, YearlyChecked, Weekly, Monthly, Quarterly, Yearly, TimeCheck, LevelID, WeeklyPublic, MonthlyPublic, QuarterlyPublic, YearlyPublic,Target,Period);

          $('#modal-group-monthly').modal('hide');
           self.loadDataKPILevel();
      });
      //update quaterly modal
      $("#btnsavequaterlymodal").off('click').on('click', function (e) {
          e.preventDefault();
          var ID = Number($('#modal-group-quarterly .KPILevelID').val());
          var Target = $('#modal-group-quarterly .target').val();
          var Period = "Q";

          var KPIID = $('#modal-group-quarterly .quaterly').data('id');
          var LevelID = $('#box .kpi-name .code').text();
          var TimeCheck = new Date();
          var KPILevelCode = "";
          var Checked = null;
          var UserCheck = $('#user').data('userid');
          var Weekly = null;
          var Monthly = null;
          var Quarterly = $("#modal-group-quarterly input[name=quarterly-datepicker]").val();
          var Yearly = null;
          var WeeklyChecked = null;
          var MonthlyChecked = null;
          var QuarterlyChecked = null;
          var YearlyChecked = null;
          var WeeklyPublic = null;
          var MonthlyPublic = null;
          var QuarterlyPublic = $('#modal-group-quarterly input[type="radio"][name=quarterlypublic]:checked').val() === "true" ? true : false;
          var YearlyPublic = null;

          self.updateKPILevel(ID, KPIID, KPILevelCode, UserCheck, Checked, WeeklyChecked, MonthlyChecked, QuarterlyChecked, YearlyChecked, Weekly, Monthly, Quarterly, Yearly, TimeCheck, LevelID, WeeklyPublic, MonthlyPublic, QuarterlyPublic, YearlyPublic,Target,Period);

          $('#modal-group-quarterly').modal('hide');
           self.loadDataKPILevel();
      });
      //update yearly modal
      $("#btnsaveyearly").off('click').on('click', function () {
          var ID = Number($('#modal-group-yearly .KPILevelID').val());
          var Target = $('#modal-group-yearly .target').val();
          var Period = "Y";

          var KPIID = $('#modal-group-yearly .yearly').data('id');
          var LevelID = $('#box .kpi-name .code').text();
          var TimeCheck = new Date();
          var KPILevelCode = "";
          var Checked = null;
          var UserCheck = $('#user').data('userid');
          var Weekly = null;
          var Monthly = null;
          var Quarterly = null;
          var Yearly = $("#modal-group-yearly input[name=yearly-datepicker]").val();
          var WeeklyChecked = null;
          var MonthlyChecked = null;
          var QuarterlyChecked = null;
          var YearlyChecked = null;
          var WeeklyPublic = null;
          var MonthlyPublic = null;
          var QuarterlyPublic = null;
          var YearlyPublic = $('#modal-group-quarterly input[type="radio"][name=quarterlypublic]:checked').val() === "true" ? true : false;

          self.updateKPILevel(ID, KPIID, KPILevelCode, UserCheck, Checked, WeeklyChecked, MonthlyChecked, QuarterlyChecked, YearlyChecked, Weekly, Monthly, Quarterly, Yearly, TimeCheck, LevelID, WeeklyPublic, MonthlyPublic, QuarterlyPublic, YearlyPublic,Target,Period);

          $('#modal-group-yearly').modal('hide');
           self.loadDataKPILevel();

      });

    },
    updateKPILevel: function (ID ,KPIID, KPILevelCode, UserCheck, Checked, WeeklyChecked, MonthlyChecked, QuarterlyChecked, YearlyChecked, Weekly, Monthly, Quarterly, Yearly, TimeCheck, LevelID, WeeklyPublic, MonthlyPublic, QuarterlyPublic, YearlyPublic,Target ="",Period="") {
      var mObj = {
        ID : Number(ID),
        KPIID: KPIID,
        KPILevelCode: KPILevelCode,
        UserCheck: UserCheck,
        Checked: Checked,
        WeeklyChecked: WeeklyChecked,
        MonthlyChecked: MonthlyChecked,
        QuarterlyChecked: QuarterlyChecked,
        YearlyChecked: YearlyChecked,
        Weekly: Weekly,
        Monthly: Monthly,
        Quarterly: Quarterly,
        Yearly: Yearly,
        TimeCheck: TimeCheck,
        LevelID: LevelID,
        WeeklyPublic: WeeklyPublic,
        MonthlyPublic: MonthlyPublic,
        QuarterlyPublic: QuarterlyPublic,
        YearlyPublic: YearlyPublic,
        Target: Target,
        Period:Period
      };
      axios.post("AdminKPILevel/UpdateKPILevel",JSON.stringify(mObj))
      .then(result=>{
          if (result) {
            success('Update successfully!.');
          }
      })
    },
    getAllUser() {
      axios.get("AdminKPILevel/GetListAllUser")
      .then(data=>{
        var users = [],
            username,
            fullname;
          var arrays = data;
          $.each(arrays, function(i, item) {
            users.push({
              username: item.Username,
              fullname: item.FullName
            });
          });

          $(".KPILevelSelect2").suggest("@@", {
            data: users,
            map: function(user) {
              return {
                value: user.username + "\f",
                text:
                  "<strong>" +
                  user.username +
                  "  </strong> <small>" +
                  user.fullname +
                  "</small>"
              };
            },
            get: function(index) {
              console.log(index);
            }
          });
      })
        
    },
    loadDetail: function(id) {
      let self = this
      axios.get(`AdminKPILevel/GetbyID/${id}`)
      .then(res=>{
        console.log("loadDetail");
        console.log(res);

          var result = res.data;
          
          // $(".KPILevelID").val(id);
          $("#modal-group-weekly .weekly").val(result.data.Weekly);
          $("#modal-group-weekly .weekly").val(result.data.Weekly);
          $("#modal-group-weekly input[name=weekpublic]").val(result.data.WeeklyPublic);
          $("#modal-group-weekly .standard").val(result.data.WeeklyStandard);
          $('#modal-group-weekly .target').val(result.WorkingPlanOfWeekly);

          $("#modal-group-showdata .weekly1").val(result.data.Weekly);
          $("#modal-group-showdata .weekly1").val(result.data.Weekly);
          $("#modal-group-showdata input[name=weekpublic1]").val(result.data.WeeklyPublic);
          $("#modal-group-showdata .standard").val(result.data.WeeklyStandard);
          $('#modal-group-showdata .target1').val(result.WorkingPlanOfWeekly);

          // $("#modal-group-monthly .monthly").val(kpiController.convertDateJson(result.Monthly));
          // $("#modal-group-monthly input[name=monthpublic]").val(result.MonthlyChecked);
          // $("#modal-group-monthly .standard").val(result.MonthlyStandard);

          $('#modal-group-monthly input[name=monthly-datepicker]').val(self.convertDateJson(result.data.Monthly));
          $('#modal-group-monthly input[name=monthpublic]').val(result.data.MonthlyChecked);
          $('#modal-group-monthly .standard').val(result.data.MonthlyStandard);
          $('#modal-group-monthly .target').val(result.WorkingPlanOfMonthly);
          console.log(result.WorkingPlanOfMonthly)

          $('#modal-group-showdata input[name=monthly-datepicker]').val(self.convertDateJson(result.data.Monthly));
          $('#modal-group-showdata input[name=monthpublic]').val(result.data.MonthlyChecked);
          $('#modal-group-showdata .standard').val(result.data.MonthlyStandard);
          $('#modal-group-showdata .target2').val(result.WorkingPlanOfMonthly);
          console.log(result.WorkingPlanOfMonthly)


          $('#modal-group-quarterly input[name=quarterly-datepicker]').val(self.convertDateJson(result.data.Quarterly));
          $('#modal-group-quarterly input[name=quarterpublic]').val(result.data.QuarterlyChecked);
          $('#modal-group-quarterly .standard').val(result.data.QuarterlyStandard);
          $('#modal-group-quarterly .target').val(result.WorkingPlanOfQuarterly);


          $('#modal-group-showdata input[name=quarterly-datepicker]').val(self.convertDateJson(result.data.Quarterly));
          $('#modal-group-showdata input[name=quarterpublic]').val(result.data.QuarterlyChecked);
          $('#modal-group-showdata .standard').val(result.data.QuarterlyStandard);
          $('#modal-group-showdata .target3').val(result.WorkingPlanOfQuarterly);
          console.log(result.WorkingPlanOfQuarterly)
          $('#modal-group-yearly  input[name=yearly-datepicker]').val(self.convertDateJson(result.Yearly));
          $('#modal-group-yearly input[name=quaterpublic]').val(result.data.YearlyChecked);
          $('#modal-group-yearly .standard').val(result.data.YearlyStandard);
          $('#modal-group-yearly .target').val(result.WorkingPlanOfYearly);


          $('#modal-group-showdata  input[name=yearly-datepicker]').val(self.convertDateJson(result.Yearly));
          $('#modal-group-showdata input[name=quaterpublic]').val(result.data.YearlyChecked);
          $('#modal-group-showdata .standard').val(result.data.YearlyStandard);
          $('#modal-group-showdata .target4').val(result.WorkingPlanOfYearly);
          console.log(result.WorkingPlanOfYearly)

      })
      
    },
    convertDateJson(d) {
      // if (d === null) d = "/Date(1000000000000)/";

      // let milli = d.replace(/\/Date\((-?\d+)\)\//, "$1");
      let date = new Date(d|| new Date());
      let dd = String(date.getDate()).padStart(2, "0");
      let mm = String(date.getMonth() + 1).padStart(2, "0"); //January is 0!
      let yyyy = date.getFullYear();
      return mm + "/" + dd + "/" + yyyy;
    }
  }
};
</script>
