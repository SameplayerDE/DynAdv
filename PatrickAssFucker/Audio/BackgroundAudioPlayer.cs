using NAudio.Wave;
using System;

namespace PatrickAssFucker.Audio
{
    public class BackgroundAudioPlayer : IDisposable
    {
        private WaveOutEvent _audioOutput;
        private AudioFileReader _audioFile;

        public void Play(string filePath, bool loop = true)
        {
            Stop();

            _audioFile = new AudioFileReader(filePath);
            _audioOutput = new WaveOutEvent();
            _audioOutput.Init(_audioFile);
            _audioOutput.Play();

            if (loop)
            {
                _audioOutput.PlaybackStopped += (s, e) =>
                {
                    _audioFile.Position = 0;
                    _audioOutput.Play();
                };
            }
        }

        public void Stop()
        {
            _audioOutput?.Stop();
            _audioFile?.Dispose();
            _audioOutput?.Dispose();
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
