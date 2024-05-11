using AuthoWorkAppp.Model;
using AuthoWorkAppp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Net;
using System.Windows;

namespace AuthoWorkAppp.ViewModel
{
    internal class RegistrationViewModel: ViewModelBase
    {
        #region Fields
        private string _username;
        private string _password;
        private string _email;
        private string _fullname;
        private string _errormessage;
        private bool _isViewVisiable = true;

        private IUserRepository userRepository;

        public string Username { 
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            } 
        }
        public string Password { 
            get => _password;
            set 
            { 
                _password = value;
                OnPropertyChanged(nameof(Password));
            } 
        }
        public string Email 
        { 
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            } 
        }
        public string Fullname 
        { 
            get => _fullname;
            set 
            { 
                _fullname = value;
                OnPropertyChanged(nameof(Fullname));
            } 
        }
        public string Errormessage 
        { 
            get => _errormessage;
            set 
            { 
                _errormessage = value;
                OnPropertyChanged(nameof(Errormessage));
            } 
        }
        public bool IsViewVisible 
        { 
            get => _isViewVisiable;
            set
            {
                _isViewVisiable = value;
                OnPropertyChanged(nameof(IsViewVisible));
            } 
        }
        #endregion

        #region Commands

        public ICommand RegistationCommand { get; }
        public ICommand SendEmailCommand { get; }
        public ICommand ShowPasswordCommand { get; }
        #endregion

        #region Constructors
        public RegistrationViewModel()
        {
            userRepository = new UserRepository();
            RegistationCommand = new ViewModelCommand(ExecuteRegistrationCommand, CanExecuteRegistationCommand);
        }
        #endregion

        #region Methods
        private bool CanExecuteRegistationCommand(object obj)
        {
            bool validData;
            if(string.IsNullOrEmpty(Username) || Username.Length < 5 || Password == null || Password.Length < 5)
                validData = false;
            else
                validData = true;

            return validData;
        }

        private void ExecuteRegistrationCommand(object obj)
        {
            bool isUserExist = userRepository.GetExistUserByName(Username) && userRepository.GetExistUserByEmail(Email);
            if (isUserExist)
            {
                userRepository.Add(new UserModel
                {
                    Username = Username,
                    Email = Email,
                    Password = Password,
                    Name = "Пользователь",
                    LastName = "Пользователь"
                });
            }
            else
            {
                Errormessage = "*Такой пользователь уже существует";
            }
        }
        #endregion
    }
}
