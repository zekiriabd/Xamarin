
using Xamarin.Forms;
using Mp3Demo.Droid;
using AVFoundation;
using Foundation;
using System.IO;

[assembly: Dependency(typeof(AudioRender))]

namespace Mp3Demo.Droid
{
    public class AudioRender : IAudioService {
        public void PlayAudioFile(string fileName)
        {
            NSError err;
            var player = new AVAudioPlayer(new NSUrl(fileName), "MP3", out err);
            player.FinishedPlaying += delegate
            {
                player = null;
            };
            player.Play();
        }
        }
    }