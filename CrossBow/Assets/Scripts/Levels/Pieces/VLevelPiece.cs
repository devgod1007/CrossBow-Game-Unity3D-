using UnityEngine;

namespace Levels.Pieces
{
    public class VLevelPiece : LevelPiece
    {
        [SerializeField] private GameObject[] elements;

        public void Activate(bool[] activations)
        {
            for (int i = 0; i < elements.Length; i++)
            {
                var element = elements[i];
                element.SetActive(activations[i]);
            }
        }
    }
}