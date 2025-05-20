using System.Collections.Generic;
using UnityEngine;

public class PopupCanvas : MonoBehaviour
{
    [SerializeField] private GameObject canvasToShow;

    public void ShowPopup()
    {
        canvasToShow.SetActive(true);
    }

    public void HidePopup()
    {
        canvasToShow.SetActive(false);
    }

    // public void ShowForSeconds(float duration)
    // {
    //     StartCoroutine(AutoHide(duration));
    // }
    //
    // private IEnumerator<> AutoHide(float seconds)
    // {
    //     canvasToShow.SetActive(true);
    //     yield return new WaitForSeconds(seconds);
    //     canvasToShow.SetActive(false);
    // }
}