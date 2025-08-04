$(document).ready(() => {
    paisGetAll();
});

function paisGetAll() {
    $.ajax({
        url: PaisGetAllEndPoint,
        type: 'GET',
        success: function (result) {

            var ddlPais = $('#IdPais');
            ddlPais.empty();
            ddlPais.append('<option value="0">Selecciona uno</option>');
            $.each(result.objects, function (i, pais) {
                ddlPais.append('<option value="' + pais.idPais + '">' + pais.nombrePais + '</option>');
            });
        },
        error: function (xhr, status, error) {
            console.log('Error al cargar los países:', error);
        }
    });

}

async function getEstadosPorPais(idPais) {
    if (idPais === '0') {
        $('#IdEstado').empty().append('<option value="0">Selecciona uno</option>');
        return;
    }
    try {
        const result = await $.ajax({
            url: EstadoGetByIdPaisEndPoint + idPais,
            type: 'GET'
        });

        var ddlEstado = $('#IdEstado');
        ddlEstado.empty();
        ddlEstado.append('<option value="0">Selecciona uno</option>');
        $('#IdMunicipio').empty().append('<option value="0">Selecciona uno</option>');
        $('#IdColonia').empty().append('<option value="0">Selecciona uno</option>');

        $.each(result.objects, function (i, estado) {
            ddlEstado.append('<option value="' + estado.idEstado + '">' + estado.nombreEstado + '</option>');
        });
    } catch (error) {
        console.log('Error al cargar los estados:', error);
    }
}

async function getMunicipiosPorEstado(idEstado) {
    if (idEstado === '0') {
        $('#IdMunicipio').empty().append('<option value="0">Selecciona uno</option>');
        $('#IdColonia').empty().append('<option value="0">Selecciona uno</option>');
        return;
    }
    try {
        const result = await $.ajax({
            url: MunicipioGetByIdEstado + idEstado,
            type: 'GET'
        });

        var ddlMunicipio = $('#IdMunicipio');
        ddlMunicipio.empty();
        ddlMunicipio.append('<option value="0">Selecciona uno</option>');
        $('#IdColonia').empty().append('<option value="0">Selecciona uno</option>');

        $.each(result.objects, function (i, municipio) {
            ddlMunicipio.append('<option value="' + municipio.idMunicipio + '">' + municipio.nombreMunicipio + '</option>');
        });
    } catch (error) {
        console.log('Error al cargar los municipios:', error);
    }
}

async function getColoniasPorMunicipio(idMunicipio) {
    if (idMunicipio === '0') {
        $('#IdColonia').empty().append('<option value="0">Selecciona uno</option>');
        return;
    }
    try {
        const result = await $.ajax({
            url: ColoniaGetByIdMunicipio + idMunicipio,
            type: 'GET'
        });

        var ddlColonia = $('#IdColonia');
        ddlColonia.empty();
        ddlColonia.append('<option value="0">Selecciona uno</option>');

        $.each(result.objects, function (i, colonia) {
            ddlColonia.append('<option value="' + colonia.idColonia + '">' + colonia.nombreColonia + '</option>');
        });
    } catch (error) {
        console.log('Error al cargar las colonias:', error);
    }
}


//function getEstadosPorPais(idPais) {
//    if (idPais != '0') {
//        $.ajax({
//            url: EstadoGetByIdPaisEndPoint + idPais,
//            type: 'GET',
//            success: function (result) {
//                var ddlEstado = $('#IdEstado');
//                ddlEstado.empty();
//                ddlEstado.append('<option value="0">Selecciona uno</option>');
//                var ddlMunicipio = $('#IdMunicipio');
//                ddlMunicipio.empty().append('<option value="0">Selecciona uno</option>');
//                $('#IdColonia').empty().append('<option value="0">Selecciona uno</option>');

//                $.each(result.objects, function (i, estado) {
//                    ddlEstado.append('<option value="' + estado.idEstado + '">' + estado.nombreEstado + '</option>');
//                });
//            },
//            error: function (xhr, status, error) {
//                console.log('Error al cargar los estados:', error);
//            }
//        });
//    } else {
//        $('#IdEstado').empty().append('<option value="0">Selecciona uno</option>');
//    }
//}

//function getMunicipiosPorEstado(idEstado) {
//    if (idEstado != '0') {
//        $.ajax({
//            url: MunicipioGetByIdEstado + idEstado,
//            type: 'GET',
//            success: function (result) {
//                var ddlMunicipio = $('#IdMunicipio');
//                ddlMunicipio.empty().append('<option value="0">Selecciona uno</option>');
//                $('#IdColonia').empty().append('<option value="0">Selecciona uno</option>');

//                $.each(result.objects, function (i, municipio) {
//                    ddlMunicipio.append('<option value="' + municipio.idMunicipio + '">' + municipio.nombreMunicipio + '</option>');
//                });
//            },
//            error: function (xhr, status, error) {
//                console.log('Error al cargar los municipios:', error);
//            }
//        });
//    }
//}

//function getColoniasPorMunicipio(idMunicipio) {
//    if (idMunicipio != '0') {
//        $.ajax({
//            url: ColoniaGetByIdMunicipio + idMunicipio,
//            type: 'GET',
//            success: function (result) {
//                var ddlColonia = $('#IdColonia');
//                ddlColonia.empty().append('<option value="0">Selecciona uno</option>');

//                $.each(result.objects, function (i, colonia) {
//                    ddlColonia.append('<option value="' + colonia.idColonia + '">' + colonia.nombreColonia + '</option>');
//                });
//            },
//            error: function (xhr, status, error) {
//                console.log('Error al cargar las colonias:', error);
//            }
//        });
//    }
//}
