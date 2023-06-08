using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class TextureDownloader : MonoBehaviour
{
    public string textureUrl = "https://example.com/texture.png"; // URL of the texture to download

    private void Start()
    {
        StartCoroutine(DownloadTexture());
    }

    private IEnumerator DownloadTexture()
    {
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(textureUrl))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                // Get the downloaded texture
                Texture2D texture = DownloadHandlerTexture.GetContent(www);

                // Save the texture as a file
                SaveTextureToFile(texture, "downloadedTexture.png");

                Debug.Log("Texture downloaded and saved successfully.");
            }
            else
            {
                Debug.Log("Failed to download texture. Error: " + www.error);
            }
        }
    }

    private void SaveTextureToFile(Texture2D texture, string filename)
    {
        byte[] bytes = texture.EncodeToPNG();
        string filePath = Path.Combine(Application.persistentDataPath, filename);
        File.WriteAllBytes(filePath, bytes);
    }
}
