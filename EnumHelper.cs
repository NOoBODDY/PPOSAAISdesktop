using System;
using System.ComponentModel;
using System.Reflection;


namespace ClientDB
{
    public static class EnumHelper
    {

        public static string GetDescription(Enum enumElement)
        {
            Type type = enumElement.GetType();

            MemberInfo[] memInfo = type.GetMember(enumElement.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }

            return enumElement.ToString();
        }

        public static Enum GetEnum(string description , Type type) 
        {
            foreach (Enum one in Enum.GetValues(type))
            {
                if (description == Enum.GetName(type, one))
                    return one;
            }
            return default;
        }
    }
}
