using API.DTO;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ICourse? _icourse;
        public CourseController(ICourse? icourse)
        {
            _icourse = icourse;
        }

        [HttpGet("getAll")]
        public async Task<ActionResult> getCourses()
        {
            try
            {
                var courses = await _icourse!.GetCourses();
                if (courses == null)
                {
                    return NoContent();
                }
                return Ok(courses);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/get")]
        public async Task<ActionResult> getCourse([FromRoute] string id)
        {
            try
            {
                var course = await _icourse!.GetCourse(id);
                if (course == null)
                {
                    return NotFound();
                }
                return Ok(course);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [HttpPost("create")]
        public async Task<ActionResult> createCourse([FromForm] Course_DTO newCourse)
        {
            const string A = "Course already exists";
            const string B = "Course created";
            const string C = "Something went wrong";
            try
            {
                if (ModelState.IsValid)
                {
                    var course = await _icourse!.CreateCourse(newCourse);
                    switch (course.ToString())
                    {
                        case (A or C):
                            return BadRequest(course);
                        case B:
                            return Ok(course);
                        default:
                            return BadRequest(course);
                    }
                }
                return BadRequest("Please fill the form correctly as required");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/update")]
        public async Task<ActionResult> updateCourse([FromForm] Course_DTO editCourse, [FromRoute] string id)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var course = await _icourse!.UpdateCourse(id, editCourse);
                    if (course.ToString() == "Course updated")
                    {
                        return Ok(course);
                    }
                    return BadRequest(course.ToString());
                }
                return BadRequest("Please fill all required feild");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}/delete")]
        public async Task<ActionResult> deleteCourse([FromRoute] string id)
        {
            try
            {
                var course = await _icourse!.DeleteCourse(id);
                if (course == null)
                {
                    return NotFound();
                }
                if (course.ToString() == "Deleted successfuly")
                {
                    return Ok(course);
                }
                return BadRequest(course.ToString());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
