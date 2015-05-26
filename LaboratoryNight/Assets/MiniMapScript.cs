using UnityEngine;
using System.Collections;

public class MiniMapScript : MonoBehaviour {
    public Shader unlitShader;
	// Use this for initialization
	void Start () {
        GetComponent<Camera>().SetReplacementShader(unlitShader, "");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
