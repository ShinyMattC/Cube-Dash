using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamemodePortal : MonoBehaviour
{
    public gamemode gameModeToChangeTo;
    public MovePlayer playerToModify;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerToModify = other.gameObject.GetComponent<MovePlayer>();
            ChangeGamemode(gameModeToChangeTo);
        }
    }
    public void ChangeGamemode(gamemode g)
    {
        playerToModify.gameMode = g;
        Debug.Log($"Changed the player's gamemode to {playerToModify.gameMode}.");
    }
}
