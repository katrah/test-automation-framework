using System.Collections.Generic;
using Framework.Library;
using Reqnroll;

namespace Automation.Common.Reqnroll
{
    [Binding]
    public class Storage
    {
        private const string keyTemplate = "{0}|{1}|{2}";
        private static volatile Storage instance;
        private static readonly object syncRoot = new object();
        private FeatureContext ftCtxt { get; set; }
        private ScenarioContext scnCtxt { get; set; }

        public Storage(FeatureContext featureContext)
        {
            ftCtxt = featureContext;
        }

        public Storage(ScenarioContext scenarioContext)
        {
            scnCtxt = scenarioContext;
        }

        public static Storage GetInstance()
        {
            lock (syncRoot)
            {
                if(instance == null)
                {
                    throw new System.Exception("No instance was created in Hooks.cs, how did you get here?");
                }
            }
            return instance;
        }

        public static Storage GetInstance(FeatureContext featureContext)
        {
            lock (syncRoot) 
            {
                if (instance == null)
                {
                    instance = new Storage(featureContext);
                }
                if (instance.ftCtxt == null)
                {
                    instance.ftCtxt = featureContext;
                }
            }
            return instance;
        }

        public static Storage GetInstance(ScenarioContext scenarioContext)
        {
            lock (syncRoot)
            {
                if (instance == null)
                {
                    instance = new Storage(scenarioContext);
                }
                if (instance.scnCtxt == null)
                {
                    instance.scnCtxt = scenarioContext;
                } 
            }
            return instance;
        }

        public void DisposeFeatureContext() 
        {
            ftCtxt = null;
        }

        public void DisposeScenarioContext()
        {
            scnCtxt = null;
        }

        #region Basic Accessors
        /// <summary>
        /// Adds the supplied value to the List for the given Key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val"></param>
        /// <param name="key"></param>
        /// <param name="subKey"></param>
        public static void AddValue<T>(T val, string key, string subKey = "")
        {
            key = string.Format(keyTemplate, key, subKey, typeof(T));
            List<T> tmp = new List<T>();
            if (ScenarioContext.Current.ContainsKey(key))
            {
                tmp = (List<T>)ScenarioContext.Current[key];
            }
            tmp.Add(val);
            ScenarioContext.Current.Set(tmp, key);
        }

        /// <summary>
        /// Adds a range of values from the supplied List to the List for the given Key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val"></param>
        /// <param name="key"></param>
        /// <param name="subKey"></param>
        public void AddValues<T>(List<T> val, string key, string subKey = "")
        {
            key = string.Format(keyTemplate, key, subKey, typeof(T));
            List<T> tmp = new List<T>();
            if (scnCtxt.ContainsKey(key))
            {
                tmp = (List<T>)scnCtxt[key];
            }
            tmp.AddRange(val);
            scnCtxt.Set(tmp, key);
        }

        public void ClearValues<T>(string key, string subKey = "")
        {
            key = string.Format(keyTemplate, key, subKey, typeof(T));
            scnCtxt.Remove(key);
        }

        /// <summary>
        /// Returns a List of type T for the given Key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="subKey"></param>
        /// <returns></returns>
        public List<T> GetValues<T>(string key, string subKey = "")
        {
            List<T> retVal;
            key = string.Format(keyTemplate, key, subKey, typeof(T));
            scnCtxt.TryGetValue(key, out retVal);
            if (retVal == null)
            {
                retVal = new List<T>();
            }
            return retVal;
        }

        /// <summary>
        /// Returns a List of type T when the given Key and data type are used in the Dictionary.Key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<T> GetFuzzyValues<T>(string key)
        {
            List<T> retVal = new List<T>();
            foreach (KeyValuePair<string, object> kvp in scnCtxt)
            {
                if (kvp.Key.StartsWith(string.Format("{0}|", key)) && kvp.Key.EndsWith(string.Format("|{0}", typeof(T))))
                {
                    retVal.AddRange(kvp.Value as List<T>);
                }
            }
            return retVal;
        }

        public T GetCurrent<T>(string key, string subKey = "")
        {
            T obj;
            GetCurrent(key, out obj, subKey);
            return obj;
        }

        public bool GetCurrent<T>(string key, out T obj, string subKey = "")
        {
            // If subKey is provided, try that first
            if (!string.IsNullOrEmpty(subKey))
            {
                List<T> list = GetValues<T>(key, subKey);
                if (list != null && list.Count > 0)
                {
                    obj = list[list.Count - 1];
                    return true;
                }

                list = GetFuzzyValues<T>(key);
                if (list != null && list.Count > 0)
                {
                    obj = list[list.Count - 1];
                    return true;
                }
                obj = Default<T>.Value;
            }
            else
            {
                List<T> list = GetFuzzyValues<T>(key);
                if (list != null && list.Count > 0)
                {
                    obj = list[list.Count - 1];
                    return true;
                }

                list = GetValues<T>(key, subKey);
                if (list != null && list.Count > 0)
                {
                    obj = list[list.Count - 1];
                    return true;
                }
                obj = Default<T>.Value;
            }
            return false;
        }

