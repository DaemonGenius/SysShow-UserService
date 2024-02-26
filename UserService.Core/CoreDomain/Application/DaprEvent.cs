namespace UserService.Core.CoreDomain.Application;

public class DaprEvent
{
    public Guid Identity { get; set; }
    public object State { get; set; }
    public object Metadata { get; set; }
}