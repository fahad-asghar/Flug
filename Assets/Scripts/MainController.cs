using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class MainController : MonoBehaviour
{
    [SerializeField] private RectTransform mainFader;
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private TextMeshProUGUI loadingText;

    public GameObject activePanel;

    public static MainController instance;

    private void Awake() => instance = this;

    private void Start()
    {
        activePanel.SetActive(true);
        activePanel.GetComponent<Animator>().Play("PanelOpen", -1, 0);
        SetPanelInFront(activePanel);

        mainFader.GetComponent<Image>().DOFade(0, 1f).OnComplete(delegate(){
            mainFader.gameObject.SetActive(false);
        });
    }

    public void OpenLoadingPanel(string text)
    {
        loadingText.text = text;
        loadingPanel.SetActive(true);
        loadingPanel.GetComponent<Animator>().Play("LoadingPanelOpen", -1, 0);
    }

    public void CloseLoadingPanel() => loadingPanel.SetActive(false);

    public void SetPanelInFront(GameObject panel) => panel.GetComponent<RectTransform>().SetAsLastSibling();

    public void ButtonClickSound() => SoundManager.instance.playSound(SoundManager.instance.buttonClick1);

}
