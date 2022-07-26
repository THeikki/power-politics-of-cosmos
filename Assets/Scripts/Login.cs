using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public Text alertText;
    public InputField username;
    public InputField password;
    public Button loginButton;
    public Button quitButton;
    public Button linkButton;

    void Start()
    {
        alertText.text = "";
        username.text = "";
        password.text = "";
        loginButton.interactable = false;
        CheckIfAllCrecentialsIsGiven();
    }

    public void CheckIfAllCrecentialsIsGiven()
    {
        if (username.text != "" && password.text != "")
        {
            loginButton.interactable = true;     
        }
        else
        {
            loginButton.interactable = false;
        }
    }

    public void Update()
    {
        CheckIfAllCrecentialsIsGiven();
    }

    public void LinkToWebpage()
    {
        Application.OpenURL("https://project-webpage.herokuapp.com");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ButtonClicked()
    {
        string url = String.Format("https://project-game-api.herokuapp.com/api/users/login");
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.ContentType = "application/json";
        request.Method = "POST";

        try
        {
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = "{\"username\":\"" + username.text + "\",\"password\":\"" + password.text + "\"}";

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
        }
        catch(WebException error)
        {
            Debug.Log(error.Status);
            alertText.text = error.Status.ToString();
        }
        
        try
        {
            var httpResponse = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                string result = streamReader.ReadToEnd();
                
                if (result.Length > 40)
                {
                    string token;
                    string id;
                    string[] resultParts = result.Split(':', ',');

                    token = resultParts[1].Trim('"');
                    id = resultParts[3].Trim('"', '}');
                    PlayerPrefs.SetString("playerToken", token);
                    PlayerPrefs.SetString("playerId", id);
                    PlayerPrefs.Save();

                    SceneManager.LoadScene("Main Menu");
                }
                
                else
                {
                    username.text = "";
                    password.text = "";
                    string alert;
                    string[] resultParts = result.Split(':');
                    alert = resultParts[1].Trim('"', '}');
                    loginButton.interactable = false;
                    alertText.text = alert;
                    return; 
                }           
            }
        }
        catch (WebException error)
        {
            string alert = error.Status.ToString();
            Debug.Log(error);
            if (alert == "ConnectFailure")
            {
                alertText.text = alert + "\n" + "Lost connection to server";
            }
            else if (alert == "NameResolutionFailure")
            {
                alertText.text = alert + "\n" + "Please check internet connection!";
            }
            else
            {
                alertText.text = alert;
            }
        }
    }
}
