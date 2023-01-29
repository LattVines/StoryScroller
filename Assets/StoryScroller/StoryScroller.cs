using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class StoryScroller : MonoBehaviour
{

    // █ is ascii 219, and it is a 
    //    h e s i t a t i o n of three cursor blinks 1.5 seconds

    public TMP_Text text_obj;
    public AudioClip sfx_click;
    public int typeXBottomLines = 18;
    public char hesitation_character = '█';
    public char backspace_character = '⌐';
    public char clearScreen_character = 'µ';
    float type_speed_random = 0.05f;//notice the delay with this inside LineTyper. It is slightly randomized
    AudioSource speaker;
    Queue<TypeLine> buffer = new Queue<TypeLine>();//will consume lines from buffer during typing

    //private class to hold what is needed for each line
    private class TypeLine
    {
        public TypeLine(string s, float t)
        {
            this.text = s;
            this.waitAfter = t;
        }

        public string text = "";
        public float waitAfter = 0f;
    }

    void Start()
    {
        speaker = this.gameObject.AddComponent<AudioSource>();
        StartCoroutine(LineTyper());
    }

    //Use this to Type a line
    public void Write(string toType, float waitAfter = 0f)
    {
        buffer.Enqueue(new TypeLine(toType, waitAfter));
    }

    IEnumerator LineTyper()
    {
        while (true)
        {
            yield return null;
            if (buffer.Count != 0)
            {
                TypeLine typeLine = buffer.Dequeue();
                string typeThis = typeLine.text;
                float waitAfter = typeLine.waitAfter;

                while (typeThis != "")
                {
                    string character = typeThis.Substring(0, 1);//consume the first character

                    //█ will pause briefly. █ will not be typed
                    //cursor will blink a few times
                    if (character == hesitation_character.ToString())
                    {
                        text_obj.text = text_obj.text.Replace("|", "");
                        yield return new WaitForSeconds(0.5f);
                        text_obj.text += "|";
                        yield return new WaitForSeconds(0.5f);
                        text_obj.text = text_obj.text.Replace("|", "");
                        yield return new WaitForSeconds(0.5f);
                        typeThis = typeThis.Substring(1);// this skips forward and skips the hesitation_character
                    }
                    //will remove a character to simulate a backspace
                    else if (character == backspace_character.ToString())
                    {
                        text_obj.text = text_obj.text.Substring(0, text_obj.text.Length - 1);
                        typeThis = typeThis.Substring(1);// this skips forward and skips the backspace character
                    }
                    else if (character == clearScreen_character.ToString())
                    {
                        text_obj.text = "";
                        typeThis = typeThis.Substring(1);// this skips forward and skips the clearscreen character
                    }
                    else
                    {
                        text_obj.text = text_obj.text.Replace("|", "");
                        text_obj.text += character + "|";
                        typeThis = typeThis.Substring(1);//then set TypeThis to everything After the 1st character
                        if (sfx_click) speaker.PlayOneShot(sfx_click);
                    }

                    yield return new WaitForSeconds(type_speed_random + (type_speed_random * UnityEngine.Random.value));
                }

                //while waiting for next line,
                //blink cursor and deduct time
                //but ONLY if the waitAfter time is long enough to notice
                if (waitAfter > 1f)
                {
                    float startTimer = Time.time;
                    while (waitAfter > 0)
                    {
                        waitAfter -= Time.time - startTimer;
                        //Debug.Log($"time left: {waitAfter}");

                        text_obj.text = text_obj.text.Replace("|", "");
                        yield return new WaitForSeconds(0.5f);
                        text_obj.text += "|";
                        yield return new WaitForSeconds(0.5f);
                    }
                }
                else
                {
                    yield return new WaitForSeconds(waitAfter);
                }


                text_obj.text = GetXBottomLines(typeXBottomLines, text_obj.text);
            }
            //When buffer is EMPTY blink the cursor
            else
            {
                text_obj.text = text_obj.text.Replace("|", "");
                yield return new WaitForSeconds(0.5f);
                text_obj.text += "|";
                yield return new WaitForSeconds(0.5f);
            }

        }
    }

    //a string post-processor, to be used
    //after text_obj has been written to.
    //returns: the last X lines of a string, delimit is \n
    private string GetXBottomLines(int x, string bigString)
    {
        string s = "";
        string[] allLines = bigString.Split("\n");
        Stack<String> stack = new Stack<string>(); ;
        for (int i = 0; i < x; i++)
        {
            if (i >= allLines.Length) break;

            string token = allLines[allLines.Length - i - 1];
            stack.Push(token);
        }

        foreach (string str in stack)
        {
            s += str + "\n";//add the newline BACK in, the split will have removed them
        }

        return s;
    }

    //types a variable number of newlines
    public void NewLine(int howMany = 1)
    {
        string s = "";
        for (int i = 0; i < howMany; i++)
        {
            s += "\n";
        }

        Write(s);
    }


}
