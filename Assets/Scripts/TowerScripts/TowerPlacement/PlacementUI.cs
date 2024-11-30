using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacementUI : MonoBehaviour
{
    public static PlacementUI Instance{get;private set;}
    public Placement placementLocation;
    public TowerImage towerIconPrefab;
    public Transform towerIconParent;
    public Transform towerList;
    private List<TowerImage> selectableImages;

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

                selectableImages.Add(instantiedImage);
            } 
        }
   }

   public void Build(Tower Tower)
   {
    if (Tower!=null)
    {
        Tower tower=Instantiate(Tower);
        tower.transform.position= new(placementLocation.transform.position.x,placementLocation.transform.position.y + offSetY);
        placementLocation.gameObject.SetActive(false);   
    }
    }
}
