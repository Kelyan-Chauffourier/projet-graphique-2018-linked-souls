using UnityEngine;

public class MenuView : MonoBehaviour {
    public GameObject canvasMenuPrincipal;
    public GameObject canvasMenuNetwork;
    public GameObject canvasSettingMenu;
	public GameObject canvasHelpMenu;

    private void Awake()
    {
        MenuController.instance = new MenuController();
    }
    public void OnQuitButtonPressed()
    {
        MenuController.instance.QuitGame();
    }

    public void OnSettingButtonPressed()
    {
        canvasMenuPrincipal.SetActive(false);
        canvasSettingMenu.SetActive(true);
    }

    public void OnHelpButtonPressed()
    {
		canvasMenuPrincipal.SetActive(false);
		canvasHelpMenu.SetActive(true);
    }

    public void OnCreateMatchButtonPressed()
    {
        canvasMenuPrincipal.SetActive(false);
        canvasMenuNetwork.SetActive(true);

    }
}
