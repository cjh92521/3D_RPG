using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SheetManager : MonoBehaviour
{
    const string APPS_PATH = "https://script.google.com/macros/s/AKfycbzO0j7m6SyPa_G-KExWUd6fbzFVugcU-B--VHW1MAHiQYHcP9vPJRKGjxFMgiujohBwTw/exec";
    public static SheetManager Instance { get;private set; }
    private void Awake()
    {
        Instance = this;
    }


    [ContextMenu("Post")]
    public void Post()
    {
        WWWForm form = new WWWForm();
        form.AddField("order", "signUp");
        form.AddField("userID","test20");
        form.AddField("password","tewst!20");
        form.AddField("nickname","tester20");

        StartCoroutine(PostWeb(form));
    }
    IEnumerator PostWeb(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(APPS_PATH, form))
        {
            yield return www.SendWebRequest();

            if (www.isDone)
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }

}
