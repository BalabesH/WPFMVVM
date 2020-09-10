using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace WPFMVVM
{
    class UserViewModel: INotifyPropertyChanged
    {
        private Model user;

        public UserViewModel(Model John)
        {
            user = John;
        }

        public string LOGIN
        {
            get { return user.LOGIN; }
            set
            {
                user.LOGIN = value;
                OnPropertyChanged("LOGIN");
            }
        }

        public string FIRST_NAME
        {
            get { return user.FIRST_NAME; }
            set
            {
                user.FIRST_NAME = value;
                OnPropertyChanged("FIRST_NAME");
            }
        }

        public string LAST_NAME
        {
            get { return user.LAST_NAME; }
            set
            {
                user.LAST_NAME = value;
                OnPropertyChanged("LAST_NAME");
            }
        }

        public string MIDDLE_NAME
        {
            get { return user.MIDDLE_NAME; }
            set
            {
                user.MIDDLE_NAME = value;
                OnPropertyChanged("MIDDLE_NAME");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
