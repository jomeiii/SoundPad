using NAudio.Wave;

namespace SoundPad;

public class SoundPlayer
{
    private AudioFileReader _audioFile;
    private WaveOutEvent _outputDevice;
    private Thread _playbackThread;
    public bool IsPlaying { get; private set; }

    public void PlaySound(string filePath)
    {
        if (IsPlaying)
            return;

        IsPlaying = true;
        _playbackThread = new Thread(() =>
        {
            try
            {
                using (_outputDevice = new WaveOutEvent())
                using (_audioFile = new AudioFileReader(filePath))
                {
                    _outputDevice.Init(_audioFile);
                    _outputDevice.Play();
                    while (_outputDevice.PlaybackState == PlaybackState.Playing) Thread.Sleep(100);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error playing sound: {ex.Message}");
            }
            finally
            {
                StopSound();
            }
        });

        _playbackThread.Start();
    }

    public void StopSound()
    {
        if (!IsPlaying)
            return;

        IsPlaying = false;
        if (_outputDevice != null)
        {
            _outputDevice.Stop();
            _outputDevice.Dispose();
        }

        if (_audioFile != null) _audioFile.Dispose();
    }
}