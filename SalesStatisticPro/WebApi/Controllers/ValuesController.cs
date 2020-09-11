using EntitiesModels;
using EntitiesModels.Models;
using FXKJ.Infrastructure.Core.Helper;
using FXKJ.Infrastructure.Core.Util;
using FXKJ.Infrastructure.DataAccess;
using FXKJ.Infrastructure.Log.Log4NetWrite;
using FXKJ.Infrastructure.Log.LogModel;
using FXKJ.Infrastructure.WebApi;
using FXKJ.Infrastructure.WebApi.Filter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;
using System.Web.Http;

namespace WebApi.Controllers
{

    /// <summary>
    /// bcbcbcbcbcbcbccbcb
    /// </summary>
    public class ValuesController : ApiController
    {
        private readonly IEFRepository<Business> _businessEFRepository;
        private readonly IEFRepository<Category> _categoryEFRepository;
        public ValuesController(IEFRepository<Business> businessEFRepository, IEFRepository<Category> categoryEFRepository)
        {
            _businessEFRepository = businessEFRepository;
            _categoryEFRepository = categoryEFRepository;
        }
        // GET api/values

        /// <summary>
        /// gggg
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> Get()
        {

           var dd=  FileUtil.CreateDir("kg");
            // FileUtil.GetFileNames();
            //写入文本记录
           // LogWriter.WriteLog(FolderName.Info, "3333333");
           return new string[] { "value1", "value2" ,dd};
        }

        /// <summary>
        /// hhhh
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/values/5
        //  [ApiDTC]
        ////  [ApiCustmTransactionScope]
        //public string Get(int id)
        //{

        //    //var sql = string.Format(@"INSERT INTO [dbo].[Business]
        //    //      ([BusinessCode]
        //    //      ,[BusinessName]
        //    //      ,[CreateTime]
        //    //      ,[CreateUserCode]
        //    //      ,[UpdateTime]
        //    //      ,[UpdateUserCode]
        //    //      ,[Remark]
        //    //      ,[P_MerchantNo])
        //    //VALUES
        //    //      (
        //    //'111'
        //    //      ,'2222'
        //    //      ,'2019-09-09'
        //    //      ,'333'
        //    //      ,'2019-09-09'
        //    //      ,'333'
        //    //      ,'4444'
        //    //      ,'55555')");
        //    //SqlUtil.ExecuteNonQuery(GlobalParamsHelper.ReadConnectionString(), CommandType.Text, sql);

        //    //Business business = new Business();
        //    //business.BusinessCode = "111";
        //    //business.BusinessName = "111";
        //    //business.CreateTime = DateTime.Now;
        //    //business.UpdateTime = DateTime.Now;
        //    //var aa = _businessEFRepository.Add(business);

        //    //Category category = new Category();
        //    //category.CategoryCode = null;
        //    //category.CategoryName = "1111";
        //    //var bb = _categoryEFRepository.Add(category);

        //    //for (var i=0;i<10000;i++)
        //    //{
        //    //    DoRedisHash.SetEntryInHash("auth-token", i.ToString(), i.ToString());
        //    //}

        //    //int o = 0;
        //    //int d = 6 / o;
        //    //return d.ToString();
        //    //var pathUrl = AppDomain.CurrentDomain.SetupInformation.ApplicationBase ;
        //    // return ConfigUtils.GetKey(pathUrl + "Web.config", "JWTSecretKey");
        //    return "value";
        //}



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
        [AllowAnonymous]
        [Route("GetKKK")]
        public string GetKKK()
        {
            string fileName = "C://Users//m1767//Desktop//aa.xls";
            var dt = ExportHelper.ReadExcel(fileName);
            ExportHelper.ReadMergedExcel(fileName);

            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            string aa = "";
            ExportHelper.SeparateDt(ref dt1,ref dt2,out aa );

            List<Model1> list = new List<Model1>();
            Model1 model1 = new Model1();
            model1.TableName = "a1";
            model1.RelationTableName = "b1";
            model1.Fields = new List<string>() { "AA", "BB" };
            list.Add(model1);

            Model1 model2 = new Model1();
            model2.TableName = "b1";
            model2.RelationTableName = "c1";
            model2.Fields = new List<string>() { "CC", "DD","EE" };
            list.Add(model2);

            foreach (var item in  list)
            {

                foreach (DataColumn columns in dt.Columns)
                {

                }
                foreach (DataRow row in dt.Rows)
                {

                    var rowss = row;
                }

                for (int i=0;i<dt.Rows.Count;i++)
                {
                    a1 a = new a1();
                    foreach (var f in item.Fields)
                    {
                        a.AA = dt.Rows[i][f].ToString();
                    }
                    
                }

            }

            return "value";
        }
        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {

        }

        public class Model1
        {
            public string TableName { set; get; }

            public string RelationTableName { set; get; }

            public List<string> Fields { set; get; }
        }

        public class a1
        {
            public string AA { set; get; }
            public string BB { set; get; }
        }

        public class b1
        {
            public string CC { set; get; }
            public string DD { set; get; }

            public string EE { set; get; }
        }
    }
}
