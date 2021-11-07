using System.Collections;
using UnityEngine;


public class ScrollingImage : MonoBehaviour {

    public float horizontalScrollSpeed = 0.05f;
    public float verticalScrollSpeed = 0;

    private bool scroll = true;

    public void FixedUpdate() {
        if (scroll) {
            float verticalOffset = Time.time * verticalScrollSpeed;
            float horizontalOffset = Time.time * horizontalScrollSpeed;
            GetComponent<Renderer>().material.mainTextureOffset = new Vector2(horizontalOffset, verticalOffset);
        }
    }

    public void DoActivateTrigger() {
        scroll = !scroll;
    }

}
