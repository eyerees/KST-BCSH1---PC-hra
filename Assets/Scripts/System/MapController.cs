using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MapController_Manual : MonoBehaviour
{
   public static MapController_Manual Instance { get; set; }

   public GameObject mapParent;
   List<Image> mapImages;

   public Color higlightColour = Color.yellow;
   public Color dimmedColour = new Color(1f, 1f, 1f, 0.5f);

   public RectTransform playerIconTransform;

   private void Awake()
   {
      if (Instance != null && Instance != this)
      {
         Destroy(gameObject);
         return;
      }
      else
      {
         Instance = this;
      }

      mapImages = mapParent.GetComponentsInChildren<Image>().ToList();
   }

   public void HiglightArea(string areaName)

   {

      foreach (Image area in mapImages)
      {
         area.color = dimmedColour;
      }

      Image currentArea = mapImages.Find(x => x.name == areaName);

      if (currentArea != null)
      {
         currentArea.color = higlightColour;
         playerIconTransform.position = currentArea.GetComponent<RectTransform>().position;
      }
      else
      {
         Debug.LogWarning($"Area with name {areaName} not found on the map.");
      }
   }
}