using FindTheCat.Config;
using FindTheCat.Enums;
using UnityEngine;
using Zenject;

namespace FindTheCat.Service
{
    public class SoundService : IInitializable
    {
        private readonly SoundConfig _config;

        private AudioSource _sfxSource;
        private AudioSource _musicSource;

        [Inject]
        public SoundService(SoundConfig config)
        {
            _config = config;
        }

        public void Initialize()
        {
            var go = new GameObject("[SoundService]");
            Object.DontDestroyOnLoad(go);

            _sfxSource = go.AddComponent<AudioSource>();
            _sfxSource.playOnAwake = false;

            _musicSource = go.AddComponent<AudioSource>();
            _musicSource.playOnAwake = false;
            _musicSource.loop = true;

            PlayMusic();
        }

        public void Play(SoundType type)
        {
            switch (type)
            {
                case SoundType.Click:
                    PlayOneShot(_config.ClickClip, _config.ClickVolume);
                    break;

                case SoundType.Sound:
                    PlayOneShot(_config.SoundClip, _config.SoundVolume);
                    break;

                case SoundType.Music:
                    PlayMusic();
                    break;
            }
        }

        public void Stop(SoundType type)
        {
            if (type == SoundType.Music)
                _musicSource.Stop();
        }

        public void SetMusicVolume(float volume)
        {
            _musicSource.volume = Mathf.Clamp01(volume);
        }

        private void PlayOneShot(AudioClip clip, float volume)
        {
            if (clip == null) return;

            _sfxSource.PlayOneShot(clip, volume);
        }

        private void PlayMusic()
        {
            if (_config.MusicClip == null) return;

            if (_musicSource.isPlaying && _musicSource.clip == _config.MusicClip)
                return;

            _musicSource.clip = _config.MusicClip;
            _musicSource.volume = _config.MusicVolume;
            _musicSource.Play();
        }
    }
}
