using NUnit.Framework;
using Moq;
using TipMeBackend.Services.PropinaService;
using TipMeBackend.Data.PropinaRepository;
using TipMeBackend.Controllers.DTOs;
using TipMeBackend.Models;

namespace TipMeBackend.Tests.ServiceTests
{
    [TestFixture]
    public class PropinaServiceTests
    {
        [Test]
        public async Task Grabar_propina_llama_repositorio_y_devuelve_200()
        {
            var repoMock = new Mock<IPropinaRepository>();
            repoMock.Setup(r => r.GrabarPropina(It.IsAny<Propina>())).ReturnsAsync(new Response<string>("ok", 200));
            var service = new PropinaService(repoMock.Object);

            var result = await service.GrabarPropina(new PropinaDTO());

            Assert.That(result.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task Obtener_Propinas_llama_repositorio_y_devuelve_lista()
        {
            var repoMock = new Mock<IPropinaRepository>();
            repoMock.Setup(r => r.ObtenerPropinas(1))
                .ReturnsAsync(new Response<System.Collections.Generic.List<PropinaDTOGet>>(new System.Collections.Generic.List<PropinaDTOGet>(), 200));
            var service = new PropinaService(repoMock.Object);

            var result = await service.ObtenerPropinas(1);

            Assert.That(result.StatusCode, Is.EqualTo(200));
        }
    }
}