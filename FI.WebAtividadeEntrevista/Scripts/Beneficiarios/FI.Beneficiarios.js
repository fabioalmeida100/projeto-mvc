$(document).ready(function () {
    var idBeneficiario = 0;
    LoadBeneficiarios();
    $('#formCadastroBeneficiario').submit(function (e) {
        e.preventDefault();
        AddBeneficiario();
    })
})

function AddBeneficiario() {  
    var empObj = {};

    if (idBeneficiario == 0) {
        empObj = {
            Nome: $('#NomeBeneficiario').val(),
            CPF: $('#CPFBeneficiario').val(),
            IdCliente: ObterIdCliente()
        };
    }
    else {
        empObj = {
            Nome: $('#NomeBeneficiario').val(),
            CPF: $('#CPFBeneficiario').val(),
            Id: idBeneficiario,
            IdCliente: ObterIdCliente()
        };
    }

    $.ajax({
        url: "/Beneficiario/Incluir",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            LoadBeneficiarios();
            ClearAllTextBox();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function LoadBeneficiarios() {
    $.ajax({
        url: "/Beneficiario/Listar",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            html += '<thead>';
            html += '    <tr>';
            html += '        <th>CPF</th>';
            html += '        <th>Nome</th>';
            html += '        <th></th>';
            html += '        <th></th>';
            html += '    </tr>';
            html += '</thead>';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '  <td>' + item.CPF + '</td>';
                html += '  <td>' + item.Nome + '</td>';
                html += '  <td><a class="btn btn-primary btn-sm" href="#" onclick="return GetBeneficiarioById(' + item.Id + ')">Alterar</a></td> ';
                html += '  <td><a class="btn btn-primary btn-sm" href="#" onclick="DeleteBeneficiarioById(' + item.Id + ')">Excluir</a></td>';
                html += '</tr>';
            });
            $('#gridBeneficiarios').html(html);

            idBeneficiario = 0;
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function GetBeneficiarioById(id) {
    $.ajax({
        url: "/Beneficiario/GetByID/" + id,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#CPFBeneficiario').val(result.CPF);
            $('#NomeBeneficiario').val(result.Nome);
            idBeneficiario = result.Id;
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

function DeleteBeneficiarioById(id) {
    var ans = confirm("Deseja excluir este registro?");
    if (ans) {
        $.ajax({
            url: "/Beneficiario/Delete/" + id,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                LoadBeneficiarios();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

function ClearAllTextBox() {
    $('#CPFBeneficiario').val("");
    $('#NomeBeneficiario').val("");
}

function ObterIdCliente() {
    var url = window.location.href;
    var array = url.split('/');

    return array[array.length - 1].replace('#', '');    
}
