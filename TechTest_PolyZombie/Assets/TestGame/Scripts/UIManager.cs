using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public GameObject CompletePanel;
    public GameObject LosePanel;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1f;
    }

    public void ShowCompletePanel()
    {
        CompletePanel.SetActive(true);
        LosePanel.SetActive(false);
    }

    public void ShowLosePanel()
    {
        CompletePanel.SetActive(false);
        LosePanel.SetActive(true);
    }

    public void ReloadScene(int numScene)
    {
        SceneManager.LoadScene(numScene);
    }
}
