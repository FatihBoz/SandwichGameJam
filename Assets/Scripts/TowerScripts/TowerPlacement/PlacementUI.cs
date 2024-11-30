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
    private List<TowerImage> selectableImages;

    [SerializeField] private float offSetY = 3f;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
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
                TowerImage instantiedImage = Instantiate(towerIconPrefab,towerIconParent);
                instantiedImage.SetImageIcon(item.towerIcon);
                instantiedImage.SetTower(item);

                //Button button = instantiedImage.gameObject.AddComponent<Button>();
                //button.onClick.AddListener(()=>SelectTowerImage(instantiedImage));

                selectableImages.Add(instantiedImage);
            } 
        }
   }

   //public void SelectTowerImage(TowerImage towerImage)
   //{
        
   //     if (selectedTowerImage!=null)
   //     {
   //         selectedTowerImage.GetComponent<Image>().color=Color.white;
   //     }
   //     selectedTowerImage=towerImage;
   //     selectedTowerImage.GetComponent<Image>().color=Color.green;
   //}

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