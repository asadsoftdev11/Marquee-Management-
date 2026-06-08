using MarqueeManagement.Samples;
using Xunit;

namespace MarqueeManagement.EntityFrameworkCore.Applications;

[Collection(MarqueeManagementTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<MarqueeManagementEntityFrameworkCoreTestModule>
{

}
