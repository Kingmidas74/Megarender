using System;

namespace Megarender.IdentityService
{
    public class ApplicationOptions {
        public String Pepper { get; set; }
        public int LowerBoundCode {get;set;}
        public int UpperBoundCode {get;set;}
    }
}