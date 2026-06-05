using MarqueeManagement.Samples;
using Xunit;

namespace MarqueeManagement.EntityFrameworkCore.Domains;

[Collection(MarqueeManagementTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<MarqueeManagementEntityFrameworkCoreTestModule>
{

}
