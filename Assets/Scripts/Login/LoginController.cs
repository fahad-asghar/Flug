using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using TMPro;

public class LoginController : MonoBehaviour
{
    private string loginURL = "https://devsite.flugsite.com/app_script/login.php";
    [SerializeField] private TMP_InputField username;
    [SerializeField] private TMP_InputField password;

    [SerializeField] private GameObject loggedInPanel;
    [SerializeField] private LoggedInPanelController loggedInController;

    public void Login_OnClick()
    {
        MainController.instance.ButtonClickSound();

        MainController.instance.OpenLoadingPanel("Logging In...");
        StartCoroutine(SendLoginRequest(username.text, password.text));
    }

    private IEnumerator SendLoginRequest(string username, string password)
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>();

        form.Add(new MultipartFormDataSection("userlogin", username));
        form.Add(new MultipartFormDataSection("userpass", password));

        UnityWebRequest webRequest = UnityWebRequest.Post(loginURL, form);

        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(webRequest.error);
            MainController.instance.CloseLoadingPanel();
        }

        else
        {
            Debug.Log(webRequest.downloadHandler.text);
            string loginDetails = webRequest.downloadHandler.text;

            SplitData(loginDetails);
        }
    }

    private void SplitData(string loginDetails)
    {
        string [] splitDetails = loginDetails.Split("#");

        if(splitDetails.Length != 3)
        {
            MainController.instance.CloseLoadingPanel();
            return;
        }

        string username = splitDetails[0];
        string email = splitDetails[1];
        string userId = splitDetails[2];

        SaveLoginData(username, email, userId);
    }

    private void SaveLoginData(string username, string email, string userId)
    {
        PlayerPrefs.SetString("Username", username);
        PlayerPrefs.SetString("Email", email);
        PlayerPrefs.SetString("UserId", userId);

        MainController.instance.CloseLoadingPanel();
        OpenLoggedInPanel();
    }

    private void OpenLoggedInPanel()
    {
        if(MainController.instance.activePanel == loggedInPanel)
            return;

        loggedInController.SetTitleText();
        loggedInPanel.SetActive(true);
        loggedInPanel.GetComponent<Animator>().Play("PanelOpen", -1, 0);

        MainController.instance.SetPanelInFront(loggedInPanel);
        MainController.instance.activePanel = loggedInPanel;
    }
}
