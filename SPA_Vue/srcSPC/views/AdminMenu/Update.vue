<template>
  <div class="row">
    <!-- left column -->
    <div class="col-md-12">
      <!-- general form elements -->
      <div class="card card-primary">
        <div class="card-header">
          <h3 class="card-title">Update Menu</h3>
        </div>
        <!-- /.card-header -->
        <!-- form start -->
        <form @submit.prevent="update" role="form">
          <div class="card-body">
            <div class="form-group col-md-6">
              <label for="exampleInputEmail1">Name</label>
              <input
                type="text"
                v-model="Name"
                class="form-control"
                id="exampleInputEmail1"
                placeholder="Enter Name"
              />
            </div>
            <div class="form-group col-md-6">
              <label for="exampleInputEmail1">Link</label>
              <input
                type="text"
                v-model="Link"
                class="form-control"
                placeholder="Enter Code"
              />
            </div>
            <div class="form-group col-md-6">
              <label for="exampleInputEmail1">FontAwesome</label>
              <input
                type="text"
                v-model="FontAwesome"
                class="form-control"
                placeholder="Enter Level"
              />
            </div>
            <div class="form-group col-md-6">
              <label for="exampleInputEmail1">BackgroudColor</label>
              <input
                type="text"
                v-model="BackgroudColor"
                class="form-control"
                placeholder="Enter Level"
              />
            </div>
            <div class="form-group col-md-6">
              <label>Permission</label>
              <select
                class="form-control select2bs4 select2-hidden-accessible"
                style="width: 100%;"
                data-select2-id="17"
                v-model="Permission"
                tabindex="-1"
                aria-hidden="true"
              >
                <option
                v-for="item in data"
                :key="item.value"
                :label="item.permissionName"
                :value="item.id" >
                </option>

              </select>

            </div>
            <div class="form-group col-md-6">
              <label for="exampleInputEmail1">Position</label>
              <input
                type="number"
                v-model="Position"
                class="form-control"
                placeholder="Enter Level"
              />
            </div>
          </div>
          <!-- /.card-body -->

          <div class="card-footer">
            <button type="submit" class="btn btn-primary">Save</button>
            <button type="submit" class="btn btn-primary">Close</button>
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
import VueJwtDecode from "vue-jwt-decode";
export default {
  name: "UpdateMenu",
  data(){
    return {
      data:[],
      ID:0,
      Name: null,
      Link: "",
      FontAwesome: "",
      BackgroudColor: "",
      Permission: "",
      Position: ""
    }
  },
  created(){
    this.get(this.$route.params.id);
    this.ID = this.$route.params.id;
    this.getpermission();
  },
  methods : {
    get(id) {
      if (id == undefined) return;
        axios.get("menus/getbyid/" + id,{
          headers:{
            Authorization: 'Bearer '+ localStorage.getItem("authToken")
          }
        })
        .then(r => {
          this.Name = r.data.name
          this.Link = r.data.link
          this.FontAwesome = r.data.fontAwesome
          this.BackgroudColor = r.data.backgroudColor
          this.Position = r.data.position
          this.Permission = r.data.permission
        })
        .catch(r => {

        });
    },
    getpermission() {
      axios.get("menus/getpermissions",{
        headers:{
          Authorization: 'Bearer '+ localStorage.getItem("authToken")
        }
      })
      .then(r => {
        this.data = r.data
      })
    },
    update() {
      axios.post("menus/update",{
        id: this.ID,
        name: this.Name,
        link: this.Link,
        fontAwesome: this.FontAwesome,
        backgroudColor: this.BackgroudColor,
        position: this.Position,
        permission: this.Permission,
        headers:{
          Authorization: 'Bearer '+ localStorage.getItem("authToken")
        }
      }).then(r => {
          this.$router.push("/menus")
          swal.fire({
            title: "Success!",
            text: "Update successfully!",
            type: "success"
          });
      })
    },
  }
};
</script>
