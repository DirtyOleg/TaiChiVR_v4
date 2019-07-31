namespace TaiChiVR.Simulation
{
    using TaiChiVR.Simulation.ControlPanel;
    using TaiChiVR.Simulation.MenuPanel;
    using TaiChiVR.Utility.ListManager;
    using UnityEngine;

    //This script defines method, which will be used in dealing with panel switch between MenuPanel and ControlPanel
    public class TwoPanelSwitcher : MonoBehaviour
    {
        [SerializeField] MenuPanelManager menuPanelManager = null;
        [SerializeField] ControlPanelManager controlPanelManager = null;
        [SerializeField] OppositeTelePanelManager oppositeTelePanelManager = null;
        [SerializeField] TeleDestListManager teleDestManager = null;

        bool doublePauseFlag = false; // double-pause situation happens when instructor animation is already paused when user try to turn on MenuPanel, which will call PauseAnimation() again. One problem double-pause could cause is that when user close MenuPanel (reproduce procedure: pause instructor animation -> turn on Menu Panel -> turn off Menu Panel), then the pause/resume button in ControlPanel will not work anymore, while all other three button work fine. The cause for this undesired result is bacause the second PauseAnimation() will set savedSpeed varibale of InstructorAnimManager to 0, and when user close MenuPanel and trigger ResumeAnimation(), the speed for animator is 0, which is not correct.

        //TODO: even though double pause flag is introduced, it does not prevent situation aforementioned from happening, when user change instructor 
        bool isMenuPanelOn = true;

        public void SwitchPanel()
        {
            if (isMenuPanelOn)
            {
                //user wanna turn off MenuPanel    
                if (!doublePauseFlag)
                {
                    InstructorListManager.Instance.ResumeAnimation();
                }
                else
                {
                    doublePauseFlag = false;
                }

                menuPanelManager.DisablePanelObj();
                controlPanelManager.EnablePanelObj();
                teleDestManager.EnableLocations();
                oppositeTelePanelManager.EnablePanelObj();
            }
            else
            {
                //user wanna turn on MenuPanel                 
                if (!InstructorListManager.Instance.isPaused)
                {
                    InstructorListManager.Instance.PauseAnimation();
                }
                else
                {
                    doublePauseFlag = true;
                }

                controlPanelManager.DisablePanelObj();
                teleDestManager.DisEnableLocations();
                oppositeTelePanelManager.DisablePanelObj();
                menuPanelManager.EnablePanelObj();
            }

            isMenuPanelOn = !isMenuPanelOn;
        }
    }
}