using System.Collections.Generic;
using UnityEngine;

namespace GridMask
{
    public class CellObject
    {
        readonly public static int ALIGN_CELL_START = 0, ALIGN_CELL_CENTER = 1, ALIGN_CELL_END = 2;
        readonly public Vector3 CELL_SIZE, POSITION_END, POSITION_START;

        readonly private List<Prop> propList;

        public CellObject(Vector3 startPosition, Vector3 cellSize)
        {
            this.POSITION_START = startPosition;
            this.CELL_SIZE = cellSize;
            this.POSITION_END = startPosition + cellSize;
            this.propList = new List<Prop>();
        }

        public void Clear()
        {
            this.propList.Clear();
        }

        public List<Prop> GetPropList => this.propList;

        public bool IsEmpty => this.propList.Count < 1;

        public bool IsEnclosing(Vector3 vector3)
        {
            return (this.POSITION_START.x <= vector3.x && this.POSITION_START.z <= vector3.z && this.POSITION_END.x >= vector3.x && this.POSITION_END.z >= vector3.z);
        }

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
            this.propList.Add(prop);
        }

    }
}
