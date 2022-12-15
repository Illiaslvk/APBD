using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace students.Controllers
{
    [ApiController]
    public class StudentController : Controller
    {
        private readonly StudentService service = new();

        [HttpGet("/students")] 
        public ActionResult GetAll()
        {
            return Ok(service.GetAll());
        }

        [HttpGet("/students/{index}")]
        public ActionResult GetByIndex([FromRoute] string index)
        {
            Student student = service.GetByIndex(index);
            return student == null ? NotFound() : Ok(student);
        }

        [HttpPost("/students")]
        public ActionResult Create([FromBody] object postBody)
        {
            return service.Add(postBody) == null ? Ok() : BadRequest("Invalid arguments");
        }

        [HttpPut("/students")]
        public IActionResult Update([FromBody] object postBody)
        {
            Student student = service.Modify(postBody);
            return student != null ? Ok(student) : BadRequest("No student with this index or data set is incomplete");
        }

        
        [HttpDelete("students/{index}")]
        public IActionResult Delete([FromRoute] string index)
        {
            return service.Delete(index) == -1 ? BadRequest() : Ok();
        }
    }
}
