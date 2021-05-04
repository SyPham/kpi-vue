<template>
  <div>
    <section id="chartperiod" class="animatedParent">
      <div class="row">
        <div class="col-md-12">
        </div>
        <div class="col-md-12">
          <!-- LINE CHART -->
          <div class="box box-widget">
            <div class="box-header with-border">
              <h5 v-if="period == 'W'" class="box-title1" style="font-weight:bold">KPI Chart - {{name}} - {{$t("Weekly")}}</h5>
              <h5 v-if="period == 'M'" ref="message"  class="box-title1" id="box-title1" style="font-weight:bold">KPI Chart - {{name}} - {{$t("Monthly")}}</h5>
              <h5 v-if="period == 'Q'" class="box-title1" style="font-weight:bold">KPI Chart - {{name}} - {{$t("Quarterly")}}</h5>
              <h5 v-if="period == 'H'" class="box-title1" style="font-weight:bold">KPI Chart - {{name}} - Half Year</h5>
              <h5 v-if="period == 'Y'" class="box-title1" style="font-weight:bold">KPI Chart - {{name}} - {{$t("Yearly")}}</h5>
            </div>
            <div class="box-body">

              <!-- month  -->
              <div class="row" :style="period ==  'M' ? '':'display:none'">
                <div class="col-md-7">
                  <div class="row">
                    <div class="col-md">
                      <div class="form-group">
                        <label class="my-1 mr-2" for="startMonth">{{$t("Year")}}</label>
                        <select v-model="searchyear" @change="changeyears($event)"  class="custom-select form-control year my-1 mr-sm-2" id="monthOfYear">
                          <option value="0" selected>Choose...</option>
                          <option
                           v-for="(n, key, index) in 100" 
                           :key="index" 
                           :value="2000 + n"
                           > 
                           {{2000 + n}}
                           </option>
                        </select>
                      </div>
                    </div>
                    <div class="col-md">
                      <div class="form-group">
                        <label class="my-1 mr-2" for="startMonth">{{$t("Start")}}</label>
                        <select v-model="vstart" @change="starts($event)" class="custom-select form-control my-1 mr-sm-2" id="startMonth">
                          <option value="0" selected>Choose...</option>
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
                          <option value="12">Dec</option>
                        </select>
                      </div>
                    </div>
                    <div class="col-md">
                      <div class="form-group">
                        <label class="my-1 mr-2" for="endMonth">{{$t("End")}}</label>
                        <select v-model="vend" @change="ends($event)" class="custom-select form-control my-1 mr-sm-2" id="endMonth">
                          <option value="0" selected>Choose...</option>
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
                          <option value="12">Dec</option>
                        </select>
                      </div>
                    </div>
                    <div class="col-md">
                      <div class="form-group">
                        <label class="my-1 mr-2" style="color:transparent" for="endMonth">asd</label>
                        <button type="button" @click="searchyear=searchyear, vstart=1, vend=12" class="btn bg-teal my-1 mr-sm-2 form-control margin btnClearSearch">
                          <i class="fas fa-minus-circle"></i> {{$t("btnClearSearch")}}
                        </button>
                      </div>
                    </div>
                  </div>
                  <div class="form-inline" :style="period ==  'M' ? 'display:block':'display:none'" id="searchMonth"></div>
                </div>
                
                <div class="col-md-5">
                  <div class="float-right mt-5">
                    <button type="button" data-toggle="modal" data-target="#modal-group" class="btn btn-danger margin btnLike pull-right" id="btnCompare" @click="compare">
                      <i class="fa fa-adjust"></i> Compare
                    </button>

                    <button v-if="statusfavorite == false" type="button" class="btn margin btnLike bg-navy pull-right" id="btnLike" @click="btnLike">
                      <i class="fa fa-heart"></i> Add Favourite
                    </button>

                    <button v-else type="button" class="btn btn-default margin btnLike pull-right disabled" disabled id="btnLike">
                      <i class="fa fa-heart"></i> Added Favourite
                    </button>
                  </div>
                </div>
              </div>

              <!-- half year  -->
              <div class="row" :style="period ==  'H' ? '':'display:none'">
                <div class="col-md-7">
                  <div class="row">
                    <div class="col-md">
                      <div class="form-group">
                        <label class="my-1 mr-2" for="startMonth">{{$t("Year")}}</label>
                        <select v-model="searchyear"  @change="changeyears($event)" class="custom-select form-control year my-1 mr-sm-2" id="monthOfYear">
                          <option value="0" selected>Choose...</option>
                          <option
                           v-for="(n, key, index) in 100" 
                           :key="index" 
                           :value="2000 + n"
                           > 
                           {{2000 + n}}
                           </option>
                        </select>
                      </div>
                    </div>
                    <div class="col-md">
                      <div class="form-group">
                        <label class="my-1 mr-2" for="startMonth">{{$t("Start")}}</label>
                        <select v-model="vstart" @change="starts($event)" class="custom-select form-control my-1 mr-sm-2" id="startMonth">
                          <option value="0" selected>Choose...</option>
                          <option value="1">Jun</option>
                          <option value="2">Dec</option>
                        </select>
                      </div>
                    </div>
                    <div class="col-md">
                      <div class="form-group">
                        <label class="my-1 mr-2" for="endMonth">{{$t("End")}}</label>
                        <select v-model="vend" @change="ends($event)" class="custom-select form-control my-1 mr-sm-2" id="endMonth">
                          <option value="0" selected>Choose...</option>
                          <option value="1">Jun</option>
                          <option value="2">Dec</option>
                        </select>
                      </div>
                    </div>
                    <div class="col-md">
                      <div class="form-group">
                        <label class="my-1 mr-2" style="color:transparent" for="endMonth">asd</label>
                        <button type="button" @click="searchyear=searchyear, vstart=1, vend=12" class="btn bg-teal my-1 mr-sm-2 form-control margin btnClearSearch">
                          <i class="fas fa-minus-circle"></i> {{$t("btnClearSearch")}}
                        </button>
                      </div>
                    </div>
                  </div>
                  <div class="form-inline" :style="period ==  'M' ? 'display:block':'display:none'" id="searchMonth"></div>
                </div>
                
                <div class="col-md-5">
                  <div class="float-right mt-5">
                    <button type="button" data-toggle="modal" data-target="#modal-group" class="btn btn-danger margin btnLike pull-right" id="btnCompare" @click="compare">
                      <i class="fa fa-adjust"></i> Compare
                    </button>

                    <button v-if="statusfavorite == false" type="button" class="btn margin btnLike bg-navy pull-right" id="btnLike" @click="btnLike">
                      <i class="fa fa-heart"></i> Add Favourite
                    </button>

                    <button v-else type="button" class="btn btn-default margin btnLike pull-right disabled" disabled id="btnLike">
                      <i class="fa fa-heart"></i> Added Favourite
                    </button>
                  </div>
                </div>
              </div>

               <!-- week  -->
              <div class="row" :style="period ==  'W' ? '':'display:none'">
                <div class="col-md-7">
                  <div class="row">
                    <div class="col-md">
                      <div class="form-group">
                        <label class="my-1 mr-2" for="startMonth">{{$t("Year")}}</label>
                        <select v-model="searchyear" @change="changeyears($event)" class="custom-select form-control year my-1 mr-sm-2" id="weekOfYear">
                          <option value="0" selected>Choose...</option>
                          <option v-for="(n, key, index) in 100" :key="index" :value="2000 + n">  {{2000 + n}}</option>
                        </select>
                      </div>
                    </div>
                    <div class="col-md">
                      <div class="form-group">
                        <label class="my-1 mr-2" for="startWeek">{{$t("WeekStart")}}</label>
                        <select v-model="vstart" @change="starts($event)" class="custom-select form-control year my-1 mr-sm-2" id="startWeek">
                          <option value="0" selected>Choose...</option>
                          <option v-for="(n, key, index) in 53" :key="index" :value="n">Week {{n}}</option>
                        </select>
                      </div>
                    </div>
                    <div class="col-md">
                      <div class="form-group">
                        <label class="my-1 mr-2" for="endWeek">{{$t("WeekEnd")}}</label>
                        <select v-model="vend" @change="ends($event)" class="custom-select form-control year my-1 mr-sm-2" id="endWeek">
                          <option value="0" selected>Choose...</option>
                          <option v-for="(n, key, index) in 53" :key="index" :value=" n">Week {{ n}}</option>
                        </select>
                      </div>
                    </div>
                    <div class="col-md">
                      <div class="form-group">
                        <label class="my-1 mr-2" style="color:transparent" for="endMonth">asd</label>
                        <button type="button" @click="searchyear=searchyear, vstart=1, vend=53" class="btn bg-teal my-1 mr-sm-2 form-control margin btnClearSearch">
                          <i class="fas fa-minus-circle"></i> {{$t("btnClearSearch")}}
                        </button>
                      </div>
                    </div>
                  </div>
                  <div class="form-inline" :style="period ==  'M' ? 'display:block':'display:none'" id="searchMonth"></div>
                </div>
                
                <div class="col-md-5">
                  <div class="float-right mt-5">
                    <button type="button" data-toggle="modal" data-target="#modal-group" class="btn btn-danger margin btnLike pull-right" id="btnCompare" @click="compare">
                      <i class="fa fa-adjust"></i> Compare
                    </button>

                    <button v-if="statusfavorite == false" type="button" class="btn margin btnLike bg-navy pull-right" id="btnLike" @click="btnLike">
                      <i class="fa fa-heart"></i> Add Favourite
                    </button>

                    <button v-else type="button" class="btn btn-default margin btnLike pull-right disabled" disabled id="btnLike">
                      <i class="fa fa-heart"></i> Added Favourite
                    </button>
                  </div>
                </div>
              </div>

               <!-- quater  -->
              <div class="row" :style="period ==  'Q' ? '':'display:none'">
                <div class="col-md-7">
                  <div class="row">
                    <div class="col-md">
                      <div class="form-group">
                        <label class="my-1 mr-2" for="startMonth">{{$t("Year")}}</label>
                        <select v-model="searchyear" class="custom-select form-control year my-1 mr-sm-2" id="quarterOfYear">
                          <option value="0" selected>Choose...</option>
                          <option v-for="(n, key, index) in 100" :key="index" :value="2000 + n"> {{2000 + n}}</option>
                        </select>
                      </div>
                    </div>
                    <div class="col-md">
                      <div class="form-group">
                        <label class="my-1 mr-2" for="startMonth">{{$t("Start")}}</label>
                        <select v-model="vstart" class="custom-select form-control my-1 mr-sm-2" id="startQuarter">
                          <option value="0" selected>Choose...</option>
                          <option v-for="(n, key, index)  in 4" :selected="start == n ? true : false" :key="index" :value=" n">Quarter {{n}}</option>
                        </select>
                      </div>
                    </div>
                    <div class="col-md">
                      <div class="form-group">
                        <label class="my-1 mr-2" for="endMonth">{{$t("End")}}</label>
                        <select v-model="vend" class="custom-select form-control my-1 mr-sm-2" id="endQuarter">
                          <option value="0" selected>Choose...</option>
                          <option v-for="(n, key, index)  in 4" :selected="end == n ? true : false" :key="index" :value=" n">Quarter {{n}}</option>
                        </select>
                      </div>
                    </div>
                    <div class="col-md">
                      <div class="form-group">
                        <label class="my-1 mr-2" style="color:transparent" for="endMonth">asd</label>
                        <button type="button" @click="searchyear=searchyear, vstart=1, vend=4" class="btn bg-teal my-1 mr-sm-2 form-control margin btnClearSearch">
                          <i class="fas fa-minus-circle"></i> {{$t("btnClearSearch")}}
                        </button>
                      </div>
                    </div>
                  </div>
                  <div class="form-inline" :style="period ==  'M' ? 'display:block':'display:none'" id="searchMonth"></div>
                </div>
                
                <div class="col-md-5">
                  <div class="float-right mt-5">
                    <button type="button" data-toggle="modal" data-target="#modal-group" class="btn btn-danger margin btnLike pull-right" id="btnCompare" @click="compare">
                      <i class="fa fa-adjust"></i> Compare
                    </button>

                    <button v-if="statusfavorite == false" type="button" class="btn margin btnLike bg-navy pull-right" id="btnLike" @click="btnLike">
                      <i class="fa fa-heart"></i> Add Favourite
                    </button>

                    <button v-else type="button" class="btn btn-default margin btnLike pull-right disabled" disabled id="btnLike">
                      <i class="fa fa-heart"></i> Added Favourite
                    </button>
                  </div>
                </div>
              </div>

               <!-- year  -->
              <div class="row" :style="period ==  'Y' ? '':'display:none'">
                <div class="col-md-7">
                  <div class="row">
                    <div class="col-md">
                      <div class="form-group">
                        <label class="my-1 mr-2" for="YearOfYear">Year</label>
                        <select v-model="searchyear" class="custom-select form-control year my-1 mr-sm-2" id="YearOfYear">
                          <option value="0" selected>Choose...</option>
                          <option v-for="(n, key, index) in 100" :key="index" :value="2000 + n"> Year {{2000 + n}}</option>
                        </select>
                      </div>
                    </div>
                    <div class="col-md">
                      <div class="form-group">
                        <label class="my-1 mr-2" for="startMonth">Start</label>
                        <select v-model="vstart" class="custom-select form-control year my-1 mr-sm-2" id="startYear">
                          <option value="0" selected>Choose...</option>
                          <option v-for="(n, key, index) in 100" :key="index" :value="2000 + n"> Year {{2000 + n}}</option>
                        </select>
                      </div>
                    </div>
                    <div class="col-md">
                      <div class="form-group">
                        <label class="my-1 mr-2" for="endMonth">End</label>
                        <select v-model="vend" class="custom-select form-control year my-1 mr-sm-2" id="endYear">
                          <option value="0" selected>Choose...</option>
                          <option v-for="(n, key, index) in 100" :key="index" :value="2000 + n"> Year {{2000 + n}}</option>
                        </select>
                      </div>
                    </div>
                    <div class="col-md">
                      <div class="form-group">
                        <label class="my-1 mr-2" style="color:transparent" for="endMonth">asd</label>
                        <button type="button" @click="searchyear=searchyear, vstart=vstart, vend=vend" class="btn bg-teal my-1 mr-sm-2 form-control margin btnClearSearch">
                          <i class="fa fa-remove"></i> Reset
                        </button>
                      </div>
                    </div>
                  </div>
                  <div class="form-inline" :style="period ==  'M' ? 'display:block':'display:none'" id="searchMonth"></div>
                </div>
                
                <div class="col-md-5">
                  <div class="float-right mt-5">
                    <button type="button" data-toggle="modal" data-target="#modal-group" class="btn btn-danger margin btnLike pull-right" id="btnCompare" @click="compare">
                      <i class="fa fa-adjust"></i> Compare
                    </button>

                    <button v-if="statusfavorite == false" type="button" class="btn margin btnLike bg-navy pull-right" id="btnLike" @click="btnLike">
                      <i class="fa fa-heart"></i> Add Favourite
                    </button>

                    <button v-else type="button" class="btn btn-default margin btnLike pull-right disabled" disabled id="btnLike">
                      <i class="fa fa-heart"></i> Added Favourite
                    </button>
                  </div>
                </div>
              </div>

              <div class="row">
                <div class="col-md-4">
                  <div class="form-inline">
                    <label style="margin: 3px" class="my-1 mr-2" for="startMonth">Step</label>
                    <input type="number" class="form-control" min="0" step="0" v-model="stepSize" id="stepChart" @change="stepChart" style="width:70px;margin: 3px"/>
                    <label style="margin: 3px" class="my-1 mr-2" for="endMonth">Min</label>
                    <input type="number" class="form-control" min="0" step="0" v-model="min" id="minChart" @change="minChart" style="width:70px;margin: 3px"/>

                    <button style="margin: 5px" type="button" @click="hiddenData()" class="btn btn-sm bg-navy margin btnHiddenData">Hide Data</button>
                    <button style="margin: 5px" type="button" @click="showData()" class="btn btn-sm bg-success margin btnShowData">Show Data</button>
                  </div>
                </div>
                <div class="col-md-8">
                  <button type="button" class="btn btn-sm bg-navy margin">
                    {{$t("Manager_label")}}:
                    <strong>{{OwnerManagerment}}</strong>
                  </button>
                  <button type="button" class="btn btn-sm bg-navy margin">
                    {{$t("Owner_label")}}:
                    <strong>{{Owner}}</strong>
                  </button>
                  <button type="button" class="btn btn-sm bg-navy margin">
                    {{$t("Updater_label")}}:
                    <strong>{{PIC}}</strong>
                  </button>
                  <button type="button" class="btn btn-sm bg-navy margin">
                    {{$t("Sponsor_label")}}:
                    <strong>{{Sponsor}}</strong>
                  </button>
                  <button type="button" class="btn btn-sm bg-navy margin">
                    {{$t("Participant_label")}}:
                    <strong>{{Participant}}</strong>
                  </button>
                </div>
              </div>

              <!-- <div class="col-md-12 working-plan" >
                <div class="box box-solid">
                  <div class="box-header with-border">
                      <i class="fa fa-tag"></i>
                      <h5 class="box-title font-weight-bold">{{$t("WorkingPlan_label")}}:</h5>
                  </div>
                  <div class="box-body">
                    <blockquote>
                      <p>Not available!</p>
                    </blockquote>
                  </div>
                </div>
              </div> -->

              <!-- Chartjs -->
              <div class="chart-container">
                <canvas id="planet-chart"></canvas>
              </div>
              <!-- <div class="chart">
                <canvas id="planet-chart" style="height:500px"></canvas>
              </div> -->
            </div>
            <!-- <div class="box-footer">
              <p class="text-bold" style="font-size:16px;color:green;display:none" id="fromDateEndDate">
                <span class="fromDayOfWeek"></span>
                <span class="endDayOfWeek"></span>
              </p>
              <p class="text-bold" style="font-size:16px;color:green;display:none" id="fromDateEndDateM">
                <span class="fromDayOfMonth"></span>
                <span class="endDayOfMonth"></span>
              </p>
              <p class="text-bold" style="font-size:16px;color:green;display:none" id="fromDateEndDateQ">
                <span class="fromDayOfQuarter"></span>
                <span class="endDayOfQuarter"></span>
              </p>
            </div> -->
          </div>
        </div>
        <div class="col-md-12">
          <div class="box box-widget">
            <!-- <div class="box-header with-border">
              <h5 v-if="period == 'W'" class="box-title" style="font-weight:bold">Data - {{kpiname}} - {{$t("Weekly")}}</h5>
              <h5 v-if="period == 'M'" class="box-title" id="box-title" style="font-weight:bold">Data - {{kpiname}} - {{$t("Monthly")}}</h5>
              <h5 v-if="period == 'Q'" class="box-title" style="font-weight:bold">Data - {{kpiname}} - {{$t("Quarterly")}}</h5>
              <h5 v-if="period == 'H'" class="box-title" style="font-weight:bold">Data - {{kpiname}} - Half Year</h5>
              <h5 v-if="period == 'Y'" class="box-title" style="font-weight:bold">Data - {{kpiname}} - {{$t("Yearly")}}</h5>
              <div class="box-tools float-right">
                <button @click="$router.push(`/Chartperiod/ListTasks/${kpilevelcode}`)" type="button" class="btn btn-info btn-sm listTaskBtn"><i class="fa fa-info"></i> {{$t("listTaskBtn")}}</button>
              </div>
            </div> -->
            <!-- /.box-header -->
            <div class="box-body">
              <div class="table-responsive">
                <table class="table table-bordered" style="margin-bottom:20px;" id="tblDataChart">
                  <tbody>
                    <tr>
                      <th v-if="period == 'W'" class="text-center" width="5%">Week</th>
                      <th v-if="period == 'M'" class="text-center" width="5%">Month</th>
                      <th v-if="period == 'Q'" class="text-center" width="5%">Quater</th>
                      <th v-if="period == 'H'" class="text-center" width="5%">Half Year</th>
                      <th v-if="period == 'Y'" class="text-center" width="5%">Year</th>
                      <th v-for="(item,key,index) in labels " :key="index" class="text-center">{{item}}</th>
                    </tr>
                    <tr>
                      <th class="text-center" width="5%">Target</th>
                      <td v-for="(item,key,index) in dataremarks " :key="index" :class="checktarget(item.Value,item.Target)" :data-id="item.ID" @click="opencomment(item,$event)">{{item.Value}}</td>
                    </tr>
                  </tbody>
                </table>
              </div>
            </div>
          </div>
        </div>
      </div>
      <!-- <comment> -->
      <!-- compare -->
      

      <!-- </comment>  -->
    </section>

    <div class="modal fade" id="modal-group">
        <div class="modal-dialog modal-lg " >
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Compare KPILevel </h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div>
                        <div class="box-body" id="comparechart">
                            <table class="table table-hover table-striped">
                                <thead>
                                    <tr>
                                        <th class="text-center" style="width:3%">#</th>
                                        <th class="text-center" style="width:5%">Checkbox</th>
                                        <th class="text-center">Level Number</th>
                                        <th class="text-center">Area</th>
                                        <th class="text-center">Data Status </th>
                                        <th class="text-center">Publicity Status</th>
                                    </tr>
                                </thead>
                                <tbody v-for="(item,key,index) in data2" :key="index" id="tblcomparechart">
                                  <tr >
                                      <td class="text-center">{{key+1}}</td>
                                      <td class="text-center">
                                          <div class="pretty p-image p-plain">
                                              <input type="checkbox" :value="item.KPILevelCode" @click="clickcompare" v-if="item.StatusPublic == true"  class="compare" />
                                              <input type="checkbox"  :value="item.KPILevelCode" @click="clickcompare" v-else class="compare" disabled />
                                              <div class="state">
                                                  <img class="image" src="src/img/004.png">
                                                  <label class="comparelabel"></label>
                                              </div>
                                          </div>
                                      </td>
                                      <td class="text-center">{{item.LevelNumber}}</td>
                                      <td class="text-center">{{item.Area}}</td>
                                      <td class="text-center">
                                        <span v-if="item.Status == true" class="badge badge-success">visible</span>
                                        <span v-else class="badge badge-danger">not visible</span>
                                      </td>
                                      <td class="text-center">
                                        <span v-if="item.StatusPublic == true" class="badge badge-success">Public</span>
                                        <span v-else class="badge badge-danger">Private</span>
                                      </td>
                                  </tr>
                                    
                                </tbody>
                            </table>
                            
                        </div>
                        <!-- /.box-body -->

                        <div class="box-footer">
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
                        <div class="box-footer">
                            <span style="display:none" class="arrcompare"></span>
                            <button id="btnCompare-kpilevel"   data-compare="" class="btn btn-primary">Compare</button>
                            <button type="button" class="btn btn-default pull-right" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>

            </div>
            <!-- /.modal-content -->
        </div>
    </div>


    <!-- comment -->
    <div class="modal fade modal" id="modal-group-comment-data">
      <div class="modal-dialog modal-lg" >
        <div class="modal-content">
          <div class="modal-header" style="background-color:#00a65a;color:#fff">
            <h4 class="modal-title">
              <i class="fa fa-tags"></i>&#32;
              <span class="RemarkChartasd">{{remarkText}}</span>
                
            </h4>
            <button type="button" class="close" data-dismiss="modal">
              <span aria-hidden="true">&times;</span>
              <span class="sr-only">Close</span>
            </button>
          </div>
          <div class="modal-body">
            <div class="row bootstrap snippets">
              <div class="col-md-12 col-sm-12">
                <div class="comment-wrapper">
                  <div class="panel panel-info">
                    <div class="panel-body">
                        <span class="dataid" style="display:none"></span>

                        <div class="form-group">
                            <textarea class="form-control" id="comment" placeholder="write a comment..." rows="6"></textarea>
                        </div>

                        <div class="clearfix"></div>
                        <hr />
                        <button type="button" @click="addcomment" class="btn btn-info pull-right btnComment">
                            <i class="far fa-paper-plane"></i> Post
                        </button>
                        <hr />
                        <ul class="media-list" id="media-list">
                          <li v-for="(item,key,index) in dataComment" :key="index" class="media">
                            <a class="float-left">
                              <img src="../../../static/img/user-icon.png" alt="" class="img-circle">
                            </a>
                            <div class="media-body">
                              <span class="text-muted float-right "  style="padding-left: 13px; cursor: pointer" >
                                <p @click="deleteComment(item.CommentID)" class="text-danger"> <i class="fas fa-times"></i> Delete</p>
                              </span>
                              <strong class="text-success">{{item.FullName}}</strong>
                              <span class="text-muted float-right">
                                <small class="text-muted">{{JSONDateWithTime(item.CommentedDate)}}</small>
                              </span>
                              <span v-if="item.Read" class="badge bg-green"> new</span>
                              <p v-if="item.Task"><a @click="btntabload(item)"  data-toggle="modal"  data-dismiss="modal" class="btn btn-sm btn-danger text-bold btnTask"><i class="fa fa-tags"></i> Action Plan</a> </p>
                              <p v-else><a @click="btntabload(item)"  data-toggle="modal" title="There are no task."  data-dismiss="modal" class="btn btn-sm btn-success text-bold btnTask tooltip-ui"><i class="fa fa-tags"></i> Action Plan</a> </p>
                              <p>{{item.CommentMsg}}</p>
                            </div>
                          </li>
                        </ul>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-dismiss="modal" >Close</button>
          </div>
        </div>
      </div>
    </div>

    <b-modal ref="modalActionPlan" size="lg" hide-header-close hide-header hide-footer  no-close-on-esc no-close-on-backdrop>
        <div class="modal-header" style="background-color:#00a65a;color:#fff" >
          <h4 class="modal-title">
            <a  @click="backComment" ref="comment" data-toggle="modal" data-dismiss="modal" style="color:#fff;cursor:pointer" >
              <i class="fa fa-reply"></i>
            </a>&#32; 
            <span class="RemarkChart"> {{actionPlanText}}</span>
          </h4>
          <button type="button" class="close" @click="closemodalAc" aria-label="Close">
              <span aria-hidden="true">×</span>
          </button>
        </div>
      <template v-slot:modal-title>
      </template>
      <div class="modal-body">
        <ejs-tab id='element'>
         <div class="e-tab-header">
            <div>List </div>
            <div v-if="usid == currentUser">Add </div>
          </div>
          <div class="e-content">

            <div>
              <ejs-grid  ref="grid" :cellEdit ='cellEdit' :gridLines='lines' :allowTextWrap='true' :allowResizing='true'  :editSettings='editSettings' :actionBegin='actionBegin' :searchSettings="searchSettings" :rowSelected="rowSelected"  :toolbar='toolbarOptions' :dataSource='dataActionPlan' :allowPaging="true" :pageSettings='pageSettings' >
                <e-columns>
                  <e-column field='Title' :template="TitleTemplate" :headerText="$t('actionPlanName')" :edit="editparams"  width='200' textAlign='Center' ></e-column>
                  <e-column field='Description' :template="DescriptionTemplate" :headerText="$t('Description')" width='150' textAlign='Center' ></e-column>
                  <e-column field='Tag' :template="TagTemplate" :headerText="$t('PIC')" width='120' isPrimaryKey='true' textAlign='Center' ></e-column>

                  <e-column field='Deadline' :editTemplate="DeadlineTemplate" editType='datepickeredit' :headerText="$t('Duedate')" width='250' textAlign='Center' ></e-column>

                  <e-column field='UpdateSheduleDate' :editTemplate="UpdateSheduleDateTemplate" editType='datepickeredit' :headerText="$t('Update_chedule_date')" width='250' textAlign='Center' ></e-column>

                  <e-column field='ActualFinishDate' isPrimaryKey='true' :headerText="$t('Actual_finish_date')" width='250' textAlign='Center' ></e-column>

                  <e-column field='Remark' :headerText="$t('remark')" width='250' textAlign='Center' ></e-column>
                  <e-column field='CreatedByName' isPrimaryKey='true' :headerText="$t('createdBy')" width='250' textAlign='Center' ></e-column>
                  <e-column field='' isPrimaryKey='true' :template="StatusTemplate" :headerText="$t('Status')" width='200' textAlign='Center' ></e-column>
                  <e-column field='' isPrimaryKey='true' :template="ApproveTemplate" :headerText="$t('Approve')" width='200' textAlign='Center' ></e-column>
                  <e-column :headerText="$t('ManageRecords')" isPrimaryKey='true' :template="RecordTemplate" width='150' textAlign='Center'  ></e-column>

                </e-columns>
              </ejs-grid>
            </div>

            <div>
              <div class="addTask">
                <div class="form-group">
                  <label>Action Plan name<b class="text-danger"> (*) </b></label>
                  <input type="text" v-model="taskname" class="form-control Title"  placeholder="">
                </div>

                <div class="form-group">
                  <label>Description<b class="text-danger"> (*) </b></label>
                  <textarea type="text" v-model="description" class="form-control Description" rows="3" placeholder=""></textarea>
                </div>

                <div class="form-group" tooltip-position="top" data-c-tooltip='Enter a character "@@" inside the textarea and you will see the dropdown list populated with suggestions.'>
                  <label class="control-label" for="Tag">Assign PIC<b class="text-danger"> (*) </b></label>
                  <multiselect
                    v-model="selectedAssignPIC"
                    
                    track-by="Username"
                    label="Alias"
                    placeholder="Select one"
                    :options="optionsAc"
                    :searchable="true"
                    :multiple="true"
                    
                  ></multiselect>
                </div>
                <div class="form-group" tooltip-position="top" data-c-tooltip='Enter a character "@@" inside the textarea and you will see the dropdown list populated with suggestions.'>
                  <label class="control-label" for="Auditor">Auditor<b class="text-danger"> (*) </b></label>
                  <multiselect
                    v-model="selectedAuditor"
                    
                    track-by="Username"
                    label="Alias"
                    placeholder="Select one"
                    :options="optionsAc"
                    :searchable="true"
                    :multiple="true"
                    
                  ></multiselect>
                </div>
                <div  class="form-group">
                  <label>Due date<b class="text-danger"> (*) </b></label><br>
                  <ejs-datepicker @change='onChange($event,dateValue)' width="500"  format='MM-dd-yyyy' :value="dateValue" placeholder='Enter date' floatLabelType='Never' v-model="dateValue"></ejs-datepicker>
                </div>
                <button type="button" @click="addActionPlan" class="btn btn-success ">Save</button>
                <div class="form-group">
                  <b class="text-danger">(*) Require fields not to be blank. </b>
                </div>
              </div>
            </div>
          </div>
        </ejs-tab>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-light" @click="closemodalAc">Close</button>
        <button type="submit" @click="backComment" class="btn btn-primary">Back to remark</button>
      </div>
    </b-modal>
    
  </div>
