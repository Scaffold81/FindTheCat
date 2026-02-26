using System;
using System.Collections.Generic;
using FindTheCat.Searchable;
using UnityEngine;

namespace FindTheCat.Config
{
    [CreateAssetMenu(fileName = "SearchableConfig", menuName = "FindTheCat/SearchableConfig")]
    public class SearchableConfig : ScriptableObject
    {
        [SerializeField] private Entry[] _entries;

        public IReadOnlyDictionary<SearchableType, int> BuildMaxCount()
        {
            var dict = new Dictionary<SearchableType, int>(_entries.Length);

            foreach (var entry in _entries)
            {
                if (dict.ContainsKey(entry.Type))
                    continue;

                dict[entry.Type] = entry.MaxCount;
            }

            return dict;
        }

        [Serializable]
        private struct Entry
        {
            public SearchableType Type;

            [Min(1)]
            public int MaxCount;
        }
    }
}
