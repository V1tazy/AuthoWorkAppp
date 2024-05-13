using AuthoWorkAppp.Model;
using AuthoWorkAppp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AuthoWorkAppp.ViewModel
{
    internal class PersonalViewModel: ViewModelBase
    {
        private UserModel _currentUser;
        private IUserRepository userRepository;


        public UserModel CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }

        public ICommand PersonalEditCommand { get; }



        public PersonalViewModel()
        {
            userRepository = new UserRepository();
            CurrentUser = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            PersonalEditCommand = new ViewModelCommand(ExecutePersonalEditCommand, CanExecutePersonalEditCommand);
        }

        private bool CanExecutePersonalEditCommand(object obj)
        {
            if(CurrentUser != null)
            {
                return true;
            }

            return false;
        }

        private void ExecutePersonalEditCommand(object obj)
        {
            userRepository.Edit(CurrentUser);
        }
    }
}
