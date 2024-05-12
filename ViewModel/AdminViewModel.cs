using AuthoWorkAppp.Model;
using AuthoWorkAppp.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AuthoWorkAppp.ViewModel
{
    internal class AdminViewModel: ViewModelBase
    {
        #region Field
        private UserRepository userRepository = new UserRepository();
        private IEnumerable<UserModel> userModels;
        private UserModel _currentUser;
        #endregion


        public UserModel CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }
        public IEnumerable<UserModel> UserModels
        {
            get { return userModels; }
            set 
            { 
                userModels = value;
                OnPropertyChanged(nameof(UserModels));
            }
        }

        #region Commands
        public ICommand AdminEditCommand { get; }
        public ICommand AdminDeleteCommand { get; }
        #endregion

        #region Edit
        private bool CanExecuteEditCommand(object obj)
        {
            if (CurrentUser != null && CurrentUser.Username != "admin")
            {
                return true;
            }
            return false;
        }

        private void ExecuteEditCommand(object obj)
        {
            userRepository.Edit(CurrentUser);
            UserModels = userRepository.GetAll();
        }
        private bool CanExecutedDeletedCommand(object obj)
        {
            if(CurrentUser != null && CurrentUser.Username != "admin")
            {
                return true;
            }
            return false;
        }

        private void ExecuteDeleteCommand(object obj)
        {
            userRepository.Remove(CurrentUser.Id);
            UserModels = userRepository.GetAll();
        }
        #endregion

        #region Constructor
        public AdminViewModel()
        {
            userRepository = new UserRepository();
            UserModels = userRepository.GetAll();
            AdminEditCommand = new ViewModelCommand(ExecuteEditCommand, CanExecuteEditCommand);
            AdminDeleteCommand = new ViewModelCommand(ExecuteDeleteCommand, CanExecutedDeletedCommand);
        }
        #endregion
    }
}
