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

        //GUILayout.Label("Tile Options", centeredStyle);
        //GUILayout.BeginHorizontal();
        //if(GUILayout.Button("Field", GUILayout.Width(twoButtonWidth)))
        //{
        //    GameObject newTile = FindFirstObjectByType<TileSetHolder>().tileField;

        //    foreach (var targetTile in targets)
        //    {
        //        ((TileSlot)targetTile).SwitchTile(newTile);
        //    }
        //}
        //if (GUILayout.Button("Road", GUILayout.Width(twoButtonWidth)))
        //{
        //    GameObject newTile = FindFirstObjectByType<TileSetHolder>().tileRoad;

        //    foreach (var targetTile in targets)
        //    {
        //        ((TileSlot)targetTile).SwitchTile(newTile);
        //    }
        //}
        //GUILayout.EndHorizontal();

        //GUILayout.BeginHorizontal();
        //if (GUILayout.Button("Sideway", GUILayout.Width(oneButtonWidth)))
        //{
        //    GameObject newTile = FindFirstObjectByType<TileSetHolder>().tileSideway;

        //    foreach (var targetTile in targets)
        //    {
        //        ((TileSlot)targetTile).SwitchTile(newTile);
        //    }
        //}
        //GUILayout.EndHorizontal();

        //GUILayout.Label("Corner Options", centeredStyle);
        //GUILayout.BeginHorizontal();
        //if (GUILayout.Button("Inner Corner", GUILayout.Width(twoButtonWidth)))
        //{
        //    GameObject newTile = FindFirstObjectByType<TileSetHolder>().tileInnerCorner;

        //    foreach (var targetTile in targets)
        //    {
        //        ((TileSlot)targetTile).SwitchTile(newTile);
        //    }
        //}
        //if (GUILayout.Button("Outer Corner", GUILayout.Width(twoButtonWidth)))
        //{
        //    GameObject newTile = FindFirstObjectByType<TileSetHolder>().tileOuterCorner;

        //    foreach (var targetTile in targets)
        //    {
        //        ((TileSlot)targetTile).SwitchTile(newTile);
        //    }
        //}
        //GUILayout.EndHorizontal();
        //GUILayout.BeginHorizontal();
        //if (GUILayout.Button("Small Inner Corner", GUILayout.Width(twoButtonWidth)))
        //{
        //    GameObject newTile = FindFirstObjectByType<TileSetHolder>().tileInnerCornerSmall;

        //    foreach (var targetTile in targets)
        //    {
        //        ((TileSlot)targetTile).SwitchTile(newTile);
        //    }
        //}
        //if (GUILayout.Button("Small Outer Corner", GUILayout.Width(twoButtonWidth)))
        //{
        //    GameObject newTile = FindFirstObjectByType<TileSetHolder>().tileOuterCornerSmall;

        //    foreach (var targetTile in targets)
        //    {
        //        ((TileSlot)targetTile).SwitchTile(newTile);
        //    }
        //}
        //GUILayout.EndHorizontal();

        //GUILayout.Label("Bridges and Hills", centeredStyle);
        //GUILayout.BeginHorizontal();
        //if (GUILayout.Button("Hill 1", GUILayout.Width(threeButtonWidth)))
        //{
        //    GameObject newTile = FindFirstObjectByType<TileSetHolder>().tileHill1;

        //    foreach (var targetTile in targets)
        //    {
        //        ((TileSlot)targetTile).SwitchTile(newTile);
        //    }
        //}
        //if (GUILayout.Button("Hill 2", GUILayout.Width(threeButtonWidth)))
        //{
        //    GameObject newTile = FindFirstObjectByType<TileSetHolder>().tileHill2;

        //    foreach (var targetTile in targets)
        //    {
        //        ((TileSlot)targetTile).SwitchTile(newTile);
        //    }
        //}
        //if (GUILayout.Button("Hill 3", GUILayout.Width(threeButtonWidth)))
        //{
        //    GameObject newTile = FindFirstObjectByType<TileSetHolder>().tileHill3;

        //    foreach (var targetTile in targets)
        //    {
        //        ((TileSlot)targetTile).SwitchTile(newTile);
        //    }
        //}
        //GUILayout.EndHorizontal();
        
        //GUILayout.BeginHorizontal();
        //if (GUILayout.Button("Bridge with Field", GUILayout.Width(threeButtonWidth)))
        //{
        //    GameObject newTile = FindFirstObjectByType<TileSetHolder>().tileBridgeField;

        //    foreach (var targetTile in targets)
        //    {
        //        ((TileSlot)targetTile).SwitchTile(newTile);
        //    }
        //}
        //if (GUILayout.Button("Bridge with Road", GUILayout.Width(threeButtonWidth)))
        //{
        //    GameObject newTile = FindFirstObjectByType<TileSetHolder>().tileBridgeRoad;

        //    foreach (var targetTile in targets)
        //    {
        //        ((TileSlot)targetTile).SwitchTile(newTile);
        //    }
        //}
        //if (GUILayout.Button("Bridge with Sideway", GUILayout.Width(threeButtonWidth)))
        //{
        //    GameObject newTile = FindFirstObjectByType<TileSetHolder>().tileBridgeSideway;

        //    foreach (var targetTile in targets)
        //    {
        //        ((TileSlot)targetTile).SwitchTile(newTile);
        //    }
        //}
        //GUILayout.EndHorizontal();

        GUILayout.Label("Kenny Assets", centeredStyle);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Tile", GUILayout.Width(threeButtonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().snowTile;

            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTile(newTile);
            }
        }
        if (GUILayout.Button("Dirt", GUILayout.Width(threeButtonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().snowTileDirt;

            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTile(newTile);
            }
        }
        if (GUILayout.Button("Bump", GUILayout.Width(threeButtonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().snowTileBump;

            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTile(newTile);
            }
        }
        GUILayout.EndHorizontal();
        
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Corner", GUILayout.Width(threeButtonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().snowTileCorner;

            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTile(newTile);
            }
        }
        if (GUILayout.Button("Straight", GUILayout.Width(threeButtonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().snowTileStraight;

            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTile(newTile);
            }
        }
        if (GUILayout.Button("Crossing", GUILayout.Width(threeButtonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().snowTileCrossing;

            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTile(newTile);
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Corner Inner", GUILayout.Width(threeButtonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().snowTileCornerInner;

            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTile(newTile);
            }
        }
        if (GUILayout.Button("Corner Outer", GUILayout.Width(threeButtonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().snowTileCornerOuter;

            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTile(newTile);
            }
        }
        if (GUILayout.Button("Corner Round", GUILayout.Width(threeButtonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().snowTileCornerRound;

            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTile(newTile);
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Hill", GUILayout.Width(threeButtonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().snowTileHill;

            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTile(newTile);
            }
        }
        if (GUILayout.Button("Slope", GUILayout.Width(threeButtonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().snowTileSlope;

            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTile(newTile);
            }
        }
        if (GUILayout.Button("Split", GUILayout.Width(threeButtonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().snowTileSplit;

            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTile(newTile);
            }
        }
        GUILayout.EndHorizontal();


        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Corner Square", GUILayout.Width(threeButtonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().snowTileCornerSquare;

            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTile(newTile);
            }
        }
        if (GUILayout.Button("End", GUILayout.Width(threeButtonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().snowTileEnd;

            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTile(newTile);
            }
        }
        if (GUILayout.Button("End Round", GUILayout.Width(threeButtonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().snowTileEndRound;

            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTile(newTile);
            }
        }
        GUILayout.EndHorizontal();


        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Straight Slope", GUILayout.Width(threeButtonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().snowTileStraightSlope;

            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTile(newTile);
            }
        }
        if (GUILayout.Button("Straight Slope Large", GUILayout.Width(threeButtonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().snowTileStraightSlopeLarge;

            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTile(newTile);
            }
        }
        if (GUILayout.Button("Transition", GUILayout.Width(threeButtonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().snowTileTransition;

            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTile(newTile);
            }
        }
        GUILayout.EndHorizontal();


        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Crystal", GUILayout.Width(threeButtonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().snowTileCrystal;

            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTile(newTile);
            }
        }
        if (GUILayout.Button("Trees Quad", GUILayout.Width(threeButtonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().snowTileTreeQuad;

            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTile(newTile);
            }
        }
        if (GUILayout.Button("Tree Single", GUILayout.Width(threeButtonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().snowTileTree;

            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTile(newTile);
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Rock", GUILayout.Width(threeButtonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().snowTileRock;

            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTile(newTile);
            }
        }
        if (GUILayout.Button("Hill", GUILayout.Width(threeButtonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().snowTileHill;

            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTile(newTile);
            }
        }
        if (GUILayout.Button("Tree Double", GUILayout.Width(threeButtonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().snowTileTreeDouble;

            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTile(newTile);
            }
        }
        GUILayout.EndHorizontal();
    }
}
