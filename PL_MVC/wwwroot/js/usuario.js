
$(document).ready(() => {
    getAll();
    rolGetAll();

    var imgBase64 = null;
});


function guardar() {
    let idUsuario = $('#IdUsuario').val();

    if (idUsuario == 0) {
        add();
    } else {
        update();
    }
}
function rolGetAll() {
    $.ajax({
        url: RolGetAllEndPoint,
        type: 'GET',
        success: function (result) {
            console.log(result)
            var ddlRol = $('#IdRol');
            ddlRol.empty();
            ddlRol.append('<option value="0">Selecciona uno</option>');

            $.each(result.objects, function (i, rol) {
                ddlRol.append('<option value="' + rol.idRol + '">' + rol.nombreRol + '</option>');
            });
        },
        error: function (xhr, status, error) {
            console.log('Error al cargar los roles:', error);
        }
    });
}

function getAll() {
    $.ajax({
        url: UsuarioGetAllEndPoint,
        type: "GET",
        dataType: "JSON",
        success: function (result) {
            var tbody = $("#usuarioTableBody");
            tbody.empty();

            if (result != null) {
                result.objects.forEach(function (registro) {

                    var partes = registro.fechaNacimiento.split('-');
                    var fechaNacimiento = new Date(partes[2] + '-' + partes[1] + '-' + partes[0]); // yyyy-mm-dd

                    // Calcular edad
                    var hoy = new Date();
                    var edad = hoy.getFullYear() - fechaNacimiento.getFullYear();
                    var mes = hoy.getMonth() - fechaNacimiento.getMonth();
                    if (mes < 0 || (mes === 0 && hoy.getDate() < fechaNacimiento.getDate())) {
                        edad--;
                    }

                    imgBase64 = registro.imagen;

                    if (imgBase64) {
                        $('#imgUsuario').attr('src', 'data:image/*;base64,' + imgBase64);
                    } else {
                        $('#imgUsuario').removeAttr('src'); // Opcional: limpia la imagen si está vacía
                    }

                    var tag = `
                        <tr>
                            <td>
                                <button class="btn btn-warning btn-sm" onclick="openModal(${registro.idUsuario})">
                                    <i class="bi bi-pencil-fill"></i>
                                </button>
                            </td>
                            <td>
                                <button class="btn btn-danger btn-sm" onclick="eliminarUsuario(${registro.idUsuario})">
                                    <i class="bi bi-trash-fill"></i>
                                </button>
                            </td>
                            <td>
                                <img src="${imgBase64 ? 'data:image/*;base64,' + imgBase64 : 'https://cdn-icons-png.flaticon.com/512/1077/1077114.png'}" 
         alt="Imagen base64" style="width: 30%;">
                            </td>

                            <td>${registro.nombreUsuario} ${registro.apellidoPaterno} ${registro.apellidoMaterno}</td>
                            <td>${registro.telefono}</td>
                            <td>${edad}</td>
                            <td>${registro.rol.nombreRol}</td>
                            <td>${registro.genero.nombreGenero}</td>
                            <td>
                                ${registro.direccion.calle} ${registro.direccion.numeroExterior} ${registro.direccion.numeroInterior},
                                ${registro.direccion.colonia.nombreColonia}, ${registro.direccion.colonia.codigoPostal}
                            </td>
                            <td>
                                <a href='/Poliza/GetAllByUsuario?idUsuario=${registro.idUsuario}'">Ver</a>
                            </td>
                            
                        </tr>
                    `;
                    tbody.append(tag);
                });
            }
        },
        error: function (xhr) {
            console.log(xhr);
        }
    });
}


function add() {

    let usuario = getInfo();

    $.ajax({
        url: UsuarioAddEndPoint,
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(usuario),
        success: function (response) {
            alert('Usuario guardado correctamente.');
            $('#modalAgregarUsuario').modal('hide');
            getAll();
        },
        error: function (xhr, status, error) {
            alert('Error al guardar el usuario: ' + error);
        }
    });
}
function update() {

    let usuario = getInfo();

    $.ajax({
        url: UsuarioUpdateEndPoint,
        type: 'PUT',
        contentType: 'application/json',
        data: JSON.stringify(usuario),
        success: function (response) {
            alert('Usuario guardado correctamente.');
            $('#modalAgregarUsuario').modal('hide');
            getAll();
        },
        error: function (xhr, status, error) {
            alert('Error al guardar el usuario: ' + error);
        }
    });
}

function getInfo() {

    let usuario = {
        IdUsuario: $('#IdUsuario').val(),
        NombreUsuario: $('#NombreUsuario').val(),
        ApellidoMaterno: $('#ApellidoMaterno').val(),
        ApellidoPaterno: $('#ApellidoPaterno').val(),
        Correo: $('#Correo').val(),
        ImagenBase64: imgBase64,
        Contraseña: $('#Contraseña').val(),
        Telefono: $('#Telefono').val(),
        FechaNacimiento: $('#FechaNacimiento').val(),
        Genero: {
            IdGenero: parseInt($('input[name="IdGenero"]:checked').val())
        },
        Rol: {
            IdRol: parseInt($('#IdRol').val())

        },
        Direccion: {
            Calle: $('#Calle').val(),
            NumeroInterior: $('#NumeroInterior').val(),
            NumeroExterior: $('#NumeroExterior').val(),
            Colonia: {
                IdColonia: parseInt($('#IdColonia').val())
            }

        }
    };

    return usuario;
}

