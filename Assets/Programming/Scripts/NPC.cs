using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NPC dialogue", menuName = "Dialogue Menu ")]
public class NPC : ScriptableObject
{
    public string NPCName;

    [TextArea(3, 15)]
    public string[] npcdialogue;

    [TextArea(3, 15)]
    public string[] questdialogue;

    public string[] NPCDialogue => npcdialogue;

    public string[] questDialogue => questdialogue;
 
}
