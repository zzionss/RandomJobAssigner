using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using TMPro;

using UnityEngine;

public class PopUp : Singleton<PopUp>
{
    public static async void ShowPopUp(string text)
    {
        Instance.ShowPopUp();
        Instance.SetPopUpText(text);
        await Task.Delay(1000);
        Instance.HidePopUp();
    }

    [SerializeField]
    private TextMeshProUGUI popUpText;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void ShowPopUp()
    {
        canvasGroup.alpha = 1;
    }

    private void HidePopUp()
    {
        canvasGroup.alpha = 0;
    }

    private void SetPopUpText(string text)
    {
        popUpText.text = text;
    }
}
