using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using UnityEngine.Networking;
using System.IO;
using Newtonsoft.Json;
using System;

public class LoadWeather : MonoBehaviour
{
    string jsonResult;
    bool isOnLoading = true;

    string condition;
    string condition_description;
    string temperture;
    string windspeed;

    public string cityname;
    

    public Material[] mat = new Material[5];

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadData());
    }

    // Update is called once per frame
    IEnumerator LoadData()
    {
        string GetDataUrl = "https://api.openweathermap.org/data/2.5/weather?q=" + cityname + "&appid=d924df8990f0f42be86692f9df84a25e";
        //string GetDataUrl =
        //   "https://api.openweathermap.org/data/2.5/weather?q=Gumi&appid=d924df8990f0f42be86692f9df84a25e";
  
        using (UnityWebRequest www = UnityWebRequest.Get(GetDataUrl))
        {
            //www.chunkedTransfer = false;
            yield return www.Send();
            if (www.isNetworkError || www.isHttpError) //�ҷ����� ���� ��
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.isDone)
                {
                    // Load Jason Data
                    isOnLoading = false;
                    jsonResult = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);

                    // Use JsonObject Parse
                    JObject jObject = JObject.Parse(jsonResult);
                    JToken jToken = jObject["weather"];

                    foreach (JToken data in jToken)
                    {
                        condition = data["main"].ToString();
                        condition_description = data["description"].ToString();
                    }

                    // use JsonObject ContainsKey
                    JObject root = JObject.Parse(jsonResult);
                    JObject header = (JObject)root["main"];

                    if (header.ContainsKey("temp"))
                        temperture = header["temp"].ToString();

                    header = (JObject)root["wind"];
                    if (header.ContainsKey("speed"))
                        windspeed = header["speed"].ToString();

                    Double temperutre = Double.Parse(temperture);
                    temperutre = temperutre - 273;

                    /*Debug.Log(cityname);
                    Debug.Log("condition : "+condition );
                    Debug.Log("condition_description : "+condition_description);
                    Debug.Log("temperture : "+temperutre);
                    Debug.Log("windspeed : "+windspeed);*/           
                }
            }
        }

        if (condition.Equals("Clear")) //sky - 7
            RenderSettings.skybox = mat[0];
        else if (condition.Equals("Rain")) // sky - 5
            RenderSettings.skybox = mat[1];
        else if (condition.Equals("Clouds")) // sky - 4
            RenderSettings.skybox = mat[2];
        else if (condition.Equals("Snow")) // sky - 11
            RenderSettings.skybox = mat[3];
        else
            RenderSettings.skybox = mat[0]; // sky - 1

    }
    // weather condition
    // clouds, clear, mist, smoke, dust, Thunderstorm, Drizzle, Rain
    // Snow, Ash, Sand
}