using MelonLoader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics;

namespace WorldPredownload.Cache
{
    public class CacheManager
    {
        private const string NULL_STRING_ARGUMENT = "Tried to check if null string was downloaded";

        private static HashSet<string> directories = new HashSet<string>(); //Still not sure if its even worth using this lel

        public static System.Collections.IEnumerator UpdateDirectoriesBackground()
        {
            yield return null;
            Stopwatch timer = new Stopwatch();
            timer.Start();
            directories.Clear();
            foreach (var directoryInfo in new DirectoryInfo(GetCache().path).GetDirectories())
            {
                directories.Add(directoryInfo.Name);
            }
            timer.Stop();
            MelonLogger.Log($"Finished getting { directories.Count } cache entries in { timer.ElapsedMilliseconds }ms");
            yield break; 
            
        }

        public static void AddDirectory(string hash)
        {
            directories.Add(hash);
        }

        public static bool HasDownloadedWorld(string id)
        {
            return false; //Dead Method for now in case I want to rework the invite checking mechanism
        }

        public static bool HasDownloadedWorld(string id, int version)
        {
            _ = id ?? throw new ArgumentNullException(paramName: nameof(id), message: NULL_STRING_ARGUMENT); //Lazy null check
            if (directories.Contains(ComputeAssetHash(id)))
            {
                if (HasVersion(ComputeAssetHash(id), version))
                    return true;
                else
                    return false;
            }
            else return false;
        }

        public static string ComputeAssetHash(string id)
        {
            return Utilities.ByteArrayToString(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(id))).ToUpper().Substring(0, 16);
        }

        public static UnityEngine.Cache GetCache()
        {
            return Utilities.GetAssetBundleDownloadManager().field_Private_Cache_0;
        }

        public static bool HasVersion(string hash, int version)
        {
            foreach(DirectoryInfo directoryInfo in new DirectoryInfo(Path.Combine(GetCache().path, hash)).GetDirectories())
            {
                if(directoryInfo.Name.Substring(0, directoryInfo.Name.Length - 6).EndsWith(version.ToString("X").ToLower())) return true;
            }
            return false;
        }

    }
}
