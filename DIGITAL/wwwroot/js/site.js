// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('#filtro-form').submit(function (e) {
    e.preventDefault();

    var form = $(this);
    var url = form.attr('action');
    var formData = form.serialize();

    $.ajax({
        type: 'GET',
        url: url,
        data: formData,
        success: function (result) {
            $('#lista-empleados-partial').html(result);
        },
        error: function () {
            alert('Error al cargar la lista de empleados');
        }
    });
});
