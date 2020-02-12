using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class WebRequest : MonoBehaviour
{
    public static WebRequest instance;
    [SerializeField] private string url;
    [SerializeField] private TextMeshProUGUI text;
    protected User data;

    private List<char> caracteres = new List<char>();
    private List<char> caracteres2 = new List<char>();

    public User Data { get => data; }
    private void Start()
    {
        instance = this;
        data = new User();
        StartCoroutine(GetWebRequest());

        //GetWeb();
    }

    void GetWeb()
    {
        User user = JsonUtility.FromJson<User>(url);
        print(user.user);
    }

    int counter;
    private IEnumerator GetWebRequest()
    {   
        UnityWebRequest request = UnityWebRequest.Get(url);
   

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
            print(request.error);
        else
        {
            var web = request.downloadHandler;
            for (int i = 0; i < web.data.Length; i++)
            {
                if(web.text[i] == 34)
                    counter++;


                if (counter == 3)
                {
                    if(web.text[i] != 34)
                        caracteres.Add(web.text[i]);
                }

                if(counter == 7)
                {
                    if (web.text[i] != 34)
                        caracteres2.Add(web.text[i]);
                }
            }
            char[] chars = new char[caracteres.Count];

            for (int i = 0; i < caracteres.Count; i++)
                chars[i] = caracteres[i];

            data.user = chars.ArrayToString();
            chars = null;
            chars = new char[caracteres2.Count];

            for (int i = 0; i < caracteres2.Count; i++)
                chars[i] = caracteres2[i];


            data.password = chars.ArrayToString();
        }
    }
}

[System.Serializable]
public class User
{
    public string user;
    public string password;
}
