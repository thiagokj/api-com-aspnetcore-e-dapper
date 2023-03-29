using Microsoft.AspNetCore.Mvc;

namespace Store.Api.Controllers;
public class HomeController : ControllerBase
{
    [HttpGet]
    [Route("")]
    public object Get()
    {
        return new { app = "Store API", version = "0.0.1" };
    }

    [HttpGet]
    [Route("error")]
    public string Error()
    {
        throw new Exception("Ocorreu um erro na aplicação");

    }

}