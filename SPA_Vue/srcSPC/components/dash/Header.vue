<template>
  <div id="appHeader" >
     <!-- <ejs-toast  :position='position' ref='defaultRef' cssClass= 'e-toast-success' title='Matt sent you a friend request' timeOut=0 content='Hey, wanna dress up as wizards and ride our hoverboards?'></ejs-toast> -->

    <!-- Navbar -->
    <nav  class="main-header navbar navbar-expand navbar-white navbar-light">
      <!-- <ul class="navbar-nav">
        <li class="nav-item">
          <a class="nav-link" data-widget="pushmenu" href="#"
            ><i class="fas fa-bars"></i
          ></a>
        </li>

      </ul> -->

      <!-- Right navbar links -->
      <ul  class="navbar-nav ml-auto">
        <!-- Messages Dropdown Menu -->
        <!-- <button v-for="entry in languages" :key="entry.title" >
          <flag :iso="entry.flag" v-bind:squared=false />
            {{entry.title}}
        </button> -->

        <!-- language -->

        <li>
          <div class="henry-pham">
            <ul>
              <li
                v-for="entry in languages"
                :key="entry.title"
                @click="changeLocale(entry.language)"
                :title="entry.title"
                >
                <!-- <flag
                  style="fontsize:"
                  :iso="entry.flag"
                  v-bind:squared="false"
                /> -->
                {{ entry.title }}
              </li>
            </ul>
          </div>
        </li>

        <!-- time -->
        <li class="nav-item">
          <a class="nav-link">
            <!-- <i class="fas fa-clock"></i> -->
            <span class="font-weight-bold text-info">{{currentTime}}</span>
          </a>
        </li>

        <!-- notification -->
        <li  class="nav-item dropdown">
          <a class="nav-link" data-toggle="dropdown" href="#" aria-expanded="false" data-id="123">
            <i class="far fa-bell"></i>
            <h6><span class="badge badge-warning navbar-badge">{{listdata.total}}</span></h6>
          </a>

          <ul class="dropdown-menu dropdown-menu-lg2 dropdown-menu-right" style="border-radius: 11px" >
            <span class="dropdown-item dropdown-header">You have {{ listdata.total }} Notifications</span>
            <div  class="dropdown-divider"></div>

            <li>
              <ul style="list-style: none ; padding: 0 ; margin: 0 ; overflow-x: scroll ; height:240px"  class="box-scrollbar">
                <li v-for="(item, key, index) in data" :key="index">

                    <!-- comment -->
                    <a v-if="item.Action == 'Comment' && item.SenderID !== userid "  @click="getcomment(item,item.Link)"   class="dropdown-item" style="cursor: pointer" :data-id="item.ID" >
                      <h6 v-if="item.Seen === false"><span class="badge bg-green">New</span> Comment <i class="fas fa-comment"></i><small class="float-right"><i class="far fa-clock"></i> {{JSONDateWithTime(item.CreateTime)}} </small></h6>
                      <h6 v-else> Comment <i class="fas fa-comment"></i><small class="float-right"><i class="far fa-clock"></i> {{JSONDateWithTime(item.CreateTime)}} </small></h6>
                      <p style="font-size:14px">The account {{item.SenderID == userid ? "you" : item.Sender}} mentioned {{item.RecipientID == userid ? "you" : item.Recipient}} in {{item.Title}}</p>
                      <p>{{item.Content}}</p>
                    </a>

                    <div  class="dropdown-divider"></div>

                    <!-- Task -->
                    <a v-if="item.Action == 'Task' && item.SenderID !== userid " @click="gettask(item)"   class="dropdown-item" style="cursor: pointer" :data-id="item.ID" >
                      <h6 v-if="item.Seen === false" ><span class="badge bg-green">New</span> Add Task <i class="fas fa-tasks"></i><small class="float-right"><i class="far fa-clock"></i> {{JSONDateWithTime(item.CreateTime)}} </small></h6>
                      <h6 v-else > Add Task <i class="fas fa-tasks"></i><small class="float-right"><i class="far fa-clock"></i> {{JSONDateWithTime(item.CreateTime)}} </small></h6>
                      <p style="font-size:14px">The account {{item.Sender}} assigned  {{item.RecipientID == userid ? "you" : item.Recipient}} the task {{item.TaskName}} </p>
                      <p>{{item.Title}}</p>
                      <p>KPI - {{item.KPIName}}</p>
                    </a>

                     <!-- Task-Auditor -->
                    <a v-if="item.Action == 'Task-Auditor' && item.SenderID !== userid " @click="gettask(item)" class="dropdown-item" style="cursor: pointer" :data-id="item.ID" >
                      <h6 v-if="item.Seen === false"><span class="badge bg-green">New</span> Add Task (Auditor) <i class="fas fa-tasks"></i><small class="float-right"><i class="far fa-clock"></i> {{JSONDateWithTime(item.CreateTime)}} </small></h6>
                      <h6 v-else> Add Task (Auditor) <i class="fas fa-tasks"></i><small class="float-right"><i class="far fa-clock"></i> {{JSONDateWithTime(item.CreateTime)}} </small></h6>
                      <p class="wordWrap">The account {{item.Sender}} created the task "{{item.TaskName}}", assigned to {{item.RecipientID === userid ? "you" : item.Recipient}} are Auditor. </p>
                      <p class="wordWrap">{{item.Title}}</p>
                      <p class="wordWrap">KPI Name {{item.KPIName}}</p>
                    </a>

                    <!-- Done -->
                    <a v-if="item.Action == 'Done' && item.SenderID !== userid " @click="gettask(item)" class="dropdown-item" style="cursor: pointer" :data-id="item.ID" >
                      <h6 v-if="item.Seen === false"><span  class="badge bg-green">New</span> Update Task Status <i class="fas fa-tasks"></i><small class="float-right"><i class="far fa-clock"></i> {{JSONDateWithTime(item.CreateTime)}} </small></h6>
                      <h6 v-else> Update Task Status <i class="fas fa-tasks"></i><small class="float-right"><i class="far fa-clock"></i> {{JSONDateWithTime(item.CreateTime)}} </small></h6>
                      <p style="font-size:14px">The account {{item.Sender}} has finished the task {{item.TaskName}}</p>
                      <p>{{item.Title}}</p>
                      <p>KPI - {{item.KPIName}}</p>
                    </a>

                    <!-- Approval -->
                    <a v-if="item.Action == 'Approval' && item.SenderID !== userid " @click="gettask(item)" class="dropdown-item" style="cursor: pointer" :data-id="item.ID" >
                      <h6 v-if="item.Seen === false"><span  class="badge bg-green">New</span> Approval Task  <i class="fas fa-tasks"></i><small class="float-right"><i class="far fa-clock"></i> {{JSONDateWithTime(item.CreateTime)}} </small></h6>
                      <h6 v-else> Approval Task  <i class="fas fa-tasks"></i><small class="float-right"><i class="far fa-clock"></i> {{JSONDateWithTime(item.CreateTime)}} </small></h6>
                      <p style="font-size:14px">Your task {{item.TaskName}} was approved by {{item.Sender}}</p>
                    </a>

                    <!-- UpdateApproval -->
                    <a v-if="item.Action == 'UpdateApproval' && item.SenderID !== userid " @click="gettask(item)" class="dropdown-item" style="cursor: pointer" :data-id="item.ID" >
                      <h6 v-if="item.Seen === false"><span  class="badge bg-green">New</span> Update Approval Task <i class="fas fa-tasks"></i><small class="float-right"><i class="far fa-clock"></i> {{JSONDateWithTime(item.CreateTime)}} </small></h6>
                      <h6 v-else> Update Approval Task <i class="fas fa-tasks"></i><small class="float-right"><i class="far fa-clock"></i> {{JSONDateWithTime(item.CreateTime)}} </small></h6>
                      <p style="font-size:14px">The account {{item.Sender}} hasn't approved status the task {{item.TaskName}}</p>
                    </a>

                     <!-- REMARK -->
                    <a v-if="item.Action == 'Remark' && item.SenderID !== userid " @click="gettask(item)" class="dropdown-item" style="cursor: pointer" :data-id="item.ID" >
                      <h6 v-if="item.Seen === false"><span  class="badge bg-green">New</span> Remark Task <i class="fas fa-tasks"></i><small class="float-right"><i class="far fa-clock"></i> {{JSONDateWithTime(item.CreateTime)}} </small></h6>
                      <h6 v-else> Remark Task <i class="fas fa-tasks"></i><small class="float-right"><i class="far fa-clock"></i> {{JSONDateWithTime(item.CreateTime)}} </small></h6>
                      <p style="font-size:14px">The account {{item.Sender}} has changed Remark the task {{item.TaskName}}</p>
                      <p>{{item.Content}}</p>
                      <p>KPI - {{item.KPIName}}</p>
                    </a>

                    <!-- LateOnTask -->
                    <a v-if="item.Action == 'LateOnTask' " href="#/Workplace" class="dropdown-item" style="cursor: pointer" :data-id="item.ID" >
                      <h6 v-if="item.Seen === false"><span  class="badge bg-green">New</span> Late On Task  <i class="fas fa-tasks"></i><small class="float-right"><i class="far fa-clock"></i> {{JSONDateWithTime(item.CreateTime)}} </small></h6>
                      <h6 v-else> Late On Task  <i class="fas fa-tasks"></i><small class="float-right"><i class="far fa-clock"></i> {{JSONDateWithTime(item.CreateTime)}} </small></h6>
                      <p style="font-size:14px">Some of task are overdue. Please check your email</p>
                    </a>

                    <!-- LateOnUploadData -->
                    <a v-if="item.Action == 'LateOnUploadData' " @click="getnotifi(item,item.NotificationID)"  style="cursor: pointer" class="dropdown-item" :data-id="item.ID" >
                      <h6 v-if="item.Seen === false"><span  class="badge bg-green">New</span> Late On Upload Data  <i class="fas fa-tasks"></i><small class="float-right"><i class="far fa-clock"></i> {{JSONDateWithTime(item.CreateTime)}} </small></h6>
                      <h6 v-else>Late On Upload Data  <i class="fas fa-tasks"></i><small class="float-right"><i class="far fa-clock"></i> {{JSONDateWithTime(item.CreateTime)}} </small></h6>
                      <p style="font-size:14px">There are some KPIs that haven't uploaded their data on time. Please check your email</p>
                    </a>

                    <!-- Upload -->
                    <a v-if="item.Action == 'Upload' || item.SenderID === userid || item.RecipientID === userid || item.UserID === userid " @click="seen(item,index)" href="#/Workplace" class="dropdown-item" style="cursor: pointer" :data-id="item.ID" >
                      <h6 v-if="item.Seen === false"><span  class="badge bg-green">New</span> Upload Successfully <small class="float-right"><i class="far fa-clock"></i> {{JSONDateWithTime(item.CreateTime)}} </small></h6>
                      <h6 v-else> Upload Successfully <small class="float-right"><i class="far fa-clock"></i> {{JSONDateWithTime(item.CreateTime)}} </small></h6>
                      <p style="font-size:14px">{{item.SenderID == userid ? "You" : "The account " + item.Sender}} has uploaded KPIs data successfully!</p>
                    </a>
                </li>
              </ul>
            </li>
            <div class="dropdown-divider"></div>

            <a href="#/ListHistoryNotification" style="text-align: center" class="dropdown-item dropdown-footer">See All Notifications</a>
          </ul>
        </li>

        <!-- changepassword -->
        <li  class="nav-item dropdown">
          <a  class="nav-link" data-toggle="dropdown" href="#">
            {{username}}
          </a>
          <div class="dropdown-menu dropdown-menu-lg dropdown-menu-right">
            <a  style="cursor:pointer" data-toggle="modal" data-target="#modal-group-change-password" class="dropdown-item">
              <i class="fas fa-key"></i> Change Password
            </a>
            <div class="dropdown-divider"></div>
            <!-- <a href="" @click.prevent="logout" class="dropdown-item">
              <i class="fas fa-sign-out-alt"></i> Logout
            </a> -->
            <!-- <div class="dropdown-divider"></div> -->
          </div>
        </li>

        <!-- logout -->
        <li >
          <a class="nav-link"  href="" @click.prevent="logout">
            <i class="fas fa-sign-out-alt"></i> Sign-out
          </a>
        </li>

      </ul>

    </nav>
    <div class="modal fade" id="modal-group-change-password" style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Change password </h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div>
                        <div class="box-body" id="ChangePassword">
                            <div class="form-group">
                                <label for="username">Username </label>
                                <input type="text"  v-model="user" class="form-control username disabled" placeholder="username" disabled>
                            </div>
                            <!-- <div class="form-group">
                                <label for="password">Password </label>
                                <input type="password" v-model="password" class="form-control password" placeholder="Enter password">

                              <i class="fa fa-eye-slash" aria-hidden="true"></i>

                            </div> -->
                            <div data-v-1d03bb73="" class="input-group mb-3">
                              <input  :type="type" v-model="password" class="form-control password" placeholder="Enter password">
                              <div data-v-1d03bb73="" class="input-group-append">
                              <div data-v-1d03bb73="" class="input-group-text">
                                <span style="cursor:pointer" >
                                  <i :class="fonticon" @click="showPassword" aria-hidden="true"></i>
                                </span>
                                </div>
                              </div>
                            </div>

                        </div>
                        <!-- /.box-body -->

                        <div class="box-footer">
                            <button id="btnChangePassword" @click="changepassword" class="btn btn-primary">Save</button>
                            <button type="button" class="btn btn-default pull-right" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>

            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
  </div>

