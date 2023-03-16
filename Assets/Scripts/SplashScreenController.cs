using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenController : MonoBehaviour
{
    public static SplashScreenController instance;

    [SerializeField] private GameObject logoObject;
    [SerializeField] private GameObject tagObject;

    private void Awake() => instance = this;

    private void Start() => Invoke("Delay", 1);

    private void Delay() => logoObject.SetActive(true);

    public void EnableTag() => tagObject.SetActive(true);

    public void OpenMainScene() => SceneManager.LoadScene("Main");
}
