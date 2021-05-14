using UnityEngine;

using System.Linq;

public class LightMapSwitcher : MonoBehaviour {
    public Texture2D[] Day;
    public Texture2D[] Evening;
    public Texture2D[] Night;

    public LightmapData[] lvl0LightMaps;
    public LightmapData[] lvl2LightMaps;
    public LightmapData[] lvl3LightMaps;

    void Start() {
        // Sort the Day and Night arrays in numerical order, so you can just blindly drag and drop them into the inspector
        /*Day = Day.OrderBy(t2d => t2d.name, new NaturalSortComparer<string>()).ToArray();
        Evening = Night.OrderBy(t2d => t2d.name, new NaturalSortComparer<string>()).ToArray();
        Night = Night.OrderBy(t2d => t2d.name, new NaturalSortComparer<string>()).ToArray();*/

        // Put them in a LightMapData structure
        lvl0LightMaps = new LightmapData[Day.Length];
        lvl0LightMaps[0].lightmapDir = Day[0];
        lvl0LightMaps[1].lightmapColor = Day[1];

        /*for (int i = 0; i < Day.Length; i++) {
            lvl0LightMaps[i] = new LightmapData();
            lvl0LightMaps[i].lightmapDir = Day[i];
            lvl0LightMaps[i].lightmapColor = Day[i];
        }*/

        lvl2LightMaps = new LightmapData[Evening.Length];
        lvl2LightMaps[0].lightmapDir = Evening[0];
        lvl2LightMaps[1].lightmapColor = Evening[1];

        /*for (int i = 0; i < Evening.Length; i++) {
            lvl2LightMaps[i] = new LightmapData();
            lvl2LightMaps[i].lightmapDir = Evening[i];
            lvl2LightMaps[i].lightmapColor = Evening[i];
        }*/

        lvl3LightMaps = new LightmapData[Night.Length];
        lvl3LightMaps[0].lightmapDir = Night[0];
        lvl3LightMaps[1].lightmapColor = Night[1];


        /*for (int i = 0; i < Night.Length; i++) {
            lvl3LightMaps[i] = new LightmapData();
            lvl3LightMaps[i].lightmapDir = Night[i];
            lvl3LightMaps[i].lightmapColor = Night[i];
        }*/
    }
    
    #region Publics
    public void SetToDay() {
        LightmapSettings.lightmaps = lvl0LightMaps;
    }
    public void SetToEvening() {
        LightmapSettings.lightmaps = lvl2LightMaps;
    }

    public void SetToNight() {
        LightmapSettings.lightmaps = lvl3LightMaps;
    }
    
    #endregion
}