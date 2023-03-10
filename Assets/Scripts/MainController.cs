using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    [SerializeField] private RectTransform mainFader;
    [SerializeField] private RectTransform userSidebar;
    [SerializeField] private RectTransform loginPanel;
    [SerializeField] private RectTransform registerPanel;
    [SerializeField] private RectTransform userPanel;
    [SerializeField] private RectTransform menuButton;

    private bool userPanelOpened = false;

    private void Start()
    {
        mainFader.GetComponent<Image>().DOFade(0, 0.5f).OnComplete(delegate(){
            mainFader.gameObject.SetActive(false);
        });
    }

    public void MenuButton_OnClick()
    {
        ButtonClickSound();

        if(!userPanelOpened)
        {
            menuButton.GetComponent<Animator>().Play("In", -1, 0);
            userSidebar.GetComponent<CanvasGroup>().DOFade(0, 0).OnComplete(delegate(){
                userSidebar.GetComponent<CanvasGroup>().DOFade(1, 0.2f);
                userSidebar.DOAnchorPos(Vector2.zero, 0.3f);
                userPanelOpened = true;
                return;
            });   
        }

        else
            CloseUserSidebar();
    }

    private void CloseUserSidebar()
    {
        menuButton.GetComponent<Animator>().Play("Out", -1, 0);
        userSidebar.GetComponent<CanvasGroup>().DOFade(0, 0.3f);
        userSidebar.DOAnchorPos(new Vector2(-1140, 0), 0.3f);
        userPanelOpened = false;    
    }

    public void HomeButton_OnClick()
    {
        ButtonClickSound();
        CloseUserSidebar();

        registerPanel.GetComponent<CanvasGroup>().DOFade(0, 0);
        registerPanel.gameObject.SetActive(false);

        loginPanel.GetComponent<CanvasGroup>().DOFade(0, 0);
        loginPanel.gameObject.SetActive(false);
    }

    public void UserSidebarLogin_OnClick()
    {
        ButtonClickSound();
        CloseUserSidebar();

        registerPanel.GetComponent<CanvasGroup>().DOFade(0, 0);
        registerPanel.gameObject.SetActive(false);

        loginPanel.GetComponent<CanvasGroup>().DOFade(0, 0);
        loginPanel.gameObject.SetActive(true);
        loginPanel.GetComponent<CanvasGroup>().DOFade(1, 0.2f);
    }

    public void UserSidebarRegister_OnClick()
    {
        ButtonClickSound();
        CloseUserSidebar();

        loginPanel.GetComponent<CanvasGroup>().DOFade(0, 0);
        loginPanel.gameObject.SetActive(false);

        registerPanel.GetComponent<CanvasGroup>().DOFade(0, 0);
        registerPanel.gameObject.SetActive(true);
        registerPanel.GetComponent<CanvasGroup>().DOFade(1, 0.2f);
    }

    public void LoginPanelLogin_OnClick()
    {
        ButtonClickSound();
    
        userPanel.GetComponent<CanvasGroup>().DOFade(0, 0);
        userPanel.gameObject.SetActive(true);
        userPanel.GetComponent<CanvasGroup>().DOFade(1, 0.4f).OnComplete(delegate(){
            
            registerPanel.GetComponent<CanvasGroup>().DOFade(0, 0);
            registerPanel.gameObject.SetActive(false);

            loginPanel.GetComponent<CanvasGroup>().DOFade(0, 0);
            loginPanel.gameObject.SetActive(false);
        });
    }

    public void ButtonClickSound()
    {
        SoundManager.instance.playSound(SoundManager.instance.buttonClick1);
    }
}
