namespace encuesta_pro.DTOs.Company;

public class CompanyDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string CorporateEmail { get; set; }
}

public class CreateCompanyDto
{
    public string Name { get; set; }
    public string CorporateEmail { get; set; }
}

public class UpdateCompanyDto
{
    public string Name { get; set; }
    public string CorporateEmail { get; set; }
}
