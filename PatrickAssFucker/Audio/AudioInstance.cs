using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatrickAssFucker.Audio
{
    public class AudioInstance
    {
        private AudioFileReader _audioFile;
        private WaveOutEvent _outputDevice;
        
        public bool IsLooping { get; }
        public bool IsPlaying => _outputDevice.PlaybackState == PlaybackState.Playing;

        public float Volume;
        public bool OnLoop;

        public AudioInstance(string path, bool loop, float volume)
        {
            _audioFile = new AudioFileReader(path);
            _audioFile.Volume = volume;
            _outputDevice = new WaveOutEvent();

            OnLoop = loop;
            Volume = volume;

            _outputDevice.Init(_audioFile);

            IsLooping = loop;
        }

        public void Play()
        {
            _outputDevice.Play();
            if (OnLoop)
            {
                _outputDevice.PlaybackStopped += (s, e) =>
                {
                    _audioFile.Position = 0;
                    _outputDevice.Play();
                };
            }
        }

        public void Stop()
        {
            _outputDevice.Stop();
            _outputDevice.Dispose();
            _audioFile.Dispose();
        }

        public void SetVolume(float volume)
        {
            _audioFile.Volume = volume;
        }
    }
}
