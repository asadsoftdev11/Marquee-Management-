using Xunit;

namespace MarqueeManagement.EntityFrameworkCore;

[CollectionDefinition(MarqueeManagementTestConsts.CollectionDefinitionName)]
public class MarqueeManagementEntityFrameworkCoreCollection : ICollectionFixture<MarqueeManagementEntityFrameworkCoreFixture>
{

}
