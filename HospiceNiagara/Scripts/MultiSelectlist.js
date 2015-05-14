$(document).ready(function () {
    $('#btnRemoveFile').on('click', function (e) {
        var selectedFiles = $('#selectedFiles option:selected');
        if (selectedFiles.length == 0) {
            alert("Nothing to move");
            e.preventDefault();
        }
        $(selectedFiles).remove();
        e.preventDefault();
    });

    $('#btnSubmit').on('click', function (e) {
        $('#selectedFiles option').prop('selected', true);
    });
});