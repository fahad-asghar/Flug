using UnityEngine;
using TMPro;

public class LoggedInPanelController : MonoBehaviour
{ 
    [SerializeField] private AgendaController agendaController;
    [SerializeField] private GameObject loginPanel;

    [SerializeField] private TextMeshProUGUI loggedinTitleText;
    
    private void Start() => SetTitleText();

    public void SetTitleText() => loggedinTitleText.text = "Good Evening, " + PlayerPrefs.GetString("Username") + "!";

    public void Agenda_OnClick()
    {
        MainController.instance.ButtonClickSound();

        MainController.instance.OpenLoadingPanel("Fetching Agendas...");
        StartCoroutine(agendaController.SendAgendaRequest(PlayerPrefs.GetString("UserId")));   
    }

    public void SignOut_OnClick()
    {
        MainController.instance.ButtonClickSound();
        PlayerPrefs.DeleteKey("UserId");

        if(MainController.instance.activePanel == loginPanel)
            return;

        loginPanel.SetActive(true);
        loginPanel.GetComponent<Animator>().Play("PanelOpen", -1, 0);

        MainController.instance.SetPanelInFront(loginPanel);
        MainController.instance.activePanel = loginPanel;
    }
}