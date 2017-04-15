using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using SportBet.Contracts;
using SportBet.Contracts.Subjects;
using SportBet.Models.Display;

namespace SportBet.SuperuserControls.ViewModels
{
    public class BookmakerListViewModel : ObservableObject, IObserver
    {
        private readonly ISubject subject;
        private readonly FacadeBase<BookmakerDisplayModel> facade;

        private BookmakerDisplayModel bookmaker;
        private string firstNameFilter;
        private string lastNameFilter;
        private string phoneNumberFilter;

        public BookmakerListViewModel(ISubject subject, FacadeBase<BookmakerDisplayModel> facade)
        {
            this.subject = subject;
            this.facade = facade;

            subject.Subscribe(this);

            this.Bookmakers = new ObservableCollection<BookmakerDisplayModel>(facade.GetAll());
            this.SortedBookmakers.Filter = FilterBookmaker;
            this.RefreshBookmakers = new DelegateCommand(() => SortedBookmakers.Refresh(), obj => true);

            RaisePropertyChangedEvent("Bookmakers");
            RaisePropertyChangedEvent("SelectedBookmaker");
        }

        public ICommand RefreshBookmakers { get; private set; }

        public string FirstNameFilter
        {
            get { return firstNameFilter; }
            set
            {
                firstNameFilter = value;
                RaisePropertyChangedEvent("FirstNameFilter");
            }
        }

        public string LastNameFilter
        {
            get { return lastNameFilter; }
            set
            {
                lastNameFilter = value;
                RaisePropertyChangedEvent("LastNameFilter");
            }
        }

        public string PhoneNumberFilter
        {
            get { return phoneNumberFilter; }
            set
            {
                phoneNumberFilter = value;
                RaisePropertyChangedEvent("PhoneNumberFilter");
            }
        }

        public void Update()
        {
            IEnumerable<BookmakerDisplayModel> bookmakers = facade.GetAll();

            Bookmakers.Clear();
            foreach (BookmakerDisplayModel bookmaker in bookmakers)
            {
                Bookmakers.Add(bookmaker);
            }

            RaisePropertyChangedEvent("SelectedBookmaker");
        }

        public BookmakerDisplayModel SelectedBookmaker
        {
            get { return bookmaker; }
            set
            {
                bookmaker = value;
                RaisePropertyChangedEvent("SelectedBookmaker");
            }
        }

        public ICollectionView SortedBookmakers
        {
            get { return CollectionViewSource.GetDefaultView(Bookmakers); }
        }

        public ObservableCollection<BookmakerDisplayModel> Bookmakers { get; private set; }

        private bool FilterBookmaker(object obj)
        {
            BookmakerDisplayModel bookmaker = obj as BookmakerDisplayModel;

            return
                bookmaker.FirstName.Contains(FirstNameFilter ?? String.Empty) &&
                bookmaker.LastName.Contains(LastNameFilter ?? String.Empty) &&
                bookmaker.PhoneNumber.Contains(PhoneNumberFilter ?? String.Empty);
        }
    }
}
