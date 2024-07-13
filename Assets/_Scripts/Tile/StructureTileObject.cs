using UnityEngine;

public class StructureTileObject : TileObject
{
    [SerializeField] private Transform structureParent;
    private GameObject currentStructure;

    public void PlaceStructure(GameObject structurePrefab, Vector3? offset = null, Quaternion? rotation = null) {
        if(currentStructure != null) Destroy(currentStructure);
        currentStructure = Instantiate(structurePrefab, structureParent);

        if(rotation != null){
            currentStructure.transform.rotation = (Quaternion)rotation;
        }
        if(offset != null) {
            transform.position += (Vector3)offset;
        }
    }

    public bool HasStructurePlaced() => currentStructure != null;
}
