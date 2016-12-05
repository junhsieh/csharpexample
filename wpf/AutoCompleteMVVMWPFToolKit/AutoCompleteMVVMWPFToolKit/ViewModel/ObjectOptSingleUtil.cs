using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace AutoCompleteMVVMWPFToolKit.ViewModel
{
    static class ObjectOptSingleUtil
    {
        public static T GetID<T>(ObservableCollection<ObjectOpt> optArr)
        {
            return optArr.Where((d) => d.IsSelected == true).Select((d) => ObjectOptUtil.ConvObjToT<T>(d.ID)).FirstOrDefault<T>();

            /*
            var query = optArr.Where((d) => d.IsSelected == true);

            if (query.Any() == true)
            {
                return query.Select((d) => ObjectOptUtil.ConvObjToT<T>(d.ID)).FirstOrDefault<T>();
            }
            else
            {
                return default(T);
            }
            */
        }

        public static void SetID<T>(ObservableCollection<ObjectOpt> optArr, object value)
        {
            // reset IsSelected to false
            optArr.ToList().ForEach(d => d.IsSelected = false);

            //
            optArr.Where((d) => ObjectOptUtil.ConvObjToT<T>(d.ID).Equals(value)).Skip(0).Take(1).ToList().ForEach((d) => d.IsSelected = true);
        }

        public static string GetText(ObservableCollection<ObjectOpt> optArr)
        {
            string str = "";

            if (optArr != null)
            {
                str = optArr.Where(d => d.IsSelected == true).Select(d => d.Text).Skip(0).Take(1).FirstOrDefault<string>();
            }

            return str;
        }
    }
}
