const API_URL = "http://localhost:5233/api/Usuario/";

$(document).ready(() => {
    getAll();
});

let getAll = () => {
    $.ajax({
        url: API_URL + "UsuarioGetAll",
        type: "GET",
        dataType: "JSON",
        success: (result) => {
            console.log(result);
            $("#tbody").empty();
            if (result != null) {
                for (let registro of result.objects) {
                    let tbody = `
                        
                        <tr>
                            <td>Editar</td>
                            <td>${registro.nombreUsuario}</td>
                            <td>${registro.apellidoPaterno} ${registro.ApellidoMaterno}</td>
                            <td>${registro.correo}</td>
                            <td>${registro.teléfono}</td>
                            <td>${registro.rol.nombreRol}</td>
                            <td>${registro.genero.nombreGenero}</td>
                            <td>${registro.fechaNacimiento}</td>
                            <td>${registro.direccion.calle} ${registro.direccion.numeroExterior} ${registro.direccion.numeroInterior} ${registro.direccion.numeroExterior} ${registro.direccion.colonia.nombreColonia} ${registro.direccion.colonia.nombreColonia} </td>
                            <td>Eliminar</td>
                        </tr>
                    `;
                    $("#tbody").append(tbody);
                }
            }
        },
        error: (xhr) => {
            console.log(xhr);
        },
    });
};