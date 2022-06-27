using System;
using System.Collections.Generic;
using System.Linq;
using Levels.Pieces;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Serializable]
    public struct PieceInfo
    {
        public PieceType type;
        public bool[] activation;
    }
    
    
    [SerializeField] private Vector3 startPoint;
    [SerializeField] private Vector3 distanceBetween;


    [SerializeField] public PieceInfo[] levelPieces;
    
    private const float ZSizeOfOnePiece = 2.032648f;

    [HideInInspector] public List<GameObject> spawned;
    [HideInInspector]public Transform oldParent;

    public List<GameObject> Generate(Transform parent)
    {
        #if UNITY_EDITOR
        var point = startPoint;
        if (oldParent)
            DestroyImmediate(oldParent.gameObject);

        oldParent = parent;
        var pieces = Resources.LoadAll<LevelPiece>("Levels/Pieces");

        for (int i = 0; i < levelPieces.Length; i++)
        {
            var current = levelPieces[i];
            var piece =
                UnityEditor.PrefabUtility.InstantiatePrefab(pieces.First(p =>
                    p.PieceType == current.type), parent) as LevelPiece;

            if (piece is VLevelPiece levelPiece)
            {
                levelPiece.Activate(levelPieces[i].activation);
            }

            piece.transform.position = point;
            spawned.Add(piece.gameObject);
            point.z += ZSizeOfOnePiece;
            point += distanceBetween;
        }

        return spawned;
        #endif
        return null;
    }
}