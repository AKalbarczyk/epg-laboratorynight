using UnityEngine;
using System.Collections;

public class FogScript : MonoBehaviour {


    private bool revertFogState = true;
    void OnPreRender() {
        revertFogState = RenderSettings.fog;
        RenderSettings.fog = enabled;
    }
    void OnPostRender() {
        RenderSettings.fog = revertFogState;
    }
}

