var sub = 0;
var igv = 0;
var tot = 0;
//formato fecha dd/mm/aaaa
var now = new Date();
var day = ("0" + now.getDate()).slice(-2);
var month = ("0" + (now.getMonth() + 1)).slice(-2);
var today = now.getFullYear() + "-" + (month) + "-" + (day);
$('#idfechapedido').val(today);
objDatosColumna = new Array();

//eliminar - index cliente
$("body").on('click', '.eliminarCliente', function () {
    var vidcliente = $(this).data('idcliente');
    var parametro = { idcliente: vidcliente };

    Swal.fire({
        title: '¿Esta seguro de eliminar el registro?',
        text: "No podras revertir si eliminas!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si',
        cancelButtonText: 'No'
    }).then((result) => {
        if (result.isConfirmed) {
            $.post("/Cliente/Eliminar", parametro).done(function (results) {
                if (results = true) {
                    location.reload();
                }
            })
        }
    })
});

//eliminar - index producto
$("body").on('click', '.eliminarProducto', function () {
    var vidproducto = $(this).data('idproducto');
    var parametro = { idproducto: vidproducto };
    Swal.fire({
        title: '¿Esta seguro de eliminar el registro?',
        text: "No podras revertir si eliminas!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si',
        cancelButtonText: 'No'
    }).then((result) => {
        if (result.isConfirmed) {
            $.post("/Producto/Eliminar", parametro).done(function (results) {
                if (results = true) {
                    location.reload();
                }
            })
        }
    })
});

//eliminar - index pedido
$("body").on('click', '.eliminarPedido', function () {
    var vididpedido = $(this).data('idpedido');
    var parametro = { idpedido: vididpedido };
    Swal.fire({
        title: '¿Esta seguro de eliminar el registro?',
        text: "No podras revertir si eliminas!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si',
        cancelButtonText: 'No'
    }).then((result) => {
        if (result.isConfirmed) {
            $.post("/Pedido/Eliminar", parametro).done(function (results) {
                if (results = true) {
                    location.reload();
                }
            })
        }
    })
});

//seteando valores a modal editar cliente
$("body").on('click', '.editarCliente', function () {
    var vidcliente = $(this).data('idcliente');
    var vnomcli = $(this).data('nomcli');
    var vapep = $(this).data('apep');
    var vapem = $(this).data('apem');
    var vdni = $(this).data('dni');
    $("#idclienteModal_e").val(vidcliente);
    $("#nombreModal_e").val(vnomcli);
    $("#appModal_e").val(vapep);
    $("#apmModal_e").val(vapem);
    $("#dniModal_e").val(vdni);
    $('#btnCerrarModalClienteEditar').trigger('click');
});

//seteando valores a modal editar producto
$("body").on('click', '.editarProducto', function () {
    var vidproducto = $(this).data('idproducto');
    var viddescripcion = $(this).data('iddesp');
    var vidprecio = $(this).data('idprecio');
    $("#idproductoModal").val(vidproducto);
    $("#descripcionModal").val(viddescripcion);
    $("#precioModal").val(vidprecio);
    $('#btnCerrarModalClienteEditar').trigger('click');
});

//grabar modal cliente editar
$("body").on('click', '#btnGrabarModalClienteEditar', function () {
    var vidcliente = $('#idclienteModal_e').val();
    var vnomcli = $('#nombreModal_e').val();
    var vapep = $('#appModal_e').val();
    var vapem = $('#apmModal_e').val();
    var vdni = $('#dniModal_e').val();
    var parametro = { idcliente: vidcliente, nomcliente: vnomcli, apcliente: vapep, amcliente: vapem, dni: vdni };
    $.post("/Cliente/Editar", parametro).done(function (results) {
        if (results = true) {
            const Toast = Swal.mixin({
                toast: true,
                position: 'top-end',
                showConfirmButton: false,
                timer: 1500,
                timerProgressBar: true,
                didOpen: (toast) => {
                    toast.addEventListener('mouseenter', Swal.stopTimer)
                    toast.addEventListener('mouseleave', Swal.resumeTimer)
                }
            })
            Toast.fire({
                icon: 'success',
                title: 'Se esta actualizando el registro'
            })
            setTimeout(function () {
                location.reload();
            }, 1500);
        }
    })
});

