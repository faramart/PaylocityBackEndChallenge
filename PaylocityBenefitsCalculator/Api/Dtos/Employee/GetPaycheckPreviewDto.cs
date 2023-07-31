namespace Api.Dtos.Employee;

public class GetPaycheckPreviewDto
{
    public int EmployeeId { get; set; }

    public decimal BenefitCostsPerPaycheck { get; set; }
}