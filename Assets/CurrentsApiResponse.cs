using System;

[System.Serializable]
public class CurrentsApiResponse
{
    public string status;
    public NewsArticle[] news;
}

[System.Serializable]
public class NewsArticle
{
    public string id;
    public string title;
    public string description;
    public string url;
    public string author;
    public string image;
    public string language;
    public string[] category;
    public string published;
}