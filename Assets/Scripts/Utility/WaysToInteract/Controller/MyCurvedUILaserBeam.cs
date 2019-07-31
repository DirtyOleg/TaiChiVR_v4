namespace TaiChiVR.Utility.WaysOfInteract.Controller
{
    using CurvedUI;
    using UnityEngine;

    //This script is copied from CurvedUILaserBeam of CurvedUI and modified
    public class MyCurvedUILaserBeam : MonoBehaviour
    {
        [SerializeField] Transform LaserBeamTransform = null;

        private bool isTriggerPressed = false;
        public void SetTriggerPressed(bool value)
        {
            isTriggerPressed = value;
        }

        private void OnDisable() 
        {            
            Ray myRay = new Ray();
            CurvedUIInputModule.CustomControllerRay = myRay;
            CurvedUIInputModule.CustomControllerButtonState = false;
        }

        // Update is called once per frame
        protected void Update()
        {
            //get direction of the controller
            Ray myRay = new Ray(this.transform.position, this.transform.forward);
            CurvedUIInputModule.CustomControllerRay = myRay;
            CurvedUIInputModule.CustomControllerButtonState = isTriggerPressed;

            //change the laser's length depending on where it hits
            float length = 10;

            RaycastHit hit;
            if (Physics.Raycast(myRay, out hit, length))
            {
                length = Vector3.Distance(hit.point, this.transform.position) / 10;
            }

            //set the leangth of the beam
            LaserBeamTransform.localScale = LaserBeamTransform.localScale.ModifyZ(length);
        }
    }
}