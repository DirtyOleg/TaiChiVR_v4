namespace TaiChiVR.Utility.EventDispatcher
{
    using TaiChiVR.Utility.Ables;
    using UnityEngine;
    using Zinnia.Extension;
    using Zinnia.Rule;
    using static Zinnia.Pointer.ObjectPointer;

    //This script will raise event when a Pointable GameObject is entered, exited or selected by VRTK pointer.  
    public class PointableEventDispatcher : MonoBehaviour
    {
        [SerializeField] RuleContainer SourceValidity = null;

        public void Selected(EventData data)
        {
            if (data == null || !SourceValidity.Accepts(data.CollisionData.collider.gameObject))
            {
                return;
            }

            //Check if hit any ISelectable Object, in this case, the Instructors
            IPointable pointable = data.CollisionData.collider.GetComponent<IPointable>();
            if (pointable != null)
            {
                pointable.SelectAction();
            }
        }

        public void Entered(EventData data)
        {
            if (data == null || !SourceValidity.Accepts(data.CollisionData.collider.gameObject))
            {
                return;
            }

            //Check if hit any IHighlightable Object, in this case, the Instructors
            IPointable pointable = data.CollisionData.collider.GetComponent<IPointable>();
            if (pointable != null)
            {
                pointable.EnterAction();
            }
        }

        public void Exited(EventData data)
        {
            if (data == null || !SourceValidity.Accepts(data.CollisionData.collider.gameObject))
            {
                return;
            }

            //Check if hit any IHighlightable Object, in this case, the Instructors
            IPointable pointable = data.CollisionData.collider.GetComponent<IPointable>();
            if (pointable != null)
            {
                pointable.ExitAction();
            }
        }
    }
}