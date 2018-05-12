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
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace Mineffort_Movie_DB.ViewModels
{
    public class PersonDetailsPageViewModel : ViewModelBase
    {
        private PersonExtended person;
        public PersonExtended Person
        {
            get { return person; }
            set
            {
                Set(ref person, value);
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

        public ObservableCollection<PersonCredits.Cast> Credits { get; set; } = new ObservableCollection<PersonCredits.Cast>();

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            getPersonData((int)parameter);
            getPersonCredits((int)parameter);
            await base.OnNavigatedToAsync(parameter, mode, state);
        }

        private async void getPersonData(int id)
        {
            var service = new MovieService();
            var person = await service.GetPersonExtended(id);
            Person = person;
        }

        private async void getPersonCredits(int id)
        {
            var service = new MovieService();
            var credits = await service.GetPersonCredits(id);
            credits.cast = credits.cast.OrderByDescending(o => o.release_date).ToList();
            if (credits.cast != null)
            {
                foreach (var item in credits.cast)
                {
                    Credits.Add(item);
                }
            }
        }

        public void OpenMovie(object sender, ItemClickEventArgs e)
        {
            var item = e.ClickedItem as PersonCredits.Cast;
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
