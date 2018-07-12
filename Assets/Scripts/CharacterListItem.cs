using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterListItem : MonoBehaviour {

  [SerializeField]
  private CharacterRecord _character;

  public CharacterRecord character {
    get { return _character; }
    set {
      _character = value;
      UpdateChildren();
    }
  }

  private void UpdateText() {
      Text textComponent = (transform.Find("Name").GetComponent("Text") as Text);
      textComponent.text = _character.fields.Name;
      Color color = textComponent.color;
      color.a = 1f;
      textComponent.color = color;
  }

  private IEnumerator UpdateImage() {
      bool hasImage = _character.fields.Images != null;

      Transform image = transform.Find("Image");
      image.gameObject.SetActive(hasImage);

      Image imageComponent = transform.Find("Image").GetComponent("Image") as Image;
      if (hasImage)
      {
        Thumbnail largeThumb = _character.fields.Images[0].thumbnails.large;
        WWW www = new WWW(largeThumb.url);
        yield return www;
        imageComponent.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
        imageComponent.preserveAspect = true;
        // Color color = imageComponent.color;
        // color.a = 1f;
        // color.r = 1f;
        // color.g = 1f;
        // color.b = 1f;
        // imageComponent.color = color;
        Animator animator = imageComponent.GetComponent<Animator>();
        animator.StartPlayback();
      }
  }

  private void UpdateHPBar() {
    Slider slider = transform.Find("HPBar").GetComponent<Slider>();
    slider.value = _character.fields.HPPercent/100;
  }

  private void UpdateChildren() {
      if(_character == null) {
        return;
      }
      UpdateText();
      StartCoroutine(UpdateImage());
      UpdateHPBar();

  }

}