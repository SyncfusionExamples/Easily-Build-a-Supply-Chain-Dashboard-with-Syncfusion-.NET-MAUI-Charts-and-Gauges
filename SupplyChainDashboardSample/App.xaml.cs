using Syncfusion.Licensing;

namespace SupplyChainDashboardSample;

public partial class App : Application
{
    public App()
    {
        SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1JFaF1cXGFCf0x3WmFZfVhgcV9CZ1ZRQ2YuP1ZhSXxWd0djW39bdHNURGRUVEZ9XEM=");
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