//grabar modal producto editar
$("body").on('click', '#btnGrabarModalProductoEditar', function () {
    var vidproducto = $('#idproductoModal').val();
    var vdescpro = $('#descripcionModal').val();
    var vprep = $('#precioModal').val();
    var parametro = { idproducto: vidproducto, descpro: vdescpro, prep: vprep };
    $.post("/Producto/Editar", parametro).done(function (results) {
        if (results = true) {
            const Toast = Swal.mixin({
                toast: true,
                position: 'top-end',
                showConfirmButton: false,
                timer: 1500,
                timerProgressBar: true,
                didOpen: (toast) => {
                    toast.addEventListener('mouseenter', Swal.stopTimer)
                    toast.addEventListener('mouseleave', Swal.resumeTimer)
                }
            })
            Toast.fire({
                icon: 'success',
                title: 'Se esta actualizando el registro'
            })
            setTimeout(function () {
                location.reload();
            }, 1500);
        }
    })
});

//listar cliente
listarCliente = function () {
    $.post(
        "/Pedido/listarCliente", {
    }, function (res) {
        $("#tabla_cliente").html(res)
    });
};

//seleccionar cliente grilla modal cliente
$("body").on('click', '.selcliente', function () {
    var vidcliente = $(this).data('idcliente');
    var vnomcom = $(this).data('nombrecompleto');
    $("#idcliente").val(vidcliente);
    $("#nomcliente").val(vnomcom);
    $('#btnCerrarModalCliente').trigger('click');
});

//listar producto
listarProduto = function () {
    $.post(
        "/Pedido/listarProducto", {
    }, function (res) {
        $("#tabla_producto").html(res)
        $("#cantidad").val(1);
    });
};

//seleccionar prducto grilla modal producto
$("body").on('click', '.selproducto', function () {
    var vidproducto = $(this).data('idproducto');
    var vdescripcion = $(this).data('descripcion');
    var vpreprod = $(this).data('precio');
    $("#idproducto").val(vidproducto);
    $("#desprod").val(vdescripcion);
    $("#precprod").val(vpreprod);
    $('#btnCerrarModalProducto').trigger('click');
});

//quitar iten pedido
$("body").on('click', '.selquitar', function () {
    $(this).closest('tr').remove();
    calcularTotal();
});

//agregar iten pedido
$("#btnAgregar").click(function () {
    var idpedido = $('#idpedido').val();
    var idproducto = $('#idproducto').val();
    var producto = $('#desprod').val();
    var cantidad = $('#cantidad').val();
    var precio = $('#precprod').val();
    var subtot = (cantidad * precio) / 1.18;
    var igv = subtot * 0.18;

    var fila = '<tr>' +
        '<td align="right" hidden="hidden">' + idpedido + '</td>' +
        '<td align="right" hidden="hidden">' + idproducto + '</td>' +
        '<td align="right">' + producto + '</td>' +
        '<td align="right">' + cantidad + '</td>' +
        '<td align="right">' + precio + '</td>' +
        '<td align="right">' + subtot.toFixed(2) + '</td>' +
        '<td align="right">' + igv.toFixed(2) + '</td>' +
        "<td class='text-center'>" + "<button type='button' class='btn btn-light selquitar'>Quitar</button>" + "</td>";
    + '</tr>';
    $("#tbdetalle").append(fila);
    calcularTotal();
});

//guardar recorrer grilla pedido - detalle pedido
$(document).ready(function () {
    $("#btnGrabar").click(function () {
        var frmDataPedido = $('#frmDataPedido');
        if (frmDataPedido[0].checkValidity() === false) {
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: 'Faltan ingresar datos.',
                showConfirmButton: false,
                timer: 1500
            })
        } else {
            var $objCuerpoTabla = $("#grilla").children().prev().parent();
            $objCuerpoTabla.find("tbody tr").each(function () {
                var idpe = $(this).find('td').eq(0).html();
                var idpr = $(this).find('td').eq(1).html();
                var prod = $(this).find('td').eq(2).html();
                var cant = $(this).find('td').eq(3).html();
                var pre = $(this).find('td').eq(4).html();
                var sub = (cant * pre) / 1.18;
                var igv = sub * 0.18;
                valor = new Array(idpe, idpr, prod, cant, pre, sub, igv);
                objDatosColumna.push(valor);
            });
            var idpedidocab = $("#idpedido").val();
            var idcliente = $("#idcliente").val();
            var fechapedido = $("#idfechapedido").val();
            var cal_total = tot;
            var parametros = { arraydetalle: objDatosColumna, total_calculado: cal_total, idcliente: idcliente, fechapedido: fechapedido, idpedidocab: idpedidocab };
            $.post("/Pedido/Guardar", parametros).done(function (results) {
                if (results = true) {
                    const Toast = Swal.mixin({
                        toast: true,
                        position: 'top-end',
                        showConfirmButton: false,
                        timer: 2000,
                        timerProgressBar: true,
                        didOpen: (toast) => {
                            toast.addEventListener('mouseenter', Swal.stopTimer)
                            toast.addEventListener('mouseleave', Swal.resumeTimer)
                        }
                    })
                    Toast.fire({
                        icon: 'success',
                        title: 'Se guardo el pedido'
                    })
                    limpiarPedido();

                    setTimeout(function () {
                        location.reload();
                    }, 1500);
                }
            });
        }
        frmDataPedido.addClass('was-validated');
    });
});

