using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DevItUp.Grain.API.Specification.Helpers
{
    public static class PropertyHelper
    {
        public static void SetProperty(object controlObject, string parameterName, object parameterValue)
        {
            Type objectType = controlObject.GetType();
            PropertyInfo myPropInfo = objectType.GetProperty(parameterName);
            myPropInfo.SetValue(controlObject, parameterValue, null);
        }

        public static bool HasProperty(object controlObject, string parameterName)
        {
            Type objectType = controlObject.GetType();
            PropertyInfo myPropInfo = objectType.GetProperty(parameterName);
            object propertyValue = myPropInfo.GetValue(controlObject);
            if(propertyValue != null)
            {
                return true;
            }
            return false;
        }

        public static bool HasPropertyOfType(object controlObject, string parameterName, Type parameterType)
        {
            Type objectType = controlObject.GetType();
            PropertyInfo myPropInfo = objectType.GetProperty(parameterName);
            Type propertyType = myPropInfo.GetValue(controlObject).GetType();
            if (propertyType == parameterType)
            {
                return true;
            }
            return false;
        }

        public static bool HasPropertyOfTypeWithValue(object controlObject, string parameterName, Type parameterType, object expectedValue)
        {
            Type objectType = controlObject.GetType();
            PropertyInfo myPropInfo = objectType.GetProperty(parameterName);
            object propertyValue = myPropInfo.GetValue(controlObject);
            Type propertyType = propertyValue.GetType();
            if (propertyType == parameterType)
            {
                if(propertyValue == expectedValue)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool HasPropertyWithValue(object controlObject, string parameterName, object expectedValue)
        {
            Type objectType = controlObject.GetType();
            PropertyInfo myPropInfo = objectType.GetProperty(parameterName);
            object propertyValue = myPropInfo.GetValue(controlObject);
            if (propertyValue == expectedValue)
            {
                return true;
            }
            return false;
        }
    }
}
