using AuthoWorkAppp.Model;
using AuthoWorkAppp.Repositories;
using AuthoWorkAppp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AuthoWorkAppp.ViewModel
{
    internal class MainViewModel: ViewModelBase
    {
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


        #region Commands
        public ICommand AdminCommand { get; set; }
        public ICommand PlannerCommand { get; set; }
        public ICommand PersonalCommand { get; set; }
        #endregion

        #region Methods
        private void Admin(object obj) => CurrentView = new AdminViewModel();
        private void Planner(object obj) => CurrentView = new PlannerViewModel();
        private void Personal(object obj) => CurrentView = new PlannerViewModel();
        #endregion


        #region Constructor
        public MainViewModel()
        {
            AdminCommand = new ViewModelCommand(Admin);
            PlannerCommand = new ViewModelCommand(Planner);
            PersonalCommand = new ViewModelCommand(Personal);
        }
        #endregion
    }
}
