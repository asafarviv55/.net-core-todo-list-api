using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        [HttpGet("summary")]
        public ActionResult<TaskSummaryReport> GetSummary()
        {
            return Ok(ReportService.GenerateSummaryReport());
        }

        [HttpGet("category")]
        public ActionResult<List<CategoryReport>> GetCategoryReport()
        {
            return Ok(ReportService.GenerateCategoryReport());
        }

        [HttpGet("user-productivity")]
        public ActionResult<List<UserProductivityReport>> GetUserProductivity()
        {
            return Ok(ReportService.GenerateUserProductivityReport());
        }

        [HttpGet("priority-distribution")]
        public ActionResult<List<PriorityDistribution>> GetPriorityDistribution()
        {
            return Ok(ReportService.GeneratePriorityDistribution());
        }

        [HttpGet("status-distribution")]
        public ActionResult<Dictionary<string, int>> GetStatusDistribution()
        {
            return Ok(ReportService.GetTaskStatusDistribution());
        }

        [HttpGet("creation-trend")]
        public ActionResult<Dictionary<DateTime, int>> GetCreationTrend([FromQuery] int days = 30)
        {
            return Ok(ReportService.GetTaskCreationTrend(days));
        }
    }
}
