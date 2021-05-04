<template>
<section class="content">
    <div class="container-fluid">
        <div>
            <!-- breadcrumb -->
            <ol class="breadcrumb mt-2" >
                <li class="breadcrumb-item">
                    <a href="#/home">Home</a>
                </li>
                <li class="breadcrumb-item active">ChartPeriod</li>
                <li class="breadcrumb-item active">ListTasks</li>
            </ol>

            <div class="row">
                <div class="col-md-12">
                    <div class="card">
                        <div class="card-header">
                        <h3 class="card-title">List Tasks</h3>
                        </div>
                        <div class="card-header">
                        
                        </div>
                    
                        <!-- /.card-header -->
                        <div class="card-body box-scrollbar" style="overflow-x:scroll">
                        <table class="table table-bordered">
                            <thead>
                            <tr>
                                <th>.No</th>
                            <th>
                                OC
                            </th>
                            <th>
                                KPIName
                            </th>
                            <th>
                                TaskName
                            </th>
                            <th>
                                Description
                            </th>
                            <th>
                                PIC
                            </th>
                            <th>
                                DueDate
                            </th>
                            <th>
                                UpdateSheduleDate
                            </th>
                            <th>
                                ActualFinishDate
                            </th>
                            <th>
                                Status
                            </th>
                            <th>
                                Approved
                            </th>

                            <th>
                                CreatedBy
                            </th>
                            <th>
                                CreatedDate
                            </th>
                            </tr>
                            </thead>
                            <tbody>
                            <tr v-for="(kpi,key,index) in data" :key="index">
                                <td>{{key+1}}</td>
                                <td>{{kpi.OC}}</td>
                                <td>
                                    <a :href="kpi.URL">{{kpi.KPIName}}</a>
                                </td>
                                <td>{{kpi.TaskName}}</td>
                                <td>{{kpi.Description}}</td>
                                <td>{{kpi.PIC}}</td>
                                <td>{{kpi.DueDate}}</td>
                                <td>{{kpi.UpdateSheduleDate}}</td>
                                <td>{{kpi.ActualFinishDate}}</td>
                                <td>
                                    <span v-if="kpi.Status == true" class="badge bg-green"> Finished</span>
                                    <span v-else class="badge bg-red">Not Finished</span>
                                </td>
                                <td>
                                    <span v-if="kpi.Approved == true" class="badge bg-green">Approved</span>
                                    <span v-else class="badge bg-red">Not Approved</span>
                                </td>
                                <td>{{kpi.CreatedBy}}</td>
                                <td>{{kpi.CreatedDate}}</td>
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
    </div>
</section>
</template>
<script>
import { HTTP } from "../../http-constants";
import Paginate from "vuejs-paginate";
import VueJwtDecode from 'vue-jwt-decode'
import Axios from 'axios';
export default {
    data(){
        return {
            data: [],
            totalPage: 0,
            page: 1,
            skip: 0,
            pageSize: 5,
            code: 0
        }
    },
    components:{
        Paginate
    },
    created(){
        this.loadTask()
    },
    methods: {
        changePage(pageNum){
            this.loadTask(this.name,pageNum)
        },
        loadTask(){
            axios.post(`ChartPeriod/ListTasks/${this.$route.params.kpilevelcode}/${this.page}/${this.pageSize}`)
            .then(res => {
                this.totalPage = res.data.total;
                this.page = res.data.page;
                this.data = res.data.data;
                this.pageSize = res.data.pageSize;
                //console.log(res)
            })
        }
    }
}
</script>