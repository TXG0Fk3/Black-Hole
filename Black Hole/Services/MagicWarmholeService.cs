﻿using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace Black_Hole.Services
{
    internal class MagicWormholeService(string path = "", string code = "")
    {
        public string Path { get; private set; } = path;
        public string Code { get; private set; } = code;
        public string Name { get; private set; }
        public double Size { get; private set; }

        private readonly ProcessStartInfo _startInfo = new()
        {
            FileName = "wormhole.exe",
            RedirectStandardError = true,
            RedirectStandardInput = true,
            UseShellExecute = false,
            CreateNoWindow = true,
        };
        
        private Process _process;

        public void LoadSendInfo()
        {
            var fileInfo = new FileInfo(Path);
            Name = fileInfo.Name;
            Size = Math.Round(fileInfo.Length / 1000000.0, 3); // Converte em MB com 3 casas decimais

            ProcessStart($"send --no-qr \"{Path}\"");

            for (int i = 0; i < 4; i++) // Coleta o código e descarta o restante das linhas pois são inúteis
            {
                var line = _process.StandardError.ReadLine();
                if (i == 1)
                {
                    Code = line.Split(':')[1].Trim();
                }
            }
        }

        public async Task LoadSendFolderInfo()
        {
            ZipFile.CreateFromDirectory(Path, Path + ".zip"); // TO-DO: Trocar para criar em TEMP
            Path += ".zip";

            LoadSendInfo();
        }

        public void LoadReceiveInfo()
        {
            ProcessStart($"receive {Code}");

            var receivedInfo = _process.StandardError.ReadLine();

            Name = receivedInfo.Split(':')[1].Trim();
            Size = double.Parse(receivedInfo.Split('(')[1].Split(" ")[0]);
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
