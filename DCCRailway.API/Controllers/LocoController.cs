using DccRailway.API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DccRailway.API.Controllers;

[ApiController]
[Route("locos")]
public class LocoController : ControllerBase {
    private readonly ILogger<LocoController> _logger;

    public LocoController(ILogger<LocoController> logger) {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<Loco>? GetLocos() {
        return null;
    }

    [HttpGet("{id}")]
    public Loco GetLoco(Guid id) {
        return new Loco();
    }
}