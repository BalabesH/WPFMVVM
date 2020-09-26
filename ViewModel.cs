using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Linq;
using System;
using System.Data.Entity;
using System.Collections.Generic;

namespace WPFMVVM
{
    class ViewModel: INotifyPropertyChanged
    {
        Context db;
        RelayCommand addCommand;
        RelayCommand editCommand;
        RelayCommand deleteCommand;

        IFileService fileService;
        IDialogService dialogService;

        IEnumerable<Model> people;
        public IEnumerable<Model> People
        {
            get { return people; }
            set
            {
                people = value;
                OnPropertyChanged("People");
            }
        }


        public ViewModel(IDialogService dialogService, IFileService fileService)
        {
            this.dialogService = dialogService;
            this.fileService = fileService;

            //загружаем данные из бд в локальный кэш
            db = new Context();
            db.People.Load();
            People = db.People.Local.ToBindingList();
        }

        public RelayCommand AddCommand // добавление
        {
            get
            {
                return addCommand ?? (addCommand = new RelayCommand((o) =>
                {
                    UserWindow userWindow = new UserWindow(new Model());
                    if (userWindow.ShowDialog() == true)
                    {
                        Model user = userWindow.Model;
                        db.People.Add(user);
                        db.SaveChanges();
                    }
                }));
            }
        }

        public RelayCommand EditCommand //редактирование
        {
            get
            {
                return editCommand ?? (editCommand = new RelayCommand((selectedItem) =>
                  {
                      if (selectedItem == null) return;//выделенный объект
                      Model user = selectedItem as Model;

                      Model vm = new Model()
                      {
                          ID = user.ID,
                          LOGIN = user.LOGIN,
                          FIRST_NAME = user.FIRST_NAME,
                          LAST_NAME = user.LAST_NAME,
                          MIDDLE_NAME = user.MIDDLE_NAME
                      };
                      UserWindow userWindow = new UserWindow(vm);

                      if (userWindow.ShowDialog() == true)
                      {
                          //измененный объект
                          user = db.People.Find(userWindow.Model.ID);
                          if (user != null)
                          {
                              user.LOGIN = userWindow.Model.LOGIN;
                              user.FIRST_NAME = userWindow.Model.FIRST_NAME;
                              user.LAST_NAME = userWindow.Model.LAST_NAME;
                              user.MIDDLE_NAME = userWindow.Model.MIDDLE_NAME;
                              db.Entry(user).State = EntityState.Modified;
                              db.SaveChanges();
                          }
                      }
                  }));
            }
        }

        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ?? (deleteCommand = new RelayCommand((selectedItem) =>
                {
                    if (selectedItem == null) return;//выделенный объект
                    Model user = selectedItem as Model;
                    db.People.Remove(user);
                    db.SaveChanges();
                }));
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
