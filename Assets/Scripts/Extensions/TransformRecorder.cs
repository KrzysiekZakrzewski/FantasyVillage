using BlueRacconGames.Extensions;
using UnityEditor;
using UnityEngine;

public class TransformRecorder : MonoBehaviour
{
    [SerializeField] private TransfromType transfromType;

    public void RecordTransform()
    {
        TransformDataContainer container = ScriptableObject.CreateInstance<TransformDataContainer>();
        container.NodeTransformData = new TransformData
        {
            Position = transfromType == TransfromType.World ? transform.position : transform.localPosition,
            Rotation = transfromType == TransfromType.World ? transform.rotation : transform.localRotation,
            Scale = transfromType == TransfromType.World ? transform.localScale : transform.localScale
        };

        string name = $"{transfromType}{gameObject.name}TransformDataContainer";

        AssetDatabase.CreateAsset(container, $"Assets/Data/TransformDataContainers/{name}.asset");
    }
}

public enum TransfromType
{
    World,
    Local
}