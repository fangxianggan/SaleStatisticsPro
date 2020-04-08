using FXKJ.Infrastructure.Auth;
using FXKJ.Infrastructure.Auth.Auth;
using FXKJ.Infrastructure.Config;
using log4net.Appender;
using log4net.Config;
using log4net.Layout;
using System;
using System.IO;
using System.Reflection;
using System.Web;

namespace FXKJ.Infrastructure.Log.Log4NetWrite
{
    /// <summary>
    ///     日志记录:依赖Log4net
    /// </summary>
    public class LogWriter
    {
        #region 常量
        private static readonly object Lock = new object();

        /// <summary>
        ///     日志记录地址
        /// </summary>
        private static readonly string LogPath = GlobalParams.Get("logPath").ToString();

        #endregion

        #region 方法

        /// <summary>
        ///     记录日志
        /// </summary>
        /// <param name="folderName">文件夹名字</param>
        /// <param name="message">日志内容</param>
        /// <param name="path">日志存放磁盘路径</param>

        public static void WriteLog(string folderName,
            string message,
            string path = "")
        {
            try
            {

                AuthInfoViewModel authInfo = FormAuthenticationExtension.CurrentAuth();
                if (authInfo == null)
                {
                    authInfo = new AuthInfoViewModel();
                    authInfo.Name = "测试用户";
                    authInfo.PhoneNumber = "15255458934";
                    authInfo.GuidId = new Guid("00000000-0000-0000-0000-000000000000");
                }


                var strPath = string.IsNullOrEmpty(path) ? LogPath : path;
                strPath = strPath + folderName + "\\" + DateTime.Now.ToString("yyyy-MM-dd");
                lock (Lock)
                {
                    var strFilename = strPath + "\\" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt";
                    if (!Directory.Exists(strPath))
                    {
                        Directory.CreateDirectory(strPath);
                    }
                    var layout = new PatternLayout("%m%n");
                    var appender = new FileAppender(layout, strFilename, true);
                    BasicConfigurator.Configure(appender);
                    var log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
                    log.Info(
                        "</br>----------------------------------------------</br>\r\n" +
                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "  " + authInfo.Name + "(" + authInfo.GuidId + ")" + "</br>\r\n" + message);
                    log4net.LogManager.Shutdown();
                }
            }
            catch
            {
                throw new Exception("日志记录失败");
            }
        }

        [Obsolete]
        public static void WriteLog(string folderName,
         string message,
         string path,
          int p = 1)
        {
            try
            {
                AuthInfoViewModel authInfo = FormAuthenticationExtension.CurrentAuth();
                if (authInfo == null)
                {
                    authInfo = new AuthInfoViewModel();
                    authInfo.Name = "测试用户";
                    authInfo.PhoneNumber = "15255458934";
                    authInfo.GuidId = new Guid("00000000-0000-0000-0000-000000000000");
                }

                lock (Lock)
                {
                    var strFilename = path + "\\" + folderName;
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    var layout = new PatternLayout("%m%n");
                    var appender = new FileAppender(layout, strFilename, true);
                    BasicConfigurator.Configure(appender);
                    var log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
                    log.Info(message);
                    log4net.LogManager.Shutdown();
                }
            }
            catch
            {
                throw new Exception("日志记录失败");
            }
        }

        /// <summary>
        ///     日志记录
        /// </summary>
        /// <param name="folderName">文件夹名字</param>
        /// <param name="message">内容</param>
        /// <param name="fileName">文件名（不带后缀）</param>
        /// <param name="path">保存文件地址</param>
        [Obsolete]
        public static void WriteLog(string folderName,
            string message,
            string fileName,
            string path)
        {
            try
            {
                AuthInfoViewModel authInfo = FormAuthenticationExtension.CurrentAuth();
                if (authInfo == null)
                {
                    authInfo = new AuthInfoViewModel();
                    authInfo.Name = "测试用户";
                    authInfo.PhoneNumber = "15255458934";
                    authInfo.GuidId = new Guid("00000000-0000-0000-0000-000000000000");
                }
                var strPath = string.IsNullOrEmpty(path) ? LogPath : path;
                strPath = strPath + folderName + "\\" + DateTime.Now.ToString("yyyy-MM-dd");
                lock (Lock)
                {
                    var strFilename = strPath + "\\" + fileName + ".txt";
                    if (!Directory.Exists(strPath))
                    {
                        Directory.CreateDirectory(strPath);
                    }
                    var layout = new PatternLayout("%m%n");
                    var appender = new FileAppender(layout, strFilename, true);
                    BasicConfigurator.Configure(appender);
                    var log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
                    log.Info(
                        "</br>----------------------------------------------</br>\r\n" +
                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "  " + authInfo.Name + "(" + authInfo.GuidId + ")" + "</br>\r\n" + message);
                    log4net.LogManager.Shutdown();
                }
            }
            catch
            {
                throw new Exception("日志记录失败");
            }
        }

        /// <summary>
        ///     记录异常日志
        /// </summary>
        /// <param name="folderName">文件夹名称</param>
        /// <param name="e">异常</param>
        /// <param name="fileName">文件名称</param>
        [Obsolete]
        public static void WriteLog(string folderName,
            Exception e,
            string fileName = "")
        {
            if (string.IsNullOrEmpty(fileName))
            {
                WriteLog(folderName, e.Message + "\r\n" + e.Source + "\r\n" + e.StackTrace);
            }
            else
            {
                WriteLog(folderName, e.Message + "\r\n" + e.Source + "\r\n" + e.StackTrace, fileName);
            }
        }

        #endregion
    }
}
