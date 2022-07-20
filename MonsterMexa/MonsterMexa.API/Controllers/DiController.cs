using Microsoft.AspNetCore.Mvc;

namespace MonsterMexa.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DiController : ControllerBase
    {
        public DiController(ServiceA serviceA, ServiceB serviceB, ILogger<DiController> logger)
        {
            ServiceA = serviceA;
            ServiceB = serviceB;
            logger.LogInformation(id);
        }

        public ServiceA ServiceA { get; }
        public ServiceB ServiceB { get; }

        private readonly string id = Guid.NewGuid().ToString();

        [HttpGet]
        public IActionResult Test()
        {
            return Ok();
        }
    }

    public class ServiceA
    {
        private readonly string id = Guid.NewGuid().ToString();

        public ServiceA(RepositoryA repositoryA, RepositoryB repositoryB, ILogger<ServiceA> logger)
        {
            RepositoryA = repositoryA;
            RepositoryB = repositoryB;
            logger.LogInformation(id);
        }

        public RepositoryA RepositoryA { get; }
        public RepositoryB RepositoryB { get; }
    }
    public class ServiceB
    {
        public ServiceB(RepositoryA repositoryA, RepositoryB repositoryB, ILogger<ServiceB> logger)
        {
            RepositoryA = repositoryA;
            RepositoryB = repositoryB;
            logger.LogInformation(id);
        }
        private readonly string id = Guid.NewGuid().ToString();

        public RepositoryA RepositoryA { get; }
        public RepositoryB RepositoryB { get; }
    }

    public class RepositoryA
    {
        private readonly string id = Guid.NewGuid().ToString();

        public RepositoryA(ILogger<RepositoryA> logger)
        {
            logger.LogInformation(id);
        }
    }

    public class RepositoryB
    {
        private readonly string id = Guid.NewGuid().ToString();

        public RepositoryB(ILogger<RepositoryB> logger)
        {
            logger.LogInformation(id);

        }
    }
}
