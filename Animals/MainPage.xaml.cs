using Animals.ViewModels;

namespace Animals;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageViewModel vm)
    {
        BindingContext = vm;
        
        InitializeComponent();
    }
}