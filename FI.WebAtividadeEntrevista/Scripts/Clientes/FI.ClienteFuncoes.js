$(document).ready(function () {
    $("#CPF").mask("999.999.999-99");
});

$('#btnBeneficiario').on('click', function (e) {
    e.preventDefault();
    $('#modalBeneficiario').modal('show').find('.modal-content').load($(this).attr('href')); 
});

$('#modalBeneficiario').on('shown.bs.modal', function (e) {
    $("#CPFBeneficiario").mask("999.999.999-99");
})