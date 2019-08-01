Important: This simulator prefab could and should only be used in Welcome, Customize, InstructorSelection, TerrainSelection, and Simulation scene. Used in Initialization scene will conflict with other VRTK component, which will cause errors. The simulation prefab is already placed in those scene, you do not need to add it again.

------------------------------------------------------------------------------

Overview:
This simulator prefab includes almost all the component, such as camera, terrain, and instructor, needed for testing one single scene. All you need to do is to enable it when testing, and to disable it when you done testing.

The way this simulator interacting with other objects is mouse control + gaze control. When you move mouse around, the view of camera will change. When you look at an Button UI and click the left mouse button, the OnClick will be trigger. When you look at a "Pointable object"* or a Teleport Destination Location, the gaze control will take over, so you will see a circle being filled and when it fully filled, the object you are looking at will be selected.

* a Pointable object is a GameObject that has a collider and a script, which implements the IPointable interface. For example, each instructor is a Pointable object, PointThisInstructor is a script implemented the IPointable interface.

------------------------------------------------------------------------------

Known Issue:

1. If the code needed to be tested involved scene changing, for example, calling "UnityEngine.SceneManagement.SceneManager.LoadScene();", this prefab may be broken. Because this prefab is designed to be used exclusively for testing one single scene, it will not carry any infomation from this scene to the next scene, therefore, there are chances the next scene will not run correctly.

2. In Simulation scene, you will encounter the following error: 
"NullReferenceException: Object reference not set to an instance of an object
TaiChiVR.Utility.ListManager.InstructorListManager.GetAnimStateNormalizedTime () (at Assets/Scripts/Utility/List Manager/InstructorListManager.cs:243)"