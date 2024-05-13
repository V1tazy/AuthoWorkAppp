using AuthoWorkAppp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AuthoWorkAppp.Repositories;
using System.Net;
using System.Threading;
using System.Security.Principal;
using System.Windows.Ink;
using AuthoWorkAppp.View;
using System.ComponentModel.Design;

namespace AuthoWorkAppp.ViewModel
{
    public class LoginViewModel: ViewModelBase
    {
        #region fields
        private string _login;
        private SecureString _password;
        private string _username;
        private string _errorMessage;
        private bool _isViewVisible = true;

        private IUserRepository userRepository;
        public string Login { 
            get => _login;
            set 
            { 
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }
        public SecureString Password {
            get => _password; 
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string Username { 
            get => _username; 
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public string ErrorMessage { 
            get => _errorMessage; 
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }
        public bool IsViewVisible { 
            get => _isViewVisible; 
            set
            {
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }
        #endregion

        #region Commands
        public ICommand LoginCommand { get;}
        public ICommand RecoverPasswordCommand { get;}
        public ICommand ShowPasswordCommand { get;}
        public ICommand RememberPasswordCommand { get;}

        #endregion

        #region Constructor
        public LoginViewModel()
        {
            userRepository  = new UserRepository();
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            RecoverPasswordCommand = new ViewModelCommand(p => ExecuteRecoverPassCommand("", ""));
        }

        #endregion

        #region Methods

        private bool CanExecuteLoginCommand(object obj)
        {
            bool validData;
            if (string.IsNullOrEmpty(Username) || Username.Length < 5 || Password == null || Password.Length < 5)
                validData = false;
            else
                validData = true;

            return validData;
        }


        private void ExecuteLoginCommand(object obj)
        {
            var isValidUser = userRepository.AuthenticateUser(new NetworkCredential(Username, Password));

            if(isValidUser)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(Username), null);
                IsViewVisible = false;
            }
            else
            {
                ErrorMessage = "* Введено неверное имя или пароль";
            }
        }
        private void ExecuteRecoverPassCommand(string username, string email)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
