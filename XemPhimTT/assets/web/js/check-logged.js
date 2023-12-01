$(".dropdown").hover(
    function () {
        $(this).find(".dropdown-menu").slideDown();
    },
    function () {
        $(this).find(".dropdown-menu").slideUp();
    }
);

