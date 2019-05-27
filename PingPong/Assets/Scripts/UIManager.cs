using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private Text txTopScore = null;
    [SerializeField] private Text txBottomScore = null;

    [SerializeField] private Button btStartSingleGame = null;
    [SerializeField] private Button btStartHostGame = null;
    [SerializeField] private Button btStartClientGame = null;
    [SerializeField] private Button btConnectGame = null;

    [SerializeField] private Button btToMainMenu = null;

    [SerializeField] private InputField tbServerAddress = null;
    public string ServerAddressString => tbServerAddress.text;


    [SerializeField] private GameObject pnMainMenu = null;
    [SerializeField] private GameObject pnScore = null;
    [SerializeField] private GameObject pnGameOver = null;
    [SerializeField] private GameObject pnPleaseWait = null;
    [SerializeField] private GameObject pnConnectToHost = null;


    private void Awake()
    {
        Instance = this;
        btStartSingleGame.onClick.AddListener(OnStartSingleClick);
        btToMainMenu.onClick.AddListener(ToMainMenu);
        btStartHostGame.onClick.AddListener(OnStartHostClick);
        btStartClientGame.onClick.AddListener(OnStartClientClick);
        btConnectGame.onClick.AddListener(ActivateConnectionPanel);

        DisableAllPanels();
        pnMainMenu.SetActive(true);
    }

    private void ActivateConnectionPanel()
    {
        pnMainMenu.SetActive(false);
        pnConnectToHost.SetActive(true);
    }

    private void OnStartClientClick()
    {
        DisableAllPanels();
        SetActiveWaitPanel(true);
        pnScore.SetActive(true);

        StartGameManager.Instance.StartClientGame();
    }

    private void OnStartHostClick()
    {
        DisableAllPanels();
        SetActiveWaitPanel(true);
        pnScore.SetActive(true);

        StartGameManager.Instance.StartHostGame();
    }

    private void OnStartSingleClick()
    {
        pnMainMenu.SetActive(false);
        pnScore.SetActive(true);

        StartGameManager.Instance.StartSingleGame();
    }

    private void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void SetScore(int topScore, int bottomScore)
    {
        txTopScore.text = topScore.ToString();
        txBottomScore.text = bottomScore.ToString();
    }

    public void OnGameOver()
    {
        pnGameOver.SetActive(true);
    }

    public void SetActiveWaitPanel(bool value)
    {
        pnPleaseWait.SetActive(value);
    }

    void DisableAllPanels()
    {
        pnMainMenu.SetActive(false);
        pnScore.SetActive(false);
        pnGameOver.SetActive(false);
        pnPleaseWait.SetActive(false);
        pnConnectToHost.SetActive(false);
    }

    private void Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
        {
            ToMainMenu();
        }
    }
}
