using UnityEngine;
using SimpleJSON;
using System.Collections;

public enum SummonerRegion
{
    euw = 0
}

public class ReadRiotAPI {
	
	string url = "https://euw.api.pvp.net/api/lol/euw/v1.4/summoner/by-name/Nodgez?api_key=a6a80146-1c66-4544-abeb-1c08303a6674";
	byte[] data;

    private bool runningAPI = false;
    private SaveData saveData;

    private static ReadRiotAPI instance;

	public void ParseString()
	{
		string result = System.Text.Encoding.UTF8.GetString(data);
		Debug.Log (result);

		JSONNode node = JSON.Parse (result);
		Debug.Log (node[0][1]);
	}

    public IEnumerator PopulateChampionSelection()
    {
        WWW www = new WWW("https://euw.api.pvp.net/api/lol/euw/v1.2/champion?api_key=a6a80146-1c66-4544-abeb-1c08303a6674");
        byte[] data;
        yield return www;
        data = www.bytes;

        string result = System.Text.Encoding.UTF8.GetString(data);
        JSONNode node = JSON.Parse(result);
        foreach (JSONNode n in node[0].Childs)
        {
            Debug.Log(n[0]);
        }
    }

    public IEnumerator GetSummoner(SummonerRegion summonerRegion, string summonerName)
    {
        runningAPI = true;
        WWW www = new WWW("https://euw.api.pvp.net/api/lol/euw/v1.2/champion?api_key=a6a80146-1c66-4544-abeb-1c08303a6674");
        byte[] data;

        yield return www;
        data = www.bytes;
        string result = System.Text.Encoding.UTF8.GetString(data);
        JSONNode node = JSON.Parse(result);

        saveData = new SaveData(Application.persistentDataPath + "/ChampionWorkout/" + node[0][1] + ".cw");
        saveData["name"] = summonerName;
        saveData["region"] = summonerRegion;

        saveData.Save();

        runningAPI = false;
    }

    public bool RunningAPI
    {
        get { return runningAPI; }
    }

    public static ReadRiotAPI Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ReadRiotAPI();
            }

            return instance;
        }
    }
}
