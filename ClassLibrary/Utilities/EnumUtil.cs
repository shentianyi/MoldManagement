using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using ClassLibrary.ENUM;

namespace ClassLibrary.Utilities
{
    public class EnumUtil
    {
        /// <summary>
        /// get the enum Desription by the enum value
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetEnumDescriptionByEnumValue(Enum enumValue)
        {
            string description = string.Empty;
            FieldInfo info = enumValue.GetType().GetField(enumValue.ToString());
            DescriptionAttribute[] attributes = null;
            try
            {
                attributes = info.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
            }
            catch { }
            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return enumValue.ToString();
        }

        /// <summary>
        /// get the enum list
        /// </summary>
        /// <param name="enumType">the type of enum</param>
        /// <returns>the list of enum</returns>
        public static List<EnumItem> GetEnumItemList(string _enumType)
        {
            string type = _enumType.Substring(_enumType.LastIndexOf('.')+1, _enumType.Length - _enumType.LastIndexOf('.')-1);
            Type enumType = Type.GetType("ClassLibrary.ENUM."+type);
            if (!enumType.IsEnum)
                return null;
            Type descriptionType = typeof(DescriptionAttribute);
            FieldInfo[] infos = enumType.GetFields();
            List<EnumItem> items = new List<EnumItem>();

            string _value = string.Empty;
            string _description = string.Empty;
            foreach (FieldInfo info in infos)
            {
                if (info.IsSpecialName)
                    continue;
                _value = info.GetRawConstantValue().ToString();
                DescriptionAttribute[] attributes = info.GetCustomAttributes(descriptionType, true) as DescriptionAttribute[];
                if (attributes != null && attributes.Length > 0)
                {
                    _description = attributes[0].Description;
                    items.Add(new EnumItem() { Value = _value, Description = _description });
                }
            }
            return items;

        }
    }
}
