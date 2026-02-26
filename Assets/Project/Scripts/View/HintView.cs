using System;
using FindTheCat.Enums;
using FindTheCat.Service;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace FindTheCat.View
{
    public class HintView : MonoBehaviour, IDisposable
    {
        [Header("UI")]
        [SerializeField] private Button _hintButton;
        [SerializeField] private TMP_Text _countLabel;

        [Header("Settings")]
        [SerializeField] private int _maxHints = 3;
        [SerializeField] private bool _limitedHints = true;

        private HintService _hintService;
        private SoundService _soundService;

        [Inject]
        public void Construct(HintService hintService, SoundService soundService)
        {
            _hintService = hintService;
            _soundService = soundService;
        }

        private int _hintsLeft;

        private void Start()
        {
            _hintsLeft = _maxHints;
            RefreshUI();
            _hintButton.onClick.AddListener(OnHintClicked);
        }

        private void OnHintClicked()
        {
            if (_limitedHints && _hintsLeft <= 0) return;

            _soundService.Play(SoundType.Click);

            bool shown = _hintService.ShowHint();

            if (shown && _limitedHints)
            {
                _hintsLeft--;
                RefreshUI();
            }
        }

        private void RefreshUI()
        {
            if (_countLabel != null)
                _countLabel.text = $"x{_hintsLeft}";

            if (_limitedHints)
                _hintButton.interactable = _hintsLeft > 0;
        }

        public void Dispose() { }

        private void OnDestroy()
        {
            _hintButton.onClick.RemoveListener(OnHintClicked);
        }
    }
}
