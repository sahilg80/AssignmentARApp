using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.Networking;

public class WeatherAPIManagement : MonoBehaviour
{
    private GameObject humidityText;
    private GameObject temperatureText;
    private GameObject weatherText;
    
    void Start(){

        
        humidityText = GameObject.FindGameObjectWithTag("HumidityText");
        
        temperatureText = GameObject.FindGameObjectWithTag("TemperatureText");
        
        weatherText = GameObject.FindGameObjectWithTag("WeatherText");

        #if UNITY_ANDROID
            if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission (UnityEngine.Android.Permission.FineLocation))
                {
                   UnityEngine.Android.Permission.RequestUserPermission (UnityEngine.Android.Permission.FineLocation);
                }
        #elif UNITY_IOS
            PlayerSettings.iOS.locationUsageDescription = "Details to use location";
        #endif
        StartCoroutine(GPSLoc());
        
    }

    IEnumerator GPSLoc()
    {
        while(!UnityEngine.Android.Permission.HasUserAuthorizedPermission (UnityEngine.Android.Permission.FineLocation)){
            yield return null;
        }
        print(Input.location.isEnabledByUser);
        if (!Input.location.isEnabledByUser)
            yield break;

        UnityEngine.Input.location.Start(500f, 500f);
                
        int maxWait = 15;
        while (UnityEngine.Input.location.status == LocationServiceStatus.Initializing && maxWait > 0) {
            yield return new WaitForSecondsRealtime(1);
            maxWait--;
        }

        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            string apiUrl = "https://api.openweathermap.org/data/2.5/onecall?lat="+Input.location.lastData.latitude.ToString()+"&lon="+Input.location.lastData.longitude.ToString()+"&exclude=hourly,daily&appid=60d5403014ff1a4945b4ee23e417ca0c&units=metric";
            StartCoroutine(GetWeatherData(apiUrl));
            //print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
            
        }

        Input.location.Stop();
    }

    

    IEnumerator GetWeatherData(string url)
    {

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            webRequest.downloadHandler = new DownloadHandlerBuffer();

            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                
                print(webRequest.error);
            }
            else
            {
                string data = System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);
                
                WeatherDetails responseData = JsonUtility.FromJson<WeatherDetails>(data);
                string tempValue = string.Format("{0:0.##}", System.Convert.ToDecimal(responseData.current.temp));
                
                weatherText.GetComponent<TextMeshProUGUI>().SetText("Weather : "+responseData.current.weather[0].main);
                temperatureText.GetComponent<TextMeshProUGUI>().SetText("Temp : "+tempValue+" C");
                humidityText.GetComponent<TextMeshProUGUI>().SetText("Humidity : "+responseData.current.humidity);
                
            }

        }
    }
}