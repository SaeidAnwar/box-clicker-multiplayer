using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : NetworkBehaviour
{
    public string playerName;
    private GameManager gm;
    public int score = 0;
    [SerializeField] private int winScore = 8;

    private void Awake()
    {
        score = 0;
        gm = GameObject.FindObjectOfType<GameManager>();
        TMP_InputField inField = gm.nameInput.GetComponent<TMP_InputField>();
        if(inField != null)
        {
            Debug.Log(inField.text);
            playerName = inField.text;
        }
    }
    private void Update()
    {
        if (!IsOwner)
            return;

        if(gm.won.Value == true)
        {
            score = 0;
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 1000f))
            {
                if (raycastHit.transform != null && raycastHit.transform.gameObject.tag == "Box")
                {
                    DestroyServerRpc(raycastHit.transform.GetComponent<NetworkObject>().NetworkObjectId);
                    score++;
                    if(score == winScore)
                    {
                        GameWonServerRpc(playerName);
                    }
                }
            }
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void DestroyServerRpc(ulong itemNetId)
    {
        gm.boxCnt.Value--;
        Debug.Log(itemNetId);
        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");
        foreach(GameObject bx in boxes)
        {
            NetworkObject no = bx.GetComponent<NetworkObject>();
            if(no.NetworkObjectId == itemNetId)
            {
                Debug.Log("rpc"); 
                no.Despawn();
                return;
            }
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void GameWonServerRpc(string plName)
    {
        gm.won.Value = true;
        gm.winnerName.Value = plName;
    }
}
