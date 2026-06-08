using Volo.Abp.Settings;

namespace MarqueeManagement.Settings;

public class MarqueeManagementSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(MarqueeManagementSettings.MySetting1));
    }
}
