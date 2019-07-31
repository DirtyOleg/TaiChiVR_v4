namespace TaiChiVR.Utility.Scriptables
{
    using UnityEngine;
    using TaiChiVR.Utility.Data;

    [CreateAssetMenu(fileName = "New InstructorList", menuName = "InstructorList", order = 51)]
    public class InstructorListScriptable : ScriptableObject
    {
        public InstructorInfo[] instructorList;
        public RuntimeAnimatorController[] animControllerList;
    }
}