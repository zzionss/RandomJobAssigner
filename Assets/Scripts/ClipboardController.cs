using System.Collections;
using UnityEngine;
using System.IO;
using System.Diagnostics;
using System;

public class ClipboardController : Singleton<ClipboardController>
{
    [SerializeField]
    private RenderTexture targetTexture;
    [SerializeField]
    private RectTransform targetRectTransform;
    [SerializeField]
    private string clipboardFileName;
    [SerializeField]
    private string textureDataFileName;

    private Rect GetRectFromRectTransform(RectTransform rectTransform)
    {
        targetRectTransform.anchorMin = new Vector2(0, 1);
        targetRectTransform.anchorMax = new Vector2(0, 1);
        targetRectTransform.pivot = new Vector2(0, 1);

        return new Rect(
            targetRectTransform.anchoredPosition.x,
            -targetRectTransform.anchoredPosition.y,
            targetRectTransform.sizeDelta.x,
            targetRectTransform.sizeDelta.y
            );
    }

    private Texture2D ConvertToTexture2D(RenderTexture renderTexture, Rect renderRect)
    {
        Texture2D tex = new Texture2D((int)renderRect.width, (int)renderRect.height);
        RenderTexture.active = renderTexture;
        tex.ReadPixels(renderRect, 0, 0, false);
        tex.Apply();
        return tex;
    }

    private void CopyToClipboard(Texture2D screenshot)
    {
        byte[] bytes = screenshot.EncodeToJPG();
        string byteString = Convert.ToBase64String(bytes);

        File.WriteAllText(Path.Combine(Application.dataPath, textureDataFileName), byteString);
        Process.Start(Path.Combine(Application.dataPath, clipboardFileName), 
            Path.Combine(Application.dataPath, textureDataFileName));

        // TODO: 클립보드 exe가 없을 때 알림창이 나옵니다.
    }

    public void Capture()
    {
        StartCoroutine(CaptureCoroutine());
    }

    private IEnumerator CaptureCoroutine()
    {
        Camera.main.targetTexture = targetTexture;
        yield return new WaitForEndOfFrame();

        CopyToClipboard(ConvertToTexture2D(targetTexture, GetRectFromRectTransform(targetRectTransform)));
        yield return new WaitForEndOfFrame();
        Camera.main.targetTexture = null;
    }
}