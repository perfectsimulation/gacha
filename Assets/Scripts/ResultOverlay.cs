using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultOverlay : MonoBehaviour
{
    public Image BronzeImage;
    public Image SilverImage;
    public Image GoldImage;

    public GameObject ItemDropResultPrefab;
    public Transform FirstItemDropPosition;

    private Sprite starFullSprite;
    private Sprite starEmptySprite;

    private void Start()
    {
        Texture2D starFullTexture = Resources.Load("Images/star-full") as Texture2D;
        Texture2D starEmptyTexture = Resources.Load("Images/star-empty") as Texture2D;
        this.starFullSprite = Sprite.Create(starFullTexture, new Rect(0, 0, starFullTexture.width, starFullTexture.height), new Vector2(0.5f, 0.5f));
        this.starEmptySprite = Sprite.Create(starEmptyTexture, new Rect(0, 0, starEmptyTexture.width, starEmptyTexture.height), new Vector2(0.5f, 0.5f));
    }

    public void DisplayScoreTier(float score, int[] scoreTier)
    {
        bool[] didPassTier = new bool[scoreTier.Length];
        for (int i = 0; i < didPassTier.Length; i++)
        {
            didPassTier[i] = score > scoreTier[i];
        }

        this.SetTierImages(didPassTier);
    }

    public void DisplayItemDrops(List<ItemData> itemDrops)
    {
        // Create an Item Drop Result gameObject for each dropped item
        float spacing = 330f;
        for (int i = 0; i < itemDrops.Count; i++)
        {
            // Position equidistant for now - think of something fun later TODO
            GameObject resultPreview = Instantiate(this.ItemDropResultPrefab);
            resultPreview.transform.SetParent(this.FirstItemDropPosition);
            resultPreview.transform.localPosition = new Vector3(i * spacing, 0);

            // Set name label
            ItemDropResult itemDropResult = resultPreview.GetComponent<ItemDropResult>();
            itemDropResult.SetImageName(itemDrops[i].name);
        }
    }

    private void SetTierImages(bool[] didPassTier)
    {
        this.BronzeImage.sprite = didPassTier[0] ? this.starFullSprite : this.starEmptySprite;
        this.SilverImage.sprite = didPassTier[1] ? this.starFullSprite : this.starEmptySprite;
        this.GoldImage.sprite = didPassTier[2] ? this.starFullSprite : this.starEmptySprite;
    }

}
