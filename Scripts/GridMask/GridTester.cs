using GridMask;
using UnityEngine;

public class GridTester : MonoBehaviour
{

    public GameObject prop1;
    public GameObject prop2;
    public GameObject prop3;
    public GameObject prop4;
    public GameObject prop5;

    void Start()
    {
        GameObject terrain = (GameObject)Instantiate(prop3);
        Renderer terrainRenderer = terrain.GetComponent<Renderer>();
        Vector3 outerPadding = new Vector3(1, 0, 1);
        Vector3 sizeConstraint = new Vector3(2, 0, 2);
        GridMask.Grid outerGrid = GridMask.Grid.Build((terrainRenderer.bounds.min + outerPadding), (terrainRenderer.bounds.max - terrainRenderer.bounds.min - sizeConstraint), 2, 2);


        int subgridN = 10; ;
        //fill outer grid
        GridMask.Grid subgrid;
        for (int i = 0, j; i < 2; i++)
        {
            for(j = 0; j < 2; j++)
            {
                _ = outerGrid.Subgrid(i, j, subgridN, subgridN);
            }
        }

        //fill subgrid (1,0) with prop 2
        subgrid = (GridMask.Grid)outerGrid.GetCellObject(1, 0).PropObj;

        //PropObject propObject;
        GameObject tempGameObject;
        PropObject propObject;
        System.Random random = new System.Random();
        int cellX = (int)subgrid.CELL_SIZE.x + 1;
        int cellZ = (int)subgrid.CELL_SIZE.z + 1;
        for (int i = 0, j; i < subgridN; i++)
        {
            for(j = 0; j < subgridN; j++)
            {
                tempGameObject = (GameObject)Instantiate(prop1);
                propObject = new PropObject(tempGameObject);
                if(!subgrid.Put(i,j,propObject, (float)random.NextDouble() * cellX, terrainRenderer.bounds.max.y + 1, (float)random.NextDouble() * cellZ))
                    Destroy(tempGameObject);
            }
        }

        //fill rest of the subgrids - (0,0)
        subgrid = (GridMask.Grid)outerGrid.GetCellObject(0, 0).PropObj;
        for (int i = 0, j; i < subgridN; i++)
        {
            for (j = 0; j < subgridN; j++)
            {
                tempGameObject = (GameObject)Instantiate(prop2);
                propObject = new PropObject(tempGameObject);
                if (!subgrid.Put(i, j, propObject, (float)random.NextDouble() * cellX, terrainRenderer.bounds.max.y + 1, (float)random.NextDouble() * cellZ))
                    Destroy(tempGameObject);
            }
        }

        //fill rest of the subgrids - (0,1)
        subgrid = (GridMask.Grid)outerGrid.GetCellObject(0, 1).PropObj;
        propObject = new PropObject((GameObject)Instantiate(prop5));
        subgrid.Put(5, 5, propObject, offsetY: terrainRenderer.bounds.max.y);
        for (int i = 0, j; i < subgridN; i++)
        {
            for (j = 0; j < subgridN; j++)
            {
                tempGameObject = (GameObject)Instantiate(prop4);
                propObject = new PropObject(tempGameObject);
                if (!subgrid.Put(i, j, propObject, (float)random.NextDouble() * cellX, terrainRenderer.bounds.max.y + 1, (float)random.NextDouble() * cellZ))
                    Destroy(tempGameObject);
            }
        }

        //fill rest of the subgrids - (1,1)
        subgrid = (GridMask.Grid)outerGrid.GetCellObject(1, 1).PropObj;
        for (int i = 0, j; i < subgridN; i++)
        {
            for (j = 0; j < subgridN; j++)
            {
                tempGameObject = (GameObject)Instantiate(prop1);
                propObject = new PropObject(tempGameObject);
                if (!subgrid.Put(i, j, propObject, (float)random.NextDouble() * cellX, terrainRenderer.bounds.max.y + 1, (float)random.NextDouble() * cellZ))
                    Destroy(tempGameObject);
            }
        }
        
    }

}
