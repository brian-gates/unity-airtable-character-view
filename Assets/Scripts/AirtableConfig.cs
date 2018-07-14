using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using UnityEngine.Networking;

public class AirtableQuery {

	public string table = "Characters";
	public string view = "Main View";
	public int maxRecords = 100;
	public AirtableQuery(string table, string view = "Main View", int maxRecords = 100) {
		
	}

	public UnityWebRequest getRequest(string token) {
    UnityWebRequest www = UnityWebRequest.Get ("https://api.airtable.com/v0/appQ3dmrqe9MBHoos/" + table + "?maxRecords=" + maxRecords + "&view=" + view);
    www.SetRequestHeader ("Authorization", "Bearer " + token);
    return www;
	}

}

public class AirtableConfig : MonoBehaviour {

	public bool isValid;
	public string token {
		get { return tokenInput.text; }
		set { tokenInput.text = value; }
	}

	public TMP_InputField tokenInput;

	public void Start() {
		token = PlayerPrefs.GetString("Airtable Token");
	}

	public void Submit() {
		PlayerPrefs.SetString("Airtable Token", token);
		StartCoroutine(TestConfig());
	}

  public IEnumerator TestConfig () {
		AirtableQuery query = new AirtableQuery("Character", "Main View");
    UnityWebRequest www = query.getRequest(token);
    yield return www.Send ();

    if (www.isNetworkError || www.isHttpError) {
      Debug.Log("AirtableConfig is not valid");
      isValid = false;
    } else {
      Debug.Log("AirtableConfig is valid");
      Debug.Log (www.downloadHandler.text);
			isValid = true;
    }
  }

	public void UpdateToken(string text) {
		PlayerPrefs.SetString("Airtable Token", text);
	}

}
