using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using DJI_Pilot.Helpers;
using DJI_Pilot.Models;
using DJI_Pilot.Services;
using DJI_Pilot.Views;

using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace DJI_Pilot.ViewModels
{
    public class ImagesViewModel : Observable
    {
        public const string ImagesSelectedIdKey = "ImagesSelectedIdKey";
        public const string ImagesAnimationOpen = "Images_AnimationOpen";
        public const string ImagesAnimationClose = "Images_AnimationClose";

        private ObservableCollection<SampleImage> _source;
        private ICommand _itemSelectedCommand;
        private GridView _imagesGridView;

        public ObservableCollection<SampleImage> Source
        {
            get => _source;
            set => Set(ref _source, value);
        }

        public ICommand ItemSelectedCommand => _itemSelectedCommand ?? (_itemSelectedCommand = new RelayCommand<ItemClickEventArgs>(OnsItemSelected));

        public ImagesViewModel()
        {
            // TODO WTS: Replace this with your actual data
            Source = SampleDataService.GetGallerySampleData();
        }

        public void Initialize(GridView imagesGridView)
        {
            _imagesGridView = imagesGridView;
        }

        public async Task LoadAnimationAsync()
        {
            var selectedImageId = ImagesNavigationHelper.GetImageId(ImagesSelectedIdKey);
            if (!string.IsNullOrEmpty(selectedImageId))
            {
                var animation = ConnectedAnimationService.GetForCurrentView().GetAnimation(ImagesAnimationClose);
                if (animation != null)
                {
                    var item = _imagesGridView.Items.FirstOrDefault(i => ((SampleImage)i).ID == selectedImageId);
                    _imagesGridView.ScrollIntoView(item);
                    await _imagesGridView.TryStartConnectedAnimationAsync(animation, item, "galleryImage");
                }

                ImagesNavigationHelper.RemoveImageId(ImagesSelectedIdKey);
            }
        }

        private void OnsItemSelected(ItemClickEventArgs args)
        {
            var selected = args.ClickedItem as SampleImage;
            _imagesGridView.PrepareConnectedAnimation(ImagesAnimationOpen, selected, "galleryImage");
            ImagesNavigationHelper.AddImageId(ImagesSelectedIdKey, selected.ID);
            NavigationService.Navigate<ImagesDetailPage>(selected.ID);
        }
    }
}
