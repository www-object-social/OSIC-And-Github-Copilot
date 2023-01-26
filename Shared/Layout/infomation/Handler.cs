using OSIC.Shared.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSIC.Shared.Layout.infomation;
class Handler
{
    private readonly OSIC.Shared.Layout.frame.Handler FrameHandler;
    private readonly OSIC.Shared.Project.Configation ProjectConfigation;
    public OSIC.Shared.Project.Software Software { get; set; }
    public event Action Change = null!;
    public void Open(OSIC.Shared.Project.Software Software) {
        if (Software == this.Software) return;
        this.Software = Software;
        this.Change?.Invoke();
    }
    public async void Open() {
        this.Software = ProjectConfigation.Software is OSIC.Shared.Project.Software.OBJECTSOCIAL or OSIC.Shared.Project.Software.InterConnecting ? this.Software : Software.Claims;
        this.Change?.Invoke();   
        await FrameHandler.Open("OSIC.Shared.Layout.infomation.Component");
    }
    public Handler(OSIC.Shared.Layout.frame.Handler FrameHandler,OSIC.Shared.Project.Configation ProjectConfigation )
    {
        this.FrameHandler = FrameHandler;
        this.ProjectConfigation = ProjectConfigation;
    }
}
