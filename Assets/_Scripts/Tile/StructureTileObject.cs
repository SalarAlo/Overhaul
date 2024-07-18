using UnityEngine;

public class StructureTileObject : TileObject
{
    [SerializeField] private Transform structureParent;
    private GameObject currentStructure;
    private bool currentStructureIsBlocking;

    public GameObject PlaceStructure(GameObject structurePrefab, bool blockingStructure, Vector3? offset = null, Quaternion? rotation = null) {
        currentStructureIsBlocking = blockingStructure;
        if(currentStructure != null) Destroy(currentStructure);
        currentStructure = Instantiate(structurePrefab, structureParent);

        if(rotation != null){
            currentStructure.transform.rotation = (Quaternion)rotation;
        }
        if(offset != null) {
            transform.position += (Vector3)offset;
        }
        return currentStructure;
    }

    public void RemovePlacedStructure(){
        if(currentStructure == null) return;
        Destroy(currentStructure);
        currentStructureIsBlocking = false;
        currentStructure = null;
    }

    public bool HasStructurePlaced() => currentStructure != null;
    public bool HasBlockStructurePlaced() => currentStructureIsBlocking && currentStructure != null;
}
