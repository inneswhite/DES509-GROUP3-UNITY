using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypingSpeed : MonoBehaviour
{
    [SerializeField]
    private float speed;
   
    public Coroutine Run(string dialoguetext,Text nametext)
    {
       return  StartCoroutine(TypeText(dialoguetext,nametext));
    }

    private IEnumerator TypeText(string dialoguetext,Text nametext)
    {
        nametext.text = string.Empty;
        float t = 0;
        int characterindex = 0;

        while(characterindex<dialoguetext.Length)
        {
            t += Time.deltaTime*speed; // time increment 
            characterindex = Mathf.FloorToInt(t);   // stores value 
            characterindex = Mathf.Clamp(characterindex, 0, dialoguetext.Length);
            nametext.text = dialoguetext.Substring(0, characterindex);

            yield return null;
        }

        nametext.text = dialoguetext;
    }
}
