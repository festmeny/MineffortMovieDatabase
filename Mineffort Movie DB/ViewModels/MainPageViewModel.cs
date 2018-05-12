using Mineffort_Movie_DB.Models;
using Mineffort_Movie_DB.Services;
using Mineffort_Movie_DB.Utils;
using Mineffort_Movie_DB.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace Mineffort_Movie_DB.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {

        private string queryText;
        public string QueryText
        {
            get { return queryText; }
            set
            {
                if (!string.Equals(this.queryText, value))
                {
                    this.queryText = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        public ObservableCollection<MovieBasic> PopularMovies { get; set; } = new ObservableCollection<MovieBasic>();
        public ObservableCollection<MovieBasic> TopRatedMovies { get; set; } = new ObservableCollection<MovieBasic>();
        public ObservableCollection<MovieBasic> NowPlayingMovies { get; set; } = new ObservableCollection<MovieBasic>();
        public ObservableCollection<MovieBasic> UpcomingMovies { get; set; } = new ObservableCollection<MovieBasic>();

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            getPopularMovies();
            getTopRatedMovies();
            getNowPlayingMovies();
            getUpcomingMovies();
            await base.OnNavigatedToAsync(parameter, mode, state);
        }

        private async void getPopularMovies()
        {
            var service = new MovieService();
            var popular = await service.GetPopularMovies();
            if (popular != null)
            {
                foreach (var item in popular.results)
                {
                    PopularMovies.Add(item);
                }
            }
        }

        private async void getTopRatedMovies()
        {
            var service = new MovieService();
            var topRated = await service.GetTopRatedMovies();
            if (topRated != null)
            {
                foreach (var item in topRated.results)
                {
                    TopRatedMovies.Add(item);
                }
            }
        }

        private async void getNowPlayingMovies()
        {
            var service = new MovieService();
            var popular = await service.GetNowPlayingMovies();
            if (popular != null)
            {
                foreach (var item in popular.results)
                {
                    NowPlayingMovies.Add(item);
                }
            }
        }

        private async void getUpcomingMovies()
        {
            var service = new MovieService();
            var upcoming = await service.GetUpcomingMovies();
            if (upcoming != null)
            {
                foreach (var item in upcoming.results)
                {
                    UpcomingMovies.Add(item);
                }
            }
        }

        public void OpenMovie(object sender, ItemClickEventArgs e)
        {
            var item = e.ClickedItem as MovieBasic;
            NavigationService.Navigate(typeof(MovieDetailsPage), item.id);
        }

        public void SearchMovieButtonClick(object sender, RoutedEventArgs e)
        {
            SearchMovie();
        }

        public void SearchMovieKeyboardDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
                SearchMovie();
        }

        public void SearchMovie()
        {
            if (queryText != null)
                NavigationService.Navigate(typeof(MovieListPage), queryText);
        }
    }
}