//grabar modal cliente nuevo
$(document).ready(function () {
    $("#btnGrabarModalClienteNuevo").click(function () {
        var frmDataClienteGuardar = $('#frmDataClienteGuardar');
        console.log(frmDataClienteGuardar)

        if (frmDataClienteGuardar[0].checkValidity() === false) {
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: 'Faltan ingresar datos.',
                showConfirmButton: false,
                timer: 1500
            })
        } else {
            var vnomcli = $('#nombreModal_n').val();
            var vapep = $('#appModal_n').val();
            var vapem = $('#apmModal_n').val();
            var vdni = $('#dniModal_n').val();
            var parametro = { nomcliente: vnomcli, apcliente: vapep, amcliente: vapem, dni: vdni };
            $.post("/Cliente/Grabar", parametro).done(function (results) {
                if (results = true) {
                    const Toast = Swal.mixin({
                        toast: true,
                        position: 'top-end',
                        showConfirmButton: false,
                        timer: 1500,
                        timerProgressBar: true,
                        didOpen: (toast) => {
                            toast.addEventListener('mouseenter', Swal.stopTimer)
                            toast.addEventListener('mouseleave', Swal.resumeTimer)
                        }
                    })
                    Toast.fire({
                        icon: 'success',
                        title: 'Se esta guardando el registro'
                    })
                    setTimeout(function () {
                        location.reload();
                    }, 1500);
                }
            })
        }
        frmDataClienteGuardar.addClass('was-validated');
    });
});

//grabar modal producto nuevo
$(document).ready(function () {
    $("#btnGrabarModalProductoNuevo").click(function () {
        var frmDataProductoNuevo = $('#frmDataProductoNuevo');
        console.log(frmDataProductoNuevo)

        if (frmDataProductoNuevo[0].checkValidity() === false) {
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: 'Faltan ingresar datos.',
                showConfirmButton: false,
                timer: 1500
            })
        } else {
            var vdesc = $('#descripcionModal_n').val();
            var vprec = $('#precioModal_n').val();
            var parametro = { descpro: vdesc, prep: vprec };
            $.post("/Producto/Grabar", parametro).done(function (results) {
                if (results = true) {
                    const Toast = Swal.mixin({
                        toast: true,
                        position: 'top-end',
                        showConfirmButton: false,
                        timer: 1500,
                        timerProgressBar: true,
                        didOpen: (toast) => {
                            toast.addEventListener('mouseenter', Swal.stopTimer)
                            toast.addEventListener('mouseleave', Swal.resumeTimer)
                        }
                    })
                    Toast.fire({
                        icon: 'success',
                        title: 'Se esta guardando el registro'
                    })
                    setTimeout(function () {
                        location.reload();
                    }, 1500);
                }
            })
        }
        frmDataProductoNuevo.addClass('was-validated');
    });
});

//Calcular sub-total, igv, total 
function calcularTotal() {
    sub = 0;
    igv = 0;
    tot = 0;
    var $objCuerpoTabla = $("#grilla").children().prev().parent();
    $objCuerpoTabla.find("tbody tr").each(function () {
        var cant = $(this).find('td').eq(3).html();
        var pre = $(this).find('td').eq(4).html();
        sub += (cant * pre) / 1.18;
        igv = sub * 0.18;
    });
    tot = sub + igv;
    $("#subtotal").val(sub.toFixed(2));
    $("#igv").val(igv.toFixed(2));
    $("#total").val(tot.toFixed(2));
}

//limpiar pedido
function limpiarPedido() {
    $("#idfechapedido").val(today);
    $("#idcliente").val('');
    $("#nomcliente").val('');
    $("#idproducto").val('');
    $("#desprod").val('');
    $("#precprod").val('');
    $("#cantidad").val('');
    $("#subtotal").val('');
    $("#igv").val('');
    $("#total").val('');
    $("#tbdetalle tr").remove();
}

//Validacion campos en blanco
(function () {
    'use strict';
    window.addEventListener('load', function () {
        var forms = document.getElementsByClassName('needs-validation');
        var validation = Array.prototype.filter.call(forms, function (form) {
            form.addEventListener('submit', function (event) {
                if (form.checkValidity() === false) {
                    event.preventDefault();
                    event.stopPropagation();
                }
                form.classList.add('was-validated');
            }, false);
        });
    }, false);
})();


