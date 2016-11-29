using FinancialReports.Data;
using Xunit;

namespace FinancialReportsTests
{
    public class FirstTests
    {
        [Fact]
        public void DatabasePathSet()
        {
            BangazonConnection connection = new BangazonConnection();
            Assert.True(connection.path.Contains(".db"));
        }
    }
}
