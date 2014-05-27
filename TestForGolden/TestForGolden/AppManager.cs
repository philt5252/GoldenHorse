using System;
using System.Collections.Generic;

namespace TestForGolden
{
    public class AppManager
    {
        private Dictionary<string, IAppWindow> windowDict = new Dictionary<string, IAppWindow>();
        private Dictionary<string, IAppControl> controlDict = new Dictionary<string, IAppControl>();
        private Dictionary<string, IAppProcess> processDict = new Dictionary<string, IAppProcess>();

        public IAppWindow GetAppWindow(string id)
        {
            if (windowDict.ContainsKey(id))
                return windowDict[id];

            throw new Exception(string.Format("No window exists with id '{0}'", id));
        }

        public IAppControl GetAppControl(string id)
        {
            if (controlDict.ContainsKey(id))
                return controlDict[id];

            throw new Exception(string.Format("No control exists with id '{0}'", id));
        }

        public IAppProcess GetAppProcess(string id)
        {
            if (processDict.ContainsKey(id))
                return processDict[id];

            throw new Exception(string.Format("No process exists with id '{0}'", id));
        }

        public void SetAppWindow(IAppWindow appWindow)
        {
            windowDict[appWindow.Id] = appWindow;
        }

        public void SetAppControl(IAppControl appControl)
        {
            controlDict[appControl.Id] = appControl;
        }

        public void SetAppProcess(IAppProcess appProcess)
        {
            processDict[appProcess.Id] = appProcess;
        }
    }
}