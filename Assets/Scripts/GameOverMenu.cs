using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    public Text pointsText;
    public Button tryAgainButton;
    public Button quitButton;
    public Text alertText;

    public void Start()
    {
        alertText.text = "";
    }

    public void Setup(int score)
    {
        pointsText.text = score.ToString() + " Points";       
    }
    
    public void RestartButton()
    {
        IncreasePlayerData();
        SceneManager.LoadScene("Game");
        Points.points = 0;    
    }

    public void ExitButton()
    {
        UpdatePlayerData();
        SceneManager.LoadScene("Main Menu");
        Points.points = 0;
    }
   
    public void IncreasePlayerData()
    {
        int GameTimes = Int32.Parse(PlayerPrefs.GetString("GameTimes"));
        int HighScore = Int32.Parse(PlayerPrefs.GetString("HighScore"));
        int OverallPoints = Int32.Parse(PlayerPrefs.GetString("OverallPoints"));

        GameTimes += 1;

        if (Points.points > HighScore)
        {
            HighScore = Points.points;
        }
        
        OverallPoints += Points.points;

        PlayerPrefs.SetString("GameTimes", GameTimes.ToString());           
        PlayerPrefs.SetString("HighScore", HighScore.ToString());              
        PlayerPrefs.SetString("OverallPoints", OverallPoints.ToString());          

        PlayerPrefs.Save();
    }

    public void UpdatePlayerData()
    {
        IncreasePlayerData();
        string id = PlayerPrefs.GetString("playerId");
        string token = PlayerPrefs.GetString("playerToken");
        
        int GameTimes = Int32.Parse(PlayerPrefs.GetString("GameTimes"));                 
        int HighScore = Int32.Parse(PlayerPrefs.GetString("HighScore"));
        int OverallPoints = Int32.Parse(PlayerPrefs.GetString("OverallPoints"));

        string url = String.Format("https://project-game-api.herokuapp.com/api/users/update/"+ id);    
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Headers.Add("Authorization", "bearer " + token);
        request.ContentType = "application/json";
        request.Method = "PUT";
        
        try
        {
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = "{\"gameTimes\":\"" + GameTimes + "\",\"highScore\":\"" + HighScore + "\",\"overallPoints\":\"" + OverallPoints + "\"}";

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
        }

        catch(WebException error)
        {
            Debug.Log(error.Status);
        }
        
        try
        {
            var httpResponse = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                string result = streamReader.ReadToEnd();
                //Debug.Log(result);
            }
        }
        catch (WebException error)
        {
            Debug.Log(error);
            alertText.text = error.Status.ToString();
        }
    }
}
