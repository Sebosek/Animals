using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Animals.ViewModels;

namespace Animals;

public partial class AnimalsPage : ContentPage
{
    public AnimalsPage(AnimalsViewModel vm)
    {
        BindingContext = vm;
        
        InitializeComponent();
    }

    public AnimalsViewModel ViewModel => (AnimalsViewModel)BindingContext;
}