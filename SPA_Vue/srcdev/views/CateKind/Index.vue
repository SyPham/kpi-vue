<template>
<section class="content">
    <div class="container-fluid">
      <div class="row">
        <div class="col-md-12 my-3">
          <button type="button" class="btn bg-gradient-secondary btn-sm" @click="openAddUser">
              <i class="fas fa-plus"></i> Add Category Kind
            </button>
        </div>
        <div class="col-md-12">
          <div class="card">
            <div class="card-header">
                <h5 class="card-title">Kind lists</h5>
                <div class="card-tools">
                    <button type="button" class="btn btn-tool" data-card-widget="collapse">
                        <i class="fas fa-minus"></i>
                    </button>
                </div>
            </div>
              <ejs-grid  ref="grid" :gridLines="lines" :allowResizing="true" :searchSettings="searchSettings" :rowSelected="rowSelected"  :toolbar='toolbarOptions' :dataSource='data' :allowPaging="true" :pageSettings='pageSettings' >
                <e-columns>
                  <e-column field='ID'  headerText='#' width='150' textAlign='Center'  >
                  </e-column>
                  <e-column field='Cate_Kind_Name' headerText='Name' width='120' textAlign='Center' >
                  </e-column>
                  <e-column headerText='Options' :template="RecordTemplate" width='150' textAlign='Center'  >
                  </e-column>
                </e-columns>
              </ejs-grid>
          </div>

        </div>

        <b-modal ref="modalAddUser" size="lg" hide-footer title="Add">
          <div class="modal-body">
            <div class="row">
              <div class="col-md-12">
                <div class="form-group">
                  <label for="Name">Name</label>
                  <span style="color:red">(*)</span>
                  <input v-model="name" name="name" type="text"  class="form-control Name" />
                </div>
              </div>
            </div>
          </div>
          <div class="modal-footer">
            <button type="button" @click="hideModal" class="btn btn-light" >Close</button>
            <button type="submit" @click="save"  class="btn btn-primary">Save changes</button>
          </div>
        </b-modal>

        <b-modal ref="modalEditUser" size="lg" hide-footer title="Edit">
          <div class="modal-body">
            <div class="row">
              <div class="col-md-12">
                <div class="form-group">
                  <label for="Name">Name</label>
                  <span style="color:red">(*)</span>
                  <input v-model="nameEdit" name="nameEdit" type="text"  class="form-control Name" />
                </div>
              </div>
            </div>
          </div>
          <div class="modal-footer">
            <button type="button" @click="hideModal" class="btn btn-light" >Close</button>
            <button type="submit" @click="update"  class="btn btn-primary">Save changes</button>
          </div>
        </b-modal>
      </div>
    </div>
</section>
</template>

<script>
import { HTTP } from "../../http-constants";
import Paginate from "vuejs-paginate";
import VueJwtDecode from 'vue-jwt-decode'
import EventBus from "../../utils/EventBus.js";
import Vue from "vue";
import { GridPlugin, Page ,Toolbar,Filter } from "@syncfusion/ej2-vue-grids";

Vue.use(GridPlugin);

