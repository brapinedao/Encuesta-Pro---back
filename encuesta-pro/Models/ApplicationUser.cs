namespace encuesta_pro.Models;
using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    public int CompanyId { get; set; }
    public Company Company { get; set; }
}