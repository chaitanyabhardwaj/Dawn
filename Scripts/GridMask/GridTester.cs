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
        Renderer terrainRenderer = prop3.GetComponent<Renderer>();
        Vector3 outerPadding = new Vector3(1, 0, 1);
        Vector3 sizeConstraint = new Vector3(2, 0, 2);
        GridMask.Grid outerGrid = GridMask.Grid.Build((terrainRenderer.bounds.min + outerPadding), (terrainRenderer.bounds.max - terrainRenderer.bounds.min - sizeConstraint), 2, 2);
        //GridMask.Grid grid = GridMask.Grid.Build((terrainRenderer.bounds.min + outerPadding), (terrainRenderer.bounds.max - terrainRenderer.bounds.min - sizeConstraint), 10, 10);
        /*PropObject propObj1 = new PropObject((GameObject)Instantiate(prop1));
        PropObject propObj2 = new PropObject((GameObject)Instantiate(prop2));
        PropObject propObj3 = new PropObject((GameObject)Instantiate(prop3));
        PropObject propObj4 = new PropObject((GameObject)Instantiate(prop2));
        PropObject propObj5 = new PropObject((GameObject)Instantiate(prop1));
        grid.FillCell(0, 0, propObj1);
        grid.FillCell(0, 1, propObj2, offsetY: 2, offsetZ: grid.CELL_SIZE.z);
        grid.FillCell(1, 0, propObj3, offsetX: grid.CELL_SIZE.x, offsetY: 2);
        grid.FillCell(1, 1, propObj4, align: CellObject.ALIGN_CELL_END);
        grid.FillCell(1, 1, propObj5, align: CellObject.ALIGN_CELL_CENTER);*/

        //fill outer grid
        GridMask.Grid subgrid;
        for (int i = 0, j; i < 2; i++)
        {
            for(j = 0; j < 2; j++)
            {
                _ = outerGrid.Subgrid(i, j, 15, 15);
            }
        }

        //will subgrid (1,0) with prop 2
        subgrid = (GridMask.Grid)outerGrid.GetCellObject(1, 0).GetPropList[0];

        PropObject propObject;
        System.Random random = new System.Random();
        int cellX = (int)subgrid.CELL_SIZE.x + 1;
        int cellZ = (int)subgrid.CELL_SIZE.z + 1;
        for (int i = 0, j; i < 15; i++)
        {
            for(j = 0; j < 15; j++)
            {
                propObject = new PropObject((GameObject)Instantiate(prop1));
                subgrid.Put(i,j,propObject, (float)random.NextDouble() * cellX, terrainRenderer.bounds.max.y + 1, (float)random.NextDouble() * cellZ);
                propObject = new PropObject((GameObject)Instantiate(prop5));
                subgrid.Put(i, j, propObject, (float)random.NextDouble() * cellX, terrainRenderer.bounds.max.y + 1, (float)random.NextDouble() * cellZ);
            }
        }

        //fill rest of the subgrids - (0,0)
        subgrid = (GridMask.Grid)outerGrid.GetCellObject(0, 0).GetPropList[0];
        for (int i = 0, j; i < 15; i++)
        {
            for (j = 0; j < 15; j++)
            {
                propObject = new PropObject((GameObject)Instantiate(prop1));
                subgrid.Put(i, j, propObject, (float)random.NextDouble() * cellX, terrainRenderer.bounds.max.y + 1, (float)random.NextDouble() * cellZ);
                propObject = new PropObject((GameObject)Instantiate(prop5));
                subgrid.Put(i, j, propObject, (float)random.NextDouble() * cellX, terrainRenderer.bounds.max.y + 1, (float)random.NextDouble() * cellZ);
                propObject = new PropObject((GameObject)Instantiate(prop2));
                subgrid.Put(i, j, propObject, (float)random.NextDouble() * cellX, terrainRenderer.bounds.max.y + 1, (float)random.NextDouble() * cellZ);
            }
        }

        //fill rest of the subgrids - (0,1)
        subgrid = (GridMask.Grid)outerGrid.GetCellObject(0, 1).GetPropList[0];
        for (int i = 0, j; i < 15; i++)
        {
            for (j = 0; j < 15; j++)
            {
                propObject = new PropObject((GameObject)Instantiate(prop1));
                subgrid.Put(i, j, propObject, (float)random.NextDouble() * cellX, terrainRenderer.bounds.max.y + 1, (float)random.NextDouble() * cellZ);
                propObject = new PropObject((GameObject)Instantiate(prop1));
                subgrid.Put(i, j, propObject, (float)random.NextDouble() * cellX, terrainRenderer.bounds.max.y + 1, (float)random.NextDouble() * cellZ);
                propObject = new PropObject((GameObject)Instantiate(prop4));
                subgrid.Put(i, j, propObject, (float)random.NextDouble() * cellX, terrainRenderer.bounds.max.y + 1, (float)random.NextDouble() * cellZ);
            }
        }

        //fill rest of the subgrids - (1,1)
        subgrid = (GridMask.Grid)outerGrid.GetCellObject(1, 1).GetPropList[0];
        for (int i = 0, j; i < 15; i++)
        {
            if (i == 10 || i == 11) continue;
            for (j = 0; j < 15; j++)
            {
                propObject = new PropObject((GameObject)Instantiate(prop1));
                subgrid.Put(i, j, propObject, (float)random.NextDouble() * cellX, terrainRenderer.bounds.max.y + 1, (float)random.NextDouble() * cellZ);
                propObject = new PropObject((GameObject)Instantiate(prop4));
                subgrid.Put(i, j, propObject, (float)random.NextDouble() * cellX, terrainRenderer.bounds.max.y + 1, (float)random.NextDouble() * cellZ);
            }
        }
    }

}
