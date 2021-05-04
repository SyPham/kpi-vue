<template>
<section class="content">
  <div class="container-fluid">
    <div class="row">
      <div class="col-md-12"></div>
      <div class="col-md-12">
        <div class="box boxDataset boxdataset-scrollbar">

          <div class="box-header">
            <h3
              v-if="period == 'w'"
              class="box-title"
              style="font-weight:bold"
            >Category - {{categoryname}} - Weekly</h3>
            <h3
              v-if="period == 'm'"
              class="box-title"
              style="font-weight:bold"
            >Category - {{categoryname}} - Monthly</h3>
            <h3
              v-if="period == 'q'"
              class="box-title"
              style="font-weight:bold"
            >Category - {{categoryname}} - Quarterly</h3>

            <h3
              v-if="period == 'h'"
              class="box-title"
              style="font-weight:bold"
            >Category - {{categoryname}} - Half Year</h3>

            <h3
              v-if="period == 'y'"
              class="box-title"
              style="font-weight:bold"
            >Category - {{categoryname}} - Yearly</h3>
          </div>
          <!-- /.box-header -->
          <div class="box-body ">
            <div class="col-md-12">

              <!-- @* month *@ -->
              <div class="form-inline" :style="period ==  'm' ? '':'display:none'"  id="searchMonth">
                <label class="my-1 mr-2" for="startMonth">Year</label>
                <select v-model="searchyear"  @change="changeyears($event)" class="custom-select form-control year my-1 mr-sm-2" id="monthOfYear">
                  <option value="0" selected >Choose...</option>
                  <option
                    v-for="(n, key, index) in 100"
                    :key="index"
                    :value="2000 + n"

                  >Year {{2000 + n}}</option>
                </select>
                <label class="my-1 mr-2" for="startMonth">Start</label>
                <select v-model="vstart" @change="starts($event)" class="custom-select form-control my-1 mr-sm-2" id="startMonth">
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
                <select v-model="vend" @change="ends($event)" class="custom-select form-control my-1 mr-sm-2" id="endMonth">
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
                <button type="button" class="btn bg-teal margin btnClearSearch">
                  <i class="fa fa-remove"></i> Reset search
                </button>
              </div>

              <!-- @* week *@ -->
              <div class="form-inline" :style="period ==  'w' ? '':'display:none'" id="searchWeek">
                <label class="my-1 mr-2" for="startMonth">Year</label>
                <select v-model="searchyear"  @change="changeyears($event)" class="custom-select form-control year my-1 mr-sm-2" id="weekOfYear">
                  <option value="0" selected>Choose...</option>

                  <option v-for="(n, key, index) in 100" :key="index" :value="2000 + n" >Year {{2000 + n}}</option>
                </select>
                <label class="my-1 mr-2" for="startWeek">Week Start</label>
                <select v-model="vstart" @change="starts($event)" class="custom-select form-control my-1 mr-sm-2" id="startWeek">
                  <option value="0" >Choose...</option>

                  <option v-for="(n, key, index) in 53" :key="index" :value="n" >Week {{n}}</option>
                </select>
                <label class="my-1 mr-2" for="endWeek">Week End</label>
                <select v-model="vend" @change="ends($event)" class="custom-select form-control my-1 mr-sm-2" id="endWeek">
                  <option value="0" selected>Choose...</option>

                  <option v-for="(n, key, index) in 53" :key="index" :value="n"  >Week {{n}}</option>
                </select>
                <button type="button" class="btn bg-teal margin btnClearSearch">
                  <i class="fa fa-remove"></i> Reset search
                </button>
              </div>

              <!-- @* Quarter *@ -->
              <div class="form-inline" :style="period ==  'q' ? '':'display:none'" id="searchQuarter">
                <label class="my-1 mr-2" for="startMonth">Year</label>
                <select v-model="searchyear" @change="changeyears($event)" class="custom-select form-control year my-1 mr-sm-2" id="quarterOfYear">
                  <option value="0" selected>Choose...</option>

                  <option v-for="(n, key, index) in 100" :key="index" :value="2000 + n">Year {{2000 + n}}</option>
                </select>
                <label class="my-1 mr-2" for="startMonth">Start</label>
                <select v-model="vstart" @change="starts($event)" class="custom-select form-control my-1 mr-sm-2" id="startQuarter">
                  <option value="0" selected>Choose...</option>

                  <option v-for="(n, key, index)  in 4"  :key="index" :value=" n">Quarter {{n}}</option>
                </select>
                <label class="my-1 mr-2" for="endMonth">End</label>
                <select v-model="vend" @change="ends($event)" class="custom-select form-control my-1 mr-sm-2" id="endQuarter">
                  <option value="0" selected>Choose...</option>

                  <option v-for="(n, key, index)  in 4"  :key="index" :value=" n">Quarter {{n}}</option>
                </select>
                <button type="button" class="btn bg-teal margin btnClearSearch">
                  <i class="fa fa-remove"></i> Reset search
                </button>
              </div>

              <!-- @* Half Year *@ -->
              <div class="form-inline" :style="period ==  'h' ? '':'display:none'" id="searchQuarter">
                <label class="my-1 mr-2" for="startMonth">Year</label>
                <select v-model="searchyear" @change="changeyears($event)" class="custom-select form-control year my-1 mr-sm-2" id="quarterOfYear">
                  <option value="0" selected>Choose...</option>

                  <option v-for="(n, key, index) in 100" :key="index" :value="2000 + n">Year {{2000 + n}}</option>
                </select>
                <label class="my-1 mr-2" for="startMonth">Start</label>
                <select v-model="vstart" @change="starts($event)" class="custom-select form-control my-1 mr-sm-2" id="startQuarter">
                  <option value="0" selected>Choose...</option>

                  <option v-for="(n, key, index)  in 1"  :key="index" :value=" n">HalfYear {{n}}</option>
                </select>
                <label class="my-1 mr-2" for="endMonth">End</label>
                <select v-model="vend" @change="ends($event)" class="custom-select form-control my-1 mr-sm-2" id="endQuarter">
                  <option value="0" >Choose...</option>
                  <option value="6" >HalfYear 6</option>
                  <option value="12">HalfYear 12</option>
                </select>
                <button type="button" class="btn bg-teal margin btnClearSearch">
                  <i class="fa fa-remove"></i> Reset search
                </button>
              </div>

              <!-- @* Year *@ -->
              <div class="form-inline" :style="period ==  'y' ? '':'display:none'" id="searchYear">
                <label class="my-1 mr-2" for="YearOfYear">Year</label>
                <select v-model="searchyear" @change="changeyears($event)" class="custom-select form-control my-1 mr-sm-2 YearOfYear" id="YearOfYear">
                  <option value="0" selected>Choose...</option>

                  <option v-for="(n, key, index) in 100" :key="index" :value="2000 + n">Year {{2000 + n}}</option>
                </select>
                <label class="my-1 mr-2" for="startMonth">Start</label>
                <select v-model="vstart" @change="starts($event)" class="custom-select form-control my-1 mr-sm-2" id="startYear">
                  <option value="0" selected>Choose...</option>

                  <option v-for="(n, key, index) in 100" :key="index" :value="2000 + n">Year {{2000 + n}}</option>
                </select>
                <label class="my-1 mr-2" for="endMonth">End</label>
                <select v-model="vend" @change="ends($event)" class="custom-select form-control my-1 mr-sm-2" id="endYear">
                  <option value="0" selected>Choose...</option>

                  <option v-for="(n, key, index) in 100" :key="index" :value="2000 + n">Year {{2000 + n}}</option>
                </select>
                <button type="button" class="btn bg-teal margin btnClearSearch">
                  <i class="fa fa-remove"></i> Reset search
                </button>
              </div>


            </div>
          </div>

          <div class="box-body table-responsive no-padding" id="tblDataset">
            <div class="col-md-12">
              <table class="table">
                <thead>
                  <th class="text-center">KPI</th>
                  <th v-for="(item,key,index) in title" :key="index" class="text-center">{{item }}</th>
                </thead>
                <tbody v-for="(item,key,index) in data" :key="index">
                  <tr v-if="item.Datasets.length > 0 " class="warning">
                    <td class="text-center">
                      <div class="tooltip-css">
                        {{item.KPIName}}
                        <div class="right">
                          <h4>Category: {{item.CategoryName}}</h4>
                          <p>Manager: {{item.Manager}}</p>
                          <p>Owner: {{item.Owner}}</p>
                          <p>Updater: {{item.Updater}}</p>
                          <p>Sponsor: {{item.Sponsor}}</p>
                          <p>Participant: {{item.Participant}}</p>
                          <i></i>
                        </div>
                      </div>
                    </td>
                    <td
                      v-for="(item2,key,index) in item.Datasets"
                      :key="index"
                      :class="item2.ValueArray[1] === 'True' ? 'datasetTd text-center active-td2'  : 'datasetTd text-center active-td' "
                      @click="opencomment(item2,$event)"
                      :data-id="item2.ID"
                    >
                      <div class="tooltip-css" style="text-align: center;">
                        {{item2.ValueArray[0] == 0 ? "N/A" : item2.ValueArray[0]}}
                        <div class="top">
                          <p>{{item.CategoryName}}</p>
                          <p>{{item.Period.substring(0, item.Period.length - 2)}} : {{item2.Week}}</p>
                          <p>Target : {{item2.ValueArray[2] || "#N/A"}}</p>
                          <i></i>
                        </div>
                      </div>
                    </td>
                  </tr>
                </tbody>
              </table>
            </div>
          </div>
          <!-- /.box-body -->
        </div>
        <!-- /.box -->
         <!-- comment -->

      <div class="modal fade modal" id="modal-group-comment-data2">
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
                                <img src="src/img/user-icon.png" alt="" class="img-circle">
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
                                <p v-if="item.Task"><a @click="btntabload(item)"  data-toggle="modal"  data-dismiss="modal" class="btn btn-sm btn-danger text-bold btnTask"><i class="fa fa-tags"></i> Task</a> </p>
                                <p v-else><a @click="btntabload(item)"  data-toggle="modal" title="There are no task."  data-dismiss="modal" class="btn btn-sm btn-success text-bold btnTask tooltip-ui"><i class="fa fa-tags"></i> Task</a> </p>
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
              <div>Add </div>
            </div>
            <div class="e-content">

              <div>
                <ejs-grid  ref="grid" :gridLines='lines' :allowTextWrap='true' :allowResizing='true' :editSettings='editSettings' :actionBegin='actionBegin' :searchSettings="searchSettings" :rowSelected="rowSelected"  :toolbar='toolbarOptions' :dataSource='dataActionPlan' :allowPaging="true" :pageSettings='pageSettings' >
                  <e-columns>

                    <e-column field='Title' :template="TitleTemplate" headerText='Action Plan name' :edit="editparams"  width='200' textAlign='Center' ></e-column>
                    <e-column field='Description' :template="DescriptionTemplate" headerText='Descriptions' width='150' textAlign='Center' ></e-column>
                    <e-column field='Tag' :template="TagTemplate" headerText='PIC' width='120' isPrimaryKey='true' textAlign='Center' ></e-column>

                    <e-column field='Deadline' :editTemplate="DeadlineTemplate" editType='datepickeredit' headerText='Dua Date' width='250' textAlign='Center' ></e-column>

                    <e-column field='UpdateSheduleDate' :editTemplate="UpdateSheduleDateTemplate" editType='datepickeredit' headerText='Update shedule date' width='250' textAlign='Center' ></e-column>

                    <e-column field='ActualFinishDate' isPrimaryKey='true' headerText='Actual finish date' width='250' textAlign='Center' ></e-column>

                    <e-column field='Remark' headerText='Remark' width='250' textAlign='Center' ></e-column>

                    <e-column field='' isPrimaryKey='true' :template="StatusTemplate" headerText='Finish' width='200' textAlign='Center' ></e-column>
                    <e-column field='' isPrimaryKey='true' :template="ApproveTemplate" headerText='Approve' width='200' textAlign='Center' ></e-column>
                    <e-column headerText='Manage Records' isPrimaryKey='true' :template="RecordTemplate" width='150' textAlign='Center'  ></e-column>

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
    </div>
  </div>
