# StoryScroller
Some Unity c# code for exposition story text.

![typing](https://user-images.githubusercontent.com/13487583/215347480-15e09883-4c24-4267-8925-62389635fead.gif)

## What is this?
* simulate the experience of watching somebody type into a command prompt
* use secret '█' character to insert brief hesitations
* use secret '⌐' character to backspace and remove a typed character
* use secret 'µ' character to clearscreen
* the | cursor will blink at the end
* newlines will push all lines up, scrolling them away at X value
* supports adding a sound effect for typing sound effects

### Why?
This was made to support a story in a game that is delivered through sequences of watching
the text type out on screen.

### Anything else?
Yes. There is a class called TypeWriterText.cs that can go directly on a text object to make it type in.
That component is not as flexibile as the StoryScroller.cs. It was an older system I reused many times and I included it here.

## Scene set up.
1. Put StoryScroller.cs on a gameObject in the scene.
2. For my ChapterOne.cs example, put that on a gameObject too.
3. link Text_obj on the StoryScroller.cs to a textmesh pro text object in your scene.
4. Hit play and ChapterOne will begin.
5. To write your own lines into StoryScroller, see ChapterOne example. It's very simple.



