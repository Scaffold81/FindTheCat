using System;
using System.Collections.Generic;
using System.Linq;
using FindTheCat.Searchable;
using R3;

namespace FindTheCat.Service
{
    public class HintService : IDisposable
    {
        private readonly List<SearchableObject> _objects = new();

        private readonly Subject<Unit> _onHintsExhausted = new();
        public Observable<Unit> OnHintsExhausted => _onHintsExhausted;

        public void Register(SearchableObject obj)
        {
            if (!_objects.Contains(obj))
                _objects.Add(obj);
        }

        public void Unregister(SearchableObject obj)
        {
            _objects.Remove(obj);
        }

        public bool ShowHint()
        {
            var unfound = _objects.Where(o => !o.IsFound).ToList();

            if (unfound.Count == 0)
            {
                _onHintsExhausted.OnNext(Unit.Default);
                return false;
            }

            var target = unfound[UnityEngine.Random.Range(0, unfound.Count)];
            target.ShowHint();

            return true;
        }

        public void Dispose() => _onHintsExhausted.Dispose();
    }
}
