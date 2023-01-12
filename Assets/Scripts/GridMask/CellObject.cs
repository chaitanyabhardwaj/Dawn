using System.Collections.Generic;
using UnityEngine;

namespace GridMask
{
    public class CellObject
    {
        readonly public static int ALIGN_CELL_START = 0, ALIGN_CELL_CENTER = 1, ALIGN_CELL_END = 2;
        readonly public Vector3 CELL_SIZE, POSITION_END, POSITION_START;

        public CellObject(Vector3 startPosition, Vector3 cellSize)
        {
            this.POSITION_START = startPosition;
            this.CELL_SIZE = cellSize;
            this.POSITION_END = startPosition + cellSize;
            this.IsEmpty = true;
        }

        public void Clear()
        {
            this.IsEmpty = true;
        }

        public bool IsEmpty { get; private set; }

        public bool IsEnclosing(PropObject propObject)
        {
            Vector3 min, max;
            min = propObject.BoundsMin;
            max = propObject.BoundsMax;
            //check min bounds at cell start position and end position
            bool minCheck = (this.POSITION_START.x <= min.x && this.POSITION_START.z <= min.z && this.POSITION_END.x >= min.x && this.POSITION_END.z >= min.z);
            if (minCheck == true) return true;
            //check max bounds at cell start position and end position
            return (this.POSITION_START.x <= max.x && this.POSITION_START.z <= max.z && this.POSITION_END.x >= max.x && this.POSITION_END.z >= max.z);
        }

        public Prop PropObj { get; private set; }

        public void Put(PropObject propObject, Vector3 offset, int align = 0)
        {
            Vector3 pos;
            switch (align)
            {
                case 1: pos = (this.POSITION_START + this.POSITION_END) / 2; break;
                case 2: pos = this.POSITION_END; break;
                default: pos = this.POSITION_START; break;
            }
            propObject.SetPosition(pos + offset);
            Mark(propObject);
        }

        public void Mark(Prop prop)
        {
            this.PropObj = prop;
            this.IsEmpty = false;
        }

    }
}
