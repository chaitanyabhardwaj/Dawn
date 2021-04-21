using UnityEngine;

namespace GridMask
{
    public class PropObject : Prop
    {

        readonly private GameObject gameObject;

        public PropObject(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }

        public Vector3 Position => this.gameObject.transform.position;

        public Vector3 Size => this.gameObject.transform.localScale;

        public void SetPosition(Vector3 vector3) => gameObject.transform.position = vector3;

        public void SetSize(Vector3 vector3) => gameObject.transform.localScale= vector3;

    }
}
