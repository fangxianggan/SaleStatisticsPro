
using EntitiesModels.Models;
using EntitiesModels.Models.SysModels;
using EntitiesModels.HttpResponse;
using EntitiesModels.QueryModels;
using System.Collections.Generic;
using System.Web.Http;
using WebApi.IBLL;
using System;
using FXKJ.Infrastructure.Core.Attributes;
using EntitiesModels.DtoModels;

namespace WebApi.Controllers

{
   /// <summary>
    /// DataBackup 控制器api 代码自动生成
    /// </summary>
    public partial class DataBackupController : ApiController
    {

        /// <summary>
        /// 
        /// </summary>
        public DataBackupController()
        {
           
        }


        /// <summary>
        /// 保存数据 当前数据
        /// </summary>
        /// <param name="dataBackup"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DataBaseBackup")]
        [ActionRecord(Describe = "数据库备份")] 
        public HttpReponseModel<DataBackup> DataBaseBackup([FromBody] DataBackup dataBackup)
        {
            return  _dataBackupBLL.DataBaseBackup(dataBackup);
        }

        /// <summary>
        /// 数据库列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSelectViewModelList")]
        [ActionRecord(Describe = "数据库列表")]
        public HttpReponseModel<List<SelectViewModel>> GetSelectViewModelList()
        {
            return _dataBackupBLL.GetSelectViewModelList();
        }



        /// <summary>
        /// 默认配置下数据库备份的路径
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetDBDefaultPath")]
        [ActionRecord(Describe = "数据库备份文件路径")]
        public HttpReponseModel<string> GetDBDefaultPath()
        {
            return _dataBackupBLL.GetDBDefaultPath();
        }


        /// <summary>
        /// 默认配置下数据库备份的路径
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("DestoreData")]
        [ActionRecord(Describe = "还原数据库")]
        public HttpReponseModel<string> DestoreData(int id)
        {
            return _dataBackupBLL.DestoreData(id);
        }

    }
}

