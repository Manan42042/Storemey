(function () {

    $(function () {



        var $RegisterForm = $('#RegisterForm');


        $RegisterForm.find('#regStorename').bind('focusout', function (e) {

            setTimeout(function () {
                $('#regStorename').val($('#regStorename').val().cleanup());
                var storeName = $('#regStorename').val();

                if (storeName.length < 26) {

                    if (storeName.length > 3) {

                        abp.ui.setBusy(
                            $('.validate-form'),
                            abp.ajax({
                                contentType: 'application/x-www-form-urlencoded',
                                url: 'TenancyExists',
                                data: $RegisterForm.serialize()

                            }).done(function (response) {
                                $('.tenantIcon').hide();
                                $('.tenantIcon1').hide();
                                $('.tenantIcon2').show();
                                $(".tenantIcon1").css("visibility", "visible");
                                $(".tenantIcon2").css("visibility", "visible");
                                validRegiserForm = false;
                            })

                                .fail(function (response) {
                                    console.log(response);
                                    $('.tenantIcon').hide();
                                    $('.tenantIcon1').show();
                                    $('.tenantIcon2').hide();
                                    $(".tenantIcon1").css("visibility", "visible");
                                    $(".tenantIcon2").css("visibility", "visible");
                                    validRegiserForm = true;
                              
                                })
                        );
                    }
                    else {
                        $('.tenantIcon1').hide();
                        $('.tenantIcon2').hide();
                        $('.tenantIcon').html('Storename minimum 4 characters. <i class="fa fa-times"></i>');
                        $('.tenantIcon').show();
                        $(".tenantIcon").css("visibility", "visible");
                        validRegiserForm = false;
                    }
                }
                else {
                    $('.tenantIcon1').hide();
                    $('.tenantIcon2').hide();
                    $('.tenantIcon').html('Storename maximum 25 characters. <i class="fa fa-times"></i>');
                    $('.tenantIcon').show();
                    $(".tenantIcon").css("visibility", "visible");
                    validRegiserForm = false;
                }
            }, 500);

        });

        String.prototype.cleanup = function () {
            return this.toLowerCase().replace(/[^a-zA-Z0-9]+/g, "");
        }

        //# Using our new.cleanup() method
        //var clean = "Hello World".cleanup(); 



        $RegisterForm.submit(function (e) {
            e.preventDefault();

            var isvaliddata = true;

       
            if ($('#username').val() === '') {
                abp.notify.error("Username is required.");
                isvaliddata = false;
            };

            if ($('#email').val() === '') {
                abp.notify.error("Email is required.");
                isvaliddata = false;
            };

            if ($('#RegPassword').val() === '') {
                abp.notify.error("Password is required.");
                isvaliddata = false;
            };

            if ($('#checkbox1').prop("checked") === false) {
                abp.notify.error("Please checked terms and conditions.");
                isvaliddata = false;
            };

            if (!validRegiserForm) {
                abp.notify.error("Please enter valid storename.");
                isvaliddata = false;
            };

            if (!isvaliddata) {
                return;
            }


            if (!$RegisterForm.valid() || !validRegiserForm) {
                return;
            }

            //if (!$('checkbox1').checked()) {
        

            abp.ui.setBusy(
                $('.validate-form'),

                abp.ajax({
                    contentType: 'application/x-www-form-urlencoded',
                    url: $RegisterForm.attr('action'),
                    data: $RegisterForm.serialize()
                })
            );
        });

        $('a.social-login-link').click(function () {
            var $a = $(this);
            var $form = $a.closest('form');
            $form.find('input[name=provider]').val($a.attr('data-provider'));
            $form.submit();
        });

        $('input[name=returnUrlHash]').val(location.hash);

        $('#LoginForm input:first-child').focus();





    });

})();