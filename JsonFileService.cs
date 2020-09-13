using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;

//С помощью класса DataContractJsonSerializer
//здесь производится сериализация/десериализация объектов в файл json в виде набора List<Phone>.
namespace WPFMVVM
{
    class JsonFileService: IFileService
    {
        public List<Model> Open(string filename)
        {
            List<Model> users = new List<Model>();
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Model>));
            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                users = jsonFormatter.ReadObject(fs) as List<Model>;
            }

            return users;
        }

        public void Save(string filename, List<Model> usersList)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Model>));
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                jsonFormatter.WriteObject(fs, usersList);
            }
        }
    }
}
