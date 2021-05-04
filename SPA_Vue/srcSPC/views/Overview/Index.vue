<template>
<!-- <section class="content">

  <div class="container-fluid">
    <div class="row">

      <div class="col-md-12">
        <div class="box boxDataset boxdataset-scrollbar">
          <div class="box-body ">
            <div class="col-md-12">
              <div class="form-inline"  id="searchYear">

                <label class="my-1 mr-2" for="startMonth">Period</label>
                <div class="form-group">
                    <ejs-dropdownlist @change="OnchangePeriod()" :value="value" v-model="value" id='dropdownlistPeriod'  placeholder='Select a Period' :allowFiltering='allowFiltering'  :dataSource='dataperiod' :fields='fieldsperiod'></ejs-dropdownlist>
                </div>
                 <label class="my-1 mr-2" for="YearOfYear">Year</label>
                <select v-model="year" class="custom-select form-control my-1 mr-sm-2 YearOfYear" id="YearOfYear">
                  <option :value="0" selected>Choose...</option>

                  <option v-for="(n, key, index) in 100" :key="index" :value="2000 + n">{{2000 + n}}</option>
                </select>
                <label class="my-1 mr-2" for="endMonth">Category</label>
                <div class="form-group">
                  <select @change="OnchangeCategory($event)" class="custom-select form-control" id="YearOfYear">
                    <option value="0" selected>All</option>

                    <option  v-for="(item, key, index) in categorymodel" :key="index" :value="item.ID">{{item.Name}}</option>
                  </select>
                </div>
                <p style="color:transparent">|</p>
                <div class="form-group">
                  <button @click="refresh()"  type="button" class="btn bg-teal  ">
                    <i class="fas fa-sync-alt"></i> Refresh
                  </button>
                </div>
                <p style="color:transparent">|</p>
                <div class="form-group">
                  <button @click="exportTableToExcel('tblDataset2','KPI System Download')" type="button" class="btn bg-teal">
                    <i class="far fa-file-excel"></i> Export
                  </button>
                </div>

              </div>
            </div>
          </div>
          <div class="box-body table-responsive no-padding" id="tblDataset2" >
            <div  class="col-md-12">
              <hr style="color: transparent">
              <div class="cart">
                <div class="card-header">
                  <div class="row">
                    <div class="col-md-4">
                      <h3 class="card-title">List Overview</h3>
                    </div>
                    <div class="col-md-4">

                    </div>
                    <div class="col-md-4"></div>
                  </div>

                </div>
                <div class="card-body table-responsive p-0 tableFixHead" style="height: 700px">
                  <div class="row">
                    <div class="col-md-4">
                      <span class="card-title">OC</span>
                    </div>
                  </div>
                  <table border="1" class="table table-head-fixed table-bordered text-nowrap" >
                    <tr>
                      <th>No.</th>
                      <th>Kind</th>
                      <th >KPI</th>
                      <th>OC</th>
                      <th>Manager</th>
                      <th>Owner</th>
                      <th>Updater</th>
                      <th>Target</th>
                      <th v-for="(item,key,index) in title" :key="index" class="text-center">{{item}}</th>
                    </tr>
                    <tr v-for="(item,key,index) in data" :key="index" v-if="item.Datasets.length >= 0 " >
                      <td >{{key+1}}</td>
                      <td>{{item.CategoryCode}}</td>
                      <td  @click="pushTrendView(item)">
                        <span style="text-decoration: none ; cursor:pointer ;color:green" >{{item.KPIName}}</span>
                      </td>
                      <td>{{item.Area}}</td>
                      <td>{{item.Manager}}</td>
                      <td>{{item.Owner}}</td>
                      <td>{{item.Updater}}</td>
                      <td v-if="item.Unit == 'Percentage'" style="background-color: yellow" class="datasetTd text-center active-tdtarget">{{item.Target + '%'}}</td>
                      <td v-else style="background-color: yellow" class="datasetTd text-center active-tdtarget">{{item.Target }}</td>
                      <td v-for="(item2,key,index) in item.Datasets" :key="index"
                        :style="item2.ValueArray[1] === 'True' ? 'background-color: green'  : 'background-color: red' "
                        :class="checktarget(item,item2)"
                        >
                        <span v-if="item.Unit == 'Percentage'"> {{item2.ValueArray[0] == 0 ? "N/A" : item2.ValueArray[0] + '%'}} </span>
                        <span v-else> {{item2.ValueArray[0] }} </span>
                      </td>

                    </tr>
                  </table>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</section> -->
