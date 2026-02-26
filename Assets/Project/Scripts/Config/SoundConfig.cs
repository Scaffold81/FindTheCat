using UnityEngine;

namespace FindTheCat.Config
{
    [CreateAssetMenu(fileName = "SoundConfig", menuName = "FindTheCat/SoundConfig")]
    public class SoundConfig : ScriptableObject
    {
        [Header("Click")]
        [SerializeField] private AudioClip _clickClip;
        [SerializeField] [Range(0f, 1f)] private float _clickVolume = 1f;

        [Header("Sound")]
        [SerializeField] private AudioClip _soundClip;
        [SerializeField] [Range(0f, 1f)] private float _soundVolume = 1f;

        [Header("Music")]
        [SerializeField] private AudioClip _musicClip;
        [SerializeField] [Range(0f, 1f)] private float _musicVolume = 0.5f;

        public AudioClip ClickClip   => _clickClip;
        public float     ClickVolume => _clickVolume;

        public AudioClip SoundClip   => _soundClip;
        public float     SoundVolume => _soundVolume;

        public AudioClip MusicClip   => _musicClip;
        public float     MusicVolume => _musicVolume;
    }
}
