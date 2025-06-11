using NUnit.Framework;
using NUnit.Framework.Legacy;
using Moq;
using TipMeBackend.Controllers;
using TipMeBackend.Services.PropinaService;
using Microsoft.AspNetCore.Mvc;
using TipMeBackend.Models;
using TipMeBackend.Controllers.DTOs;
using TipMeBackend.Data.PropinaRepository;

namespace TipMeBackend.Tests.ControllerTests
{
    [TestFixture]
    public class PropinaControllerTests
    {
        [Test]
        public async Task Grabar_propina_devuelve_Ok_si_es_llamado_exitoso()
        {
            var propinaService = new Mock<IPropinaService>();
            propinaService.Setup(s => s.GrabarPropina(It.IsAny<PropinaDTO>()))
                .ReturnsAsync(new Response<string>("ok", 200));
            var controller = new PropinaController(propinaService.Object);

            var result = await controller.grabarPropina(new PropinaDTO());

            ClassicAssert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task Obtener_propinas_devuelve_Ok_si_es_llamado_exitoso()
        {
            var propinaService = new Mock<IPropinaService>();
            propinaService.Setup(s => s.ObtenerPropinas(1))
                .ReturnsAsync(new Response<System.Collections.Generic.List<PropinaDTOGet>>(new System.Collections.Generic.List<PropinaDTOGet>(), 200));
            var controller = new PropinaController(propinaService.Object);

            var result = await controller.getPropinas(1);

            ClassicAssert.IsInstanceOf<OkObjectResult>(result);
        }
    }
}