export default {
  name: "IndexKpi",
  data() {
    return {
      lines: "Both",
      name: null,
      nameEdit: "",
      dataUnit: [],
      RecordTemplate: function() {
        return {
          template: Vue.component("RecordTemplate", {
            template: `
              <div v-if="role == 1" >
               <ejs-button  v-on:click.native="EditKind(data)"  iconCss='e-icons e-edit' cssClass= 'e-flat'></ejs-button>
               <ejs-button v-on:click.native="Delete(data.ID)"  iconCss='e-icons e-delete' cssClass= 'e-flat'></ejs-button>
              </div>
              <div v-else-if="data.UserID == userid" >
               <ejs-button  v-on:click.native="EditKind(data)"  iconCss='e-icons e-edit' cssClass= 'e-flat'></ejs-button>
               <ejs-button v-on:click.native="Delete(data.ID)"  iconCss='e-icons e-delete' cssClass= 'e-flat'></ejs-button>
              </div>
            `,
            data: function() {
              return {
                data: {},
                role: localStorage.getItem('Role'),
                userid: VueJwtDecode.decode(localStorage.getItem("authToken")).nameid,
              };
            },
            methods: {
              EditKind(data){
                EventBus.$emit("id",data)
              },
              Delete(id){
                EventBus.$emit("delete",id)
              }
            }
          })
        };
      },
      toolbarOptions : ['Search'],
      pageSettings: { pageSize: 10 },
      data: [],
      data2: [],
      searchSettings: { hierarchyMode: 'Parent' },
      editparams: { params: { format: 'n' } },
      editing:{ allowDeleting: false, allowEditing: false, mode: 'Normal' },
      pageSettings: { pageSize: 10 },
      expanded: {},
      role: null,
      userid: VueJwtDecode.decode(localStorage.getItem("authToken")).nameid,
      totalPage: 0,
      page: 1,
      skip: 0,
      ID: null,
      searchname: "",
      pageSize: 1000,
      search:" "
    };
  },
  provide: {
    grid: [Page,Toolbar,Filter],
  },
  components: {
    Paginate
  },
  watch: {
    // searchname: function(newOld, oldVal) {
    //   this.LoadData(this.searchname, newOld, 1);
    // }
  },
  mounted(){
    EventBus.$on("data", this.updateState);
    EventBus.$on("id", this.EditUser);
    EventBus.$on("delete", this.Delete);
  },
  destroyed(){
    EventBus.$off("data", this.updateState);
    EventBus.$off("id", this.EditUser);
    EventBus.$off("delete", this.Delete);
  },
  created() {
    let seft = this;
    setTimeout(function(){
      seft.LoadData();
    },500)
    seft.role = localStorage.getItem('Role')
  },
  methods: {

    Delete(id){
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
        axios.post(`AdminCategory/DeleteCateKind/${id}`)
        .then(res=>{
          success("Kind has been deleted!")
          this.LoadData()
        })
       }
      })
    },

    hideModal(){
      this.$refs['modalEditUser'].hide()
      this.$refs['modalAddUser'].hide()
    },
    openAddUser(){
      this.$refs['modalAddUser'].show()
    },
   
    rowSelected(args){
    },
    LoadData() {
      let seft = this;
      axios.get('AdminCategory/GetAllCateKind').then(res => {
        seft.data = res.data;
      });
    },
   
    remove(id) {
      const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
          confirmButton: "btn btn-success",
          cancelButton: "btn btn-danger"
        },
        buttonsStyling: false
      });

      swalWithBootstrapButtons
        .fire({
          customClass: {
            confirmButton: "btn btn-success",
            cancelButton: "btn btn-danger"
          },
          buttonsStyling: false,
          title: "Are you sure?",
          text: "Are you sure to delete this user?",
          icon: "warning",
          showCancelButton: true,
          confirmButtonText: "Yes, delete it!",
          cancelButtonText: "No, cancel!",
          reverseButtons: true
        })
        .then(result => {
          if (result.value) {
            axios.post("AdminUser/Delete/" + id)
              .then(r => {
                this.LoadData();
                $("#RemoveModal").modal("hide");
                // alertify.success("Success");
              })
              .catch(r => {
               // console.log(r);
              });
            swalWithBootstrapButtons.fire(
              "Deleted!",
              "User has been deleted.",
              "success"
            );
          } else if (
            /* Read more about handling dismissals below */
            result.dismiss === Swal.DismissReason.cancel
          ) {
            swalWithBootstrapButtons.fire(
              "Cancelled",
              "Your imaginary file is safe :)",
              "error"
            );
          }
        });
    },

    save(){
      if (this.name !== null) {
        axios.post('AdminCategory/AddCateKind',{
          Cate_Kind_Name: this.name,
        }).then(res=>{
          console.log(res)
          if(res.data)
          {
            success("Add successfully");
            this.LoadData();
            this.$refs['modalAddUser'].hide()
          }
          else{
            warning("Add Fail");
          }
        })
      } else {
        warning("Name Kind not Empty");
      }
    },

    EditUser(data){
      this.$refs['modalEditUser'].show()
      this.ID = data.ID;
      this.nameEdit = data.Cate_Kind_Name
    },

    update(){
      axios.post('AdminCategory/UpdateCateKind',{
        ID: this.ID,
        Cate_Kind_Name: this.nameEdit,
      }).then(res=>{
        success("Update successfully");
        this.LoadData()
        this.$refs['modalEditUser'].hide()
      })
    }
  }
};
</script>