<section class="content">

  <div class="container-fluid">
    <div class="row">

      <div class="col-md-12">
        <div class="box boxDataset boxdataset-scrollbar">
          <div class="box-body ">
            <div class="col-md-12">

              <!-- @* month *@ -->
              <div class="form-inline"   id="searchMonth">

                <label class="my-1 mr-2" for="startMonth">Period</label>
                <div class="form-group">

                  <ejs-dropdownlist :change="OnchangePeriod" :value="value" v-model="value" id='dropdownlistPeriod'  placeholder='Select a Period' :allowFiltering='allowFiltering'  :dataSource='dataperiod' :fields='fieldsperiod'></ejs-dropdownlist>
                </div>

                <div  class="form-group" v-if="periodDefault ==  'm'">
                  <label class="my-1 mr-2" for="startMonth">Start</label>
                  <select v-model="vstartM" @change="starts($event)" class="custom-select form-control my-1 mr-sm-2" id="startMonth">
                    <option value="0" >Choose...</option>
                    <option value="1" >Jan</option>
                    <option value="2">Feb</option>
                    <option value="3">Mar</option>
                    <option value="4">Apr</option>
                    <option value="5">May</option>
                    <option value="6">Jun</option>
                    <option value="7">Jul</option>
                    <option value="8">Aug</option>
                    <option value="9">Sep</option>
                    <option value="10">Oct</option>
                    <option value="11">Nov</option>
                    <option value="12">Dec</option>
                  </select>

                  <label class="my-1 mr-2" for="endMonth">End</label>
                  <select v-model="vendM" @change="ends($event)" class="custom-select form-control my-1 mr-sm-2" id="endMonth">
                    <option value="0" >Choose...</option>
                    <option value="1">Jan</option>
                    <option value="2">Feb</option>
                    <option value="3">Mar</option>
                    <option value="4">Apr</option>
                    <option value="5">May</option>
                    <option value="6">Jun</option>
                    <option value="7">Jul</option>
                    <option value="8">Aug</option>
                    <option value="9">Sep</option>
                    <option value="10">Oct</option>
                    <option value="11">Nov</option>
                    <option value="12" >Dec</option>
                  </select>
                </div>

                <div  class="form-group" v-else-if="periodDefault ==  'w'">
                  <label class="my-1 mr-2" for="startWeek">Start</label>
                  <select v-model="vstartW" @change="starts($event)" class="custom-select form-control my-1 mr-sm-2" id="startWeek">
                    <option value="0" >Choose...</option>

                    <option v-for="(n, key, index) in 53" :key="index" :value="n" >Week {{n}}</option>
                  </select>
                  <label class="my-1 mr-2" for="endWeek">End</label>
                  <select v-model="vendW" @change="ends($event)" class="custom-select form-control my-1 mr-sm-2" id="endWeek">
                    <option value="0" selected>Choose...</option>

                    <option v-for="(n, key, index) in 53" :key="index" :value="n"  >Week {{n}}</option>
                  </select>
                </div>

                <div  class="form-group" v-else-if="periodDefault ==  'q'">
                  <label class="my-1 mr-2" for="startMonth">Start</label>
                  <select v-model="vstartQ" @change="starts($event)" class="custom-select form-control my-1 mr-sm-2" id="startQuarter">
                    <option value="0" selected>Choose...</option>

                    <option v-for="(n, key, index)  in 4"  :key="index" :value=" n">Quarter {{n}}</option>
                  </select>
                  <label class="my-1 mr-2" for="endMonth">End</label>
                  <select v-model="vendQ" @change="ends($event)" class="custom-select form-control my-1 mr-sm-2" id="endQuarter">
                    <option value="0" selected>Choose...</option>

                    <option v-for="(n, key, index)  in 4"  :key="index" :value=" n">Quarter {{n}}</option>
                  </select>
                </div>

                <div  class="form-group" v-else-if="periodDefault ==  'h'">
                  <label class="my-1 mr-2" for="YearOfYear">Year</label>
                  <select v-model="year" @change="changeyears($event)" class="custom-select form-control my-1 mr-sm-2 YearOfYear" id="YearOfYear">
                    <option :value="0" selected>Choose...</option>

                    <option v-for="(n, key, index) in 100" :key="index" :value="2000 + n">{{2000 + n}}</option>
                  </select>
                </div>

                <div  class="form-group" v-else>
                  <label class="my-1 mr-2" for="startMonth">Start</label>
                  <select v-model="vstartY" @change="starts($event)" class="custom-select form-control my-1 mr-sm-2" id="startYear">
                    <option value="0" selected>Choose...</option>

                    <option v-for="(n, key, index) in 100" :key="index" :value="2009 + n">Year {{2010 + n}}</option>
                  </select>
                  <label class="my-1 mr-2" for="endMonth">End</label>
                  <select v-model="vendY" @change="ends($event)" class="custom-select form-control my-1 mr-sm-2" id="endYear">
                    <option value="0" selected>Choose...</option>

                    <option v-for="(n, key, index) in 100" :key="index" :value="2009 + n">Year {{2010 + n}}</option>
                  </select>
                </div>

                <label class="my-1 mr-2" for="endMonth">Category</label>

                <div class="form-group">
                  <select @change="OnchangeCategory($event)"  class="custom-select form-control" id="YearOfYear">
                    <option value="0" selected>All</option>

                    <option  v-for="(item, key, index) in categorymodel" :key="index" :value="item.ID">{{item.Name}}</option>
                  </select>
                </div>
                <p style="color:transparent">|</p>
                <div class="form-group">
                  <button @click="refresh()"  type="button" class="btn bg-teal  ">
                    <i class="fas fa-sync-alt"></i> Refresh
                  </button>
                </div>
                <p style="color:transparent">|</p>
                <div class="form-group">
                  <button @click="exportTableToExcel('tblDataset2','KPI System Download')" type="button" class="btn bg-teal">
                    <i class="far fa-file-excel"></i> Export
                  </button>
                </div>

              </div>

            </div>
          </div>

          <div class="box-body table-responsive no-padding" id="tblDataset2" >
            <!-- <loading :active.sync="isLoading"
            :is-full-page="fullPage"></loading> -->
            <div class="vld-parent">
              <loading :active.sync="isLoading"
              :is-full-page="fullPage"></loading>
              <div  class="col-md-12" >
                <hr style="color: transparent">
                <div class="cart">
                  <div class="card-header">
                    <div class="row">
                      <div class="col-md-4">
                        <h3 class="card-title">List Overview</h3>
                      </div>
                      <div class="col-md-4">

                      </div>
                      <div class="col-md-4"></div>
                    </div>
                    <!-- <hr> -->

                  </div>

                  <div v-if="isYear == false && isHalf == false" v-for="(items,key,index) in data" :key="index"  >
                    <div class="cart">
                      <div class="card-header">
                        <span style="color: red">{{items.Area}}</span>
                      </div>
                    </div>
                    <div class="card-body table-responsive p-0 tableFixHead" style="height: 500px">
                      <table border="1" class="table table-head-fixed table-bordered text-nowrap" >
                        <tr>
                          <th>No.</th>
                          <th>Kind</th>
                          <th>KPI</th>
                          <th>Manager</th>
                          <th>Owner</th>
                          <th>Updater</th>
                          <th>Target</th>
                          <th v-for="(item,key,index) in title" :key="index" class="text-center">{{item}}</th>
                        </tr>
                        <tr v-for="(item,key,index) in items.Data" :key="index" v-if="item.Datasets.length >= 0 " >
                          <td>{{key+1}}</td>
                          <td>{{item.CategoryCode}}</td>
                          <td @click="pushTrendView(item)">
                            <span style="text-decoration: none ; cursor:pointer ;color:green" >{{item.KPIName}}</span>
                          </td>
                          <td>{{item.Manager}}</td>
                          <td>{{item.Owner}}</td>
                          <td>{{item.Updater}}</td>
                          <td v-if="item.Unit == 'Percentage'" style="background-color: yellow" class="datasetTd text-center active-tdtarget">{{item.Target + '%'}}</td>
                          <td v-else style="background-color: yellow" class="datasetTd text-center active-tdtarget">{{item.Target }}</td>

                          <td v-for="(item2,key,index) in item.Datasets" :key="index"
                            :style="item2.ValueArray[1] === 'True' ? 'background-color: green'  : 'background-color: red' "
                            :class="checktarget(item,item2)"
                            >
                            <span v-if="item.Unit == 'Percentage'"> {{item2.ValueArray[0] == 0 ? "N/A" : item2.ValueArray[0] + '%'}} </span>
                            <span v-else> {{item2.ValueArray[0] }} </span>
                          </td>

                        </tr>
                      </table>
                    </div>
                    <hr>

                  </div>

                  <div v-else-if="isYear == false && isHalf == true" v-for="(items,key,index) in data" :key="index"  >
                    <div class="cart">
                      <div class="card-header">
                        <span style="color: red">{{items.Area}}</span>
                      </div>
                    </div>
                    <div class="card-body table-responsive p-0 tableFixHead" style="height: 500px">
                      <table border="1" class="table table-head-fixed table-bordered text-nowrap" >
                        <tr>
                          <th>No.</th>
                          <th>Kind</th>
                          <th>KPI</th>
                          <th>Manager</th>
                          <th>Owner</th>
                          <th>Updater</th>
                          <th>H1 Target</th>
                          <th>H2 Target</th>
                          <th v-for="(item,key,index) in title" :key="index" class="text-center">{{item}}</th>
                        </tr>
                        <tr v-for="(item,key,index) in items.Data" :key="index" v-if="item.Datasets.length >= 0 " >
                          <td>{{key+1}}</td>
                          <td>{{item.CategoryCode}}</td>
                          <td @click="pushTrendView(item)">
                            <span style="text-decoration: none ; cursor:pointer ;color:green" >{{item.KPIName}}</span>
                          </td>
                          <td>{{item.Manager}}</td>
                          <td>{{item.Owner}}</td>
                          <td>{{item.Updater}}</td>

                          <td v-if="item.Unit == 'Percentage'" style="background-color: yellow" class="datasetTd text-center active-tdtarget">{{item.Target2 + '%'}}</td>
                          <td v-else style="background-color: yellow" class="datasetTd text-center active-tdtarget">{{item.Target2 }}</td>

                          <td v-if="item.Unit == 'Percentage'" style="background-color: yellow" class="datasetTd text-center active-tdtarget">{{item.Target + '%'}}</td>
                          <td v-else style="background-color: yellow" class="datasetTd text-center active-tdtarget">{{item.Target }}</td>

                          <td v-for="(item2,key,index) in item.Datasets" :key="index"
                            :style="item2.ValueArray[1] === 'True' ? 'background-color: green'  : 'background-color: red' "
                            :class="checktarget(item,item2)"
                            >
                            <span v-if="item.Unit == 'Percentage'"> {{item2.ValueArray[0] == 0 ? "N/A" : item2.ValueArray[0] + '%'}} </span>
                            <span v-else> {{item2.ValueArray[0] }} </span>
                          </td>

                        </tr>
                      </table>
                    </div>
                    <hr>

                  </div>

                  <div v-else v-for="(items,key,index) in data" :key="index"  >
                    <div class="cart">
                      <div class="card-header">
                        <span style="color: red">{{items.Area}}</span>
                      </div>
                    </div>
                    <div class="card-body table-responsive p-0 tableFixHead" style="height: 500px">
                      <table border="1" class="table table-head-fixed table-bordered text-nowrap" >
                        <tr>
                          <th>No.</th>
                          <th>Kind</th>
                          <th>KPI</th>
                          <th>Manager</th>
                          <th>Owner</th>
                          <th>Updater</th>
                          <th>Target({{year - 1}})</th>
                          <th>Target({{year}})</th>
                          <th v-for="(item,key,index) in title" :key="index" class="text-center">{{item}}</th>
                        </tr>
                        <tr v-for="(item,key,index) in items.Data" :key="index" v-if="item.Datasets.length >= 0 " >
                          <td>{{key+1}}</td>
                          <td>{{item.CategoryCode}}</td>
                          <td @click="pushTrendView(item)">
                            <span style="text-decoration: none ; cursor:pointer ;color:green" >{{item.KPIName}}</span>
                          </td>
                          <td>{{item.Manager}}</td>
                          <td>{{item.Owner}}</td>
                          <td>{{item.Updater}}</td>

                          <td v-if="item.Unit == 'Percentage'" style="background-color: yellow" class="datasetTd text-center active-tdtarget">{{item.Target2 + '%'}}</td>
                          <td v-else style="background-color: yellow" class="datasetTd text-center active-tdtarget">{{item.Target2 }}</td>

                          <td v-if="item.Unit == 'Percentage'" style="background-color: yellow" class="datasetTd text-center active-tdtarget">{{item.Target + '%'}}</td>
                          <td v-else style="background-color: yellow" class="datasetTd text-center active-tdtarget">{{item.Target }}</td>

                          <td v-for="(item2,key,index) in item.Datasets" :key="index"
                            :style="item2.ValueArray[1] === 'True' ? 'background-color: green'  : 'background-color: red' "
                            :class="checktarget(item,item2)"
                            >
                            <span v-if="item.Unit == 'Percentage'"> {{item2.ValueArray[0] == 0 ? "N/A" : item2.ValueArray[0] + '%'}} </span>
                            <span v-else> {{item2.ValueArray[0] }} </span>
                          </td>

                        </tr>
                      </table>
                    </div>
                    <hr>

                  </div>


                </div>
              </div>

            </div>
          </div>
        </div>
      </div>
    </div>
  </div>

