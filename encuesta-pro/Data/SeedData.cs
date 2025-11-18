using encuesta_pro.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace encuesta_pro.Data;

public static class SeedData
{
    public static async Task SeedAdmin(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
    {
        string email = "admin@survey.com";
        string password = "Admin123!";

        var existing = await userManager.FindByEmailAsync(email);
        if (existing != null) return;

        // Crear Company si no existe
        var company = await context.Companies.FirstOrDefaultAsync();
        if (company == null)
        {
            company = new Company
            {
                Name = "Default Company",
                CorporateEmail = "default@company.com"
            };
            context.Companies.Add(company);
            await context.SaveChangesAsync();
        }

        var user = new ApplicationUser
        {
            Email = email,
            UserName = email,
            CompanyId = company.Id   // ✔ válido
        };

        await userManager.CreateAsync(user, password);
    }
}