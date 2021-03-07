using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LearnScript : MonoBehaviour
{
    [SerializeField]
    private List<LearnItem> learnItems;
    [SerializeField]
    private GameObject tipPanel;
    [SerializeField]
    private Text tipText;

    [SerializeField]
    private UnityEvent startAction;

    public void InvokeOnStartActionAfterTime(float time)
    {
        StartCoroutine(InvokeStartEventCoroutine(time));
    }

    public void NextTip(float time) 
    {
        StartCoroutine(NextTipCoroutine(time));
    }

    public void TipComplete()
    {
        tipPanel.SetActive(false);
        learnItems[0].ofterLearnEvent?.Invoke();
        learnItems.RemoveAt(0);
    }

    private IEnumerator NextTipCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        tipPanel.SetActive(true);
        tipText.text = learnItems[0].message;
    }

    private IEnumerator InvokeStartEventCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        startAction?.Invoke();
    }
}

[System.Serializable]
public class LearnItem
{
    public string message;
    public UnityEvent ofterLearnEvent;
}
