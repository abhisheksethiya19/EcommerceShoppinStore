using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceShoppingStore.WebAPI.Authentication
{
    public class ApplicationRole : IdentityRole
    {
        public int Id { get; set; }
        public string RoleName { get; set; }

        
    }
}
