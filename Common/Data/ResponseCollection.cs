using System;
using System.Collections.Generic;

namespace Common.Data
{
    [Serializable]
    public class ResponseCollection<T> : ResponseBase
    {
        private List<T> list = new List<T>();

        public IEnumerable<T> Collection
        {
            set
            {
                list = new List<T>(value);
            }
        }

        public T[] Array
        {
            get
            {
                return list.ToArray();
            }
        }

        public List<T> List
        {
            get
            {
                return list;
            }
        }

        public bool EqualsCollection(ResponseCollection<T> compareCollection, bool compareOrder = false)
        {
            if (Array.Length == compareCollection.Array.Length)
            {
                bool isResponseBaseType = (typeof(T).IsSubclassOf(typeof(ResponseBase)) || typeof(T) == typeof(ResponseBase));
                if (compareOrder)
                {
                    for (int i = 0; i < Array.Length; i++)
                    {
                        if (isResponseBaseType)
                        {
                            ResponseBase source = Array[i] as ResponseBase;
                            ResponseBase compare = compareCollection.Array[i] as ResponseBase;
                            if (source == null && compare == null)
                            {
                                continue;
                            }

                            if (source == null || compare == null)
                            {
                                return false;
                            }
                            return source.IsEqual(compare);
                        }

                        if (!Array[i].Equals(compareCollection.Array[i]))
                        {
                            return false;
                    }
                }
                    return true;
                }
                
                // Not comparing order
                    List<T> secondList = new List<T>(compareCollection.List);
                    foreach (T item in Array)
                    {
                        bool keepGoing = false;
                        for (int i = 0; i < secondList.Count; i++)
                        {
                            if (isResponseBaseType)
                            {
                                ResponseBase source = item as ResponseBase;
                                ResponseBase compare = secondList[i] as ResponseBase;
                                if (source == null && compare == null)
                                {
                                    secondList.RemoveAt(i);
                                    keepGoing = true;
                                    break;
                                }

                                if (source != null)
                                {
                                    if (source.IsEqual(compare))
                                    {
                                        secondList.RemoveAt(i);
                                        keepGoing = true;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                if (item.Equals(secondList[i]))
                                {
                                    secondList.RemoveAt(i);
                                    keepGoing = true;
                                    break;
                                }
                            }
                        }

                        if (keepGoing)
                        {
                            continue;
                        }
                        return false;
                    }
                }
            else
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// This compares two ResponseCollections where the order of the items in the collections matters
        /// </summary>
        /// <param name="compareCollection"></param>
        /// <param name="errorList"></param>
        /// <returns></returns>
        public bool EqualsCollection(ResponseCollection<T> compareCollection, out List<string> errorList)
        {
            errorList = new List<string>();
            if (Array.Length != compareCollection.Array.Length)
            {
                errorList.Add(string.Format("Collections have different counts; source {0} vs. compare {1}", Array.Length, compareCollection.Array.Length));
                return false;
            }

            bool retVal = true;
            bool isResponseBaseType = (typeof(T).IsSubclassOf(typeof(ResponseBase)) || typeof(T) == typeof(ResponseBase));
            for (int i = 0; i < Array.Length; i++)
            {
                if (isResponseBaseType)
                {
                    ResponseBase source = Array[i] as ResponseBase;
                    ResponseBase compare = compareCollection.Array[i] as ResponseBase;
                    if (source == null && compare == null)
                    {
                        continue;
                    }

                    if (source == null)
                    {
                        errorList.Add(string.Format("At index {0}, Source object is null", i));
                        retVal = false;
                    }
                    else if (compare == null)
                    {
                        errorList.Add(string.Format("At index {0}, Compare object is null", i));
                        retVal = false;
                    }
                    else
                    {
                        List<string> tmpList;
                        if (!source.IsEqual(compare, out tmpList))
                        {
                            errorList.Add(string.Format("Objects at index {0} are different:", i));
                            errorList.AddRange(tmpList);
                        }
                    }
                }
                else
                {
                    if (!Array[i].Equals(compareCollection.Array[i]))
                    {
                        errorList.Add(string.Format("Items at index {0} are not equal", i));
                        retVal = false;
                    }
                }
            }
            return retVal;
        }
    }
}
