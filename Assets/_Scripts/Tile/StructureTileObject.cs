using UnityEngine;

public class StructureTileObject : TileObject
{
    [SerializeField] private Transform structureParent;
    private GameObject currentStructure;
    private bool currentStructureIsBlockStructure;

    public void PlaceStructure(GameObject structurePrefab, bool blockStructure, Vector3? offset = null, Quaternion? rotation = null) {
        currentStructureIsBlockStructure = blockStructure;
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
    public bool HasBlockStructurePlaced() => currentStructureIsBlockStructure && currentStructure != null;
}
