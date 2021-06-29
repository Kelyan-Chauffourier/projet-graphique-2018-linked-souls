using UnityEngine;

public class StartView : MonoBehaviour
{
    public GameObject canvasMenuPrincipal;
    public GameObject canvasMenuStart;

    public void OnStartButtonPressed()
    {
        canvasMenuPrincipal.SetActive(true);
        canvasMenuStart.SetActive(false);
    }
}
