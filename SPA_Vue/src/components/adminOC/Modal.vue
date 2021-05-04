<template>
  <div>
    <div class="modal fade" id="modal-group" style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                    <h4 class="modal-title">Add User </h4>
                </div>
                <div class="modal-body">
                    <div>
                        <div class="box-body" id="addKPI">
                            <div class="form-group" style="display:none">
                                <label for="Name">ID </label>
                                <input type="text" class="form-control ID" id="ID" placeholder="ID" disabled>
                            </div>
                            <div class="form-group">
                                <label for="Name">Username </label>
                                <input type="text" class="form-control Name" id="Name" placeholder="Enter Name">
                            </div>
                            <div class="form-group">
                                <label for="Passwrod">Passwrod </label>
                                <input type="password" class="form-control Passwrod" id="Passwrod" placeholder="Enter Passwrod">
                            </div>
                            <div class="form-group">
                                <label for="Code">Code </label>
                                <input type="text" class="form-control Code" id="Code" placeholder="Enter Code">
                            </div>
                            <div class="form-group">
                                <label for="Code">Level </label>
                                <input type="text" class="form-control LevelID" id="LevelID" placeholder="Enter Level">
                            </div>

                        </div>
                        <!-- /.box-body -->

                        <div class="box-footer">
                            <button id="btnAdd" class="btn btn-primary">Add</button>
                            <button type="button" class="btn btn-default pull-right" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>

            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- @* update *@ -->
    <div class="modal fade" id="modal-group2" style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                    <h4 class="modal-title">Update User </h4>
                </div>
                <div class="modal-body">
                    <div>
                        <div class="box-body" id="updateKpi">
                            <div class="form-group" style="display:none">
                                <label for="Name">ID </label>
                                <input type="text" class="form-control ID" id="ID" placeholder="ID" disabled>
                            </div>
                            <div class="form-group">
                                <label for="Name">Name </label>
                                <input type="text" class="form-control Name" id="Name" placeholder="Enter Name">
                            </div>
                            <div class="form-group">
                                <label for="Code">Code </label>
                                <input type="text" class="form-control Code" id="Code" placeholder="Enter Code">
                            </div>
                            <div class="form-group">
                                <label for="Code">Level </label>
                                <input type="text" class="form-control LevelID" id="LevelID" placeholder="Enter Level">
                            </div>
                        </div>
                        <!-- /.box-body -->

                        <div class="box-footer">

                            <button type="button" class="btn btn-primary" id="btnSaveUpdateModal">Update</button>
                            <button type="button" class="btn btn-default pull-right" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>

            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    
    <!-- @* weekly madal *@ -->
    <div class="modal fade" id="modal-group-weekly" style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Weekly </h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                    <input type="text" value="" class="KPILevelID" style="display:none" />
                    <input type="text" value="" class="code" style="display:none" />
                    <input type="text" value="" class="period" style="display:none" />

                </div>
                <div class="modal-body">
                    <div>
                        <div class="box-body">
                            <div class="form-group">
                                <label for="weekly">Day of the week </label>
                                <select class="form-control weekly" id="weekly">
                                    <option value="">Choose day of the week</option>
                                    <option value="2">Monday</option>
                                    <option value="3">Tuesday</option>
                                    <option value="4">Wednesday</option>
                                    <option value="5">Thursday</option>
                                    <option value="6">Friday</option>
                                    <option value="7">Saturday</option>
                                </select>
                            </div>
                            <div class="form-group">
                                <label for="target">Woking Plan</label>
                                <textarea class="form-control target" id="target" rows="3"></textarea>
                            </div>
                            <div class="form-group">
                                <div class="pretty p-icon p-round p-jelly">
                                    <input type="radio" value="true" name="weekpublic" checked />
                                    <div class="state p-success">
                                        <i class="icon fa fa-check"></i>
                                        <label>Public</label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="pretty p-icon p-round p-jelly">
                                    <input type="radio" value="false" name="weekpublic" />
                                    <div class="state p-success">
                                        <i class="icon fa fa-check"></i>
                                        <label>Private</label>
                                    </div>
                                </div>
                            </div>


                        </div>
                        <!-- /.box-body -->

                        <div class="box-footer">
                            <button type="button" class="btn btn-primary btnsaveweekly" id="btnsaveweekly">Save</button>
                            <button type="button" class="btn btn-default pull-right" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>

            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- @* monthly modal *@ -->
    <div class="modal fade" id="modal-group-monthly" style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Monthly</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                    <input type="text" value="" class="KPILevelID" style="display:none" />
                    <input type="text" value="" class="code" style="display:none" />
                    <input type="text" value="" class="period" style="display:none" />
                </div>
                <div class="modal-body">
                    <div class="box-body">
                        <div class='form-group'>
                            <label for="monthly">Monthly</label>
                            <div class='input-group date monthly'>
                                <input id="datepicker" class="form-control datepicker date monthly-datepicker" name="monthly-datepicker" autocomplete="off"  width="276" />
                                <!-- <input type='text' class="form-control datepicker date monthly-datepicker" name="monthly-datepicker" /> -->
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="target">Woking Plan</label>
                            <textarea class="form-control target" id="target" rows="3" ></textarea>

                        </div>
                        <div class="form-group">
                            <div class="pretty p-icon p-round p-jelly">
                                <input type="radio" value="true" name="monthpublic" checked />
                                <div class="state p-success">
                                    <i class="icon fa fa-check"></i>
                                    <label>Public</label>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="pretty p-icon p-round p-jelly">
                                <input type="radio" value="false" name="monthpublic" />
                                <div class="state p-success">
                                    <i class="icon fa fa-check"></i>
                                    <label>Private</label>
                                </div>
                            </div>
                        </div>

                    </div>
                    <!-- /.box-body -->

                    <div class="box-footer">
                        <a href="#" class="btn btn-primary" id="btnsavemonthlymodal">Save</a>
                        <button type="button" class="btn btn-default pull-right" data-dismiss="modal">Close</button>
                    </div>
                </div>

            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- @* Quaterly Modal *@ -->
    <div class="modal fade" id="modal-group-quarterly" style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Quaterly </h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                    <input type="text" value="" class="KPILevelID" style="display:none" />
                    <input type="text" value="" class="code" style="display:none" />
                    <input type="text" value="" class="period" style="display:none" />
                </div>
                <div class="modal-body">
                    <div class="box-body">
                        <div class='form-group'>
                            <label for="quaterly">Quarterly</label>
                            <div class='input-group date quaterly'>
                                <input id="datepicker2" class="form-control datepicker date quarterly-datepicker" name="quarterly-datepicker" autocomplete="off"  width="276" />
                                <!-- <input type='text' class="form-control datepicker date quarterly-datepicker" name="quarterly-datepicker" /> -->
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="target">Woking Plan</label>
                            <textarea class="form-control target" id="target" rows="3"></textarea>
                        </div>
                        <div class="form-group">
                            <div class="pretty p-icon p-round p-jelly">
                                <input type="radio" value="true" name="quarterpublic" checked />
                                <div class="state p-success">
                                    <i class="icon fa fa-check"></i>
                                    <label>Public</label>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="pretty p-icon p-round p-jelly">
                                <input type="radio" value="false" name="quarterpublic" />
                                <div class="state p-success">
                                    <i class="icon fa fa-check"></i>
                                    <label>Private</label>
                                </div>
                            </div>
                        </div>

                    </div>
                    <!-- /.box-body -->

                    <div class="box-footer">

                        <a href="#" class="btn btn-primary" id="btnsavequaterlymodal">Save</a>
                        <button type="button" class="btn btn-default pull-right" data-dismiss="modal">Close</button>
                    </div>
                </div>

            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- @* Yearly Modal *@ -->
    <div class="modal fade" id="modal-group-yearly" style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Yearly</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                    <input type="text" value="" class="KPILevelID" style="display:none" />
                    <input type="text" value="" class="code" style="display:none" />
                    <input type="text" value="" class="period" style="display:none" />
                </div>
                <div class="modal-body">
                    <div class="box-body">
                        <div class='form-group'>
                            <label for="yearly">Yearly</label>
                            <div class='input-group date yearly'>
                              <input id="datepicker3" class="form-control datepicker date yearly-datepicker" name="yearly-datepicker" autocomplete="off"  width="276" />
                                <!-- <input type='text' class="form-control datepicker date yearly-datepicker" name="yearly-datepicker" /> -->
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="target">Woking Plan</label>
                            <textarea class="form-control target" id="target" rows="3"></textarea>
                        </div>
                        <div class="form-group">
                            <div class="pretty p-icon p-round p-jelly">
                                <input type="radio" value="true" name="yearpublic" checked />
                                <div class="state p-success">
                                    <i class="icon fa fa-check"></i>
                                    <label>Public</label>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="pretty p-icon p-round p-jelly">
                                <input type="radio" value="false" name="yearpublic" />
                                <div class="state p-success">
                                    <i class="icon fa fa-check"></i>
                                    <label>Private</label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- /.box-body -->
                    <div class="box-footer">
                        <button type="button" class="btn btn-primary" id="btnsaveyearly">Save</button>
                        <button type="button" class="btn btn-default pull-right" data-dismiss="modal">Close</button>
                    </div>
                </div>

            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <!-- @* weekly madal *@ -->
    <div class="modal fade" id="modal-group-showdata" style="display: none">
        <div class="modal-dialog">
            <!-- week -->
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Weekly </h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                    <input type="text" value="" class="KPILevelID" style="display:none" />
                    <input type="text" value="" class="code" style="display:none" />
                    <input type="text" value="" class="period" style="display:none" />

                </div>
                <div class="modal-body">
                    <div>
                        <div class="box-body">
                            <div class="form-group">
                                <label for="weekly">Day of the week </label>
                                <select class="form-control weekly1" id="weekly1">
                                    <option value="">Choose day of the week</option>
                                    <option value="2">Monday</option>
                                    <option value="3">Tuesday</option>
                                    <option value="4">Wednesday</option>
                                    <option value="5">Thursday</option>
                                    <option value="6">Friday</option>
                                    <option value="7">Saturday</option>
                                </select>
                            </div>
                            <div class="form-group">
                                <label for="target">Woking Plan</label>
                                <textarea class="form-control target1" id="target1" rows="3"></textarea>
                            </div>
                            <div class="form-group">
                                <div class="pretty p-icon p-round p-jelly">
                                    <input type="radio" value="true" name="weekpublic1" checked />
                                    <div class="state p-success">
                                        <i class="icon fa fa-check"></i>
                                        <label>Public</label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="pretty p-icon p-round p-jelly">
                                    <input type="radio" value="false" name="weekpublic1" />
                                    <div class="state p-success">
                                        <i class="icon fa fa-check"></i>
                                        <label>Private</label>
                                    </div>
                                </div>
                            </div>


                        </div>
                        <!-- /.box-body -->

                        <div class="box-footer">
                            <button type="button" class="btn btn-primary btnsaveweekly1" id="btnsaveweekly1">Save</button>
                            <button type="button" class="btn btn-default pull-right" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>

            </div>
            <!-- month -->
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Monthly</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <!-- <span aria-hidden="true">×</span> -->
                    </button>
                    <input type="text" value="" class="KPILevelID" style="display:none" />
                    <input type="text" value="" class="code" style="display:none" />
                    <input type="text" value="" class="period" style="display:none" />
                </div>
                <div class="modal-body">
                    <div class="box-body">
                        <div class='form-group'>
                            <label for="monthly">Monthly</label>
                            <div class='input-group date monthly'>
                                <input id="datepicker4" class="form-control datepicker date monthly-datepicker" name="monthly-datepicker" autocomplete="off"  width="276" />
                                <!-- <input type='text' class="form-control datepicker date monthly-datepicker" name="monthly-datepicker" /> -->
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="target">Woking Plan</label>
                            <textarea class="form-control target2" id="target2" rows="3" ></textarea>

                        </div>
                        <div class="form-group">
                            <div class="pretty p-icon p-round p-jelly">
                                <input type="radio" value="true" name="monthpublic" checked />
                                <div class="state p-success">
                                    <i class="icon fa fa-check"></i>
                                    <label>Public</label>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="pretty p-icon p-round p-jelly">
                                <input type="radio" value="false" name="monthpublic" />
                                <div class="state p-success">
                                    <i class="icon fa fa-check"></i>
                                    <label>Private</label>
                                </div>
                            </div>
                        </div>

                    </div>
                    <!-- /.box-body -->

                    <div class="box-footer">
                        <a href="#" class="btn btn-primary" id="btnsavemonthlymodal">Save</a>
                        <button type="button" class="btn btn-default pull-right" data-dismiss="modal">Close</button>
                    </div>
                </div>

            </div>
            <!-- quater -->
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Quaterly </h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <!-- <span aria-hidden="true">×</span> -->
                    </button>
                    <input type="text" value="" class="KPILevelID" style="display:none" />
                    <input type="text" value="" class="code" style="display:none" />
                    <input type="text" value="" class="period" style="display:none" />
                </div>
                <div class="modal-body">
                    <div class="box-body">
                        <div class='form-group'>
                            <label for="quaterly">Quarterly</label>
                            <div class='input-group date quaterly'>
                                <input id="datepicker5" class="form-control datepicker date quarterly-datepicker" name="quarterly-datepicker" autocomplete="off"  width="276" />
                                <!-- <input type='text' class="form-control datepicker date quarterly-datepicker" name="quarterly-datepicker" /> -->
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="target">Woking Plan</label>
                            <textarea class="form-control target3" id="target3" rows="3"></textarea>
                        </div>
                        <div class="form-group">
                            <div class="pretty p-icon p-round p-jelly">
                                <input type="radio" value="true" name="quarterpublic" checked />
                                <div class="state p-success">
                                    <i class="icon fa fa-check"></i>
                                    <label>Public</label>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="pretty p-icon p-round p-jelly">
                                <input type="radio" value="false" name="quarterpublic" />
                                <div class="state p-success">
                                    <i class="icon fa fa-check"></i>
                                    <label>Private</label>
                                </div>
                            </div>
                        </div>

                    </div>
                    <!-- /.box-body -->

                    <div class="box-footer">

                        <a href="#" class="btn btn-primary" id="btnsavequaterlymodal">Save</a>
                        <button type="button" class="btn btn-default pull-right" data-dismiss="modal">Close</button>
                    </div>
                </div>

            </div>
            <!-- year -->
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Yearly</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <!-- <span aria-hidden="true">×</span> -->
                    </button>
                    <input type="text" value="" class="KPILevelID" style="display:none" />
                    <input type="text" value="" class="code" style="display:none" />
                    <input type="text" value="" class="period" style="display:none" />
                </div>
                <div class="modal-body">
                    <div class="box-body">
                        <div class='form-group'>
                            <label for="yearly">Yearly</label>
                            <div class='input-group date yearly'>
                              <input id="datepicker6" class="form-control datepicker date yearly-datepicker" name="yearly-datepicker" autocomplete="off"  width="276" />
                                <!-- <input type='text' class="form-control datepicker date yearly-datepicker" name="yearly-datepicker" /> -->
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="target">Woking Plan</label>
                            <textarea class="form-control target4" id="target4" rows="3"></textarea>
                        </div>
                        <div class="form-group">
                            <div class="pretty p-icon p-round p-jelly">
                                <input type="radio" value="true" name="yearpublic" checked />
                                <div class="state p-success">
                                    <i class="icon fa fa-check"></i>
                                    <label>Public</label>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="pretty p-icon p-round p-jelly">
                                <input type="radio" value="false" name="yearpublic" />
                                <div class="state p-success">
                                    <i class="icon fa fa-check"></i>
                                    <label>Private</label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- /.box-body -->
                    <div class="box-footer">
                        <button type="button" class="btn btn-primary" id="btnsaveyearly">Save</button>
                        <button type="button" class="btn btn-default pull-right" data-dismiss="modal">Close</button>
                    </div>
                </div>

            </div>
        </div>
        <!-- /.modal-dialog -->
    </div>
  </div>
</template>