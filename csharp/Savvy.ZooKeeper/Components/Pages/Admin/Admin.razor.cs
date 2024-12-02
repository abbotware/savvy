using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Savvy.ZooKeeper.Models;
using Savvy.ZooKeeper.Models.Data;
using Savvy.ZooKeeper.Models.Entities;
using Savvy.ZooKeeper.Services;

namespace Savvy.ZooKeeper.Components.Pages.Admin
{
    public partial class Admin
    {
        [Parameter]
        public long UserId { get; set; }

        [Inject]
        private ModelContext ModelContext { get; set; } = null!;

        [Inject]
        private IUserSession UserSession { get; set; } = null!;

        protected override void OnInitialized()
        {
            UserId = UserSession.UserId;
        }

        [Inject]
        private IWebHostEnvironment webHostEnvironment { get; set; } = null!;

        public IReadOnlyList<Principal> Principals => ModelContext.Principals.ToList();

        public IReadOnlyList<Role> Roles => ModelContext.Roles.Include(x => x.Principals).Include(x => x.PrincipalRoles).ToList();

        public IReadOnlyList<Permission> Permissions => ModelContext.Permissions.Include(x => x.Roles).Include(x => x.RolePermissions).ToList();

        public IReadOnlyList<Employee> Employees => MaskEmployees();

        private Task OnChangeUserId()
        {
            UserSession.UserId = UserId;
            StateHasChanged();
            return Task.CompletedTask;
        }

        public IReadOnlyList<Employee> MaskEmployees()
        {
            if (UserSession.IsAdmin)
            {
                return ModelContext.Employees.ToList();
            }
            else
            {
                var masked = ModelContext.Employees.ToList();
                foreach (var r in masked)
                {
                    r.Name = r.Name.First() + "***";
                    r.LastName = r.LastName?.First() + "***";
                    r.Email = "***";
                    r.Phone = "***";
                }

                return masked;
            }
        }
    }
}