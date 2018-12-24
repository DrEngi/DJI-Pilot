using System;

using DJI_Pilot.ViewModels;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace DJI_Pilot.Views
{
    public sealed partial class ImagesPage : Page
    {
        public ImagesViewModel ViewModel { get; } = new ImagesViewModel();

        public ImagesPage()
        {
            InitializeComponent();
            ViewModel.Initialize(gridView);
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.NavigationMode == NavigationMode.Back)
            {
                await ViewModel.LoadAnimationAsync();
            }
        }
    }
}
