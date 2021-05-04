<template>
<section class="content">
  <div class="container-fluid">
    <div class="row">

      <div class="col-md-12">
        <div class="callout bg-yellow-gradient" style="border-color:#c57901">
          <h3>6. OC Category KPI</h3>
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
            <!-- /.card-header -->
            <div class="card-body table-responsive p-0">
              <ejs-treegrid
                ref="treegrid"
                height='550px'
                :dataSource="data"
                childMapping="children"
                :treeColumnIndex="1"
                :allowPaging="true"
                :pageSettings="pageSettings"
                :allowExcelExport="true"
                :allowPdfExport="true"
                :allowSorting="true"
                :contextMenuItems="contextMenuItems"
                :contextMenuClick="contextMenuClick"
                :editSettings="editing"
                :rowSelected="rowSelected"
                :actionComplete="actionComplete"
                :searchSettings="searchSettings"
                :toolbar="toolbar" >
                  <e-columns>
                      <e-column
                          field="levelnumber"
                          width="50"
                          isPrimaryKey="true"
                          headerText="Level"
                          :disableHtmlEncode="false">
                      </e-column>
                      <e-column
                          field="title"
                          editType="stringedit"
                          headerText="OC Name"
                          :edit="editparams"
                          :template="OCTemplate"
                          :disableHtmlEncode="false">
                      </e-column>
                  </e-columns>
              </ejs-treegrid>
            </div>
          </div>

      </div>
      <div class="col-md-8">
        <div class="card">
          <div class="card-header">
              <h5 class="card-title">Category lists {{title}}</h5>
              <div class="card-tools">
                  <button type="button" class="btn btn-tool" data-card-widget="collapse">
                      <i class="fas fa-minus"></i>
                  </button>
              </div>
          </div>
          <div v-if="isHide" class="card-body table-responsive p-0">
            <ejs-grid  ref="grid" :searchSettings="searchSettings" :actionComplete="actionComplete" :rowSelected="rowSelected2"  :toolbar='toolbarOptions' :dataSource='data2' allowPaging='true' :pageSettings='pageSettings2' >
              <e-columns>
                  <e-column field='ID' headerText='#' width='80' textAlign='Left' isPrimaryKey='true'></e-column>
                  <e-column field='Name' headerText='Category Name' width='150' textAlign='Left' >
                  </e-column>
                  <e-column field='LevelID' headerText='Level Number' width='120' textAlign='Center' ></e-column>
              </e-columns>
            </ejs-grid>
          </div>
        </div>
        <div v-if="ocHide" class="card">
          <div class="card-header">
            <h5 class="card-title">OC {{titleoc}}</h5>
            <div class="card-tools">
              <button type="button" class="btn btn-tool" data-card-widget="collapse">
                  <i class="fas fa-minus"></i>
              </button>
            </div>
          </div>
          <div class="card-body table-responsive p-0">
              <ejs-grid  ref="grid" :searchSettings="searchSettings" :actionComplete="actionComplete" :rowSelected="rowSelected3"  :toolbar='toolbarOptions' :dataSource='data3' allowPaging='true' :pageSettings='pageSettings2' >
                <e-columns>
                  <e-column field='ID' headerText='#' :template="IndexTemplate"  width='30' textAlign='Left' isPrimaryKey='true'></e-column>
                  <e-column field='' :template="modifyTemplate" headerText='Modify' width='100' textAlign='Center' ></e-column>
                  <e-column field='' :template="ModifyByTemplate" headerText='Modified By' width='160' textAlign='Center' ></e-column>
                  <e-column :template="addMemberTemplate" width='60'></e-column>
                  <e-column field='KPIName' headerText='KPI Organization Chart' width='250' textAlign='Left' ></e-column>
                  <e-column field='' :template="PeriodTemplate" headerText='Period' width='500' textAlign='Center' ></e-column>
                </e-columns>
              </ejs-grid>
          </div>
        </div>


      </div>

      <b-modal ref="member" hide-footer title="Add Member">
        <div class="modal-body">
          <div class="form-group ownerManager">
            <label for="ownerManager">
              {{$t("Manager_label")}}
              <span style="color:red">(*)</span>
            </label>
            <multiselect
              v-model="selectedManager"

              track-by="Username"
              label="FullName"
              placeholder="Select one"
              :options="options"
              :searchable="true"
              :multiple="true"

            ></multiselect>
          </div>
          <div class="form-group Owner">
            <label for="owner">
              {{$t("Owner_label")}}
              <span style="color:red">(*)</span>
            </label>
              <multiselect
              v-model="selectedOwner"

              track-by="Username"
              label="FullName"
              placeholder="Select one"
              :options="options"
              :searchable="true"
              :multiple="true"

            ></multiselect>
          </div>
          <div class="form-group PIC">
            <label for="PIC">
              {{$t("Updater_label")}}
              <span style="color:red">(*)</span>
            </label>
            <multiselect
              v-model="selectedUpdater"

              track-by="Username"
              label="FullName"
              placeholder="Select one"
              :options="options"
              :searchable="true"
              :multiple="true"

            ></multiselect>
          </div>

          <div class="form-group Sponsor">
            <label for="Sponsor">{{$t("Sponsor_label")}}</label>
            <multiselect
              v-model="selectedSponsor"

              track-by="Username"
              label="FullName"
              placeholder="Select one"
              :options="options"
              :searchable="true"
              :multiple="true"

            ></multiselect>
          </div>
          <div class="form-group Participant">
            <label for="Participant">{{$t("Participant_label")}}</label>

            <multiselect
              v-model="selectedParticipant"

              track-by="Username"
              label="FullName"
              placeholder="Select one"
              :options="options"
              :searchable="true"
              :multiple="true"

            ></multiselect>
          </div>
        </div>
        <div class="modal-footer">
            <button type="button" class="btn btn-light" @click="closemodal" >Close</button>
            <button type="submit" @click="saveMember"  class="btn btn-primary">Save changes</button>
        </div>
      </b-modal>

      <b-modal ref="showdata" hide-footer title="Add Member">
        <div class="modal-body">
          <div class="form-group ownerManager">
            <label for="ownerManager">
              {{$t("Manager_label")}}
              <span style="color:red">(*)</span>
            </label>
            <multiselect
              v-model="selectedManager"

              track-by="Username"
              label="FullName"
              placeholder="Select one"
              :options="options"
              :searchable="true"
              :multiple="true"

            ></multiselect>
          </div>
          <div class="form-group Owner">
            <label for="owner">
              {{$t("Owner_label")}}
              <span style="color:red">(*)</span>
            </label>
              <multiselect
              v-model="selectedOwner"

              track-by="Username"
              label="FullName"
              placeholder="Select one"
              :options="options"
              :searchable="true"
              :multiple="true"

            ></multiselect>
          </div>
          <div class="form-group PIC">
            <label for="PIC">
              {{$t("Updater_label")}}
              <span style="color:red">(*)</span>
            </label>
            <multiselect
              v-model="selectedUpdater"

              track-by="Username"
              label="FullName"
              placeholder="Select one"
              :options="options"
              :searchable="true"
              :multiple="true"

            ></multiselect>
          </div>

          <div class="form-group Sponsor">
            <label for="Sponsor">{{$t("Sponsor_label")}}</label>
            <multiselect
              v-model="selectedSponsor"

              track-by="Username"
              label="FullName"
              placeholder="Select one"
              :options="options"
              :searchable="true"
              :multiple="true"

            ></multiselect>
          </div>
          <div class="form-group Participant">
            <label for="Participant">{{$t("Participant_label")}}</label>

            <multiselect
              v-model="selectedParticipant"

              track-by="Username"
              label="FullName"
              placeholder="Select one"
              :options="options"
              :searchable="true"
              :multiple="true"

            ></multiselect>
          </div>
        </div>
        <div class="modal-footer">
            <button type="button" class="btn btn-light" @click="closemodal1" >Close</button>
            <button type="submit" @click="saveMember"  class="btn btn-primary">Save changes</button>
        </div>
      </b-modal>

    </div>
  </div>
