using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using DCCRailway.Common.Helpers;

namespace DCCRailway.WiThrottle.ServiceHelper;

public static class ServiceKill {
    public static IResult KillService(int port) {
        try {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                return KillProcessOnPortWindows(port);
            } else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) || RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) {
                return KillProcessOnPortUnix(port);
            } else {
                return Result.Fail("Unsupported operating system");
            }
        } catch (Exception ex) {
            return Result.Fail("Failed to terminate process.", ex);
        }
    }

    private static IResult KillProcessOnPortWindows(int port) {
        var netstatProcess = new Process();
        netstatProcess.StartInfo.FileName               = "netstat";
        netstatProcess.StartInfo.Arguments              = "-ano";
        netstatProcess.StartInfo.RedirectStandardOutput = true;
        netstatProcess.StartInfo.UseShellExecute        = false;
        netstatProcess.StartInfo.CreateNoWindow         = true;
        netstatProcess.Start();

        var output = netstatProcess.StandardOutput.ReadToEnd();
        netstatProcess.WaitForExit();

        var pattern = $@"\s+0\.0\.0\.0:{port}\s+.*\s+LISTENING\s+(\d+)";
        var regex   = new Regex(pattern);
        var match   = regex.Match(output);

        if (match.Success) {
            var pid     = int.Parse(match.Groups[1].Value);
            var process = Process.GetProcessById(pid);
            process.Kill();
            return Result.Ok($"Process with PID {pid} listening on port {port} has been terminated.");
        } else {
            return Result.Fail($"No process found listening on port {port}");
        }
    }

    private static IResult KillProcessOnPortUnix(int port) {
        var lsofProcess = new Process();
        lsofProcess.StartInfo.FileName               = "lsof";
        lsofProcess.StartInfo.Arguments              = $"-i :{port}";
        lsofProcess.StartInfo.RedirectStandardOutput = true;
        lsofProcess.StartInfo.UseShellExecute        = false;
        lsofProcess.StartInfo.CreateNoWindow         = true;
        lsofProcess.Start();

        var output = lsofProcess.StandardOutput.ReadToEnd();
        lsofProcess.WaitForExit();

        var pattern = @"\blisten\b.*?(\d+)";
        var regex   = new Regex(pattern, RegexOptions.IgnoreCase);
        var match   = regex.Match(output);

        if (match.Success) {
            var pid         = int.Parse(match.Groups[1].Value);
            var killProcess = new Process();
            killProcess.StartInfo.FileName        = "kill";
            killProcess.StartInfo.Arguments       = $"-9 {pid}";
            killProcess.StartInfo.UseShellExecute = false;
            killProcess.StartInfo.CreateNoWindow  = true;
            killProcess.Start();
            killProcess.WaitForExit();
            return Result.Ok($"Process with PID {pid} listening on port {port} has been terminated.");
        } else {
            return Result.Fail($"No process found listening on port {port}");
        }
    }
}