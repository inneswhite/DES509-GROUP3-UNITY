using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player dialogue", menuName = "PlayerDialogue Menu ")]
public class PlayerCop:ScriptableObject
{
    public string copName;



    [TextArea(1, 5)]
    public string[] playerstartdialogue;

    [TextArea(1, 5)]
    public string[] playeroptiondialogue;

    [TextArea(1, 5)]
    public string[] playeroptiondialogue2;

    [TextArea(1, 5)]
    public string[] playeroptiondialogue3;

    [TextArea(1, 5)]
    public string[] singleitemdialogue;         //CONFISCATE SINGLE ITEM

    [TextArea(1, 5)]
    public string[] singleitemdialogue2;


    [TextArea(1, 5)]                              
    public string[] bothitemsdialogue;                       //CONFISCATE TWO ITEMS

    [TextArea(1, 5)]
    public string[] bothitemsdialogue2;

    [TextArea(1, 5)]
    public string[] bothitemsdialogue3;

    [TextArea(1, 5)]
    public string[] bothitemsdialogue4;

    [TextArea(1, 5)]
    public string[] bothitemsdialogue5;




    public string[] PlayerDialogue=>playerstartdialogue;        //start


                                                                 //After quest dialogue
    public string[] PlayerOptionDialogue => playeroptiondialogue;

    public string[] PlayerOptionDialogue2 => playeroptiondialogue2;

    public string[] PlayerOptionDialogue3 => playeroptiondialogue3;


    public string[] BothItemsDialogue => bothitemsdialogue;     //final outcome

    public string[] BothItemsDialogue2 => bothitemsdialogue2;

    public string[] BothItemsDialogue3 => bothitemsdialogue3;

    public string[] BothItemsDialogue4 => bothitemsdialogue4;

    public string[] BothItemsDialogue5 => bothitemsdialogue5;

    public string[] SingleItemDialogue => singleitemdialogue;

    public string[] SingleItemDialogue2 => singleitemdialogue2;





}
