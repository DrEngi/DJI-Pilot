using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using DJI_Pilot.Helpers;
using DJI_Pilot.Models;
using DJI_Pilot.Services;

using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace DJI_Pilot.ViewModels
{
    public class ImagesDetailViewModel : Observable
    {
        private static UIElement _image;
        private object _selectedImage;
        private ObservableCollection<SampleImage> _source;

        public object SelectedImage
        {
            get => _selectedImage;
            set
            {
                Set(ref _selectedImage, value);
                ImagesNavigationHelper.UpdateImageId(ImagesViewModel.ImagesSelectedIdKey, ((SampleImage)SelectedImage).ID);
            }
        }

        public ObservableCollection<SampleImage> Source
        {
            get => _source;
            set => Set(ref _source, value);
        }

        public ImagesDetailViewModel()
        {
            // TODO WTS: Replace this with your actual data
            Source = SampleDataService.GetGallerySampleData();
        }

        public void SetImage(UIElement image) => _image = image;

        public void Initialize(string sampleImageId, NavigationMode navigationMode)
        {
            if (!string.IsNullOrEmpty(sampleImageId) && navigationMode == NavigationMode.New)
            {
                SelectedImage = Source.FirstOrDefault(i => i.ID == sampleImageId);
            }
            else
            {
                var selectedImageId = ImagesNavigationHelper.GetImageId(ImagesViewModel.ImagesSelectedIdKey);
                if (!string.IsNullOrEmpty(selectedImageId))
                {
                    SelectedImage = Source.FirstOrDefault(i => i.ID == selectedImageId);
                }
            }

            var animation = ConnectedAnimationService.GetForCurrentView().GetAnimation(ImagesViewModel.ImagesAnimationOpen);
            animation?.TryStart(_image);
        }

        public void SetAnimation()
        {
            ConnectedAnimationService.GetForCurrentView()?.PrepareToAnimate(ImagesViewModel.ImagesAnimationClose, _image);
        }
    }
}
