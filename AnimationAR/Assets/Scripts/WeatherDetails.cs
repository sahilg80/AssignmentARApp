using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class WeatherDetails
{
    public string timezone;
    public CurrentTemp current;
    
    
}
[Serializable]
public class CurrentTemp{
    public string temp;
    public string humidity;
    public WeatherMain[] weather;
}

[Serializable]
public class WeatherMain{
    public string id;
    public string main;
    public string description;
    public string icon;
}
