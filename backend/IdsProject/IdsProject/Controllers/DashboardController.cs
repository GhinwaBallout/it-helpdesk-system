using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IdsProject.Models;
namespace IdsProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        [HttpGet("admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminDashboard()
        {
            return Ok($"Welcome Admin {RoleNames.Admin}. You can manage users, roles, and system settings.");
        }

        [HttpGet("manager")]
        [Authorize(Roles = "Manager")]
        public IActionResult ManagerDashboard()
        {
            return Ok($"Welcome Manager {RoleNames.Manager}. You can view tickets, assign work, and monitor IT agents.");
        }

        [HttpGet("it-agent")]
        [Authorize(Roles = "ITAgent")]
        public IActionResult ITAgentDashboard()
        {
            return Ok($"Welcome IT Agent {RoleNames.ITAgent}. You can view and resolve assigned tickets.");
        }

        [HttpGet("employee")]
        [Authorize(Roles = "Employee")]
        public IActionResult EmployeeDashboard()
        {
            return Ok($"Welcome Employee {RoleNames.Employee} . You can create and track your support requests.");
        }
    }
}