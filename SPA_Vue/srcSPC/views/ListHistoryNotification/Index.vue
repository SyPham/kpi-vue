<template>
<section class="content">
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-12">
            <div class="card">
                <div class="card-header">
                <h3 class="card-title">List Histories Notifications</h3>
                </div>
                <div class="card-header">
                
                </div>
            
                <!-- /.card-header -->
                <div class="card-body">
                <table class="table table-hover">
                    <thead>
                    <tr>
                        <th>No.</th>
                        <th>Notification</th>
                    </tr>
                    </thead>

                    <tbody v-for="(item,key,index) in data" :key="index">

                        <!-- Comment -->
                        <tr v-if="item.Action == 'Comment'">
                            <td style="width: 15px">{{key+1}}</td>
                            <td>
                                <a @click="gettask(item.Link)"  style="cursor: pointer">
                                    <p> <i class="far fa-check-circle"></i> {{(item.SenderID == userId ? "You" : item.Sender)}} mentioned {{(item.RecipientID == userId ? "You" : item.Recipient)}} in {{item.Title}}</p>
                                    <p>
                                        <b>Content: </b>
                                        <ul class="chart-legend clearfix">
                                            <li><i class="fa fa-check-circle-o text-green"></i> {{item.Content}}</li>
                                        </ul>
                                    </p>
                                    <p><i class="far fa-clock"></i>{{JSONDateWithTime(item.CreateTime)}}</p>
                                </a>
                            </td>
                        </tr>

                        <!-- Task -->
                        <tr v-if="item.Action == 'Task'">
                            <td>{{key+1}}</td>
                            <td>
                                <a @click="gettask(item.Link)"  style="cursor: pointer">
                                    <p> <i class="far fa-check-circle"></i> {{(item.SenderID == userId ? "You" : item.Sender)}} mentioned {{(item.RecipientID == userId ? "You" : item.Recipient)}} in {{item.Title}}</p>

                                    <p>
                                        <b>Content: </b>
                                        <ul class="chart-legend clearfix">
                                            <li><i class="fa fa-check-circle-o text-green"></i> {{item.Content}}</li>
                                        </ul>
                                    </p>
                                    <p><b>Due Date: </b>{{JSONDateWithTime(item.Deadline)}}</p>
                                    <p><i class="far fa-clock"></i>{{JSONDateWithTime(item.CreateTime)}}</p>

                                </a>
                            </td>
                        </tr>

                        <!-- Done -->
                        <tr v-if="item.Action == 'Done'">
                            <td>{{key+1}}</td>
                            <td>
                                <a @click="gettask(item.Link)"  style="cursor: pointer">
                                    <p><i class="far fa-check-circle"></i> {{(item.SenderID == userId ? "You" : item.Sender)}} updated the status in {{item.Title}}</p>
                                    <p><i class="far fa-clock"></i> {{JSONDateWithTime(item.CreateTime)}}</p>
                                </a>
                            </td>
                        </tr>

                        <!-- Approval -->
                        <tr v-if="item.Action == 'Approval'">
                            <td>{{key+1}}</td>
                            <td>
                                <a @click="gettask(item.Link)"  style="cursor: pointer">
                                    <p><i class="far fa-check-circle"></i> {{(item.SenderID == userId ? "You" : item.Sender)}} approved in {{item.Title}}</p>
                                    <p><i class="far fa-clock"></i>{{JSONDateWithTime(item.CreateTime)}}</p>
                                </a>
                            </td>
                        </tr>

                        <!-- LateOnTask -->
                        <tr v-if="item.Action == 'LateOnTask'">
                            <td>{{key+1}}</td>
                            <td>
                                <a @click="gettask(item.Link)"  style="cursor: pointer">
                                    <p><i class="far fa-check-circle"></i> From System</p>
                                    <p><i class="far fa-clock"></i> {{JSONDateWithTime(item.CreateTime)}}</p>
                                </a>
                            </td>
                        </tr>

                        <!-- LateOnUploadData -->
                        <tr v-if="item.Action == 'LateOnUploadData'">
                            <td>{{key+1}}</td>
                            <td>
                                <a @click="gettask(item.Link)"  style="cursor: pointer">
                                    <p><i class="far fa-check-circle"></i>From System</p>
                                    <p><i class="far fa-clock"></i> {{JSONDateWithTime(item.CreateTime)}}</p>
                                </a>
                            </td>
                        </tr>
                    </tbody>
                </table>
                </div>
                <div class="card-footer clearfix">
                <Paginate
                    v-model="page"
                    :page-count="totalPage"
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
                </div>
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
export default {
    data(){
        return {
            data:[],
            totalPage: 0,
            page: 1,
            skip: 0,
            pageSize: 5,
            userId: 0
        }
    },
    components: {
        Paginate
    },
    created() {
        let self = this
        self.LoadAll()
        self.userId = VueJwtDecode.decode(localStorage.getItem("authToken")).nameid
    },
    methods: {
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
        changePage(pageNum) {
         this.LoadAll(this.name, pageNum);
        },
        LoadAll(){
            let self = this
            axios.get("Home/ListHistoryNotification")
            .then(res=>{
                self.data = res.data
            })
        },
        gettask(Link) {
            EventBus.$emit('item',Link);   
            return this.$router.push(Link)
        }
    }
}
</script>