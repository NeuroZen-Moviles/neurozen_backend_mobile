using System.ComponentModel;

namespace neurozen.API.Subscriptions.Interfaces.REST.Resources;

public class CreateSubscriptionResource
{
    public int PlanId { get; set; }
    public string NameUser { get; set; } = string.Empty;
    public string LastNameUser { get; set; } = string.Empty;
    public string EmailUser { get; set; } = string.Empty;
    public string NumberCard { get; set; } = string.Empty;
    public string ExpirationDate { get; set; } = string.Empty;
    public string Cvv { get; set; } = string.Empty;

}
