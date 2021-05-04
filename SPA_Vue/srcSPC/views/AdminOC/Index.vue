<template>
  <section class="content">
    <div class="container-fluid">
      <div class="row">

        <div class="col-md-12">
          <div class="callout bg-yellow-gradient" style="border-color:#c57901">
            <h3>5. OC KPI</h3>
          </div>
        </div>

        <div class="col-md-4">
          <div class="card">
            <div class="card-header">
              <h5 class="card-title">Organization Chart</h5>
              <div class="card-tools">
                <button type="button" class="btn btn-tool" data-card-widget="collapse">
                  <i class="fas fa-minus"></i>
                </button>
              </div>
            </div>
            <div class="card-body table-responsive p-0">
              <ejs-treegrid
                ref='treegrid'
                :dataSource="data"
                childMapping="children"
                :treeColumnIndex="1"
                height='550px'
                :pageSettings="pageSettings"
                :allowExcelExport="true"
                :allowPdfExport="true"
                :allowSorting="true"
                :contextMenuItems="contextMenuItems"
                :contextMenuClick="contextMenuClick"
                :rowSelected="rowSelected"
                :actionComplete="actionComplete"
                :searchSettings="searchSettings"
                :toolbar="toolbar" >
                <e-columns>
                  <e-column  field="levelnumber" width="50" isPrimaryKey="true" headerText="Level" :disableHtmlEncode="false"></e-column>
                  <e-column field="title" editType="stringedit" headerText="OC Name" :edit="editparams" :template="OCTemplate" :disableHtmlEncode="false"></e-column>
                </e-columns>
              </ejs-treegrid>
            </div>
          </div>
        </div>

        <div class="col-md-8">
          <div class="card">
            <div class="card-header">
              <h5 class="card-title">KPI  {{title}}</h5>
              <div class="card-tools">
                <button type="button" class="btn btn-tool" data-card-widget="collapse">
                  <i class="fas fa-minus"></i>
                </button>
              </div>
            </div>
            <div v-if="isHide" class="card-body table-responsive p-0">
              <ejs-grid ref="grid" :searchSettings="searchSettings" :gridLines="lines" :allowResizing="true" :dataBound='dataBound' :actionComplete="actionComplete" :rowSelected="rowSelected2"  :toolbar='toolbarOptions' :dataSource='data2' :allowPaging='true' :pageSettings='pageSettings2' >
                <e-columns>
                  <e-column field='ID' :template="SttTemplate" headerText='#' width='50' textAlign='Center' isPrimaryKey='true'></e-column>
                  <e-column field='' :template="updateTemplate" headerText='' width='80' textAlign='Center' ></e-column>
                  <e-column field='Name' headerText='KPI Organization Chart' width='200' textAlign='Left' ></e-column>
                  <e-column field='Kind' headerText='Kind' width='200' textAlign='Center' ></e-column>
                  <e-column field='' :template="ConversionTemplate" headerText='Conversion perfomance' width='100' textAlign='Center' ></e-column>
                  <e-column field='' :template="WeeklyTemplate" headerText='Weekly' width='100' textAlign='Center' ></e-column>
                  <e-column field='' :template="MonthlyTemplate" headerText='Monthly' width='100' textAlign='Center' ></e-column>
                  <e-column field='' :template="QuaterlyTemplate" headerText='Quaterly' width='100' textAlign='Center' ></e-column>
                  <e-column field='' :template="HalfYearTemplate" headerText='Half Year' width='100' textAlign='Center' ></e-column>
                  <e-column field='' :template="YearlyTemplate" headerText='Yearly' width='100' textAlign='Center' ></e-column>
                  <e-column field='' :template="ShowdataTemplate" headerText='Modify' width='100' textAlign='Center' ></e-column>
                  <e-column field='' :template="ModifyByTemplate" headerText='Modify By' width='150' textAlign='Center' ></e-column>
                </e-columns>
              </ejs-grid>
            </div>
          </div>

        </div>

          <!-- modal -->
          <b-modal ref="weekly" hide-footer title="Weekly">
            <div class="modal-body">
                <div>
                  <div class="box-body">
                    <div class="form-group">
                      <label for="weekly">Day of the week </label>
                      <select v-model="dayweekly" class="form-control weekly" id="weekly">
                        <option value="">Choose day of the week</option>
                        <option value="2">Monday</option>
                        <option value="3">Tuesday</option>
                        <option value="4">Wednesday</option>
                        <option value="5">Thursday</option>
                        <option value="6">Friday</option>
                        <option value="7">Saturday</option>
                      </select>
                    </div>
                    <div class="form-group">
                      <label for="target">Woking Plan</label>
                      <textarea v-model="workweekly" class="form-control target" id="target" rows="3"></textarea>
                    </div>
                    <div class="form-group">
                      <ejs-radiobutton label='Public'  @change="CheckWeekPublic($event)" name='default' :checked="weekpublic"></ejs-radiobutton>
                      <!-- <ejs-switch ref="wswitch" @change="CheckWeekPublic()" :checked="weekpublic"></ejs-switch> -->
                      <!-- <label>Public</label> -->
                    </div>
                    <div class="form-group">
                      <ejs-radiobutton label='Private' @change="CheckPrivateW($event)" name='default' :checked="!weekpublic"></ejs-radiobutton>
                      <!-- <ejs-switch ref="wswitch"  @change="CheckPrivate()" :checked="weekprivate"></ejs-switch> -->
                      <!-- <label>Private</label> -->
                    </div>
                  </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-light" >Close</button>
                <button type="submit" @click="saveWeekly"  class="btn btn-primary">Save changes</button>
            </div>
          </b-modal>

          <b-modal ref="monthly" hide-footer title="Monthly">
            <div class="modal-body">
                <div class='form-group'>
                  <label for="monthly">Monthly</label>
                  <div class='input-group date monthly'>
                    <ejs-datepicker @change='onChange($event)' width="500" format='MM-dd-yyyy' placeholder='Enter date' :value="dateValue" v-model="dateValue"></ejs-datepicker>
                  </div>
                </div>
                <div class="form-group">
                    <label for="target">Woking Plan</label>
                    <textarea v-model="workmonthly" class="form-control target" id="target" rows="3" ></textarea>

                </div>

                <div class="form-group">
                  <ejs-radiobutton label='Public' @change="CheckMonthPublic($event)" name='default' :checked="monthpublic"></ejs-radiobutton>
                  <!-- <ejs-switch ref="wswitch" @change="CheckMonthPublic($event)" :checked="weekpublic"></ejs-switch>
                  <label>Public</label> -->
                </div>
                <div class="form-group">
                  <ejs-radiobutton label='Private' @change="CheckPrivateM($event)" name='default' :checked="!monthpublic"></ejs-radiobutton>
                  <!-- <ejs-switch ref="wswitch" @change="CheckPrivateM($event)" :checked="monthprivate"></ejs-switch>
                    <label>Private</label> -->
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-light" >Close</button>
                <button type="submit" @click="saveMonthly"  class="btn btn-primary">Save changes</button>
            </div>
          </b-modal>

          <b-modal ref="halfyear" hide-footer title="HalfYear">
            <div class="modal-body">
                <div class='form-group'>
                  <label for="monthly">Half Year</label>
                  <div class='input-group date monthly'>
                    <ejs-datepicker @change='onChange($event)' width="500" format='MM-dd-yyyy' placeholder='Enter date' :value="dateHalf" v-model="dateHalf"></ejs-datepicker>
                  </div>
                </div>
                <div class="form-group">
                    <label for="target">Woking Plan</label>
                    <textarea v-model="workhalfyear" class="form-control target" id="target" rows="3" ></textarea>

                </div>

                <div class="form-group">
                  <ejs-radiobutton label='Public' @change="CheckHalfPublic($event)" name='default' :checked="halfpublic"></ejs-radiobutton>

                </div>
                <div class="form-group">
                  <ejs-radiobutton label='Private' @change="CheckPrivateH($event)" name='default' :checked="!halfpublic"></ejs-radiobutton>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-light" >Close</button>
                <button type="submit" @click="saveHalfYear"  class="btn btn-primary">Save changes</button>
            </div>
          </b-modal>

          <b-modal ref="quaterly" hide-footer title="Quaterly">
            <div class="modal-body">
                <div class="box-body">
                  <div class='form-group'>
                    <label for="monthly">Quaterly</label>
                    <div class='input-group date monthly'>
                      <ejs-datepicker @change='onChange($event)' width="500" format='MM-dd-yyyy' placeholder='Enter date' v-model="dayquater" :value="dayquater"></ejs-datepicker>
                    </div>
                  </div>
                  <div class="form-group">
                    <label for="target">Woking Plan</label>
                    <textarea v-model="workquaterly" class="form-control target" id="target" rows="3" ></textarea>
                  </div>

                  <div class="form-group">
                    <ejs-radiobutton label='Public' @change="CheckQuaterPublic($event)" name='default' :checked="quaterpublic"></ejs-radiobutton>
                    <!-- <ejs-switch @change="CheckQuaterPublic($event)"   ref="wswitch" :checked="quaterpublic" ></ejs-switch>
                    <label>Public</label> -->
                  </div>
                  <div class="form-group">
                    <ejs-radiobutton label='Private' @change="CheckPrivateQ($event)" name='default' :checked="!quaterpublic"></ejs-radiobutton>
                      <!-- <ejs-switch @change="CheckPrivateQ($event)"  ref="wswitch" :checked="quaterprivate" ></ejs-switch>
                      <label>Private</label> -->
                  </div>
              </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-light" >Close</button>
                <button type="submit" @click="saveQuaterly"  class="btn btn-primary">Save changes</button>
            </div>
          </b-modal>

          <b-modal ref="yearly" hide-footer title="Yearly">
            <div class="modal-body">
                <div class="box-body">
                  <div class='form-group'>
                      <label for="monthly">Yearly</label>
                      <div class='input-group date monthly'>
                          <ejs-datepicker @change='onChange($event)' width="500" format='MM-dd-yyyy' placeholder='Enter date' v-model="dayyear" :value="dayyear"></ejs-datepicker>
                      </div>
                  </div>
                  <div class="form-group">
                      <label for="target">Woking Plan</label>
                      <textarea v-model="workyearly" class="form-control target" id="target" rows="3" ></textarea>

                  </div>

                  <div class="form-group">
                    <ejs-radiobutton label='Public' @change="CheckYearPublic($event)" name='default' :checked="yearpublic"></ejs-radiobutton>
                      <!-- <ejs-switch @change="CheckYearPublic($event)"   ref="wswitch" :checked="yearpublic" ></ejs-switch>
                      <label>Public</label> -->
                  </div>
                  <div class="form-group">
                    <ejs-radiobutton label='Private' @change="CheckPrivateY($event)" name='default' :checked="!yearpublic"></ejs-radiobutton>
                      <!-- <ejs-switch @change="CheckPrivateY($event)"  ref="wswitch" :checked="yearprivate" ></ejs-switch>
                      <label>Private</label> -->
                </div>
              </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-light" >Close</button>
                <button type="submit" @click="saveYearly"  class="btn btn-primary">Save changes</button>
            </div>
          </b-modal>


          <b-modal ref="showdata" hide-footer >
            <!-- weekly -->
            <div class="modal-header">
                <h4 class="modal-title">
                    <i class="fas fa-plus"></i> Weekly
                </h4>
            </div>
            <div class="modal-body">
                <div>
                  <div class="box-body">
                    <div class="form-group">
                      <label for="weekly">Day of the week </label>
                      <select v-model="dayweekly" class="form-control weekly" id="weekly">
                        <option value="">Choose day of the week</option>
                        <option value="2">Monday</option>
                        <option value="3">Tuesday</option>
                        <option value="4">Wednesday</option>
                        <option value="5">Thursday</option>
                        <option value="6">Friday</option>
                        <option value="7">Saturday</option>
                      </select>
                    </div>
                    <div class="form-group">
                      <label for="target">Woking Plan</label>
                      <textarea v-model="workweekly" class="form-control target" id="target" rows="3"></textarea>
                    </div>
                    <div class="form-group">
                      <ejs-radiobutton label='Public' @change="CheckWeekPublic($event)"  :checked="weekpublic"></ejs-radiobutton>
                    </div>
                    <div class="form-group">
                      <ejs-radiobutton label='Private' @change="CheckPrivateW($event)"  :checked="!weekpublic"></ejs-radiobutton>
                    </div>
                  </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-light" >Close</button>
                <button type="submit" @click="saveWeekly"  class="btn btn-primary">Save changes</button>
            </div>


            <!-- monthly -->
            <div class="modal-header">
                <h4 class="modal-title">
                    <i class="fas fa-plus"></i> Monthly
                </h4>
            </div>
            <div class="modal-body">
                <div class='form-group'>
                  <label for="monthly">Monthly</label>
                  <div class='input-group date monthly'>
                    <ejs-datepicker @change='onChange($event)' width="500" format='MM-dd-yyyy' placeholder='Enter date' :value="dateValue" v-model="dateValue"></ejs-datepicker>
                  </div>
                </div>
                <div class="form-group">
                    <label for="target">Woking Plan</label>
                    <textarea v-model="workmonthly" class="form-control target" id="target" rows="3" ></textarea>

                </div>

                <div class="form-group">
                  <ejs-radiobutton label='Public' @change="CheckMonthPublic($event)"  :checked="monthpublic"></ejs-radiobutton>
                </div>
                <div class="form-group">
                  <ejs-radiobutton label='Private' @change="CheckPrivateM($event)"  :checked="!monthpublic"></ejs-radiobutton>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-light" >Close</button>
                <button type="submit" @click="saveMonthly"  class="btn btn-primary">Save changes</button>
            </div>

            <!-- halfyear -->
            <div class="modal-header">
              <h4 class="modal-title">
                <i class="fas fa-plus"></i> Half Year
              </h4>
            </div>
            <div class="modal-body">
              <div class="box-body">
                <div class='form-group'>
                  <label for="monthly">Half Year</label>
                  <div class='input-group date monthly'>
                    <ejs-datepicker @change='onChange($event)' width="500" format='MM-dd-yyyy' placeholder='Enter date' v-model="dateHalf" :value="dateHalf"></ejs-datepicker>
                  </div>
                </div>
                <div class="form-group">
                  <label for="target">Woking Plan</label>
                  <textarea v-model="workquaterly" class="form-control target" id="target" rows="3" ></textarea>
                </div>
                <div class="form-group">
                  <ejs-radiobutton label='Public' @change="CheckHalfPublic($event)" name='default' :checked="halfpublic"></ejs-radiobutton>
                </div>
                <div class="form-group">
                  <ejs-radiobutton label='Private' @change="CheckPrivateH($event)" name='default' :checked="!halfpublic"></ejs-radiobutton>
                </div>
              </div>
            </div>
            <div class="modal-footer">
              <button type="button" class="btn btn-light" >Close</button>
              <button type="submit" @click="saveHalfYear"  class="btn btn-primary">Save changes</button>
            </div>


            <!-- quaterly -->
            <div class="modal-header">
                <h4 class="modal-title">
                    <i class="fas fa-plus"></i> Quaterly
                </h4>
            </div>
            <div class="modal-body">
                <div class="box-body">
                  <div class='form-group'>
                    <label for="monthly">Quaterly</label>
                    <div class='input-group date monthly'>
                      <ejs-datepicker @change='onChange($event)' width="500" format='MM-dd-yyyy' placeholder='Enter date' v-model="dayquater" :value="dayquater"></ejs-datepicker>
                    </div>
                  </div>
                  <div class="form-group">
                    <label for="target">Woking Plan</label>
                    <textarea v-model="workquaterly" class="form-control target" id="target" rows="3" ></textarea>
                  </div>

                  <div class="form-group">
                    <ejs-radiobutton label='Public' @change="CheckQuaterPublic($event)" :checked="quaterpublic"></ejs-radiobutton>

                  </div>
                  <div class="form-group">
                    <ejs-radiobutton label='Private' @change="CheckPrivateQ($event)"  :checked="!quaterpublic"></ejs-radiobutton>

                  </div>
              </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-light" >Close</button>
                <button type="submit" @click="saveQuaterly"  class="btn btn-primary">Save changes</button>
            </div>

            <!-- yearly -->
            <div class="modal-header">
                <h4 class="modal-title">
                    <i class="fas fa-plus"></i> Yearly
                </h4>
            </div>
            <div class="modal-body">
              <div class="box-body">
                <div class='form-group'>
                    <label for="monthly">Yearly</label>
                    <div class='input-group date monthly'>
                        <ejs-datepicker @change='onChange($event)' width="500" format='MM-dd-yyyy' placeholder='Enter date' v-model="dayyear" :value="dayyear"></ejs-datepicker>
                    </div>
                </div>
                <div class="form-group">
                    <label for="target">Woking Plan</label>
                    <textarea v-model="workyearly" class="form-control target" id="target" rows="3" ></textarea>

                </div>

                <div class="form-group">
                  <ejs-radiobutton label='Public' @change="CheckYearPublic($event)"  :checked="yearpublic"></ejs-radiobutton>

                </div>
                <div class="form-group">
                  <ejs-radiobutton label='Private' @change="CheckPrivateY($event)"  :checked="!yearpublic"></ejs-radiobutton>

              </div>
            </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-light" >Close</button>
                <button type="submit" @click="saveYearly"  class="btn btn-primary">Save changes</button>
            </div>

          </b-modal>

          <b-modal
            ref="conversion"
            size="lg"
            hide-header-close
            hide-header
            hide-footer
          >
            <div class="modal-header" style="background-color:#00a65a;color:#fff">
              <h4 class="modal-title">
                <span class="RemarkChart">
                  Conversion Perfomance Data
                </span>
              </h4>
            </div>
            <template v-slot:modal-title> </template>
            <div>
            <ejs-grid
                ref="grid"
                :gridLines="lines"
                :allowTextWrap="true"
                :allowResizing="true"
                :actionBegin="actionBegin"
                :searchSettings="searchSettings"
                :editSettings="editSettings"
                :dataSource="dataConversion"
                :toolbar="toolbarOptions"
                :allowPaging="true"
                :pageSettings="pageSettings"
              >
                <e-columns>
                  <e-column field="Period" :editTemplate="PeriodEditTemplate" headerText="Period" width="100" textAlign="Center"></e-column>
                  <e-column field="Target" :editTemplate="TargetEditTemplate" headerText="Target"  textAlign="Center"></e-column>
                  <e-column field="Value" :editTemplate="ValueEditTemplate" headerText="Perfomance Data"  textAlign="Center"></e-column>
                  <e-column field="Score" headerText="Score for perfomance"   textAlign="Center"></e-column>
                </e-columns>
              </ejs-grid>
            </div>
          <div class="modal-footer">

          </div>
        </b-modal>

      </div>
    </div>
  </section>
