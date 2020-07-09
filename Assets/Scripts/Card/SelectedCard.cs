using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectedCard : MonoBehaviour
{
    public Button SelectedCardButton;

    // Show selected card details
    void Start()
    {
        // TODO: Prefill with recommended card
    }

    public void ShowSelectedCardDetail(Card card)
    {
        // Disable Add text on button
        this.SelectedCardButton.GetComponentInChildren<TextMeshProUGUI>().enabled = false;

        // TODO Load local card image

        // TODO Set loaded card image to button image
        Texture2D texture = new Texture2D(256, 256);
        Image selectedCardImage = this.SelectedCardButton.GetComponentInChildren<Image>();
        selectedCardImage.enabled = false;
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        selectedCardImage.sprite = sprite;
        selectedCardImage.enabled = true;
    }
}
