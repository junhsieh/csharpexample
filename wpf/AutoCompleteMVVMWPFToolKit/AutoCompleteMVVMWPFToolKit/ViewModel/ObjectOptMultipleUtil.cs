using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AutoCompleteMVVMWPFToolKit.ViewModel
{
    static class ObjectOptMultipleUtil
    {
        public static List<T> GetID<T>(ObservableCollection<ObjectOpt> optArr)
        {
            List<T> IDArr = optArr.Where((d) => d.IsSelected == true).Select((d) => ObjectOptUtil.ConvObjToT<T>(d.ID)).ToList<T>();

            // NOTE: it is very important to return null if IDArr.Count is zero. Otherwise, the SetID will not be triggered.
            return IDArr.Count == 0 ? null : IDArr.Cast<T>().ToList();
        }

        public static void SetID<T>(ObservableCollection<ObjectOpt> optArr, List<object> value)
        {
            if (value != null)
            {
                // reset IsSelected to false
                optArr.ToList().ForEach((d) => d.IsSelected = false);

                //
                optArr.Where((d) => value.Contains(ObjectOptUtil.ConvObjToT<T>(d.ID))).ToList().ForEach((d) => d.IsSelected = true);
            }
        }

        public static List<string> GetText(ObservableCollection<ObjectOpt> optArr)
        {
            List<string> strArr = new List<string>();

            if (optArr != null)
            {
                strArr = optArr.Where((d) => d.IsSelected == true).Select((d) => d.Text).ToList<string>();
            }

            return strArr;
        }

        /*
        public static void RemoveOptArr(ObservableCollection<ObjectOpt> optArr)
        {
            ObservableCollection<ObjectOpt> tmpArr = new ObservableCollection<ObjectOpt>(optArr.Where(d => d.IsSelected == false));

            foreach (var item in tmpArr)
            {
                optArr.Remove(item);
            }
        }
        */
    }
}
