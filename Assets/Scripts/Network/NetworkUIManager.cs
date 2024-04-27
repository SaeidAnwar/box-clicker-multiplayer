
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkUIManager : NetworkBehaviour
{
    [SerializeField] private Button hostBtn, clientBtn;
    [SerializeField] private TextMeshProUGUI playerCnt;

    private NetworkVariable<int> _plCnt = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone);

    private void Awake()
    {
        hostBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
        });

        clientBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
        });
    }

    private void Update()
    {
        playerCnt.text = "Player: " + _plCnt.Value.ToString();

        if (!IsServer)
            return;

        _plCnt.Value = NetworkManager.Singleton.ConnectedClients.Count;
    }
}
