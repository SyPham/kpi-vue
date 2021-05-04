<template>
  <div class="row">
    <!-- left column -->
	<div class="container-fluid">
      <div class="row mb-2">
        <div class="col-sm-6"></div>
        <div class="col-sm-6">
          <ol class="breadcrumb float-sm-right">
            <li class="breadcrumb-item">
              <a href="#/home">{{$t('home')}}</a>
            </li>
            <li class="breadcrumb-item active">User</li>
          </ol>
        </div>
      </div>
    </div>	
    <div class="col-md-12">
      <!-- general form elements -->
      <div class="card card-primary">
        <div class="card-header">
          <h3 class="card-title">Add User</h3>
        </div>
        <!-- /.card-header -->
        <!-- form start -->
        <form @submit.prevent="save" role="form">
          <div class="card-body">
            <div class="form-group col-md-6">
              <label for="exampleInputEmail1">UserName</label>
              <span style="color:red">(*)</span>
              <input
                type="text"
                class="form-control"
                v-model="UserName"
                placeholder="Enter UserName"
                :class="{ 'is-invalid': submitted && $v.UserName.$error }"
              />
              <div v-if="submitted && !$v.UserName.required" class="invalid-feedback">Name is required</div>
            </div>
            <div class="form-group col-md-6">
              <label for="exampleInputEmail1">Password</label>
              <span style="color:red">(*)</span>
              <input
                :class="{ 'is-invalid': submitted && $v.Password.$error }"
                type="password"
                class="form-control"
                v-model="Password"
                placeholder="Enter Password"
              />
              <div
                v-if="submitted && !$v.Password.required"
                class="invalid-feedback"
              >Password is required</div>
            </div>
            <div class="form-group col-md-6">
              <label for="exampleInputEmail1">Email</label>
              <span style="color:red">(*)</span>
              <input
                :class="{ 'is-invalid': submitted && $v.Email.$error }"
                type="text"
                class="form-control"
                v-model="Email"
                placeholder="Enter Email"
              />
              <div
                v-if="submitted && !$v.Email.required"
                class="invalid-feedback"
              >Email is required</div>
            </div>
            <div class="form-group col-md-6">
              <label for="exampleInputEmail1">Display Name</label>
              <span style="color:red">(*)</span>
              <input
                :class="{ 'is-invalid': submitted && $v.Alilas.$error }"
                type="text"
                class="form-control"
                v-model="Alilas"
                placeholder="Enter Alilas"
              />
              <div
                v-if="submitted && !$v.Alilas.required"
                class="invalid-feedback"
              >Alilas is required</div>
            </div>
            <div class="form-group col-md-6">
              <label for="exampleInputEmail1">FullName</label>
              <span style="color:red">(*)</span>
              <input
                :class="{ 'is-invalid': submitted && $v.FullName.$error }"
                type="text"
                class="form-control"
                v-model="FullName"
                placeholder="Enter FullName"
              />
              <div
                v-if="submitted && !$v.FullName.required"
                class="invalid-feedback"
              >FullName is required</div>
            </div>
            <div class="form-group col-md-6">
              <label for="exampleInputEmail1">Skype</label>
              <input
                type="text"
                class="form-control"
                v-model="Skype"
                placeholder="Enter Skype"
              />
            </div>

            <div class="form-group col-md-6">
              <label for="exampleInputEmail1">Code</label>
              <input
                type="text"
                class="form-control"
                v-model="Code"
                placeholder="Enter Code"
              />
              
            </div>

            <div class="form-group col-md-6">
              <label>Permission</label>
              <span style="color:red">(*)</span>
              <select class="form-control select2bs4 select2-hidden-accessible" style="width: 100%;" data-select2-id="17" tabindex="-1" v-model="Permission" aria-hidden="true" :class="{ 'is-invalid': submitted && $v.Permission.$error }">
                <option v-for="item in data" :key="item.value" :label="item.Name" :value="item.ID"></option>
              </select>
              <div v-if="submitted && !$v.Permission.required" class="invalid-feedback">Permission is required</div>
            </div>
          </div>
          <!-- /.card-body -->

          <div class="card-footer">
            <button type="submit" class="btn btn-success">Save</button>
            <button @click="resetForm()" type="submit" class="btn btn-danger">Close</button>
          </div>
        </form>
      </div>
      <!-- /.card -->
    </div>
    <!--/.col (left) -->
  </div>
</template>

<script>
import { HTTP } from "../../http-constants";
import { required, email, minLength, sameAs } from "vuelidate/lib/validators";
import VueJwtDecode from "vue-jwt-decode";

export default {
  name: "CreateUser",
  data() {
    return {
      data: [],
      Userid: 0,
      UserName: null,
      Password: null,
      Email: "",
      Alilas: "",
      FullName: "",
      Skype: "",
      Code:"",
      Permission: null,
      submitted: false
    };
  },
  validations: {
    UserName: { required },
    Password: { required },
    Email: { required },
    Alilas: { required },
    FullName: { required },
    Permission: { required }
  },
  created() {
    let seft = this ;
    seft.GetListAllPermissions();
  },
  methods: {
    save() {
      this.submitted = true;
      // stop here if form is invalid
      this.$v.$touch();
      if (this.$v.$invalid) {
        return;
      } else {
        axios.post("AdminUser/add", {
          Username: this.UserName,
          Password: this.Password,
          Email: this.Email,
          Alias: this.Alilas,
          FullName: this.FullName,
          Skype: this.Skype,
          Code: this.Code,
          Permission: this.Permission,
          Role: this.Permission,
          CreatedBy: localStorage.getItem("User"),
          UserID: VueJwtDecode.decode(localStorage.getItem("authToken")).nameid
        })
          .then(response => {
            this.$router.push("/adminUser");
            swal.fire({
              title: "Success!",
              text: "Add User successfully!",
              type: "success"
            });
            // console.log()
          })
          .catch(e => {
           // console.error(e);
          });
      }
    },
    GetListAllPermissions() {
      axios.get(`AdminUser/GetListAllPermissions/${this.Userid}`)
        .then(r => {
          this.data = r.data;
         // console.log(r.data);
        })
        .catch(r => {
          // console.log(r);
        });
    },
    resetForm() {
      this.$router.push("/adminUser");
    }
  }
};
</script>