<template>
<section class="content">
    <div class="container-fluid">
      <div class="row">
        <!-- left column -->
        <div class="col-md-12">
          <!-- general form elements -->
          <div class="card card-primary">
            <div class="card-header">
              <h3 class="card-title">Update Category</h3>
            </div>
            <!-- /.card-header -->
            <!-- form start -->
            <form @submit.prevent="update" role="form">
              <div class="card-body">

                <div class="row">

                  <div class="form-group col-md-6">
                    <label for="exampleInputEmail1">Chinese Name</label>
                    <input type="text" class="form-control" id="exampleInputEmail1" placeholder="Enter Name" v-model="NameTW"/>
                  </div>

                  <div class="form-group col-md-6">
                    <label for="exampleInputEmail1">English Name</label>
                    <input type="text" class="form-control" id="exampleInputEmail1" placeholder="Enter Name" v-model="NameEn"/>
                  </div>
                </div>

                <div class="row">

                  <div class="form-group col-md-6">
                    <label for="exampleInputEmail1">Local Language</label>
                    <input type="text" class="form-control" id="exampleInputEmail1" placeholder="Enter Name" v-model="NameVI"/>
                  </div>

                  <div class="form-group col-md-6">
                    <label for="exampleInputEmail1">Code</label>
                    <input v-model="Code" type="text" class="form-control" placeholder="Enter Code" />
                  </div>
                </div>

                <div class="row">

                  <div class="form-group col-md-6">
                    <label>KPI Kinds</label>
                    <span style="color:red">(*)</span>
                    <select v-model="Kind" class="form-control select2bs4 select2-hidden-accessible" style="width: 100%;" data-select2-id="17" tabindex="-1" aria-hidden="true" >
                      <option value="0" selected>None</option>
                      <option v-for="item in dataKind" :key="item.value" :label="item.Name" :value="item.ID"></option>
                    </select>
                  </div>

                  <div class="form-group col-md-6">
                    <label for="exampleInputEmail1">Level</label>
                    <ejs-dropdowntree ref="ddtInstance" :value='value' :changeOnBlur="false" @change='onchange' :fields='fields' :showCheckBox='true'></ejs-dropdowntree>
                    <!-- <input v-model="Level" type="text" class="form-control" placeholder="Enter Level" /> -->
                  </div>
                </div>



              </div>
              <!-- /.card-body -->

              <div class="card-footer">
                <button  type="submit" class="btn btn-success">Save</button>
                <button @click="$router.push('/adminCategory')"  class="btn btn-danger">Close</button>
              </div>

            </form>
          </div>
          <!-- /.card -->
        </div>
        <!--/.col (left) -->
      </div>
    </div>
</section>
</template>

<script>
import { HTTP } from "../../http-constants";
import { required, minLength, sameAs } from "vuelidate/lib/validators";
import EventBus from "../../utils/EventBus.js";
import { DropDownTreePlugin } from '@syncfusion/ej2-vue-dropdowns';
import Vue from "vue";
Vue.use(DropDownTreePlugin);
var data = [
  { id: 1, name: '1'},
  { id: 2, name: '2'},
  { id: 3, name: '3'},
  { id: 4, name: '4'},
  { id: 5, name: '5'},
];
export default {
  name: "UpdateCategory",
  data() {
    return {
      fields: { dataSource: data, value: 'id', text: 'name', parentValue:"pid", hasChildren: 'hasChild' },
      data: [],
      ID: 0,
      value: [],
      Name: null,
      NameVI: null,
      NameEn: null,
      NameTW: null,
      Level: 0,
      Code: null,
      Kind: null,
      dataKind: [],
      locale: $cookies.get("Lang"),
      submitted: false,
      LevelDefault: '',
      ListLevel: []
    }
  },

  validations: {
    Name: { required },
    Level: { required },
    Code: { required }
  },

  created() {
    EventBus.$on("flag", locale => {
      this.locale = locale;
    });
    this.get(this.$route.params.id);
    this.ID = this.$route.params.id;
    this.getKpiKind();
  },

  watch:{
    locale: function(newOld,oldVal){
      this.locale = newOld
      this.getKpiKind()
    }
  },

  methods: {

    onchange: function() {
      var defaultObj = this.$refs.ddtInstance.ej2Instances;
      this.ListLevel = defaultObj.selectedData;
    },
    
    getKpiKind(){
      axios.get(`AdminKPI/GetAllKpiKindWithLang/${this.locale}`).then(res => {
        this.dataKind = res.data
      })
    },

    resetForm() {
      this.$router.push("/adminCategory");
    },

    get(id) {
      if (id == undefined) return;
      axios.get("adminCategory/GetbyID/" + id , {
        headers:{
          Authorization: 'Bearer '+ localStorage.getItem("authToken")
        }
      })
      .then(r => {
        this.NameVI = r.data.NameVI
        this.NameEn = r.data.NameEn
        this.NameTW = r.data.NameTW
        this.Level = r.data.LevelID
        this.Code = r.data.Code
        this.Kind = r.data.Cate_Kind_ID
        this.value = r.data.List_Level
        this.LevelDefault = r.data.List_Level.length
      })
      .catch(r => {});
    },

    update() {
      if((this.ListLevel.length)/2 != this.LevelDefault){
        Swal.fire({
          title: 'Are you sure?',
          text: "If your change the level number! you may lose data from OC Category and OC Category KPI",
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Yes, Update it!'
          }).then((result) => {
            if (result.value) {
            axios.post("adminCategory/Update2", {
              ID: this.ID,
              NameVI: this.NameVI,
              NameEn: this.NameEn,
              NameTW: this.NameTW,
              LevelID: this.Level,
              Code: this.Code,
              LevelID2: this.ListLevel,
              Cate_Kind_ID: this.Kind,
              headers:{
                Authorization: 'Bearer '+ localStorage.getItem("authToken")
              }
            })
            .then(r => {
              this.$router.push("/adminCategory");
              success("update success!");
            })
            .catch(e => {
              alertify.error("Erro");
            });
          }
        })
      }
      else {
        axios.post("adminCategory/Update2", {
          ID: this.ID,
          NameVI: this.NameVI,
          NameEn: this.NameEn,
          NameTW: this.NameTW,
          LevelID: this.Level,
          LevelID2: this.ListLevel,
          Code: this.Code,
          Cate_Kind_ID: this.Kind,
          headers:{
            Authorization: 'Bearer '+ localStorage.getItem("authToken")
          }
        })
        .then(r => {
          this.$router.push("/adminCategory");
          success("update success!");
        })
        .catch(e => {
          alertify.error("Erro");
        });
      }
    }

  }

}
</script>