</template>

<script>
import axios from 'axios';
import VueJwtDecode from "vue-jwt-decode";
import i18n from "../../lang/i18n";
import signal from "../../hub";
import EventBus from "../../utils/EventBus.js";
import VueCookies from 'vue-cookies'
import moment from "moment-timezone";
export default {
  name: "appHeader",
  data: function() {
    return {
      position:  { X: 'Right', Y: 'Bottom' },
      fonticon: "fa fa-eye-slash",
      type:'password',
      currentTime: null,
      userid: 0,
      username: null,
      user: '',
      password : null,
      connection:{},
      time: 0,
      arrayID: [],
      data: [],
      listdata: [],
      languages: [
        { flag: "tw", language: "zh-TW", title: "中文" },
        { flag: "us", language: "en", title: "English" },
        { flag: "vn", language: "vi", title: "Local language" }
      ],
      locale: $cookies.get("Lang"),
      connection: ''
    }
  },
  mounted() {
    let seft = this
    seft.userid = VueJwtDecode.decode(localStorage.getItem("authToken")).nameid;
    // seft.$refs.defaultRef.show();

  },
  created(){
    let seft = this;
    seft.currentTime = moment().format("LTS");
    setInterval(() => seft.updateCurrentTime(), 1 * 1000);
    seft.userid = VueJwtDecode.decode(localStorage.getItem("authToken")).nameid;
    seft.username  =  localStorage.getItem("User")
    seft.user =  localStorage.getItem("Username")
    seft.getAllNotifications();
    // Listen to score changes coming from SignalR events
    seft.connection = new signalR.HubConnectionBuilder()
    .withUrl("http://10.10.0.34:91/henry-hub")
    .configureLogging(signalR.LogLevel.Information)
    .build();
    seft.connection.start().then(function () {
    });
    seft.connection.on("ReceiveMessage", (user, message) => {
      seft.getAllNotifications();
      for (let item of user.notifiUserID) {
        if(item.userID == VueJwtDecode.decode(localStorage.getItem("authToken")).nameid) {
          seft.$toast.open({
            message: "You have a new Message",
            type: "success",
            duration: 5000,
            dismissible: true,
            position: 'top-right'
          })
        }
      }

    });
  },
  beforeDestroy (){
    this.connection.off("ReceiveMessage")
  },
  methods: {
    showPassword(){
      if(this.type === 'password') {
        this.type = 'text'
        this.fonticon = 'fa fa-eye'
       } else {
        this.type = 'password'
        this.fonticon = 'fa fa-eye-slash'
       }
    },
    updateCurrentTime() {
      this.currentTime = moment().format("LTS");
    },
	  seen(item,index){
      axios.post(`/Notification/Update/${item.ID}`).then(r=>{
        this.getAllNotifications();
      })
    },
    getcomment(data,Link){
      EventBus.$emit('item',data);
      axios.post(`/Notification/Update/${data.ID}`).then(r=>{
        this.getAllNotifications();
      })
     return this.$router.push(data.Link)
    },
    gettask(data2){
      EventBus.$emit('hello', data2);
      axios.post(`/Notification/Update/${data2.ID}`).then(r=>{
        this.getAllNotifications();
      })
      return this.$router.push(data2.URL)

    },
    getnotifi(item,NotificationID){
      EventBus.$emit('hello', NotificationID);
	    axios.post(`/Notification/Update/${item.ID}`).then(r=>{
        this.getAllNotifications();
      })
      return this.$router.push(`/LateOnUpload/${NotificationID}`)
    },
    changepassword(){
      let mObj = {
        Username: this.user ,
        Password: this.password,
      }
      axios.post("AdminUser/ChangePassword",mObj).then(r=>{
        success("ChangePass successfully! Please Login again");
        $("#modal-group-change-password").modal("hide");
        this.$auth.destroyToken();
        var uri = this.$route.path;
        this.$router.push({ path: '/login', query: { redirect: uri } });
      })
    },
    JSONDateWithTime(dateStr){
      var d = new Date(dateStr);
      var m, day;
      m = d.getMonth() + 1;
      if (m < 10)
          month = '0' + m;
      else
          month = m;
      if (d.getDate() < 10)
          day = '0' + d.getDate();
      else
          day = d.getDate();

      var year = d.getFullYear();
      var formattedDate = day + "/" + month + "/" + year;
      var hours = d.getHours() < 10 ? "0" + d.getHours() : d.getHours();
      var minutes = d.getMinutes() < 10 ? "0" + d.getMinutes() : d.getMinutes();
      var seconds = d.getSeconds();
      var formattedTime = hours + ":" + minutes;
      formattedDate = formattedTime + " - " + formattedDate;

      //Ngày giờ hiện tại
      var dateObj = new Date();
      var dayNow = dateObj.getDate();
      var monthNow = dateObj.getMonth() + 1;
      var yearNow = dateObj.getFullYear();
      var hoursNow = dateObj.getHours();
      var minutesNow = dateObj.getMinutes();
      var secondsNow = dateObj.getSeconds();

      dateObj = formattedDate;

      if (hoursNow - hours === 0 && minutesNow - minutes === 0 && yearNow - year === 0 && monthNow - month === 0 && dayNow - day === 0) {
          if (secondsNow - seconds === 0)
              dateObj = "just recently";
          else if (secondsNow - seconds === 1)
              dateObj = secondsNow - seconds + " second ago";
          else
              dateObj = secondsNow - seconds + " seconds ago";
      }

      if (hoursNow - hours === 0 && minutesNow - minutes > 0 && yearNow - year === 0 && monthNow - month === 0 && dayNow - day === 0) {


          if (minutesNow - minutes === 1)
              dateObj = minutesNow - minutes + " minute ago";
          else
              dateObj = minutesNow - minutes + " minutes ago";
      }

      if (hoursNow - hours > 0 && yearNow - year === 0 && monthNow - month === 0 && dayNow - day === 0) {
          if (hoursNow - hours === 1)
              dateObj = hoursNow - hours + " hour ago";
          else
              dateObj = hoursNow - hours + " hours ago";
      }

      if (yearNow - year === 0 && monthNow - month === 0 && dayNow - day > 0 && dayNow - day <= 7) {
          if (dayNow - day === 1)
              dateObj = dayNow - day + " day ago";
          else
              dateObj = dayNow - day + " days ago";
      }

      if (yearNow - year === 0 && monthNow !== month) {
          day = dayNow - day;
          var month = monthNow - month;
          day = month * 30 + day;
          if (0 < day && day <= 7) {
              dateObj = day + " days ago";
          }

      }

      return dateObj;
    },
    onScoreChanged({ user, message }) {
    },
    changeLocale(locale) {
      i18n.locale = locale;
      $cookies.set("Lang", locale)
      EventBus.$emit('flag', locale);
    },
    getAllNotifications() {
      let seft = this;
      axios.get("/Home/GetNotifications").then(r=>{
        seft.arrayID = r.data.arrayID;

        seft.data = r.data.data;
        seft.listdata = r.data;
        seft.userid = VueJwtDecode.decode(localStorage.getItem("authToken")).nameid;

      })
    },
    logout: function() {
      this.$auth.destroyToken();
      // this.user = {};
      var uri = this.$route.path;
      this.$router.push({ path: '/login', query: { redirect: uri } });
      success("Sign-out successfully!");
      // axios.post(`api/Auth/UpdateIsOnline/${this.userid}`).then(res =>{
      // })
    }
  }
};
</script>

