$(document).ready(function () {

    cargarEstatus();
    cargarGenero();
    cargarTipoPoliza();
});

function openModal(id) {

    if (id == 0) {
        limpiarModal();
        obtenerNumeroPoliza()

    }


    $('#modalAgregarPoliza').modal('show');
}

function limpiarModal() {
    // Limpiar inputs por id
    $('#NumeroPoliza').val('');
    $('#MontoPrima').val('');
    $('#FechaInicio').val('');
    $('#FechaFin').val('');

    $('#NumeroPoliza').prop("readonly", true);

    $('#EsEdicion').val("false");


    // Limpiar selects por id (solo quitar selección, mantener opciones)
    $('#IdTipoPoliza').prop('selectedIndex', 0);
    $('#IdGenero').prop('selectedIndex', 0);
}


function guardar() {
    var esEdicion = $('#EsEdicion').val() === "true";

    if (esEdicion) {
        Update();
    } else {
        AddPoliza();
    }
}

function UpdatePoliza() {
    var poliza = {
        numeroPoliza: $('#NumeroPoliza').val(),
        montoPrima: parseFloat($('#MontoPrima').val()),
        fechaInicio: $('#FechaInicio').val(),
        fechaFinal: $('#FechaFin').val(),
        tipoPoliza: { idTipoPoliza: parseInt($('#IdTipoPoliza').val()) },
        estatus: { idEstatus: parseInt($('#IdEstatus').val()) }
    };

    $.ajax({
        url: PolizaUpdateEndPoint,
        type: 'PUT',
        data: JSON.stringify(poliza),
        contentType: 'application/json',
        success: function (response) {
            if (response.correct) {
                $('#modalAgregarPoliza').modal('hide');
                getAllPoliza();
            } else {
                alert('Error al actualizar: ' + response.errorMessage);
            }
        }
    });
}


function AddPoliza() {

    let montoStr = $('#MontoPrima').val().replace(/,/g, '');
    let idUsuario = $('#idUsuario').val();

    var poliza = {
        NumeroPoliza: parseInt($('#NumeroPoliza').val()),
        MontoPrima: parseFloat(montoStr),
        FechaInicio: $('#FechaInicio').val(),
        FechaFinal: $('#FechaFin').val(),
        TipoPoliza: { IdTipoPoliza: parseInt($('#IdTipoPoliza').val()) },
        Usuario: {
            IdUsuario: idUsuario
        }
    };

    $.ajax({
        url: PolizaAddEndPoint,
        type: 'POST',
        data: JSON.stringify(poliza),
        contentType: 'application/json',
        success: function (response) {
            if (response.correct) {
                $('#modalAgregarPoliza').modal('hide');
                getAllPoliza(idUsuario);
            } else {
                alert('Error al agregar: ' + response.errorMessage);
            }
        }
    });
}


function getAllPoliza(idUsuario) {
    $.ajax({
        url: PolizaGetAllEndPoint + idUsuario,
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            if (response.correct) {
                var tbody = $('#polizaTableBody');
                tbody.empty();

                const puedeEditarEliminar = sessionRol === 'Admin' || sessionRol === 'Broker';
                response.objects.forEach(function (item) {
                    let row = '<tr>';

                    if (puedeEditarEliminar) {
                        row += `
        <td>
            <button class="btn btn-sm btn-warning" onclick="GetById(${item.numeroPoliza})">
                <i class="bi bi-pencil-square"></i>
            </button>
        </td>
        <td>
            <button class="btn btn-sm btn-danger" onclick="Delete(${item.numeroPoliza})">
                <i class="bi bi-trash"></i>
            </button>
        </td>`;
                    }

                    let estatusHtml = '';
                    if (item.estatus.idEstatus === 1 && puedeEditarEliminar) {
                        estatusHtml = `<select class="form-select form-select-sm" onchange="cambiarEstatus(this, ${item.numeroPoliza})">`;

                        estatusList.forEach(function (estatus) {
                            const selected = item.estatus.idEstatus === estatus.idEstatus ? 'selected' : '';
                            estatusHtml += `<option value="${estatus.idEstatus}" ${selected}>${estatus.nombreEstatus}</option>`;
                        });

                        estatusHtml += `</select>`;
                    } else {
                        estatusHtml = item.estatus.nombreEstatus ?? '';
                    }

                    row += `
        <td>${item.numeroPoliza ?? ''}</td>
        <td>${item.tipoPoliza.nombre ?? ''}</td>
        <td>${item.fechaInicio ?? ''}</td>
        <td>${item.fechaFinal ?? ''}</td>
        <td>$ $${item.montoPrima != null ? item.montoPrima.toLocaleString() : ''}</td>
        <td>${estatusHtml}</td>
    </tr>`;

                    tbody.append(row);
                });


            } else {
                alert('Error: ' + response.errorMessage);
            }
        },
        error: function (xhr, status, error) {
            alert('Error en la petición AJAX: ' + error);
        }
    });
}


function cambiarEstatus(selectElement, numeroPoliza) {
    var nuevoIdEstatus = selectElement.value;

    $.ajax({
        url: EstatusCambioEndPoint + numeroPoliza + '/' + nuevoIdEstatus,
        type: 'GET',
        contentType: 'application/json',
        data: JSON.stringify({
            NumeroPoliza: numeroPoliza,
            IdEstatus: parseInt(nuevoIdEstatus)
        }),
        success: function (response) {
            if (response.correct) {
                alert('Estatus actualizado correctamente.');
                location.reload();
            } else {
                alert('Error al actualizar el estatus.');
            }
        },
        error: function () {
            alert('Error en la petición.');
        }
    });
}

