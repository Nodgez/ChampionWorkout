using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Logon : MonoBehaviour {

    public Dropdown dropDown;
    public Text text;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void Log()
    {
        SummonerRegion sr = (SummonerRegion)dropDown.value;
        string n = text.text;

        StartCoroutine(ReadRiotAPI.Instance.GetSummoner(sr, n));
    }
}
