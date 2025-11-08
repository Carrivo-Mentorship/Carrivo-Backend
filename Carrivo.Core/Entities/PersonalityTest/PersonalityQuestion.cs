using Carrivo.Core.Enums;

namespace Carrivo.Core.Entities;

public class PersonalityQuestion : BaseEntity
{
    public Guid PersonalityTestId { get; set; }
    public string Question { get; set; } = string.Empty;
    public QuestionType QuestionType { get; set; }
    public string? OptionsJson { get; set; }
    public int OrderIndex { get; set; }
    
    // Navigation Properties
    public PersonalityTest PersonalityTest { get; set; } = null!;
    public ICollection<StudentAnswer> StudentAnswers { get; set; } = new List<StudentAnswer>();
}
