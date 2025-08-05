
using KhatiExtendedADO;

namespace KhatiExtendedADONugetImplementation.ADOContext
{
    public class PersonContext : AdoProperties , IPersonContext
    {
        private readonly IConfiguration _configuration;
        public PersonContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public override string ConnectionString()
        {
            return _configuration.GetConnectionString("DevConnection")??""; 
        }
    }
}
