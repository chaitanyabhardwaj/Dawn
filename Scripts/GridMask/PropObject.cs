using UnityEngine;

namespace GridMask
{
    public class PropObject : Prop
    {

        public readonly bool MULTI_SPAN_IF_POSSIBLE;

        private readonly GameObject gameObject;
        private readonly Renderer renderer;

        public PropObject(GameObject gameObject, bool multiSpanIfPossible = true)
        {
            this.gameObject = gameObject;
            this.renderer = gameObject.GetComponent<Renderer>();
            this.MULTI_SPAN_IF_POSSIBLE = multiSpanIfPossible;
        }

        public Vector3 BoundsMax => this.renderer.bounds.max;

        public Vector3 BoundsMin => this.renderer.bounds.min;

        public Vector3 Position => this.gameObject.transform.position;

        public Vector3 Size => this.renderer.bounds.max - this.renderer.bounds.min;

        public void SetPosition(Vector3 vector3) => this.gameObject.transform.position = vector3;

        //public void SetSize(Vector3 vector3) => this.gameObject.transform.localScale= vector3;

    }
}
