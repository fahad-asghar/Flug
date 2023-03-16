using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class AgendaController : MonoBehaviour
{
    private string agendaURL = "http://devsite.flugsite.com/wp-content/themes/flug_v1/app_script/agenda-template-user-app.php";
    public List<Agenda> agenda;

    [SerializeField] private GameObject agendaPanel;
    [SerializeField] private GameObject agendaEntryPrefab;
    [SerializeField] private Transform agendaEntryParent;
    [SerializeField] private GameObject dateTimeTextPrefab;
    [SerializeField] private GameObject workshopTextPrefab;

    public IEnumerator SendAgendaRequest(string userId)
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>();

        form.Add(new MultipartFormDataSection("user_id", userId));

        UnityWebRequest webRequest = UnityWebRequest.Post(agendaURL, form);

        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(webRequest.error);
            MainController.instance.CloseLoadingPanel();
        }

        else
        {
            Debug.Log(webRequest.downloadHandler.text);
            ExtractAgendaData(webRequest.downloadHandler.text);   
        }
    }

    private void ExtractAgendaData(string data)
    {
        agenda.Clear();
        agenda = new List<Agenda>();
        List<string> agendaList = new List<string>();

        string [] splitData = data.Split("=");

        for(int i = 0; i < splitData.Length; i++)
            agendaList.Add(splitData[i]);

        for(int i = 0; i < agendaList.Count; i++)
        {
            string [] split = agendaList[i].Split("#");

            string dateTime = "";
            List<string> workshopsList = new List<string>();

            for(int j = 0; j < split.Length; j++)
            {
                if(j == 0)
                    dateTime = split[j];
                else
                    workshopsList.Add(split[j]);           
            }

            this.agenda.Add(new Agenda(){
                dateTime = dateTime,
                workshops = workshopsList
            });   
        } 

        CreateLayout();
    }

    private void CreateLayout()
    {
        for(int i = 0; i < agendaEntryParent.childCount; i++)
            Destroy(agendaEntryParent.GetChild(i).gameObject);
            
        for(int i = 0; i < agenda.Count; i++)
        {
            if(agenda[i].workshops.Count == 0)
                continue;

            GameObject agendaEntry = Instantiate(agendaEntryPrefab);

            agendaEntry.GetComponent<RectTransform>().SetParent(agendaEntryParent);
            agendaEntry.transform.localScale = Vector3.one;


            GameObject dateTimeText = Instantiate(dateTimeTextPrefab);

            dateTimeText.GetComponent<RectTransform>().SetParent(agendaEntry.transform);
            dateTimeText.transform.localScale = Vector3.one;

            dateTimeText.GetComponent<TextMeshProUGUI>().text = agenda[i].dateTime;

            for(int j = 0; j < agenda[i].workshops.Count; j++)
            {
                GameObject workshopText = Instantiate(workshopTextPrefab);

                workshopText.GetComponent<RectTransform>().SetParent(agendaEntry.transform);
                workshopText.transform.localScale = Vector3.one;

                workshopText.GetComponent<TextMeshProUGUI>().text = agenda[i].workshops[j];  
            }
        }

        MainController.instance.CloseLoadingPanel();
        OpenAgendaPanel();
    }

    private void OpenAgendaPanel()
    {
        if(MainController.instance.activePanel == agendaPanel)
            return;

        agendaPanel.SetActive(true);
        agendaPanel.GetComponent<Animator>().Play("PanelOpen", -1, 0);

        MainController.instance.SetPanelInFront(agendaPanel);
        MainController.instance.activePanel = agendaPanel;
    }
}

[System.Serializable]
public class Agenda
{
    public string dateTime;
    public List<string> workshops;
}