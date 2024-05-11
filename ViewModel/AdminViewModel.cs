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
        #endregion

        public IEnumerable<UserModel> UserModels
        {
            get { return userModels; }
            set 
            { 
                userModels = value;
                OnPropertyChanged(nameof(userModels));
            }
        }

        #region Commands
        ICommand AdminEditCommand { get; }
        ICommand AdminDeleteCommand { get; }
        #endregion

        #region Constructor
        public AdminViewModel()
        {
            userRepository = new UserRepository();
            userModels = userRepository.GetAll();
        }
        #endregion
    }
}
