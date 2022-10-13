using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebApi.Controllers;

[Route( Route.Project )]
public class ProjectController : ApiController {

    /// <summary>
    /// Get all projects inside the system
    /// </summary>
    /// <returns>List of Projects</returns>
    [HttpGet]
    public IActionResult GetAll() {
        return View();
    }

    [HttpGet]
    [Route( "{id:guid}" )]
    public IActionResult GetById( Guid id ) {
        return View();
    }
}
