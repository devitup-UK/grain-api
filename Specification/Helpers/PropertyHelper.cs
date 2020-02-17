using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DevItUp.Grain.API.Specification.Helpers
{
    public static class PropertyHelper
    {
        public static void SetProperty(object controlObject, string parameterName, string parameterValue)
        {
            Type objectType = controlObject.GetType();
            PropertyInfo myPropInfo = objectType.GetProperty(parameterName);
            myPropInfo.SetValue(controlObject, parameterValue, null);
        }

        public static bool HasProperty(object controlObject, string parameterName, object parameterValue)
        {
            Type objectType = controlObject.GetType();
            PropertyInfo myPropInfo = objectType.GetProperty(parameterName);
            object propertyValue = myPropInfo.GetValue(controlObject);
            if(propertyValue == parameterValue)
            {
                return true;
            }
            return false;
        }
    }
}
