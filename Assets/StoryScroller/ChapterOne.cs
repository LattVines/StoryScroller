using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterOne : MonoBehaviour
{
    StoryScroller scroll;

    void Start()
    {
        scroll = FindObjectOfType<StoryScroller>();
        StoryComposer();
    }

    //convenience methods
    void Write(string s, float delayAfter = 0, int extraNewLines = 0)
    {
        scroll.Write(s, delayAfter);

        if (extraNewLines > 0)
            scroll.NewLine(extraNewLines);
    }

    void NewLine(int howMany = 1)
    {
        scroll.NewLine(howMany);
    }

    void ClearScreen()
    {
        scroll.ClearScreen();
    }

    //will write the content into the buffer for the StoryScroller
    // █ is ascii 219, and it is a 
    //                             h e s i t a t i o n
    void StoryComposer()
    {
        Debug.Log("StoryComposer Ran");
        Write("Once...", 5f);
        Write("Upon...", 0.5f);
        Write("A Time...", 0.5f);

        Write("A nerd wanted to make █a game");
        Write("A great game█ that people would play");
        Write("and somehow KNOW that it's okay to have fun");
        Write("it's okay to get really deep into something");
        Write("BUT!");
        Write("To do that the nerd needed something█ elusive");
        Write("Something very, very RARE", 0.5f);
        Write("The nerd needed a story", 0.5f);
        Write("Because without a story");
        Write("the nerd had nothing", 0.5f);
        Write("no idea, meant no game");
        Write("and█ without a story");
        Write("what was the point███ :(", 1.3f);

        Write("what's the point in anything?", 1.3f);
        Write("then one day.....");

    }
}
