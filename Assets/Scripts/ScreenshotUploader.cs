using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ScreenshotUploader : MonoBehaviour
{
    public string imgbbApiKey = "6748a8edfecaa2cd07f35772234beb57";

    public void CaptureAndUpload()
    {
        Debug.Log("🔹 Bắt đầu CaptureAndUpload bằng ReadPixels");
        StartCoroutine(CaptureScreenshot());
    }

    private IEnumerator CaptureScreenshot()
    {
        yield return new WaitForEndOfFrame(); // Đợi khung hình kết thúc

        int width = Screen.width;
        int height = Screen.height;

        Texture2D screenshot = new Texture2D(width, height, TextureFormat.RGB24, false);
        screenshot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        screenshot.Apply();

        byte[] imageData = screenshot.EncodeToPNG();
        Debug.Log("📦 Ảnh đã encode PNG, kích thước byte: " + imageData.Length);

        Destroy(screenshot);

        StartCoroutine(UploadImage(imageData));
    }

    private IEnumerator UploadImage(byte[] imageData)
    {
        WWWForm form = new WWWForm();
        form.AddField("key", imgbbApiKey);
        form.AddBinaryData("image", imageData, "screenshot.png");

        using (UnityWebRequest www = UnityWebRequest.Post("https://api.imgbb.com/1/upload", form))
        {
            Debug.Log("📡 Gửi yêu cầu POST tới imgbb...");
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("❌ Upload failed: " + www.error);
            }
            else
            {
                string json = www.downloadHandler.text;
                string url = ExtractImageUrl(json);
                Debug.Log("✅ Link ảnh: " + url);

                string facebookShareUrl = "https://www.facebook.com/sharer/sharer.php?u=" + UnityWebRequest.EscapeURL(url);
                Debug.Log("🌐 Mở Facebook Share URL: " + facebookShareUrl);

                Application.OpenURL(facebookShareUrl);
            }
        }
    }

    private string ExtractImageUrl(string json)
    {
        string search = "\"display_url\":\"";
        int startIndex = json.IndexOf(search) + search.Length;
        int endIndex = json.IndexOf("\"", startIndex);
        return json.Substring(startIndex, endIndex - startIndex).Replace("\\/", "/");
    }

}
