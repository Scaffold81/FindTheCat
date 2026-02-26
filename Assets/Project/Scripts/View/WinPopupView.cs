using System;
using FindTheCat.Enums;
using FindTheCat.Service;
using R3;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace FindTheCat.View
{
    public class WinPopupView : MonoBehaviour, IDisposable
    {
        [Header("UI")]
        [SerializeField] private GameObject _popupRoot;
        [SerializeField] private Button _restartButton;

        [Header("Particles")]
        [SerializeField] private ParticleSystem _winParticles;
        [SerializeField] private float _particlesDelay = 0.2f;

        private SearchableService _service;
        private SoundService _soundService;

        [Inject]
        public void Construct(SearchableService service, SoundService soundService)
        {
            _service = service;
            _soundService = soundService;
        }

        private readonly CompositeDisposable _disposables = new();

        private void Start()
        {
            _popupRoot.SetActive(false);

            _service.OnVictory
                    .Subscribe(_ => ShowPopup())
                    .AddTo(_disposables);

            _restartButton.onClick.AddListener(Restart);
        }

        private void ShowPopup()
        {
            _popupRoot.SetActive(true);

            _soundService.Stop(SoundType.Music);
            _soundService.Play(SoundType.Sound);

            PlayParticles();
        }

        private void PlayParticles()
        {
            if (_winParticles == null) return;

            Observable
                .Timer(TimeSpan.FromSeconds(_particlesDelay))
                .Subscribe(_ =>
                {
                    _winParticles.gameObject.SetActive(true);
                    _winParticles.Play();
                })
                .AddTo(_disposables);
        }

        private void Restart()
        {
            if (_winParticles != null)
            {
                _winParticles.Stop();
                _winParticles.Clear();
            }

            _soundService.Play(SoundType.Click);

            _service.Reset();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void Dispose() => _disposables.Dispose();

        private void OnDestroy()
        {
            _restartButton.onClick.RemoveListener(Restart);
            Dispose();
        }
    }
}