function eliminarUsuario(idUsuario) {
    if (confirm('¿Estás seguro de que deseas eliminar este usuario?')) {
        $.ajax({
            url: UsuarioDeleteEndPoint + idUsuario,
            type: 'DELETE',
            success: function (result) {
                if (result.correct) {
                    alert('Usuario eliminado correctamente.');
                    getAll(); // Vuelve a cargar la tabla o lista
                } else {
                    alert('Error al eliminar el usuario: ' + result.errorMessage);
                }
            },
            error: function (xhr, status, error) {
                console.error('Error en la petición AJAX:', error);
            }
        });
    }
}

function getById(idUsuario) {
    $.ajax({
        url: UsuarioGetByIdEndPoint + idUsuario,
        type: 'GET',
        success: function (result) {
            if (result.correct) {
                var usuario = result.object;

                imgBase64 = usuario.imagenBase64;

                if (imgBase64) {
                    $('#imgUsuario').attr('src', 'data:image/*;base64,' + imgBase64);
                } else {
                    $('#imgUsuario').removeAttr('src'); // Opcional: limpia la imagen si está vacía
                }

                $('#IdUsuario').val(usuario.idUsuario);
                $('#NombreUsuario').val(usuario.nombreUsuario);
                $('#ApellidoPaterno').val(usuario.apellidoPaterno);
                $('#ApellidoMaterno').val(usuario.apellidoMaterno);
                $('#Correo').val(usuario.correo);
                $('#Telefono').val(usuario.telefono);
                $('#FechaNacimiento').val(usuario.fechaNacimiento);

                $('input[name="IdGenero"][value="' + usuario.genero.idGenero + '"]').prop('checked', true);

                $('#IdRol').val(usuario.rol.idRol);

                $('#Calle').val(usuario.direccion.calle);
                $('#NumeroInterior').val(usuario.direccion.numeroInterior);
                $('#NumeroExterior').val(usuario.direccion.numeroExterior);
                $('#IdPais').val(usuario.direccion.colonia.municipio.estado.pais.idPais);

                seleccionarDDLDireccion(usuario);

            } else {
                alert('No se pudo obtener la información del usuario: ' + result.errorMessage);
            }
        },
        error: function (xhr, status, error) {
            console.error('Error en la petición AJAX:', error);
        }
    });
}

async function seleccionarDDLDireccion(usuario) {
    // Espera que cada combo se llene antes de asignar valor
    await getEstadosPorPais(usuario.direccion.colonia.municipio.estado.pais.idPais);
    $('#IdEstado').val(usuario.direccion.colonia.municipio.estado.idEstado);

    await getMunicipiosPorEstado(usuario.direccion.colonia.municipio.estado.idEstado);
    $('#IdMunicipio').val(usuario.direccion.colonia.municipio.idMunicipio);

    await getColoniasPorMunicipio(usuario.direccion.colonia.municipio.idMunicipio);
    $('#IdColonia').val(usuario.direccion.colonia.idColonia);

    // Ahora sí, muestra el modal ya con todo cargado
    //$('#modalAgregarUsuario').modal('show');
}

function openModal(idUsuario) {

    limpiarModal();

    if (idUsuario > 0) {
        getById(idUsuario);
    }

    $("#modalAgregarUsuario").modal("show");
}

function limpiarModal() {
    // Limpiar inputs
    $('#IdUsuario').val('0');
    $('#NombreUsuario').val('');
    $('#ApellidoPaterno').val('');
    $('#ApellidoMaterno').val('');
    $('#Correo').val('');
    $('#Contraseña').val('');
    $('#Telefono').val('');
    $('#FechaNacimiento').val('');
    $('#Imagen').val('');
    $('#imgUsuario').attr('src', '');
    imgBase64 = null;

    // Radio buttons de género
    $('input[name="Genero"]').prop('checked', false);

    //// Selects
    $('#IdRol').val('0');   // Rol: seleccionar opción por default
    $('#IdPais').val('0');  // País: seleccionar opción por default

    //// Estado: dejar solo opción 0
    $('#IdEstado').find('option').not('[value="0"]').remove();
    $('#IdEstado').val('0');

    //// Municipio: dejar solo opción 0
    $('#IdMunicipio').find('option').not('[value="0"]').remove();
    $('#IdMunicipio').val('0');

    //// Colonia: dejar solo opción 0
    $('#IdColonia').find('option').not('[value="0"]').remove();
    $('#IdColonia').val('0');

    // Dirección
    $('#Calle').val('');
    $('#NumeroInterior').val('');
    $('#NumeroExterior').val('');
}

function validarImagen() {
    var archivo = $('#Imagen')[0].files[0];

    if (!archivo) return;

    var extensionesPermitidas = ['image/jpeg', 'image/png', 'image/jpg', 'image/gif'];

    if (!extensionesPermitidas.includes(archivo.type)) {
        alert('Por favor selecciona un archivo de imagen válido (jpeg, jpg, png, gif).');
        $('#Imagen').val(''); // Limpia el input
        $('#imgUsuario').attr('src', ''); // Limpia la vista previa
    }
}


function previsualizarImagen() {
    convertirImagenABase64(document.getElementById('Imagen'), function (base64) {
        $('#imgUsuario').attr('src', base64);
    });
}

function convertirImagenABase64(input, callback) {
    var archivo = input.files[0];

    if (archivo) {
        var lector = new FileReader();

        lector.onload = function (e) {
            var base64 = e.target.result;
            imgBase64 = base64.substring(base64.indexOf(',') + 1);
            callback(base64); // Llama a la función que quieras con el resultado
        }

        lector.readAsDataURL(archivo);
    } else {
        callback(null);
    }
}