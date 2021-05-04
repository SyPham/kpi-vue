<template>
<section class="content">
    <div class="container-fluid">
      <div class="row">



        <div class="col-md-12">
          <div class="callout bg-yellow-gradient" style="border-color:#c57901">
            <h3>1. {{$t('OC')}}</h3>
          </div>
        </div>
        <div class="col-md-12 pb-4">
            <button v-if="role == 1"
                type="button"
                class="btn bg-gradient-secondary btn-sm"
                @click="openAddOC">
                <i class="fas fa-plus"></i> Add OC
            </button>
        </div>
        <div class="col-md-12">
          <ejs-treegrid
            ref="treegrid"
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
            :toolbar="toolbar"
            >
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
                    :template="OCTemplate"
                    edit="editparams"
                    :disableHtmlEncode="false">
                </e-column>
            </e-columns>
        </ejs-treegrid>

        </div>
        <b-modal ref="modalAddOC" hide-footer :title="modalTitle">
          <div class="modal-body">
              <div class="row">
                  <div class="col-md-12">
                      <div class="form-group">
                          <label for="Name">Name</label>
                          <input v-model="oc.Name" type="text"  class="form-control Name" />
                      </div>
                  </div>
              </div>
          </div>
          <div class="modal-footer">
              <button type="button" class="btn btn-light" >Close</button>
              <button type="submit" @click="save"  class="btn btn-primary">Save changes</button>
          </div>
        </b-modal>
        <b-modal ref="addSubOC" hide-footer :title="modalTitle">
          <div class="modal-body">
              <div class="row">
                  <div class="col-md-12">
                      <div class="form-group">
                          <label for="Name">Name</label>
                          <input v-model="oc.Name" type="text"  class="form-control Name" />
                      </div>
                  </div>
              </div>
          </div>
          <div class="modal-footer">
              <button type="button" class="btn btn-light" >Close</button>
              <button type="submit" @click="save"  class="btn btn-primary">Save changes</button>
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
import VueJwtDecode from "vue-jwt-decode";
import Vue from "vue";
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
      data: [],
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
        'Search',
        'ExpandAll',
        'CollapseAll',
      ],
      editing:{ allowDeleting: true, allowEditing: true, mode: 'Row' },
      pageSettings: { pageSize: 100 },
      expanded: {},
      oc: { id: 0, Code: '', Name: '', LevelNumber: 1, ParentID: 0 },
      edit: {
        key: 0,
        title: '',
      },
      name: null,
      modalTitle:  'Add OC',
      role: ''
    }
  },
  components: {
    listoc,
    Hierarchy
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
  created() {
    let seft = this;
    seft.GetOcs()
    seft.role = localStorage.getItem('Role')
  },
  methods: {
    clearFrom() {
      this.oc = { id: 0, name: '', level: 0 };
    },
    save(){
      if(this.oc.ParentID > 0) {
        axios.post('AdminLevel/Add',this.oc).then(res =>{
          if(res){
            this.GetOcs()
            success("Add SubOC successfully");
            this.$refs['addSubOC'].hide()
            this.clearFrom()
          }
        })
      }
      else{
        axios.post('AdminLevel/Add',this.oc).then(res =>{
          if(res){
            this.clearFrom()
            this.GetOcs()
            this.$refs['modalAddOC'].hide()
            success("Add OC successfully");
          }
        })
      }
    },
    openAddOC(){
       this.$refs['modalAddOC'].show()
    },
    GetOcs(){
      axios.get("KPI/GetListTreeClient/" + VueJwtDecode.decode(localStorage.getItem("authToken")).nameid).then(res=>{
        this.data = res.data
      })
    },
    actionComplete(args){
      if (args.requestType === 'save') {
        this.edit.title = args.data.title;
        axios.post('AdminLevel/Rename', {
          key: this.edit.key,
          title: this.edit.title
        }).then(res => {
          success("Edit Successfully")

          this.GetOcs()
        });
      }
    },
    rowSelected(args){
      this.edit = {
        key: args.data.key,
        title: args.data.title
      };
      this.oc = {
        id: args.data.key,
        Name: args.data.title,
        LevelNumber: args.data.levelnumber,
        ParentID: args.data.parentid
      }
    },
    contextMenuClick(args,addSubOC){
      if (args.item.id === 'Add-Child-OC') {
        this.modalTitle = 'Add Sub OC'
        this.$refs['addSubOC'].show()
        this.oc = { id: 0, Code: '', Name: '', LevelNumber: args.rowInfo.rowData.levelnumber + 1, ParentID: args.rowInfo.rowData.key }
      } else {
        this.delete(args.rowInfo.rowData.key)
      }
    },
    delete(id){
      this.$swal({
        title: 'Are you sure?',
        text: 'You can\'t revert your action',
        type: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes Delete it!',
        cancelButtonText: 'No, Keep it!',
        showCloseButton: true,
        showLoaderOnConfirm: true
        }).then((result) => {
        if (result.value) {
        axios.get(`AdminLevel/Remove/${id}`)
        .then(res=>{
          success("OC has been deleted!")
          this.GetOcs()
        })
       } else {
          this.$swal('Cancelled', 'Your file is still intact', 'info')
        }
      })
    }
  }
}
</script>
