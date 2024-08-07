$(function () {
    var feedback = $('#feedbackMessage');
    var message = feedback.text();
    if (message !== '') {
        showFeedbackMessage(message, "success");
    }
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
