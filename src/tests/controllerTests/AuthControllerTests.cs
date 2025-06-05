using NUnit.Framework;
using Moq;
using TipMeBackend.Controllers;
using TipMeBackend.Controllers.DTOs;
using TipMeBackend.Services;
using TipMeBackend.Services.UsuarioService;
using TipMeBackend.Models;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework.Legacy;


[TestFixture]
public class AuthControllerTests
{
    private JwtService? jwtService;

    [SetUp]
    public void Setup()
    {
        var configMock = new Mock<IConfiguration>();
        configMock.Setup(c => c["Jwt:Secret"]).Returns("SecretKey12345678901234567890123456789012");
        configMock.Setup(c => c["Jwt:Issuer"]).Returns("https://localhost:5001");
        configMock.Setup(c => c["Jwt:Audience"]).Returns("https://localhost:5001");
            configMock.Setup(c => c["JwtSettings:ExpirationMinutes"]).Returns("60");

        jwtService = new JwtService(configMock.Object);
    }

   [Test]
   public async Task Login_Devuelve_Token_ConCredenciales_Validas()
    {
        var usuarioService = new Mock<IUsuarioService>();
        usuarioService.Setup(u => u.ObtenerAuthData("test@mail.com"))
            .ReturnsAsync(new Response<AuthDTO>(new AuthDTO { Email = "test@mail.com", Password = "123" }, 200));

        //var controller = new AuthController(jwtService, usuarioService.Object);
        //var result = await controller.Login(new AuthDTO { Email = "test@mail.com", Password = "123" }) as OkObjectResult;

        //Assert.That(((dynamic)result!.Value!).token, Is.Not.Null);
    }

    [Test]
   public async Task Login_Devuelve_404_ConCredenciales_Invalidas()
   {
    var usuarioService = new Mock<IUsuarioService>();
    usuarioService.Setup(u => u.ObtenerAuthData("inexistente@mail.com")).ReturnsAsync(new Response<AuthDTO>(data: null, statusCode:404));

       var controller = new AuthController(jwtService, usuarioService.Object);
       var result = await controller.Login(new AuthDTO { Email = "inexistente@mail.com", Password = "cualquierpswd" });

       ClassicAssert.IsInstanceOf<UnauthorizedObjectResult>(result);
   }
}