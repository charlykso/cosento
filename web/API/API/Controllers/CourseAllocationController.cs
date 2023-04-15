using API.DTO;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseAllocationController : ControllerBase
    {
        private readonly ILecturerCourse? _ilecturerCourse;

        public CourseAllocationController(ILecturerCourse? ilecturerCourse)
        {
            _ilecturerCourse = ilecturerCourse;
        }

        [HttpPost("allocateCourse")]
        public async Task<ActionResult> allocateCourse([FromForm] Lecturer_Course_DTO l_course)
        {
            try
            {
                var myCourse = await _ilecturerCourse!.CreateLecturerCourse(l_course);
                if (myCourse.ToString() == "Course allocated")
                {
                    return Ok(myCourse);
                }
                return BadRequest("Not created");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}/delete")]
        public async Task<ActionResult> deleteLecturerCourse([FromRoute] string id)
        {
            try
            {
                var myCourse = await _ilecturerCourse!.DeleteLecturerCourse(id);
                if (myCourse.ToString() == "Deleted successfully")
                {
                    return Ok(myCourse);
                }
                return BadRequest(myCourse.ToString());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/get")]
        public async Task<ActionResult> getLecturerCourse([FromRoute] string id)
        {
            try
            {
                var myCourse = await _ilecturerCourse!.GetLecturerCourse(id);
                if (myCourse == null)
                {
                    return NoContent();
                }
                return Ok(myCourse);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getAll")]
        public async Task<ActionResult> getAll()
        {
            try
            {
                var myCourse = await _ilecturerCourse!.GetLecturerCourses();
                if (myCourse.ToString() == null)
                {
                    return NotFound();
                }
                return Ok(myCourse);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/update")]
        public async Task<ActionResult> updateAllocation([FromForm] Lecturer_Course_DTO l_course, [FromRoute] string id)
        {
            try
            {
                var myCourse = await _ilecturerCourse!.UpdateLecturerCourse(id, l_course);
                if (myCourse.ToString() == "Updated successfully")
                {
                    return Ok(myCourse);
                }
                return NotFound();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
