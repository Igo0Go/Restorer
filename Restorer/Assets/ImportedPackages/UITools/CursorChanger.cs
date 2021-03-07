using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorChanger : MonoBehaviour
{
    public Texture2D defaultCursore;
    public Texture2D activeCursor;

    public bool CursorActiveState
    { 
        set
        {
            Cursor.SetCursor(value ? activeCursor : defaultCursore, Vector2.zero, CursorMode.Auto);
        }
    }
}
