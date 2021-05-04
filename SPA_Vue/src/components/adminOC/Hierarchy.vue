<template>
     <div class="box-header">
        <span >Organization Chart</span>

        <div class="float-right box-tools">
          <!-- button with a dropdown -->
          <button type="button" class="btn btn-warning btn-sm fancy-collapse">
            <i class="fas fa-compress-arrows-alt"></i> {{ $t("Collapse") }}
          </button>
          <button type="button" class="btn btn-info btn-sm fancy-expand">
            <i class="fas fa-expand-arrows-alt"></i> {{$t("Expand")}}
          </button>
          
        </div>
      </div>
</template>