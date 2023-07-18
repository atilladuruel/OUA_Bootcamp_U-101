using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePair
{
    public string tile;
    public string tileMap;

    public TilePair(string _tile, string _tileMap)
    {
        tile = _tile;
        tileMap = _tileMap;
    }

    public bool CheckTile(string name)
    {
        if (name == tile)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

[CustomGridBrush(false, true, true, "Layer Select Brush")]
public class SpecialBrush : UnityEditor.Tilemaps.GridBrush
{
    private Tilemap targetTilemap;
    private string tileMapName;
    private Vector2Int currentChunk;
    private bool justChangedTile = false;

    // These are the names of tile objects that i use in the project
    private List<TilePair> coliders = new List<TilePair>()
        {
            // to add a new tile, write the name of the tile asset in the project tab, and the name of the tilemap gameobject you want it to auto-draw to.
            // for example new TilePair("TileName", "TileMapName"),
            new TilePair("ExampleTile", "Tilemap"),

            // make sure that this last one is here, just so that it doesn't break the code.
            // I'm sure there is a more elegant solution, but this is the one I give to you.
            new TilePair("EndTile", "null"),
        };

    public override void Pick(GridLayout gridLayout, GameObject brushTarget, BoundsInt position, Vector3Int pickStart)
    {
        base.Pick(gridLayout, brushTarget, position, pickStart);
        TileBase currentTile = cells[0].tile;
        if (currentTile != null)
        {
            string tilemapName = this.GetCorrespondingTilemapName(currentTile.name);
            List<Tilemap> tilemaps = GameObject.FindObjectsOfType<Tilemap>().ToList();
            this.targetTilemap = tilemaps.Find(tilemap => tilemap.name.Equals(tilemapName));
            tileMapName = tilemapName;
            Selection.activeObject = this.targetTilemap.gameObject;
            justChangedTile = true;
        }
    }

    public override void Paint(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
    {
        if (brushTarget != null)
        {
            Undo.RegisterCompleteObjectUndo(this.targetTilemap, string.Empty);
            base.Paint(gridLayout, brushTarget, position);
        }
    }

    public override void BoxFill(GridLayout gridLayout, GameObject brushTarget, BoundsInt position)
    {
        if (brushTarget != null)
        {
            Undo.RegisterCompleteObjectUndo(this.targetTilemap, string.Empty);
            base.BoxFill(gridLayout, brushTarget, position);
        }
    }

    public override void FloodFill(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
    {
        return;
        if (brushTarget != null)
        {
            Undo.RegisterCompleteObjectUndo(this.targetTilemap, string.Empty);
            base.FloodFill(gridLayout, brushTarget, position);
        }
    }

    public override void Erase(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
    {
        if (brushTarget != null)
        {
            Undo.RegisterCompleteObjectUndo(this.targetTilemap, string.Empty);
            base.Erase(gridLayout, brushTarget, position);      
        }
    }


    public override void BoxErase(GridLayout gridLayout, GameObject brushTarget, BoundsInt position)
    {
        if (brushTarget != null)
        {
            Undo.RegisterCompleteObjectUndo(this.targetTilemap, string.Empty);
            base.BoxErase(gridLayout, brushTarget, position);
        }
    }

    // Here i get the corresponding tilemap name.
    private string GetCorrespondingTilemapName(string tileName)
    {
        foreach (TilePair pair in coliders)
        {
            if (pair.CheckTile(tileName))
            {
                return pair.tileMap;
            }
        }

        return string.Empty;
    }
    void FixUndo()
    {
        Debug.Log("Undo");
    }
}