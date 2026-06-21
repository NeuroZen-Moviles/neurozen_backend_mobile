namespace neurozen.API.Triggers.Interfaces.REST.Resources;

public record TriggerResource(Guid Id, long PatientId, long CategoryId, int StressLevel, DateTime TriggerDateTime, string Description);