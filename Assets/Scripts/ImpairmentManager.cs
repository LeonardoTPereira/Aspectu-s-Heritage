using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wilberforce;
using UnityEngine.PostProcessing;

public class ImpairmentManager : MonoBehaviour
{

    public static ImpairmentManager instance;
    [SerializeField]
    private List<int> impairmentList;
    private int actualImpairment;

    void Awake()
    {
        //Singleton
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SelectImpairment()
    {
        actualImpairment = impairmentList[Random.Range(0, impairmentList.Count)];
        Debug.Log("Actual Impairment: " + actualImpairment);
        switch (actualImpairment)
        {
            //Glaucoma
            case 0:
                Player.instance.gameObject.transform.Find("Glaucoma").gameObject.SetActive(true);
                break;
            //Retinitis
            case 1:
                Player.instance.gameObject.transform.Find("Retinitis").gameObject.SetActive(true);
                break;
            //ColorBlind Protanopia
            case 2:
                Camera.main.GetComponent<Colorblind>().Type = 1;
                break;
            //ColorBlind Deuteranopia
            case 3:
                Camera.main.GetComponent<Colorblind>().Type = 2;
                break;
            //ColorBlind Tritanopia
            case 4:
                Camera.main.GetComponent<Colorblind>().Type = 3;
                break;
            case 5:
                Camera.main.GetComponent<PostProcessingBehaviour>().profile.bloom.enabled = true;
                break;
            default:
                break;
        }
    }

    public void RemoveImpairment()
    {
        switch (actualImpairment)
        {
            //Glaucoma
            case 0:
                Player.instance.gameObject.transform.Find("Glaucoma").gameObject.SetActive(false);
                break;
            //Retinitis
            case 1:
                Player.instance.gameObject.transform.Find("Retinitis").gameObject.SetActive(false);
                break;
            case 2:
            case 3:
            case 4:
                GameObject.Find("Main Camera").GetComponent<Colorblind>().Type = 0;
                break;
            case 5:
                Camera.main.GetComponent<PostProcessingBehaviour>().profile.bloom.enabled = false;
                break;
            default:
                break;
        }
    }

    public void RemovePostProcessing()
    {
        Camera.main.GetComponent<PostProcessingBehaviour>().profile.bloom.enabled = false;
    }

}
