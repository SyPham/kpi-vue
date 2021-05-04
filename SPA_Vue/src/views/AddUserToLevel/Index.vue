<template>
<section class="content">
    <div class="container-fluid">
      <div class="row">
        <div class="col-md-12">
          <div class="callout bg-yellow-gradient" style="border-color:#c57901">
            <h3>9. Add User To Oc</h3>
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
                    <ejs-button style="display:none" id='clear' @click.native='clear'>Clear Search</ejs-button>
                    <ejs-treegrid
                      ref="grid"
                      :dataSource="data"
                      height='550px'
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
                              width="100"
                              isPrimaryKey="true"
                              headerText="Level"
                              :disableHtmlEncode="false">
                          </e-column>
                          <e-column
                              field="title"
                              editType="stringedit"
                              headerText="OC Name"
                              :edit="editparams"
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
                <h5 class="card-title">User List {{title}}</h5>
                <div class="card-tools">
                    <button type="button" class="btn btn-tool" data-card-widget="collapse">
                        <i class="fas fa-minus"></i>
                    </button>
                </div>
            </div>
            <div v-if="isHide" class="card-body table-responsive p-0">
              <ejs-button style="display:none" id='clear' @click.native='clear'>Clear Search</ejs-button>
              <ejs-grid  ref="grid" :searchSettings="searchSettings" :actionComplete="actionComplete" :rowSelected="rowSelected2"  :toolbar='toolbarOptions' :dataSource='data2' allowPaging='true' :pageSettings='pageSettings2' >
                <e-columns>
                    <e-column field='ID' :template="SttTemplate" headerText='#' width='50' textAlign='Left' isPrimaryKey='true'>
                        <!-- <ng-template #template let-data>
                            {{data.index}}
                        </ng-template> -->
                    </e-column>
                    <e-column :template="addUserTemplate" width='60'>
                    </e-column>
                    <e-column field='Username' headerText='User Name' width='100' textAlign='Left' >
                    </e-column>
                    <e-column field='FullName' headerText='Display Name' width='150' textAlign='Center' ></e-column>
                    <e-column :template="OCNameTemplate" field='OCName' headerText='OC Name' width='130' textAlign='Center' >
                    </e-column>
                    <e-column field='' :template="ModifyByTemplate" headerText='Modify By' width='150' textAlign='Center' >

                    </e-column>
                </e-columns>
              </ejs-grid>
            </div>
          </div>

        </div>
      </div>
    </div>
</section>
</template>

<script>
import { HTTP } from "../../http-constants";
import Hierarchy from "../../components/adminOC/Hierarchy";
import listoc from "../../components/adminOC/Modal";
import Paginate from "vuejs-paginate";
import EventBus from "../../utils/EventBus.js";
import VueJwtDecode from 'vue-jwt-decode'

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
      tipposition : "TopCenter",
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
      OCNameTemplate: function() {
        return {
          template: Vue.component("OCNameTemplate", {
            template: `
              <ejs-tooltip ref="tooltip" position='TopCenter'  content='The account is binding to OC Name' >
                 {{data.OCName}}<i style="color:cornflowerblue;" class="icon fa fa-info-circle danger"></i>
              </ejs-tooltip>
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
      ModifyByTemplate: function() {
        return {
          template: Vue.component("ModifyByTemplate", {
            template: `
             <h6>
             <span v-if="data.Status" class="badge badge-pill badge-light"> {{ data.ModifyBy}}</span>
              <span v-else class="badge badge-pill badge-light"> {{ "#N/A"}}</span>
             </h6>
            `,
            data: function() {
              return {
                data: {},
                modifyBy: localStorage.getItem('User')
              };
            },
            methods: {

            }
          })
        };
      },
      addUserTemplate: function() {
        return {
          template: Vue.component("addUserTemplate", {
            template: `<ejs-switch ref="wswitch"  cssClass='e-success' @change="addUserToLevel(data)" :checked="data.Status"></ejs-switch>`,
            data: function() {
              return {
                data: {}
              };
            },
            methods: {
              addUserToLevel(data){
                EventBus.$emit("addUserToLevel",data)
              }
            }
          })
        };
      },
      searchSettings: { hierarchyMode: 'Parent' },
      editparams: { params: { format: 'n' } },
      contextMenuItems: [
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
      toolbar: [
        'ExpandAll',
        'CollapseAll',
      ],
      toolbarOptions : ['Search'],
      editing: { allowDeleting: false, allowEditing: false, mode: 'Row' },
      pageSettings: { pageSize: 100 },
      pageSettings2: { pageSize: 10 },
      expanded: {},
      data: [],
      data2:[],
      page: 1,
      pageSize: 1000,
      levelid: 0,
      ocId: 0,
      title: '',
      code: ' ',
      isHide: false,
      events: [],
      // test: "A",
      id: 0,
      ID: null,
      modifyBy: null
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
  watch: {
    searchname: function(newOld, oldVal) {
      this.code = newOld;
      this.LoadDataUser();
    }
  },
  components: {
    listoc,
    Hierarchy,
    Paginate
  },
  created() {
    let seft = this;
    seft.getOc()
  },
  mounted(){
    EventBus.$on("addUserToLevel", this.addUserToLevel);
  },
  destroyed(){
    EventBus.$off("addUserToLevel", this.addUserToLevel);
  },
  methods: {
    clear() {
      this.$refs.grid.ej2Instances.searchSettings.key = "";
    },
    actionComplete(args){
    },
    rowSelected(args){
      this.title = '- ' + args.data.title
      this.levelid = args.data.levelnumber
      this.ocId = args.data.key
      this.LoadDataUser()
      this.isHide = true
      this.$refs.grid.ej2Instances.searchSettings.key = "";

    },
    rowSelected2(args){
    },
    contextMenuClick(args){
    },
    getOc(){
      axios.get("KPI/GetListTreeClient/" + VueJwtDecode.decode(localStorage.getItem("authToken")).nameid).then(res=>{
        this.data = res.data
      })

    },
    LoadDataUser(){
      axios.get(`AddUserToLevel/LoadDataUser/${this.ocId}/${this.code}/${this.page}/${this.pageSize}`)
      .then(res=>{
        this.data2 = res.data.data
      })
    },
    addUserToLevel(data){
      if(!data.Status === true){
        axios.post(`AddUserToLevel/AddUserToLevel/${data.ID}/${this.ocId}/${localStorage.getItem('User')}`)
        .then(res=>{
          success("Add successfully");
          this.LoadDataUser()
        })
      }
      else{
      axios.post(`AddUserToLevel/AddUserToLevel/${data.ID}/${this.ocId}/${localStorage.getItem('User')}`)
        .then(res=>{
          success("Uncheck successfully");
          this.LoadDataUser()
        })
      }
    }

  }
};
</script>
