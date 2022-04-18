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

    [TextArea(3, 15)]
    public string[] choicedialogue;

    [TextArea(1, 15)]
    public string[] finaldialogue;

    [TextArea(1, 15)]
    public string[] finaldialogue2;

    [TextArea(1, 15)]
    public string[] finaldialogue3;

    [TextArea(1, 5)]
    public string[] singleitemdialogue;

    public string[] NPCDialogue => npcdialogue;

    public string[] questDialogue => questdialogue;

    public string[] ChoiceDialogue => choicedialogue;

    public string[] FinalDialogue => finaldialogue;

    public string[] FinalDialogue2 => finaldialogue2;

    public string[] FinalDialogue3 => finaldialogue3;

    public string[] SingleItemDialogue => singleitemdialogue;

}
