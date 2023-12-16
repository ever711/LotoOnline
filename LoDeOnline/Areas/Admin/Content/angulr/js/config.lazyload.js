// lazyload config

angular.module('app')
    /**
   * jQuery plugin config use ui-jq directive , config the js and css files that required
   * key: function name of the jQuery plugin
   * value: array of the css js file located
   */
  .constant('JQ_CONFIG', {
      easyPieChart:   [   '/Areas/Admin/Content/angulr/bower_components/jquery.easy-pie-chart/dist/jquery.easypiechart.fill.js'],
      sparkline:      [   '/Areas/Admin/Content/angulr/bower_components/jquery.sparkline/dist/jquery.sparkline.retina.js'],
      plot:           [   '/Areas/Admin/Content/angulr/bower_components/flot/jquery.flot.js',
                          '/Areas/Admin/Content/angulr/bower_components/flot/jquery.flot.pie.js', 
                          '/Areas/Admin/Content/angulr/bower_components/flot/jquery.flot.resize.js',
                          '/Areas/Admin/Content/angulr/bower_components/flot.tooltip/js/jquery.flot.tooltip.js',
                          '/Areas/Admin/Content/angulr/bower_components/flot.orderbars/js/jquery.flot.orderBars.js',
                          '/Areas/Admin/Content/angulr/bower_components/flot-spline/js/jquery.flot.spline.js'],
      moment:         [   '/Areas/Admin/Content/angulr/bower_components/moment/moment.js'],
      screenfull:     [   '/Areas/Admin/Content/angulr/bower_components/screenfull/dist/screenfull.min.js'],
      slimScroll:     [   '/Areas/Admin/Content/angulr/bower_components/slimscroll/jquery.slimscroll.min.js'],
      sortable:       [   '/Areas/Admin/Content/angulr/bower_components/html5sortable/jquery.sortable.js'],
      nestable:       [   '/Areas/Admin/Content/angulr/bower_components/nestable/jquery.nestable.js',
                          '/Areas/Admin/Content/angulr/bower_components/nestable/jquery.nestable.css'],
      filestyle:      [   '/Areas/Admin/Content/angulr/bower_components/bootstrap-filestyle/src/bootstrap-filestyle.js'],
      slider:         [   '/Areas/Admin/Content/angulr/bower_components/bootstrap-slider/bootstrap-slider.js',
                          '/Areas/Admin/Content/angulr/bower_components/bootstrap-slider/bootstrap-slider.css'],
      chosen:         [   '/Areas/Admin/Content/angulr/bower_components/chosen/chosen.jquery.min.js',
                          '/Areas/Admin/Content/angulr/bower_components/bootstrap-chosen/bootstrap-chosen.css'],
      TouchSpin:      [   '/Areas/Admin/Content/angulr/bower_components/bootstrap-touchspin/dist/jquery.bootstrap-touchspin.min.js',
                          '/Areas/Admin/Content/angulr/bower_components/bootstrap-touchspin/dist/jquery.bootstrap-touchspin.min.css'],
      wysiwyg:        [   '/Areas/Admin/Content/angulr/bower_components/bootstrap-wysiwyg/bootstrap-wysiwyg.js',
                          '/Areas/Admin/Content/angulr/bower_components/bootstrap-wysiwyg/external/jquery.hotkeys.js'],
      dataTable:      [   '/Areas/Admin/Content/angulr/bower_components/datatables/media/js/jquery.dataTables.min.js',
                          '/Areas/Admin/Content/angulr/bower_components/plugins/integration/bootstrap/3/dataTables.bootstrap.js',
                          '/Areas/Admin/Content/angulr/bower_components/plugins/integration/bootstrap/3/dataTables.bootstrap.css'],
      vectorMap:      [   '/Areas/Admin/Content/angulr/bower_components/bower-jvectormap/jquery-jvectormap-1.2.2.min.js', 
                          '/Areas/Admin/Content/angulr/bower_components/bower-jvectormap/jquery-jvectormap-world-mill-en.js',
                          '/Areas/Admin/Content/angulr/bower_components/bower-jvectormap/jquery-jvectormap-us-aea-en.js',
                          '/Areas/Admin/Content/angulr/bower_components/bower-jvectormap/jquery-jvectormap-1.2.2.css'],
      footable:       [   '/Areas/Admin/Content/angulr/bower_components/footable/dist/footable.all.min.js',
                          '/Areas/Admin/Content/angulr/bower_components/footable/css/footable.core.css'],
      fullcalendar:   [   '/Areas/Admin/Content/angulr/bower_components/moment/moment.js',
                          '/Areas/Admin/Content/angulr/bower_components/fullcalendar/dist/fullcalendar.min.js',
                          '/Areas/Admin/Content/angulr/bower_components/fullcalendar/dist/fullcalendar.css',
                          '/Areas/Admin/Content/angulr/bower_components/fullcalendar/dist/fullcalendar.theme.css'],
      daterangepicker:[   '/Areas/Admin/Content/angulr/bower_components/moment/moment.js',
                          '/Areas/Admin/Content/angulr/bower_components/bootstrap-daterangepicker/daterangepicker.js',
                          '/Areas/Admin/Content/angulr/bower_components/bootstrap-daterangepicker/daterangepicker-bs3.css'],
      tagsinput:      [   '/Areas/Admin/Content/angulr/bower_components/bootstrap-tagsinput/dist/bootstrap-tagsinput.js',
                          '/Areas/Admin/Content/angulr/bower_components/bootstrap-tagsinput/dist/bootstrap-tagsinput.css']
                      
    }
  )
  // oclazyload config
  .config(['$ocLazyLoadProvider', function($ocLazyLoadProvider) {
      // We configure ocLazyLoad to use the lib script.js as the async loader
      $ocLazyLoadProvider.config({
          debug:  true,
          events: true,
          modules: [
              {
                  name: 'ngGrid',
                  files: [
                      '/Areas/Admin/Content/angulr/bower_components/ng-grid/build/ng-grid.min.js',
                      '/Areas/Admin/Content/angulr/bower_components/ng-grid/ng-grid.min.css',
                      '/Areas/Admin/Content/angulr/bower_components/ng-grid/ng-grid.bootstrap.css'
                  ]
              },
              {
                  name: 'ui.grid',
                  files: [
                      '/Areas/Admin/Content/angulr/bower_components/angular-ui-grid/ui-grid.min.js',
                      '/Areas/Admin/Content/angulr/bower_components/angular-ui-grid/ui-grid.min.css',
                      '/Areas/Admin/Content/angulr/bower_components/angular-ui-grid/ui-grid.bootstrap.css'
                  ]
              },
              {
                  name: 'ui.select',
                  files: [
                      '/Areas/Admin/Content/angulr/bower_components/angular-ui-select/dist/select.min.js',
                      '/Areas/Admin/Content/angulr/bower_components/angular-ui-select/dist/select.min.css'
                  ]
              },
              {
                  name:'angularFileUpload',
                  files: [
                    '/Areas/Admin/Content/angulr/bower_components/angular-file-upload/angular-file-upload.min.js'
                  ]
              },
              {
                  name:'ui.calendar',
                  files: ['/Areas/Admin/Content/angulr/bower_components/angular-ui-calendar/src/calendar.js']
              },
              {
                  name: 'ngImgCrop',
                  files: [
                      '/Areas/Admin/Content/angulr/bower_components/ngImgCrop/compile/minified/ng-img-crop.js',
                      '/Areas/Admin/Content/angulr/bower_components/ngImgCrop/compile/minified/ng-img-crop.css'
                  ]
              },
              {
                  name: 'angularBootstrapNavTree',
                  files: [
                      '/Areas/Admin/Content/angulr/bower_components/angular-bootstrap-nav-tree/dist/abn_tree_directive.js',
                      '/Areas/Admin/Content/angulr/bower_components/angular-bootstrap-nav-tree/dist/abn_tree.css'
                  ]
              },
              {
                  name: 'toaster',
                  files: [
                      '/Areas/Admin/Content/angulr/bower_components/angularjs-toaster/toaster.js',
                      '/Areas/Admin/Content/angulr/bower_components/angularjs-toaster/toaster.css'
                  ]
              },
              {
                  name: 'textAngular',
                  files: [
                      '/Areas/Admin/Content/angulr/bower_components/textAngular/dist/textAngular-sanitize.min.js',
                      '/Areas/Admin/Content/angulr/bower_components/textAngular/dist/textAngular.min.js'
                  ]
              },
              {
                  name: 'vr.directives.slider',
                  files: [
                      '/Areas/Admin/Content/angulr/bower_components/venturocket-angular-slider/build/angular-slider.min.js',
                      '/Areas/Admin/Content/angulr/bower_components/venturocket-angular-slider/build/angular-slider.css'
                  ]
              },
              {
                  name: 'com.2fdevs.videogular',
                  files: [
                      '/Areas/Admin/Content/angulr/bower_components/videogular/videogular.min.js'
                  ]
              },
              {
                  name: 'com.2fdevs.videogular.plugins.controls',
                  files: [
                      '/Areas/Admin/Content/angulr/bower_components/videogular-controls/controls.min.js'
                  ]
              },
              {
                  name: 'com.2fdevs.videogular.plugins.buffering',
                  files: [
                      '/Areas/Admin/Content/angulr/bower_components/videogular-buffering/buffering.min.js'
                  ]
              },
              {
                  name: 'com.2fdevs.videogular.plugins.overlayplay',
                  files: [
                      '/Areas/Admin/Content/angulr/bower_components/videogular-overlay-play/overlay-play.min.js'
                  ]
              },
              {
                  name: 'com.2fdevs.videogular.plugins.poster',
                  files: [
                      '/Areas/Admin/Content/angulr/bower_components/videogular-poster/poster.min.js'
                  ]
              },
              {
                  name: 'com.2fdevs.videogular.plugins.imaads',
                  files: [
                      '/Areas/Admin/Content/angulr/bower_components/videogular-ima-ads/ima-ads.min.js'
                  ]
              },
              {
                  name: 'xeditable',
                  files: [
                      '/Areas/Admin/Content/angulr/bower_components/angular-xeditable/dist/js/xeditable.min.js',
                      '/Areas/Admin/Content/angulr/bower_components/angular-xeditable/dist/css/xeditable.css'
                  ]
              },
              {
                  name: 'smart-table',
                  files: [
                      '/Areas/Admin/Content/angulr/bower_components/angular-smart-table/dist/smart-table.min.js'
                  ]
              }
          ]
      });
  }])
;
