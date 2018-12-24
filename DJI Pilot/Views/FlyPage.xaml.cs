using System;

using DJI_Pilot.ViewModels;

using Windows.UI.Xaml.Controls;

namespace DJI_Pilot.Views
{
    public sealed partial class FlyPage : Page
    {
        public FlyViewModel ViewModel { get; } = new FlyViewModel();

        public FlyPage()
        {
            InitializeComponent();
        }
    }
}
