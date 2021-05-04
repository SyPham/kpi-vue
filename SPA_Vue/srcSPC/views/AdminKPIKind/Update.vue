<template>
<section class="content">
    <div class="container-fluid">
      <div class="row">
        <!-- left column -->
        <div class="col-md-12">
          <!-- general form elements -->
          <div class="card card-primary">
            <div class="card-header">
              <h3 class="card-title">Update KPI</h3>
            </div>
            <!-- /.card-header -->
            <!-- form start -->
            <form @submit.prevent="update" role="form">
              <div class="card-body">
                <div class="form-group col-md-6">
                  <label for="exampleInputEmail1">Chinese Name</label>
                  <input type="text" class="form-control" id="exampleInputEmail1" placeholder="Enter Name" v-model="NameTW"/>
                </div>
                <div class="form-group col-md-6">
                  <label for="exampleInputEmail1">English Name</label>
                  <input type="text" class="form-control" id="exampleInputEmail1" placeholder="Enter Name" v-model="NameEn"/>
                </div>
                <div class="form-group col-md-6">
                  <label for="exampleInputEmail1">Local Language</label>
                  <input type="text" class="form-control" id="exampleInputEmail1" placeholder="Enter Name" v-model="NameVI"/>
                </div>
                <div class="form-group col-md-6">
                  <label>Unit</label>
                  <select class="form-control select2bs4 select2-hidden-accessible" style="width: 100%;" data-select2-id="17" tabindex="-1" v-model="Unit" aria-hidden="true">
                    <option v-for="item in data" :key="item.value" :label="item.Name" :value="item.ID"></option>
                  </select>
                </div>
                <div class="form-group col-md-6">
                  <label>KPI Scores</label>
                  <select class="form-control select2bs4 select2-hidden-accessible" style="width: 100%;" data-select2-id="17" tabindex="-1" v-model="Over" aria-hidden="true">
                    <option value="1">Yes</option>
                    <option value="0">No</option>
                  </select>
                </div>
              </div>
              <!-- /.card-body -->

              <div class="card-footer">
                <button  type="submit" class="btn btn-success">Save</button>
                <button @click="resetForm"  class="btn btn-danger">Close</button>
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
export default {
  name: "UpdateKPI",
  data() {
    return {
      data: [],
      ID:0,
      Name: null,
      NameVI: null,
      NameEn: null,
      NameTW: null,
      LevelID: 0,
      Unit: "",
      Over: 0
    };
  },
  created() {
    this.getUnit();
    this.get(this.$route.params.id);
    this.ID = this.$route.params.id;
  },
  methods: {
    getUnit() {
      axios.get("AdminKPI/getallunit",{
        headers:{
          Authorization: 'Bearer '+ localStorage.getItem("authToken")
        }
      })
        .then(r => {
          this.data = r.data;
        })
        .catch(r => {
        });
    },
    resetForm() {
      this.$router.go("/AdminKPI");
    },
    get(id) {
      if (id == undefined) return;
        axios.get("AdminKPI/GetbyID/"+ id,{
          headers:{
            Authorization: 'Bearer '+ localStorage.getItem("authToken")
          }
        })
        .then(r => {
          this.NameVI = r.data.NameVI
          this.NameEn = r.data.NameEn
          this.NameTW = r.data.NameTW
          this.LevelID = r.data.LevelID
          this.Unit = r.data.Unit
          this.Over = this.converStatus(r.data.Status)
        })
        .catch(r => {

        });
    },
    converStatus(status){
     if(status == true) {
       return 1
     }
     else {
       return 0
     }
    },
    update() {
      axios.post("AdminKPI/Update",{
        ID: this.ID,
        NameVI: this.NameVI,
        NameEn: this.NameEn,
        NameTW: this.NameTW,
        LevelID: this.LevelID,
        Unit: this.Unit,
        Status: parseInt(this.Over),
        headers:{
          Authorization: 'Bearer '+ localStorage.getItem("authToken")
        }
      })
      .then(r => {
        this.$router.go(-1);
        success("success!");
      })
      .catch(e => {
        warning("error!");
      });
    }
  }
};
</script>
