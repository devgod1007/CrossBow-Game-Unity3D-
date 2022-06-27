using UnityEngine;

public class LevelPiece : MonoBehaviour
{
    [field: SerializeField] public PieceType PieceType { get; private set; }
}

public enum PieceType
{
    Road,
    C1,
    C2,
    C3,
    C4,
    C5,
    C6,
    C7,
    C10,
    V1,
    V2,
    V3,
    V4,
    V5,
    V6,
    Empty,
    BossRing,
    BossRing1
}