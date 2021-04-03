﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiModels;
using CommonModels;
using Database.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Misc;

namespace Services
{
    public class DatabaseFunctions
    {
        public static List<Credential> GetCredentials(PcHealthContext dbContext, NewAccountInfo newAccountInfo)
        {
            return dbContext.Credentials.Where(c => c.CredentialsUsername == newAccountInfo.CredentialsUsername).ToList();
        }

        public static List<Credential> GetCredentials(PcHealthContext dbContext, Credential credential)
        {
            return dbContext.Credentials.Where(c => c.CredentialsUsername == credential.CredentialsUsername).ToList();
        }

        public static void CreateNewAdmin(PcHealthContext dbContext, NewAccountInfo newAccountInfo)
        {
            var newAdmin = new Admin()
            {
                AdminFirstName = newAccountInfo.AdminFirstName,
                AdminLastName = newAccountInfo.AdminLastName,
                AdminCredentialsUsername = newAccountInfo.CredentialsUsername
            };
            dbContext.Admins.Add(newAdmin);
            dbContext.SaveChanges();
        }

        public static void CreateNewCredentials(PcHealthContext dbContext, NewAccountInfo newAccountInfo)
        {
            var (salt, passwordHash) = Services.HashServices.Encrypt(newAccountInfo.CredentialsPassword);
            var newCredential = new Credential()
            {
                CredentialsUsername = newAccountInfo.CredentialsUsername,
                CredentialsPassword = passwordHash,
                CredentialsSalt = salt
            };
            dbContext.Credentials.Add(newCredential);
            dbContext.SaveChanges();
        }

        public static string GetPasswordSalt(PcHealthContext dbContext, Credential credential)
        {
            var credentials = dbContext.Credentials;
            return credentials.Where(c => c.CredentialsUsername == credential.CredentialsUsername)
                .Select(c => c.CredentialsSalt).First().ToString();
        }

        public static string GetPasswordFromDb(PcHealthContext dbContext, Credential credential)
        {
            var credentials = dbContext.Credentials;
            return credentials.Where(c => c.CredentialsUsername == credential.CredentialsUsername)
                .Select(c => c.CredentialsPassword).First().ToString();
        }

        public static Pc CreatePc(DiagnosticData diagnosticData)
        {
            var newPc = new Pc()
            {
                AdminCredentialsUsername = diagnosticData.AdminUsername,
                PcCpuUsage = diagnosticData.CpuUsage,
                PcDiskTotalFreeSpace = diagnosticData.TotalFreeDiskSpace,
                PcDiskTotalSpace = diagnosticData.DiskTotalSpace,
                PcFirewallStatus = diagnosticData.FirewallStatus,
                PcId = diagnosticData.PcId,
                PcMemoryUsage = diagnosticData.MemoryUsage,
                PcNetworkAverageBytesReceived = diagnosticData.AvgNetworkBytesReceived,
                PcNetworkAverageBytesSend = diagnosticData.AvgNetworkBytesSent,
                PcOs = diagnosticData.Os,
                PcUsername = diagnosticData.PcUsername
            };
            return newPc;
        }

        public static void UpdatePcInDatabase(PcHealthContext _db, DiagnosticData diagnosticData, string admin)
        {
            var _pc = _db.Pcs.Where(p => p.AdminCredentialsUsername.Equals(admin) && p.PcId.Equals(diagnosticData.PcId)).FirstOrDefault<Pc>();
            if (_pc != null)
            {
                _pc.PcCpuUsage = diagnosticData.CpuUsage;
                _pc.PcDiskTotalFreeSpace = diagnosticData.TotalFreeDiskSpace;
                _pc.PcDiskTotalSpace = diagnosticData.DiskTotalSpace;
                _pc.PcFirewallStatus = diagnosticData.FirewallStatus;
                _pc.PcId = diagnosticData.PcId;
                _pc.PcMemoryUsage = diagnosticData.MemoryUsage;
                _pc.PcNetworkAverageBytesReceived = diagnosticData.AvgNetworkBytesReceived;
                _pc.PcNetworkAverageBytesSend = diagnosticData.AvgNetworkBytesSent;
                _pc.PcOs = diagnosticData.Os;
                _pc.PcUsername = diagnosticData.PcUsername;
                _db.SaveChanges();
            }
        }
    }
}
