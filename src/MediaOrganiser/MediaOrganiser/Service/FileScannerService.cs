﻿using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MediaOrganiser.Data;
using MediaOrganiser.Extensions;
using MediaOrganiser.Messages;

namespace MediaOrganiser.Service
{
    public class FileScannerService
    {
        private readonly DataRepository _repo;

        public FileScannerService()
        {
            _repo = new DataRepository();
        }

        public async Task StartScanAsync()
        {
            MessengerService.Default.Send(new FileScanStartedMessage());

            var library = _repo.SelectLibrary();

            if (!string.IsNullOrEmpty(library))
            {
                var allFiles = Directory.GetFiles(library, "*.*", SearchOption.AllDirectories);

                await Task.Run(() => _repo.ReplaceAllAudioFiles(allFiles.Where(x => x.PathIsAudioFile()).ToList()));

                await Task.Run(() => _repo.ReplaceAllVideoFiles(allFiles.Where(x => x.PathIsVideoFile()).ToList()));
            }

            MessengerService.Default.Send(new FileScanCompleteMessage(), MessageContexts.FileScanComplete);
        }

        public void StartScan()
        {
            MessengerService.Default.Send(new FileScanStartedMessage());

            var library = _repo.SelectLibrary();

            if (!string.IsNullOrEmpty(library))
            {
                var allFiles = Directory.GetFiles(library, "*.*", SearchOption.AllDirectories);

                _repo.ReplaceAllAudioFiles(allFiles.Where(x => x.PathIsAudioFile()).ToList());

                _repo.ReplaceAllVideoFiles(allFiles.Where(x => x.PathIsVideoFile()).ToList());
            }

            MessengerService.Default.Send(new FileScanCompleteMessage(), MessageContexts.FileScanComplete);
        }
    }
}
