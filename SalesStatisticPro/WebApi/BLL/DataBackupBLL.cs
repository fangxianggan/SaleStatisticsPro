
using EntitiesModels.Models;
using FXKJ.Infrastructure.Logic;
using WebApi.IBLL;
using EntitiesModels.HttpResponse;
using System.Collections.Generic;
using EntitiesModels.Enum;
using FXKJ.Infrastructure.DataAccess;
using FXKJ.Infrastructure.Core.Helper;
using EntitiesModels.DtoModels;
using FXKJ.Infrastructure.Core.Util;

namespace WebApi.BLL
{
    /// <summary>
    ///   业务访问——DataBackup
    /// </summary>
    public partial class DataBackupBLL : IDataBackupBLL
    {

        private readonly IEFRepository<DataBackup> _dataBackupEFRepository;
        private readonly IEFRepository<SysConfig> _sysConfigEFRepository;
        /// <summary>
        /// 
        /// </summary>
        public DataBackupBLL(IEFRepository<DataBackup> dataBackupEFRepository, IEFRepository<SysConfig> sysConfigEFRepository,ILogic<DataBackup> logic)
            :this(logic)
        {
            _dataBackupEFRepository = dataBackupEFRepository;
            _sysConfigEFRepository = sysConfigEFRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataBackup"></param>
        /// <returns></returns>
        public HttpReponseModel<DataBackup> DataBaseBackup(DataBackup dataBackup)
        {
            HttpReponseModel<DataBackup> httpReponse = new HttpReponseModel<DataBackup>();
            FileUtil.CreateDirectory(dataBackup.Path);
            DataBaseHelper.DataBaseBackup(GlobalParamsHelper.ReadConnectionString(), dataBackup.DBName, dataBackup.Path+"\\"+dataBackup.BackUpName);
            httpReponse.Data = _dataBackupEFRepository.Add(dataBackup);
            return httpReponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public HttpReponseModel<List<SelectViewModel>> GetSelectViewModelList()
        {
            HttpReponseModel<List<SelectViewModel>> httpReponse = new HttpReponseModel<List<SelectViewModel>>();
            List<SelectViewModel> list = new List<SelectViewModel>();
            var dbList = DataBaseHelper.DatabaseListNames(GlobalParamsHelper.ReadConnectionString());
            int i = 1;
            foreach (var item in dbList)
            {
                SelectViewModel viewModel = new SelectViewModel();
                viewModel.Key = i.ToString();
                viewModel.Label = item;
                viewModel.Value = item;
                list.Add(viewModel);
                i++;
            }
            httpReponse.Data = list;
            return httpReponse;
        }

        /// <summary>
        /// 获取默认存储路径
        /// </summary>
        /// <returns></returns>
        public HttpReponseModel<string> GetDBDefaultPath()
        {
            HttpReponseModel<string> httpReponse = new HttpReponseModel<string>();
            httpReponse.Data=  _sysConfigEFRepository.GetEntity(p => p.Code == "databackupPath").Value;
            return httpReponse;
        }

        /// <summary>
        ///数据库还原
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HttpReponseModel<string> DestoreData(int id)
        {
            HttpReponseModel<string> httpReponse = new HttpReponseModel<string>();
            var ent= _dataBackupEFRepository.GetEntity(id);
            var res=  DataBaseHelper.DataBaseRestore(GlobalParamsHelper.ReadConnectionString(),ent.DBName,ent.Path + "\\" + ent.BackUpName);
            httpReponse.Flag = res!=0? true: false;
            if (httpReponse.Flag)
            {
                httpReponse.ResultSign = ResultSign.Successful;
                httpReponse.Message = "数据库还原成功!";
            }
            else {
                httpReponse.ResultSign = ResultSign.Error;
                httpReponse.Message = "数据库还原失败!";
            }
            return httpReponse;
        }


    }
}

