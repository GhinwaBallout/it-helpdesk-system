namespace IdsProject.Models
{
    public class Role
    {
        public int id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<User> Users { get; set; } = new List<User>();
    }

    public static class RoleNames
    {
        public const string Admin = "Admin";
        public const string Manager = "Manager";
        public const string ITAgent = "ITAgent";
        public const string Employee = "Employee";
        public static readonly string[] All = { Admin, Manager, ITAgent, Employee };
    }
}