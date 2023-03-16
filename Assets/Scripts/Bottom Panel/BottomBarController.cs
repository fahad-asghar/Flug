using System.Collections.Generic;
using UnityEngine;

public class BottomBarController : MonoBehaviour
{
    [SerializeField] private GameObject homePanel;
    [SerializeField] private GameObject agendaPanel;
    [SerializeField] private GameObject settingsPanel;
    [HideInInspector] private GameObject mePanel;

    [SerializeField] private List<GameObject> mePanels;

    public void Home_OnClick(GameObject button)
    {
        MainController.instance.ButtonClickSound();
        button.GetComponent<Animator>().Play("ClickEffect", -1, 0);

        if(MainController.instance.activePanel == homePanel)
            return;

        homePanel.SetActive(true);
        homePanel.GetComponent<Animator>().Play("PanelOpen", -1, 0);

        MainController.instance.SetPanelInFront(homePanel);
        MainController.instance.activePanel = homePanel;
    }

    public void WaitingList_OnClick(GameObject button)
    {
        MainController.instance.ButtonClickSound();
        button.GetComponent<Animator>().Play("ClickEffect", -1, 0);

        if(MainController.instance.activePanel == agendaPanel)
            return;

        agendaPanel.SetActive(true);
        agendaPanel.GetComponent<Animator>().Play("PanelOpen", -1, 0);

        MainController.instance.SetPanelInFront(agendaPanel);
        MainController.instance.activePanel = agendaPanel;
    }

    public void Settings_OnClick(GameObject button)
    {
        MainController.instance.ButtonClickSound();
        button.GetComponent<Animator>().Play("ClickEffect", -1, 0);

        if(MainController.instance.activePanel == settingsPanel)
            return;

        settingsPanel.SetActive(true);
        settingsPanel.GetComponent<Animator>().Play("PanelOpen", -1, 0);

        MainController.instance.SetPanelInFront(settingsPanel);
        MainController.instance.activePanel = settingsPanel;
    }

    public void Me_OnClick(GameObject button)
    {
        if(PlayerPrefs.HasKey("UserId"))
            mePanel = mePanels[1];
        else
            mePanel = mePanels[0];
             
        MainController.instance.ButtonClickSound();
        button.GetComponent<Animator>().Play("ClickEffect", -1, 0);

        if(MainController.instance.activePanel == mePanel)
            return;

        mePanel.SetActive(true);
        mePanel.GetComponent<Animator>().Play("PanelOpen", -1, 0);

        MainController.instance.SetPanelInFront(mePanel);
        MainController.instance.activePanel = mePanel;
    }
}
