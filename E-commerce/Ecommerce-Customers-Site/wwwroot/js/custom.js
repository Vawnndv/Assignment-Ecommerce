// navbarDropdown
$(document).ready(function () {
    $('.dropdown-toggle').mouseenter(function () {
        $(this).addClass('show');
        $(this).next('.dropdown-menu').addClass('show');
    });

    $('.dropdown-toggle').mouseleave(function () {
        var $this = $(this);
        setTimeout(function () {
            if (!$('.dropdown-menu:hover').length) {
                $this.removeClass('show');
                $this.next('.dropdown-menu').removeClass('show');
            }
        }, 100);
    });

    $('.dropdown-menu').mouseleave(function () {
        var $this = $(this);
        setTimeout(function () {
            if (!$('.dropdown-toggle:hover').length) {
                $this.removeClass('show');
                $this.prev('.dropdown-toggle').removeClass('show');
            }
        }, 100);
    });
});