<style lang="scss" scoped>
.box-scrollbar::-webkit-scrollbar-track {
    -webkit-box-shadow: inset 0 0 6px rgba(0,0,0,0.3);
    background-color: #F5F5F5;
    border-radius: 5px
}

.box-scrollbar::-webkit-scrollbar {
    width: 10px;
    background-color: #F5F5F5;
    border-radius: 5px;
}

.box-scrollbar::-webkit-scrollbar-thumb {
    background-color: black;
    border: 2px solid black;
    border-radius: 5px
}

.box-notification {
    -webkit-box-shadow: 10px 10px 23px 0px rgba(0,0,0,0.2);
    -moz-box-shadow: 10px 10px 23px 0px rgba(0,0,0,0.1);
    box-shadow: 10px 10px 23px 0px rgba(0,0,0,0.1);
    background-color: #F4F4F4;
    border-radius: 10px;
}


.boxdataset-scrollbar::-webkit-scrollbar-track {
    -webkit-box-shadow: inset 0 0 6px rgba(0,0,0,0.3);
    background-color: #F5F5F5;
    border-radius: 5px
}

.boxdataset-scrollbar::-webkit-scrollbar {
    width: 10px;
    background-color: #F5F5F5;
    border-radius: 5px
}

.boxdataset-scrollbar::-webkit-scrollbar-thumb {
    background-color: #808080;
    border: 2px solid #808080;
    border-radius: 5px
}

.textOverflow {
    white-space: nowrap;
    max-width: 250px;
    overflow: hidden;
    text-overflow: ellipsis;
}

.labelOverflow {
    overflow: hidden;
    text-overflow: ellipsis;
}
a.dropdown-item:hover {
    background: gainsboro;
}
.dropdown-menu-lg2 {
    max-width: 361px;
    min-width: 700px;
}
button {
  border: 1px solid green;
  font-size: 15px;
  margin: auto;
}

.henry-pham {
  padding-right: 12px;
  padding-left: 12px;
  display: block;
}

.henry-pham ul > li > :hover {
  background-color: rgba(60, 105, 101, 0.5);
}

.henry-pham li {
  display: inline-block;
  height: 40px;
  line-height: 40px;
  cursor: pointer;
  margin-left: 13px;
}

.henry-pham li img {
  width: 29px;
}

.ulbadge {
  position: relative;
  background: rgba(0, 0, 0, 0.1);
  display: block;
  cursor: pointer;
  text-decoration: none;
}

.ulbadge > .spabbadge {
  position: absolute;
  top: 0;
  transform: translate(70%, 30%);
}
</style>
