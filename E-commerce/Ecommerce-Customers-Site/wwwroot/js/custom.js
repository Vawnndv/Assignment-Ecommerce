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

// Handle choose type product in Product detail
function showProductImages(productTypeId, button) {
    // Xóa lớp active từ tất cả các carousel item
    $('#productImageCarousel .carousel-item').removeClass('active');

    // Thêm lớp active cho carousel item có productTypeId tương ứng
    $(`#productImageCarousel .carousel-item[data-producttype='${productTypeId}']`).addClass('active');

    // Remove 'active' class from all buttons
    document.querySelectorAll('.btn-group .btn').forEach(btn => {
        btn.classList.remove('active');
    });

    // Add 'active' class to the clicked button
    button.classList.add('active');
}

// Handle choose quantity of product in Product detail
function updateQuantity(change) {
    var quantityInput = document.getElementById('productQuantity');
    var currentQuantity = parseInt(quantityInput.value);
    var newQuantity = currentQuantity + change;
    if (newQuantity < 1) {
        newQuantity = 1;
    }
    quantityInput.value = newQuantity;
}