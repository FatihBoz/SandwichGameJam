
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private static readonly float scaleAmount = 1.2f;
    private Tower tower;
    private Image image;
    private TMP_Text goldText;

    private void Awake()
    {
        image = GetComponent<Image>();
        goldText = GetComponentInChildren<TMP_Text>();
    }
    public void SetImageIcon(Sprite sprite)
    {
        image.sprite = sprite;
    }
    public void SetTower(Tower tower)
    {
        this.tower=tower;
    }
    public void SetGoldText(string text)
    {
        goldText.text=text;
    }
    public Tower GetTower()
    {
        return tower;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.rectTransform.localScale = Vector3.one * scaleAmount;
        print("On Pointer Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.rectTransform.localScale = Vector3.one;
        print("On Pointer Exit");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PlacementUI.Instance.Build(GetTower());
        print("On Pointer Click");
    }
}
