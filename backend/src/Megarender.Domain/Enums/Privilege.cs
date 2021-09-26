using System;

namespace Megarender.Domain
{
    [Flags]
    public enum PrivilegeId
    {
        None=0,
        CanAuthorize=1<<0,
        CanSeeScenes=1<<1,
        CanSeeRenderTasks=1<<2
    }
    public class Privilege {
        public PrivilegeId PrivilegeId { get; set; }
        public string Value { get; set; }
    }
}