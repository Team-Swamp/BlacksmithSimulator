# Blacksmith Simulator

Blacksmith Simulator is a game made for the GMTK gamejam 2023, with the thema roles reversed.

##

```cs
using UnityEngine;

public class TheTeam : MonoBehaviour
{
    private string[] gameDevs;
    private string[] gameArtists;
    private string songArtist;
    
    private void Start()
    {
        gameDevs[0] = "Bas de Reus";
        gameDevs[1] = "Ryan de Fost";
        gameDevs[2] = "Tatum de Vries";
        gameDevs[3] = "Björn Emil Oudekerk";
        
        gameArtists[0] = "Björn Emil Oudekerk";
        gameArtists[1] = "Bas de Reus";

        songArtist = "Ruud de Vries";

        DebugEveryonesName();
    }

    private void DebugEveryonesName()
    {
        foreach (var gameDev in gameDevs)
        {
            Debug.Log(gameDev);
        }
        
        foreach (var gameArtist in gameArtists)
        {
            Debug.Log(gameArtist);
        }
        
        Debug.Log(songArtist);
    }
}
```