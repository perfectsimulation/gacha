using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SelectedCard : MonoBehaviour
{
    public Button SelectedCardButton;

    // Show selected card details
    void Start()
    {
        // TODO: Prefill with recommended card
    }

    public IEnumerator ShowSelectedCardDetail(CardData cardData)
    {
        // Disable Add text on button
        this.SelectedCardButton.GetComponentInChildren<TextMeshProUGUI>().enabled = false;

        // Load card image from URL
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(cardData.imageUrl);
        yield return www.SendWebRequest();

        // Set loaded card image to button image
        Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
        Image selectedCardImage = this.SelectedCardButton.GetComponentInChildren<Image>();
        selectedCardImage.enabled = false;
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        selectedCardImage.sprite = sprite;
        selectedCardImage.enabled = true;
    }
}
