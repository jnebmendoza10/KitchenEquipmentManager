using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using KitchenEquipmentManager.Domain.Models;
using KitchenEquipmentManager.Infrastructure.Services.Sites;
using KitchenEquipmentManager.UI.Command;
using KitchenEquipmentManager.UI.Command.Generic;

namespace KitchenEquipmentManager.UI.ViewModels
{
    public class SiteViewModel : BaseViewModel
    {
        private ObservableCollection<Site> _sites;
        private string _description;
        private string _isActive;
        private UserViewModel _currentUserLoggedIn = new UserViewModel();

        private readonly ISiteService _siteService;

        private ICommand _addSiteCommand;
        private ICommand _updateSiteCommand;
        private ICommand _removeSiteCommand;
        private ICommand _cancelCommand;

        public SiteViewModel(ISiteService siteService) 
        { 
            _siteService = siteService ?? throw new ArgumentNullException(nameof(siteService));
            SelectedSiteState = SiteStates.FirstOrDefault();
        }

        public ICommand AddSiteCommand
        {
            get
            {
                if (_addSiteCommand == null)
                {
                    _addSiteCommand = new RelayCommand(addSite);
                }

                return _addSiteCommand;
            }
        }

        public ICommand UpdateSiteCommand
        {
            get
            {
                if (_updateSiteCommand == null)
                {
                    _updateSiteCommand = new RelayCommand<Site>(u => updateSite(u));
                }

                return _updateSiteCommand;
            }
        }

        public ICommand DeleteSiteCommand
        {
            get
            {
                if (_removeSiteCommand == null)
                {
                    _removeSiteCommand = new RelayCommand<Site>(d => deleteSite(d));
                }

                return _removeSiteCommand;
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                {
                    _cancelCommand = new RelayCommand(clearFields);
                }

                return _cancelCommand;
            }
        }

        public ObservableCollection<Site> Sites
        {
            get => _sites;
            set => SetProperty(ref _sites, value, nameof(Sites));
        }

        public string SiteDescription
        {
            get => _description;
            set => SetProperty(ref _description, value, nameof(SiteDescription));
        }

        public string SelectedSiteState
        {
            get => _isActive;
            set => SetProperty(ref _isActive, value, nameof(SelectedSiteState));
        }

        public UserViewModel CurrentUserLoggedIn
        {
            get => _currentUserLoggedIn;
            set => SetProperty(ref _currentUserLoggedIn, value, nameof(CurrentUserLoggedIn));
        }

        public List<string> SiteStates
        {
            get
            {
                return new List<string>() 
                { 
                    "Yes",
                    "No"
                };
            }
        }

        public void GetSites()
        {
            var user = new User();
            user.Id = CurrentUserLoggedIn.UserId;
            user.UserName = CurrentUserLoggedIn.UserName;
            user.UserType = CurrentUserLoggedIn.UserType;

            var sites = _siteService.RetrieveSitesForUser(user);

            Sites = new ObservableCollection<Site>(sites);
        }

        private void addSite()
        {
            if (string.IsNullOrEmpty(SiteDescription))
            {
                MessageBox.Show("Please fill up empty fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var site = new Site();
            site.Active = SelectedSiteState == "Yes";
            site.Description = SiteDescription;
            site.UserId = CurrentUserLoggedIn.UserId;

            bool isAdded = _siteService.AddSite(site);

            if (isAdded)
            {
                Sites.Add(site);
                MessageBox.Show("Added successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("There is an error adding the item.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void updateSite(Site site)
        {
            if (site == null)
                return;

            bool isUpdateSuccessful = _siteService.UpdateSite(site);

            if (isUpdateSuccessful)
            {
                MessageBox.Show("Update successful", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Update unsuccessful.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void deleteSite(Site site)
        {
            if (site == null)
                return;

            bool isDeleteSuccessful = _siteService.DeleteSite(site);

            if (isDeleteSuccessful)
            {
                Sites.Remove(site);
                MessageBox.Show("Delete successful", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Delete successful", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void clearFields()
        {
            SiteDescription = "";
        }
    }
}