function GetById(id) {
    $.ajax({
        url: PolizaGetByIdEndPoint + id,
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            if (response.correct) {
                const poliza = response.object;

                limpiarModal();

                $('#EsEdicion').val("true");
                $('#NumeroPoliza').val(poliza.numeroPoliza);
                $('#MontoPrima').val(poliza.montoPrima);
                $('#FechaInicio').val(poliza.fechaInicio);
                $('#FechaFin').val(poliza.fechaFinal);
                $('#IdTipoPoliza').val(poliza.tipoPoliza?.idTipoPoliza);

                $('#IdEstatus').val(poliza.estatus?.idEstatus);
                $('#idUsuario').val(poliza.usuario.idUsuario);
                // etc.

                openModal(id);
            } else {
                alert('Error: ' + response.errorMessage);
            }
        },
        error: function (xhr, status, error) {
            console.error('Error AJAX:', error);
            alert('Ocurrió un error al obtener la póliza.');
        }
    });
}

function DeletePoliza(numeroPoliza) {
    if (!confirm("¿Estás seguro de eliminar esta póliza?")) {
        return;
    }

    $.ajax({
        url: PolizaDeleteEndPoint + numeroPoliza,
        type: 'DELETE',
        success: function (response) {
            if (response.correct) {
                alert("Póliza eliminada correctamente.");
                // Opcional: refrescar la lista
                let idUsuario = $('#idUsuario').val();
                getAllPoliza(idUsuario);
            } else {
                alert("Error al eliminar: " + response.errorMessage);
            }
        },
        error: function (xhr, status, error) {
            console.error("Error en la eliminación:", error);
            alert("Ocurrió un error al eliminar la póliza.");
        }
    });
}


function cargarEstatus() {
    $.ajax({
        url: EstatusGetAllEndPoint, // Ajusta esta URL según tu API
        type: 'GET',
        dataType: 'json',
        success: function (result) {
            if (result.correct) {
                const estatusSelect = $('#IdEstatus');
                estatusSelect.empty(); // Limpia opciones actuales
                estatusSelect.append('<option selected disabled>Selecciona uno</option>');

                if (result.objects && result.objects.length > 0) {
                    result.objects.forEach(function (estatus) {
                        estatusSelect.append(`<option value="${estatus.idEstatus}">${estatus.nombreEstatus}</option>`);
                    });
                }
            } else {
                alert("ERROR: " + result.errorMessage)
            }

        },
        error: function (xhr, status, error) {
            console.error('Error al cargar estatus:', error);
        }
    });
}

function cargarGenero() {
    $.ajax({
        url: GeneroGetAllEndPoint, // Ajusta esta URL según tu API
        type: 'GET',
        dataType: 'json',
        success: function (result) {
            if (result.correct) {
                const estatusSelect = $('#IdGenero');
                estatusSelect.empty(); // Limpia opciones actuales
                estatusSelect.append('<option selected disabled>Selecciona uno</option>');

                if (result.objects && result.objects.length > 0) {
                    result.objects.forEach(function (estatus) {
                        estatusSelect.append(`<option value="${estatus.idGenero}">${estatus.nombreGenero}</option>`);
                    });
                }
            } else {
                alert("ERROR: " + result.errorMessage)
            }

        },
        error: function (xhr, status, error) {
            console.error('Error al cargar estatus:', error);
        }
    });
}

function cargarTipoPoliza() {
    $.ajax({
        url: TipoPolizaGetAllEndPoint,
        type: 'GET',
        dataType: 'json',
        success: function (result) {
            if (result.correct) {
                console.log(result)
                const tipoPolizaSelect = $('#IdTipoPoliza');
                tipoPolizaSelect.empty(); // Limpia opciones actuales
                tipoPolizaSelect.append('<option selected disabled>Selecciona uno</option>');

                if (result.objects && result.objects.length > 0) {
                    result.objects.forEach(function (tipoPoliza) {
                        tipoPolizaSelect.append(`<option value="${tipoPoliza.idTipoPoliza}">${tipoPoliza.nombre}</option>`);
                    });
                }
            } else {
                alert("ERROR: " + result.errorMessage);
            }
        },
        error: function (xhr, status, error) {
            console.error('Error al cargar tipo de póliza:', error);
        }
    });
}

function obtenerNumeroPoliza() {
    $.ajax({
        url: ObtenerNumeroPolizaEndPoint,
        type: 'GET',
        success: function (response) {
            if (response.correct) {
                $('#NumeroPoliza').val(response.object);
            } else {
                alert('Error al generar el número de póliza: ' + response.errorMessage);
            }
        },
        error: function (xhr, status, error) {
            console.log('Error AJAX:', error);
            alert('Ocurrió un error al generar el número de póliza.');
        }
    });

}

function calcularMonto(select) {
    var genero = parseInt($(select).val());
    var monto = 0;

    if (genero === 1) {
        monto = 2000;
    } else if (genero === 2) {
        monto = 2500;
    } else {
        monto = 0;
    }

    // Formatear con comas de miles
    var montoFormateado = monto.toLocaleString('es-MX', { minimumFractionDigits: 2, maximumFractionDigits: 2 });

    $('#MontoPrima').val(montoFormateado);
}
