using Syncfusion.Licensing;

namespace SupplyChainDashboardSample;

public partial class App : Application
{
    public App()
    {
        SyncfusionLicenseProvider.RegisterLicense("YOUR_LICENSE_KEY");
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var isDesktop = DeviceInfo.Idiom == DeviceIdiom.Desktop || DeviceInfo.Idiom == DeviceIdiom.TV;
        Page root = isDesktop
            ? new DesktopView()
            : new MobileView();

        return new Window(root);
    }
}
