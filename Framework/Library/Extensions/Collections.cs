using System;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Library.Extensions
{
    public static class Collections
    {
        public static List<T> AddDistinct<T>(this List<T> currentList, List<T> addList, IEqualityComparer<T> comparer = null)
        {
            foreach (T item in addList)
            {
                if (comparer != null)
                {
                    if (!currentList.Contains(item, comparer))
                    {
                        currentList.Add(item);
                    }
                }
                else
                {
                    if (!currentList.Contains(item))
                    {
                        currentList.Add(item);
                    }
                }
            }
            return currentList;
        }

        public static List<T> AddDistinct<T>(this List<T> currentList, T addItem, IEqualityComparer<T> comparer = null)
        {
            if (comparer != null)
            {
                if (!currentList.Contains(addItem, comparer))
                {
                    currentList.Add(addItem);
                }
            }
            else
            {
                if (!currentList.Contains(addItem))
                {
                    currentList.Add(addItem);
                }
            }
            return currentList;
        }

        public static T[] AddDistinct<T>(this T[] currentArray, T[] addArray, IEqualityComparer<T> comparer = null)
        {
            List<T> tmpList = new List<T>(currentArray);
            foreach (T item in addArray)
            {
                if (comparer != null)
                {
                    if (!tmpList.Contains(item, comparer))
                    {
                        tmpList.Add(item);
                    }
                }
                else
                {
                    if (!tmpList.Contains(item))
                    {
                        tmpList.Add(item);
                    }
                }
            }
            return tmpList.ToArray();
        }

        public static Dictionary<T1, T2> AddDistinct<T1, T2>(this Dictionary<T1, T2> dictionary, T1 key, T2 val)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, val);
            }
            return dictionary;
        }

        public static List<T> AddDistinctValues<T>(this List<T> currentList, params T[] array)
        {
            foreach (T item in array)
            {
                if (!currentList.Contains(item))
                {
                    currentList.Add(item);
                }
            }
            return currentList;
        }

        public static List<T> AddValues<T>(this List<T> currentList, params T[] array)
        {
            currentList.AddRange(array);
            return currentList;
        }

        public static T[] Concatinate<T>(this T[] array1, T[] array2)
        {
            if (array1 == null)
            {
                array1 = new T[0];
            }

            if (array2 == null)
            {
                array2 = new T[0];
            }

            T[] retVal = new T[array1.Length + array2.Length];
            array1.CopyTo(retVal, 0);
            array2.CopyTo(retVal, array1.Length);
            return retVal;
        }

        public static bool ContainsAny<T>(this IEnumerable<T> source, IEnumerable<T> collection)
        {
            foreach (T checkVal in collection)
            {
                foreach (T sourceVal in source)
                {
                    if (checkVal.Equals(sourceVal))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool ContainsAny(this IEnumerable<string> source, IEnumerable<string> collection, StringComparison stringComp = StringComparison.Ordinal)
        {
            foreach (string checkVal in collection)
            {
                foreach (string sourceVal in source)
                {
                    if (checkVal.Equals(sourceVal, stringComp))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Checks to see if the two collections have the same values but they can be in any order
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static bool EqualTo<T>(this IList<T> source, IList<T> collection)
        {
            if (source.Count != collection.Count)
            {
                return false;
            }

            // Have to create a new List otherwise you'll actually blow away the collection List if you just do "collection.RemoveAt"
            List<T> tmp = new List<T>(collection);
            foreach (T thing in source)
            {
                int index = -1;
                for (int i = 0; i < tmp.Count; i++)
                {
                    if (tmp[i].Equals(thing))
                    {
                        index = i;
                        break;
                    }
                }

                if (index > -1)
                {
                    tmp.RemoveAt(index);
                }
                else
                {
                    return false;
                }
            }
            return (tmp.Count == 0);
        }

        /// <summary>
        /// Checks to see if the two collections have the same values but they can be in any order
        /// </summary>
        /// <param name="source"></param>
        /// <param name="collection"></param>
        /// <param name="stringComp"></param>
        /// <returns></returns>
        public static bool EqualTo(this IList<string> source, IList<string> collection, StringComparison stringComp = StringComparison.Ordinal)
        {
            if (source.Count != collection.Count)
            {
                return false;
            }

            // Have to create a new List otherwise you'll actually blow away the collection List if you just do "collection.RemoveAt"
            List<string> tmp = new List<string>(collection);
            foreach (string thing in source)
            {
                int index = -1;
                for (int i = 0; i < tmp.Count; i++)
                {
                    if (tmp[i].Equals(thing, stringComp))
                    {
                        index = i;
                        break;
                    }
                }

                if (index > -1)
                {
                    tmp.RemoveAt(index);
                }
                else
                {
                    return false;
                }
            }
            return (tmp.Count == 0);
        }

        /// <summary>
        /// Checks to see if the two collections have the same values but they can be in any order
        /// </summary>
        /// <param name="source"></param>
        /// <param name="collection"></param>
        /// <param name="stringComp"></param>
        /// <returns></returns>
        public static bool EqualTo(this string[] source, string[] collection, StringComparison stringComp = StringComparison.Ordinal)
        {
            if (source.Length != collection.Length)
            {
                return false;
            }

            // Have to create a new List otherwise you'll actually blow away the collection List if you just do "collection.RemoveAt"
            // Of course, this was built from an array but still, I want to remove values at certain indexes so make a List
            List<string> tmp = new List<string>(collection);
            foreach (string thing in source)
            {
                int index = -1;
                for (int i = 0; i < tmp.Count; i++)
                {
                    if (tmp[i].Equals(thing, stringComp))
                    {
                        index = i;
                        break;
                    }
                }

                if (index > -1)
                {
                    tmp.RemoveAt(index);
                }
                else
                {
                    return false;
                }
            }
            return (tmp.Count == 0);
        }

        /// <summary>
        /// Returns all the things in firstList that do not exist in secondList
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="firstList"></param>
        /// <param name="secondList"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static List<T> Except<T>(this List<T> firstList, List<T> secondList, IEqualityComparer<T> comparer = null)
        {
            List<T> retVal = new List<T>();
            foreach (T item in firstList)
            {
                if (comparer != null)
                {
                    if (!secondList.Contains(item, comparer))
                    {
                        retVal.Add(item);
                    }
                }
                else
                {
                    if (!secondList.Contains(item))
                    {
                        retVal.Add(item);
                    }
                }
            }
            return retVal;
        }

        /// <summary>
        /// Returns all the things in firstArray that do not exist in secondArray
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="firstArray"></param>
        /// <param name="secondArray"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static T[] Except<T>(this T[] firstArray, T[] secondArray, IEqualityComparer<T> comparer = null)
        {
            List<T> retVal = new List<T>();
            foreach (T item in firstArray)
            {
                if (comparer != null)
                {
                    if (!secondArray.Contains(item, comparer))
                    {
                        retVal.Add(item);
                    }
                }
                else
                {
                    if (!secondArray.Contains(item))
                    {
                        retVal.Add(item);
                    }
                }

            }
            return retVal.ToArray();
        }

        public static List<T> SplitList<T>(this List<T> list, int after, out List<T> outList)
        {
            List<T> first = new List<T>(list.Take(after).ToList());
            outList = new List<T>(list.Skip(after).ToList());
            return first;
        }

        public static Tuple<List<T>, List<T>> SplitList<T>(this List<T> list, int after)
        {
            List<T> first = list.Take(after).ToList();
            List<T> second = list.Skip(after).ToList();
            return new Tuple<List<T>, List<T>>(first, second);
        }
    }
}