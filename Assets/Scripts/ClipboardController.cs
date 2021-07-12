using System.Collections;
using UnityEngine;
using System.IO;
using System.Diagnostics;
using System;
using Debug = UnityEngine.Debug;

public class ClipboardController : Singleton<ClipboardController>
{
    [SerializeField]
    private RectTransform targetRectTransform;
    [SerializeField]
    private UnityEngine.Object unityDataReceiver;

    private readonly string unityDataReceiverName = "UnityDataReceiver.exe";
    private readonly string textureDataFileName = "TextureData.txt";
    private string TextureDataPath
    {
        get
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\RandomJobAssigner\Texture\";
            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return Path.Combine(path, textureDataFileName);
        }
    }

    private string DataReceiverPath
    {

        get
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\RandomJobAssigner\Texture\", unityDataReceiverName);
            if(!File.Exists(path))
            {
                File.Copy(Path.Combine(Application.streamingAssetsPath, unityDataReceiverName), path);
            }

            return path;
        }
    }

    private Rect GetRectFromRectTransform(RectTransform rectTransform)
    {
        Vector2 pivot = rectTransform.pivot;

        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        rectTransform.ForceUpdateRectTransforms();

        Vector2 position = RectTransformUtility.WorldToScreenPoint(Camera.main, rectTransform.position);
        Rect rect = rectTransform.rect;

        rect.x = position.x - rect.width / 2;
        rect.y = position.y - rect.height / 2;

        rectTransform.pivot = pivot;
        rectTransform.ForceUpdateRectTransforms();

        return rect;
    }

    private Texture2D ConvertToTexture2D(Rect renderRect)
    {
        Texture2D texture = new Texture2D((int)renderRect.width, (int)renderRect.height);
        texture.ReadPixels(renderRect, 0, 0, false);
        texture.Apply();

        return texture;
    }

    private void CopyToClipboard(Texture2D screenshot)
    {
        byte[] bytes = screenshot.EncodeToJPG();
        string byteString = Convert.ToBase64String(bytes);

        File.WriteAllText(TextureDataPath, byteString);
        Process.Start(DataReceiverPath,
            $"\"{TextureDataPath}\"");
    }

    public void Capture()
    {
        StartCoroutine(CaptureCoroutine());
    }

    private IEnumerator CaptureCoroutine()
    {
        yield return new WaitForEndOfFrame();

        CopyToClipboard(ConvertToTexture2D(GetRectFromRectTransform(targetRectTransform)));
    }
}