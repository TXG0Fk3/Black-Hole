using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Black_Hole.Services
{
    internal class MagicWormholeService
    {
        public string FilePath { get; private set; }
        public string Code { get; private set; }
        public string FileName { get; private set; }
        public double FileSize { get; private set; }

        private ProcessStartInfo _startInfo = new ProcessStartInfo
        {
            FileName = "wormhole.exe",
            RedirectStandardError = true,
            RedirectStandardInput = true,
            UseShellExecute = false,
            CreateNoWindow = true,
        };

        private Process _process;

        public MagicWormholeService(string filePathOrCode)
        {
            if (File.Exists(filePathOrCode))
            {
                FilePath = filePathOrCode;

                var fileInfo = new FileInfo(FilePath);
                FileName = fileInfo.Name;
                FileSize = Math.Round(fileInfo.Length / 1000000.0, 3); // Converte em MB com 3 casas decimais
            }
            else
            {
                Code = filePathOrCode;

                _startInfo.Arguments = $"receive {Code}";
                _process = new Process { StartInfo = _startInfo };
                _process.Start();

                var receiveInfo = _process.StandardError.ReadLine(); // !! Talvez precise se tornar assíncrono para evitar travamentos na UI !!

                FileName = receiveInfo.Split(':')[1].Trim();
                FileSize = double.Parse(receiveInfo.Split('(')[1].Split(" ")[0]);
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

        private void ProcessStart(string args)
        {
            _startInfo.Arguments = args;
            _process = new Process { StartInfo = _startInfo };
            _process.Start();
        }
    }
}
