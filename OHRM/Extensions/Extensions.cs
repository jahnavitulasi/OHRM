using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.ComponentModel;

namespace OrangeHRM.Automation.Framework.Extensions
{
    public class Extensions
    {
        public static string GetEnumDescription<T>(T value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute),false);
            if(attributes!= null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }

        public static T ParseDescriptionToEnum <T>(string description)
        {
            Array array= Enum.GetValues(typeof(T));
            List<T> list= new List<T>(array.Length);
            for(int i=0;i<array.Length; i++)
            {
                list.Add((T)array.GetValue(i));
            }
            Dictionary<string,T> dict = list.Select(v=> new {Value = v,Description = GetEnumDescription(v)}).ToDictionary(x=>x.Description,x=>x.Value);
            return dict[description];
        }
    }
}