</section>
</template>
<script>
import { HTTP } from "../../http-constants";
import VueJwtDecode from "vue-jwt-decode";
import Paginate from "vuejs-paginate";
import EventBus from "../../utils/EventBus.js";
import { TabComponent, TabPlugin } from '@syncfusion/ej2-vue-navigations';
import Multiselect from "vue-multiselect";
import Vue from "vue";
Vue.component(TabPlugin.name, TabComponent);

import { GridPlugin, Page ,Toolbar,Filter,Edit ,Resize,GridLine} from "@syncfusion/ej2-vue-grids";

Vue.use(GridPlugin);
export default {
  data() {
    return {
      lines: 'Both',
      currentUser: Number(VueJwtDecode.decode(localStorage.getItem("authToken")).nameid),
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
              <ejs-datepicker :enabled="data.CreatedBy == currentUser || data.Auditor == currentUser ? true :false" @change='DeadlineChange($event)' format='MM-dd-yyyy' placeholder='Enter date' floatLabelType='Never' v-model="data.Deadline"></ejs-datepicker>
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
      toolbarOptions : ['Search'],
      pageSettings: { pageSize: 10 },
      data: [],
      searchSettings: { hierarchyMode: 'Parent' },
      editparams: { params: { format: 'n' } },
      editSettings:{ allowDeleting: false, allowEditing: true, mode: 'Normal',allowEditOnDblClick: true },
      activetab: 1,
      dataActionPlan: [],
      dataComment: [],
      dataID: '',
      commentID: '',
      actionPlanText: 'actionplan',
      remarkText: 'asdasdasd',
      listencom: '',
      defaultLink: null,
      URL: '',
      Link:'',
      closemodal:"",
      seachActionPlan: "",
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
      start: 0,
      end: 0,
    };
  },
  provide: {
    grid: [Page,Toolbar,Filter,Edit,Resize],
  },
  watch: {
    seachActionPlan: function(newOld,oldVal){
      let seft = this
      let dataid = $('.dataid').text();
      let commentid = $('.commentid').text()
      seft.keyword = newOld;
      seft.LoadDataActionPlan(dataid, commentid);
    },
    data2: function(newOld, oldVal) {
      let self = this;
      let comID = Number(self.$route.params.comID);
      self.commentID = Number(self.$route.params.comID);
      let dataID = Number(self.$route.params.dataID);
      self.dataID = Number(self.$route.params.dataID);
      let title = self.$route.params.title;
      let type = self.$route.params.type;
      let period = this.$route.params.period;
      if (comID > 0 && dataID > 0 && title != "" & type === "remark") {
        self.remarkText = title.replace(/-/g, ' ')
        + ' - ' + self.kpiname
        + ' - ' + self.convertPeriod(period, false)
        self.actionPlanText = self.remarkText.replace('Remark','Action Plan')
        $("#modal-group-comment-data2").modal("show");
        self.remark(dataID);
        self.LoadDataActionPlan(dataID, comID);
        self.loadDataComment(dataID);
      }
      if (comID > 0 && dataID > 0 && title != "" && type === "task") {
        self.actionPlanText = title.replace(/-/g, ' ')
        + ' - ' + self.kpiname
        + ' - ' + self.convertPeriod(period, false)
        self.remarkText = self.actionPlanText.replace('Action Plan', 'Remark')
        self.$refs['modalActionPlan'].show()
        self.remark(dataID);
        self.loadDataComment(dataID);
        self.LoadDataActionPlan(dataID, comID);
      }

    },
    categoryname: function(newOld,oldVal){

      var item = this.actionPlanText.replace("-  -",' - '+newOld + ' - ');
      this.actionPlanText = item;
      var item2 = this.remarkText.replace("-  -",' - '+newOld + ' - ');
      this.remarkText = item2
    },
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
  },
  components:{
    Paginate,
    Multiselect
  },
  mounted(){

    let seft = this
    EventBus.$on('item',data =>{
      seft.$router.push(data.Link)
      seft.ListenGetCommentDataset(data);
    })

    EventBus.$on('hello',data2 =>{
      seft.$router.push(data2.URL)
      seft.ListenGettaskDataset(data2);
    })

    $('#modal-group-comment-data2').off('hidden.bs.modal').on('hidden.bs.modal', function () {
      seft.$router.push(`/Dataset/${seft.$route.params.catid}/${seft.$route.params.period}/${seft.$route.params.start}/${seft.$route.params.end}/${seft.$route.params.year}`)
      seft.LoadDataset()
    });
  },
  destroyed() {
    EventBus.$off('DeadlineChange',this.DeadlineChange);
    EventBus.$off('DeadlineUpdateChange',this.DeadlineUpdateChange);
    EventBus.$off('approval',this.approval);
    EventBus.$off('done',this.done);
  },
  beforeDestroy: function () {
    EventBus.$off('item')
    EventBus.$off('hello')
  },
  created() {
    let seft = this;
    seft.period = seft.$route.params.period;
    seft.vstart = seft.$route.params.start;
    seft.vend = seft.$route.params.end;
    seft.searchyear = seft.$route.params.year
    seft.LoadDataset();

    seft.defaultLink = `/dataset/${seft.$route.params.catid}/${seft.$route.params.period}/${seft.$route.params.start}/${seft.$route.params.end}/${seft.$route.params.year}`
    EventBus.$on('DeadlineChange',seft.DeadlineChange)
    EventBus.$on('DeadlineUpdateChange',seft.DeadlineUpdateChange)
    EventBus.$on('approval',seft.approval)
    EventBus.$on('done',seft.done)
    EventBus.$on('deleteActionPlan',seft.deleteActionPlan)
  },
  methods: {

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
    closemodalAc(){
      let seft = this
      seft.$refs['modalActionPlan'].hide();
      seft.LoadDataset()
      seft.$router.push(`/Dataset/${seft.$route.params.catid}/${seft.$route.params.period}/${seft.$route.params.start}/${seft.$route.params.end}/${seft.$route.params.year}`)
    },
    onChange(args){
      this.dateValue = moment(String(args.value)).format('MM-DD-YYYY')
    },
    addActionPlan(){
      let seft = this
      let datenew = moment(String(new Date())).format('MM-DD-YYYY')

      seft.dateValue = moment(String(seft.dateValue)).format('MM-DD-YYYY')
      var KPILevelCodeAndPeriod = seft.KPILevelCode + seft.$route.params.period;
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
        KPILevelCode: seft.KPILevelCode,
        DefaultLink: seft.defaultLink
      };
      axios.post('ChartPeriod/Add' , obj)
      .then(res=>{
        if(res.data.status === true){
          seft.LoadDataActionPlan(seft.dataID,seft.commentID)
          Toast.fire({
            icon: 'success',
            title: 'Add ActionPlan successfully'
          })
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
      $("#modal-group-comment-data2").modal("show");
    },
    hideModal(){
    },
    save(){
    },
    changeyears(event) {
      let newYear = event.target.value
      this.$router.push(`/dataset/${this.$route.params.catid}/${this.period}/${this.vstart}/${this.vend}/${newYear}`)
      this.LoadDataset();
    },
    starts(event){
      let newStart = event.target.value
      this.$router.push(`/dataset/${this.$route.params.catid}/${this.period}/${newStart}/${this.vend}/${this.$route.params.year}`)
      this.LoadDataset();
    },
    ends(event){
      let newEnd = event.target.value
      this.$router.push(`/dataset/${this.$route.params.catid}/${this.period}/${this.vstart}/${newEnd}/${this.$route.params.year}`)
      this.LoadDataset();
    },
    ListenGettaskDataset(data2){
      let comID = Number(this.$route.params.comID);
      let dataID = Number(this.$route.params.dataID);
      let title = this.$route.params.title;
      let type = this.$route.params.type;
      let period = this.$route.params.period;
      if (comID > 0 && dataID > 0 && title != "" && type === "task") {
        this.actionPlanText = title.replace(/-/g, ' ')
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
    ListenGetCommentDataset(data){

      let comID = Number(this.$route.params.comID);
      let dataID = Number(this.$route.params.dataID);
      let title = this.$route.params.title;
      let type = this.$route.params.type;
      if (comID > 0 && dataID > 0 && title != "" & type === "remark") {
      this.remarkText = data.Title
      this.actionPlanText = this.remarkText.replace('Remark', 'Action Plan')

        $("#modal-group-comment-data2").modal("show");
        this.remark(dataID);
        this.LoadDataActionPlan(dataID, comID);
        this.loadDataComment(dataID);
      }
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
                  self.loadDataComment();
                  success("success!");
                  } else {
                    warning('error');
                }
              }
            })
          }
      });
    },
    changePage(pageNum) {
      this.loadDataProvide(this.obj,pageNum);
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
    deleteActionPlan(data) {
      let seft = this
      if (Number(data.ID) > 0 && data.CreatedBy == seft.currentUser) {
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
        KPILevelCode: this.KPILevelCode,
        CategoryID: Number(this.$route.params.catid),
        DefaultLink: this.defaultLink
      };
      axios.post("/ChartPeriod/AddComment", mObj)
        .then(data => {
          var res = data.data;

          if (res.status == true && res.isSendmail == true) {
          $("#comment").val("");
          success("success!");
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
        .catch(error => {

      });
    },
    opencomment(item2,e) {
      if (e.toElement.classList[2] == "active-td" && "active-td2") {
        let number = Number(e.toElement.textContent),
          value = Number(e.toElement.cellIndex),
          period = $("#tblDataChart tr:nth-child(1) th:nth-child(1)").text();
        $("#modal-group-comment-data2").modal("show");
        var id = e.toElement.dataset.id;
        this.dataID = item2.ID;
        $(".dataid").text(id);
        const monthNames = ["January", "February", "March", "April", "May", "June",
          "July", "August", "September", "October", "November", "December"
        ];
        this.remarkText = 'Remark on '
        + monthNames[value - 1]
        + ' - ' + this.kpiname
        + ' - '
        + this.convertPeriod(this.period, false)
        this.remark(id);
        this.loadDataComment(id);
      }
    },
    remark(id) {
     axios.get(`ChartPeriod/Remark/${id}`).then(r => {
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
      });
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
    btnSaveActionPlan(){
      let seft = this
       $('.btnSaveActionPlan').unbind('click').on('click', function () {
          seft.addActionPlan();
        });
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
    btntabload(item) {
      let seft = this;
      $("#modal-group-comment-data").on("shown.bs.modal", function (){
      });
      this.$refs['modalActionPlan'].show()
      seft.commentID = item.CommentID;
      seft.actionPlanText = seft.remarkText.replace("Remark", "Action Plan");
      seft.LoadDataActionPlan(seft.dataID, seft.commentID);
    },
    loadDataComment(id) {
      let seft = this;
      $.ajax({
        url: `http://10.4.5.174:91/ChartPeriod/LoadDataComment/${id}/${VueJwtDecode.decode(localStorage.getItem("authToken")).nameid}`,
        type: "GET",
        dataType: "json",
        success: function(res) {
          var data = res.data;
          var total = res.total;
          seft.dataComment = res.data
          $(".deleteComment").off('click').on('click', function () {
            seft.deleteComment(Number($(this).data("commentid")))
          });
        },
        error: function(err) {}
      });
    },
    LoadTitle() {
      let seft = this;
      switch (seft.$route.params.period) {
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
      axios.get(`Dataset/getalldatabycategory/${seft.$route.params.catid}/${seft.$route.params.period}/${seft.$route.params.start}/${seft.$route.params.end}/${seft.$route.params.year}`)
      .then(response => {
        seft.data = response.data;
        seft.data2 = response.data[0].Datasets;
        seft.categoryname = response.data[0].CategoryName;
        seft.kpiname = response.data[0].KPIName;
        seft.KPILevelCode = response.data[0].KPILevelCode;

        seft.statusfavorite = response.data.statusfavorite;
        seft.unit = response.data.Unit;
        seft.datasets = response.data[0].Datasets;
        seft.labels = response.data.labels;
        seft.label = response.data.label;
        seft.PIC = response.data.PIC;
        seft.Owner = response.data.Owner;
        seft.OwnerManagerment = response.data.OwnerManagerment;
        seft.Sponsor = response.data.Sponsor;
        seft.Participant = response.data.Participant;
        seft.dataremarks = response.data.Dataremarks;
        seft.targets = response.data.targets;
        $("#editBugModal").modal("show");
        $(".btnShowData").hide();
        seft.LoadTitle();
      })
    }
  }
};
</script>
<style src="vue-multiselect/dist/vue-multiselect.min.css"></style>
