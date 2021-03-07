using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class UiReactedItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public AudioSource source;
    public ReactItem enterReaction;
    public ReactItem exitReaction;
    public ReactItem clickReaction;

    private CursorChanger changer;

    private void Start()
    {
        GameObject bufer = GameObject.FindGameObjectWithTag("CursorChanger");
        if(bufer != null)
        {
            changer = bufer.GetComponent<CursorChanger>();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(enterReaction.reactClip != null)
            source.PlayOneShot(enterReaction.reactClip);

        enterReaction.reactEvent?.Invoke();
        
        if (changer != null)
            changer.CursorActiveState = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (exitReaction.reactClip != null)
            source.PlayOneShot(exitReaction.reactClip);

        exitReaction.reactEvent?.Invoke();

        if (changer != null)
            changer.CursorActiveState = false;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickReaction.reactClip != null)
            source.PlayOneShot(clickReaction.reactClip);

        clickReaction.reactEvent?.Invoke();
    }
}

[System.Serializable]
public class ReactItem
{
    public AudioClip reactClip;
    public UnityEvent reactEvent;
}
