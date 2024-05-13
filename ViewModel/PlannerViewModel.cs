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
        private string _eventName;
        private string _description;
        private string _date;


        private IEnumerable<PlanerEventModel> _plannerEvents;

        private PlanerEventRepository planerRepository;
        private UserRepository userRepository;

        private UserModel _currentUser;
        
        
        
        public string EventName 
        {
            get => _eventName;
            set 
            { 
                _eventName = value;
                OnPropertyChanged(nameof(EventName));
            } 
        }
        public string Description 
        { 
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            } 
        }
        public string Date 
        { 
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
            } 
        }

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

        public ICommand PlannerClearCommand { get; }
        #endregion


        #region Constructor
        public PlannerViewModel()
        {
            userRepository = new UserRepository();
            planerRepository = new PlanerEventRepository();


            PlannerAddCommand = new ViewModelCommand(ExecutePlanerAddCommand, CanExecutePlanerAddCommand);
            PlannerDeleteCommand = new ViewModelCommand(ExecutePlannerDeleteCommand, CanExecutePlannerGroupCommand);
            PlannerClearCommand = new ViewModelCommand(ExecutePlannerClearCommand, CanExecutePlannerGroupCommand);
            PlannerEditCommand = new ViewModelCommand(ExecutePlannerEditCommand, CanExecutePlannerGroupCommand);


            CurrentUser = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            PlannerEvents = planerRepository.GetAll(CurrentUser.Id);
        }

        #endregion

        #region Methods
        private void ExecutePlanerAddCommand(object obj)
        {
            planerRepository.Add(new PlanerEventModel()
            {
                EventName = EventName,
                Description = Description,
                Date = Date,
            }, 
            CurrentUser.Id);
            PlannerEvents = planerRepository.GetAll(CurrentUser.Id);
        }

        private bool CanExecutePlanerAddCommand(object obj)
        {
            if(CurrentPlanerEvent == null)
            {
                return true;
            }
            return false;
        }

        private void ExecutePlannerDeleteCommand(object obj)
        {
            planerRepository.Remove(CurrentPlanerEvent.Id);
            PlannerEvents = planerRepository.GetAll(CurrentUser.Id);
        }

        private void ExecutePlannerClearCommand(object obj)
        {
            CurrentPlanerEvent = null;
            EventName = null;
            Description = null;
            Date = null;
        }

        private void ExecutePlannerEditCommand(object obj)
        {
            planerRepository.Edit(CurrentPlanerEvent.Id);
        }

        private bool CanExecutePlannerGroupCommand(object obj)
        {
            if(CurrentPlanerEvent !=  null)
            {
                EventName = CurrentPlanerEvent.EventName;
                Description = CurrentPlanerEvent.Description;
                Date = CurrentPlanerEvent.Date;


                return true;
            }
            return false;
        }
        #endregion

    }
}
