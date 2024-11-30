using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private static readonly float scaleAmount = 1.2f;
    private Tower tower;
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }
    public void SetImageIcon(Sprite sprite)
    {
        image.sprite = sprite;
    }
    public void SetTower(Tower tower)
    {
        this.tower=tower;
    }
    public Tower GetTower()
    {
        return tower;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.rectTransform.localScale = Vector3.one * scaleAmount;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.rectTransform.localScale = Vector3.one;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PlacementUI.Instance.Build(GetTower());
    }
}