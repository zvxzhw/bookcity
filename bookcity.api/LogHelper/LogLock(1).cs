 "AOPLog.log"), Encoding.UTF8)
                .Split("--------------------------------")
                .Where(d => !string.IsNullOrEmpty(d) && d != "\n" && d != "\r\n")
                .Select(d => new LogInfo
                {
                    Datetime = d.Split("|")[0].ObjToDate(),
                    Content = d.Split("|")[1]?.Replace("\r\n", "<br>"),
                    LogColor = "AOP",
                }).ToList();

            }
            catch (Exception)
            {
            }

            try
            {
                excLogs = ReadLog(Path.Combine(_contentRoot, "Log", $"GlobalExcepLogs_{DateTime.Now.ToString("yyyMMdd")}.log"), Encoding.UTF8)?
                      .Split("--------------------------------")
                      .Where(d => !string.IsNullOrEmpty(d) && d != "\n" && d != "\r\n")
                      .Select(d => new LogInfo
                      {
                          Datetime = (d.Split("|")[0]).Split(',')[0].ObjToDate(),
                          Content = d.Split("|")[1]?.Replace("\r\n", "<br>"),
                          LogColor = "EXC",
                          Import = 9,
                      }).ToList();
            }
            catch (Exception)
            {
            }


            try
            {
                sqlLogs = ReadLog(Path.Combine(_contentRoot, "Log", "SqlLog.log"), Encoding.UTF8)
                      .Split("--------------------------------")
                      .Where(d => !string.IsNullOrEmpty(d) && d != "\n" && d != "\r\n")
                      .Select(d => new LogInfo
                      {
                          Datetime = d.Split("|")[0].ObjToDate(),
                          Content = d.Split("|")[1]?.Replace("\r\n", "<br>"),
                          LogColor = "SQL",
                      }).ToList();
            }
            catch (Exception)
            {
            }

            try
            {
                reqresLogs = ReadLog(Path.Combine(_contentRoot, "Log", "RequestResponseLog.log"), Encoding.UTF8)
                      .Split("--------------------------------")
                      .Where(d => !string.IsNullOrEmpty(d) && d != "\n" && d != "\r\n")
                      .Select(d => new LogInfo
                      {
                          Datetime = d.Split("|")[0].ObjToDate(),
                          Content = d.Split("|")[1]?.Replace("\r\n", "<br>"),
                          LogColor = "ReqRes",
                      }).ToList();
            }
            catch (Exception)
            {
            }

            if (excLogs != null)
            {
                aopLogs.AddRange(excLogs);
            }
            if (sqlLogs != null)
            {
                aopLogs.AddRange(sqlLogs);
            }
            if (reqresLogs != null)
            {
                aopLogs.AddRange(reqresLogs);
            }
            aopLogs = aopLogs.OrderByDescending(d => d.Import).ThenByDescending(d => d.Datetime).Take(100).ToList();

            return aopLogs;
        }


        public static RequestApiWeekView RequestApiinfoByWeek()
        {
            List<RequestInfo> Logs = new List<RequestInfo>();
            List<ApiWeek> apiWeeks = new List<ApiWeek>();
            string apiWeeksJson = string.Empty;
            List<string> columns = new List<string>();
            columns.Add("日期");


            try
            {
                Logs = JsonConvert.DeserializeObject<List<RequestInfo>>("[" + ReadLog(Path.Combine(_contentRoot, "Log", "RequestIpInfoLog.log"), Encoding.UTF8) + "]");

                var ddd = Logs.Where(d => d.Week == "周日").ToList();

                apiWeeks = (from n in Logs
                            group n by new { n.Week, n.Url } into g
                            select new ApiWeek
                            {
                                week = g.Key.Week,
                                url = g.Key.Url,
                                count = g.Count(),
                            }).ToList();

                //apiWeeks = apiWeeks.OrderByDescending(d => d.count).Take(8).ToList();

            }
            catch (Exception)
            {
            }

            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("[");

            var weeks = apiWeeks.GroupBy(x => new { x.week }).Select(s => s.First()).ToList();
            foreach (var week in weeks)
            {
                var apiweeksCurrentWeek = apiWeeks.Where(d => d.week == week.week).OrderByDescending(d => d.count).Take(8).ToList();
                jsonBuilder.Append("{");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("日期");
                jsonBuilder.Append("\":\"");
                jsonBuilder.Append(week.week);
                jsonBuilder.Append("\",");

                foreach (var item in apiweeksCurrentWeek)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(item.url);
                    jsonBuilder.Append("\":\"");
                    jsonBuilder.Append(item.count);
                    jsonBuilder.Append("\",");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("},");
            }

            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");

            columns.AddRange(apiWeeks.OrderByDescending(d => d.count).Take(8).Select(d => d.url).ToList());

            return new RequestApiWeekView()
            {
                columns = columns,
                rows = jsonBuilder.ToString(),
            };
        }

        public static AccessApiDateView AccessApiByDate()
        {
            List<RequestInfo> Logs = new List<RequestInfo>();
            List<ApiDate> apiDates = new List<ApiDate>();
            try
            {
                Logs = JsonConvert.DeserializeObject<List<RequestInfo>>("[" + ReadLog(Path.Combine(_contentRoot, "Log", "RequestIpInfoLog.log"), Encoding.UTF8) + "]");

                apiDates = (from n in Logs
                            group n by new { n.Date } into g
                            select new ApiDate
                            {
                                date = g.Key.Date,
                                count = g.Count(),
                            }).ToList();

                apiDates = apiDates.OrderBy(d => d.date).Take(7).ToList();

            }
            catch (Exception)
            {
            }

            return new AccessApiDateView()
            {
                columns = new string[] { "date", "count" },
                rows = apiDates,
            };
        }

        public static AccessApiDateView AccessApiByHour()
        {
            List<RequestInfo> Logs = new List<RequestInfo>();
            List<ApiDate> apiDates = new List<ApiDate>();
            try
            {
                Logs = JsonConvert.DeserializeObject<List<RequestInfo>>("[" + ReadLog(Path.Combine(_contentRoot, "Log", "RequestIpInfoLog.log"), Encoding.UTF8) + "]");

                apiDates = (from n in Logs
                            where n.Datetime.ObjToDate() >= DateTime.Today
                            group n by new { hour = n.Datetime.ObjToDate().Hour } into g
                            select new ApiDate
                            {
                                date = g.Key.hour.ToString("00"),
                                count = g.Count(),
                            }).ToList();

                apiDates = apiDates.OrderBy(d => d.date).Take(24).ToList();

            }
            catch (Exception)
            {
            }

            return new AccessApiDateView()
            {
                columns = new string[] { "date", "count" },
                rows = apiDates,
            };
        }
    }


}
                                                                                                                                                                                                                                                                                                                                 ﻿using log4net;
