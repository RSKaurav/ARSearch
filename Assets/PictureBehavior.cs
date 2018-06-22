using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureBehavior : MonoBehaviour {

	// Use this for initialization
    public Renderer quadRenderer;
    private Vector3 desiredPosition;

	void Start () {
        transform.LookAt(Camera.main.transform);
        Vector3 desiredAngle = new Vector3(0, transform.localEulerAngles.y, 0);
        transform.rotation = Quaternion.Euler(desiredAngle);
        desiredPosition = transform.localPosition;
        transform.localPosition += new Vector3(0, 20, 0);
	}
	
	// Update is called once per frame
	void Update () {
        transform.localPosition = Vector3.Lerp(transform.localPosition, desiredPosition, Time.deltaTime * 4f);
	}

    public void LoadImage(string url)
    {
        StartCoroutine (LoadImageFromURL(url));
    }

    IEnumerator LoadImageFromURL(string url){
        WWW www = new WWW(url);
        yield return www;
        quadRenderer.material.mainTexture = www.texture;
    }
}
