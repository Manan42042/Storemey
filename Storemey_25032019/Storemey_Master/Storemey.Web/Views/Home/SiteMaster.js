(function () {

    $(function () {

        $(window).on('load resize orientationchange', function () {
            $('.carousel').each(function () {
                var $carousel = $(this);
                /* Initializes a slick carousel only on mobile screens */
                // slick on mobile
                if ($(window).width() > 768) {
                    if ($carousel.hasClass('slick-initialized')) {
                        $carousel.slick('unslick');
                    }
                }
                else {
                    if (!$carousel.hasClass('slick-initialized')) {
                        $carousel.slick({
                            slidesToShow: 2,
                            slidesToScroll: 1,
                            mobileFirst: true,
                        });
                    }
                }
            });
        });
    });

    });

})();