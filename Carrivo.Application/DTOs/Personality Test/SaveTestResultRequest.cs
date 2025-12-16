using Carrivo.Core.Enums;

namespace Carrivo.Application.DTOs.Personality_Test;

public class SaveTestResultRequest
{
    public Guid UserId { get; set; }
    public CareerCategory CareerCategory { get; set; }
}
