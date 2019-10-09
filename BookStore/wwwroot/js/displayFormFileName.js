$(document).ready(function () {
    $(".custom-file-input").change(function () {
        const name = $(this).val().split("\\").pop();
        $(this).next(".custom-file-label").html(name);
    });
});