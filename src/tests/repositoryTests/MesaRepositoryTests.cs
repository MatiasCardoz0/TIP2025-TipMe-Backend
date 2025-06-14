// test for mesa repository
using NUnit.Framework;
using NUnit.Framework.Legacy;
using Moq;
using TipMeBackend.Data.MesaRepository;
using TipMeBackend.Models;
using TipMeBackend.Controllers.DTOs;
using TipMeBackend.Services.MesaService;

namespace TipMeBackend.Tests.RepositoryTests
{
    [TestFixture]
    public class MesaRepositoryTests
    {
        private Mock<IMesaRepository>? _mesaRepositoryMock;
        private IMesaService? _mesaService;

        [SetUp]
        public void Setup()
        {
            _mesaRepositoryMock = new Mock<IMesaRepository>();
            _mesaService = new MesaService(_mesaRepositoryMock.Object);
        }

        [Test]
        public async Task GrabarMesa_devuelve_Ok_cuando_llamado_exitoso()
        {
            var mesaDTO = new MesaDTO { Numero = 1, Nombre = "Mesa 1", MozoId = 1, QR = "qr", Estado = 1};
            _mesaRepositoryMock!.Setup(repo => repo.GrabarMesa(It.IsAny<Mesa>()))
                .ReturnsAsync(new Response<string>("ok", 200));

            var result = await _mesaService!.GrabarMesa(mesaDTO);

            Assert.That(result.StatusCode, Is.EqualTo(200));
            Assert.That(result.Message, Is.EqualTo("ok"));
        }

        [Test]
        public async Task ObtenerMesas_devuelve_lista_cuando_llamado_exitoso()
        {
            var mesas = new List<MesaDTOGet>
            {
                new MesaDTOGet("Mesa 1", 1, 1, "www.test.com/1", 1, "Disponible"),
                new MesaDTOGet("Mesa 2", 2, 1, "www.test.com/2", 1, "Disponible")
            };
            _mesaRepositoryMock!.Setup(repo => repo.ObtenerMesas(1))
                .ReturnsAsync(new Response<List<MesaDTOGet>>(mesas, 200));

            var result = await _mesaService!.ObtenerMesas(1);

            Assert.That(result.Data, Is.EqualTo(mesas));
            Assert.That(result.StatusCode, Is.EqualTo(200));
        }
    }
}