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
    [SerializeField]
    private UnityEngine.Object textureDataFileName;

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

        File.WriteAllText(Path.Combine(Application.dataPath, textureDataFileName.name + ".txt"), byteString);
        Process.Start(Path.Combine(Application.dataPath, unityDataReceiver.name + ".exe"),
            $"\"{Path.Combine(Application.dataPath, textureDataFileName.name + ".txt")}\"");

        // TODO: 클립보드 exe가 없을 때 알림창이 나옵니다.
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