</template>

<script>
import Hierarchy from "../../components/adminOC/Hierarchy";
import listoc from "../../components/adminOC/Modal";
import { HTTP } from '../../http-constants';
import EventBus from "../../utils/EventBus.js";
import VueJwtDecode from 'vue-jwt-decode'
import * as moment from 'moment';
import Paginate from "vuejs-paginate";
import Vue from "vue";
import { enableRipple } from '@syncfusion/ej2-base';
enableRipple(true);
import {
  TreeGridPlugin,
  ContextMenu,
  Sort,
  Filter,
  ExcelExport,
  PdfExport
} from "@syncfusion/ej2-vue-treegrid";
Vue.use(TreeGridPlugin)
import { GridPlugin, Page, Toolbar,Edit,Resize } from "@syncfusion/ej2-vue-grids";
Vue.use(GridPlugin);

export default {

  name: "IndexKpi",

  data() {
    return {
      toolbarOptions: ["Cancel"],
      dataConversion: [],
      lines: "Both",
      initialGridLoad: true,
      TargetEditTemplate: function() {
        return {
          template: Vue.component("TargetEditTemplate", {
            template: `
            <span>
             {{data.Target}}
            </span>
            `,
            data: function() {
              return {
                data: {},
                currentUser: Number(
                  VueJwtDecode.decode(localStorage.getItem("authToken")).nameid
                )
              };
            },
            methods: {}
          })
        };
      },
      ValueEditTemplate: function() {
        return {
          template: Vue.component("ValueEditTemplate", {
            template: `
            <span>
             {{data.Value}}
            </span>
            `,
            data: function() {
              return {
                data: {},
                currentUser: Number(
                  VueJwtDecode.decode(localStorage.getItem("authToken")).nameid
                )
              };
            },
            methods: {}
          })
        };
      },
      PeriodEditTemplate: function() {
        return {
          template: Vue.component("PeriodEditTemplate", {
            template: `
            <span>
             {{data.Period}}
            </span>
            `,
            data: function() {
              return {
                data: {},
                currentUser: Number(
                  VueJwtDecode.decode(localStorage.getItem("authToken")).nameid
                )
              };
            },
            methods: {}
          })
        };
      },
      SttTemplate: function() {
        return {
          template: Vue.component("SttTemplate", {
            template: `
              <span>{{NO(data)}}</span>
            `,
            data: function() {
              return {
                data: {},
                role: localStorage.getItem('Role'),
                userid: VueJwtDecode.decode(localStorage.getItem("authToken")).nameid,
              };
            },
            methods: {
              NO(data){
                return Number(data.index) + 1
              },
              Delete(id){
                EventBus.$emit("delete",id)
              }
            }
          })
        };
      },
      ModifyByTemplate: function() {
        return {
          template: Vue.component("ModifyByTemplate", {
            template: `
             <h6>
             <span v-if="data.Checked" class="badge badge-pill badge-dark"> {{ data.ModifyBy || "#N/A"}}</span>
              <span v-else class="badge badge-pill badge-dark"> {{ "#N/A"}}</span>
             </h6>
            `,
            data: function() {
              return {
                data: {},
              };
            },
            methods: {

            }
          })
        };
      },
      OCTemplate: function() {
        return {
          template: Vue.component("OCTemplate", {
            template: `
            <span><i class="fas fa-home"></i>  {{data.title}}</span>
            `,
            data: function() {
              return {
                data: {}
              };
            },
            methods: {

            }
          })
        };
      },
      IndexTemplate: function() {
        return {
          template: Vue.component("IndexTemplate", {
            template: `<p>{{data.index}}</p>`,
            data: function() {
              return {
                data: {}
              };
            },
            methods: {
            }
          })
        };
      },
      dateValue: '',
      dateHalf: '',
      dayquater: '',
      dayyear: '',
      ShowdataTemplate: function() {
        return {
          template: Vue.component("ShowdataTemplate", {
            template: `
            <button style="font-size: 12px; margin-bottom:-7px;" v-if="data.YearlyChecked || data.QuarterlyChecked || data.MonthlyChecked || data.HalfYearChecked || data.WeeklyChecked == true" @click="modifyi(data)" class="btn-sm btn-info  ">
              <i class="far fa-edit"></i>
            </button>
            <p v-else></p>
            `,
            data: function() {
              return {
                data: {}
              };
            },
            methods: {
              modifyi(data){
                EventBus.$emit("modifyi",data)
              }
            }
          })
        };
      },
      updateTemplate: function() {
        return {
          template: Vue.component("updateTemplate", {
            template: `<ejs-switch ref="wswitch"  v-model="data.Checked"  @change="update(data)" :checked="data.Checked"></ejs-switch>`,
            data: function() {
              return {
                data: {}
              };
            },
            methods: {
              update(data){
                EventBus.$emit("update",data)
              }
            }
          })
        };
      },
      ConversionTemplate: function() {
        return {
          template: Vue.component("ConversionTemplate", {
            template: `
            <button style="font-size: 12px; margin-bottom:-7px;" @click="conversion(data)" class="btn-sm btn-info  ">
              <i class="far fa-edit"></i>
            </button>
            `,
            data: function() {
              return {
                data: {}
              };
            },
            methods: {
              conversion(data){
                EventBus.$emit("conversion",data)
              }
            }
          })
        };
      },
      WeeklyTemplate: function() {
        return {
          template: Vue.component("WeeklyTemplate", {
            template: `<ejs-switch ref="wswitch"  v-model="data.WeeklyChecked"  @change="updateweekly(data)" :checked="data.WeeklyChecked"></ejs-switch>`,
            data: function() {
              return {
                data: {}
              };
            },
            methods: {
              updateweekly(data){
                EventBus.$emit("updateweekly",data)
              }
            }
          })
        };
      },
      MonthlyTemplate: function() {
        return {
          template: Vue.component("MonthlyTemplate", {
            template: `<ejs-switch ref="wswitch"  v-model="data.MonthlyChecked"  @change="updatemonthly(data)" :checked="data.MonthlyChecked"></ejs-switch>`,
            data: function() {
              return {
                data: {}
              };
            },
            methods: {
              updatemonthly(data){
                EventBus.$emit("updatemonthly",data)
              }
            }
          })
        };
      },
      HalfYearTemplate: function() {
        return {
          template: Vue.component("HalfYearTemplate", {
            template: `<ejs-switch ref="wswitch"  v-model="data.HalfYearChecked"  @change="updatehalfyear(data)" :checked="data.HalfYearChecked"></ejs-switch>`,
            data: function() {
              return {
                data: {}
              };
            },
            methods: {
              updatehalfyear(data){
                EventBus.$emit("updatehalfyear",data)
              }
            }
          })
        };
      },
      QuaterlyTemplate: function() {
        return {
          template: Vue.component("QuaterlyTemplate", {
            template: `<ejs-switch ref="wswitch"  v-model="data.QuarterlyChecked"  @change="updatequaterly(data)" :checked="data.QuarterlyChecked"></ejs-switch>`,
            data: function() {
              return {
                data: {}
              };
            },
            methods: {
              updatequaterly(data){
                EventBus.$emit("updatequaterly",data)
              }
            }
          })
        };
      },
      YearlyTemplate: function() {
        return {
          template: Vue.component("YearlyTemplate", {
            template: `<ejs-switch ref="wswitch"  v-model="data.YearlyChecked"  @change="updateyearly(data)" :checked="data.YearlyChecked"></ejs-switch>`,
            data: function() {
              return {
                data: {}
              };
            },
            methods: {
              updateyearly(data){
                EventBus.$emit("updateyearly",data)
              }
            }
          })
        };
      },
      searchSettings:  { hierarchyMode: 'Parent' },
      editparams:  { params: { format: 'n' } },
      contextMenuItems:  [
        {
          text: 'Add Sub OC',
          iconCss: ' e-icons e-edit',
          target: '.e-content',
          id: 'Add-Child-OC'
        },
        {
          text: 'Delete',
          iconCss: ' e-icons e-delete',
          target: '.e-content',
          id: 'DeleteOC'
        }
      ],
      toolbar:  [
        'ExpandAll',
        'CollapseAll',
      ],
      levelid:  0,
      ocId:  0,
      category:  0,
      title: '',
      isHide:  false,
      workweekly: '',
      workmonthly: '',
      workhalfyear: '',
      workquaterly: '',
      workyearly: '',

      weekpublic:  '',
      monthpublic:  '',
      halfpublic:  '',
      quaterpublic:  '',
      yearpublic:  '',
      weekpublic1:  '',
      monthpublic1:  '',
      halfpublic1:  '',
      quaterpublic1:  '',
      yearpublic1:  '',

      weekprivate: '',
      monthprivate: '',
      halfprivate: '',
      quaterprivate: '',
      yearprivate: '',

      ID: '',
      KPIID: '',
      LevelID: '',
      dayweekly: '',
      toolbarOptions : ['Cancel'],
      editSettings:  { allowDeleting: false, allowEditing: true, mode: 'Normal',allowEditOnDblClick: true },
      pageSettings:  { pageSizes: true,  pageSize: 20 },
      pageSettings2:  { pageSizes: true,  pageSize: 10 },
      expanded:  {},
      data: [],
      data2: [],
      page:  1,
      pageSize:  1000,
      events: [],
      locale: $cookies.get("Lang")
    };
  },

  components: {
    listoc,
    Hierarchy,
    Paginate
  },

  provide: {
    treegrid: [
      ContextMenu,
      Sort,
      ExcelExport,
      PdfExport,
      Page,
      Filter,
      Resize
    ],
    grid: [Page,Edit,Toolbar]
  },

  mounted(){
    EventBus.$on("update", this.update);
    EventBus.$on("updateweekly", this.updateweekly);
    EventBus.$on("updatemonthly", this.updatemonthly);
    EventBus.$on("updatehalfyear", this.updatehalfyear);
    EventBus.$on("updatequaterly", this.updatequaterly);
    EventBus.$on("updateyearly", this.updateyearly);
    EventBus.$on("modifyi", this.modify);
    EventBus.$on("conversion", this.conversion);
  },

  destroyed() {
    EventBus.$off("update",this.update);
    EventBus.$off("conversion",this.conversion);
    EventBus.$off("updateweekly", this.updateweekly);
    EventBus.$off("updatemonthly", this.updatemonthly);
    EventBus.$off("updatehalfyear", this.updatehalfyear);
    EventBus.$off("updatequaterly", this.updatequaterly);
    EventBus.$off("updateyearly", this.updateyearly);
    EventBus.$off("modifyi", this.modify);
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
    // seft.loadAll();
    seft.getOc();
    $('#datepicker').datepicker({
      autoclose: true,
      uiLibrary: 'bootstrap4',
      format: 'mm-dd-yyyy'
    });

  },
  methods: {

    conversion(data) {
      // const mObj = {
      //   ID : data.ID,
      //   KPIID: data.KPIID,
      //   KPILevelCode: data.KPILevelCode,
      //   IsConversion: data.IsConversion,
      // }
      // if(mObj.IsConversion == true){
      //   this.$refs['conversion'].show()
      //   // this.loadDetail(mObj.ID)
      //   console.log('tao show len roi ne ^^');
      // }
      this.$refs['conversion'].show()
      axios.get(`AdminKPILevel/LoadDataPerfomance/${data.KPILevelCode}`).then(res=>{
        this.dataConversion = res.data.data
        // this.loadDataKPILevel();
      })
    },

    actionBegin(args) {
      if (args.requestType === "save") {
        const mObj = {
          ID : args.data.ID,
          KPILevelCode: args.data.KPILevelCode,
          Score: args.data.Score,
        }
        axios.post('AdminKPILevel/UpdateScoreData',JSON.stringify(mObj))
        .then(res=>{
          success("successfully");
        })
      }
    },

    dataBound: function() {
      if (this.initialGridLoad) {
          this.initialGridLoad = false;
          let pager = document.getElementsByClassName('e-gridpager');
          let topElement;
        if (this.$refs.grid.allowGrouping || this.$refs.grid.toolbar) {
            topElement = this.$refs.grid.allowGrouping ? document.getElementsByClassName('e-groupdroparea') :
              document.getElementsByClassName('e-toolbar');
        } else {
            topElement = document.getElementsByClassName('e-gridheader');
        }
        this.$refs.grid.$el.insertBefore(pager[0], topElement[0]);
      }
    },

    modify(data){
      this.$refs['showdata'].show()
      this.loadDetail(data.ID)
    },

    onChange(args){
      this.dateValue =  moment(String(args.value)).format('MM-DD-YYYY')
      this.dateHalf =  moment(String(args.value)).format('MM-DD-YYYY')
      this.dayquater = moment(String(args.value)).format('MM-DD-YYYY')
      this.dayyear = moment(String(args.value)).format('MM-DD-YYYY')
    },

    saveWeekly(){
      const mObj = {
        ID : this.ID,
        KPIID: this.KPIID,
        KPILevelCode: '',
        UserCheck: Number(VueJwtDecode.decode(localStorage.getItem("authToken")).nameid),
        Checked: null,
         ModifyBy: localStorage.getItem('User'),
        WeeklyChecked: null,
        MonthlyChecked: null,
        HalfYearChecked: null,
        QuarterlyChecked: null,
        YearlyChecked: null,
        Weekly: this.dayweekly,
        Monthly: null,
        HalfYear: null,
        Quarterly: null,
        Yearly: null,
        TimeCheck: new Date(),
        LevelID: this.LevelID,
        WeeklyPublic: this.weekpublic,
        MonthlyPublic: null,
        HalfYearPublic: null,
        QuarterlyPublic: null,
        YearlyPublic: null,
        Target: this.workweekly,
        Period:'W'
      }
      axios.post('AdminKPILevel/UpdateKPILevel',JSON.stringify(mObj))
      .then(res=>{
        success("Add Weekly successfully");
        this.$refs['weekly'].hide()
      })
    },

    saveMonthly(){
      const mObj = {
        ID : this.ID,
        KPIID: this.KPIID,
        KPILevelCode: '',
        UserCheck: Number(VueJwtDecode.decode(localStorage.getItem("authToken")).nameid),
        Checked: null,
        ModifyBy: localStorage.getItem('User'),
        WeeklyChecked: null,
        MonthlyChecked: null,
        HalfYearChecked: null,
        QuarterlyChecked: null,
        YearlyChecked: null,
        Weekly: null,
        Monthly: this.dateValue,
        HalfYear: null,
        Quarterly: null,
        Yearly: null,
        TimeCheck: new Date(),
        LevelID: this.LevelID,
        WeeklyPublic: null,
        MonthlyPublic: this.monthpublic,
        HalfYearPublic: null,
        QuarterlyPublic: null,
        YearlyPublic: null,
        Target: this.workmonthly,
        Period:'M'
      }
      axios.post('AdminKPILevel/UpdateKPILevel',JSON.stringify(mObj))
      .then(res=>{
        success("Add Monthly successfully");
        this.$refs['monthly'].hide()
      })
    },

    saveHalfYear(){
      const mObj = {
        ID : this.ID,
        KPIID: this.KPIID,
        KPILevelCode: '',
        UserCheck: Number(VueJwtDecode.decode(localStorage.getItem("authToken")).nameid),
        Checked: null,
        ModifyBy: localStorage.getItem('User'),
        WeeklyChecked: null,
        MonthlyChecked: null,
        HalfYearChecked: null,
        QuarterlyChecked: null,
        YearlyChecked: null,
        Weekly: null,
        Monthly: null,
        HalfYear: this.dateHalf,
        Quarterly: null,
        Yearly: null,
        TimeCheck: new Date(),
        LevelID: this.LevelID,
        WeeklyPublic: null,
        MonthlyPublic: null,
        HalfYearPublic: this.halfpublic,
        QuarterlyPublic: null,
        YearlyPublic: null,
        Target: this.workhalfyear,
        Period:'H'
      }
      axios.post('AdminKPILevel/UpdateKPILevel',JSON.stringify(mObj))
      .then(res=>{
        success("Add HalfYear successfully");
        this.$refs['halfyear'].hide()
      })
    },

    saveQuaterly(){
      const mObj = {
        ID : this.ID,
        KPIID: this.KPIID,
        KPILevelCode: '',
        UserCheck: Number(VueJwtDecode.decode(localStorage.getItem("authToken")).nameid),
        Checked: null,
         ModifyBy: localStorage.getItem('User'),
        WeeklyChecked: null,
        MonthlyChecked: null,
        HalfYearChecked: null,
        QuarterlyChecked: null,
        YearlyChecked: null,
        Weekly: null,
        Monthly: null,
        HalfYear: null,
        Quarterly: this.dayquater,
        Yearly: null,
        TimeCheck: new Date(),
        LevelID: this.LevelID,
        WeeklyPublic: null,
        MonthlyPublic: null,
        HalfYearPublic: null,
        QuarterlyPublic: this.quaterpublic,
        YearlyPublic: null,
        Target: this.workquaterly,
        Period:'Q'
      }
      axios.post('AdminKPILevel/UpdateKPILevel',JSON.stringify(mObj))
      .then(res=>{
        success("Add Quaterly successfully");

        this.$refs['quaterly'].hide()
      })
    },

    saveYearly(){
      const mObj = {
        ID : this.ID,
        KPIID: this.KPIID,
        KPILevelCode: '',
        UserCheck: Number(VueJwtDecode.decode(localStorage.getItem("authToken")).nameid),
        Checked: null,
        ModifyBy: localStorage.getItem('User'),
        WeeklyChecked: null,
        MonthlyChecked: null,
        HalfYearChecked: null,
        QuarterlyChecked: null,
        YearlyChecked: null,
        Weekly: null,
        Monthly: null,
        HalfYear: null,
        Quarterly: null,
        Yearly: this.dayyear,
        TimeCheck: new Date(),
        LevelID: this.LevelID,
        WeeklyPublic: null,
        MonthlyPublic: null,
        HalfYearPublic: null,
        QuarterlyPublic: null,
        YearlyPublic: this.yearpublic,
        Target: this.workyearly,
        Period:'Y'
      }
      axios.post('AdminKPILevel/UpdateKPILevel',JSON.stringify(mObj))
      .then(res=>{
        success("Add Yearly successfully");
        this.$refs['yearly'].hide()
      })
    },

    loadDetail(id){
      axios.get(`AdminKPILevel/GetbyID/${id}`)
      .then(res=>{
        this.ID = res.data.data.ID,
        this.KPIID = res.data.data.KPIID,
        this.LevelID = res.data.data.LevelID,

        this.weekpublic = res.data.data.WeeklyPublic,
        this.monthpublic = res.data.data.MonthlyPublic,
        this.halfpublic = res.data.data.HalfYearPublic,
        this.quaterpublic = res.data.data.QuarterlyPublic,
        this.yearpublic = res.data.data.YearlyPublic,

        this.weekpublic1 = res.data.data.WeeklyPublic,
        this.monthpublic1 = res.data.data.MonthlyPublic,
        this.halfpublic1 = res.data.data.HalfYearPublic,
        this.quaterpublic1 = res.data.data.QuarterlyPublic,
        this.yearpublic1 = res.data.data.YearlyPublic,

        this.dayweekly = res.data.data.Weekly,
        this.dateValue = this.convertDateJson(res.data.data.Monthly),
        this.dateHalf = this.convertDateJson(res.data.data.HalfYear),
        this.dayquater = this.convertDateJson(res.data.data.Quarterly),
        this.dayyear = this.convertDateJson(res.data.data.Yearly),

        this.workweekly = res.data.WorkingPlanOfWeekly,
        this.workmonthly = res.data.WorkingPlanOfMonthly,
        this.workhalfyear = res.data.WorkingPlanOfHalfYear,
        this.workquaterly = res.data.WorkingPlanOfQuarterly,
        this.workyearly = res.data.WorkingPlanOfYearly
      })
    },

    CheckWeekPublic($event){
      this.weekpublic = $event.event.isTrusted
      this.weekprivate = !$event.event.isTrusted

    },

    CheckMonthPublic($event){
      this.monthpublic= $event.event.isTrusted
      this.monthprivate = !$event.event.isTrusted

    },

    CheckHalfPublic($event){
      this.halfpublic= $event.event.isTrusted
      this.halfprivate = !$event.event.isTrusted

    },

    CheckQuaterPublic($event){
      this.quaterpublic= $event.event.isTrusted
      this.quaterprivate = !$event.event.isTrusted

    },

    CheckYearPublic($event){
      this.yearpublic= $event.event.isTrusted
      this.yearprivate = !$event.event.isTrusted

    },

    CheckPrivateW($event){
      this.weekpublic = !$event.event.isTrusted
      this.weekprivate = $event.event.isTrusted

    },
    CheckPrivateM($event){
      this.monthpublic = !$event.event.isTrusted
      this.monthprivate = $event.event.isTrusted

    },
    CheckPrivateH($event){
      this.halfpublic = !$event.event.isTrusted
      this.halfprivate = $event.event.isTrusted

    },
    CheckPrivateQ($event){
      this.quaterpublic = !$event.event.isTrusted
      this.quaterprivate = $event.event.isTrusted

    },
    CheckPrivateY($event){
      this.yearpublic = !$event.event.isTrusted
      this.yearprivate = $event.event.isTrusted

    },

    updateweekly(data){
      const mObj = {
        ID : data.ID,
        KPIID: data.KPIID,
        KPILevelCode: '',
        UserCheck: Number(VueJwtDecode.decode(localStorage.getItem("authToken")).nameid),
        Checked: data.Checked,
        ModifyBy: localStorage.getItem('User'),
        WeeklyChecked: data.WeeklyChecked,
        MonthlyChecked: null,
        HalfYearChecked: null,
        QuarterlyChecked: null,
        YearlyChecked: null,
        Weekly: null,
        Monthly: null,
        HalfYear: null,
        Quarterly: null,
        Yearly: null,
        TimeCheck: new Date(),
        LevelID: data.LevelID,
        WeeklyPublic: null,
        MonthlyPublic: null,
        HalfYearPublic: null,
        QuarterlyPublic: null,
        YearlyPublic: null,
        Target: '',
        Period:''
      }
      if(mObj.WeeklyChecked == true){
        this.$refs['weekly'].show()
        this.loadDetail(mObj.ID)
      }
      if(mObj.WeeklyChecked == true){
        axios.post('AdminKPILevel/UpdateKPILevel',JSON.stringify(mObj))
        .then(res=>{
          success("Add successfully");
          this.loadDataKPILevel()
        })
      }
      else{
        axios.post('AdminKPILevel/UpdateKPILevel',JSON.stringify(mObj))
        .then(res=>{
          success("Uncheck successfully");
          this.loadDataKPILevel()
        })
      }
    },

    updatemonthly(data){
      const mObj = {
        ID : data.ID,
        KPIID: data.KPIID,
        KPILevelCode: '',
        UserCheck: Number(VueJwtDecode.decode(localStorage.getItem("authToken")).nameid),
        Checked: data.Checked,
        ModifyBy: localStorage.getItem('User'),
        WeeklyChecked: null,
        MonthlyChecked: data.MonthlyChecked,
        HalfYearChecked: null,
        QuarterlyChecked: null,
        YearlyChecked: null,
        Weekly: null,
        Monthly: null,
        HalfYear: null,
        Quarterly: null,
        Yearly: null,
        TimeCheck: new Date(),
        LevelID: data.LevelID,
        WeeklyPublic: null,
        MonthlyPublic: null,
        HalfYearPublic: null,
        QuarterlyPublic: null,
        YearlyPublic: null,
        Target: '',
        Period:''
      }
      if(mObj.MonthlyChecked == true){
        this.$refs['monthly'].show()
        this.loadDetail(mObj.ID)
      }
      if(mObj.MonthlyChecked == true){
        axios.post('AdminKPILevel/UpdateKPILevel',JSON.stringify(mObj))
        .then(res=>{
          Toast.fire({
            icon: 'success',
            title: 'Add successfully'
          })
          this.loadDataKPILevel()
        })
      }
      else{
        axios.post('AdminKPILevel/UpdateKPILevel',JSON.stringify(mObj))
        .then(res=>{
          Toast.fire({
            icon: 'success',
            title: 'Uncheck successfully'
          })
          this.loadDataKPILevel()
        })
      }
    },
    updatehalfyear(data){
      const mObj = {
        ID : data.ID,
        KPIID: data.KPIID,
        KPILevelCode: '',
        UserCheck: Number(VueJwtDecode.decode(localStorage.getItem("authToken")).nameid),
        Checked: data.Checked,
        ModifyBy: localStorage.getItem('User'),
        WeeklyChecked: null,
        MonthlyChecked: null,
        HalfYearChecked: data.HalfYearChecked,
        QuarterlyChecked: null,
        YearlyChecked: null,
        Weekly: null,
        Monthly: null,
        HalfYear: null,
        Quarterly: null,
        Yearly: null,
        TimeCheck: new Date(),
        LevelID: data.LevelID,
        WeeklyPublic: null,
        MonthlyPublic: null,
        HalfYearPublic: null,
        QuarterlyPublic: null,
        YearlyPublic: null,
        Target: '',
        Period:''
      }
      // if(mObj.HalfYearChecked == true){
      //   this.$refs['halfyear'].show()
      //   this.loadDetail(mObj.ID)
      // }
      if(mObj.HalfYearChecked == true){
        axios.post('AdminKPILevel/UpdateKPILevel',JSON.stringify(mObj))
        .then(res=>{
          Toast.fire({
            icon: 'success',
            title: 'Add successfully'
          })
          this.$refs['halfyear'].show()
          this.loadDetail(mObj.ID)
          this.loadDataKPILevel()
        })
      }
      else{
        axios.post('AdminKPILevel/UpdateKPILevel',JSON.stringify(mObj))
        .then(res=>{
          Toast.fire({
            icon: 'success',
            title: 'Uncheck successfully'
          })
          this.loadDataKPILevel()
        })
      }
    },
    updatequaterly(data){
      const mObj = {
        ID : data.ID,
        KPIID: data.KPIID,
        KPILevelCode: '',
        UserCheck: Number(VueJwtDecode.decode(localStorage.getItem("authToken")).nameid),
        Checked: data.Checked,
        ModifyBy: localStorage.getItem('User'),
        WeeklyChecked: null,
        MonthlyChecked: null,
        HalfYearChecked: null,
        QuarterlyChecked: data.QuarterlyChecked,
        YearlyChecked: null,
        Weekly: null,
        Monthly: null,
        HalfYear: null,
        Quarterly: null,
        Yearly: null,
        TimeCheck: new Date(),
        LevelID: data.LevelID,
        WeeklyPublic: null,
        MonthlyPublic: null,
        HalfYearPublic: null,
        QuarterlyPublic: null,
        YearlyPublic: null,
        Target: '',
        Period:''
      }
      if(mObj.QuarterlyChecked == true){
        this.$refs['quaterly'].show()
        this.loadDetail(mObj.ID)
      }
      if(mObj.QuarterlyChecked == true){
        axios.post('AdminKPILevel/UpdateKPILevel',JSON.stringify(mObj))
        .then(res=>{
          Toast.fire({
            icon: 'success',
            title: 'Add successfully'
          })
          this.loadDataKPILevel()
        })
      }
      else{
        axios.post('AdminKPILevel/UpdateKPILevel',JSON.stringify(mObj))
        .then(res=>{
          Toast.fire({
            icon: 'success',
            title: 'Uncheck successfully'
          })
          this.loadDataKPILevel()
        })
      }
    },
    updateyearly(data){
      const mObj = {
        ID : data.ID,
        KPIID: data.KPIID,
        KPILevelCode: '',
        UserCheck: Number(VueJwtDecode.decode(localStorage.getItem("authToken")).nameid),
        Checked: data.Checked,
        ModifyBy: localStorage.getItem('User'),
        WeeklyChecked: null,
        MonthlyChecked: null,
        HalfYearChecked: null,
        QuarterlyChecked: null,
        YearlyChecked: data.YearlyChecked,
        Weekly: null,
        Monthly: null,
        HalfYear: null,
        Quarterly: null,
        Yearly: null,
        TimeCheck: new Date(),
        LevelID: data.LevelID,
        WeeklyPublic: null,
        MonthlyPublic: null,
        HalfYearPublic: null,
        QuarterlyPublic: null,
        YearlyPublic: null,
        Target: '',
        Period:''
      }
      if(mObj.YearlyChecked == true){
        this.$refs['yearly'].show()
        this.loadDetail(mObj.ID)
      }
      if(mObj.YearlyChecked == true){
        axios.post('AdminKPILevel/UpdateKPILevel',JSON.stringify(mObj))
        .then(res=>{
          Toast.fire({
            icon: 'success',
            title: 'Add successfully'
          })
          this.loadDataKPILevel()
        })
      }
      else{
        axios.post('AdminKPILevel/UpdateKPILevel',JSON.stringify(mObj))
        .then(res=>{
          Toast.fire({
            icon: 'success',
            title: 'Uncheck successfully'
          })
          this.loadDataKPILevel()
        })
      }
    },

    update(data){
      const mObj = {
        ID : data.ID,
        KPIID: data.KPIID,
        KPILevelCode: '',
        UserCheck: Number(VueJwtDecode.decode(localStorage.getItem("authToken")).nameid),
        Checked: data.Checked,
        WeeklyChecked: null,
        MonthlyChecked: null,
        HalfYearChecked: null,
        QuarterlyChecked: null,
        YearlyChecked: null,
        Weekly: null,
        Monthly: null,
        HalfYear: null,
        Quarterly: null,
        Yearly: null,
        TimeCheck: null,
        LevelID: data.LevelID,
        WeeklyPublic: null,
        MonthlyPublic: null,
        HalfYearPublic: null,
        QuarterlyPublic: null,
        YearlyPublic: null,
        Target: '',
        Period:'',
        ModifyBy: localStorage.getItem('User')
      }
      if(!mObj.Checked == false){
        axios.post('AdminKPILevel/UpdateKPILevel',JSON.stringify(mObj))
        .then(res=>{
          success("Add successfully");
          this.loadDataKPILevel()
        })
      }
      else{
        axios.post('AdminKPILevel/UpdateKPILevel',JSON.stringify(mObj))
        .then(res=>{
          success("Uncheck successfully");
          this.loadDataKPILevel()
        })
      }
    },

    getOc(){
      axios.get("KPI/GetListTreeClient/" + VueJwtDecode.decode(localStorage.getItem("authToken")).nameid).then(res=>{
        this.data = res.data
      })
      // axios.get('AdminKPILevel/GetListTree').then(res=>{
      //   this.data = res.data
      // })
    },
    actionComplete(args){
    },
    rowSelected(args){
      this.title = '- ' + args.data.title
      this.levelid = args.data.key
      this.ocId = args.data.key
      this.loadDataKPILevel()
      this.isHide = true
    },
    rowSelected2(args){
    },
    contextMenuClick(args){
    },
    loadDataKPILevel(){
      axios.get(`AdminKPILevel/LoadDataKPILevel2/${this.levelid}/${this.category}/${this.locale}/${this.page}/${this.pageSize}`)
        .then(res=>{
        this.data2 = res.data.data
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
