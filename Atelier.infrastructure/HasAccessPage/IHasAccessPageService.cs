using Atelier.Application.Interfaces.Contexts;
using Atelier.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.infrastructure.HasAccessPage
{
    public interface IHasAccessPageService
    {
        Task<bool> Execute(Guid userId,string pageName);
    }
    public class HasAccessPageService : IHasAccessPageService
    {
        private readonly IDatabaseContext _context;
        private readonly UserManager<User> _userManager;

        public HasAccessPageService(IDatabaseContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<bool> Execute(Guid userId, string pageName)
        {
            try
            {
                var user = await _context.Users.FindAsync(userId.ToString());
                if (user == null)
                {
                    return false;
                }
                var roles = await _userManager.GetRolesAsync(user);

                var pageId = _context.Pages.Where(t => t.Name ==pageName).FirstOrDefaultAsync().Result?.Id;
                // چک کردن برای هر نقش کاربر
                foreach (var role in roles)
                {
                    var roleId = _context.Roles.Where(y=>y.Name==role).FirstOrDefaultAsync().Result?.Id;
                    // یافتن مجوز ورود برای نقش و صفحه مورد نظر
                    //var permission = await _context.RolePagePermissions.Include(p=>p.Page)
                    //    .FirstOrDefaultAsync(rp => rp.RoleId == roleId && rp.PageId == pageId);
                    
                    // اگر مجوز یافت شد و مجوز ورود دارد، true را برگردانید
                    //if (permission != null && permission.CanAccess)
                    //{
                    //    return true;
                    //}
                }
                // اگر هیچ مجوز ورودی پیدا نشد یا مجوز ورود ندارد، false را برگردانید
                return false;
            }
            catch (Exception ex)
            {
                // در صورت بروز خطا، اطلاعاتی را لاگ کنید و false را برگردانید
                Console.WriteLine($"An error occurred while checking page access: {ex.Message}");
                return false;
            }
        }
    }
}
