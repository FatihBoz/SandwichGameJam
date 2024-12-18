using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacementUI : MonoBehaviour
{
    public static PlacementUI Instance{get;private set;}
    public Action OnTowerBuilded;
    public Placement placementLocation;
    public TowerImage towerIconPrefab;
    public Transform towerIconParent;
    public Transform towerList;
    private List<TowerImage> selectableImages;


    bool canBuild;

    [SerializeField] private float offSetY = 3f;

    void Awake()
    {
        Instance=this;
        selectableImages=new List<TowerImage>();
    }

   public void SetPlacementLocation(Placement placementLocation)
   {
        this.placementLocation=placementLocation;
        towerIconParent.gameObject.SetActive(true);
        LoadTowerIcons();
   }
   public void Reset()
   {
       for (int i = 0; i < selectableImages.Count; i++)
       {
             Destroy(selectableImages[i].gameObject);
       }
        selectableImages.Clear();
        towerIconParent.gameObject.SetActive(false);
   }
   public void LoadTowerIcons()
   {
        if (placementLocation!=null)
        {
            foreach (var item in placementLocation.towerPrefabs)
            {
                TowerImage instantiedImage = Instantiate(towerIconPrefab,towerList);
                instantiedImage.SetImageIcon(item.towerIcon);
                instantiedImage.SetTower(item);
                instantiedImage.SetGoldText(item.price.ToString());
                selectableImages.Add(instantiedImage);
            } 
        }
   }

   public void Build(Tower Tower)
   {
    if (Tower!=null)
    {
        PlayerPurchase playerPurchase = FindAnyObjectByType<PlayerPurchase>();
        if (playerPurchase!=null)
        {
            if (playerPurchase.GetCurrentGold()>=Tower.price && canBuild)
            {
                OnTowerBuilded?.Invoke();
                playerPurchase.DecreaseGold((int)Tower.price);
                Tower tower=Instantiate(Tower);
                tower.SetPlacement(placementLocation.gameObject);
                //ConstractedBuilds.Instance.AddBuilding(tower, placementLocation);
                tower.transform.position = new(placementLocation.transform.position.x,placementLocation.transform.position.y + offSetY);
                placementLocation.gameObject.SetActive(false); 
            }
        }
    }
    }

    private void Placement_OnMineTutorialCompleted()
    {
        canBuild = true;
    }

    private void OnEnable()
    {
        MineTutorial.OnMineTutorialCompleted += Placement_OnMineTutorialCompleted;
    }

    private void OnDisable()
    {
        MineTutorial.OnMineTutorialCompleted -= Placement_OnMineTutorialCompleted;
    }


}
