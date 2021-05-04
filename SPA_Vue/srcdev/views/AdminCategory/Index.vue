<template>
<section class="content">
    <div class="container-fluid">
      <div class="row">
        <marquee behavior="scroll" direction="left" onmouseover="this.stop();" onmouseout="this.start();">
          <span _ngcontent-bwi-c334="" class="text-danger font-weight-bold" style="font-size: 18px;">
            Please use super admin account when you would like to modify or delete these KPI
          </span>
        </marquee>
        <!-- <div class="col-md-12 my-3">
          <button @click="$router.push(`/adminCategory/create`)" class="btn btn-success float-right">
            <i class="fa fa-plus"></i> {{$t('Add_btn')}}
          </button>
        </div> -->

        <div class="col-md-12">
          <div class="card">
            <div class="card-header">
              <div class="row">
                <div class="col-md-4">
                  <h3 class="card-title">{{$t('List_Categories')}}</h3>
                </div>
                <div class="col-md-4">
                  <!-- <div class="form-group">
                    <div class="input-group">
                      <input v-model="searchname" type="text" class="form-control" placeholder="Search name"/>
                      <div class="input-group-append" data-target="#timepicker" data-toggle="datetimepicker">
                        <button class="input-group-text btn-success" @click="searchname = ''">Clear</button>
                      </div>
                    </div>
                  </div> -->
                </div>
                <div class="col-md-4">
                  <button @click="$router.push(`/adminCategory/create`)" class="btn btn-success float-right">
                    <i class="fa fa-plus"></i> {{$t('Add_btn')}}
                  </button>
                </div>
              </div>
            </div>
            <!-- /.card-header -->
            <div class="card-body">
              <ejs-grid  ref="grids" :dataSource='data'  :gridLines="lines" :allowResizing="true" :searchSettings="searchSettings" :toolbar='toolbarOptions'  :allowPaging="true" :pageSettings='pageSettings' >
                <e-columns>
                  <e-column field='Index' headerText='#' width='80' textAlign='Center'  ></e-column>
                  <e-column field='ChineseName' headerText='Chinese Name'  textAlign='Center' ></e-column>
                  <e-column field='EngLishName' headerText='English Name' textAlign='Center' ></e-column>
                  <e-column field='LocalLanguage' headerText='Local Language Name' textAlign='Center' ></e-column>
                  <e-column field='Code' :headerText="$t('Code')" textAlign='Center' > </e-column>
                  <e-column field='LevelID' :template="LevelTemplate" :headerText="$t('levelnum')" textAlign='Center' > </e-column>
                  <e-column :template="CreatedByTemplate" field='CreatedBy' headerText='Created By' textAlign='Center' > </e-column>
                  <e-column field='KPI_Kind_Name' headerText='KPI Kind' textAlign='Center' > </e-column>
                  <e-column headerText='Options' :template="RecordTemplate" width='150' textAlign='Center'  ></e-column>
                </e-columns>
              </ejs-grid>


            </div>
            <!-- /.card-body -->

          </div>
        </div>
          <!-- /.card -->
      </div>
    </div>
</section>
</template>

