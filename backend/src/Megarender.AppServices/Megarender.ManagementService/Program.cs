using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Megarender.WebServiceCore;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Megarender.ManagementService {
    public class Program {
        public static async Task<int> Main (string[] args) => await ProgramBase.RunBase<Startup>(ProgramBase.BuildWebHost<Startup>, args);
    }
}