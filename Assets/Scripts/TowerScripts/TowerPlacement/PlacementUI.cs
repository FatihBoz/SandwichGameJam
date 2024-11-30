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
    public GameObject MAINPANEL;
    private List<TowerImage> selectableImages;

    public TowerImage selectedTowerImage;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        Instance=this;
        selectedTowerImage=null;
        selectableImages=new List<TowerImage>();
    }

   public void SetPlacementLocation(Placement placementLocation)
   {
        this.placementLocation=placementLocation;
        MAINPANEL.SetActive(true);
        LoadTowerIcons();
   }
   public void Reset()
   {
        if (selectedTowerImage!=null)
        {
            selectedTowerImage.GetComponent<Image>().color=Color.white;
        }
        selectedTowerImage=null;
       for (int i = 0; i < selectableImages.Count; i++)
       {
             Destroy(selectableImages[i].gameObject);
       }
        selectableImages.Clear();
        MAINPANEL.SetActive(false);
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

                Button button = instantiedImage.gameObject.AddComponent<Button>();
                button.onClick.AddListener(()=>SelectTowerImage(instantiedImage));

                selectableImages.Add(instantiedImage);
            } 
        }
   }

   public void SelectTowerImage(TowerImage towerImage)
   {
        
        if (selectedTowerImage!=null)
        {
            selectedTowerImage.GetComponent<Image>().color=Color.white;
        }
        selectedTowerImage=towerImage;
        selectedTowerImage.GetComponent<Image>().color=Color.green;
   }

   public void Build()
   {
    if (selectedTowerImage!=null)
    {
        Tower tower=Instantiate(selectedTowerImage.GetTower());
        tower.transform.position=placementLocation.transform.position;
        placementLocation.gameObject.SetActive(false);   
    }
    }
}
