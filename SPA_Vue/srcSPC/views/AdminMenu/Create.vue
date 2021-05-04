<template>
  <div class="row">
    <!-- left column -->
    <div class="col-md-12">
      <!-- general form elements -->
      <div class="card card-primary">
        <div class="card-header">
          <h3 class="card-title">Add Menu</h3>
        </div>
        <!-- /.card-header -->
        <!-- form start -->
        <form role="form">
          <div class="card-body">
            <div class="form-group col-md-6">
              <label for="exampleInputEmail1">Name</label>
              <input
                type="email"
                class="form-control"
                id="exampleInputEmail1"
                placeholder="Enter Name"
              />
            </div>
            <div class="form-group col-md-6">
              <label for="exampleInputEmail1">Link</label>
              <input
                type="text"
                class="form-control"
                placeholder="Enter Code"
              />
            </div>
            <div class="form-group col-md-6">
              <label for="exampleInputEmail1">FontAwesome</label>
              <input
                type="text"
                class="form-control"
                placeholder="Enter Level"
              />
            </div>
            <div class="form-group col-md-6">
              <label for="exampleInputEmail1">BackgroudColor</label>
              <input
                type="text"
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
                tabindex="-1"
                aria-hidden="true"
              >
                <option selected="selected" data-select2-id="19">Alabama</option>
                <option data-select2-id="60">Alaska</option>
                <option data-select2-id="61">California</option>
                <option data-select2-id="62">Delaware</option>
                <option data-select2-id="63">Tennessee</option>
                <option data-select2-id="64">Texas</option>
                <option data-select2-id="65">Washington</option>
              </select>
              <span
                class="select2 select2-container select2-container--bootstrap4 select2-container--below"
                dir="ltr"
                data-select2-id="18"
                style="width: 100%;"
              >
                <span class="selection">
                  <span
                    class="select2-selection select2-selection--single"
                    role="combobox"
                    aria-haspopup="true"
                    aria-expanded="false"
                    tabindex="0"
                    aria-disabled="false"
                    aria-labelledby="select2-gvbb-container"
                  >
                    <span class="select2-selection__arrow" role="presentation">
                      <b role="presentation"></b>
                    </span>
                  </span>
                </span>
                <span class="dropdown-wrapper" aria-hidden="true"></span>
              </span>
            </div>
            <div class="form-group col-md-6">
              <label for="exampleInputEmail1">Position</label>
              <input
                value=""
                type="number"
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
export default {
  name: "CreateMenu"
};
</script>