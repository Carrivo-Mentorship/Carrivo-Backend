namespace Carrivo.Application.DTOs.Personality_Test;

public class TestResultResponse
{
    public string CategoryName { get; set; } = string.Empty;
    public List<CareerResultItem> CareerResults { get; set; } = new();
}

public class CareerResultItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
