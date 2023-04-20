using System;
using System.Linq;

namespace DCCRailway.Core.Utilities; 

public class AttributeExtractor {
        public static T? GetAttribute<T>(Type type) where T : class {
            var attr = type.GetCustomAttributes(typeof(T), true).FirstOrDefault() as T;
            return attr;
    }    
}