</template>
<script>
import axios from 'axios';
import { HTTP } from "../../http-constants";
import LineChart from "../../utils/ChartJs/LineChart";
import { initLineChart } from "../../plugins/LineChartPlugin";
import planetChartData from "../../plugins/Chartjs2/Demo";
import Comment from "../ChartPeriod/Modal";
import VueJwtDecode from 'vue-jwt-decode'
import Paginate from "vuejs-paginate";
import EventBus from "../../utils/EventBus.js";
import { TabComponent, TabPlugin } from '@syncfusion/ej2-vue-navigations';
import Multiselect from "vue-multiselect";
import Vue from "vue";
Vue.component(TabPlugin.name, TabComponent);

import { GridPlugin, Page ,Toolbar,Filter,Edit,Resize ,GridLine } from "@syncfusion/ej2-vue-grids";

Vue.use(GridPlugin);

export default {
  data() {
    return {
      usid: 0,
      window: {
        width: 0,
        height: 0
      },
      lines: 'Both',
      selectedAuditor: [],
      selectedAssignPIC: [],
      optionsAc: [],
      dateValue: new Date(),
      AssignPIC: '',
      Auditor: '',
      taskname: '',
      description: '',
      DeadlineTemplate: function() {
        return {
          template: Vue.component("DeadlineTemplate", {
            template: `
              <ejs-datepicker :enabled="false" @change='DeadlineChange($event)' format='MM-dd-yyyy' placeholder='Enter date' floatLabelType='Never' v-model="data.Deadline"></ejs-datepicker>
            `,
            data: function() {
              return {
                data: {},
                currentUser: Number(VueJwtDecode.decode(localStorage.getItem("authToken")).nameid),
              };
            },
            methods: {
              DeadlineChange(args){
                EventBus.$emit('DeadlineChange',args)
              }
            }
          })
        };
      },
      UpdateSheduleDateTemplate: function() {
        return {
          template: Vue.component("UpdateSheduleDateTemplate", {
            template: `
              <ejs-datepicker :enabled="data.CreatedBy == currentUser || data.Auditor == currentUser ? true :false" @change='DeadlineUpdateChange($event)' format='MM-dd-yyyy' placeholder='Enter date' floatLabelType='Never' v-model="data.UpdateSheduleDate"></ejs-datepicker>
            `,
            data: function() {
              return {
                data: {},
                currentUser: Number(VueJwtDecode.decode(localStorage.getItem("authToken")).nameid),
              };
            },
            methods: {
              DeadlineUpdateChange(args){
                EventBus.$emit('DeadlineUpdateChange',args)
              }
            }
          })
        };
      },
      RecordTemplate: function() {
        return {
          template: Vue.component("RecordTemplate", {
            template: `
              <ejs-button v-on:click.native="deleteActionPlan(data)"  iconCss='e-icons e-delete' cssClass= 'e-flat'></ejs-button>
            `,
            data: function() {
              return {
                data: {},
              };
            },
            methods: {
              deleteActionPlan(data){
                EventBus.$emit('deleteActionPlan',data)
              }
            }
          })
        };
      },
      StatusTemplate: function() {
        return {
          template: Vue.component("StatusTemplate", {
            template: `
              <ejs-switch ref="wswitch" @change="done(data)" onLabel="YES" offLabel="NO" :disabled='data.ListUserIDs.indexOf(currentUser) != -1 && data.ApprovedStatus == false ?"":"true"' :checked="data.Status"></ejs-switch>
            `,
            data: function() {
              return {
                data: {},
                currentUser: Number(VueJwtDecode.decode(localStorage.getItem("authToken")).nameid),
              };
            },
            methods: {
              done(data){
                EventBus.$emit('done',data)
              }
            }
          })
        };
      },
      ApproveTemplate: function() {
        return {
          template: Vue.component("ApproveTemplate", {
            template: `
              <ejs-switch ref="wswitch" @change="approval(data)"  onLabel="YES" offLabel="NO" :disabled='data.CreatedBy == currentUser || data.Auditor == currentUser ?"":"true"' :checked="data.ApprovedStatus"></ejs-switch>
            `,
            data: function() {
              return {
                data: {},
                currentUser: Number(VueJwtDecode.decode(localStorage.getItem("authToken")).nameid),
              };
            },
            methods: {
              approval(data){
                EventBus.$emit('approval',data)
              }
            }
          })
        };
      },
      TagTemplate: function() {
        return {
          template: Vue.component("TagTemplate", {
            template: `
              <span class="badge badge-danger">{{data.Tag}}</span>
            `,
            data: function() {
              return {
                data: {},
                currentUser: Number(VueJwtDecode.decode(localStorage.getItem("authToken")).nameid),
              };
            },
            methods: {
             
            }
          })
        };
      },
      TitleTemplate: function() {
        return {
          template: Vue.component("TitleTemplate", {
            template: `
              <a :style='data.CreatedBy == currentUser || data.Auditor == currentUser  ? "" :"pointer-events: none;opacity: 0.5;cursor: not-allowed"'>{{data.Title}}</a>
            `,
            data: function() {
              return {
                data: {},
                currentUser: Number(VueJwtDecode.decode(localStorage.getItem("authToken")).nameid),
              };
            },
            methods: {
             
            }
          })
        };
      },
      DescriptionTemplate: function() {
        return {
          template: Vue.component("DescriptionTemplate", {
            template: `
              <a :style='data.CreatedBy == currentUser || data.Auditor == currentUser  ? "" :"pointer-events: none;opacity: 0.5;cursor: not-allowed"'>{{data.Description}}</a>
            `,
            data: function() {
              return {
                data: {},
                currentUser: Number(VueJwtDecode.decode(localStorage.getItem("authToken")).nameid),
              };
            },
            methods: {
             
            }
          })
        };
      },
      toolbarOptions : ['Search', 'Cancel'],
      pageSettings: { pageSize: 10 },
      data: [],
      searchSettings: { hierarchyMode: 'Parent' },
      editparams: { params: { format: 'n' } },
      editSettings:{ allowDeleting: false, allowEditing: true, mode: 'Normal',allowEditOnDblClick: true },
      activetab: 1,
      dataActionPlan: [],
      dataComment: [],
      actionPlanText: 'actionplan',
      remarkText: 'asdasdasd',
      defaultLink: null,
      title: null,
      name:"",
      comID: 0,
      seachActionPlan: "",
      date: '',
      data2:[],
      totalPage: 0,
      page: 1,
      skip: 0,
      pageSize: 10,
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
      standards: [],
      dataremarks: [],
      statusfavorite: true,
      dataCharts: {},
      options: {
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
                labelString: this.unit,
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
                labelString: this.period,
                fontSize: 12,
                fontStyle: "normal"
              }
            }
          ]
        }
      },
      searchyear: 0,
      currentUser: Number(VueJwtDecode.decode(localStorage.getItem("authToken")).nameid),
      keyword: ' ',
      URL: '',
      Link:'',
      kpilevelcode: '',
      kpilevelID: 0,
      locale: $cookies.get("Lang"),
      Listentask: '',
      ListenCom: '',
      dataID: '',
      commentID: '',
      taskID: '',
      tempTitleDefault: '',
      tempTitleChange: '',
      tempDescriptionDefault: '',
      tempDescriptionChange: '',
      tempRemarkDefault: '',
      tempRemarkChange: ''
    };
  },
  provide: {
    grid: [Page,Toolbar,Filter,Edit,Resize],
  },
  components: {
    LineChart,
    Comment,
    Paginate,
    Multiselect
  },
  created() {
    window.addEventListener('resize', this.handleResize);
    this.handleResize();
    let seft = this;
    EventBus.$on('flag', locale =>{
      seft.locale = locale
    });
    let boxTitle = $("#chartperiod .box-title1").text();
    seft.period = seft.$route.params.period;
    seft.vstart = seft.$route.params.start;
    seft.vend = seft.$route.params.end;
    seft.searchyear = seft.$route.params.year;
    seft.kpilevelcode = seft.$route.params.kpilevelcode
    seft.Loadchart();
    seft.getAllNotifications();
    seft.GetItem();
    seft.defaultLink = `/ChartPeriod/${seft.$route.params.kpilevelcode}/${seft.$route.params.catid}/${seft.$route.params.period}/${seft.$route.params.year}/${seft.$route.params.start}/${seft.$route.params.end}`
    EventBus.$on('DeadlineChange',seft.DeadlineChange)
    EventBus.$on('DeadlineUpdateChange',seft.DeadlineUpdateChange)
    EventBus.$on('approval',seft.approval)
    EventBus.$on('done',seft.done)
    EventBus.$on('deleteActionPlan',seft.deleteActionPlan)
  },
  mounted() {
    let seft = this
    EventBus.$on('item',data =>{
      seft.Link = data.Link
      seft.$router.push(data.Link)
      seft.ListenGetComment(data);
    })
    EventBus.$on('hello',data2 =>{
      seft.URL = data2.URL
      seft.$router.push(data2.URL)
      seft.ListenGettask(data2);
    })
    seft.Loadchart();
    $('#modal-group-comment-data').off('hidden.bs.modal').on('hidden.bs.modal', function () {
      seft.$router.push(`/ChartPeriod/${seft.$route.params.kpilevelcode}/${seft.$route.params.catid}/${seft.$route.params.period}/${seft.$route.params.year}/${seft.$route.params.start}/${seft.$route.params.end}`)
      seft.Loadchart();
    });
   
    seft.createChart("planet-chart", seft.datasets, seft.targets, seft.labels);
    
  },
  destroyed() {
    window.removeEventListener('resize', this.handleResize);
    EventBus.$off('DeadlineChange',this.DeadlineChange);
    EventBus.$off('DeadlineUpdateChange',this.DeadlineUpdateChange);
    EventBus.$off('approval',this.approval);
    EventBus.$off('done',this.done);
  },
  beforeDestroy: function () {
    EventBus.$off('item')
    EventBus.$off('hello')
  },
  watch: {
    selectedAssignPIC: function(newVal, oldVal) {
      var self = this;
      let assignPICs = newVal;
      self.AssignPIC = assignPICs.map((AssignPIC, index, assignPICs) => {
        return AssignPIC.Alias;
      }).join(',')
    },
    selectedAuditor: function(newVal, oldVal) {
      var self = this;
      let auditors = newVal;
      self.Auditor = auditors.map((Auditor, index, auditors) => {
        return Auditor.Alias;
      }).join(',')
    },
    data: function(newOld, oldVal) {
      let self = this;
      let comID = Number(self.$route.params.comID);
      self.commentID = Number(self.$route.params.comID)
      let dataID = Number(self.$route.params.dataID);
      self.dataID = Number(self.$route.params.dataID);
      let title = self.$route.params.title;
      let type = self.$route.params.type;
      let period = self.$route.params.period;
      if (comID > 0 && dataID > 0 && title != "" & type === "remark") {
        self.remarkText = title.replace(/-/g, ' ')  + ' - '
        + ' KPI Chart '
        + ' - ' + self.kpiname
        + ' - ' + self.convertPeriod(period, false)
        self.actionPlanText = self.remarkText.replace('Action Plan', 'Remark')
        $("#modal-group-comment-data").modal("show");
        self.remark(dataID);
        self.loadDataComment(dataID);
        self.LoadDataActionPlan(dataID, comID);
      }
      if (comID > 0 && dataID > 0 && title != "" && type === "task") {
        self.actionPlanText = title.replace(/-/g, ' ')  + ' - '
        + ' KPI Chart '
        + ' - ' + self.kpiname
        + ' - ' + self.convertPeriod(period, false)
        self.remarkText = self.actionPlanText.replace('Action Plan', 'Remark')
        self.$refs['modalActionPlan'].show()
        self.remark(dataID);
        self.loadDataComment(dataID);
        self.LoadDataActionPlan(dataID, comID);
      }
      
    },
    seachActionPlan: function(newOld,oldVal){
      let seft = this
      
      let dataid = $('.dataid').text();
      let commentid = $('.commentid').text()
      
      seft.keyword = newOld;
      seft.LoadDataActionPlan(dataid, commentid);
    },
    locale: function(newOld,oldVal){
      this.locale = newOld
      this.Loadchart()
      this.getAllNotifications()
      this.GetItem()
    },
    kpiname: function(newOld,oldVal){
      var item = this.actionPlanText.replace("-  -",' - '+newOld + ' - ');
      this.actionPlanText = item;
      var item2 = this.remarkText.replace("-  -",' - '+newOld + ' - ');
      this.remarkText = item2
    },
      
  },
  methods: {
    handleResize() {
      this.Loadchart();
    },
    actionComplete(args){
    },
    beforeBatchSave(args){
    },
    beforeBatchAdd(args){
    },
    cellEdit(args) {
      if (args.value == "France") {
        args.cancel = true;
      }
    },
    getAllUser() {
      axios.get('AdminKPILevel/GetListAllUser').then(res=>{
        this.optionsAc = res.data
      })
    },
    closemodalAc(){
      let seft = this
      seft.$refs['modalActionPlan'].hide();
      seft.Loadchart()
      seft.$router.push(`/ChartPeriod/${seft.$route.params.kpilevelcode}/${seft.$route.params.catid}/${seft.$route.params.period}/${seft.$route.params.year}/${seft.$route.params.start}/${seft.$route.params.end}`)
    },
    onChange(args){
      this.dateValue = moment(String(args.value)).format('MM-DD-YYYY')
    },
    addActionPlan(){
      let seft = this
      let datenew = moment(String(new Date())).format('MM-DD-YYYY')
      
      seft.dateValue = moment(String(seft.dateValue)).format('MM-DD-YYYY')
      var KPILevelCodeAndPeriod = seft.$route.params.kpilevelcode + seft.$route.params.period;
      var obj = {
        UserID: seft.currentUser,
        DataID: seft.dataID,
        CommentID: seft.commentID,
        Title: seft.taskname,
        Tag: seft.AssignPIC,
        KPILevelCodeAndPeriod: KPILevelCodeAndPeriod,
        Description: seft.description,
        Deadline: seft.dateValue,
        SubmitDate: datenew,
        Link: window.location.href,
        Subject: seft.actionPlanText,
        Auditor: seft.Auditor,
        CategoryID: Number(seft.$route.params.catid),
        KPILevelCode: seft.$route.params.kpilevelcode,
        DefaultLink: seft.defaultLink,
        KPILevelID: seft.kpilevelID,
      };
      axios.post('ChartPeriod/Add' , obj)
      .then(res=>{
        if(res.data.status === true){
          seft.LoadDataActionPlan(seft.dataID,seft.commentID)
          success("Add ActionPlan successfully!")
          seft.$refs['modalActionPlan'].hide()
          seft.$refs['modalActionPlan'].show()
        }
        else {
          if (res.message !== '') {
            Toast.fire({
              icon: 'error',
              title: 'You are not Owner this KPI !'
          })
        } else {
            Toast.fire({
              icon: 'error',
              title: 'You are not Owner this KPI !'
            })
          }
        }
      })
    },
    DeadlineUpdateChange(args){
      let deadlineUpdate = moment(String(args.value)).format('MM-DD-YYYY')
      const formData = new FormData()
      formData.append('name', 'UpdateSheduleDate')
      formData.append('value', deadlineUpdate)
      formData.append('pk', this.taskID)
      formData.append('userid', this.currentUser)
      axios.post('ChartPeriod/UpdateSheduleDate',formData).then(res=>{
        success("Update Deadline successfully");
        this.LoadDataActionPlan(this.dataID , this.commentID)
      })
    },
    DeadlineChange(args){
      let deadline = moment(String(args.value)).format('MM-DD-YYYY')
      const formData = new FormData()
      formData.append('name', 'DeadLine')
      formData.append('value', deadline)
      formData.append('pk', this.taskID)
      formData.append('userid', this.currentUser)
      axios.post('ChartPeriod/UpdateSheduleDate',formData).then(res=>{
        success("Update Deadline successfully");
        this.LoadDataActionPlan(this.dataID , this.commentID)
      })
    },
    actionBegin(args){
      if (args.requestType === 'beginEdit') {
        this.tempTitleDefault = args.rowData.Title
        this.tempDescriptionDefault = args.rowData.Description
        this.tempRemarkDefault = args.rowData.Remark
      }
      if (args.requestType === 'save') {
        this.tempTitleChange = args.data.Title;
        this.tempDescriptionChange = args.data.Description
        this.tempRemarkChange = args.data.Remark
        let TitleChange = this.tempTitleDefault.length < this.tempTitleChange.length
        || this.tempTitleDefault.length > this.tempTitleChange.length;
        let DescriptionChange = this.tempDescriptionDefault.length < this.tempDescriptionChange.length
        || this.tempDescriptionDefault.length > this.tempDescriptionChange.length;
        let RemarkChange = this.tempRemarkDefault.length < this.tempRemarkChange.length
        || this.tempRemarkDefault.length > this.tempRemarkChange.length;

        if (TitleChange) {
          const formData = new FormData()
          formData.append('name', 'Title')
          formData.append('value', this.tempTitleChange)
          formData.append('pk', this.taskID)
          formData.append('userid', this.currentUser)
          if(args.rowData.CreatedBy == this.currentUser){
            axios.post('ChartPeriod/UpdateSheduleDate',formData).then(res=>{
              success("Update Title successfully");
              this.LoadDataActionPlan(this.dataID , this.commentID)
            })
          }
          else {
            error("You can only change Remark");
            this.LoadDataActionPlan(this.dataID , this.commentID)
          }
        }
        if (DescriptionChange) {
          const formData = new FormData()
          formData.append('name', 'Description')
          formData.append('value', this.tempDescriptionChange)
          formData.append('pk', this.taskID)
          formData.append('userid', this.currentUser)
          if(args.rowData.CreatedBy == this.currentUser){
            axios.post('ChartPeriod/UpdateSheduleDate',formData).then(res=>{
              success("Update Description successfully");
              this.LoadDataActionPlan(this.dataID , this.commentID)
              this.LoadDataActionPlan(this.dataID , this.commentID)
            })
          }
          else {
            error("You can only change Remark");
          }
        }
        if (RemarkChange) {
          const formData = new FormData()
          formData.append('name', 'Remark')
          formData.append('value', this.tempRemarkChange)
          formData.append('pk', this.taskID)
          formData.append('userid', this.currentUser)
          axios.post('ChartPeriod/UpdateSheduleDate',formData).then(res=>{
            success("Update Remark successfully");
            this.LoadDataActionPlan(this.dataID , this.commentID)
          })
        }
      }
      if(args.requestType === 'refresh'){
        this.taskID = null
      }
      else{
        this.taskID = args.rowData.ID
      }
    },
    rowSelected(args){
    },
    modalclose2(){
    },
    backComment(){
      this.$refs['modalActionPlan'].hide()
      $("#modal-group-comment-data").modal("show");
    },
    hideModal(){
    },
    save(){
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
    closemodalAction(){
      this.$router.push(`/ChartPeriod/${this.$route.params.kpilevelcode}/${this.$route.params.catid}/${this.$route.params.period}/${this.$route.params.year}/${this.$route.params.start}/${this.$route.params.end}`)
      this.Loadchart()
      $("#modal-group-comment-data2").modal("hide");
    },
    closemodalComment(){
      this.$router.push(`/ChartPeriod/${this.$route.params.kpilevelcode}/${this.$route.params.catid}/${this.$route.params.period}/${this.$route.params.year}/${this.$route.params.start}/${this.$route.params.end}`)
      this.Loadchart()
      $("#modal-group-comment-data").modal("hide");
    },
    showModal() {
      this.$refs['modalComment'].show()
    },
    closechart(){
      let self = this;
      let currentUrl = self.$router.currentRoute;
     
      self.$router.replace({
        name: "chart" ,
      });
      $("#modal-group-comment-data2").modal("hide");
      
    },
    GetItem(){
      let self = this 
      axios.get(`ChartPeriod/GetItemInListOfWorkingPlan/${self.$route.params.kpilevelcode}/${self.$route.params.period}`)
      .then(r=>{
       
        if (r.data.status) {
          $(".working-plan blockquote p").text(r.data.data.Content || "Not avaiable!");
        }
      })
    },
    checktarget(value,target){
      return parseFloat(value) <= (target || 0) ? ' text-center active-td' : 'active-td2 text-center'
    },
    dateNow() {
      var date = new Date();
      var day = date.getDate();       // yields date
      var month = date.getMonth() + 1;    // yields month (add one as '.getMonth()' is zero indexed)
      var year = date.getFullYear();  // yields year
      var hour = date.getHours();     // yields hours
      var minute = date.getMinutes(); // yields minutes
      var second = date.getSeconds(); // yields seconds

      // After this construct a string with the above results as below
      var time = day + "/" + month + "/" + year + " " + hour + ':' + minute + ':' + second;
      return time;
    },
    isFutureDate(date) {
      const currentDate = new Date();
      return date > currentDate;
    },
    getAllNotifications(){
      let seft = this ;
      axios.get("Home/GetNotifications")
      .then(r=>{
        seft.arrayID = r.data.arrayID
        seft.data = r.data.data
        seft.listdata = r.data
        seft.userid = VueJwtDecode.decode(localStorage.getItem("authToken")).nameid
      })
    },
    addNotification() {
      var phrases = new Array();
        $('.Description').each(function () {
          $(this).find('li').each(function () {
            var current = $(this);
            if (current.children().length > 0) { return true; }
            phrases.push($(this).text().trim());
          });
        });
        var Description = phrases.join(';');

        var Tag = $('#Tag').val().trim();
        if (Tag !== null || Tag !== "" || Tag !== undefined)
          Tag = Tag.replace(/\@/g, '').replace(/\ /g, ',');
        var notification = {
          ID: 0,
          UserID: VueJwtDecode.decode(localStorage.getItem("authToken")).nameid,
          KPIName: this.kpiname,
          Period: this.$route.params.period,
          Seen: false,
          Link: window.location.href,
          Tag: Tag,
          Content: Description,
          Title: this.actionPlanText,
          CreateTime: new Date()
          };

      axios.post('ChartPeriod/AddNotification', {notification:notification})
        .then(function (response) {
          
      });
    },
    addNotificationComment() {
      var CommentMsg = $('#comment').val();

        var Tag = [],obj;
        if (CommentMsg !== "") {
          obj = CommentMsg.match(/@.+\f/g);
          if (obj === undefined || obj === null) {
            obj = "";
          }
          if (obj.toString().indexOf("@") === -1) {
            Tag = CommentMsg;
          } else {
            var arr = obj.toString().split(' ');
              for (let item of arr) {
                Tag.push(item.replace('@', ' ').trim());
              }
            Tag = Tag.join();
          }

        }
        var notification = {
          ID: 0,
          UserID: VueJwtDecode.decode(localStorage.getItem("authToken")).nameid,
          KPIName: this.kpiname,
          Period: this.$route.params.period,
          Seen: false,
          Link: window.location.href,
          Tag: Tag,
          Title: this.remarkText,
          Content: CommentMsg,
          CreateTime: this.dateNow()
          };

        axios.post('ChartPeriod/AddNotification', {notification:notification})
          .then(function (response) {
              
        });
    },
    btnCompare(){
      let seft = this
      $('#btnCompare-kpilevel').off('click').on('click', function () {
          // debugger
            var value = $('.arrcompare').text().toString();
            var obj = value.substring(value.length - 1, 0);
            var kpilevelcode = seft.$route.params.kpilevelcode;
            var period = seft.$route.params.period;
            obj = obj + '-' + kpilevelcode + ',' + period;
            seft.$router.push(`/compare/${obj}`) ;
        })
    },
    clickcompare(){
      var old = "";
      $('#comparechart .compare').unbind('click').change(function (e) {
          // debugger
          var code = $(this).val();
          if (old.indexOf($(this).val()) == -1) {
              old += code + '-';
              $('.arrcompare').text(old);
          }
      });
    },
    addFavourite() {
      let seft = this
      // debugger
      var UserID = Number(VueJwtDecode.decode(localStorage.getItem("authToken")).nameid) 
      if (UserID === 0 || UserID === "" || UserID === undefined) {
          Swal.fire({
              title: 'Warning!',
              text: 'Error!',
              type: 'warning',
              confirmButtonText: 'OK'
          });
          return;
      }
      var mObj = {
          KPILevelCode: seft.$route.params.kpilevelcode,
          Period: seft.$route.params.period,
          UserID: UserID,
          Link: seft.$route.path
      };
      axios.post("ChartPeriod/AddFavourite",mObj).then(result=>{
        Swal.fire({
            title: 'success!',
            text: 'Add success!',
            type: 'success',
            confirmButtonText: 'OK'
        });
        seft.Loadchart();
      })
      
    },
    btnLike(){
      this.addFavourite();
    },
    compare(){
       this.loadDataProvide();
    },
    loadDataProvide() {
      // debugger
      let seft = this 
      var obj = seft.$route.params.kpilevelcode+ ',' + seft.$route.params.period ;
      axios.get(`ChartPeriod/LoadDataProvide/${obj}/1/${seft.pageSize}`)
        .then(data => {
        var count = 1;
        seft.skip = data.data.skip;
        seft.totalPage = data.data.total;
        seft.page = data.data.page;
        seft.data2 = data.data.listCompare;      
        seft.clickcompare();
        seft.btnCompare();
      })
     
    },
    changePage(pageNum) {
      this.loadDataProvide(this.obj,pageNum);
    },
    deleteActionPlan(data) {
      let seft = this
      if (Number(data.ID) > 0 && data.CreatedBy == seft.currentUser ) {
        axios.get(`ChartPeriod/Delete/${data.ID}`).then(res =>{
          if (res) {
            seft.LoadDataActionPlan(seft.dataID , seft.commentID)
            success("Delete ActionPlan Successfully!");
          }
          else {
            success("You can not Delete this ActionPlan !");
          }
        })
      }
    },
    approval(data) {
      let seft = this
      var data2 = {
        id: data.ID,
        approveby: VueJwtDecode.decode(localStorage.getItem("authToken")).nameid,
        KPILevelCode: seft.$route.params.kpilevelcode,
        CategoryID: Number(seft.$route.params.catid)
      }
      if(!data.ApprovedStatus === true){
        axios.post('ChartPeriod/Approval',data2)
        .then(res=>{
          success("Approval successfully!")
          seft.LoadDataActionPlan(seft.dataID,seft.commentID)
        })
      }
      else{
        axios.post('ChartPeriod/Approval',data2)
        .then(res=>{
          success("Cancel Approval successfully!")
          seft.LoadDataActionPlan(seft.dataID,seft.commentID)
        })
      }
     
    },
    done(data) {
      let seft = this 
      var data2 = {
        id: data.ID,
        userid: VueJwtDecode.decode(localStorage.getItem("authToken")).nameid,
        KPILevelCode: seft.$route.params.kpilevelcode,
        CategoryID: Number(seft.$route.params.catid),
        url: window.location.href
      };

      if(!data.Status === true){
        axios.post('ChartPeriod/Done',data2)
        .then(res=>{
          success("Successfully!")
          seft.LoadDataActionPlan(seft.dataID,seft.commentID)
        })
      }
      else{
        axios.post('ChartPeriod/Done',data2)
        .then(res=>{
          success("Cancel Successfully!")
          seft.LoadDataActionPlan(seft.dataID,seft.commentID)
        })
      }
      
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
    LoadDataActionPlan(dataid, commentid) {
      let seft = this
      axios.post(`ChartPeriod/getallpaging/${dataid}/${commentid}/${Number(VueJwtDecode.decode(localStorage.getItem("authToken")).nameid)}/${seft.keyword}/${seft.page}/${seft.pageSize}`)
     .then(res => {
       if (res.data.status) {
        seft.dataActionPlan = res.data.data;
        }
     })   
    },
    btntabload(item){
      let seft = this
      $("#modal-group-comment-data").on("shown.bs.modal", function (){
      });
      this.$refs['modalActionPlan'].show()
      seft.commentID = item.CommentID;
      seft.usid = item.UserID;
      seft.actionPlanText = seft.remarkText.replace("Remark", "Action Plan");
      seft.LoadDataActionPlan(seft.dataID, seft.commentID);
    },
    loadDataComment(id) {
      let seft = this
      $.ajax({
          url: `http://10.4.5.174:91/ChartPeriod/LoadDataComment/${id}/${VueJwtDecode.decode(localStorage.getItem("authToken")).nameid}`,
          type: "GET",
          
          dataType: "json",
          success: function (res) {
            var data = res.data;
           
            var total = res.total;
            seft.dataComment = res.data
            $('.total-comments').text(total);
            $(".deleteComment").off('click').on('click', function () {
              seft.deleteComment(Number($(this).data("commentid")))
            });
          },
          error: function (err) {
        }
      });
    },
    deleteComment(id) {
      let self = this
      Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      type: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!'
      }).then((result) => {
          if (result.value) {
            axios.get(`ChartPeriod/DeleteComment2/${id}`)
            .then(res=>{
              if(res){
                  if (res.status) {
                  self.loadDataComment(self.dataID);
                  success("success!");
                  } else {
                    warning('error');
                }
              }
            })
          }
      });
    },
    remark(id) {
      axios.get(`ChartPeriod/Remark/${id}`,{
        headers:{
          Authorization: 'Bearer '+ localStorage.getItem("authToken")
        }
      }).then(r => {
       this.optionsAc = r.data.users
        let result = r.data.model;
        var userid = VueJwtDecode.decode(localStorage.getItem("authToken")).nameid;
        var users = [],
          username,
          fullname;
        var arrays = r.data.users;
        $.each(arrays, function(i, item) {
          users.push({
            username: item.Username,
            fullname: item.FullName
          });
        });

        $('#Tag').suggest('@', {
          data: users,
          map: function (user) {
            return {
              value: user.username,
              text: '<strong>'+user.username + '  </strong> <small>' + user.fullname + '</small>'
            }
          }
        })
        $('#Auditor').suggest('@', {
          data: users,
          map: function (user) {
            return {
              value: user.username,
              text: '<strong>'+user.username + '  </strong> <small>' + user.fullname + '</small>'
            }
          }
        })
        $('#comment').suggest('@', {
          data: users,
          map: function (user) {
            return {
              value: user.username+'\f',
              text: '<strong>' + user.username + '  </strong> <small>' + user.fullname + '</small>'
            }
          }
        })
      })
    },
    addcomment() {
      if ($("#comment").val() == "") {
        warning("Please enter message!!");
        return;
      }
      var CommentMsg = $("#comment").val();

      var list = [];
      for (let item of CommentMsg.split(" ")) {
        let x = item.match(/[@].+[\f]/g);
        if (x !== null)
          list.push(
            x
              .toString()
              .replace("@", " ")
              .trim()
          );
      }
      var Tag = [...new Set(list.map(x => x))].join();

      var mObj = {
        DataID: this.dataID,
        CommentMsg: CommentMsg,
        UserID: Number(VueJwtDecode.decode(localStorage.getItem("authToken")).nameid),
        Tag: Tag,
        Link: window.location.href,
        Title: this.remarkText,
        KPILevelCode: this.$route.params.kpilevelcode,
        CategoryID: Number(this.$route.params.catid),
        DefaultLink: this.defaultLink
      };
      
       axios.post("ChartPeriod/AddComment",mObj)
        .then(data => {
        var res = data.data;
        if (res.status == true && res.isSendmail == true) {
          $("#comment").val("");
          success("Add Comment success!");
          this.loadDataComment(this.dataID);
          this.addNotification();
          this.addNotificationComment();
        }
        else if (res.status === true && res.isSendmail === false) {
          $("#comment").val("");
          this.loadDataComment(this.dataID);
          warning("Failed sending mail!");
        } else {
          warning("error!");
        }
      })
    },
    opencomment(item,e) {
      if (e.toElement.classList[1] === "active-td") {
        let number = Number(e.toElement.textContent),
          value = Number(e.toElement.cellIndex),
          period = $("#tblDataChart tr:nth-child(1) th:nth-child(1)").text();

        $("#modal-group-comment-data").modal("show");
       
        this.dataID = item.ID;
        var id = item.ID;
        $(".dataid").text(id);
        const monthNames = ["January", "February", "March", "April", "May", "June",
          "July", "August", "September", "October", "November", "December"
        ];
        this.remarkText = 'Remark on '
        + monthNames[value - 1]
        + ' - '
        + ' KPI Chart '
        + ' - ' + this.kpiname
        + ' - '
        + this.convertPeriod(this.period, false)
        this.remark(id);
        this.loadDataComment(id);
      }
    },
    hiddenData() {
      let seft = this;
      seft.chart.options.plugins.datalabels = {
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
          return false;
        },
        display: function(context) {
          return false;
        }
      };
      seft.chart.update();
      $(".btnHiddenData").hide();
      $(".btnShowData").show();
    },
    showData() {
      let seft = this;
      seft.chart.options = seft.options;
      seft.chart.update();
      $(".btnHiddenData").show();
      $(".btnShowData").hide();
    },
    minChart() {
      let seft = this;
      seft.options.scales.yAxes[0].ticks.min = Number(seft.min);
      seft.chart.options = seft.options;
      seft.chart.update();
    },
    stepChart() {
      let seft = this;
      seft.options.scales.yAxes[0].ticks.stepSize = seft.stepSize;
      seft.chart.options = seft.options;
      seft.chart.update();
    },
    createChart(chartId, datasets, targets, labels, label, unit) {
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
            return value;
          },
          display: function(context) {
          },
          id: "p1"
        }
      };
      const ctx = document.getElementById(chartId);

      const myChart = new Chart(ctx, {
        type: "line",
        data: {
          labels: labels,
          datasets: [
            {
              label: label,
              spanGaps: true, //data=undefined thi k draw line
              backgroundColor: "#e7263b ",
              // pointBackgroundColor: "#e7263b",
              borderColor: "#e7263b ",
              fill: false,
              data: datasets,
            },
            {
              // another line graph
              label: "Targets",
              data: targets,
              backgroundColor: "#3c8d8a",
              borderColor: "#3c8d8a",
              borderWidth: 3,
              fill: false,
            }
          ]
        },
        options: seft.options
      });
      seft.chart = myChart;
     // console.log(seft.chart);
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
    Loadchart() {
      let seft = this;
      axios.get(`ChartPeriod/ListDatas2/${seft.$route.params.kpilevelcode}/${seft.$route.params.catid}/${seft.$route.params.period}/${seft.locale}/${seft.$route.params.year}/${seft.$route.params.start}/${seft.$route.params.end}`,{
        headers:{
          Authorization: 'Bearer '+ localStorage.getItem("authToken")
        }
        })
        .then(response=>{
          seft.statusfavorite = response.data.statusfavorite;
          seft.unit = response.data.Unit;
          seft.datasets = response.data.datasets;
          seft.labels = response.data.labels;
          seft.label = response.data.label;
          seft.kpiname = response.data.kpiname;
          seft.name = response.data.kpiname;
          seft.PIC = response.data.PIC;
          seft.kpilevelID = response.data.kpilevelID;
          seft.Owner = response.data.Owner;
          seft.OwnerManagerment = response.data.OwnerManagerment;
          seft.Sponsor = response.data.Sponsor;
          seft.Participant = response.data.Participant;
          seft.dataremarks = response.data.Dataremarks;
          seft.targets = response.data.targets;

          (seft.options.label = response.label),
            (seft.options.title.text =
              "KPI Chart -" + response.data.label + " - " + response.data.kpiname),
            (seft.options.scales.yAxes[0].scaleLabel.labelString =
              response.data.Unit);
          seft.options.scales.xAxes[0].scaleLabel.labelString = seft.convertPeriod(
            response.data.period
          );

          seft.createChart(
            "planet-chart",
            seft.datasets,
            seft.targets,
            seft.labels,
            seft.label,
            seft.unit
          );
          var lastDataset = seft.datasets[seft.datasets.length - 1],
            lastTarget = seft.targets[seft.targets.length - 1];

          if (lastDataset <= lastTarget) {
            seft.chart.data.datasets[0].backgroundColor = "#e7263b";
            seft.chart.data.datasets[0].borderColor = "#e7263b";
            seft.chart.data.datasets[0].pointBorderColor = "#e7263b";
            seft.chart.update();
          } else {
            seft.chart.data.datasets[0].borderColor = "green";
            seft.chart.data.datasets[0].pointBorderColor = "green";
            seft.chart.data.datasets[0].backgroundColor = "green";

            seft.chart.update();
          }
          $("#editBugModal").modal("show");
          $(".btnShowData").hide();
        })
      
    },
    changeyears(event) {
      let newYear = event.target.value
      this.$router.push(`/ChartPeriod/${this.kpilevelcode}/${this.$route.params.catid}/${this.period}/${newYear}/${this.vstart}/${this.vend}`)
      this.Loadchart();
    },
    starts(event){
      let newStart = event.target.value
      this.$router.push(`/ChartPeriod/${this.$route.params.kpilevelcode}/${this.$route.params.catid}/${this.period}/${this.$route.params.year}/${newStart}/${this.vend}`)
      this.Loadchart();
    },
    ends(event){
      let newEnd = event.target.value
      this.$router.push(`/ChartPeriod/${this.kpilevelcode}/${this.$route.params.catid}/${this.period}/${this.$route.params.year}/${this.vstart}/${newEnd}`)
      this.Loadchart();
    },
    ListenGettask(data2){
      this.$router.push(this.URL)
      let comID = Number(this.$route.params.comID);
      this.commentID = Number(this.$route.params.comID);
      let dataID = Number(this.$route.params.dataID);
      this.dataID = Number(this.$route.params.dataID);
      let title = this.$route.params.title;
      let type = this.$route.params.type;
      let period = this.$route.params.period;
      if (comID > 0 && dataID > 0 && title != "" && type === "task") {
        this.actionPlanText = title.replace(/-/g, ' ')  + ' - '
        + ' KPI Chart '
        + ' - ' + data2.KPIName
        + ' - ' + this.convertPeriod(period, false)
        this.remarkText = this.actionPlanText.replace('Action Plan', 'Remark')
        this.$refs['modalActionPlan'].show()
        this.remark(dataID);
        this.loadDataComment(dataID);
        this.LoadDataActionPlan(dataID, comID);
        activaTab('pills-home');
      }
    },
    ListenGetComment(data){
        this.$router.push(this.Link)
        let comID = Number(this.$route.params.comID);
        this.commentID = Number(this.$route.params.comID);
        let dataID = Number(this.$route.params.dataID);
        this.dataID = Number(this.$route.params.dataID);
        let title = this.$route.params.title;
        let type = this.$route.params.type;
        if (comID > 0 && dataID > 0 && title != "" & type === "remark") {
        this.remarkText = data.Title
        this.actionPlanText = this.remarkText.replace('Remark', 'Action Plan')
          $("#modal-group-comment-data").modal("show");
          this.remark(dataID);
          this.loadDataComment(dataID);
          this.LoadDataActionPlan(dataID, comID);
        }
    }
  }
};
</script>

<style lang="scss" scoped>
.chart-container {
  position: relative;
  margin: auto;
  height: 58vh;
  width: auto;
}
input.search {
    width: 50%;
    margin-left: 5px;
    border-radius: 5%;
}
i.fas.fa-search-minus.search2 {
    width: 40px;
    border-radius: 50%;
}
span.text-muted.float-right.deleteComment:hover {
    font-size: 17px;
}
</style>
<style src="vue-multiselect/dist/vue-multiselect.min.css"></style>