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
    internal class PlannerViewModel : ViewModelBase
    {
        #region Fields
        private PlanerEventModel _currentPlanerEvent;

        private IEnumerable<PlanerEventModel> _plannerEvents;

        private PlanerEventRepository planerRepository;
        private UserRepository userRepository;

        private UserModel _currentUser;


        public PlanerEventModel CurrentPlanerEvent
        {
            get => _currentPlanerEvent;
            set
            {
                _currentPlanerEvent = value;
                OnPropertyChanged(nameof(CurrentPlanerEvent));
            }
        }

        public IEnumerable<PlanerEventModel> PlannerEvents
        {
            get => _plannerEvents;
            set
            {
                _plannerEvents = value;
                OnPropertyChanged(nameof(PlannerEvents));
            }
        }

        public UserModel CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }
        #endregion

        #region Commands
        public ICommand PlannerEditCommand { get; }

        public ICommand PlannerDeleteCommand { get; }

        public ICommand PlannerAddCommand { get; } 
        #endregion


        #region Constructor
        public PlannerViewModel()
        {
            userRepository = new UserRepository();
            planerRepository = new PlanerEventRepository();
            PlannerAddCommand = new ViewModelCommand(ExecutePlanerAddCommand);
            CurrentUser = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            PlannerEvents = planerRepository.GetAll(CurrentUser.Id);
        }
        #endregion

        #region Methods
        private void ExecutePlanerAddCommand(object obj)
        {
        }
        #endregion

    }
}
