using Microsoft.AspNetCore.Mvc;
using Redmine.Models;
using Task = Redmine.Models.Task;

namespace Redmine.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectsController : ControllerBase
    {
   
        private readonly ILogger<ProjectsController> _logger;

        private static List<Task> _tasks = new List<Task>
        {
            new Task { Id = 1, Name = "1. feladat", Description = "API-k tervez�se", ProjectId = 1, UserId = 1, Deadline = DateTime.Now.AddDays(7) },
            new Task { Id = 2, Name = "2. feladat", Description = "ORM", ProjectId = 2, UserId = 2, Deadline = DateTime.Now.AddDays(14) },
            // Tov�bbi p�ldaobjektumok...
        };

        public ProjectsController(ILogger<ProjectsController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetProjects")]
        public IEnumerable<Project> GetProjects()
        {
            // Ide j�n a k�d, amely visszaadja a projekteket
            // P�ld�ul az adatb�zisb�l lek�rdezve vagy egy statikus list�b�l
            return new List<Project>
            {
                new Project { Id = 1, Name = "P�ldaprojekt", TypeId = 1, Description = "Seg�ts�g a tesztel�shez", DeveloperIds = new List<int> { 1, 2 } },
                new Project { Id = 2, Name = "Beadand�", TypeId = 2, Description = "Csoportmunka", DeveloperIds = new List<int> { 3, 4 } },
                // Tov�bbi projektek...
            };
        }

        [HttpGet("{userId}", Name = "GetTasksByUserId")]
        public IEnumerable<Task> GetTasksByUserId(int userId)
        {
            var userTasks = _tasks.FindAll(t => t.UserId == userId);

            return userTasks;
        }

        [HttpGet("project/{projectId}", Name = "GetTasksByProjectId")]
        public IEnumerable<Task> GetTasksByProjectId(int projectId)
        {
            // Az adott projektnek hozz�rendelt feladatok visszaad�sa a p�ldaobjektumokb�l
            var projectTasks = _tasks.FindAll(t => t.ProjectId == projectId);

            return projectTasks;
        }

        [HttpPost]
        public IActionResult AddTaskToProject([FromBody] Task task)
        {
            // Ellen�rizz�k, hogy a feladathoz tartozik-e projekt
            if (task.ProjectId == 0)
            {
                return BadRequest("A feladatnak egy projektet kell hozz�rendelnie.");
            }

            // Ellen�rizz�k, hogy a feladatot l�trehoz� fejleszt� l�tezik-e a rendszerben
            // Ezt az ellen�rz�st itt egyszer�en p�ld�nak sz�ntam, a val�s�gban adatb�zisk�r�s lenne
            if (task.UserId == 0)
            {
                return BadRequest("A feladatot l�trehoz� fejleszt� nem tal�lhat� a rendszerben.");
            }

            // P�lda: gener�lunk egyedi azonos�t�t
            task.Id = _tasks.Count + 1;

            // Hozz�adjuk az �j feladatot a list�hoz
            _tasks.Add(task);

            // Visszat�r�nk az �j feladattal �s st�tusszal 201 (Created)
            return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
        }

        [HttpGet("{id}", Name = "GetTaskById")]
        public IActionResult GetTaskById(int id)
        {
            var task = _tasks.Find(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }
    }
}
