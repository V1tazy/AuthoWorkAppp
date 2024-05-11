using AuthoWorkAppp.Model;
using AuthoWorkAppp.Repositories;
using AuthoWorkAppp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Net;
using System.Threading;
using System.Windows;
using System.ComponentModel;

namespace AuthoWorkAppp.ViewModel
{
    internal class MainViewModel: ViewModelBase
    {
        private bool _isAdmin;
        private bool _isVisible;
        private UserAccountModel _currentUserAccount;

        private IUserRepository userRepository;

        private object _currentView;
        public object CurrentView 
        { 
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }
        public UserAccountModel CurrentUserAccount
        {
            get => _currentUserAccount;
            set
            {
                _currentUserAccount = value;
                OnPropertyChanged(nameof(CurrentUserAccount));
            }
        }
        public bool IsAdmin
        {
            get => _isAdmin;
            set
            {
                _isAdmin = value;
                OnPropertyChanged(nameof(IsAdmin));
            }
        }

        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                _isVisible = value;
                OnPropertyChanged(nameof(IsVisible));
            }
        }


        #region Commands
        public ICommand AdminCommand { get; set; }
        public ICommand PlannerCommand { get; set; }
        public ICommand PersonalCommand { get; set; }
        #endregion

        #region Methods
        private void Planner(object obj) => CurrentView = new PlannerViewModel();
        private void Personal(object obj) => CurrentView = new PersonalViewModel();
        private void Admin(object obj) => CurrentView = new AdminViewModel();

        private bool CanExecuteAdmin(object obj)
        {
            bool isAdmin;
            if (userRepository.CheckAdminUser(CurrentUserAccount.Username))
            {
                isAdmin = true;
                IsVisible = true;
            }
            else
            {
                isAdmin = false;
                IsVisible = false;
            }

            return isAdmin;
        }
        #endregion


        #region Constructor
        public MainViewModel()
        {
            userRepository = new UserRepository();
            LoadCurrentUserData();
            AdminCommand = new ViewModelCommand(Admin, CanExecuteAdmin);
            PlannerCommand = new ViewModelCommand(Planner);
            PersonalCommand = new ViewModelCommand(Personal);
        }
        #endregion
        private void LoadCurrentUserData()
        {
            var user = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);

            if (user != null)
            {
                CurrentUserAccount = new UserAccountModel()
                {
                    Username = user.Username,
                    DisplayName = $"{user.Name}",
                    ProfilePicture = null
                };
            }
            else
            {
                //MessageBox.Show("Ошибка пользователь неавторизирован", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
                // Application.Current.Shutdown();
            }
        }
    }
}
