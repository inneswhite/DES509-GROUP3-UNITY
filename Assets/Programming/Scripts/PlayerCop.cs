using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player dialogue", menuName = "PlayerDialogue Menu ")]
public class PlayerCop:ScriptableObject
{
    public string copName;



    [TextArea(1, 2)]
    public string[] playerstartdialogue;

    [TextArea(1, 2)]
    public string[] playerstartdialogue2;

    [TextArea(1, 2)]
    public string[] playerstartdialogue3;

    [TextArea(1, 2)]
    public string[] playeroptiondialogue;

    [TextArea(1, 2)]
    public string[] playeroptiondialogue2;


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





    public string[] PlayerStartDialogue=>playerstartdialogue;        //start

    public string[] PlayerStartDialogue2 => playerstartdialogue2;

    public string[] PlayerStartDialogue3 => playerstartdialogue3;


                                                                 //After quest dialogue that is displayed
    public string[] PlayerOptionDialogue => playeroptiondialogue;

    public string[] PlayerOptionDialogue2 => playeroptiondialogue2;



    public string[] BothItemsDialogue => bothitemsdialogue;     //final outcome that is displayed 

    public string[] BothItemsDialogue2 => bothitemsdialogue2;

    public string[] BothItemsDialogue3 => bothitemsdialogue3;

    public string[] BothItemsDialogue4 => bothitemsdialogue4;


    public string[] SingleItemDialogue => singleitemdialogue;

    public string[] SingleItemDialogue2 => singleitemdialogue2;





}
