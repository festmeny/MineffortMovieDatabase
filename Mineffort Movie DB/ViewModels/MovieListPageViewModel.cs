using Mineffort_Movie_DB.Models;
using Mineffort_Movie_DB.Services;
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
    public class MovieListPageViewModel : ViewModelBase
    {
        public ObservableCollection<MovieBasic> Movies { get; set; } = new ObservableCollection<MovieBasic>();
        private int page = 1;
        private int pages = 1;
        private string query;
        
        public override async Task OnNavigatedToAsync( object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            query = (string)parameter;

            GetMovies(page, query);
            await base.OnNavigatedToAsync(parameter, mode, state);
        }

        private async void GetMovies(int page, string query)
        {
            var service = new MovieService();
            var movies = await service.SearchMovies(query);

            page = movies.page;
            pages = movies.total_pages;
            foreach (var item in movies.results)
            {
                Movies.Add(item);
            }
        }

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
