
$('#btnCerrarModal').on('click', function () {
    $('#modalAgregarFoto').modal('hide');
});

$('#btnAgregarFoto').on('click', function () {
    $('#modalAgregarFoto').modal('show');
});


$(document).ready(function () {
    $('#submit-btn').click(function (e) {
        e.preventDefault();
        var formData = new FormData();
        formData.append('file', $('#file')[0].files[0]);
        $.ajax({
            url: '@Url.Action("SubirImagen", "Perfil")',
            type: 'POST',
            data: formData,
            cache: false,
            contentType: false,
            processData: false,
            success: function (result) {
                Swal.fire({
                    title: '¡Éxito!',
                    text: 'La imagen se ha cargado correctamente.',
                    icon: 'success'
                }).then(function () {
                    location.reload();
                });
            },
            error: function (xhr, status, error) {
                Swal.fire({
                    title: '¡Error!',
                    text: 'Hubo un problema al cargar la imagen.',
                    icon: 'error'
                });
            }
        });
    });
});



function validateFileSize(input) {
    if (input.files[0].size > 2 * 1024 * 1024) {
        Swal.fire("Error", "La imagen debe ser menor que 2 MB.", "error");
        input.value = "";
        document.getElementById('submit-btn').disabled = true;
    } else {
        Swal.fire({
            title: "¿Está seguro de que desea subir esta imagen?",
            icon: "question",
            showCancelButton: true,
            confirmButtonText: "Sí",
            cancelButtonText: "No"
        }).then((result) => {
            if (result.isConfirmed) {
                document.getElementById('submit-btn').disabled = false;
            } else {
                input.value = "";
                document.getElementById('submit-btn').disabled = true;
            }
        });
    }
}

$(document).ready(function () {
    $('.eliminar-form').submit(function (e) {
        e.preventDefault(); // Prevenir envío del formulario
        var form = this;
        Swal.fire({
            title: '¿Estás seguro de eliminar la foto?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#d33',
            cancelButtonColor: '#3085d6',
            confirmButtonText: 'Sí, eliminar',
            cancelButtonText: 'Cancelar',
            customClass: {
                confirmButton: 'btn btn-danger',
                cancelButton: 'btn btn-primary'
            }
        }).then((result) => {
            if (result.isConfirmed) {
                form.submit(); // Continuar con el envío del formulario
            }
        });
    });
});


$(document).ready(function () {
    $.ajax({
        url: '@Url.Action("CargarDatosCliente", "Perfil")',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            if (data) {
                $('#nombre').val(data.Nombre);
                $('#descripcion').val(data.Descripcion);
            }
        },
        error: function () {
            console.error('Ocurrió un error al obtener los datos del cliente');
        }
    });
});


$(document).ready(function () {
    $("#descripcion").on("change", function () {
        var descripcion = $(this).val();
        $.ajax({
            url: "@Url.Action("ActualizarDatosCliente", "Perfil")",
            type: "POST",
            data: { description: descripcion },
            success: function (response) {
                Swal.fire({
                    icon: 'success',
                    title: 'Éxito',
                    text: response.message
                });
            },
            error: function (xhr, status, error) {
                console.log("Error al actualizar los datos: " + error);
            }
        });
    });
});




$(function () {
    $('#categorias').on('change', function () {
        var categoriasSeleccionadas = $(this).val();
        $.ajax({
            url: '@Url.Action("ObtenerInteresesPorCategoria", "Perfil")',
            type: 'POST',
            data: { categorias: categoriasSeleccionadas },
            success: function (data) {
                var selectIntereses = $('#intereses');
                selectIntereses.empty();
                $.each(data, function (index, interes) {
                    selectIntereses.append('<option value="' + interes.idinteres + '">' + interes.nombre + '</option>');
                });
            }
        });
    });
});

$(function () {
    $('#interesesSeleccionados').on('click', 'li', function () {
        $(this).remove();
    });

    $('#agregarInteres').on('click', function () {
        var interesSeleccionado = $('#intereses').find(':selected');
        var interesesSeleccionados = $('#interesesSeleccionados');

        if (interesSeleccionado.length > 0) {
            var interesId = interesSeleccionado.val();
            var interesNombre = interesSeleccionado.text();

            if (interesesSeleccionados.find('li[data-id="' + interesId + '"]').length === 0) {
                interesesSeleccionados.append('<li data-id="' + interesId + '">' + interesNombre + '</li>');
            } else {
                alert('Este interés ya ha sido seleccionado.');
            }
        } else {
            alert('Seleccione un interés para agregar.');
        }
    });
});


//modal

// Seleccionar el botón y el modal
var btnModal = document.getElementById("btn-modal");
var miModal = document.getElementById("mi-modal");

