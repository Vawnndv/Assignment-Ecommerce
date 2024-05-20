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

// Handle scroll list product view
function scrollLeftListProductNew() {
    const container = document.querySelector('.product-container-new');
container.scrollBy({left: -300, behavior: 'smooth' });
}

function scrollRightListProductNew() {
    const container = document.querySelector('.product-container-new');
container.scrollBy({left: 300, behavior: 'smooth' });
}

// Handle scroll list product view
function scrollLeftListProductDiscount() {
    const container = document.querySelector('.product-container');
    container.scrollBy({ left: -300, behavior: 'smooth' });
}

function scrollRightListProductDiscount() {
    const container = document.querySelector('.product-container');
    container.scrollBy({ left: 300, behavior: 'smooth' });
}
