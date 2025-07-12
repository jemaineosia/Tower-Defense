using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

[CustomEditor(typeof(TileSlot)), CanEditMultipleObjects]
public class TileSlotEditor : Editor
{
    private GUIStyle centeredStyle;
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        base.OnInspectorGUI();

        centeredStyle = new GUIStyle(GUI.skin.label)
        {
            alignment = TextAnchor.MiddleCenter,
            fontStyle = FontStyle.Bold,
            fontSize = 14,
        };

        float oneButtonWidth = (EditorGUIUtility.currentViewWidth - 20);
        float twoButtonWidth = (EditorGUIUtility.currentViewWidth - 20) / 2;
        float threeButtonWidth = (EditorGUIUtility.currentViewWidth - 20) / 3;

        GUILayout.Label("Position and Rotation", centeredStyle);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Rotate Left", GUILayout.Width(twoButtonWidth)))
        {
            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).RotateTile(-1);
            }
        }
        if (GUILayout.Button("Rotate Right", GUILayout.Width(twoButtonWidth)))
        {
            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).RotateTile(1);
            }
        }
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("-0.1f on the Y", GUILayout.Width(twoButtonWidth)))
        {
            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).AdjustY(-1);
            }
        }
        if (GUILayout.Button("+0.1f on the Y", GUILayout.Width(twoButtonWidth)))
        {
            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).AdjustY(1);
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.Label("Tile Options", centeredStyle);
        GUILayout.BeginHorizontal();
        if(GUILayout.Button("Field", GUILayout.Width(twoButtonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().tileField;

            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTile(newTile);
            }
        }
        if (GUILayout.Button("Road", GUILayout.Width(twoButtonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().tileRoad;

            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTile(newTile);
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Sideway", GUILayout.Width(oneButtonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().tileSideway;

            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTile(newTile);
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.Label("Corner Options", centeredStyle);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Inner Corner", GUILayout.Width(twoButtonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().tileInnerCorner;

            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTile(newTile);
            }
        }
        if (GUILayout.Button("Outer Corner", GUILayout.Width(twoButtonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().tileOuterCorner;

            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTile(newTile);
            }
        }
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Small Inner Corner", GUILayout.Width(twoButtonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().tileInnerCornerSmall;

            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTile(newTile);
            }
        }
        if (GUILayout.Button("Small Outer Corner", GUILayout.Width(twoButtonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().tileOuterCornerSmall;

            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTile(newTile);
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.Label("Bridges and Hills", centeredStyle);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Hill 1", GUILayout.Width(threeButtonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().tileHill1;

            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTile(newTile);
            }
        }
        if (GUILayout.Button("Hill 2", GUILayout.Width(threeButtonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().tileHill2;

            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTile(newTile);
            }
        }
        if (GUILayout.Button("Hill 3", GUILayout.Width(threeButtonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().tileHill3;

            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTile(newTile);
            }
        }
        GUILayout.EndHorizontal();
        
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Bridge with Field", GUILayout.Width(threeButtonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().tileBridgeField;

            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTile(newTile);
            }
        }
        if (GUILayout.Button("Bridge with Road", GUILayout.Width(threeButtonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().tileBridgeRoad;

            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTile(newTile);
            }
        }
        if (GUILayout.Button("Bridge with Sideway", GUILayout.Width(threeButtonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().tileBridgeSideway;

            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTile(newTile);
            }
        }
        GUILayout.EndHorizontal();



    }
}
