using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TypeWriterText : MonoBehaviour
{

    /* this is the very simple typer I built StoryScroller off of. StoryScroller does a lot more.*/


    Text text_obj;
    string string_to_type;
    public AudioSource soundEffectPlayer;//Set in editor
    public AudioClip sfx_click;

    void Awake()
    {
        text_obj = GetComponent<Text>();
        string_to_type = text_obj.text;
        text_obj.text = string.Empty;

    }


    void OnEnable()
    {
        StartCoroutine(TypeWriterEffect());
    }


    public IEnumerator TypeWriterEffect()
    {

        yield return new WaitForSeconds(0.05f);
        text_obj.text = string.Empty;

        int characters_to_type = string_to_type.Length;

        for (int type_spot = 0; type_spot < characters_to_type; type_spot++)
        {
            //saw this tip online years ago. Can't remember who came up with it.
            //but making the remainder of the string invisible and leaving it in the text_obj content
            //avoids wordwrap mid word as it is typed when you hit the boundary.
            text_obj.text = string_to_type.Substring(0, type_spot + 1)
                + "<color=#0000>" + string_to_type.Substring(type_spot + 1) + "</color>";

            if (type_spot % 3 == 0 && soundEffectPlayer != null)
            {
                soundEffectPlayer.PlayOneShot(sfx_click);
            }

            yield return new WaitForSeconds(0.009f);

        }

        yield return new WaitForSeconds(0.2f);
        Destroy(this);
    }

}
