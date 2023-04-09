using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class CoursesController : ControllerBase
{
    private readonly List<Courses> _courses = new List<Courses>();
    private Courses _coursesList;

    public CoursesController()
    {
        _courses.Add(new Courses()
        {
            ID = 1,
            Name = "Example Course",
            Modules = new List<Module>()
            {
                new Module()
                {
                    ID = 1,
                    Name = "Example Module",
                    Assignments = new List<Assignment>()
                    {
                        new Assignment()
                        {
                            ID = 1,
                            Name = "Example Assignment",
                            Grade = 90,
                            DueDate = new DateTime(2023, 4, 1)
                        }
                    }
                }
            }
        });
    }
    [HttpGet]
    public ActionResult<IEnumerable<Courses>> Get()
    {
        return _courses;
    }
    
    [HttpPost]
    public ActionResult<Courses> Post([FromBody] Courses courses)
    {
        if (courses == null)
        {
            return BadRequest();
        }
        _courses.Add(courses);
        return CreatedAtAction(nameof(Get), new { id = courses.ID }, courses);
    }
    [HttpGet("{id}")]
    public ActionResult<Courses> Get(int id)
    {
        var course = _courses.FirstOrDefault(c => c.ID == id);
        if (course == null)
        {
            return NotFound();
        }
        return course;
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Courses updatedCourses)
    {
        if (updatedCourses == null || updatedCourses.ID != id)
        {
            return BadRequest();
        }
        var course = _courses.FirstOrDefault(c => c.ID == id);
        if (course == null)
        {
            return NotFound();
        }
        course.Name = updatedCourses.Name;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var course = _courses.FirstOrDefault(c => c.ID == id);
        if (course == null)
        {
            return NotFound();
        }
        _courses.Remove(course);
        return NoContent();
    }

    [HttpGet("{id}/modules")]
    public ActionResult<IEnumerable<Module>> GetModulesForCourse(int id)
    {
        var course = _courses.FirstOrDefault(c => c.ID == id);
        if (course == null)
        {
            return NotFound();
        }
        return course.Modules.ToList();
    }

    [HttpPost("{id}/modules")]
    public ActionResult<Module> AddModuleToCourse(int id, [FromBody] Module module)
    {
        var course = _courses.FirstOrDefault(c => c.ID == id);
        if (course == null)
        {
            return NotFound();
        }
        course.Modules.Add(module);
        return CreatedAtAction(nameof(GetModulesForCourse), new { id = course.ID }, module);
    }
    
    /*
    [HttpGet("{courseId}")]
    public ActionResult<Course> Get(int id)
    {
        var course = _courses.FirstOrDefault(c => c.ID == id);
        if (course == null)
        {
            return NotFound();
        }
        return course;
    }
    
    [HttpPut("{courseId}")]
    public IActionResult Put(int id, [FromBody] Course updatedCourse)
    {
        if (updatedCourse == null || updatedCourse.ID != id)
        {
            return BadRequest();
        }
        var course = _courses.FirstOrDefault(c => c.ID == id);
        if (course == null)
        {
            return NotFound();
        }
        course.Name = updatedCourse.Name;
        return NoContent();
    }

    
    [HttpDelete("{courseId}")]
    public IActionResult Delete(int id)
    {
        var course = _courses.FirstOrDefault(c => c.ID == id);
        if (course == null)
        {
            return NotFound();
        }
        _courses.Remove(course);
        return NoContent();
    }
    
    [HttpGet("{courseId}/modules")]
    public ActionResult<IEnumerable<Module>> GetModulesForCourse(int id)
    {
        var course = _courses.FirstOrDefault(c => c.ID == id);
        if (course == null)
        {
            return NotFound();
        }
        return course.Modules.ToList();
    }
     
     
    [HttpPost("{courseId}/modules")]
    public ActionResult<Module> AddModuleToCourse(int id, [FromBody] Module module)
    {
        var course = _courses.FirstOrDefault(c => c.ID == id);
        if (course == null)
        {
            return NotFound();
        }
        course.Modules.Add(module);
        return CreatedAtAction(nameof(GetModulesForCourse), new { id = course.ID }, module);
    }
     */
     [HttpGet("{courseId}/modules/{moduleId}")]
     public ActionResult<Module> GetModuleForCourse(int courseId, int moduleId)
     {
         var course = _courses.FirstOrDefault(c => c.ID == courseId);
         if (course == null)
         {
             return NotFound();
         }
         var module = course.Modules.FirstOrDefault(m => m.ID == moduleId);
         if (module == null)
         {
             return NotFound();
         }
         return module;
     }
     
     [HttpPut("{courseId}/modules/{moduleId}")]
     public IActionResult UpdateModuleForCourse(int courseId, int moduleId, [FromBody] Module updatedModule)
     {
         if (updatedModule == null || updatedModule.ID != moduleId)
         {
             return BadRequest();
         }
         var course = _courses.FirstOrDefault(c => c.ID == courseId);
         if (course == null)
         {
             return NotFound();
         }
         var module = course.Modules.FirstOrDefault(m => m.ID == moduleId);
         if (module == null)
         {
             return NotFound();
         }
         module.Name = updatedModule.Name;
         return NoContent();
     }
     
     [HttpDelete("{courseId}/modules/{moduleId}")]
     public IActionResult DeleteModuleForCourse(int courseId, int moduleId)
     {
         var course = _courses.FirstOrDefault(c => c.ID == courseId);
         if (course == null)
         {
             return NotFound();
         }
         var module = course.Modules.FirstOrDefault(m => m.ID == moduleId);
         if (module == null)
         {
             return NotFound();
         }
         course.Modules.Remove(module);
         return NoContent();
     }
     
     [HttpGet("{courseId}/modules/{moduleId}/assignments")]
     public ActionResult<IEnumerable<Assignment>> GetAssignmentsForModule(int courseId, int moduleId)
     {
         var course = _courses.FirstOrDefault(c => c.ID == courseId);
         if (course == null)
         {
             return NotFound();
         }

         var module = course.Modules.FirstOrDefault(m => m.ID == moduleId);
         if (module == null)
         {
             return NotFound();
         }

         return module.Assignments;
     }
     
     [HttpPost("{courseId}/modules/{moduleId}/assignments")]
     public ActionResult<Assignment> AddAssignmentToModule(int courseId, int moduleId, [FromBody] Assignment assignment)
     {
         if (assignment == null)
         {
             return BadRequest();
         }

         var course = _courses.FirstOrDefault(c => c.ID == courseId);
         if (course == null)
         {
             return NotFound();
         }

         var module = course.Modules.FirstOrDefault(m => m.ID == moduleId);
         if (module == null)
         {
             return NotFound();
         }

         assignment.ID = module.Assignments.Count + 1;
         module.Assignments.Add(assignment);

         return CreatedAtAction(nameof(GetAssignmentsForModule), new { courseId = courseId, moduleId = moduleId }, assignment);
     }
     
     private Module GetModuleById(int courseId, int moduleId)
     {
         var course = _courses.FirstOrDefault(c => c.ID == courseId);
         if (course == null)
         {
             return null;
         }

         var module = course.Modules.FirstOrDefault(m => m.ID == moduleId);
         return module;
     }
     
     [HttpGet("{courseId}/modules/{moduleId}/assignments/{assignmentId}")]
     public ActionResult<Assignment> GetAssignment(int courseId, int moduleId, int assignmentId)
     {
         var module = GetModuleById(courseId, moduleId);
         if (module == null)
         {
             return NotFound();
         }

         var assignment = module.Assignments.FirstOrDefault(a => a.ID == assignmentId);
         if (assignment == null)
         {
             return NotFound();
         }

         return assignment;
     }
     
     [HttpPut("{courseId}/modules/{moduleId}/assignments/{assignmentId}")]
     public IActionResult PutAssignment(int courseId, int moduleId, int assignmentId, [FromBody] Assignment updatedAssignment)
     {
         var module = GetModuleById(courseId, moduleId);
         if (module == null)
         {
             return NotFound();
         }

         var assignment = module.Assignments.FirstOrDefault(a => a.ID == assignmentId);
         if (assignment == null)
         {
             return NotFound();
         }

         if (updatedAssignment == null || updatedAssignment.ID != assignmentId)
         {
             return BadRequest();
         }

         assignment.Name = updatedAssignment.Name;
         assignment.Grade = updatedAssignment.Grade;
         assignment.DueDate = updatedAssignment.DueDate;

         return NoContent();
     }
     
     [HttpDelete("{courseId}/modules/{moduleId}/assignments/{assignmentId}")]
     public IActionResult DeleteAssignment(int courseId, int moduleId, int assignmentId)
     {
         var module = GetModuleById(courseId, moduleId);
         if (module == null)
         {
             return NotFound();
         }

         var assignment = module.Assignments.FirstOrDefault(a => a.ID == assignmentId);
         if (assignment == null)
         {
             return NotFound();
         }

         module.Assignments.Remove(assignment);

         return NoContent();
     }
     
}




