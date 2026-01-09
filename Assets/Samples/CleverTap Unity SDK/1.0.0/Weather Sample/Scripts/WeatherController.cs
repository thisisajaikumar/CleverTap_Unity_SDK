using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using CleverTap;

public class WeatherController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Text statusText;
    [SerializeField] private Button fetchButton;

    private const float Latitude = 19.07f;
    private const float Longitude = 72.87f;

    private string WeatherUrl =>
        $"https://api.open-meteo.com/v1/forecast?latitude={Latitude}&longitude={Longitude}&timezone=IST&daily=temperature_2m_max";

    private void Start()
    {
        statusText.text = "Tap the button to check weather";
        fetchButton.onClick.AddListener(OnCheckWeatherClicked);
    }

    private void OnCheckWeatherClicked()
    {
        StartCoroutine(GetWeather());
    }

    private IEnumerator GetWeather()
    {
        statusText.text = "Loading weather...";
        fetchButton.interactable = false;

        // Short feedback via native Toast
        CleverTapToast.Show("Fetching weather...");

        using (UnityWebRequest request = UnityWebRequest.Get(WeatherUrl))
        {
            yield return request.SendWebRequest();
            fetchButton.interactable = true;

            if (request.result != UnityWebRequest.Result.Success)
            {
                statusText.text = "Failed to load weather";
                CleverTapToast.Show("Weather API error");
                yield break;
            }

            WeatherApiResponse response =
                JsonUtility.FromJson<WeatherApiResponse>(request.downloadHandler.text);

            string todayDate = response.daily.time[0];
            float todayMaxTemp = response.daily.temperature_2m_max[0];

            // Full data → Unity UI
            statusText.text =
                $"Date: {todayDate}\n" +
                $"Latitude: {response.latitude}\n" +
                $"Longitude: {response.longitude}\n" +
                $"Today's Max Temperature: {todayMaxTemp}°C";

            // Short success feedback → native Toast
            CleverTapToast.Show("Weather loaded");
        }
    }

    [System.Serializable]
    private class WeatherApiResponse
    {
        public float latitude;
        public float longitude;
        public DailyWeather daily;
    }

    [System.Serializable]
    private class DailyWeather
    {
        public string[] time;
        public float[] temperature_2m_max;
    }
}
