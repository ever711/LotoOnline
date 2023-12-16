
var $window = $(window);


function Page() {
    var self = this;
    this.init = function () {
        self.jquery();
        self.menu();
    };
    this.jquery = function () {

    }

    this.menu = function() {

        $('.mobile-button').click(function() {
          $('body').addClass('menu-active');
        })

        $('.main-menu').click(function() {
          $('body').removeClass('menu-active');
        })

        $('.menu-list').click(function(event) {
          event.stopPropagation();
        })

    }



}
Page = new Page();
$(document).ready(function () {
    Page.init();
});

