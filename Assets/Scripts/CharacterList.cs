using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterList : MonoBehaviour {

  private CharacterTable characterTable;
  public CharacterListItem characterListItem;

  // Use this for initialization
  void Start () {
    characterTable = GameObject.Find ("CharacterData").GetComponent<CharacterTable> ();
    CharacterTable.OnUpdate += CreateItems;
  }

  // Update is called once per frame
  void Update () { }

  void ClearItems () {
    foreach (Transform child in transform) {
      Destroy (child.gameObject);
    }
  }

  void CreateItems () {
    ClearItems ();
    foreach (CharacterRecord character in characterTable.response.records) {
      CharacterListItem item = Instantiate (characterListItem, transform.position, transform.rotation, transform);
      item.character = character;
    }
  }
}