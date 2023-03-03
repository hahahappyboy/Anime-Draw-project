using System.Text;
using Info;
using UnityEngine;

namespace Utils {
    public class JsonUtils {
        public static T Json2Class<T>(string json) {
            byte[] zoneReadByte = Encoding.UTF8.GetBytes(json);
            string zoneString = UTF8Encoding.UTF8.GetString(zoneReadByte);
            T t= JsonUtility.FromJson<T>(zoneString);
            return t;
        }
    }
}