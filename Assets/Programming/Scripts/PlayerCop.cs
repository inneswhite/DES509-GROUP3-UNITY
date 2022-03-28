using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player dialogue", menuName = "PlayerDialogue Menu ")]
public class PlayerCop:ScriptableObject
{
    public string copName;


    public enum playerState
    {
        idle,Talking
    }
    public playerState currentState;
    public enum PlayerRelations
    {
        Default,Neutral,Good,Respected,Loved,Bad,Hated
    }
    public PlayerRelations currentrelations;

    [TextArea(3, 15)]
    public string[] playerstartdialogue;

    [TextArea(3, 15)]
    public string[] playeroptiondialogue;

    [TextArea(3, 15)]
    public string[] playeroptiondialogue2;

    [TextArea(3, 15)]
    public string[] playeroptiondialogue3;


    public string[] PlayerDialogue=>playerstartdialogue;        //start


                                                                 //After quest dialogue
    public string[] PlayerOptionDialogue => playeroptiondialogue;

    public string[] PlayerOptionDialogue2 => playeroptiondialogue2;

    public string[] PlayerOptionDialogue3 => playeroptiondialogue3;




}
