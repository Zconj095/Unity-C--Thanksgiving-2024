using System;
using System.Collections.Generic;
using UnityEngine;

public class QiskitLogger
{
    private readonly Dictionary<QiskitLogDomains, LogLevel> logLevels;

    public QiskitLogger()
    {
        // Initialize default log levels for each domain
        logLevels = new Dictionary<QiskitLogDomains, LogLevel>();
        foreach (QiskitLogDomains domain in Enum.GetValues(typeof(QiskitLogDomains)))
        {
            logLevels[domain] = LogLevel.Info; // Default log level
        }
    }

    public void BuildLoggingConfig(LogLevel level, List<QiskitLogDomains> domains, string filepath = null)
    {
        // Set the logging level for specified domains
        foreach (var domain in domains)
        {
            logLevels[domain] = level;
        }

        // Filepath handling could be added here if needed in the future
        if (!string.IsNullOrEmpty(filepath))
        {
            Debug.LogWarning($"Logging to files is not supported in this implementation. Provided filepath: {filepath}");
        }
    }

    public LogLevel GetLoggingLevel(QiskitLogDomains domain)
    {
        return logLevels.ContainsKey(domain) ? logLevels[domain] : LogLevel.None;
    }

    public void SetLoggingLevel(LogLevel level, List<QiskitLogDomains> domains, string filepath = null)
    {
        BuildLoggingConfig(level, domains, filepath);
    }

    public LogLevel GetQiskitAquaLogging()
    {
        return GetLoggingLevel(QiskitLogDomains.DOMAIN_AQUA);
    }

    public void SetQiskitAquaLogging(LogLevel level, string filepath = null)
    {
        SetLoggingLevel(level, new List<QiskitLogDomains> { QiskitLogDomains.DOMAIN_AQUA }, filepath);
    }

    public void Log(QiskitLogDomains domain, LogLevel level, string message)
    {
        if (!logLevels.ContainsKey(domain) || level < logLevels[domain])
        {
            return; // Skip logging if the message level is below the domain's current log level
        }

        switch (level)
        {
            case LogLevel.Debug:
                Debug.Log($"[DEBUG] [{domain}] {message}");
                break;
            case LogLevel.Info:
                Debug.Log($"[INFO] [{domain}] {message}");
                break;
            case LogLevel.Warning:
                Debug.LogWarning($"[WARNING] [{domain}] {message}");
                break;
            case LogLevel.Error:
                Debug.LogError($"[ERROR] [{domain}] {message}");
                break;
            default:
                Debug.Log($"[{level}] [{domain}] {message}");
                break;
        }
    }
}

public enum QiskitLogDomains
{
    DOMAIN_AQUA,
    DOMAIN_CHEMISTRY,
    DOMAIN_FINANCE,
    DOMAIN_ML,
    DOMAIN_OPTIMIZATION
}

public enum LogLevel
{
    Debug = 1,
    Info = 2,
    Warning = 3,
    Error = 4,
    None = 5
}
