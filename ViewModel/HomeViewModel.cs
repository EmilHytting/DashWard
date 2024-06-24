using DashWard.Repositories;
using System.Diagnostics;
using System.Windows.Input;

namespace DashWard.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        private int _users;
        public int Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        private UserRepository _userRepository;

        public ICommand OpenGitHubCommand { get; }
        public ICommand OpenLinkedInCommand { get; }
        public ICommand OpenTwitterCommand { get; }

        public HomeViewModel()
        {
            _userRepository = new UserRepository();
            LoadData();

            // Initialize commands
            OpenGitHubCommand = new RelayCommand(OpenGitHub);
            OpenLinkedInCommand = new RelayCommand(OpenLinkedIn);
            OpenTwitterCommand = new RelayCommand(OpenTwitter);
        }

        private void LoadData()
        {
            Users = _userRepository.GetUserCount();
        }

        private void OpenGitHub()
        {
            OpenUrl("https://github.com/EmilHytting");
        }

        private void OpenLinkedIn()
        {
            OpenUrl("https://www.linkedin.com/in/emil-hytting-415032283/");
        }

        private void OpenTwitter()
        {
            OpenUrl("https://x.com/FragZui");
        }

        private void OpenUrl(string url)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                // Handle exceptions if any
            }
        }
    }

    // RelayCommand implementation
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        public void Execute(object parameter)
        {
            _execute();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
