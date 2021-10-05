using PersonalBlog.ViewModels.AdminViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PersonalBlog.BusinessManager.Interfaces
{
    public interface IAdminBusinessManager
    {
        Task<IndexViewModel> GetAdminDashboard(ClaimsPrincipal claimsPrincipal);
    }
}
