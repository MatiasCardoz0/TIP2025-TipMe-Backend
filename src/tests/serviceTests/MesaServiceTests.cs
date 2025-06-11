using NUnit.Framework;
using Moq;
using TipMeBackend.Services.MesaService;
using TipMeBackend.Data.MesaRepository;
using TipMeBackend.Controllers.DTOs;
using TipMeBackend.Models;

namespace TipMeBackend.Tests.ServiceTests
{

    [TestFixture]
    public class MesaServiceTests
    {
        [Test]
        public async Task Llamar_mozo_daError_cuando_no_existe_la_mesa()
        {
            var mesaRepoMock = new Mock<IMesaRepository>();
            mesaRepoMock.Setup(r => r.LlamarMozo(It.IsAny<int>()))
                .ReturnsAsync(new Response<(string, int)>("No se encontró la mesa", 404));
            var service = new MesaService(mesaRepoMock.Object);

            var result = await service.LlamarMozo(999);

            Assert.That(result.StatusCode, Is.EqualTo(404));
        }
        [Test]
        public async Task Grabar_mesa_daError_cuando_nombre_esta_vacio()
        {
            var mesaRepoMock = new Mock<IMesaRepository>();
            var service = new MesaService(mesaRepoMock.Object);
            var mesaDto = new MesaDTO { Nombre = "", Numero = 1, MozoId = 1, QR = "qr", Estado = 1 };

            var result = await service.GrabarMesa(mesaDto);

            Assert.That(result.StatusCode, Is.EqualTo(400));

        }
    }
}