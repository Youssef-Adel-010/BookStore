var updatedRow = undefined;
$(function () {
    var feedback = $('#feedbackMessage');
    var message = feedback.text();
    if (message !== '') {
        showFeedbackMessage(message, "success");
    }

    // Handle Render Modal
    $('body').delegate('.js-render-modal', 'click', function () {
        var modal = $('#modal');
        var button = $(this);

        modal.find('#modalLabel').text(button.data('title'));

        if (button.data('is-update') !== undefined) {
            updatedRow = button.parents('tr');
        }

        $.ajax({
            url: button.data('url'),
            method: 'GET',
            success: function (form) {
                var body = modal.find('.modal-body');
                body.html(form);
                $.validator.unobtrusive.parse(modal);
            },
            error: function () {
                showFeedbackMessage('Something went wrong', 'error');
            }
        });

        modal.modal('show');
    });

    // Handle Toggle Status
    $('body').delegate('.js-toggle-status', 'click', function () {
        const swalWithBootstrapButtons = Swal.mixin({
            customClass: {
                confirmButton: "btn btn-danger",
                cancelButton: "btn btn-success"
            },
            buttonsStyling: false
        });
        swalWithBootstrapButtons.fire({
            title: "Are you sure?",
            text: "You want to toggle this field status",
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: "Yes",
            cancelButtonText: "cancel",
            reverseButtons: true
        }).then((result) => {
            if (result.isConfirmed) {
                var toggleButton = $(this);
                $.ajax({
                    url: toggleButton.data('url'),
                    method: 'POST',
                    data: {
                        '__RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
                    },
                    success: function (lastUpdatedOn) {
                        showFeedbackMessage("Field status toggled successfully", "success");
                        var currentRow = toggleButton.parents('tr');
                        var status = currentRow.find('.js-status');
                        status.text(status.text().trim() === 'Deleted' ? 'Available' : 'Deleted');
                        status.toggleClass('badge-light-danger badge-light-success');

                        var updatedOn = currentRow.find('#js-last-updated-on');
                        updatedOn.text(lastUpdatedOn);
                        currentRow.addClass('animate__animated animate__headShake');
                        setTimeout(function () {
                            currentRow.removeClass('animate__animated animate__headShake');
                        }, 300);

                    },
                    error: function () {
                        showFeedbackMessage("Something went wrong!", "error");
                    }
                });

            }
        });
    });

});

function showFeedbackMessage(message = "Done successfully", type = "success") {
    toastr.options = {
        "closeButton": false,
        "debug": false,
        "newestOnTop": true,
        "progressBar": true,
        "positionClass": "toast-bottom-right",
        "preventDuplicates": true,
        "onclick": null,
        "showDuration": "1000",
        "hideDuration": "1000",
        "timeOut": "3000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }
    Command: toastr[type](message, "Done")
}

function onModalSuccess(newRow) {
    $('#modal').modal('hide');
    showFeedbackMessage("Field created successfully", "success");
    updatedRow === undefined ? $('tbody').append(newRow) : $(updatedRow).replaceWith(newRow);
}

function onModalFailure() {
    $('#modal').modal('hide');
    showFeedbackMessage("Something went wrong!", "error");
}