<script>
import { HTTP } from "../../http-constants";
import Paginate from "vuejs-paginate";
import VueJwtDecode from "vue-jwt-decode";
import EventBus from "../../utils/EventBus.js";
import { GridPlugin, Page ,Toolbar,Filter, Resize , ExcelExport } from "@syncfusion/ej2-vue-grids";
import Vue from "vue";
Vue.use(GridPlugin);
export default {
  name: "IndexCategory",
  data() {
    return {
      LevelTemplate: function() {
        return {
          template: Vue.component("LevelTemplate", {
            template: `
              <span>{{Level(data)}}</span>
            `,
            data: function() {
              return {
                data: {},

              };
            },
            methods: {
              Level(data){
                return data.Level.join(',')
              },
            }
          })
        };
      },
      CreatedByTemplate: function() {
        return {
          template: Vue.component("CreatedByTemplate", {
            template: `
              <span class="badge badge-pill badge-danger">{{data.CreatedBy}}</span>
            `,
            data: function() {
              return {
                data: {},

              };
            },
            methods: {
              NO(data){
                return Number(data.index) + 1
              },
            }
          })
        };
      },
      RecordTemplate: function() {
        return {
          template: Vue.component("RecordTemplate", {
            template: `
            <div v-if="role == 1" class="btn-group">
                <div class="btn-group">
                  <button @click="nextPage(data)" class="btn btn-primary btn-sm">
                    <i class="fa fa-edit"></i> Edit
                  </button>
                  <span style="color: transparent;"> | </span>
                  <button @click="Delete(data.ID)" class="btn btn-danger btn-sm">
                    <i class="fa fa-trash"></i> Delete
                  </button>
                </div>
              </div>
              <div v-else-if="data.UserID == userid" class="btn-group">
                <div class="btn-group">
                  <button @click="nextPage(data)" class="btn btn-primary btn-sm">
                    <i class="fa fa-edit"></i> Edit
                  </button>
                  <span style="color: transparent;"> | </span>
                  <button @click="Delete(data.ID)" class="btn btn-danger btn-sm">
                    <i class="fa fa-trash"></i> Delete
                  </button>
                </div>
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
              nextPage(data) {
                EventBus.$emit("nextPage",data)
              },
              Delete(id){
                EventBus.$emit("delete",id)
              }
            }
          })
        };
      },
      data: [],
      toolbarOptions : ['Search'],
      pageSettings: { pageSize: 10 },
      searchSettings: { hierarchyMode: 'Parent' },
      editparams: { params: { format: 'n' } },
      lines: "Both",
      editing:{ allowDeleting: false, allowEditing: false, mode: 'Normal' },
      ID: 0,
      DID: 0,
      totalPage: 0,
      page: 1,
      skip: 0,
      name: " ",
      pageSize: 10,
      searchname: "",
      userid: VueJwtDecode.decode(localStorage.getItem("authToken")).nameid,
      role: null
    };
  },
  provide: {
    grid: [Page, Toolbar, Filter, Resize, ExcelExport],
  },
  components: {
    Paginate
  },
  mounted(){
    EventBus.$on("nextPage", this.nextPage);
    EventBus.$on("id", this.EditUser);
    EventBus.$on("delete", this.remove);
  },
  destroyed() {
    EventBus.$off("nextPage", this.nextPage);
    EventBus.$off("id", this.EditUser);
    EventBus.$off("delete", this.remove);
  },
  created() {

    let seft = this;
    seft.LoadData();
    seft.ID = seft.$route.params.id;
    seft.role = localStorage.getItem('Role')
    // console.log(seft.ID);
  },
  watch: {
    searchname: function(newOld, oldVal) {

      this.name = newOld;
      this.LoadData();
    }
  },
  methods: {

    nextPage(data) {
       this.$router.push(`/adminCategory/${data.ID}/edit`)
    },

    LoadData() {
      let seft = this;
      axios.post(`Admincategory/LoadData/${seft.page}/${seft.pageSize}/${seft.name}/${seft.locale}`,{
        headers:{
          Authorization: 'Bearer '+ localStorage.getItem("authToken")
        }
      }).then(res => {
        seft.skip = res.data.skip;
        seft.totalPage = res.data.pageCount;
        seft.page = res.data.page;
        seft.data = res.data.data;
        seft.pageSize = res.data.pageSize;
      });
    },

    changePage(pageNum) {
      this.LoadData( this.name, pageNum);
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
          text: "You won't be able to revert this!",
          icon: "warning",
          showCancelButton: true,
          confirmButtonText: "Yes, delete it!",
          cancelButtonText: "No, cancel!",
          reverseButtons: true
        })
        .then(result => {
          if (result.value) {
            axios.get(`AdminCategory/delete/${id}`,{
              headers:{
                Authorization: 'Bearer '+ localStorage.getItem("authToken")
              }
            })
              .then(r => {
                this.LoadData();
                $("#RemoveModal").modal("hide");
                // alertify.success("Success");
              })
              .catch(r => {
                console.log(r);
              });
            swalWithBootstrapButtons.fire(
              "Deleted!",
              "Category has been deleted.",
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
      // console.log(id);
    }

  }
};
</script>
