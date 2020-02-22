using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private Button _startButton;

    private void Awake()
    {
        _startButton.onClick.AddListener(() => { SceneManager.LoadScene("Start", LoadSceneMode.Single); });
    }

    private void OnDestroy()
    {
        _startButton.onClick.RemoveAllListeners();
    }
}
