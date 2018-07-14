using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;


[Serializable]
public class Thumbnail {
  public string url;
  public int height;
  public int width;
}

[Serializable]
public class Thumbnails {
  public Thumbnail small;
  public Thumbnail large;

}

[Serializable]
public class Attachment {
  public string id;
  public string url;
  public string filename;
  public int size;
  public string type;
  public int width;
  public int height;
  public Thumbnails thumbnails;

}

[Serializable]
public class CharacterFields {
  public string Name;
  public Attachment[] Images;
  public float HPPercent;

}

[Serializable]
public class CharacterRecord {
  public string id;
  public CharacterFields fields;
}

[Serializable]
public class CharacterResponse {
  public CharacterRecord[] records;
}

public class CharacterTable : MonoBehaviour {

  public AirtableConfig airtableConfig;

  public int maxRecords = 20;
  public string view = "Main View";

  public delegate void UpdateAction ();
  public static event UpdateAction OnUpdate;

  public CharacterResponse response = null;

  public void LoadData() {
    StartCoroutine (GetData ());
  }

  public IEnumerator GetData () {
    UnityWebRequest www = UnityWebRequest.Get ("https://api.airtable.com/v0/appQ3dmrqe9MBHoos/Characters?maxRecords=" + maxRecords + "&view=" + view);
    www.SetRequestHeader ("Authorization", "Bearer " + airtableConfig.token);
    yield return www.Send ();

    if (www.isNetworkError || www.isHttpError) {
      Debug.Log("AirtableConfig is not valid");
      airtableConfig.isValid = false;
    } else {
      Debug.Log("AirtableConfig is valid");
      Debug.Log (www.downloadHandler.text);
      response = JsonUtility.FromJson<CharacterResponse> (www.downloadHandler.text);
      if (OnUpdate != null) OnUpdate ();
    }
  }

}