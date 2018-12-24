using System;

using DJI_Pilot.ViewModels;

using Windows.UI.Xaml.Controls;

namespace DJI_Pilot.Views
{
    public sealed partial class HomePage : Page
    {
        public HomeViewModel ViewModel { get; } = new HomeViewModel();

        public HomePage()
        {
            InitializeComponent();
        }
    }
}