using log4net.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Xml;

namespace bookcity.common.LogHelper
{
    public class Log4NetLogger : ILogger
    {
        private readonly string _name;
        private readonly XmlElement _xmlElement;
        private readonly ILog _log;
        private ILoggerRepository _loggerRepository;
        public Log4NetLogger(string name, XmlElement xmlElement)
        {
            _name = name;
            _xmlElement = xmlElement;
            _loggerRepository = log4net.LogManager.CreateRepository(
                Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));
            _log = LogManager.GetLogger(_loggerRepository.Name, name);
            log4net.Config.XmlConfigurator.Configure(_loggerRepository, xmlElement);
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Critical:
                    return _log.IsFatalEnabled;
                case LogLevel.Debug:
                case LogLevel.Trace:
                    return _log.IsDebugEnabled;
                case LogLevel.Error:
                    return _log.IsErrorEnabled;
                case LogLevel.Information:
                    return _log.IsInfoEnabled;
                case LogLevel.Warning:
                    return _log.IsWarnEnabled;
                default:
                    throw new ArgumentOutOfRangeException(nameof(logLevel));
            }
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state,
            Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }
            string message = null;
            if (null != formatter)
            {
                message = formatter(state, exception);
            }
            if (!string.IsNullOrEmpty(message) || exception != null)
            {
                switch (logLevel)
                {
                    case LogLevel.Critical:
                        _log.Fatal(message);
                        break;
                    case LogLevel.Debug:
                    case LogLevel.Trace:
                        _log.Debug(message);
                        break;
                    case LogLevel.Error:
                        _log.Error(message);
                        break;
                    case LogLevel.Information:
                        _log.Info(message);
                        break;
                    case LogLevel.Warning:
                        _log.Warn(message);
                        break;
                    default:
                        _log.Warn($"Encountered unknown log level {logLevel}, writing out as Info.");
                        _log.Info(message, exception);
                        break;
                }
            }
        }
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         