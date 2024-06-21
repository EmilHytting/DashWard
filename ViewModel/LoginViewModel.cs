using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Input;
using DashWard.Model;
using DashWard.Repositories;
using System.Net;
using System.Security.Principal;

namespace DashWard.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        //Fields
        private string _username;
        private SecureString _password;
        private string _errormessage;
        private bool _isViewVisible = true;

        private IUserRepository userRepository;

        // Property
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public SecureString Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string ErrorMessage
        {
            get
            {
                return _errormessage;
            }

            set
            {
                _errormessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public bool IsViewVisible
        {
            get
            {
                return _isViewVisible;
            }

            set
            {
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }

        // -> Commands
        public ICommand LoginCommand { get; }
        public ICommand RecoverPasswordCommand { get; }
        public ICommand ShowPasswordCommand { get; }
        public ICommand RememberPasswordCommand { get; }

        //Constructor
        public LoginViewModel()
        {
            userRepository = new UserRepository();
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            RecoverPasswordCommand = new ViewModelCommand( p => ExecuteRecoverPassCommand("", ""));
        }



        private bool CanExecuteLoginCommand(object obj)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(Username) || Username.Length < 3 ||
                Password == null || Password.Length < 3)
                validData = false;
            else
                validData = true;
            return validData;
        }

        private void ExecuteLoginCommand(object obj)
        {
            var isValidUser = userRepository.AuthenticateUser(new NetworkCredential(Username, Password));
            if (isValidUser)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(Username), null);
                IsViewVisible = false;
            }
            else
            {
                ErrorMessage = "* Invalid username or password.";
            }
        }

        private void ExecuteRecoverPassCommand(string username, string email)
        {
            throw new NotImplementedException();
        }
    }
}
