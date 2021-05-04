<template>
<section class="content">
  <div class="container-fluid">
    <div class="row">

      <div class="col-md-12">
          <div class="callout bg-yellow-gradient" style="border-color:#c57901">
              <h3>4. OC Category</h3>
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
              ref="treegrid"
              :dataSource="data"
              height='550px'
              childMapping="children"
              :treeColumnIndex="1"
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
              :toolbar="toolbar"
              >
              <e-columns>
                <e-column field="levelnumber" width="50" isPrimaryKey="true" headerText="Level" :disableHtmlEncode="false"></e-column>
                <e-column field="title" editType="stringedit" headerText="OC Name" edit="editparams" :template="OCTemplate" :disableHtmlEncode="false"></e-column>
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
            <ejs-grid  ref="grid" :dataSourceChanged="dataSourceChanged" :searchSettings="searchSettings2" :rowSelected="rowSelected2"  :toolbar='toolbarOptions' :dataSource='data2' allowPaging='true' :pageSettings='pageSettings2' >
              <e-columns>
                <e-column field='ID' headerText='#' width='80' textAlign='Left' isPrimaryKey='true'></e-column>
                <e-column width='60' :template="CheckboxTemplate"></e-column>
                <e-column field='Name' headerText='Category Name' width='150' textAlign='Left' ></e-column>
                <e-column field='Code' headerText='Category Code' width='150' textAlign='Left' ></e-column>
                <e-column field='KPI_Kind_Name' headerText='KPI Kind' width='150' textAlign='Left' ></e-column>
                <e-column field='LevelID' headerText='Level Number' width='120' textAlign='Center' ></e-column>
                <e-column field='' :template="ModifyByTemplate" headerText='Modify By' width='120' textAlign='Center' ></e-column>
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
import Hierarchy from "../../components/adminOC/Hierarchy"
import listoc from "../../components/adminOC/Modal"
// import { HTTP } from '../../http-constants';
import EventBus from "../../utils/EventBus.js";
import VueJwtDecode from "vue-jwt-decode";
import Paginate from "vuejs-paginate";
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
  Resize,
} from "@syncfusion/ej2-vue-treegrid";
Vue.use(TreeGridPlugin)
import { GridPlugin, Page, Toolbar } from "@syncfusion/ej2-vue-grids";
Vue.use(GridPlugin);
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
      CheckboxTemplate: function() {
        return {
          template: Vue.component("CheckboxTemplate", {
            template: `<ejs-switch  v-model="data.Status"  @change="addOCCategory(data)" :checked="data.Status"></ejs-switch>`,
            data: function() {
              return {
                data: {}
              };
            },
            methods: {
              addOCCategory(data){
                EventBus.$emit("data",data)
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
             <span v-if="data.Status" class="badge badge-pill badge-dark"> {{ data.modifyBy || "#N/A"}}</span>
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
      toolbarOptions : ['Search'],
      pageSettings2: { pageSize: 10 },
      data: [],
      data2: [],
      searchSettings: { hierarchyMode: 'Parent' },
      searchSettings2: { hierarchyMode: 'Parent' },
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
      editing:{ allowDeleting: false, allowEditing: false, mode: 'Row' },
      pageSettings: { pageSize: 100 },
      expanded: {},
      OCCategory: [],
      totalPage: 0,
      page: 1,
      skip: 0,
      levelid:  0,
      ocId:  0,
      title: '',
      isHide: false,
      locale: $cookies.get("Lang"),
      pageSize: 1000
    }
  },
  components:{
    listoc,
    Hierarchy,
    Paginate
  },
  provide: {
    treegrid: [
      ContextMenu,
      Sort,
      Edit,
      ExcelExport,
      PdfExport,
      Filter,
      Resize,
    ],
    grid: [Page,Toolbar]
  },
  mounted(){
    EventBus.$on("data", this.addOCCategory);
  },
  destroyed() {
    EventBus.$off("data",this.addOCCategory)
  },
  created() {
    let seft = this;
    EventBus.$on('flag', locale =>{
      seft.locale = locale
    });
    this.getOc()
  },
  watch:{
    locale: function(newOld,oldVal){
      let self = this
      this.locale = newOld
      self.getCategoryByOC()
    }
  },
  methods: {
    dataSourceChanged() {
      var self = this;
      self.getCategoryByOC();
    },
    addOCCategory(data){
      let self = this
      if(data.Status == true){
        axios.get(`OCCategories/AddOCCategory/${self.ocId}/${data.ID}/${localStorage.getItem('User')}`)
        .then(res =>{
          success("Add successfully");
          self.getCategoryByOC();
        })
      }
      else{
      axios.get(`OCCategories/AddOCCategory/${self.ocId}/${data.ID}/${localStorage.getItem('User')}`)
        .then(res=>{
          success("Uncheck successfully");
          self.getCategoryByOC();
        })
      }
    },
    getOc(){
      axios.get("KPI/GetListTreeClient/" + VueJwtDecode.decode(localStorage.getItem("authToken")).nameid).then(res=>{
        this.data = res.data
      })

    },
    getCategoryByOC(){
      axios.get(`OCCategories/GetCategoryByOC2/${this.levelid}/${this.locale}/${this.ocId}/${this.page}/${this.pageSize}`)
      .then(res=>{
        this.data2 = res.data.data
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
    },
    rowSelected2(args){
    },
    contextMenuClick(args){
    }
  }
};
</script>