</section>
</template>
<script>

import Hierarchy from "../../components/adminOC/Hierarchy";
import listoc from "../../components/AdminOCCategoryKpi/Modal";
import Paginate from "vuejs-paginate";
import Multiselect from "vue-multiselect";
import { HTTP } from '../../http-constants';
import VueJwtDecode from 'vue-jwt-decode'
import EventBus from "../../utils/EventBus.js";
import Vue from "vue";
import { enableRipple } from '@syncfusion/ej2-base';
enableRipple(true);
import {
  TreeGridPlugin,
  ContextMenu,
  Sort,
  Filter,
  Edit,
  ExcelExport,
  PdfExport,
  Page,
  Resize,
  Toolbar
} from "@syncfusion/ej2-vue-treegrid";
Vue.use(TreeGridPlugin)
export default {
  name: "IndexKpi",
  data() {
    return {
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
      PeriodTemplate: function() {
        return {
          template: Vue.component("PeriodTemplate", {
            template: `
            <div>
              <button v-if="data.WeeklyChecked === true && (data.StatusUploadDataW === true && data.StatusEmptyDataW === true)" :data-kpilevelcode="data.KPILevelCode" data-period="W" :class='data.WeeklyChecked === true ? (data.StatusUploadDataW === true ? "btn btn-sm btn-success week" : (data.StatusEmptyDataW === true ? "btn btn-sm btn-warning week" : "btn btn-sm btn-success week")) : "btn btn-sm bg-navy week"'   ><i class="fas fa-chart-bar"></i> Weekly</button>
              <button v-else-if="data.WeeklyChecked === true && data.StatusUploadDataW === false " :data-kpilevelcode="data.KPILevelCode" data-period="W" :class='data.WeeklyChecked === true ? (data.StatusUploadDataW === true ? "btn btn-sm btn-success week" : (data.StatusEmptyDataW === true ? "btn btn-sm btn-warning week" : "btn btn-sm btn-success week")) : "btn btn-sm bg-navy week"'   ><i class="fas fa-chart-bar"></i> Weekly</button>
              <button v-else :data-kpilevelcode="data.KPILevelCode" data-period="W"  disabled class="btn btn-sm bg-navy  week" ><i class="fas fa-chart-bar"></i> Weekly</button>

              <button v-if="data.MonthlyChecked === true && (data.StatusUploadDataM === true && data.StatusEmptyDataM === true)" :data-kpilevelcode="data.KPILevelCode" data-period="M" :class='data.MonthlyChecked === true ? (data.StatusUploadDataM === true ? "btn btn-sm btn-success month" : (data.StatusEmptyDataM === true ? "btn btn-sm btn-warning month" : "btn btn-sm btn-success month")) : "btn btn-sm bg-navy month"' ><i class="fas fa-chart-bar"></i> Monthly</button>
              <button v-else-if="data.MonthlyChecked === true && data.StatusUploadDataM === false " :data-kpilevelcode="data.KPILevelCode" data-period="M"  :class='data.MonthlyChecked === true ? (data.StatusUploadDataM === true ? "btn btn-sm btn-success month" : (data.StatusEmptyDataM === true ? "btn btn-sm btn-warning month" : "btn btn-sm btn-success month")) : "btn btn-sm bg-navy month"' ><i class="fas fa-chart-bar"></i> Monthly</button>
              <button v-else :data-kpilevelcode="data.KPILevelCode" data-period="M" disabled   class="btn btn-sm bg-navy month"><i class="fas fa-chart-bar"></i> Monthly</button>

              <button v-if="data.HalfYearChecked === true && (data.StatusUploadDataH === true && data.StatusEmptyDataH === true)" :data-kpilevelcode="data.KPILevelCode" data-period="H" :class='data.HalfYearChecked === true ? (data.StatusUploadDataH === true ? "btn btn-sm btn-success half" : (data.StatusEmptyDataH === true ? "btn btn-sm btn-warning half" : "btn btn-sm btn-success half")) : "btn btn-sm bg-navy half"' ><i class="fas fa-chart-bar"></i> Half Year</button>
              <button v-else-if="data.HalfYearChecked === true && data.StatusUploadDataH === false " :data-kpilevelcode="data.KPILevelCode" data-period="H"  :class='data.HalfYearChecked === true ? (data.StatusUploadDataH === true ? "btn btn-sm btn-success half" : (data.StatusEmptyDataH === true ? "btn btn-sm btn-warning half" : "btn btn-sm btn-success half")) : "btn btn-sm bg-navy half"' ><i class="fas fa-chart-bar"></i> Half Year</button>
              <button v-else :data-kpilevelcode="data.KPILevelCode" data-period="H" disabled   class="btn btn-sm bg-navy half"><i class="fas fa-chart-bar"></i> Half Year</button>

              <button v-if="data.QuarterlyChecked === true && (data.StatusUploadDataQ === true && data.StatusEmptyDataQ === true)" :data-kpilevelcode="data.KPILevelCode" data-period="Q" :class='data.QuaterlyChecked === true ? (data.StatusUploadDataQ === true ? "btn btn-sm btn-success quater" : (data.StatusEmptyDataQ === true ? "btn btn-sm btn-warning quarter" : "btn btn-sm btn-success quarter")) : "btn btn-sm bg-navy quarter"' ><i class="fas fa-chart-bar"></i> Quarterly</button>
              <button v-else-if="data.QuarterlyChecked === true && data.StatusUploadDataQ === false " :data-kpilevelcode="data.KPILevelCode" data-period="Q"  :class='data.QuarterlyChecked === true ? (data.StatusUploadDataQ === true ? "btn btn-sm btn-success quater" : (data.StatusEmptyDataQ === true ? "btn btn-sm btn-warning quater" : "btn btn-sm btn-success quarter")) : "btn btn-sm bg-navy quarter"' ><i class="fas fa-chart-bar"></i> Quarterly</button>
              <button v-else :data-kpilevelcode="data.KPILevelCode" data-period="Q" disabled   class="btn btn-sm bg-navy quarter"><i class="fas fa-chart-bar"></i> Quarterly</button>

              <button v-if="data.YearlyChecked === true && (data.StatusUploadDataY === true && data.StatusEmptyDataY=== true)" :data-kpilevelcode="data.KPILevelCode" data-period="Y" :class='data.YearlyChecked === true ? (data.StatusUploadDataY === true ? "btn btn-sm btn-success year" : (data.StatusEmptyDataY === true ? "btn btn-sm btn-warning year" : "btn btn-sm btn-success year")) : "btn btn-sm bg-navy year"' ><i class="fas fa-chart-bar"></i> Yearly</button>
              <button v-else-if="data.YearlyChecked === true && data.StatusUploadDataY === false " :data-kpilevelcode="data.KPILevelCode" data-period="Y"  :class='data.YearlyChecked === true ? (data.StatusUploadDataY === true ? "btn btn-sm btn-success year" : (data.StatusEmptyDataY === true ? "btn btn-sm btn-warning year" : "btn btn-sm btn-success year")) : "btn btn-sm bg-navy year"' ><i class="fas fa-chart-bar"></i> Yearly</button>
              <button v-else :data-kpilevelcode="data.KPILevelCode" data-period="Y" disabled   class="btn btn-sm bg-navy year"><i class="fas fa-chart-bar"></i> Yearly</button>
            </div>`,
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
      modifyTemplate: function() {
        return {
          template: Vue.component("modifyTemplate", {
            template: `
              <button v-if="data.CheckCategory == true" @click="modify(data)" class="btn-sm btn-info  ">
                <i class="far fa-edit"></i>
              </button>
              <p v-else></p>`,
            data: function() {
              return {
                data: {}
              };
            },
            methods: {
              modify(data){
                EventBus.$emit("modify",data)
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
             <span v-if="data.CheckCategory" class="badge badge-pill badge-dark"> {{ data.ModifyBy || "#N/A"}}</span>
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
              modify(data){
                EventBus.$emit("modify",data)
              }
            }
          })
        };
      },
      addMemberTemplate: function() {
        return {
          template: Vue.component("addMemberTemplate", {
            template: `<ejs-switch ref="wswitch"  @change="addMember(data)" :checked="data.CheckCategory"></ejs-switch>`,
            data: function() {
              return {
                data: {}
              };
            },
            methods: {
              addMember(data){
                EventBus.$emit("addMember",data)
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
      toolbarOptions: ['Search'],
      editing:  { allowDeleting: false, allowEditing: false, mode: 'Row' },
      pageSettings:  { pageSize: 100 },
      pageSettings2:  { pageSize: 5 },
      expanded:  {},
      data:  [],
      data2: [],
      data3: [],
      levelid:  0,
      ocId:  0,
      category: '',
      title: '',
      titleoc: '',
      isHide:  false,
      ocHide:  false,
      events: [],
      dataKPILV:[],
      totalPage: 0,
      page: 1,
      skip: 0,
      pageSize: 100,
      totalPage2: 0,
      page2: 1,
      skip: 0,
      selectedManager: [],
      selectedOwner: '',
      selectedUpdater: '',
      selectedSponsor: '',
      selectedParticipant: '',
      pageSize2: 100,
      locale : $cookies.get("Lang"),
      options: [],
      entity : {
        kpilevel: 0,
        category: 0,
        pic: '',
        owner: '',
        manager: '',
        sponsor: '',
        participant: ''
      }
      // test: "A"
    };
  },
  provide: {
    treegrid: [
      ContextMenu,
      Sort,
      Edit,
      ExcelExport,
      PdfExport,
      Page,
      Filter,
      Resize,
      Toolbar
    ]
  },
  components: {
    listoc,
    Hierarchy,
    Paginate,
    Multiselect
  },
  created() {
    let seft = this;
    EventBus.$on('flag', locale =>{
      seft.locale = locale
    });
    seft.getOc()
  },
  mounted(){
    EventBus.$on("addMember", this.addMember);
    EventBus.$on("modify", this.modify);
  },
  destroyed(){
    EventBus.$off("addMember", this.addMember);
    EventBus.$off("modify", this.modify);
  },
  watch: {
    locale: function(newOld,oldVal){
      this.locale = newOld
      this.loadDataKPILevel()
      this.getCategoryByOC()
    },
    selectedManager: function(newVal, oldVal) {
      var self = this;
      let managers = newVal;
      self.entity.manager = managers.map((manager, index, managers) => {
        return manager.Username;
      }).join(',')
    },
    selectedOwner: function(newVal, oldVal) {
      var self = this;
      let owners = newVal;
      self.entity.owner = owners.map((owner, index, owners) => {
        return owner.Username;
      }).join(',')
    },
    selectedUpdater: function(newVal, oldVal) {
      var self = this;
      let pics = newVal;
      self.entity.pic = pics.map((pic, index, pics) => {
        return pic.Username;
      }).join(',')
    },
    selectedSponsor: function(newVal, oldVal) {
      var self = this;
      let sponsors = newVal;
      self.entity.sponsor = sponsors.map((sponsor, index, sponsors) => {
        return sponsor.Username;
      }).join(',')
    },
    selectedParticipant: function(newVal, oldVal) {
      var self = this;
      let participants = newVal;
      self.entity.participant = participants.map((participant, index, participants) => {
        return participant.Username;
      }).join(',')
    },
  },
  methods: {
    closemodal1(){
      this.$refs['showdata'].hide()
    },
    closemodal(){
      this.$refs['member'].hide()
    },
    modify(data){
      this.kpilevelid = data.ID
      this.$refs['showdata'].show()
      this.getUserByCategoryIDAndKPILevelID()
      this.getAllUser()
    },
    getUserByCategoryIDAndKPILevelID(){
      axios.get(`CategoryKPILevelAdmin/GetUserByCategoryIDAndKPILevelID/${this.kpilevelid}/${this.category}`)
      .then(res=>{
        this.selectedManager = res.data.manager
        this.selectedUpdater = res.data.updater
        this.selectedSponsor = res.data.sponsor
        this.selectedParticipant = res.data.participant
        this.selectedOwner = res.data.owner

      })
    },
    getAllUser() {
      axios.get('AdminKPILevel/GetListAllUser').then(res=>{
        this.options = res.data
      })
    },
    addMember(data){
      this.kpilevelid = data.ID
      const entity = {
        KPILevelID: data.ID,
        CategoryID: this.category,
        ModifyBy: localStorage.getItem('User')
      }
      if(!data.CheckCategory == true){
        axios.post('CategoryKPILevelAdmin/Add', JSON.stringify(entity))
        .then(res=>{
          success("Add successfully");
          this.$refs['member'].show()
          this.getAllUser()
          this.getUserByCategoryIDAndKPILevelID()
          this.loadDataKPILevel()
        })
      }
      else{
        axios.post('CategoryKPILevelAdmin/Add', JSON.stringify(entity))
        .then(res=>{
          success("Uncheck successfully");
          this.loadDataKPILevel()
        })
      }
    },
    saveMember(){
      let entity = {
        kpilevel: this.kpilevelid,
        category: this.category,
        pic: this.entity.pic,
        owner: this.entity.owner,
        manager: this.entity.manager,
        sponsor: this.entity.sponsor,
        participant: this.entity.participant
      }
      axios.post('CategoryKPILevelAdmin/AddGeneral',JSON.stringify(entity))
      .then(res=>{
        success("Add successfully");
        this.$refs['member'].hide()
        this.$refs['showdata'].hide()
        this.loadDataKPILevel()
      })
    },
    actionComplete(args){
    },
    rowSelected(args){
      this.title = '- ' + args.data.title
      this.levelid = args.data.levelnumber
      this.ocId = args.data.key
      this.getCategoryByOC()
      this.isHide = true
      this.ocHide = false
    },
    rowSelected2(args){
      this.category = args.data.ID
      this.titleoc ='- ' + args.data.Name
      this.ocHide = true
      this.loadDataKPILevel()
    },
    rowSelected3(args){
    },
    contextMenuClick(args){
    },
    getOc(){
      axios.get("KPI/GetListTreeClient/" + VueJwtDecode.decode(localStorage.getItem("authToken")).nameid).then(res=>{
        this.data = res.data
      })
    },
    getCategoryByOC(){
      axios.get(`CategoryKPILevelAdmin/GetAllCategories2/${this.levelid}/${this.locale}/${this.ocId}/${this.page}/${this.pageSize}`)
      .then(res=>{
        this.data2 = res.data.data
      })
    },
    loadDataKPILevel(){
      axios.get(`CategoryKPILevelAdmin/LoadDataKPILevel2/${this.ocId}/${this.category}/${this.locale}/${this.page}/${this.pageSize}`)
      .then(res=>{
        this.data3 = res.data.data
      })
    }
  }
};
</script>
<style src="vue-multiselect/dist/vue-multiselect.min.css"></style>
