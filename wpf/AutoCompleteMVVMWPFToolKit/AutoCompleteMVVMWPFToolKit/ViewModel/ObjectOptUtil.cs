using System;

namespace AutoCompleteMVVMWPFToolKit.ViewModel
{
    static class ObjectOptUtil
    {
        public static T ConvObjToT<T>(object obj)
        {
            switch (typeof(T).ToString())
            {
                case "System.Int64":
                    return (T)Convert.ChangeType(obj, TypeCode.Int64);
                case "System.Int16":
                    return (T)Convert.ChangeType(obj, TypeCode.Int16);
                case "System.Byte":
                    return (T)Convert.ChangeType(obj, TypeCode.Byte);
                default:
                    return (T)obj;
            }
        }
    }
}
