using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI; // For displaying on TextMeshPro (optional)

public class NewsAPI : MonoBehaviour
{

    // Your API Key
    private const string API_KEY = "2hE5A1blz3DXVXGuDMxOHYqHwlsTk7R8DaHCncYhoYQp0nwy";

    // Currents API endpoint
    private const string API_URL = "https://api.currentsapi.services/v1/latest-news?apiKey=" + API_KEY;

    // UI elements to display the news (optional)
    // public string newsTitleText;
    // public string newsDescriptionText;

    // public TextMeshProUGUI newsText;

    // public TextControll myTextControll;

    public string title;
    public string description;
    public string url;

    void Start()
    {
        // Start fetching articles when the game starts
         StartCoroutine(GetNews());
    }

    IEnumerator GetNews()
    {
        // Send the API request
        UnityWebRequest request = UnityWebRequest.Get(API_URL);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error fetching news: " + request.error);
        }
        else
        {
            // Parse the JSON response
            string jsonResponse = request.downloadHandler.text;
            CurrentsApiResponse response = JsonUtility.FromJson<CurrentsApiResponse>(jsonResponse);

            if (response != null && response.news != null && response.news.Length > 0)
            {
                // Get a random article
                int randomIndex = Random.Range(0, response.news.Length);
                NewsArticle randomArticle = response.news[randomIndex];

                // Extract article details
                title = randomArticle.title;
                description = randomArticle.description;
                url = randomArticle.url;



                // Log the random article
                Debug.Log("Random Article Title: " + title);
                Debug.Log("Random Article Description: " + description);
                Debug.Log("Read More: " + url);

                // Update UI (if applicable)
                // if (newsTitleText != null) newsTitleText = title;
                // if (newsDescriptionText != null) newsDescriptionText = description;
            }
            else
            {
                Debug.LogWarning("No articles found.");
            }
        }
    }

    public void GetNewsAPI()
    {
        title = "";
        description = "";
        StartCoroutine(GetNews());

    }
}
