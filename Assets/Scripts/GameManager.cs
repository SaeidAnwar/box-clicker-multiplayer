
using TMPro;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;


public class GameManager : NetworkBehaviour
{
    [SerializeField] private GameObject nameText;
    [SerializeField] private GameObject wonBg;
    [SerializeField] private GameObject wonText;
    [SerializeField] private GameObject winner;
    public GameObject nameInput;
    public GameObject startBtn;
    public GameObject startText;
    public GameObject prefab;
    public NetworkVariable<bool> gameStarted = new NetworkVariable<bool>(false);
    public NetworkVariable<int> boxCnt = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<bool> won = new NetworkVariable<bool>(false);
    public NetworkVariable<FixedString128Bytes> winnerName = new NetworkVariable<FixedString128Bytes>("", NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public int numOfBox = 10;

    // Update is called once per frame
    void Update()
    {
        if(IsHost)
        {
            if (won.Value == true)
            {
                gameStarted.Value = false;
                boxCnt.Value = 0;

                GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");
                foreach (GameObject bx in boxes)
                {
                    NetworkObject no = bx.GetComponent<NetworkObject>();
                    no.Despawn();
                }

                winner.GetComponent<TextMeshProUGUI>().text = winnerName.Value.ToString();
                wonBg.SetActive(true);
                wonText.SetActive(true);
                winner.SetActive(true);
            }

            if (gameStarted.Value == false)
            {
                startBtn.SetActive(true);
                startText.SetActive(false);
            }
            else
            {
                nameInput.SetActive(false);
                nameText.SetActive(false);
            }

            if (gameStarted.Value == true && boxCnt.Value == 0)
            {
                for (int i = 0; i < numOfBox; i++)
                {
                    float posX = Random.Range(-14f, 14f);
                    float posZ = Random.Range(-14f, 14f);
                    GameObject spawnedBox = GameObject.Instantiate(prefab, new Vector3(posX, 2, posZ), prefab.transform.rotation);
                    spawnedBox.GetComponent<NetworkObject>().Spawn();
                    spawnedBox.transform.Rotate(spawnedBox.transform.up, Random.Range(0f, 360f));
                }
                boxCnt.Value += numOfBox;
            }
        }
        else if(IsClient)
        {
            startBtn.SetActive(false);
            startText.SetActive(true);
            if (gameStarted.Value == true)
            {
                startText.SetActive(false);
                nameText.SetActive(false);
                nameInput.SetActive(false);
            }

            if (won.Value == true)
            {
                winner.GetComponent<TextMeshProUGUI>().text = winnerName.Value.ToString();
                wonBg.SetActive(true);
                wonText.SetActive(true);
                winner.SetActive(true);
            }
            else
            {
                wonBg.SetActive(false);
                wonText.SetActive(false);
                winner.SetActive(false);
            }
        }
    }

    public void StartGame()
    {
        if (!IsServer)
            return;
        gameStarted.Value = true;
        won.Value = false;
        wonBg.SetActive(false);
        wonText.SetActive(false);
        winner.SetActive(false);
        startBtn.SetActive(false);
    }
}
