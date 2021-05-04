<template>
<section class="content">
    <div class="container-fluid">
      <div class="row" >
        <marquee behavior="scroll" direction="left" onmouseover="this.stop();" onmouseout="this.start();">
          <span _ngcontent-bwi-c334="" class="text-danger font-weight-bold" style="font-size: 18px;">
            Please use super admin account when you would like to modify or delete these KPI
          </span>
        </marquee>

        <div  class="col-md-12">
          <div class="card">
            <div class="card-header">
              <div class="row">
                <div class="col-md-4">
                  <h3 class="card-title">List KPIs</h3>
                </div>
                <div class="col-md-4">
                  <!-- <div class="form-group">
                    <div class="input-group">
                      <input v-model="searchname" type="text" class="form-control" placeholder="Search name"/>
                      <div class="input-group-append" data-target="#timepicker" data-toggle="datetimepicker">
                        <button class="input-group-text btn-success" @click="searchname = ''"> <i class="fas fa-remove"></i> Clear</button>
                      </div>
                    </div>
                  </div> -->
                </div>
                <div class="col-md-4">
                  <button @click="$router.push(`/adminKPI/create`)" class="btn btn-success float-right">
                    <i class="fa fa-plus"></i> {{$t('Add_btn')}}
                  </button>
                </div>
              </div>
            </div>
            <div class="card-body">
              <ejs-grid  ref="grids" :dataSource='data'  :gridLines="lines" :allowResizing="true" :searchSettings="searchSettings" :toolbar='toolbarOptions'  :allowPaging="true" :pageSettings='pageSettings' >
                <e-columns>
                  <e-column  :template="SttTemplate" headerText='#' width='80' textAlign='Center'  >
                  </e-column>
                  <e-column field='ChineseName' headerText='Chinese Name'  textAlign='Center' >
                  </e-column>
                  <e-column field='EnglishName' headerText='English Name' textAlign='Center' >
                  </e-column>
                  <e-column field='LocalLanguage' headerText='Local Language Name' textAlign='Center' >
                  </e-column>
                  <e-column :template="DescriptionTemplate"  headerText='Description' textAlign='Center' > </e-column>
                  <e-column field='Code' :headerText="$t('Code')" textAlign='Center' > </e-column>
                  <e-column field='Unit' :headerText="$t('Units')" textAlign='Center' > </e-column>
                  <e-column :template="CreatedByTemplate" field='CreatedBy' headerText='Created By' textAlign='Center' > </e-column>
                  <e-column field='KPI_Kind_Name' headerText='KPI Kind' textAlign='Center' > </e-column>
                  <e-column :template="KPITemplate" headerText='KPI Scores' textAlign='Center' > </e-column>
                  <e-column headerText='Options' :template="RecordTemplate" width='150' textAlign='Center'  >
                  </e-column>
                </e-columns>
              </ejs-grid>
              <!-- <table class="table table-bordered">
                <thead>
                  <tr>
                    <th>#</th>
                    <th>EngLish Name</th>
                    <th>Chinese Name</th>
                    <th>Local Language Name</th>
                    <th>Description</th>
                    <th>{{$t('Code')}}</th>
                    <th>{{$t('Units')}}</th>
                    <th>Created By</th>
                    <th>KPI Kind</th>
                    <th>KPI Scores</th>
                    <th>{{$t('AddEditDelete')}}</th>
                  </tr>
                </thead>
                <tbody>
                  <tr v-for="(kpi, key, index) in data" :key="index">
                    <td>{{ key + 1 }}</td>
                    <td>{{kpi.Names[2]}}</td>
                    <td>{{kpi.Names[1]}}</td>
                    <td>{{ kpi.Names[0] || kpi.Name }}</td>
                    <td style="text-align: center">{{ kpi.Description || "N/A"}}</td>
                    <td style="text-align: center">{{ kpi.Code }}</td>
                    <td style="text-align: center">{{ kpi.Unit }}</td>
                    <td style="text-align: center">
                      <span class="badge badge-pill badge-danger">{{kpi.CreatedBy}}</span>
                    </td>
                    <td style="text-align: center">{{ kpi.KPI_Kind_Name }}</td>
                    <td style="text-align: center">
                      <span v-if="kpi.Status == true" >Lower is better</span>
                      <span v-else >Higher is better</span>
                    </td>

                    <td style="text-align: center" >
                      <div v-if="role == 1" class="btn-group">
                        <div class="btn-group">
                          <button @click="$router.push(`/adminKPI/${kpi.ID}/edit`)" class="btn btn-primary btn-sm">
                            <i class="fa fa-edit"></i> {{$t('Edit')}}
                          </button>

                          <button @click="remove(kpi.ID)" class="btn btn-danger btn-sm">
                            <i class="fa fa-trash"></i> {{$t('Delete')}}
                          </button>
                        </div>
                      </div>
                      <div v-else-if="kpi.UserID == userid" class="btn-group">
                        <div class="btn-group">
                          <button @click="$router.push(`/adminKPI/${kpi.ID}/edit`)" class="btn btn-primary btn-sm">
                            <i class="fa fa-edit"></i> {{$t('Edit')}}
                          </button>

                          <button @click="remove(kpi.ID)" class="btn btn-danger btn-sm">
                            <i class="fa fa-trash"></i> {{$t('Delete')}}
                          </button>
                        </div>
                      </div>
                    </td>
                  </tr>
                </tbody>
              </table> -->
            </div>
            <!-- <div class="card-footer clearfix">
              <Paginate
                v-model="page"
                :page-count="totalPage "
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
            </div> -->
          </div>
        </div>

      </div>
    </div>