// Seleccionar el botón de cerrar el modal
var btnCerrar = miModal.getElementsByClassName("cerrar")[0];

// Función para abrir el modal
function abrirModal() {
    miModal.style.display = "block";
    $("#categorias").click(function (event) {
        event.stopPropagation();
    });
    $('#intereses').click(function (event) {
        event.stopPropagation(); // Evita que el evento se propague al modal
        event.preventDefault(); // Evita que el formulario se envíe y la página se recargue
        // Resto del código para agregar el interés
    });


}

// Función para cerrar el modal
function cerrarModal() {
    miModal.style.display = "none";
}

// Asignar eventos a los botones
btnModal.addEventListener("click", abrirModal);
btnCerrar.addEventListener("click", cerrarModal);
miModal.addEventListener("click", cerrarModal);





$(document).ready(function () {
    $('.btn-eliminar-interes').click(function () {
        var idInteres = $(this).data('id-interes');
        $.ajax({
            url: '@Url.Action("eliminarInteres", "Perfil")',
            type: 'POST',
            data: { idInteres: idInteres },
            success: function () {
                Swal.fire({
                    icon: 'success',
                    title: 'Éxito',
                    text: 'Interés eliminado correctamente'
                }).then(function () {
                    location.reload();
                });
            },
            error: function () {
                // Manejar el error
            }
        });
    });
});


$('#intereses').on('change', function () {
    var idInteres = $(this).val();
    var interes = $('#intereses option:selected').text();

    // Actualizar el valor del input con los id de los intereses seleccionados
    var interesesSeleccionados = $('#intereses-seleccionados').val();
    if (interesesSeleccionados === "") {
        $('#intereses-seleccionados').val(idInteres);
    } else {
        $('#intereses-seleccionados').val(interesesSeleccionados + "," + idInteres);
    }

    $('#lista-intereses').append('<li>' + interes + '</li>');

    // Limpia el select de intereses seleccionados para que el usuario pueda agregar nuevos intereses
    $('#intereses option:selected').remove();
});


// Controlador del evento click del botón para agregar el interés
$('button').on('click', function (event) {
    event.stopPropagation(); // Evita que el evento se propague al modal
    event.preventDefault(); // Evita que el formulario se envíe y la página se recargue

    // Verificar si el cliente ha alcanzado el límite de intereses permitidos
    $.ajax({
        type: "GET",
        url: "@Url.Action("ObtenerDatosInteres","Perfil")",
        success: function (data) {
            if (data.InteresesDisponibles <= 0) {
                alert("Ya ha seleccionado el máximo de intereses permitidos.");
                return;
            }

            // Agregar el interés a la base de datos
            var idInteres = $('#intereses').val();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log("Error al obtener el límite de intereses: " + errorThrown);
        }
    });
});

$(document).ready(function () {
    $('#agregar-interes').click(function () {
        var interesesSeleccionados = $('#intereses-seleccionados').val();

        $.ajax({
            type: "POST",
            url: "@Url.Action("AgregarInteres","Perfil")",
            data: { idInteres: interesesSeleccionados },
            success: function (data) {
                Swal.fire({
                    icon: 'success',
                    title: 'Interés agregado con éxito',
                    showConfirmButton: false,
                    timer: 1500
                }).then(() => {
                    location.reload();
                });
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log("Error al agregar el interés: " + errorThrown);
            }
        });
    });
});







$('#eliminar-interes').on('click', function () {
    var interesesSeleccionados = $('#intereses-seleccionados').val();
    var listaIntereses = $('#lista-intereses');

    // Elimina la última coma y el espacio de la cadena de intereses seleccionados
    interesesSeleccionados = interesesSeleccionados.substring(0, interesesSeleccionados.length - 2);

    // Obtiene el último interés agregado a la lista y lo agrega al select de intereses
    var ultimoInteres = listaIntereses.children('li:last').text();
    var opcion = $('<option>').val('').text(ultimoInteres);
    $('#intereses').append(opcion);

    // Elimina el último elemento de la lista de intereses seleccionados
    listaIntereses.children('li:last').remove();

    // Actualiza el valor del input con los id de los intereses seleccionados
    $('#intereses-seleccionados').val(interesesSeleccionados);
});






$.ajax({
    type: "GET",
    url: "@Url.Action("ObtenerDatosInteres","Perfil")",
    success: function (data) {
        $("#max-intereses-label").text("Cantidad de intereses disponibles: " + data.InteresesDisponibles);
    },
    error: function (jqXHR, textStatus, errorThrown) {
        console.log("Error al obtener el límite de intereses: " + errorThrown);
    }
});
