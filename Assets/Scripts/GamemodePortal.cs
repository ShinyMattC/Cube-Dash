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
    public void Switch(Component sender, params object[] data)
    {
        if((string)data[3] == this.tag)
        {
            Debug.Log($"{sender} will switch to {gameModeToChangeTo}");
            playerToModify = (MovePlayer)sender;
            
            ChangeGamemode(gameModeToChangeTo);
        }
        
    }
    public void ChangeGamemode(gamemode g)
    {
        playerToModify.gameMode = g;
        Debug.Log($"Changed the player's gamemode to {playerToModify.gameMode}.");
        Physics.gravity = new Vector3(0, (playerToModify.shipYVelocity* -9.81f) / playerToModify.shipYVelocity, 0);
        
    }
}
