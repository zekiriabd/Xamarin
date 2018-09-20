
using Xamarin.Forms;
using Windows.Media.Playback;
using Windows.ApplicationModel;
using Windows.Storage;
using System;
using Windows.Media.Core;
using Mp3Demo.UWP;

[assembly: Dependency(typeof(AudioRender))]

namespace Mp3Demo.UWP
{
    public class AudioRender : IAudioService
    {
        public async void PlayAudioFile(string fileName)
        {
            StorageFolder AssetsFolder = await Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
            StorageFile file = await AssetsFolder.GetFileAsync(fileName);

            var player = new MediaPlayer() { AutoPlay = false, Source = MediaSource.CreateFromStorageFile(file) };
            player.Play();
        }
    }
}