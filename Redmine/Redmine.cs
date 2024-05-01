using System;
using System.Collections.Generic;
using System.Linq;

namespace Redmine
{
    // Modelosztályok
    public class Manager
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class Developer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }
        public string Description { get; set; }
        public List<int> DeveloperIds { get; set; } // A projekthez hozzárendelt fejlesztők azonosítói
    }

    public class ProjectType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }


    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description
        { get; set; }
        public int ProjectId { get; set; } // Projekt azonosítója, amelyhez a feladat tartozik
        public int UserId { get; set; } // A feladatot létrehozó menedzser azonosítója
        public DateTime Deadline { get; set; }
    }

    // Szerveroldali alkalmazás
    public class RedmineService
    {
        private List<Project> projects;
        private List<Developer> developers;
        private List<ProjectType> projectTypes;
        private List<Task> tasks;
        private List<Manager> managers;

        public RedmineService()
        {
            // Alapadatok létrehozása
            projects = new List<Project>
        {
            new Project
            {
                Id = 1,
                Name = "Példaprojekt",
                TypeId = 1,
                Description = "Segítség a teszteléshez",
                DeveloperIds = new List<int> { 1 }
            },
            new Project
            {
                Id = 2,
                Name = "Beadandó",
                TypeId = 2,
                Description = "Csoportmunka",
                DeveloperIds = new List<int> { 2 }
            }
        };

            developers = new List<Developer>
        {
            new Developer { Id = 1, Name = "István", Email = "kvcsstvn@example.com" },
            new Developer { Id = 2, Name = "Karola", Email = "ngykrl@example.com" }
        };

            projectTypes = new List<ProjectType>
        {
            new ProjectType { Id = 1, Name = "A Típus" },
            new ProjectType { Id = 2, Name = "B Típus" }
        };

            tasks = new List<Task>
        {
            new Task { Id = 1, Name = "1. feladat", Description = "Részfeladat", ProjectId = 1, UserId = 1, Deadline = DateTime.Now.AddDays(7) },
            new Task { Id = 2, Name = "2. feladat", Description = "Mérföldkő", ProjectId = 2, UserId = 1, Deadline = DateTime.Now.AddDays(10) }
        };

            managers = new List<Manager>
        {
            new Manager { Id = 1, Name = "Mónika", Email = "tthmnk@example.com", Password = "password123" }
        };
        }

        // API-végpontok
        public List<Project> GetProjects(string typeFilter = null)
        {
            if (string.IsNullOrEmpty(typeFilter))
                return projects;
            else
                return projects.Where(p => p.TypeId == projectTypes.FirstOrDefault(pt => pt.Name == typeFilter)?.Id).ToList();
        }

        public Project GetProject(int projectId)
        {
            return projects.FirstOrDefault(p => p.Id == projectId);
        }

        public void AddTask(int projectId, Task task)
        {
            var project = projects.FirstOrDefault(p => p.Id == projectId);
            if (project != null)
            {
                task.Id = tasks.Count + 1;
                tasks.Add(task);
            }
        }

        public List<Task> GetTasks()
        {
            return tasks;
        }

        public List<ProjectType> ProjectTypes => projectTypes;
    }


    class Redmine
    {
        static void Main(string[] args)
        {
            // Szerver inicializálása
            var service = new RedmineService();

            // Tesztkiíratás
            Console.WriteLine("Elérhető feladatok:");
            var projects = service.GetProjects();
            foreach (var project in projects)
            {
                Console.WriteLine($"Projektazonosító: {project.Id}, Név: {project.Name}, Típus: {service.ProjectTypes.FirstOrDefault(pt => pt.Id == project.TypeId)?.Name}");
            }

            Console.WriteLine("\nFeladatok:");
            var tasks = service.GetTasks();
            foreach (var task in tasks)
            {
                Console.WriteLine($"Feladatazonosító: {task.Id}, Név: {task.Name}, Leírás: {task.Description}, Projektazonosító: {task.ProjectId}, Felhasználóazonosító: {task.UserId}");
            }
        }
    }
}
