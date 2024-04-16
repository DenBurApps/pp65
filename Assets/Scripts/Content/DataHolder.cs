using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Root
{
    public bool onBoarding;

    public List<Properties> properties = new List<Properties>();
}

[Serializable]
public class Properties
{
    public string orderName;
    public string status = "New";
    public string date;
    public string endDate;
    public Client client;
    public Car car;
    public List<string> works = new List<string>();
    public List<Part> parts = new List<Part>();
    public string description;
    public List<string> photoes = new List<string>();
    public string comment;
    public int ID;

}
[Serializable]
public class Part
{
    public string name;
    public float cost;
}
[Serializable]
public class Car
{
    public string brand;
    public string model;
    public string yearManufacture;
    public string code;
    public string description;
}
[Serializable]
public class Client
{ 
    public string name;
    public string contacts;
}
