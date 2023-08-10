namespace ServiceLifetimeDemonstration;

public class GuidService : IGuidService
{
	private readonly Guid _serviceGuid = Guid.NewGuid();

    public string GetGuid() => _serviceGuid.ToString();
}
