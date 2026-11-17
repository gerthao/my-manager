using MyManager.Routing.Attributes;
using RazorSlices;

namespace MyManager.Modules.Home;

public static class HomeEndpoint
{
    [MapGet("/home")]
    public static RazorSlice<DateTime> HandleEndpoint()
    {
        return Slices.Hello.Create(DateTime.Now);
    }
}
