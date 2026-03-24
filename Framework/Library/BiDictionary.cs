using System.Collections;
using System.Collections.Generic;

namespace Framework.Library
{
    public class BiDictionary<TKey, TValue> : IEnumerable<TKey>
    {
        private readonly IDictionary<TKey, IList<TValue>> firstToSecond = new Dictionary<TKey, IList<TValue>>();
        private readonly IDictionary<TValue, IList<TKey>> secondToFirst = new Dictionary<TValue, IList<TKey>>();

        private static readonly IList<TKey> emptyFirstList = new TKey[0];
        private static readonly IList<TValue> emptySecondList = new TValue[0];

        public BiDictionary()
        {
        }

        public BiDictionary(IEqualityComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
        {
            firstToSecond = new Dictionary<TKey, IList<TValue>>(keyComparer);
            secondToFirst = new Dictionary<TValue, IList<TKey>>(valueComparer);
        }

        public void Add(TKey first, TValue second)
        {
            IList<TKey> firsts;
            IList<TValue> seconds;
            if (!firstToSecond.TryGetValue(first, out seconds))
            {
                seconds = new List<TValue>();
                firstToSecond[first] = seconds;
            }
            if (!secondToFirst.TryGetValue(second, out firsts))
            {
                firsts = new List<TKey>();
                secondToFirst[second] = firsts;
            }
            seconds.Add(second);
            firsts.Add(first);
        }

        public IList<TKey> Keys
        {
            get
            {
                IList<TKey> keys = new List<TKey>();
                foreach (KeyValuePair<TKey, IList<TValue>> kvp in firstToSecond)
                {
                    if (!keys.Contains(kvp.Key))
                    {
                        keys.Add(kvp.Key);
                    }
                }
                return keys;
            }
        }

        public IList<TValue> Values
        {
            get
            {
                IList<TValue> vals = new List<TValue>();
                foreach (KeyValuePair<TValue, IList<TKey>> kvp in secondToFirst)
                {
                    if (!vals.Contains(kvp.Key))
                    {
                        vals.Add(kvp.Key);
                    }
                }
                return vals;
            }
        }

        /// <summary>
        /// Note potential ambiguity using indexers (if both the Key and Value datatypes are the same)
        /// Hence the methods as well...
        /// </summary>
        /// <param name="first"></param>
        /// <returns></returns>
        public IList<TValue> this[TKey first]
        {
            get
            {
                return GetValue(first);
            }
        }

        /// <summary>
        /// Note potential ambiguity using indexers (if both the Key and Value datatypes are the same)
        /// Hence the methods as well...
        /// </summary>
        /// <param name="second"></param>
        /// <returns></returns>
        public IList<TKey> this[TValue second]
        {
            get
            {
                return GetKey(second);
            }
        }

        public IList<TValue> GetValue(TKey first)
        {
            IList<TValue> list;
            if (!firstToSecond.TryGetValue(first, out list))
            {
                return emptySecondList;
            }
            return new List<TValue>(list); // Create a copy for sanity
        }

        public IList<TKey> GetKey(TValue second)
        {
            IList<TKey> list;
            if (!secondToFirst.TryGetValue(second, out list))
            {
                return emptyFirstList;
            }
            return new List<TKey>(list); // Create a copy for sanity
        }

        /// <summary>
        /// The number of pairs stored in the dictionary
        /// </summary>
        public int Count
        {
            get
            {
                int counter = 0;
                foreach (KeyValuePair<TKey, IList<TValue>> firstKvp in firstToSecond)
                {
                    counter += firstKvp.Value.Count;
                }
                return counter;
            }
        }

        /// <summary>
        /// The number of Keys in the dictionary
        /// </summary>
        public int CountKeys
        {
            get
            {
                return firstToSecond.Count;
            }
        }

        /// <summary>
        /// The number of Values in the dictionary
        /// </summary>
        public int CountValues
        {
            get
            {
                return secondToFirst.Count;
            }
        }

        /// <summary>
        /// Removes all items from the BiDictionary.
        /// </summary>
        public void Clear()
        {
            firstToSecond.Clear();
            secondToFirst.Clear();
        }

        public void RemoveByKey(TKey first)
        {
            IList<TValue> seconds;
            if (firstToSecond.TryGetValue(first, out seconds))
            {
                foreach (TValue secondVal in seconds)
                {
                    IList<TKey> tmpFirsts;
                    if (secondToFirst.TryGetValue(secondVal, out tmpFirsts))
                    {
                        if (tmpFirsts.Contains(first))
                        {
                            tmpFirsts.Remove(first);
                        }
                    }

                    if (tmpFirsts != null && tmpFirsts.Count > 0)
                    {
                        secondToFirst[secondVal] = tmpFirsts;
                    }
                    else
                    {
                        secondToFirst.Remove(secondVal);
                    }
                }
            }
            firstToSecond.Remove(first);
        }

        public void RemoveByValue(TValue second)
        {
            IList<TKey> firsts;
            if (secondToFirst.TryGetValue(second, out firsts))
            {
                foreach (TKey firstVal in firsts)
                {
                    IList<TValue> tmpSeconds;
                    if (firstToSecond.TryGetValue(firstVal, out tmpSeconds))
                    {
                        if (tmpSeconds.Contains(second))
                        {
                            tmpSeconds.Remove(second);
                        }
                    }

                    if (tmpSeconds != null && tmpSeconds.Count > 0)
                    {
                        firstToSecond[firstVal] = tmpSeconds;
                    }
                    else
                    {
                        firstToSecond.Remove(firstVal);
                    }
                }
            }
            secondToFirst.Remove(second);
        }

        public IEnumerator<TKey> GetEnumerator()
        {
            return firstToSecond.Keys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return firstToSecond.Values.GetEnumerator();
        }
    }
}
