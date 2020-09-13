using System;
using System.Collections.Generic;
using System.Text;

namespace WPFMVVM
{
    interface IDialogService //функционал для работы с диалоговыми окнами
    {
        void ShowMessage(string message);   // показ сообщения
        string FilePath { get; set; }   // путь к выбранному файлу
        bool OpenFileDialog();  // открытие файла
        bool SaveFileDialog();  // сохранение файла
    }
}
