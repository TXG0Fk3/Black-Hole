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

        public MagicWormholeService(string PathOrCode)
        {
            if (File.Exists(PathOrCode))
            {
                FilePath = PathOrCode;

                var fileInfo = new FileInfo(FilePath);
                FileName = fileInfo.Name;
                FileSize = Math.Round(fileInfo.Length / 1000000.0, 3); // Converte em MB com 3 casas decimais

                ProcessStart($"send {FilePath}");

                for (int i = 0; i < 4; i++) // Coleta o código e descarta o restante das linhas pois são inúteis
                {
                    var line = _process.StandardError.ReadLine();
                    if (i == 1)
                    {
                        Code = line.Split(':')[1].Trim();
                    }
                }
            }
            else
            {
                Code = PathOrCode;

                ProcessStart($"receive {Code}");

                var receivedInfo = _process.StandardError.ReadLine(); // !! Talvez precise se tornar assíncrono para evitar travamentos na UI !!

                FileName = receivedInfo.Split(':')[1].Trim();
                FileSize = double.Parse(receivedInfo.Split('(')[1].Split(" ")[0]);
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
