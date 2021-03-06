using System;
using System.Collections.Generic;

namespace CommonModels
{
    public class DiagnosticData
    {
        public DiagnosticData()
        {
            Services = new List<Tuple<string, string>>();
        }
        public float CpuUsage { get; set; }

        public float TotalFreeDiskSpace { get; set; }

        public float DiskTotalSpace { get; set; }

        public double MemoryUsage { get; set; }

        public double AvgNetworkBytesSent { get; set; }

        public double AvgNetworkBytesReceived { get; set; }

        public string PcId { get; set; }

        public string Os { get; set; }

        public List<Tuple<string, string>> Services { get; set; }

        public string FirewallStatus { get; set; }
        public PcConfiguration PcConfiguration { get; set; }
        public int CurrentSecond { get; set; } = 0;
        public string HealthStatus { get; set; }
    }
}
