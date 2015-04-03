$(document).ready(function () {
    $('#btnFileRight').on('click', function (e) {
        var selectedFiles = $('#selectedFiles option:selected');
        if (selectedFiles.length == 0) {
            alert("Nothing to move");
            e.preventDefault();
        }
        $('#availibleFiles').append($(selectedFiles).clone());
        $(selectedFiles).remove();
        e.preventDefault();
    });

    $('#btnFileLeft').on('click', function (e) {
        var selectedFiles = $('#availibleFiles option:selected');
        if (selectedFiles.length == 0) {
            alert("Nothing to move");
            e.preventDefault();
        }
        $('#selectedFiles').append($(selectedFiles).clone());
        $(selectedFiles).remove();
        e.preventDefault();
    });

    $('#btnSubmit').on('click', function (e) {
        $('#selectedFiles option').prop('selected', true);
    });
});