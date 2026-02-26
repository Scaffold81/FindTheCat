using System.Collections;
using FindTheCat.Enums;
using FindTheCat.Service;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace FindTheCat.Searchable
{
    [RequireComponent(typeof(Image))]
    public abstract class SearchableObject : MonoBehaviour, IPointerClickHandler
    {
        [Header("Searchable")]
        [SerializeField] private SearchableType _type;
        [SerializeField] private Color _foundColor = Color.green;

        [Header("Sound")]
        [SerializeField] private SoundType _foundSoundType = SoundType.Sound;

        [Header("Hint")]
        [SerializeField] private Color _hintColor = Color.yellow;
        [SerializeField] private float _hintDuration = 1.5f;

        private SearchableService _searchableService;
        private HintService _hintService;
        private SoundService _soundService;

        [Inject]
        public void Construct(
            SearchableService searchableService,
            HintService hintService,
            SoundService soundService)
        {
            _searchableService = searchableService;
            _hintService = hintService;
            _soundService = soundService;
        }

        private Image _image;
        private Color _originalColor;
        private bool _isFound;

        protected virtual void Awake()
        {
            _image = GetComponent<Image>();
            _originalColor = _image.color;

            _hintService.Register(this);
        }

        private void OnDestroy()
        {
            _hintService?.Unregister(this);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_isFound) return;

            _isFound = true;
            _image.color = _foundColor;

            _soundService.Play(_foundSoundType);
            _searchableService.ReportFound(_type);
            OnFound();
        }

        public void ShowHint()
        {
            if (_isFound) return;
            StopAllCoroutines();
            StartCoroutine(HintCoroutine());
        }

        private IEnumerator HintCoroutine()
        {
            float elapsed = 0f;
            float interval = 0.2f;
            bool toggle = true;

            while (elapsed < _hintDuration)
            {
                _image.color = toggle ? _hintColor : _originalColor;
                toggle = !toggle;
                elapsed += interval;
                yield return new WaitForSeconds(interval);
            }

            _image.color = _originalColor;
        }

        protected virtual void OnFound() { }

        public SearchableType Type => _type;
        public bool IsFound => _isFound;
    }
}
