var updatedRow = undefined;
$(function () {
    var feedback = $('#feedbackMessage');
    var message = feedback.text();
    if (message !== '') {
        showFeedbackMessage(message, "success");
    }

    // Handle render modal
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
