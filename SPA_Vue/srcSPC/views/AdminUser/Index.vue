<template>
  <section class="content">
      <div class="container-fluid">
        <div class="row">

          <div class="col-md-12 my-3">
            <button type="button" class="btn bg-gradient-secondary btn-sm" @click="openAddUser">
                <i class="fas fa-plus"></i> Add User
              </button>
          </div>
          <div class="col-md-12">
            <div class="card">
              <div class="card-header">
                  <h5 class="card-title">User lists</h5>
                  <div class="card-tools">
                      <button type="button" class="btn btn-tool" data-card-widget="collapse">
                          <i class="fas fa-minus"></i>
                      </button>
                  </div>
              </div>
                <ejs-grid  ref="grid" :searchSettings="searchSettings" :rowSelected="rowSelected"  :toolbar='toolbarOptions' :dataSource='data' :allowPaging="true" :pageSettings='pageSettings' >
                  <e-columns>
                    <e-column field='ID' :template="ResetTemplate" headerText='Reset Password' width='150' textAlign='Center' isPrimaryKey='true' >
                    </e-column>
                    <e-column field='Username' headerText='Username' width='120' textAlign='Left' >
                    </e-column>
                    <e-column field='Email' headerText='Email' width='250' textAlign='Center' ></e-column>
                    <e-column field='Alias' headerText='Display Name' width='150' textAlign='Center' ></e-column>
                    <e-column field='FullName' headerText='FullName' width='120' textAlign='Center' ></e-column>
                    <!-- <e-column field='' headerText='State' :template="CheckboxTemplate" width='150' textAlign='Center' >
                    </e-column> -->
                    <e-column field='PermissionName' headerText='Permission' width='120' textAlign='Center' ></e-column>
                    <e-column field='CreatedBy' headerText='Created By' width='120' textAlign='Center' ></e-column>
                    <e-column headerText='Manage Records' :template="RecordTemplate" width='150' textAlign='Center'  >
                    </e-column>
                  </e-columns>
                </ejs-grid>
            </div>

          </div>


          <b-modal ref="modalAddUser" size="lg" hide-footer title="Add User">
            <div class="modal-body">
                <div class="row">
                  <div class="col-md-12">
                      <div class="form-group">
                          <label for="Name">User Name</label>
                          <span style="color:red">(*)</span>
                          <input v-model="username" name="username" type="text"  class="form-control Name" />
                      </div>
                  </div>
                  <div class="col-md-12">
                      <div class="form-group">
                          <label for="Name">Password</label>
                          <span style="color:red">(*)</span>
                          <input v-model="password" name="password" type="password"  class="form-control Name" />
                      </div>
                  </div>
                  <div class="col-md-12">
                      <div class="form-group">
                          <label for="Name">Email</label>
                          <span style="color:red">(*)</span>
                          <input v-model="email" name="emailE" type="text"  class="form-control Name" />
                      </div>
                  </div>
                  <div class="col-md-12">
                      <div class="form-group">
                          <label for="Name">Display Name</label>
                          <span style="color:red">(*)</span>
                          <input v-model="displayname" name="displayname" type="text"  class="form-control Name" />
                      </div>
                  </div>
                  <div class="col-md-12">
                      <div class="form-group">
                          <label for="Name">Full Name</label>
                          <span style="color:red">(*)</span>
                          <input v-model="fullname" name="fullname" type="text"  class="form-control Name" />
                      </div>
                  </div>
                  <div class="col-md-12">
                      <div class="form-group">
                          <label for="Name">Skype</label>
                          <input v-model="skype" name="skype" type="text"  class="form-control Name" />
                      </div>
                  </div>
                  <div class="col-md-12">
                      <div class="form-group">
                          <label for="Name">Code</label>
                          <input v-model="code" name="code" type="text"  class="form-control Name" />
                      </div>
                  </div>
                  <div class="col-md-12">
                      <div class="form-group">
                          <label>Permission</label>
                          <span style="color:red">(*)</span>
                          <select class="form-control select2bs4 select2-hidden-accessible" style="width: 100%;" data-select2-id="17" tabindex="-1" v-model="permission" name="permission" aria-hidden="true">
                              <option v-for="item in dataUnit" :key="item.value" :label="item.Name"  :value="item.ID"></option>
                          </select>
                      </div>
                  </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" @click="hideModal" class="btn btn-light" >Close</button>
                <button type="submit" @click="save"  class="btn btn-primary">Save changes</button>
            </div>
          </b-modal>

          <b-modal ref="modalEditUser" size="lg" hide-footer title="Edit User">
            <div class="modal-body">
                <div class="row">
                  <div class="col-md-12">
                      <div class="form-group">
                          <label for="Name">User Name</label>
                          <span style="color:red">(*)</span>
                          <input v-model="usernameEdit" name="usernameEdit" type="text"  class="form-control Name" />
                      </div>
                  </div>
                  <!-- <div class="col-md-12">
                      <div class="form-group">
                          <label for="Name">Password</label>
                          <span style="color:red">(*)</span>
                          <input v-model="password" name="password" type="password"  class="form-control Name" />
                      </div>
                  </div> -->
                  <div class="col-md-12">
                      <div class="form-group">
                          <label for="Name">Email</label>
                          <span style="color:red">(*)</span>
                          <input v-model="emailEdit" name="emailEdit" type="text"  class="form-control Name" />
                      </div>
                  </div>
                  <div class="col-md-12">
                      <div class="form-group">
                          <label for="Name">Display Name</label>
                          <span style="color:red">(*)</span>
                          <input v-model="displaynameEdit" name="displaynameEdit" type="text"  class="form-control Name" />
                      </div>
                  </div>
                  <div class="col-md-12">
                      <div class="form-group">
                          <label for="Name">Full Name</label>
                          <span style="color:red">(*)</span>
                          <input v-model="fullnameEdit" name="fullnameEdit" type="text"  class="form-control Name" />
                      </div>
                  </div>
                  <div class="col-md-12">
                      <div class="form-group">
                          <label for="Name">Skype</label>
                          <input v-model="skypeEdit" name="skypeEdit" type="text"  class="form-control Name" />
                      </div>
                  </div>
                  <div class="col-md-12">
                      <div class="form-group">
                          <label for="Name">Code</label>
                          <input v-model="codeEdit" name="codeEdit" type="text"  class="form-control Name" />
                      </div>
                  </div>
                  <div class="col-md-12">
                      <div class="form-group">
                          <label>Permission</label>
                          <span style="color:red">(*)</span>
                          <select class="form-control select2bs4 select2-hidden-accessible" style="width: 100%;" data-select2-id="17" tabindex="-1" v-model="permissionEdit" name="permissionEdit" aria-hidden="true">
                              <option v-for="item in dataUnit" :key="item.value" :label="item.Name"  :value="item.ID"></option>
                          </select>
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
// import {
//   TreeGridPlugin,
//   ContextMenu,
//   Sort,
//   Filter,
//   Edit,
//   ExcelExport,
//   PdfExport,
//   Page,
//   Resize,
//   Toolbar
// } from "@syncfusion/ej2-vue-treegrid";
// Vue.use(TreeGridPlugin)
import { GridPlugin, Page ,Toolbar,Filter } from "@syncfusion/ej2-vue-grids";

