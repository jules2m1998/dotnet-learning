using Microsoft.Extensions.Options;

namespace NZWalks.API.Options;

public class JwtOptionsSetup : IConfigureOptions<JwtOptions>
{
    private readonly IConfiguration _configuration;

    public JwtOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(JwtOptions options)
    {
        // Use IOptions<JwtOptions> for injection as a singleton (IOptionSnapshot<JwtOptions>, IOptionMonitor<JwtOptions>)
        _configuration.GetSection("Jwt").Bind(options);
    }
}
