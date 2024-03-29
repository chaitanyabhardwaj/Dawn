﻿using UnityEngine;

/*
 * TODO : #1 How will you remove a Prop spanning across multiple cells? 
 * Hint : Using Prop IDs inside the cell
 */

namespace GridMask
{
    public class Grid : Prop
    {

        readonly private CellObject[,] CELL_OBJECTS;
        readonly private Prop[,] CONTENT;
        readonly private int ROW_LEN, COLUMN_LEN;

        private float cellWidth, cellLength;

        readonly public Vector3 GRID_POSITION_END, GRID_POSITION_START, GRID_SIZE;

        public Vector3 CELL_SIZE { get; private set; }

        private Grid(Vector3 gridStartPosition, Vector3 gridSize, int rowLen, int colLen)
        {
            this.GRID_POSITION_START = gridStartPosition;
            this.GRID_SIZE = gridSize;
            this.ROW_LEN = rowLen;
            this.COLUMN_LEN = colLen;
            this.CELL_OBJECTS = new CellObject[rowLen, colLen];
            this.CONTENT = new Prop[rowLen, colLen];
            this.GRID_POSITION_END = gridStartPosition + gridSize;
        }

        public static Grid Build(Vector3 gridStartPosition, Vector3 gridSize, int numberOfRows, int numberOfColumns)
        {
            return Build(gridStartPosition, gridSize, numberOfRows, numberOfColumns, null);
        }

        public static Grid Build(Vector3 gridStartPosition, Vector3 gridSize, int numberOfRows, int numberOfColumns, PropObject[,] content)
        {
            if (numberOfRows < 1 || numberOfColumns < 1 || gridSize.x < 0 || gridSize.z < 0) return null;
            Grid grid = new Grid(gridStartPosition, gridSize, numberOfRows, numberOfColumns);
            //calculated constants
            grid.cellWidth = gridSize.x / numberOfColumns;
            grid.cellLength = gridSize.z / numberOfRows;
            grid.CELL_SIZE = new Vector3(grid.cellWidth, 0, grid.cellLength);
            //construct the grid
            //filling CellObjects
            CellObject tempObj;
            float ix, jz;
            for (int i = 0, j; i < numberOfRows; i++)
            {
                for (j = 0; j < numberOfColumns; j++)
                {
                    ix = gridStartPosition.x + (i * grid.cellWidth);
                    jz = gridStartPosition.z + (j * grid.cellLength);
                    tempObj = new CellObject(new Vector3(ix, 0, jz), grid.CELL_SIZE);
                    grid.CELL_OBJECTS[i,j] = tempObj;
                }
            }
            if (content == null || content.Length < 1) return grid;
            int contentColLen;
            for (int i = 0, j; i < content.Length; i++)
            {
                contentColLen = content.GetLength(i);
                for (j = 0; j < contentColLen; j++)
                    grid.Put(i, j, content[i,j]);
            }
            return grid;
        }

        public bool CellIsFilled(int x, int z) => !this.CELL_OBJECTS[x, z].IsEmpty;

        public void ClearCell(int x, int z)
        {
            this.CELL_OBJECTS[x, z].Clear();
            //TODO : #1
        }

        public Grid Subgrid(int x, int z, int rowLength, int columnLength, int align = 0)
        {
            if (x >= this.ROW_LEN || z >= this.COLUMN_LEN)
            {
                return null;
            }
            CellObject cellObj = this.CELL_OBJECTS[x, z];
            Vector3 start;
            if (align == CellObject.ALIGN_CELL_CENTER)
                start = (cellObj.POSITION_START + cellObj.POSITION_END) / 2;
            else if (align == CellObject.ALIGN_CELL_END)
                start = cellObj.POSITION_END;
            else
                start = cellObj.POSITION_START;
            Grid subgrid = Grid.Build(start, CELL_SIZE, rowLength, columnLength);
            CONTENT[x, z] = subgrid;
            cellObj.Mark(subgrid);
            return subgrid;
        }

        public CellObject GetCellObjectByStartPosition(Vector3 startPosition)
        {
            int x = (int)((startPosition.x - GRID_POSITION_START.x) / this.cellWidth);
            int z = (int)((startPosition.z - GRID_POSITION_START.z) / this.cellLength);
            if (x >= this.ROW_LEN || z >= this.COLUMN_LEN) return null;
            return this.CELL_OBJECTS[x, z];
        }

        public bool Put(int x, int z, PropObject prop, float offsetX = 0, float offsetY = 0, float offsetZ = 0, int align = 0)
        {
            if (x >= this.ROW_LEN || z >= this.COLUMN_LEN || !this.CELL_OBJECTS[x,z].IsEmpty)
                return false;
            CONTENT[x, z] = prop;
            this.CELL_OBJECTS[x, z].Put(prop, new Vector3(offsetX, offsetY, offsetZ), align);

            //for objects spanning across multiple cells, we have to mark all those cells  as 'filled'
            //method : start from object "position" and loop all cells towards top left and bottom right
            //marking all cells as 'filled' with 'prop'
            if (!prop.MULTI_SPAN_IF_POSSIBLE) return true;
            Vector3 startPos = prop.BoundsMin;
            Vector3 endPos = prop.BoundsMax;
            int ixS = (int)((startPos.x - GRID_POSITION_START.x) / this.cellWidth);
            int jzS = (int)((startPos.z - GRID_POSITION_START.z) / this.cellLength);
            int ixE = (int)((endPos.x - GRID_POSITION_START.x) / this.cellWidth);
            int jzE = (int)((endPos.z - GRID_POSITION_START.z) / this.cellLength);
            ixS = (ixS < 0) ? 0 : ixS;
            jzS = (jzS < 0) ? 0 : jzS;
            ixE = (ixE >= this.ROW_LEN) ? this.ROW_LEN - 1 : ixE;
            jzE = (jzE >= this.ROW_LEN) ? this.ROW_LEN - 1 : jzE;
            Debug.Log("====================");
            Debug.Log(((startPos.x - GRID_POSITION_START.x) / this.cellWidth) + " --- " + (int)Mathf.Floor((startPos.z - GRID_POSITION_START.z) / this.cellLength));
            Debug.Log(((endPos.x - GRID_POSITION_START.x) / this.cellWidth) + " --- " + ((endPos.z - GRID_POSITION_START.z) / this.cellLength));
            for (; ixS <= ixE; ixS++)
            {
                for(; jzS <= jzE; jzS++)
                {
                    this.CELL_OBJECTS[ixS, jzS].Mark(prop);
                }
            }
            return true;
        }

        public Prop[,] GetAllContent()
        {
            Prop[,] content = new Prop[this.ROW_LEN, this.COLUMN_LEN];
            //deep copy CONTENT
            int i, j, len;
            for(i = 0; i < this.CONTENT.Length; i++)
            {
                len = this.CONTENT.GetLength(i);
                for(j = 0; j < len; j++)
                {
                    content[i, j] = this.CONTENT[i, j];
                }
            }
            return content;
        }

        public CellObject GetCellObject(int x, int z) => this.CELL_OBJECTS[x, z];

        public Vector3 Position => this.GRID_POSITION_START;

        public Vector3 Size => this.GRID_SIZE;

    }

}
