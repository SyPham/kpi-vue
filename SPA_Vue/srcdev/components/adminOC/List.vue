<template>
<div class="box" id="box">
        <div class="box-header with-border kpi-name">
          <h3 class="box-title pull-left">KPI</h3>
          <span class="id" style="display:none"></span>
          <span class="code" style="display:none"></span>
          <span class="level" style="display:none"></span>
          <select class="form-control pull-right" style="width:50%"></select>
        </div>
        <div class="box-body">
          <div class="box-body">
            <table class="table table-bordered">
              <thead>
                <tr>
                  <th>#</th>
                  <th>KPILevel Name</th>
                  <th>Get Code</th>
                  <th>Period</th>
                </tr>
              </thead>
              <tbody class="tbody" id="tblkpilevel">
                <tr v-for="(item,key,index) in events" :key="index">
                  <td :data-kpilevel="item.ID">{{key+1}}</td>
                  <td style="width:150px" :data-unit="item.Unit" class="tooltip-css">
                    <div class="top">
                      {{item.KPIName}}
                      <i></i>
                    </div>

                    <div class="pretty p-icon p-rotate">
                      <input
                        v-if="item.Checked == true"
                        type="checkbox"
                        :data-code="item.KPICode"
                        :value="item.KPIID"
                        checked
                      />
                      <input v-else type="checkbox" :data-code="item.KPICode" :value="item.KPIID" />
                      <span class="level" :data-level="item.LevelNumber" style="display:none"></span>
                      <span class="kpi-code" :data-kpicode="item.LevelCode" style="display:none"></span>
                      <div class="state p-success">
                        <i class="icon fa fa-check"></i>
                        <label class="textOverflow black">{{item.KPIName}}</label>
                      </div>
                    </div>
                  </td>
                  <td style="width:150px">
                    <button
                      data-toggle="modal"
                      data-target="#modal-group3"
                      class="btn btn-sm btn-success get-kpi-code"
                    >Get KPI Code</button>
                  </td>
                  <td>
                    <div class="pretty p-icon p-rotate">
                      <div class="state p-success">
                        <input
                          v-if="item.WeeklyChecked == true"
                          class="weekly"
                          :data-code="item.KPICode "
                          :value="item.KPIID"
                          type="checkbox"
                          checked
                        />
                        <input
                          v-else
                          class="weekly"
                          :data-code="item.KPICode"
                          :value="item.KPIID"
                          type="checkbox"
                        />
                        <i class="icon fa fa-check"></i>
                        <label class="black">Weekly</label>
                      </div>
                    </div>
                    <div class="pretty p-icon p-rotate">
                      <input
                        v-if="item.MonthlyChecked === true"
                        class="monthly"
                        data-code="item.KPICode"
                        value="item.KPIID"
                        type="checkbox"
                        checked
                      />
                      <input
                        v-else
                        class="monthly"
                        data-code="item.KPICode"
                        value="item.KPIID"
                        type="checkbox"
                      />
                      <div class="state p-success">
                        <i class="icon fa fa-check"></i>
                        <label class="black">Monthly</label>
                      </div>
                    </div>
                    <div class="pretty p-icon p-rotate">
                      <input
                        v-if="item.QuarterlyChecked === true"
                        class="quaterly"
                        data-code="item.KPICode"
                        value="item.KPIID"
                        type="checkbox"
                        checked
                      />
                      <input
                        v-else
                        class="quaterly"
                        data-code="item.KPICode"
                        value="item.KPIID"
                        type="checkbox"
                      />
                      <div class="state p-success">
                        <i class="icon fa fa-check"></i>
                        <label class="black">Quarterly</label>
                      </div>
                    </div>
                    <div class="pretty p-icon p-rotate">
                      <input
                        v-if="item.YearlyChecked === true"
                        class="yearly"
                        data-code="item.KPICode"
                        value="item.KPIID"
                        type="checkbox"
                        checked
                      />
                      <input
                        v-else
                        class="yearly"
                        data-code="item.KPICode"
                        value="item.KPIID"
                        type="checkbox"
                      />
                      <div class="state p-success">
                        <i class="icon fa fa-check"></i>
                        <label class="black">Yearly</label>
                      </div>
                    </div>
                  </td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
        <div class="box-footer clearfix">
          <ul id="paginationKPILevel" class="pagination pagination-sm no-margin pull-right">
            <li>
              <a href="#">«</a>
            </li>
            <li>
              <a href="#">1</a>
            </li>
            <li>
              <a href="#">2</a>
            </li>
            <li>
              <a href="#">3</a>
            </li>
            <li>
              <a href="#">»</a>
            </li>
          </ul>
        </div>
      </div>
</template>
<script>
export default {
  name: "IndexKpi",
  data() {
    return {
      events:[]
    };
  },
  created() {
    //flag = false -> header click vietnamese -> flag = true
  },
  methods: {
    //Neu flag= true -> loadData()
  }
};
</script>