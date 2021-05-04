<template>
<section class="content">
    <div class="container-fluid">
      <div class="row">
        <!-- left column -->
        <div class="col-md-12">
          <!-- general form elements -->
          <div class="card card-primary">
            <div class="card-header">
              <h3 class="card-title">Add Category</h3>
            </div>
            <!-- /.card-header -->
            <!-- form start -->
            <form @submit.prevent="save" role="form">
              <div class="card-body">

                <div class="row">

                  <div class="form-group col-md-6">
                    <label for="exampleInputEmail1">Chinese Name</label>
                    <span style="color:red">(*)</span>
                    <input type="text" class="form-control" v-model="NameTW" placeholder="Enter Name" :class="{ 'is-invalid': submitted && $v.NameTW.$error }"/>
                    <div v-if="submitted && !$v.NameTW.required" class="invalid-feedback">Name is required</div>
                  </div>

                  <div class="form-group col-md-6">
                    <label for="exampleInputEmail1">English Name</label>
                    <span style="color:red">(*)</span>
                    <input type="text" class="form-control" v-model="NameEn" placeholder="Enter Name" :class="{ 'is-invalid': submitted && $v.NameEn.$error }"/>
                    <div v-if="submitted && !$v.NameEn.required" class="invalid-feedback">Name is required</div>
                  </div>
                </div>


                <div class="row">

                  <div class="form-group col-md-6">
                    <label for="exampleInputEmail1">Local Language</label>
                    <span style="color:red">(Default English Name)</span>
                    <input type="text" class="form-control" v-model="NameVI" placeholder="Enter Name" />
                  </div>
                  <div class="form-group col-md-6">
                    <label for="exampleInputEmail1">Code</label>
                    <span style="color:red">(*)</span>
                    <input
                      :class="{ 'is-invalid': submitted && $v.Code.$error }"
                      v-model="Code"
                      type="text"
                      class="form-control"
                      placeholder="Enter Code"
                    />
                    <div v-if="submitted && !$v.Code.required" class="invalid-feedback">Code is required</div>
                  </div>
                </div>


                <div class="row">

                  <div class="form-group col-md-6">
                    <label>KPI Kinds</label>
                    <span style="color:red">(*)</span>
                    <select v-model="Kind" class="form-control select2bs4 select2-hidden-accessible" style="width: 100%;" data-select2-id="17" tabindex="-1" aria-hidden="true" >
                      <option v-for="item in dataKind" :key="item.value" :label="item.Name" :value="item.ID"></option>
                    </select>
                  </div>

                  <div class="form-group col-md-6">
                    <label for="exampleInputEmail1">Level</label>
                    <span style="color:red">(*)</span>

                    <!-- <input
                      :class="{ 'is-invalid': submitted && $v.Level.$error }"
                      v-model="Level"
                      type="text"
                      class="form-control"
                      placeholder="Enter Level"
                    /> -->

                    <ejs-dropdowntree ref="ddtInstance" :changeOnBlur="false" @change='onchange' :fields='fields' :showCheckBox='true'></ejs-dropdowntree>

                    <div v-if="submitted && !$v.Level.required" class="invalid-feedback">Level is required</div>

                  </div>

                </div>

                <div class="form-group">
                  <small
                    style="color:red;font-weight:bold;text-align:center;display:block"
                  >(*) Require fields not to be blank.</small>
                </div>
              </div>
              <!-- /.card-body -->

              <div class="card-footer">
                <button type="submit" class="btn btn-success">Save</button>
                <button @click="resetForm" type="submit" class="btn btn-danger">Close</button>
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
import VueJwtDecode from "vue-jwt-decode";
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
  name: "CreateCategory",

  data() {
    return {
      fields: { dataSource: data, value: 'id', text: 'name', parentValue:"pid", hasChildren: 'hasChild' },
      Name: null,
      NameVI: null,
      NameEn: null,
      NameTW: null,
      Level: 0,
      Code: null,
      ListLevel: [],
      Kind: null,
      dataKind: [],
      locale: $cookies.get("Lang"),
      submitted: false
    };
  },

  validations: {
    // NameVI: { required },
    NameEn: { required },
    NameTW: { required },
    Level: { required },
    Code: { required }
  },

  created() {
    EventBus.$on("flag", locale => {
      this.locale = locale;
    });
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

    save() {

      this.submitted = true;
      // stop here if form is invalid
      if (this.NameVI == null) {
        this.NameVI = this.NameEn
      }
      this.$v.$touch();
      console.log('save',this.ListLevel);
      if (this.$v.$invalid) {
        return;
      }
      else {
        axios.post("admincategory/add2", {
          NameVI: this.NameVI,
          NameEn: this.NameEn,
          NameTW: this.NameTW,
          Code: this.Code,
          Cate_Kind_ID: this.Kind,
          LevelID: this.Level,
          LevelID2: this.ListLevel,
          UserID : VueJwtDecode.decode(localStorage.getItem("authToken")).nameid,
        })
        .then(response => {
          this.$router.push("/adminCategory");
          success("success!");
        })
        .catch(e => {
        });
      }
    },

    resetForm() {
      this.$router.go(-1);
    }

  }
};
</script>


