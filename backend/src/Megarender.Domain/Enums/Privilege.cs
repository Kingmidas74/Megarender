using System;

namespace Megarender.Domain
{
    [Flags]
    public enum PrivilegeId:int {
        CanAuthorize=0,
        CanSeeScenes=1<<0,
        CanSeeRenderTasks=2<<0
    }
    public class Privilege {
        public PrivilegeId PrivilegeId { get; set; }
        public string Value { get; set; }
    }
}