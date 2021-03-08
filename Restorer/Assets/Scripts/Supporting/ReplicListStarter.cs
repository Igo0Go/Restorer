using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplicListStarter : MonoBehaviour
{
    [SerializeField]
    private bool playOnAwake = false;

    [SerializeField]
    private List<ReplicItem> items;

    [SerializeField]
    private bool clearOldReplics = false;

    [SerializeField]
    private ReplicDispether dispether;

    private void Start()
    {
        if (playOnAwake)
            StartReplics();
    }

    public void StartReplics()
    {
        if (clearOldReplics)
            dispether?.ClearList();
        dispether?.AddInList(items);
        Destroy(gameObject);
    }
}
