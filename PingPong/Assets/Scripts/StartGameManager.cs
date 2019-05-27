using System.Collections;
using GameControllers;
using Network.ClientServer;
using Unity.Networking.Transport;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameManager : MonoBehaviour
{
    public static StartGameManager Instance;

    [SerializeField] private GameController singleGamePrefab = null;
    [SerializeField] private GameController hostGamePrefab = null;
    [SerializeField] private GameController clientGamePrefab = null;

    private string serverAddressString => UIManager.Instance.ServerAddressString;

    private void Awake()
    {
        Instance = this;
    }

    public void StartSingleGame()
    {
        NetworkInfo.SetPoint(UIManager.Instance.ServerAddressString);
        var singleGameManager = Instantiate(singleGamePrefab);
    }

    public void StartHostGame()
    {
        var endpoint = NetworkEndPoint.LoopbackIpv4;
        endpoint.Port = NetworkInfo.Port;
        NetworkInfo.SetPoint(endpoint);
        StartCoroutine(StartHost());
    }


    public void StartClientGame()
    {
        if (string.IsNullOrEmpty(serverAddressString))
            NetworkInfo.SetLocalPoint();
        else
            NetworkInfo.SetPoint(serverAddressString);

        if (NetworkInfo.ServerEndPoint.IsValid)
        {
            var clientGameManager = Instantiate(clientGamePrefab);
        }
        else
        {
            Debug.LogError("Wrong ip address");
        }
    }


    IEnumerator StartHost()
    {
        yield return SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        var hostGameManager = Instantiate(hostGamePrefab);
    }
}