</section>
</template>
<script>
import VueJwtDecode from "vue-jwt-decode";
import Multiselect from "vue-multiselect";
// import Paginate from "vuejs-paginate";
import Vue from "vue";
import { DropDownListPlugin } from "@syncfusion/ej2-vue-dropdowns";
Vue.use(DropDownListPlugin);
import EventBus from "../../utils/EventBus.js";
// Import component
import Loading from 'vue-loading-overlay';
// Import stylesheet
import 'vue-loading-overlay/dist/vue-loading.css';
export default {

  data() {
    return {
      isLoading: false,
      fullPage: true,
      vstartM: 1,
      vendM: 12,
      vstartW: 1,
      vendW: 12,
      vstartQ: 1,
      vendQ: 4,
      vstartY: new Date().getFullYear() - 10,
      vendY: new Date().getFullYear() - 1,
      name: ' ',
      searchname: '',
      valueCategory: null,
      value: 'm',
      valueDefault: 'Monthly',
      categorymodel: [],
      dataperiod: [
        { text: "Weekly", value: "w" },
        { text: "Monthly", value: "m" },
        { text: "Quaterly", value: "q" },
        { text: "Half Year", value: "h" },
        { text: "Yearly", value: "y" }
      ],
      fields : { text: "Name", value: "ID" },
      fieldsperiod : { text: "text", value: "value" },
      allowFiltering: true,
      periodDefault: "m",
      currentUser: Number(VueJwtDecode.decode(localStorage.getItem("authToken")).nameid),
      data: [],
      totalPage: 0,
      page: 1,
      skip: 0,
      pageSize: 10,
      keyword: ' ',
      data: [],
      data2: [],
      title: [],
      period: "",
      datasets: [],
      categoryname: "",
      kpiname: "",
      KPILevelCode: "",
      searchyear: 0,
      vstart: 0,
      vend: 0,
      isYear: false,
      isHalf: false,
      start: 1,
      end: 12,
      year: new Date().getFullYear(),
      catid: 0,
      datamap:[],
      locale: $cookies.get("Lang")

    };
  },

  provide: {

  },
  watch: {
    locale: function(newOld,oldVal){
      this.locale = newOld
      this.LoadDataset()
    }
  },
  components:{
    // Paginate,
    Multiselect,
    Loading
  },
  mounted(){
  },
  destroyed() {

  },
  beforeDestroy: function () {

  },

  created() {
    let seft = this;
    seft.period = seft.$route.params.period;
    seft.vstart = seft.$route.params.start;
    seft.vend = seft.$route.params.end;
    seft.searchyear = seft.$route.params.year
    EventBus.$on('flag', locale =>{
      this.locale = locale
    });
    // this.$vs.loading()
    this.isLoading = true ;
    this.LoadDataset()
    // setTimeout(() => {
    // },5000)

  },

  methods: {

    changePage(pageNum) {
      this.LoadDataset(this.name, pageNum);
    },

    checktarget(item,item2){
      if(item.Status == false) {
        if(item2.ValueArray[1] === 'True'){
          return 'datasetTd text-center active-td2'
        }else{
          return 'datasetTd text-center active-td'
        }
      }else{
        if(item2.ValueArray[1] === 'True'){
          return 'datasetTd text-center active-td'
        }else{
          return 'datasetTd text-center active-td2'
        }
      }
    },

    pushTrendView(item){
      let pr = this.convertPeriodPush(this.periodDefault,true)
      return this.$router.push(`/KPITrendView/${item.KPILevelCode}/${item.CategoryID}/${pr}/${this.year}/1/${this.end}`)
    },

    OnchangePeriod(){
      this.isLoading = true ;
      this.periodDefault = this.value
      if(this.periodDefault == "w"){
        //this.isYear = false
        this.start = 1
        this.end = 53
      }
      if(this.periodDefault == "m"){
        //this.isYear = false
        this.start = 1
        this.end = 12
      }
      if(this.periodDefault == "h"){
        //this.isYear = false
        this.start = 1
        this.end = 2
      }
      if(this.periodDefault == "y"){
        //this.isYear = true
        this.start = this.year - 10
        this.end = this.year
      }
      if(this.periodDefault == "q"){
        //this.isYear = false
        this.start = 1
        this.end = 4
      }
      this.LoadDataset()

    },

    refresh() {
      this.catid = 0 ;
      this.periodDefault = "m";
      this.value = 'm'
      this.year = new Date().getFullYear()
      this.isLoading = true ;
      this.LoadDataset()
    },

    OnchangeCategory(event){
      // this.$vs.loading()
      this.isLoading = true ;
      this.catid = event.target.value
      this.LoadDataset()

    },

    exportTableToExcel(tableID, filename = ''){
      var downloadLink;
      var dataType = 'application/vnd.ms-excel';
      var tableSelect = document.getElementById(tableID);
      var tableHTML = tableSelect.outerHTML.replace(/ /g, '%20');
      // Specify file name
      filename = filename?filename+'.xls':'excel_data.xls';
      // Create download link element
      downloadLink = document.createElement("a");
      document.body.appendChild(downloadLink);
      if(navigator.msSaveOrOpenBlob){
        var blob = new Blob(['\ufeff', tableHTML], {
          type: dataType
        });
        navigator.msSaveOrOpenBlob( blob, filename);
      }else{
        // Create a link to the file
        downloadLink.href = 'data:' + dataType + ', ' + tableHTML;
        // Setting the file name
        downloadLink.download = filename;
        //triggering the function
        downloadLink.click();
      }
    },

    convertPeriodPush(period, status = true) {
      if (status) {
        switch (period) {
          case "m":
            return "M";
          case "w":
            return "W";
          case "q":
            return "Q";
          case "h":
            return "H";
          case "y":
            return "Y";
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

    JSONDateWithTime(dateStr){
      var d = new Date(dateStr);
      var m, day;
      m = d.getMonth() + 1;
      if (m < 10)
          month = '0' + m;
      else
          month = m;
      if (d.getDate() < 10)
          day = '0' + d.getDate();
      else
          day = d.getDate();

      var year = d.getFullYear();
      var formattedDate = day + "/" + month + "/" + year;
      var hours = d.getHours() < 10 ? "0" + d.getHours() : d.getHours();
      var minutes = d.getMinutes() < 10 ? "0" + d.getMinutes() : d.getMinutes();
      var seconds = d.getSeconds();
      var formattedTime = hours + ":" + minutes;
      formattedDate = formattedTime + " - " + formattedDate;

      //Ngày giờ hiện tại
      var dateObj = new Date();
      var dayNow = dateObj.getDate();
      var monthNow = dateObj.getMonth() + 1;
      var yearNow = dateObj.getFullYear();
      var hoursNow = dateObj.getHours();
      var minutesNow = dateObj.getMinutes();
      var secondsNow = dateObj.getSeconds();

      dateObj = formattedDate;

      if (hoursNow - hours === 0 && minutesNow - minutes === 0 && yearNow - year === 0 && monthNow - month === 0 && dayNow - day === 0) {
        if (secondsNow - seconds === 0)
          dateObj = "just recently";
        else if (secondsNow - seconds === 1)
          dateObj = secondsNow - seconds + " second ago";
        else
          dateObj = secondsNow - seconds + " seconds ago";
      }

      if (hoursNow - hours === 0 && minutesNow - minutes > 0 && yearNow - year === 0 && monthNow - month === 0 && dayNow - day === 0) {
        if (minutesNow - minutes === 1)
          dateObj = minutesNow - minutes + " minute ago";
        else
          dateObj = minutesNow - minutes + " minutes ago";
      }

      if (hoursNow - hours > 0 && yearNow - year === 0 && monthNow - month === 0 && dayNow - day === 0) {
        if (hoursNow - hours === 1)
          dateObj = hoursNow - hours + " hour ago";
        else
          dateObj = hoursNow - hours + " hours ago";
      }

      if (yearNow - year === 0 && monthNow - month === 0 && dayNow - day > 0 && dayNow - day <= 7) {
        if (dayNow - day === 1)
          dateObj = dayNow - day + " day ago";
        else
          dateObj = dayNow - day + " days ago";
      }

      if (yearNow - year === 0 && monthNow !== month) {
        day = dayNow - day;
        var month = monthNow - month;
        day = month * 30 + day;
        if (0 < day && day <= 7) {
          dateObj = day + " days ago";
        }

      }

      return dateObj;
    },

    changeyears(event) {
      this.year = event.target.value;
      this.isLoading = true ;
      this.LoadDataset();
    },

    starts(event){
      this.start = event.target.value
      // this.$vs.loading()
      this.isLoading = true ;
      this.LoadDataset();
    },

    ends(event){
      this.end = event.target.value
      // this.$vs.loading()
      this.isLoading = true ;
      this.LoadDataset();
    },

    convertPeriod(period, status = true) {
      if (status) {
        switch (period) {
          case "m":
            return "Months In Year";
          case "w":
            return "Weeks In Year";
          case "q":
            return "Quarters In Year";
          case "h":
            return "Halfs In Year";
          case "y":
            return "Years In Year";
        }
      } else {
        switch (period) {
          case "m":
            return "Monthly";
          case "w":
            return "Weekly";
          case "q":
            return "Quarterly";
          case "q":
            return "HalfYear";
          case "y":
            return "Yearly";
        }
      }

      return "N/A";
    },

    validate(){
       var isValid = true;
      if ($('.Title').val().trim() === "") {
        $('.Title').css('border-color', 'Red');
        isValid = false;
      }
      else {
        $('.Title').css('border-color', 'lightgrey');
      }
      if ($('.DueDate').val().trim() === "") {
        $('.DueDate').css('border-color', 'Red');
        isValid = false;
      }
      else {
        $('.DueDate').css('border-color', 'lightgrey');
      }
      return isValid;
    },

    LoadTitle() {
      let seft = this;
      switch (seft.periodDefault) {
        case "w":
          seft.title = seft.data2.map(x => {
            return x.Week;
          });
          break;
        case "m":
          seft.title = seft.data2.map(x => {
            return seft.convertNumberMonthToText(x.Week);
          });
          break;
        case "q":
          seft.title = seft.data2.map(x => {
            return "Quarter " + x.Week;
          });
          break;
        case "h":
          seft.title = seft.data2.map(x => {
            return "Half " + x.Week;
          });
          break;
        case "y":
          seft.title = seft.data2.map(x => {
            return "Year " + x.Week;
          });
          break;
      }
    },

    convertNumberMonthToText(period) {
      switch (period) {
        case 1:
          return "Jan";
        case 2:
          return "Feb";
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
      return "N/A";
    },

    LoadDataset() {
      let seft = this;
      axios.get(`Dataset/GetAllKPIOverviewWithLang/${seft.catid}/${seft.periodDefault}/${this.locale}/${seft.start}/${seft.end}/${seft.year}`)
      .then(response => {
        seft.datamap = response.data
        seft.data = response.data.model;
        seft.categorymodel = response.data.categorymodel
        seft.data2 = response.data.listTitle2;
        seft.LoadTitle();
        if (seft.periodDefault == 'y') {
          seft.isYear = true;
          seft.isHalf = false;
        } else if (seft.periodDefault == 'h') {
          seft.isYear = false;
          seft.isHalf = true;
        }
        else {
          seft.isYear = false;
        }
        this.isLoading = false;
        // this.$vs.loading.close()
      })
    },

    SortDataset() {
      let seft = this;
      axios.get(`Dataset/SortKPIOverview/${seft.catid}/${seft.periodDefault}/${seft.start}/${seft.end}/${seft.year}`)
      .then(response => {
        seft.datamap = response.data
        seft.data = response.data.model;
        seft.categorymodel = response.data.categorymodel
        seft.data2 = response.data.listTitle;
        seft.LoadTitle();
      })
    }

  }
};
</script>
<style src="vue-multiselect/dist/vue-multiselect.min.css"></style>
<style lang="scss" scoped>

.tableFixHead {
  overflow: auto;
}

.tableFixHead table {
  border-collapse: collapse;
  width: 100%;
}

.tableFixHead th, .tableFixHead td {
  padding: 8px 16px;
}

td:first-child, th:first-child {
  position: sticky;
  left:0;
  z-index:1;
  background-color:white;
}

td:nth-child(3),th:nth-child(3)  {
  position: sticky;
  left:40px;
  z-index:1;
  background-color:white;
}

.tableFixHead th {
  position: sticky;
  top: 0;
  background: #eee;
  z-index:2
}

th:first-child , th:nth-child(3) {
  z-index:3
}

table {
  border-collapse: collapse;
  border-spacing: 0;
  width: 100%;
  border: 1px solid #ddd;
}

th, td {
  text-align: left;
  padding: 8px;
}
</style>
