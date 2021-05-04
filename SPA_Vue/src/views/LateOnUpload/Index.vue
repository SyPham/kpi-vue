<template>
    <section class="content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-12">
                <div class="card">
                    <div class="card-header">
                    <h3 class="card-title">List Tasks</h3>
                    </div>
                    <div class="card-header">
                    
                    </div>
                
                    <!-- /.card-header -->
                    <div class="card-body">
                    <table class="table table-bordered">
                        <thead>
                        <tr>
                            <th>No.</th>
                            <th>OC</th>
                            <th>DeadLine</th>
                            <th>KPI Name</th>
                            <th>Code</th>
                            <th>Year</th>
                        </tr>
                        </thead>
                        <tbody>
                        <tr v-for="(kpi,key,index) in data" :key="index">
                            <td>{{key+1}}</td>
                            <td>{{kpi.Area}}</td>
                            <td>{{kpi.DeadLine}}</td>
                            <td>{{kpi.KPIName}}</td>
                            <td>{{kpi.Code}}</td>
                            <td>{{kpi.Year}}</td>
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
import VueJwtDecode from 'vue-jwt-decode'
import EventBus from "../../utils/EventBus.js";
export default {
    data(){
        return {
            NotificationID: this.$route.params.notification,
            data: [],
            totalPage: 0,
            page: 1,
            skip: 0,
            pageSize: 5,
            notificationID: this.$route.params.notification
        }
    },
    components:{
        Paginate
    },
    watch:{
        NotificationID: function(newVal, oldVal) {
            let self = this;
            this.$router.replace({
                name: "notification",
                params: { notification: self.NotificationID }
            });
            self.LoadAll();
        }   
    },
    created(){
        let self = this
        self.LoadAll()
        let currentUrl = self.$router.currentRoute;
    },
    methods:{
        changePage(pageNum) {
         this.LoadAll(this.name, pageNum);
        },
        LoadAll(){
            let self = this 
            EventBus.$on('hello', NotificationID => {
                self.NotificationID = NotificationID;
            });
            axios.post(`Home/LateOnUpload/${self.NotificationID}/${self.page}/${self.pageSize}`)
            .then(res=>{
                self.totalPage = res.data.pageCount;
                self.page = res.data.page;
                self.pageSize = res.data.pageSize;
                self.data = res.data.data
            })
            
        }
    }
}
</script>