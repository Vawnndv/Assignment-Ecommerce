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

// Handle show filter in show products
$(document).ready(function () {
    $("#toggleFilter").click(function () {
        $("#filterContainer").toggleClass("show");
        $(this).find('.icon').toggleClass('rotate');
    });
});

$(document).click(function (event) {
    if (!$(event.target).closest("#filterContainer, #toggleFilter").length) {
        if ($("#filterContainer").hasClass("show")) {
            $("#filterContainer").removeClass("show");
            $("#toggleFilter").find('.icon').removeClass('rotate');
        }
    }
});

// Handle slider price at filter products
document.addEventListener("DOMContentLoaded", function () {
    var urlParams = new URLSearchParams(window.location.search);
    var sortBy = urlParams.get('SortBy');
    var isDescending = urlParams.get('IsDescending') === 'on';
    var minPrice = parseInt(urlParams.get('MinPrice') || 0, 10);
    var maxPrice = parseInt(urlParams.get('MaxPrice') || 2147483647, 10);

    if (sortBy) {
        document.getElementById('sortBy').value = sortBy;
    }
    document.getElementById('isDescending').checked = isDescending;

    document.getElementById('minPrice').value = minPrice;
    document.getElementById('maxPrice').value = maxPrice;

    var priceRangeSlider = document.getElementById('priceRangeSlider');
    noUiSlider.create(priceRangeSlider, {
        start: [minPrice, maxPrice],
        connect: true,
        range: {
            'min': 0,
            'max': 1000000000
        },
        format: wNumb({
            decimals: 0
        }),
        tooltips: true
    });

    priceRangeSlider.noUiSlider.on('update', function (values, handle) {
        var minPriceValue = parseInt(values[0].replace('$', '').replace(',', ''), 10);
        var maxPriceValue = parseInt(values[1].replace('$', '').replace(',', ''), 10);

        document.getElementById('minPriceValue').innerText = '$' + minPriceValue.toLocaleString();
        document.getElementById('maxPriceValue').innerText = '$' + maxPriceValue.toLocaleString();

        document.getElementById('minPrice').value = minPriceValue;
        document.getElementById('maxPrice').value = maxPriceValue;
    });
});

// Hanlde Show model register/ Login
$(document).ready(function () {
    // Attach click event to the user icon
    $("#userIcon").click(function () {
        // Show the user modal
        $("#userModal").modal("show");
    });
});
