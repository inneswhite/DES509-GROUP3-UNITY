using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NPC dialogue", menuName = "Dialogue Menu ")]
public class NPC : ScriptableObject
{
    public string NPCName;

    [TextArea(1, 2)]
    public string[] npcdialogue;

    [TextArea(1, 2)]
    public string[] npcdialogue2;




    [TextArea(3, 15)]
    public string[] choicedialogue;

    [TextArea(1, 15)]
    public string[] finaldialogue;

    [TextArea(1, 15)]
    public string[] finaldialogue2;

    [TextArea(1, 15)]
    public string[] finaldialogue3;

    [TextArea(1, 5)]
    public string[] finaldialogue4;


    public string[] NPCDialogue => npcdialogue;             //start 

    public string[] NPCDialogue2 => npcdialogue2;


    public string[] ChoiceDialogue => choicedialogue;

    public string[] FinalDialogue => finaldialogue;

    public string[] FinalDialogue2 => finaldialogue2;

    public string[] FinalDialogue3 => finaldialogue3;

    public string[] FinalDialogue4 => finaldialogue4;


}
