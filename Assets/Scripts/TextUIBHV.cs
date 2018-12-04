using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextUIBHV : MonoBehaviour {

    [System.Serializable]
    struct ImpairmentDescription
    {
        public string name;
        [TextArea(5,20)]
        public string description;
        public string url;

        public ImpairmentDescription(string name, string description, string url)
        {
            this.name = name;
            this.description = description;
            this.url = url;
        }
    }

    [SerializeField]
    List<ImpairmentDescription> quotes = new List<ImpairmentDescription>();

    [SerializeField]
    Text nameText, descriptionText;
    [SerializeField]
    GameObject ImpairmentInfoScreen, winScreen;

	// Use this for initialization
	void Start () {
        ImpairmentInfoScreen.SetActive(false);
        winScreen.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (ImpairmentInfoScreen.activeSelf)
            {
                GameManager.instance.RestartGame();
                CloseDescription();
            }
            //TODO
            //Will do something to not repeat the same stage
            if (winScreen.activeSelf)
            {
                GameManager.instance.RestartGame();
                CloseWinGame();
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
            if (ImpairmentInfoScreen.activeSelf)
                Application.OpenURL(quotes[ImpairmentManager.instance.ActualImpairment].url);
        
    }

    public void GoToURL()
    {
        Application.OpenURL(quotes[ImpairmentManager.instance.ActualImpairment].url);
    }

    public void CreateDescription()
    {
        nameText.text = quotes[ImpairmentManager.instance.ActualImpairment].name;
        descriptionText.text = quotes[ImpairmentManager.instance.ActualImpairment].description;
        ImpairmentInfoScreen.SetActive(true);
    }

    public void CloseDescription()
    {
        ImpairmentInfoScreen.SetActive(false);
    }

    public void WinGame()
    {
        winScreen.SetActive(true);
    }

    public void CloseWinGame()
    {
        winScreen.SetActive(false);
    }
}
