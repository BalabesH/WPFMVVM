using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Data.SqlClient;
using System.DirectoryServices.ActiveDirectory;
using System.ComponentModel.DataAnnotations.Schema;

namespace WPFMVVM
{
    [Table("People", Schema = "dbo")]
    public class Model : INotifyPropertyChanged
    {
       private string login;
       private string first_name;
       private string last_name;
       private string middle_name;

       public int ID { get; set; }

       public virtual string LOGIN
        {
            get { return login; }
            set
            {
                login = value;
                OnCollectionChanged("LOGIN");
            }
        }

        public virtual string FIRST_NAME
        {
            get { return first_name; }
            set
            {
                first_name = value;
                OnCollectionChanged("FIRST_NAME");
            }
        }
        public virtual string LAST_NAME
        {
            get { return last_name; }
            set
            {
                last_name = value;
                OnCollectionChanged("LAST_NAME");
            }
        }
        public virtual string MIDDLE_NAME
        {
            get { return middle_name; }
            set
            {
                middle_name = value;
                OnCollectionChanged("MIDDLE_NAME");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnCollectionChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
