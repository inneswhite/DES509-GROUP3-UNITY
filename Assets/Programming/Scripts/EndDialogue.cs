using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "End dialogue", menuName = "End Dialogue Menu ")]
public class EndDialogue : ScriptableObject
{
    [TextArea(1, 5)]
    public string[] endnpcdialogue;

    [TextArea(1, 5)]
    public string[] endnpcdialogue2;

    [TextArea(1, 5)]
    public string[] endnpcdialogue3;

    [TextArea(1, 5)]
    public string[] endnpcdialogue4;



    [TextArea(1, 5)]
    public string[] endcopdialogue;

    [TextArea(1, 5)]
    public string[] endcopdialogue2;

    [TextArea(1, 5)]
    public string[] endcopdialogue3;







    public string[] EndNPCDialogue => endnpcdialogue;

    public string[] EndNPCDialogue2 => endnpcdialogue2;

    public string[] EndNPCDialogue3 => endnpcdialogue3;

    public string[] EndNPCDialogue4 => endnpcdialogue4;

    public string[] EndCopDialogue => endcopdialogue;

    public string[] EndCopDialogue2 => endcopdialogue2;

    public string[] EndCopDialogue3 => endcopdialogue3;

}
