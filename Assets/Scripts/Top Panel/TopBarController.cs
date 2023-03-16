using UnityEngine;

public class TopBarController : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;

    public bool menuPanelOpened = false;

    public void Menu_OnClick(GameObject button)
    {
        MainController.instance.ButtonClickSound();

        if(MainController.instance.activePanel != menuPanel)
            menuPanelOpened = false;

        if(!menuPanelOpened)
        {
            menuPanel.SetActive(true);
            menuPanel.GetComponent<Animator>().Play("MenuPanelOpen", -1, 0);
            menuPanelOpened = true;

            MainController.instance.SetPanelInFront(menuPanel);
            MainController.instance.activePanel = menuPanel;

            return;
        }

        if(menuPanelOpened)
        {
            menuPanel.GetComponent<Animator>().Play("MenuPanelClose", -1, 0);
            menuPanelOpened = false; 
            
            MainController.instance.activePanel = null;
        }  
    }
}
