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
    public class MovieDetailsPageViewModel : ViewModelBase
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

        private MovieExtended movie;
        public MovieExtended Movie
        {
            get { return movie; }
            set
            {
                Set(ref movie, value);
            }
        }

        private List<String> directors;
        public List<String> Directors
        {
            get { return directors; }
            set
            {
                Set(ref directors, value);
            }
        }

        public ObservableCollection<MovieBasic> SimilarMovies { get; set; } = new ObservableCollection<MovieBasic>();
        public ObservableCollection<MovieCredits.Cast> Cast { get; set; } = new ObservableCollection<MovieCredits.Cast>();

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            getSimilarMovies((int)parameter);
            getMovieData((int)parameter);
            getCredits((int)parameter);
            await base.OnNavigatedToAsync(parameter, mode, state);
        }

        private async void getMovieData(int id)
        {
            var service = new MovieService();
            var movie = await service.GetMovieDetails(id);
            Movie = movie;
        }
        
        private async void getSimilarMovies(int id)
        {
            var service = new MovieService();
            var similar= await service.GetSimilarMovies(id);
            if (similar.results != null)
            {
                foreach (var item in similar.results)
                {
                    SimilarMovies.Add(item);
                }
            }
        }

        private async void getCredits(int id)
        {
            var service = new MovieService();
            var credits = await service.GetMovieCredits(id);
            if (credits != null)
            {
                foreach (var item in credits.cast)
                {
                    Cast.Add(item);
                }

                var directorList = new List<string>();
                foreach (var item in credits.crew)
                {
                    if (item.department == "Directing")
                    {
                        directorList.Add(item.name);
                    }
                }

                Directors = directorList;
            }
        }

        public void OpenPerson(object sender, ItemClickEventArgs e)
        {
            var item = e.ClickedItem as MovieCredits.Cast;
            NavigationService.Navigate(typeof(PersonDetailsPage), item.id);
        }

        public void OpenRelatedMovie(object sender, ItemClickEventArgs e)
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
