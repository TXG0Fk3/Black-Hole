using System.IO;

namespace Black_Hole.Services
{
    internal class MagicWarmholeService
    {
        private string _filePath;
        private string _code;
        public string FileName { get; private set; }
        public string FileSize { get; private set; }

        public MagicWarmholeService(string filePathOrCode)
        {
            if (File.Exists(filePathOrCode))
            {
                _filePath = filePathOrCode;

                var fileInfo = new FileInfo(_filePath);
                FileName = fileInfo.Name;
                FileSize = fileInfo.Length.ToString();
            }
            else
            {
                _code = filePathOrCode;
                // To do: Implementar a lógica para coletar informações do arquivo a ser recebido
            }
        }

        public void SendFile()
        {
            // To Do: Implementar a lógica para enviar o arquivo
        }

        public void ReceiveFile()
        {
            // To Do: Implementar a lógica para receber o arquivo
        }
    }
}
