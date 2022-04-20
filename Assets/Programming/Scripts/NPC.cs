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




    [TextArea(1, 5)]
    public string[] choicedialogue;

    [TextArea(1, 5)]
    public string[] takesyringedialogue;

    [TextArea(1, 5)]
    public string[] takerobotdialogue;

    [TextArea(1, 5)]
    public string[] takebothdialogue;

    [TextArea(1, 5)]
    public string[] thanksdialogue;


    public string[] NPCDialogue => npcdialogue;             //start 

    public string[] NPCDialogue2 => npcdialogue2;


    public string[] ChoiceDialogue => choicedialogue;

    public string[] TakeSyringeDialogue => takesyringedialogue;

    public string[] TakeRobotDialogue => takerobotdialogue;

    public string[] TakeBothDialogue => takebothdialogue;

    public string[] ThanksDialogue => thanksdialogue;


}
