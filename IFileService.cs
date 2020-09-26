using System.Collections.Generic;
using System.Data.SqlClient;

//Первый метод предназначен для открытия файла. Он принимает путь к файлу и возвращает список объектов. 
//Второй метод сохраняет данные из списка в файле по определенному пути.

namespace WPFMVVM
{
    interface IFileService
    {
        List<Model> Open(string filename);
        void Save(string filename, List<Model> usersList);
    }
}