</section>
</template>

<script>
import { HTTP } from "../../http-constants";
import Paginate from "vuejs-paginate";
import VueJwtDecode from 'vue-jwt-decode';
import EventBus from "../../utils/EventBus.js";
import { GridPlugin, Page ,Toolbar,Filter, Resize , ExcelExport } from "@syncfusion/ej2-vue-grids";
import Vue from "vue";
Vue.use(GridPlugin);
export default {
  name: "IndexKpi",
  data() {
    return {
      lines: "Both",
      SttTemplate: function() {
        return {
          template: Vue.component("SttTemplate", {
            template: `
              <span>{{NO(data)}}</span>
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

      DescriptionTemplate: function() {
        return {
          template: Vue.component("DescriptionTemplate", {
            template: `
              <span >{{ data.Description || "N/A"}}</span>
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

      KPITemplate: function() {
        return {
          template: Vue.component("KPITemplate", {
            template: `
              <span v-if="data.Status == true" >Lower is better</span>
              <span v-else >Higher is better</span>
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
      role: null,
      userid: VueJwtDecode.decode(localStorage.getItem("authToken")).nameid,
      data: [],
      ID: 0,
      DID: 0,
      totalPage: 0,
      page: 1,
      pageSize: 10,
      catID: 0,
      name: " ",
      searchname: "",
      locale: $cookies.get("Lang"),
      toolbarOptions : ['Search'],
      pageSettings: { pageSize: 10 },
      searchSettings: { hierarchyMode: 'Parent' },
      editparams: { params: { format: 'n' } },
      editing:{ allowDeleting: false, allowEditing: false, mode: 'Normal' },
    };
  },
  provide: {
    grid: [Page, Toolbar, Filter, Resize, ExcelExport],
  },
  components: {
    Paginate
  },
  watch: {
    searchname: function(newOld, oldVal) {
      this.name = newOld;
      this.LoadData();
    }
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
    seft.role = localStorage.getItem('Role')
    seft.ID = seft.$route.params.id;
    seft.connection = new signalR.HubConnectionBuilder()
    .withUrl("http://10.4.5.132:991/henry-hub")
    .configureLogging(signalR.LogLevel.Information)
    .build();
    seft.connection.start().then(function () {
    });
    seft.connection.on("SentMess", (data) => {
      setTimeout(()=>{
        seft.LoadData();
      },500)
      // console.log(data)
    });
  },
  methods: {
    nextPage(data) {
       this.$router.push(`/adminKPI/${data.ID}/edit`)
    },
    LoadData() {
      let seft = this
      axios.post(`AdminKPI/LoadData/${seft.page}/${seft.pageSize}/${seft.name}/${seft.locale}`,{
        headers:{
          Authorization: 'Bearer '+ localStorage.getItem("authToken")
        }
      }).then(res => {
        console.log('LoadData',res);
        seft.data = res.data.data;
      });
    },
    changePage(pageNum) {
      this.LoadData(this.name, pageNum);
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
            axios.get(`AdminKPI/delete/${id}`,{
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
               // console.log(r);
              });
            swalWithBootstrapButtons.fire(
              "Deleted!",
              "Your file has been deleted.",
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
    },
  }

};
</script>
<style lang="scss" scoped>
td {
 word-break: break-word;
}
</style>
