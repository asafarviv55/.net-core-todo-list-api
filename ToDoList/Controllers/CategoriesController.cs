using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<TaskCategory>> GetAll()
        {
            return Ok(CategoryService.GetAllCategories());
        }

        [HttpGet("{id}")]
        public ActionResult<TaskCategory> GetById(int id)
        {
            var category = CategoryService.GetCategoryById(id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpPost]
        public ActionResult<TaskCategory> Create([FromBody] TaskCategory category)
        {
            var created = CategoryService.CreateCategory(category);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] TaskCategory category)
        {
            if (CategoryService.UpdateCategory(id, category))
                return NoContent();
            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (CategoryService.DeleteCategory(id))
                return NoContent();
            return NotFound();
        }

        [HttpGet("active")]
        public ActionResult<List<TaskCategory>> GetActive()
        {
            return Ok(CategoryService.GetActiveCategories());
        }
    }
}
