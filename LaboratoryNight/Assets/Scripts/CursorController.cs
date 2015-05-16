using UnityEngine;
using System.Collections;

public class CursorController : MonoBehaviour {

	// Use this for initialization
    public Texture2D cursorTexture;
    private Vector2 hotSpot = Vector2.zero;
	void Start ()
    {
        Cursor.visible = true;
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnMouseEnter()
    {
        //Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
    }
    void OnMouseExit()
    {
        //Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
