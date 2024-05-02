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
            new Task { Id = 1, Name = "1. feladat", Description = "API-k tervezése", ProjectId = 1, UserId = 1, Deadline = DateTime.Now.AddDays(7) },
            new Task { Id = 2, Name = "2. feladat", Description = "ORM", ProjectId = 2, UserId = 2, Deadline = DateTime.Now.AddDays(14) },
            // További példaobjektumok...
        };

        public ProjectsController(ILogger<ProjectsController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetProjects")]
        public IEnumerable<Project> GetProjects()
        {
            // Ide jön a kód, amely visszaadja a projekteket
            // Például az adatbázisból lekérdezve vagy egy statikus listából
            return new List<Project>
            {
                new Project { Id = 1, Name = "Példaprojekt", TypeId = 1, Description = "Segítség a teszteléshez", DeveloperIds = new List<int> { 1, 2 } },
                new Project { Id = 2, Name = "Beadandó", TypeId = 2, Description = "Csoportmunka", DeveloperIds = new List<int> { 3, 4 } },
                // További projektek...
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
            // Az adott projektnek hozzárendelt feladatok visszaadása a példaobjektumokból
            var projectTasks = _tasks.FindAll(t => t.ProjectId == projectId);

            return projectTasks;
        }

        [HttpPost]
        public IActionResult AddTaskToProject([FromBody] Task task)
        {
            // Ellenõrizzük, hogy a feladathoz tartozik-e projekt
            if (task.ProjectId == 0)
            {
                return BadRequest("A feladatnak egy projektet kell hozzárendelnie.");
            }

            // Ellenõrizzük, hogy a feladatot létrehozó fejlesztõ létezik-e a rendszerben
            // Ezt az ellenõrzést itt egyszerûen példának szántam, a valóságban adatbáziskérés lenne
            if (task.UserId == 0)
            {
                return BadRequest("A feladatot létrehozó fejlesztõ nem található a rendszerben.");
            }

            // Példa: generálunk egyedi azonosítót
            task.Id = _tasks.Count + 1;

            // Hozzáadjuk az új feladatot a listához
            _tasks.Add(task);

            // Visszatérünk az új feladattal és státusszal 201 (Created)
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
