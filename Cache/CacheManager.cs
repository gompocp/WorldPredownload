using MelonLoader;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics;

namespace WorldPredownload.Cache
{
    public class CacheManager
    {
        private static HashSet<string> directories = new HashSet<string>(); //Still not sure if its even worth using this lel

        public static void UpdateDirectoriesThread()
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            directories.Clear();
            foreach (var entry in Directory.EnumerateDirectories(GetCache().path)) // Slightly better performance 2000 entries in 95ms~ vs 2000 entries in 70ms~ with testing on my poor cpu ¯\_(ツ)_/¯ 
            {
                directories.Add(new DirectoryInfo(entry).Name);
            }
            timer.Stop();
            MelonLogger.Log($"Finished getting { directories.Count } cache entries in { timer.ElapsedMilliseconds }ms");
        }

        public static void AddDirectory(string hash) => directories.Add(hash);


        public static bool HasDownloadedWorld(string id)
        {
            return false; //Dead Method for now in case I want to rework the invite checking mechanism
        }

        public static bool HasDownloadedWorld(string id, int version)
        {
            string hash = ComputeAssetHash(id);
            if (directories.Contains(hash))
            {
                if (HasVersion(hash, version))
                    return true;
                return false;
            }
            return false;
        }

        public static string ComputeAssetHash(string id)
        {
            return Utilities.ByteArrayToString(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(id))).ToUpper().Substring(0, 16);
        }

        private static UnityEngine.Cache GetCache() => Utilities.GetAssetBundleDownloadManager().field_Private_Cache_0;

        private static bool HasVersion(string hash, int version)
        {
            foreach(DirectoryInfo directoryInfo in new DirectoryInfo(Path.Combine(GetCache().path, hash)).GetDirectories())
            {
                if (directoryInfo.Name.EndsWith(ComputeVersionString(version))) return true;
            }
            return false;
        }

        private static string ComputeVersionString(int version)
        {
            // This'll work with worlds with a version sub 4096
            string result = version.ToString("X").ToLower();
            if (result.Length == 3)
            {
                string part = result.Substring(0, 1);
                result = result.Substring(1, result.Length - 1);
                return result += $"0{part}0000";
            }
            return result += "000000";
        }

    }
}
