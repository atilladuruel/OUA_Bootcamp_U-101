using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FriendlyTile : RuleTile<FriendlyTile.Neighbor> {
    [HideInInspector]
    public bool customField;

    public int randomSeed = 54109;

    public class Neighbor : RuleTile.TilingRule.Neighbor {
        new public const int This = 1;

        new public const int NotThis = 2;
    }

    public TileBase[] friendTiles;

    public override bool RuleMatch(int neighbor, TileBase tile)
    {
        if (tile is RuleOverrideTile)
            tile = (tile as RuleOverrideTile).m_InstanceTile;

        switch (neighbor)
        {
            case Neighbor.This: return(HasFriendTile(tile));
            case Neighbor.NotThis: return HasntFriendTile(tile);
        }
        return true;
    }

    bool HasntFriendTile(TileBase tile)
    {
        if (tile != null)
            return false;
        return tile != this;
    }

    bool HasFriendTile(TileBase tile)
    {
        if (tile != null)
            return true;
        return tile == this;
    }
    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        var iden = Matrix4x4.identity;

        tileData.sprite = m_DefaultSprite;
        tileData.gameObject = m_DefaultGameObject;
        tileData.colliderType = m_DefaultColliderType;
        tileData.flags = TileFlags.LockTransform;
        tileData.transform = iden;

        foreach (TilingRule rule in m_TilingRules)
        {
            Matrix4x4 transform = iden;
            if (RuleMatches(rule, position, tilemap, ref transform))
            {
                switch (rule.m_Output)
                {
                    case TilingRule.OutputSprite.Single:
                    case TilingRule.OutputSprite.Animation:
                        tileData.sprite = rule.m_Sprites[0];
                        break;
                    case TilingRule.OutputSprite.Random:

                        Random.InitState((position.x + position.y) * randomSeed);
                            int index = Mathf.Clamp(Mathf.FloorToInt(Random.value * rule.m_Sprites.Length), 0, rule.m_Sprites.Length - 1);

                        tileData.sprite = rule.m_Sprites[index];
                        if (rule.m_RandomTransform != TilingRule.Transform.Fixed)
                            transform = ApplyRandomTransform(rule.m_RandomTransform, transform, rule.m_PerlinScale, position);
                        break;
                }
                tileData.transform = transform;
                tileData.gameObject = rule.m_GameObject;
                tileData.colliderType = rule.m_ColliderType;
                break;
            }
        }
    }
}