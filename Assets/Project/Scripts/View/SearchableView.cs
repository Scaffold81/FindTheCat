using System;
using FindTheCat.Searchable;
using FindTheCat.Service;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace FindTheCat.View
{
    public class SearchableView : MonoBehaviour, IDisposable
    {
        [SerializeField] private CounterEntry[] _entries;

        private SearchableService _service;

        [Inject]
        public void Construct(SearchableService service)
        {
            _service = service;
        }

        private readonly CompositeDisposable _disposables = new();

        private void Start()
        {
            RefreshAll();

            _service.OnFound
                    .Subscribe(Refresh)
                    .AddTo(_disposables);
        }

        private void RefreshAll()
        {
            foreach (var entry in _entries)
                Refresh(entry.Type);
        }

        private void Refresh(SearchableType type)
        {
            foreach (var entry in _entries)
            {
                if (entry.Type != type) continue;

                entry.Label.text = $"{_service.GetCurrent(type)}/{_service.GetMax(type)}";
                break;
            }
        }

        public void Dispose() => _disposables.Dispose();

        private void OnDestroy() => Dispose();

        [Serializable]
        private struct CounterEntry
        {
            public SearchableType Type;
            public Image          Icon;
            public TMP_Text       Label;
        }
    }
}
