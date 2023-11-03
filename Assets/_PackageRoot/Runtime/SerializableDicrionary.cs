using System;
using System.Collections.Generic;
using UnityEngine;

namespace Viter.Dictionary
{
    [Serializable]
    public class SerializableDicrionary<TKey, TValue>
    {
        [SerializeField] private SerializablePair<TKey, TValue>[] pairs;
        private Dictionary<TKey, TValue> dict;
        public Dictionary<TKey, TValue> Dict
        {
            get
            {
                if (dict == null)
                {
                    dict = new Dictionary<TKey, TValue>();
                    for (int i = 0; i < pairs.Length; i++)
                    {
                        if (dict.ContainsKey(pairs[i].key))
                        {
                            continue;
                        }
                        dict.Add(pairs[i].key, pairs[i].value);
                    }
#if !UNITY_EDITOR
                    pairs = null;
#endif
                }
                return dict;
            }
        }

#if UNITY_EDITOR
        public void SetPairs_EDITOR_ONLY(TKey[] keys, TValue[] comps)
        {
            if (keys.Length != comps.Length)
            {
                return;
            }
            pairs = new SerializablePair<TKey, TValue>[keys.Length];
            for (int i = 0; i < keys.Length; i++)
            {
                pairs[i] = new SerializablePair<TKey, TValue>() { key = keys[i], value = comps[i] };
            }
        }

        public void SetPairs_EDITOR_ONLY<T>(ICollection<T> collection, Func<T, TKey> getKey, Func<T, TValue> getValue)
        {

            pairs = new SerializablePair<TKey, TValue>[collection.Count];
            int i = 0;
            foreach (var item in collection)
            {
                pairs[i] = new SerializablePair<TKey, TValue>()
                {
                    key = getKey.Invoke(item),
                    value = getValue.Invoke(item)
                };
                i++;
            }
        }

#endif
    }

    [Serializable]
    public class SerializablePair<TEnum, TComp>
    {
        public TEnum key;
        public TComp value;
    }
}
