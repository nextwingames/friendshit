using Nextwin.Protocol;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Nextwin
{
    namespace Util
    {
        public class JsonManager
        {
            /// <summary>
            /// 구조체를 바이트 배열로 변환
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            public static byte[] ObjectToBytes(object obj)
            {
                return JsonToBytes(ObjectToJson(obj));
            }

            /// <summary>
            /// 헤더를 바이트 배열로 변환
            /// </summary>
            /// <param name="header"></param>
            /// <returns></returns>
            public static byte[] ObjectToBytes(Header header)
            {
                string json = ObjectToJson(header);
                SetHeaderLength(header, ref json);
                return JsonToBytes(json);
            }

            /// <summary>
            /// 바이트 배열을 구조체로 변환
            /// </summary>
            /// <param name="bytes"></param>
            /// <returns></returns>
            public static T BytesToObject<T>(byte[] bytes)
            {
                return JsonToObject<T>(BytesToJson(bytes));
            }

            private static string ObjectToJson(object obj)
            {
                return JsonUtility.ToJson(obj);
            }

            private static T JsonToObject<T>(string json)
            {
                return JsonUtility.FromJson<T>(json);
            }

            private static byte[] JsonToBytes(string json)
            {
                string jsonCamel = RenameToCamelCase(json);
                byte[] bytes = Encoding.UTF8.GetBytes(jsonCamel);
                return bytes;
            }

            private static string BytesToJson(byte[] bytes)
            {
                string json = Encoding.Default.GetString(bytes);
                string jsonPascal = RenameToPascalCase(json);
                return jsonPascal;
            }

            /// <summary>
            /// 헤더의 json 길이를 26으로 맞춤
            /// </summary>
            /// <param name="header"></param>
            /// <param name="jsonHeader"></param>
            public static void SetHeaderLength(Header header, ref string jsonHeader)
            {
                if(header.MsgType < 10)
                    jsonHeader += ' ';
                if(header.Length < 10)
                    jsonHeader += ' ';
            }

            /// <summary>
            /// C# 명명 규칙 -> JAVA 명명 규칙
            /// </summary>
            /// <param name="json"></param>
            /// <returns></returns>
            private static string RenameToCamelCase(string json)
            {
                string camelJson = "";

                int length = json.Length;

                for(int i = 0; i < length; i++)
                {
                    char c = json[i];

                    camelJson += c;

                    if(json[i] == '\"')
                    {
                        if(json[i + 1] != ':')
                        {
                            // 필드 변수 첫 글자를 소문자로
                            camelJson += char.ToLower(json[i + 1]);
                            i++;
                        }
                    }
                }

                return camelJson;
            }

            /// <summary>
            /// JAVA 명명 규칙 -> C# 명명 규칙
            /// </summary>
            /// <param name="json"></param>
            /// <returns></returns>
            private static string RenameToPascalCase(string json)
            {
                string pascalJson = "";

                int length = json.Length;

                for(int i = 0; i < length; i++)
                {
                    char c = json[i];

                    if(c == ' ')
                        continue;

                    pascalJson += c;

                    if(json[i] == '\"')
                    {
                        if(json[i + 1] != ':' && json[i - 1] != ':' && json[i - 1] != '[')
                        {
                            // 필드 변수 첫 글자를 대문자로
                            pascalJson += char.ToUpper(json[i + 1]);
                            i++;
                        }
                    }
                }

                return pascalJson;
            }
        }
    }
}
