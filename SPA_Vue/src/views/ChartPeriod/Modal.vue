<template>
  <div>
    <div class="modal fade modal" id="modal-group-comment-data">
        <div class="modal-dialog modal-lg" style="width:90%">
            <div class="modal-content">
                <div class="modal-header" style="background-color:#00a65a;color:#fff">
                    <h4 class="modal-title">
                        <i class="fa fa-tags"></i>&#32;
                        <span class="RemarkChart"></span>
                    </h4>
                    <button type="button" class="close" data-dismiss="modal">
                        <span aria-hidden="true">&times;</span>
                        <span class="sr-only">Close</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row bootstrap snippets">
                        <div class="col-md-12 col-sm-12">
                            <div class="comment-wrapper">
                                <div class="panel panel-info">
                                    <div class="panel-body">
                                        <span class="dataid" style="display:none"></span>
                                        <div class="form-group">
                                            <textarea class="form-control" id="comment" placeholder="write a comment..." rows="6"></textarea>
                                        </div>

                                        <button type="button" @click="addcomment()" class="btn btn-info pull-right btnComment">
                                            <i class="fa fa-send-o"></i> Post
                                        </button>
                                        <div class="clearfix"></div>
                                        <hr />
                                        <ul class="media-list" id="media-list"></ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <div class="modal fade modal-window modal" id="modal-group-comment-data2">
        <div class="modal-dialog modal-lg" style="width:95%;overflow-y:scroll">
            <div class="modal-content">
                <div class="modal-header" style="background-color:#00a65a;color:#fff">
                    <span class="commentid" style="display:none"></span>

                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                    <h4 class="modal-title">
              <a
                href="#modal-group-comment-data"
                data-toggle="modal"
                data-dismiss="modal"
                style="color:#fff;cursor:pointer"
              >
                <i class="fa fa-reply"></i>
              </a>
              &#32;
              <span class="ActionPlanChart"></span>
            </h4>
                </div>
                <div class="modal-body">
                    <ul class="nav nav-pills mb-3" id="pills-tab" role="tablist">
                        <li class="nav-item">
                            <a class="nav-link" id="pills-home-tab" data-toggle="pill" href="#pills-home">List</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" id="pills-profile-tab" data-toggle="pill" href="#pills-profile">Add</a>
                        </li>
                    </ul>
                    <div class="tab-content" id="pills-tabContent">
                        <div class="tab-pane fade" id="pills-home">
                            <div class="listTask">
                                <table class="table table-condensed table-bordered">
                                    <thead>
                                        <tr>
                                            <th style="width: 10px">No.</th>
                                            <th>Task name</th>
                                            <th>Descriptions</th>
                                            <th>PIC</th>
                                            <th>Due date</th>
                                            <th>Update shedule date</th>
                                            <th>Actual finish date</th>
                                            <th>Status</th>
                                            <th class="Approval" style="width: 100px">Approve</th>
                                            <th class="Option" style="width: 50px">Option</th>
                                        </tr>
                                    </thead>
                                    <tbody class="tblActionPlan">
                                        <tr>
                                            <td>1.</td>
                                            <td>To do list</td>
                                            <td>
                                                <ul>
                                                    <li>Ambitioni dedisse scripsisse iudicaretur. Cras mattis iudicium purus sit amet fermentum.</li>
                                                    <li>Ambitioni dedisse scripsisse iudicaretur. Cras mattis iudicium purus sit amet fermentum.</li>
                                                    <li>Ambitioni dedisse scripsisse iudicaretur. Cras mattis iudicium purus sit amet fermentum.</li>
                                                    <li>Ambitioni dedisse scripsisse iudicaretur. Cras mattis iudicium purus sit amet fermentum.</li>
                                                </ul>
                                            </td>
                                            <td>swook, peter, henry</td>
                                            <td></td>
                                            <td></td>
                                            <td>20-10-2019</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="pills-profile">
                            <div class="addTask">
                                <!-- text input -->
                                <div class="form-group">
                                    <label>Task name</label>
                                    <input type="text" class="form-control Title" autocomplete="off" placeholder="Enter ..." />
                                    <input type="text" class="form-control ID hidden" placeholder="Enter ..." />
                                </div>

                                <!-- textarea -->
                                <div class="form-group">
                                    <label>Description</label>
                                    <div class="editable Description" style="border:1px solid #d2d6de" contenteditable="true">
                                        <ul style="list-style: decimal;padding-left: 20px;">
                                            <li></li>
                                        </ul>
                                    </div>
                                </div>

                                <!-- input states -->
                                <div class="form-group">
                                    <label class="control-label" for="Tag">Assign PIC</label>
                                    <textarea type="text" class="form-control Tag" id="Tag" rows="1" placeholder="Require ..."></textarea>
                                </div>
                                <div class="form-group">
                                    <label class="control-label" for="Auditor">Auditor</label>
                                    <textarea type="text" class="form-control Auditor" id="Auditor" rows="1" placeholder="Optional ..."></textarea>
                                </div>
                                <!-- radio -->
                                <div class="form-group">
                                    <label>Due date</label>
                                    <input autocomplete="off" type="text" class="form-control DueDate datepicker" name="datepicker" />
                                </div>
                                <button type="button" class="btn btn-success btnSaveActionPlan">Save</button>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <a href="#modal-group-comment-data" data-toggle="modal" data-dismiss="modal" class="btn btn-primary">Back to remark</a>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
</div>
</template>
<script>
export default {
    data(){
        return {

        }
    },
    created() {

    },
    method: {
        addcomment(){
            console.log('ballala')
        }
    }
}
</script>