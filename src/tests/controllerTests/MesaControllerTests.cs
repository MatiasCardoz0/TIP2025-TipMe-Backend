using NUnit.Framework;
using Moq;
using TipMeBackend.Controllers;
using TipMeBackend.Services;
using TipMeBackend.Services.MesaService;
using Microsoft.AspNetCore.Mvc;
using TipMeBackend.Models;
using TipMeBackend.Controllers.DTOs;
using NUnit.Framework.Legacy;

namespace TipMeBackend.Tests.ControllerTests
{
    [TestFixture]
    public class MesaControllerTests
    {
        [Test]
        public async Task getMesas_devuelve_ok_si_llamdado_es_exitoso()
        {
            var mesaService = new Mock<IMesaService>();
            mesaService.Setup(s => s.ObtenerMesas(1))
                .ReturnsAsync(new Response<List<MesaDTOGet>>(new List<MesaDTOGet>(), 200));
            var controller = new MesaController(mesaService.Object);

            var result = await controller.getMesas(1);

            ClassicAssert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task recibir_llamado_retorna_BadRequest_si_mesa_no_existe()
        {
            var mesaServiceMock = new Mock<IMesaService>();
            // Se simula que la llamada al service devuelve error 404
            mesaServiceMock.Setup(s => s.LlamarMozo(It.IsAny<int>()))
                .ReturnsAsync(new Response<(string, int)>("No se encontró la mesa", 404));
            var controller = new MesaController(mesaServiceMock.Object);
            var result = await controller.recibirLlamado(10000, 1);

            ClassicAssert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task GrabarMesa_Devuelve_BadRequest_si_nombre_vacio()
        {
            var mesaService = new Mock<IMesaService>();
            mesaService.Setup(s => s.GrabarMesa(It.IsAny<MesaDTO>()))
                .ReturnsAsync(new Response<string>("El nombre no puede ser vacio", 400));
            var controller = new MesaController(mesaService.Object);

            var result = await controller.grabarMesa(new MesaDTO { Nombre = "mesa teest", Numero = 1, MozoId = 1, QR = "qr", Estado = 1 });

            ClassicAssert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task pedirCuenta_devuelve_badRequest_si_hay_error()
        {
            var mesaService = new Mock<IMesaService>();
            mesaService.Setup(s => s.PedirCuenta(It.IsAny<int>()))
                .ReturnsAsync(new Response<(string, int)>(message: "No se encontró la mesa", statusCode: 404));
            var controller = new MesaController(mesaService.Object);

            var result = await controller.pedirCuenta(123, 1);

            ClassicAssert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task pedirCuenta_devuelve_ok_si_llamado_exitoso()
        {
            var mesaService = new Mock<IMesaService>();
            mesaService.Setup(s => s.PedirCuenta(It.IsAny<int>()))
                .ReturnsAsync(new Response<(string, int)>("Cuenta pedida con éxito", 200));
            var controller = new MesaController(mesaService.Object);

            var result = await controller.pedirCuenta(1, 1);

            ClassicAssert.IsInstanceOf<OkObjectResult>(result);
        }
    }
}