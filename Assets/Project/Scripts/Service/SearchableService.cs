using System;
using System.Collections.Generic;
using System.Linq;
using FindTheCat.Config;
using FindTheCat.Searchable;
using R3;
using Zenject;

namespace FindTheCat.Service
{
    public class SearchableService : IInitializable, IDisposable
    {
        private readonly SearchableConfig _config;

        private Dictionary<SearchableType, int> _currentCount;
        private Dictionary<SearchableType, int> _maxCount;

        private readonly Subject<SearchableType> _onFound = new();
        public Observable<SearchableType> OnFound => _onFound;

        private readonly Subject<Unit> _onVictory = new();
        public Observable<Unit> OnVictory => _onVictory;

        [Inject]
        public SearchableService(SearchableConfig config)
        {
            _config = config;
        }

        public void Initialize() => Reset();

        public void ReportFound(SearchableType type)
        {
            if (!_currentCount.ContainsKey(type)) return;
            if (_currentCount[type] >= _maxCount[type]) return;

            _currentCount[type]++;

            _onFound.OnNext(type);

            if (IsAllComplete())
                _onVictory.OnNext(Unit.Default);
        }

        public void Reset()
        {
            _maxCount = new Dictionary<SearchableType, int>(_config.BuildMaxCount());
            _currentCount = new Dictionary<SearchableType, int>();

            foreach (var key in _maxCount.Keys)
                _currentCount[key] = 0;
        }

        public int GetRemaining(SearchableType type) =>
            _maxCount.GetValueOrDefault(type, 0) - _currentCount.GetValueOrDefault(type, 0);

        public int GetCurrent(SearchableType type) =>
            _currentCount.GetValueOrDefault(type, 0);

        public int GetMax(SearchableType type) =>
            _maxCount.GetValueOrDefault(type, 0);

        public bool IsComplete(SearchableType type) =>
            GetRemaining(type) <= 0;

        public bool IsAllComplete() =>
            _maxCount.Keys.All(IsComplete);

        public void Dispose()
        {
            _onFound.Dispose();
            _onVictory.Dispose();
        }
    }
}
