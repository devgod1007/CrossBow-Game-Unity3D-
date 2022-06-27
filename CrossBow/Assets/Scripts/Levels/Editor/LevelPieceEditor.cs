using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Levels.Editor
{
    [CustomEditor(typeof(LevelGenerator), true)]
    public class LevelPieceEditor : UnityEditor.Editor
    {
        private LevelGenerator generator;
        private SerializedProperty levelPieces;
        private ReorderableList list;

        private void OnEnable()
        {
            generator = target as LevelGenerator;

            levelPieces = serializedObject.FindProperty("levelPieces");

            list = new ReorderableList(serializedObject, levelPieces, true, true, true, true)
 {
     drawElementCallback = DrawListItems,
     drawHeaderCallback = DrawHeader
 };
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update(); // Update the array property's representation in the inspector

            list.DoLayoutList(); // Have the ReorderableList do its work

            // We need to call this so that changes on the Inspector are saved by Unity.
            serializedObject.ApplyModifiedProperties();

            if (GUILayout.Button("Generate"))
            {
                var parent = new GameObject().transform;
                parent.position = Vector3.zero;
                var pieces = generator.Generate(parent);
            }
        }

        private void DrawHeader(Rect rect)
        {
            EditorGUI.LabelField(rect, "Pieces");
        }

        private void DrawListItems(Rect rect, int index, bool isactive, bool isfocused)
        {
            SerializedProperty element = list.serializedProperty.GetArrayElementAtIndex(index);

            var isVPiece = generator.levelPieces[index].type.ToString()[0] == 'V';

            var enumLenght = isVPiece ? 150 : 250;

            generator.levelPieces[index].type = (PieceType) EditorGUI.EnumPopup(
                new Rect(rect.x, rect.y, enumLenght, EditorGUIUtility.singleLineHeight),
                generator.levelPieces[index].type);

            if (isVPiece)
            {
                if (generator.levelPieces[index].activation.Length != 3)
                    generator.levelPieces[index].activation = new bool[3];

                EditListBool(rect, index, enumLenght, 0);
                EditListBool(rect, index, enumLenght, 1);
                EditListBool(rect, index, enumLenght, 2);
            }
        }
        private void EditListBool(Rect rect, int index, int enumLenght, int boolIndex)
        {
            generator.levelPieces[index].activation[boolIndex] = EditorGUI.Toggle(
                new Rect(rect.x + enumLenght + 10 + boolIndex * 20, rect.y, 15, EditorGUIUtility.singleLineHeight),
                generator.levelPieces[index].activation[boolIndex]);
        }
    }
}