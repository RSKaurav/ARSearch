using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoogleService : MonoBehaviour {
    public PictureFactory pictureFactory;
    public Text buttonText;
    private const string API_KEY = "AIzaSyDRlYEZGYDl6f4YLj7NAbUQYdG_AeRhywI";

    public void GetPicture()
    {
        StartCoroutine(PictureRoutine());
    }

    IEnumerator PictureRoutine()
    {
        string query = buttonText.text;
        buttonText.transform.parent.gameObject.SetActive(false);
        query = WWW.EscapeURL(query + " images");
        pictureFactory.DeleteOldPictures();
        Vector3 cameraForward = Camera.main.transform.forward;

        int rowNum = 1;
        for(int i=1; i<=60;i += 10){
            string url = "https://www.googleapis.com/customsearch/v1?q=" + query + "&cx=016925171207090246864%3Aowoq0xec9s0&filter=1&num=10&searchType=image&start=" + i + "&fields=items%2Flink&key=" + API_KEY;
            WWW www = new WWW(url);
           
            yield return www;
            pictureFactory.CreateImages(ParseResponse(www.text), rowNum, cameraForward);
            rowNum++;
        }
        yield return new WaitForSeconds(5f);
        buttonText.transform.parent.gameObject.SetActive(true);
    }

    List<string> ParseResponse(string text){
         List <string> urlList = new List<string>();
        string []urls = text.Split('\n');
        foreach(string line in urls){
            if(line.Contains("link")){
                string url = line.Substring(12,line.Length-13);
                if(url.Contains(".jpg") || url.Contains(".png")){
                    urlList.Add(url);
                }

            }
        }
        return urlList;
    }
}
 