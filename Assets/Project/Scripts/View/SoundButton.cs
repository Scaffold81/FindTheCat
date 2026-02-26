using FindTheCat.Enums;
using FindTheCat.Service;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace FindTheCat.View
{
    [RequireComponent(typeof(Button))]
    public class SoundButton : MonoBehaviour
    {
        [SerializeField] private SoundType _soundType = SoundType.Click;

        private Button _button;
        private SoundService _soundService;

        [Inject]
        public void Construct(SoundService soundService)
        {
            _soundService = soundService;
        }

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            _soundService.Play(_soundType);
        }
    }
}
