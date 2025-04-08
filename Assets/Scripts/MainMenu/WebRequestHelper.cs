using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.MagicWords
{
    public static class WebRequestHelper
    {
        public static async Task<string> GetJsonData(string url)
        {
            using (UnityWebRequest www = UnityWebRequest.Get(url))
            {
                var operation = www.SendWebRequest();
                while (!operation.isDone)
                    await Task.Yield();

                if (www.result == UnityWebRequest.Result.Success)
                {
                    return www.downloadHandler.text;
                }
                else
                {
                    Debug.LogError("Failed to load JSON: " + www.error);
                    return string.Empty;
                }
            }
        }

        public static async Task<Texture2D> GetTextureFromUrlAsync(string url)
        {
            using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
            {
                var operation = www.SendWebRequest();
                while (!operation.isDone)
                    await Task.Yield();

                if (www.result == UnityWebRequest.Result.Success)
                {
                    return ((DownloadHandlerTexture)www.downloadHandler).texture;
                }
                else
                {
                    Debug.LogError("Failed to load texture: " + www.error);
                    return null;
                }
            }
        }
    }
}
