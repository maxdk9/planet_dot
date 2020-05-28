using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class DotBase : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer selectionRenderer;
    private bool selected;
    
    public Side side;
    public bool Selected
    {
        get => selected;
        set
        {
            selected = value;
            selectionRenderer.enabled = value;
        }
    }

    

    
}