        public T GetFirst<T>(string key, string subKey = "")
        {
            List<T> list = GetValues<T>(key, subKey);
            if (list != null && list.Count > 0)
            {
                return list[0];
            }

            if (typeof(T).IsClass)
            {
                return (T)(new object());
            }
            return default(T);
        }

        public bool GetFirst<T>(string key, out T obj, string subKey = "")
        {
            List<T> list = GetValues<T>(key, subKey);
            if (list != null && list.Count > 0)
            {
                obj = list[0];
                return true;
            }

            if (typeof(T).IsClass)
            {
                obj = (T)(new object());
            }
            else
            {
                obj = default(T);
            }
            return false;
        }

        /// <summary>
        /// Replaces the List entirely for the given Key with a new List containing the supplied value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val"></param>
        /// <param name="key"></param>
        /// <param name="subKey"></param>
        public void SetValue<T>(T val, string key, string subKey = "")
        {
            key = string.Format(keyTemplate, key, subKey, typeof(T));
            List<T> tmp = new List<T> { val };
            scnCtxt.Set(tmp, key);
        }

        /// <summary>
        /// Replaces the List entirely for the given Key with the supplied List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val"></param>
        /// <param name="key"></param>
        /// <param name="subKey"></param>
        public void SetValues<T>(List<T> val, string key, string subKey = "")
        {
            key = string.Format(keyTemplate, key, subKey, typeof(T));
            List<T> tmp = new List<T>(val);
            scnCtxt.Set(tmp, key);
        }
        #endregion

        #region Advanced
        /// <summary>
        /// Returns the current dialog but doesn't look for the Type since that can change.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /*
        public static T GetCurrentDialog<T>()
        {
            foreach (KeyValuePair<string, object> kvp in ScenarioContext.Current)
            {
                if (kvp.Key.StartsWith(string.Format("{0}|{1}", StringConstants.CurrentDialog, string.Empty)))
                {
                    if (kvp.Value != null)
                    {
                        List<object> tmp = kvp.Value as List<object>;
                        if (tmp == null)
                        {
                            tmp = new List<object>() { kvp.Value };
                        }
                        dynamic x = tmp[0];
                        return (T)x[0];
                    }
                    return Default<T>.Value;
                }
            }
            return Default<T>.Value;
        }
        */

        ///// <summary>
        ///// Gets the CustomerInstanceObject for a given shortcode (instance).  If the CustomerInstanceObject is not in
        ///// Storage already, this will get it from Jobs.GetCustomerInstanceObject, store a copy in Storage, and return
        ///// a copy.
        ///// </summary>
        ///// <param name="subKey"></param>
        ///// <returns></returns>
        //public static CustomerInstanceObject GetCustomerInstanceObject(string subKey)
        //{
        //    List<CustomerInstanceObject> cioList = GetValues<CustomerInstanceObject>(StringConstants.CustomerInstanceObject, subKey);
        //    if (cioList != null && cioList.Count > 0)
        //    {
        //        return cioList[0];
        //    }
        //    InstanceResponse cir = Jobs.GetCustomerInstanceObject(subKey);
        //    AddValue(cir.CustomerInstanceObject, StringConstants.CustomerInstanceObject, subKey);
        //    return cir.CustomerInstanceObject;
        //}

        /// <summary>
        /// This returns a dynamic rather than the type of object stored because the type is not known when you are trying to
        /// get the object out.  Note, this gets the very first match in Storage that matches the key and subKey.  Be careful.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="subKey"></param>
        /// <returns></returns>
        public bool GetCurrentObject(string key, out dynamic obj, string subKey = "")
        {
            obj = null;
            foreach (KeyValuePair<string, object> kvp in ScenarioContext.Current)
            {
                if (kvp.Key.StartsWith(string.Format("{0}|{1}", key, subKey)))
                {
                    if (kvp.Value != null)
                    {
                        List<object> tmp = kvp.Value as List<object>;
                        if (tmp == null)
                        {
                            tmp = new List<object>() { kvp.Value };
                        }
                        dynamic x = tmp[0];
                        obj = x[0];
                        return true;
                    }
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// This returns a dynamic rather than the type of object stored because the type is not known when you are trying to
        /// get the object out.  Note, this gets the very first match in Storage that matches the key and subKey.  Be careful.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="subKey"></param>
        /// <returns></returns>
        public dynamic GetCurrentObject(string key, string subKey = "")
        {
            dynamic obj;
            GetCurrentObject(key, out obj, subKey);
            return obj;
        }
        #endregion

        #region Global (Feature) Storage
        public WebTestMethodType GetWebTestMethod()
        {
            return (WebTestMethodType)ftCtxt[StringConstants.WebTestMethod];
        }

        public void SetWebTestMethod(WebTestMethodType testMethodType)
        {
            ftCtxt[StringConstants.WebTestMethod] = testMethodType;
        }
        #endregion

    }
}