using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Linq;
using System;

namespace WPFMVVM
{
    class ViewModel: INotifyPropertyChanged
    {
        private Model selectedUser;
        IFileService fileService;
        IDialogService dialogService;

        public ObservableCollection<Model> People { get; set; }

        // команда сохранения файла
        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ?? 
                  (saveCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          if (dialogService.SaveFileDialog() == true)
                          {
                              fileService.Save(dialogService.FilePath, People.ToList());
                              dialogService.ShowMessage("Файл сохранен");
                          }
                      }
                      catch (Exception ex)
                      {
                          dialogService.ShowMessage(ex.Message);
                      }
                  }));
            }
        }

        // команда открытия файла
        private RelayCommand openCommand;
        public RelayCommand OpenCommand
        {
            get
            {
                return openCommand ??
                  (openCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          if (dialogService.OpenFileDialog() == true)
                          {
                              var phones = fileService.Open(dialogService.FilePath);
                              People.Clear();
                              foreach (var p in phones)
                              {
                                  People.Add(p);
                                  dialogService.ShowMessage("Файл открыт");
                              }
                          }
                      }
                      catch (Exception ex)
                      {
                          dialogService.ShowMessage(ex.Message);
                      }
                  }));
            }
        }
        // команда добавления новой записи
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

        //копирование элементов полей выделенной записи
        private RelayCommand copyCommand;
        public RelayCommand CopyCommand
        {
            get
            {
                return copyCommand ??
                    (copyCommand = new RelayCommand(obj =>
                    {
                        Model user = obj as Model;
                        if (user != null)
                        {
                            Model userCopy = new Model
                            {
                                LOGIN = user.LOGIN,
                                FIRST_NAME = user.FIRST_NAME,
                                LAST_NAME = user.LAST_NAME,
                                MIDDLE_NAME = user.MIDDLE_NAME
                            };
                            People.Insert(0, userCopy);
                        }
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



        public ViewModel(IDialogService dialogService, IFileService fileService)
        {
            this.dialogService = dialogService;
            this.fileService = fileService;

            //Данные по дефолту
            People = new ObservableCollection<Model>
            {
                new Model {LOGIN = "Giran", FIRST_NAME = "Lexa", LAST_NAME = "Kurochkin", MIDDLE_NAME = "Huropov" },
                new Model {LOGIN = "Doner", FIRST_NAME = "Hummel", LAST_NAME = "Ukropin", MIDDLE_NAME = "Yorka'Ogly" },
                new Model {LOGIN = "ALEX_KILLER", FIRST_NAME = "Sanek", LAST_NAME = "Churochkin", MIDDLE_NAME = "Mixailov" },
                new Model {LOGIN = "Kawai_09209", FIRST_NAME = "Elena", LAST_NAME = "Ortopedova", MIDDLE_NAME = "Galimovna" }
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
