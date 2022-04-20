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
    public string[] takesyringedialogue;         //CONFISCATE SINGLE ITEM

    [TextArea(1, 5)]
    public string[] takerobotdialogue;


    [TextArea(1, 5)]                              
    public string[] bothitemsdialogue;                       //CONFISCATE TWO ITEMS

    [TextArea(1, 5)]
    public string[] giverobotdialogue;

    [TextArea(1, 5)]
    public string[] givesyringedialogue;

    [TextArea(1, 5)]
    public string[] takebothdialogue;





    public string[] PlayerStartDialogue=>playerstartdialogue;        //start

    public string[] PlayerStartDialogue2 => playerstartdialogue2;

    public string[] PlayerStartDialogue3 => playerstartdialogue3;


                                                                 //After quest dialogue that is displayed
    public string[] PlayerOptionDialogue => playeroptiondialogue;

    public string[] PlayerOptionDialogue2 => playeroptiondialogue2;



    public string[] BothItemsDialogue => bothitemsdialogue;     //final outcome that is displayed 

    public string[] GiveRobotDialogue => giverobotdialogue;

    public string[] GiveSyringeDialogue => givesyringedialogue;

    public string[] TakeBothDialogue => takebothdialogue;


    public string[] TakeSyringeDialogue => takesyringedialogue;

    public string[] TakeRobotDialogue => takerobotdialogue;





}
