using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace WPFMVVM
{
    class ViewModel: INotifyPropertyChanged
    {
        private Model selectedUser;
        public ObservableCollection<Model> People { get; set; }
        // команда добавления нового объекта
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??(addCommand = new RelayCommand(obj =>
                  {
                      Model user = new Model();
                      People.Insert(0, user);
                      SelectedUser = user;
                  }));
            }
        }

        // команда удаления
        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ?? (removeCommand = new RelayCommand(obj =>
                {
                    Model user = obj as Model;
                    if (user != null)
                    {
                        People.Remove(user);
                    }
                },
              (obj) => People.Count > 0));
            }
        }

        public Model SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                OnPropertyChanged("SelectedUser");
            }
        }

        public ViewModel()
        {
            People = new ObservableCollection<Model>
            {
                new Model {LOGIN = "Gira", FIRST_NAME = "Lexa", LAST_NAME = "Kurochkin", MIDDLE_NAME = "Huropov" },
                new Model {LOGIN = "Tupok", FIRST_NAME = "Vis", LAST_NAME = "Ukropin", MIDDLE_NAME = "Yorka" },
                new Model {LOGIN = "ALEX_KILLER", FIRST_NAME = "Sanek", LAST_NAME = "Churochkin", MIDDLE_NAME = "Mixailov" },
                new Model {LOGIN = "EkonBukon", FIRST_NAME = "Elena", LAST_NAME = "Ortopedova", MIDDLE_NAME = "Galimovna" }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
    
}
