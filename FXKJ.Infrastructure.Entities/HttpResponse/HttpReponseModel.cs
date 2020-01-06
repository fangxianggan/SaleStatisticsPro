using FXKJ.Infrastructure.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FXKJ.Infrastructure.Entities.HttpResponse
{
    /// <summary>
    /// httpreponse
    /// </summary>
    public class HttpReponseModel
    {
        public HttpReponseModel()
        {
            ResultSign = ResultSign.Error;
            Message = HttpReponseMessage.ErrorMsg;
            Code = 20000;
        }
        public HttpReponseModel(HttpReponseModel httpReponse)
        {
            ResultSign = httpReponse.ResultSign;
            Message = httpReponse.Message;
            FormatParams = httpReponse.FormatParams;
        }
        #region 属性

        /// <summary>
        ///     返回标记
        /// </summary>
        public ResultSign ResultSign { get; set; }

        /// <summary>
        ///     消息字符串(有多语言后将删除该属性)
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     消息的参数
        /// </summary>
        public List<string> FormatParams { get; set; }

        /// <summary>
        /// 状态码
        /// </summary>
        public int Code { set; get; }
        #endregion
    }

    /// <summary>
    /// 返回实体对象 包含分页功能
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HttpReponseModel<T> : HttpReponseModel
    {
        public HttpReponseModel()
        {
            PageIndex = 0;
            PageSize = 0;
            Total = 0;
        }

        public HttpReponseModel(HttpReponseModel<T> httpReponse)
        {
            PageIndex = httpReponse.PageIndex;
            PageSize = httpReponse.PageSize;
            Total = httpReponse.Total;
            Data = httpReponse.Data;
            PageData = httpReponse.PageData;
        }
        public int PageIndex { set; get; }

        public int PageSize { set; get; }

        public long Total { set; get; }
        /// <summary>
        /// 泛型对象
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 当前的分页里的数据
        /// </summary>
        public List<T> PageData { set; get; }

    }
}