Vue.use(GridPlugin);

export default {
  name: "IndexKpi",
  data() {
    return {
      username: "",
      password: "",
      email: "",
      permission: "",
      fullname: "",
      displayname: "",
      skype:  "",
      code:  "",
      usernameEdit: "",
      emailEdit: '',
      fullnameEdit: '',
      displaynameEdit: '',
      skypeEdit:  '',
      codeEdit:  '',
      permissionEdit: '',
      dataUnit: [],
      CheckboxTemplate: function() {
        return {
          template: Vue.component("CheckboxTemplate", {
            template: `
              <div>
                <ejs-switch ref="wswitch"  v-model="data.IsActive"  @change="updateState(data)" :checked="data.IsActive"></ejs-switch>
                <label v-if="data.IsActive">Unlocked</label>
                <label v-else>Locked</label>
              </div>
            `,
            data: function() {
              return {
                data: {}
              };
            },
            methods: {
              updateState(data){
                EventBus.$emit("data",data)
              }
            }
          })
        };
      },
      RecordTemplate: function() {
        return {
          template: Vue.component("RecordTemplate", {
            template: `
              <div v-if="role == 1" >
               <ejs-button  v-on:click.native="EditUser(data.ID)"  iconCss='e-icons e-edit' cssClass= 'e-flat'></ejs-button>
               <ejs-button v-on:click.native="Delete(data.ID)"  iconCss='e-icons e-delete' cssClass= 'e-flat'></ejs-button>
              </div>
              <div v-else-if="data.UserID == userid" >
               <ejs-button  v-on:click.native="EditUser(data.ID)"  iconCss='e-icons e-edit' cssClass= 'e-flat'></ejs-button>
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
              EditUser(id){
                EventBus.$emit("id",id)
              },
              Delete(id){
                EventBus.$emit("delete",id)
              }
            }
          })
        };
      },
      ResetTemplate: function() {
        return {
          template: Vue.component("ResetTemplate", {
            template: `
              <div v-if="role == 1" >
                <ejs-button cssClass="e-outline" v-on:click.native="ResetPass(data)" :isPrimary="true">Reset Now</ejs-button>
              </div>
              <div v-else-if="data.UserID == userid" >
                 <ejs-button cssClass="e-outline" v-on:click.native="ResetPass(data)" :isPrimary="true">Reset Now</ejs-button>
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
              ResetPass(data){
                EventBus.$emit("resetpass",data)
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
    searchname: function(newOld, oldVal) {
      this.LoadData(this.searchname, newOld, 1);
    }
  },
  mounted(){
    EventBus.$on("data", this.updateState);
    EventBus.$on("id", this.EditUser);
    EventBus.$on("delete", this.Delete);
    EventBus.$on("resetpass", this.ResetPass);
  },
  destroyed(){
    EventBus.$off("data", this.updateState);
    EventBus.$off("id", this.EditUser);
    EventBus.$off("delete", this.Delete);
    EventBus.$off("resetpass", this.ResetPass);
  },
  created() {
    let seft = this;
    setTimeout(function(){
      seft.LoadData();
    },500)
    seft.GetUnit()
    seft.role = localStorage.getItem('Role')
  },
  methods: {
    ResetPass(data){
      Swal.fire({
          title: 'Are you sure?',
          text: "Are you sure you want to reset the password for user " + data.FullName + "?" ,
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Yes, Reset now!'
          }).then((result) => {
            if (result.value) {
              let mObj = {
                Email: data.Email,
                Username: data.Username
              }
              axios.post('AdminUser/ForgotPassword',mObj).then(res=>{
                Swal.fire(
                  'success!',
                  'Password has been reset for user'+ ' ' + data.FullName,
                  'success'
                )
              })
            }
        })
    },
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
        axios.post(`AdminUser/Delete/${id}`)
        .then(res=>{
          success("User has been deleted!")
          this.LoadData()
        })
       } else {
          this.$swal('Cancelled', 'Your file is still intact', 'info')
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
    updateState(data){
      axios.get(`AdminUser/LockUser/${data.ID}`)
        .then(r => {
          success('Successfully');
        })
        .catch(r => {
        });
    },
    rowSelected(args){
    },
    LoadData() {
      let seft = this;
      axios.post(`AdminUser/LoadData/${seft.page}/${seft.pageSize}/${seft.search}`).then(res => {
        seft.skip = res.data.skip;
        seft.totalPage = res.data.totalPage;
        seft.page = res.data.page;
        seft.data = res.data.data;
      });
    },
    LockUser: function(user,index){
      axios.get(`AdminUser/LockUser/${user.ID}`)
        .then(r => {
          success('Successfully');
          this.LoadData();
        })
        .catch(r => {
        });
    },
    GetUnit(){
      axios.get('AdminUser/GetListAllPermissions/0').then(res=>{
        this.dataUnit = res.data
      })
    },
    changePage(pageNum) {
      this.LoadData(this.search, pageNum);
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
              })
              .catch(r => {
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
      axios.post('AdminUser/add',{
        Username: this.username,
        Password: this.password,
        Email: this.email,
        Alias: this.displayname,
        FullName: this.fullname,
        Skype: this.skype,
        Code: this.code,
        Permission: this.permission,
        Role: this.permission,
        CreatedBy: localStorage.getItem("User"),
        UserID: VueJwtDecode.decode(localStorage.getItem("authToken")).nameid
      }).then(res=>{
        if(res.data == 3)
        {
          error("Email already Exist");
        }
        else{
          success("Add User successfully");
          this.LoadData()
          this.$refs['modalAddUser'].hide()
        }
      })
    },
    EditUser(id){
      this.$refs['modalEditUser'].show()
      axios.get(`AdminUser/GetbyID/${id}`).then(res=>{
        this.ID = res.data.ID
        this.usernameEdit = res.data.Username
        this.emailEdit = res.data.Email
        this.displaynameEdit = res.data.Alias
        this.fullnameEdit = res.data.FullName
        this.skypeEdit = res.data.Skype
        this.codeEdit = res.data.Code
        this.permissionEdit = res.data.Permission
      })
    },
    update(){
      axios.post('AdminUser/Update',{
        ID: this.ID,
        Username: this.usernameEdit,
        Email: this.emailEdit,
        Alias: this.displaynameEdit,
        FullName: this.fullnameEdit,
        Skype: this.skypeEdit,
        Code: this.codeEdit,
        Permission: this.permissionEdit,
        Role: this.permissionEdit
      }).then(res=>{
        success("Update User successfully");
        this.LoadData()
        this.$refs['modalEditUser'].hide()
      })
    },
    showRemoveModal: function(kpi) {
      $("#RemoveModal").modal("show");
    }
  }
};
</script>
