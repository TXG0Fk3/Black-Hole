using System.IO;

namespace Black_Hole.Services
{
    internal class MagicWarmholeService
    {
        public string FilePath { get; private set; }
        public string Code { get; private set; }
        public string FileName { get; private set; }
        public double FileSize { get; private set; }

        public MagicWarmholeService(string filePathOrCode)
        {
            if (File.Exists(filePathOrCode))
            {
                FilePath = filePathOrCode;

                var fileInfo = new FileInfo(FilePath);
                FileName = fileInfo.Name;
                FileSize = fileInfo.Length / 1000000; // Converte em MB
            }
            else
            {
                Code = filePathOrCode;
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
