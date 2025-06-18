using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Menu
{
    internal class SaveManager
    {
        private static readonly string filePath = Path.Combine(Application.persistentDataPath, "save.dat");
        private static readonly string secretKey = "hhjj23aaww";

        public static void Save(PlayerData data)
        {
            string json = JsonUtility.ToJson(data);
            string encrypted = Encrypt(json);
            File.WriteAllText(filePath, encrypted);
        }

        public static PlayerData Load()
        {
            if (!File.Exists(filePath))
            {
                return new PlayerData() { currentLevel = 1 };
            }

            string encrypted = File.ReadAllText(filePath);
            string decrypted = Decrypt(encrypted);
            var obj = JsonUtility.FromJson<PlayerData>(decrypted);
            Debug.Log(obj.volume);
            Debug.Log(obj.currentLevel);
            return JsonUtility.FromJson<PlayerData>(decrypted);
        }

        private static string Encrypt(string plainText)
        {
            var key = Encoding.UTF8.GetBytes(secretKey);
            var input = Encoding.UTF8.GetBytes(plainText);
            byte[] result = new byte[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                result[i] = (byte)(input[i] ^ key[i % key.Length]);
            }

            return Convert.ToBase64String(result);
        }

        private static string Decrypt(string encryptedBase64)
        {
            var key = Encoding.UTF8.GetBytes(secretKey);
            byte[] input = Convert.FromBase64String(encryptedBase64);
            byte[] result = new byte[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                result[i] = (byte)(input[i] ^ key[i % key.Length]);
            }

            return Encoding.UTF8.GetString(result);
        }
    }
}
