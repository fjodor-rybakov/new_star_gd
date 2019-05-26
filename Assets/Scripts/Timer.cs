using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text counterText;

    public int seconds, minutes;
    
    // Start is called before the first frame update
    void Start()
    {
        counterText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        minutes = (int) (Time.time / 60f);
        seconds = (int) (Time.time % 60f);
        counterText.text = $"{minutes:00} : {seconds:00}";
    }
}
